using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.IAM;
using Constructs;

namespace Rest.Api.Infrastructure.CDK.IAM
{
    internal class DynamoDBLambdaRole : Role
    {
        public DynamoDBLambdaRole(Construct scope, string id, RoleProps props, ITable dynamoDbTable) : base(scope, id, props)
        {
            AddToPolicy(new PolicyStatement(
                new PolicyStatementProps
                {
                    Effect = Effect.ALLOW,
                    Resources = new string[] { "*" },
                    Actions = new string[]
                    {
                        "logs:CreateLogGroup",
                        "logs:CreateLogStream",
                        "logs:PutLogEvents"
                    }
                }));

            AddToPolicy(new PolicyStatement(
                new PolicyStatementProps
                {
                    Effect = Effect.ALLOW,
                    Resources = new string[]
                    {
                        dynamoDbTable.TableArn,
                        $"{dynamoDbTable.TableArn}/index/*",
                    },
                    Actions = new string[]
                    {
                        "dynamodb:BatchGet*",
                        "dynamodb:DescribeStream",
                        "dynamodb:DescribeTable",
                        "dynamodb:Get*",
                        "dynamodb:Query",
                        "dynamodb:Scan",
                        "dynamodb:BatchWrite*",
                        "dynamodb:CreateTable",
                        "dynamodb:Delete*",
                        "dynamodb:Update*",
                        "dynamodb:PutItem"
                    }
                }));
        }
    }
}
