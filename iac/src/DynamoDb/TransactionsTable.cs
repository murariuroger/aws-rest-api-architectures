using Amazon.CDK.AWS.DynamoDB;
using Constructs;
using Rest.Api.Core.Storage;
using Rest.Api.Core.Storage.Models;

namespace Rest.Api.Infrastructure.CDK.DynamoDb
{
    internal class TransactionsTable : Table
    {
        private static ITableProps _props = new TableProps
        {
            TableName = DynamoDbConstants.TransactionsTableName,
            BillingMode = BillingMode.PROVISIONED,
            PartitionKey = new Attribute
            {
                Name = nameof(TransactionDto.TransactionId),
                Type = AttributeType.STRING
            },
            Encryption = TableEncryption.AWS_MANAGED,
            ReadCapacity = 10,
            WriteCapacity = 10
        };

        public TransactionsTable(Construct scope) : base(scope, DynamoDbConstants.TransactionsTableName, _props)
        {
            this.AddGlobalSecondaryIndex(new GlobalSecondaryIndexProps
            {
                IndexName = DynamoDbConstants.TransactionsUserDateIndexName,
                PartitionKey = new Attribute { Name = nameof(TransactionDto.UserEmail), Type = AttributeType.STRING },
                SortKey = new Attribute { Name = nameof(TransactionDto.Date), Type = AttributeType.STRING },
                ReadCapacity = 10,
                WriteCapacity = 10,
                ProjectionType = ProjectionType.ALL
            });

            this.AutoScaleReadCapacity(new EnableScalingProps { MinCapacity = 10, MaxCapacity = 10000 })
                .ScaleOnUtilization(new UtilizationScalingProps { TargetUtilizationPercent = 85 });

            this.AutoScaleWriteCapacity(new EnableScalingProps { MinCapacity = 10, MaxCapacity = 10000 })
                .ScaleOnUtilization(new UtilizationScalingProps { TargetUtilizationPercent = 85 });

            this.AutoScaleGlobalSecondaryIndexReadCapacity(DynamoDbConstants.TransactionsUserDateIndexName, new EnableScalingProps { MinCapacity = 10, MaxCapacity = 10000 })
                .ScaleOnUtilization(new UtilizationScalingProps { TargetUtilizationPercent = 85 });

            this.AutoScaleGlobalSecondaryIndexWriteCapacity(DynamoDbConstants.TransactionsUserDateIndexName, new EnableScalingProps { MinCapacity = 10, MaxCapacity = 10000 })
                .ScaleOnUtilization(new UtilizationScalingProps { TargetUtilizationPercent = 85 });
        }
    }
}
