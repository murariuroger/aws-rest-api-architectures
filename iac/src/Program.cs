using Amazon.CDK;
using Rest.Api.Infrastructure.CDK.DynamoDb;
using Rest.Api.Infrastructure.CDK.Stacks;

namespace Rest.Api.Infrastructure.CDK
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();

            var mainStack = new MainStack(app, "MainStack", new StackProps
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
