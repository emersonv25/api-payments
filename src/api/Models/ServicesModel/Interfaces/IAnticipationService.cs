using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.EntityModel;

namespace api.Models.ServicesModel.Interfaces
{
    public interface IAnticipationService
    {
        Task<List<Transaction>> GetTransactionAvailable();
        Task<List<Anticipation>> GetAnticipations(int filter);
        Task<Anticipation> RequestAnticipation(List<long> transactionIds);
        Task<Anticipation> StartAnticipation(long anticipationId);
        Task<List<Transaction>> ApproveAnticipation(List<long> transactionIds);
        Task<List<Transaction>> DisapproveAnticipation(List<long> transactionIds);


    }
}