using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Rest.Api.Core.Storage.Models;

namespace Rest.Api.Core.Storage.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDynamoDBContext _dBContext;

        public TransactionRepository(IDynamoDBContextFactory dynamoDBContextFactory)
        {
            _ = dynamoDBContextFactory ?? throw new ArgumentNullException(nameof(dynamoDBContextFactory));
            _dBContext = dynamoDBContextFactory.GetDynamoDBContext();
        }

        public async Task<List<TransactionDto>> GetUserTransactionsAsync(string userEmail, DateTime start, DateTime end)
        {
            var operationConfig = new DynamoDBOperationConfig
            {
                IndexName = DynamoDbConstants.TransactionsUserDateIndexName

            };
            var sortKeyValues = new List<object> { start, end };

            return await _dBContext
                .QueryAsync<TransactionDto>(userEmail, QueryOperator.Between, sortKeyValues, operationConfig)
                .GetRemainingAsync();
        }

        public async Task UpsertAsync(TransactionDto transaction)
        {
            await _dBContext.SaveAsync(transaction);
        }
    }
}
