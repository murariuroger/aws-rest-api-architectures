using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;

namespace Rest.Api.Infrastructure.CDK.Constructs
{
    internal class RestApiInsideLambdaProps
    {
        /// <summary>
        /// DynamoDB Table that will be used by AWS Lambda.
        /// </summary>
        public ITable DynamoDbTable { get; set; }

        /// <summary>
        /// Lambda description.
        /// </summary>
        public string LambdaDescription { get; set; }

        /// <summary>
        /// Path to the code to run inside AWS Lambda.
        /// </summary>
        public string LambdaAssembliesPath { get; set; }

        /// <summary>
        /// Lambda entry point.
        /// </summary>
        public string LambdaHandler { get; set; }

        /// <summary>
        /// Rest Api Name
        /// </summary>
        public string RestApiName { get; set; }

        /// <summary>
        /// Stage name for Api Gateway
        /// </summary>
        public string ApiGatewayStageName { get; set; }
    }
}
