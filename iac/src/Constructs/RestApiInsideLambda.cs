using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Constructs;
using Rest.Api.Infrastructure.CDK.ApiGateway;
using Rest.Api.Infrastructure.CDK.IAM;

namespace Rest.Api.Infrastructure.CDK.Constructs
{
    /// <summary>
    /// RestApiInsideLambda construct is responsible for creating:
    /// - IAM Role for Lambda
    /// - Lambda function
    /// - API Gateway which proxies all requests to Lambda
    /// </summary>
    internal class RestApiInsideLambda : Construct
    {
        public RestApiInsideLambda(Construct scope, string id, RestApiInsideLambdaProps props) : base(scope, id)
        {
            // IAM
            var roleProps = new RoleProps()
            {
                RoleName = $"{props.Name}LambdaRole",
                AssumedBy = new ServicePrincipal("lambda.amazonaws.com")
            };
            var lambdaRole = new DynamoDBLambdaRole(this, $"{props.Name}LambdaRole", roleProps, props.DynamoDbTable);

            // Lambda
            var lambdaProps = new FunctionProps
            {
                Description = props.LambdaDescription,
                Handler = props.LambdaHandler,
                Code = Code.FromAsset(props.LambdaAssembliesPath),
                Runtime = Runtime.DOTNET_6,
                Timeout = Duration.Seconds(90),
                LogRetention = Amazon.CDK.AWS.Logs.RetentionDays.TWO_MONTHS,
                Role = lambdaRole,
                MemorySize = 2500,
                Tracing = Tracing.ACTIVE
            };
            var lambda = new Function(scope, $"{props.Name}Function", lambdaProps);

            // ApiGateway
            var restApiProps = new RestApiProps
            {
                Description = $"Proxy for {lambda.FunctionArn}",
                DeployOptions = new StageOptions()
                {
                    StageName = "Production"
                }
            };
            var apiGateway = new ProxyRestApi(this, $"{props.Name}ApiGateway", restApiProps, lambda);
        }
    }
}
