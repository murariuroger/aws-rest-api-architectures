using Amazon.CDK;

namespace Rest.Api.Infrastructure.CDK
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new RestApiInfrastructureCdkStack(app, "RestApiInfrastructureCdkStack", new StackProps
            {
                Env = new Amazon.CDK.Environment
                {
                    Account = "570298684443",
                    Region = "eu-west-1"
                }
            });
            app.Synth();
        }
    }
}
