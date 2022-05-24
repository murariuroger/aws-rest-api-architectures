using Rest.Api.Core.Storage.Models;

namespace Rest.Api.Core.Storage.Repositories
{
    public interface ITransactionRepository
    {
        Task UpsertAsync(TransactionDto transaction);
        Task<List<TransactionDto>> GetUserTransactionsAsync(string userEmail, DateTime start, DateTime end);
    }
}
