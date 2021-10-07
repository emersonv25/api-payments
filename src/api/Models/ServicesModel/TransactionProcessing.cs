using System.Threading.Tasks;
using api.Data;
using api.Models.EntityModel;

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
        
        public async Task<Transaction> Process(Transaction transaction)
        {
            Transaction newTransaction = transaction;
            // Validations           
            _context.Transactions.Add(newTransaction);
            await _context.SaveChangesAsync();

            return newTransaction;
        }

        public async Task<Transaction> FindTransaction(long transactionId)
        { 
        
            Transaction Transaction = await _context.Transactions.FindAsync(transactionId);

            return Transaction;
        }

    }
}