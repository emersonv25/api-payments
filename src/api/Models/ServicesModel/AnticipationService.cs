using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models.EntityModel;
using api.Models.ServicesModel.Interfaces;
using api.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace api.Models.ServicesModel
{
    public class AnticipationService : IAnticipationService
    {
        private readonly AppDbContext _context;
        public decimal amountRequest;
        public AnticipationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Transaction>> GetTransactionAvailable()
        { 
            List<Transaction> Transaction = await _context.Transactions.Where(t => t.AnticipationId == null && t.Acquirer == true).ToListAsync();
            
            return Transaction;
        }
        public async Task<List<Anticipation>> GetAnticipations(int filter)
        { 
            List<Anticipation> Anticipations = new List<Anticipation>();
            switch (filter)
            {
                case 0:
                    Anticipations = await _context.Anticipations.Where(a => a.Status == 0).Include(t => t.TransactionsList).ToListAsync();
                break;

                case 1:
                    Anticipations = await _context.Anticipations.Where(a => a.Status == 1).Include(t => t.TransactionsList).ToListAsync();
                break;

                case 2:
                    Anticipations = await _context.Anticipations.Where(a => a.Status == 2).Include(t => t.TransactionsList).ToListAsync();
                break;
            }

            return Anticipations;
        }
        public async Task<Anticipation> RequestAnticipation(List<long> transactionIds)
        {
            Anticipation anticipation = new Anticipation();
            List <Transaction> transactions = new List<Transaction>();

            if(await _context.Anticipations.FirstOrDefaultAsync(a => a.Status != 2) != null)
            {
                throw new Exception("To carry out a new advance request, it is necessary that the current request has already been completed");
            }
            
            foreach(var id in transactionIds)
            {
                Transaction transaction = await _context.Transactions.FindAsync(id);
                if(transaction != null)
                {
                    if(transaction.Acquirer == false)
                    {
                        throw new Exception("It is not possible to request an anticipation of a disapproved transaction");
                    }
                    if(transaction.AnticipationId != null)
                    {
                        throw new Exception("It is not allowed to include previously requested transactions in a new anticipation request");
                    }
                    amountRequest += transaction.NetAmount;
                    transactions.Add(transaction); 
                }
            }
            anticipation.RequestAt = DateTime.Now;
            anticipation.Status = 0;
            anticipation.AmountRequest = amountRequest;
            anticipation.TransactionsList = transactions;
            
            _context.Anticipations.Add(anticipation);

            await _context.SaveChangesAsync();
            return anticipation;
        }
        public async Task<Anticipation> StartAnticipation(long anticipationId)
        {
            Anticipation anticipation = await _context.Anticipations.FindAsync(anticipationId);
            if(anticipation == null){
                throw new Exception("Anticipation not found!");
            }   
            if(anticipation.Status != 0)
            {
                throw new Exception("Anticipation already started!");
            }
            anticipation.StartAt = DateTime.Now;
            anticipation.Status = 1;

            await _context.SaveChangesAsync();
            return anticipation;
        }
        public async Task<List<Transaction>> ApproveAnticipation(List<long> transactionIds)
        {
            List<Transaction> transactionsList = new List<Transaction>();

            foreach(var id in transactionIds)
            {
                Transaction transaction = _context.Transactions.Include(i => i.Installments).FirstOrDefault(x => x.Id == id);
                if(transaction == null)
                {
                    throw new Exception("One or more transactions not found!");
                }
                if(transaction.Anticipated != null)
                {
                    throw new Exception("A transaction with a pass or fail analysis cannot be modified");
                }
                if(transaction.AnticipationId == null)
                {
                    throw new Exception("There is no anticipation request for one or more transactions");
                }
                Anticipation anticipation =  new Anticipation();
                anticipation =  _context.Anticipations.Include(i => i.TransactionsList).FirstOrDefault(x => x.Id == transaction.AnticipationId);
                if(anticipation.Status != 1)
                {
                    throw new Exception("It is necessary to start the anticipation analysis before approving or disapproving");
                }
                foreach(var installment in transaction.Installments)
                {
                    var fee = (installment.NetAmount * (decimal)0.038);
                    installment.AnticipatedAt = DateTime.Now;
                    installment.AnticipatedAmount = installment.NetAmount - (fee);
                    if(anticipation.AmountApproved == null){anticipation.AmountApproved = installment.AnticipatedAmount; }
                    else{anticipation.AmountApproved += installment.AnticipatedAmount;}
                    
                }
                transaction.Anticipated = true;
                transactionsList.Add(transaction); 
                anticipation = VerifyResultStatus(anticipation);
                
            }

            await _context.SaveChangesAsync();
            return transactionsList;
        }
        public async Task<List<Transaction>> DisapproveAnticipation(List<long> transactionIds)
        {
            List<Transaction> transactionsList = new List<Transaction>();

            foreach(var id in transactionIds)
            {
                Transaction transaction = _context.Transactions.Include(i => i.Installments).FirstOrDefault(x => x.Id == id);
                if(transaction == null)
                {
                    throw new Exception("One or more transactions not found!");
                }
                if(transaction.Anticipated != null)
                {
                    throw new Exception("A transaction with a pass or fail analysis cannot be modified");
                }
                if(transaction.AnticipationId == null)
                {
                    throw new Exception("There is no anticipation request for one or more transactions");
                }
                Anticipation anticipation =  new Anticipation();
                anticipation =  _context.Anticipations.Include(i => i.TransactionsList).FirstOrDefault(x => x.Id == transaction.AnticipationId);
                if(anticipation.Status != 1)
                {
                    throw new Exception("It is necessary to start the anticipation analysis before approving or disapproving");
                }
                transaction.Anticipated = false;
                transactionsList.Add(transaction); 
                anticipation = VerifyResultStatus(anticipation);

            }

            await _context.SaveChangesAsync();
            return transactionsList;
        }
        public static Anticipation VerifyResultStatus(Anticipation anticipation)
        {
            var finished = true;
            var approved = 0;
            var disapproved = 0;
            foreach(var t in anticipation.TransactionsList)
            {
                if(t.Anticipated == null)
                {
                    finished = false;
                }
                if(t.Anticipated == true)
                {
                    approved += 1;
                }
                if(t.Anticipated == false)
                {
                    disapproved += 1;
                }
            }
            if(finished)
            {
                anticipation.EndAt = DateTime.Now;
                anticipation.Status = 2;
                if(approved > 0 && disapproved == 0)
                {
                    anticipation.Result = 1;
                }
                if(approved > 0 && disapproved > 0)
                {
                    anticipation.Result = 2;
                }
                if(disapproved > 0  && approved == 0)
                {
                    anticipation.Result = 0;
                }
            }
            return anticipation;
        }
    }
}