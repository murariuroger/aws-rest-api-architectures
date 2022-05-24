using Amazon.CDK;
using Constructs;
using Rest.Api.Infrastructure.CDK.Constructs;
using Rest.Api.Infrastructure.CDK.DynamoDb;

namespace Rest.Api.Infrastructure.CDK
{
    public class RestApiInfrastructureCdkStack : Stack
    {
        internal RestApiInfrastructureCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            #region DynamoDB
            var transactionsTable = new TransactionsTable(this);
            #endregion

            #region RestAPI inside AWS Lambda
            var restApiInsideLambdaProps = new RestApiInsideLambdaProps
            {
                Name = "RestApiInsideLambda",
                DynamoDbTable = transactionsTable,
                LambdaDescription = "Minimal APIs running inside AWS Lambda.",
                LambdaHandler = "Rest.Api",
                LambdaAssembliesPath = "../src/Rest.Api/bin/Debug/net6.0"
            };
            var restApiInsideLambda = new RestApiInsideLambda(this, "RestApiInsideLambda", restApiInsideLambdaProps);

            #endregion

            #region Cloud Native RestAPI using AWS Lambdas 
            #endregion

            #region RestAPI inside ECS Fargate
            #endregion


        }
    }
}
