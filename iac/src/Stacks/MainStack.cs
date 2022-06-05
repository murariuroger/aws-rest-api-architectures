using Amazon.CDK;
using Constructs;
using Rest.Api.Infrastructure.CDK.Constructs;
using Rest.Api.Infrastructure.CDK.DynamoDb;

namespace Rest.Api.Infrastructure.CDK.Stacks
{
    internal class MainStack : Stack
    {
        public MainStack(Construct scope, string id, IStackProps props): base(scope, id, props)
        {
            #region DynamoDB
            var transactionsTable = new TransactionsTable(this);
            #endregion

            #region RestAPI inside AWS Lambda
            var restApiInsideLambdaProps = new RestApiInsideLambdaProps
            {
                DynamoDbTable = transactionsTable,
                LambdaDescription = "Minimal APIs running inside AWS Lambda.",
                LambdaHandler = "Rest.Api",
                LambdaAssembliesPath = "./Assets/Rest.Api/",
                RestApiName = "RestApiInsideLambdaApiGateway",
                ApiGatewayStageName = "Production"
            };
            var restApiInsideLambda = new RestApiInsideLambda(this, "RestApiInsideLambda", restApiInsideLambdaProps);
            #endregion
        }
    }
}
