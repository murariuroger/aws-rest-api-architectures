using Amazon.CDK.AWS.DynamoDB;

namespace Rest.Api.Infrastructure.CDK.Constructs
{
    internal class RestApiInsideLambdaProps
    {
        /// <summary>
        /// Name will be appended to AWS resources.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// DynamoDB Table that will be used by AWS Lambda.
        /// </summary>
        public Table DynamoDbTable { get; set; }

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
        public string LambdaHandler{ get; set; }
    }
}
