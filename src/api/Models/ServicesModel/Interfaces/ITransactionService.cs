
using System.Threading.Tasks;
using api.Models.EntityModel;

namespace api.Models.ServicesModel.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> Process(Transaction payment);
        Task<Transaction> GetTransaction(long transactionId);

    }
}