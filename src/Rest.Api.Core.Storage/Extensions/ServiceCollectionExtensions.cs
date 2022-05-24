using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rest.Api.Core.Storage.Models.Options;
using Rest.Api.Core.Storage.Repositories;

namespace Rest.Api.Core.Storage.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<AwsOptions>()
                .Bind(configuration.GetSection(AwsOptions.Section));

            var awsOptions = configuration
                .GetSection(AwsOptions.Section)
                .Get<AwsOptions>();

            services.AddSingleton(sp =>
            {
                if (awsOptions.UseLocal)
                {
                    var credential = new BasicAWSCredentials(awsOptions.AccessKey, awsOptions.SecretKey);

                    return new AmazonDynamoDBClient(credential, Amazon.RegionEndpoint.EUWest2);
                }
                return new AmazonDynamoDBClient();
            });

            services.AddSingleton<ITransactionRepository, TransactionRepository>();
            services.AddSingleton<IDynamoDBContextFactory, DynamoDBContextFactory>();
        }
    }
}
