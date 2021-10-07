using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Models.EntityModel;
using api.Models.ViewModel;

namespace api.Models.ServicesModel
{
    public class TransactionProcessing
    {
        private readonly AppDbContext _context;
        
        public TransactionProcessing(AppDbContext context)
        {
            _context = context;
        }
        
        public Transaction transaction { get; private set; }
        
        public async Task<Transaction> Process(Transaction payment)
        {
            transaction = payment;
            
            // Validations    
            if (transaction.CreditCardNumber.Substring(0, 4) == "5999")
            {
                transaction.DisapprovedAt = DateTime.Now;
                transaction.Acquirer = false;
                //_context.Transactions.Add(transaction);
                //await _context.SaveChangesAsync();
                return transaction;
            }   

            transaction.Acquirer = true;
            transaction.ApprovedAt = DateTime.Now;
            transaction.Fee = (decimal)0.90;
            transaction.NetAmount = transaction.Amount - transaction.Fee;

            List<Installment> installments = new List<Installment>();
            for(int i = 1; i <= transaction.InstallmentsNumber; i++)
            {
                Installment installment = new Installment();
                installment.TransactionId = transaction.Id;
                installment.Amount = transaction.Amount/transaction.InstallmentsNumber;
                installment.NetAmount = transaction.NetAmount/transaction.InstallmentsNumber;
                installment.InstallmentNumber = i;
                installment.ForecastPaymentAt = transaction.ApprovedAt.Value.AddDays(30*i);
                installment.Transaction = transaction;
                installments.Add(installment);
            }
            transaction.Installments = installments;
            // newTransaction.LastFourDigtCard = payment.CreditCard.Substring(CreditCardNumber.Length -4)
            //_context.Transactions.Add(newTransaction);
            //await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction> FindTransaction(long transactionId)
        { 
        
            Transaction Transaction = await _context.Transactions.FindAsync(transactionId);

            return Transaction;
        }

    }
}