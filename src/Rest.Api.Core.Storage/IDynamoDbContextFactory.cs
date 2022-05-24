using Amazon.DynamoDBv2.DataModel;

namespace Rest.Api.Core.Storage
{
    public interface IDynamoDBContextFactory
    {
        public IDynamoDBContext GetDynamoDBContext();
    }
}
