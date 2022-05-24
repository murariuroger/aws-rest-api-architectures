using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Constructs;

namespace Rest.Api.Infrastructure.CDK.ApiGateway
{
    internal class ProxyRestApi : RestApi
    {
        public ProxyRestApi(Construct scope, string id, RestApiProps props, Function function) : base(scope, id, props)
        {
            var proxyResource = Root.AddResource("{proxy+}");
            var integration = new LambdaIntegration(function);

            proxyResource.AddMethod("GET", integration);
            proxyResource.AddMethod("POST", integration);
            proxyResource.AddMethod("PUT", integration);
            proxyResource.AddMethod("DELETE", integration);
        }
    }
}
