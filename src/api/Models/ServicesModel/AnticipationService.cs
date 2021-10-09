using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models.EntityModel;
using api.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace api.Models.ServicesModel
{
    public class AnticipationService
    {
        private readonly AppDbContext _context;
        public decimal amountRequest;
        public AnticipationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Transaction>> GetTransactionAvailable()
        { 
            List<Transaction> Transaction = await _context.Transactions.Where(t => t.AnticipationId == null).ToListAsync();
            
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
        public async Task<dynamic> RequestAnticipation(List<long> transactionIds)
        {
            Anticipation anticipation = new Anticipation();
            List <Transaction> transactions = new List<Transaction>();

            if(await _context.Anticipations.FirstOrDefaultAsync(a => a.Status != 2) != null)
            {
                return null;
            }
            
            foreach(var id in transactionIds)
            {
                Transaction transaction = await _context.Transactions.FindAsync(id);
                if(transaction != null)
                {
                    if(transaction.AnticipationId != null)
                    {
                        return null;
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
        public async Task<dynamic> StartAnticipation(long anticipationId)
        {
            Anticipation anticipation = await _context.Anticipations.FindAsync(anticipationId);
            if(anticipation == null){
                return null;
            }   
            if(anticipation.Status != 0)
            {
                return null;
            }
            anticipation.StartAt = DateTime.Now;
            anticipation.Status = 1;

            await _context.SaveChangesAsync();
            return anticipation;
        }
    }
}