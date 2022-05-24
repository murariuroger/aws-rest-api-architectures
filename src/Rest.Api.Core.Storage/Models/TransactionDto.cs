using Amazon.DynamoDBv2.DataModel;
using Rest.Api.Core.Storage;

namespace Rest.Api.Core.Storage.Models
{
    [DynamoDBTable(DynamoDbConstants.TransactionsTableName)]
    public class TransactionDto
    {
        [DynamoDBHashKey]
        public string TransactionId { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey]
        public string UserEmail { get; set; }

        [DynamoDBGlobalSecondaryIndexRangeKey]
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public decimal Price { get; set; }

        public decimal OrigQty { get; set; }

        public decimal ExecutedQty { get; set; }

        public decimal CummulativeQuoteQty { get; set; }

        public string TimeInForce { get; set; }

        public string Type { get; set; }

        public string OrderSide { get; set; }

        public string FailedReason { get; set; }
        public decimal UsdtAmount { get; set; }
    }
}
