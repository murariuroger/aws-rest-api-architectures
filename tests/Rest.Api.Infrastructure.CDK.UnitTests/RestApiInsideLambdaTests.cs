using Amazon.CDK.Assertions;
using Amazon.CDK.AWS.DynamoDB;
using Moq;
using Rest.Api.Infrastructure.CDK.Constructs;
using System.Collections.Generic;
using Xunit;
using Match = Amazon.CDK.Assertions.Match;
using Stack = Amazon.CDK.Stack;

namespace Rest.Api.Infrastructure.CDK.UnitTests
{
    public class RestApiInsideLambdaTests
    {
        private readonly Stack _sut;
        private const string LambdaDescription = nameof(LambdaDescription);
        private const string LambdaHandler = nameof(LambdaHandler);
        private const string RestApiName = nameof(RestApiName);
        private const string ApiGatewayStageName = nameof(ApiGatewayStageName);
        private const string LambdaAssembliesPath = "Assets/";

        public RestApiInsideLambdaTests()
        {
            _sut = new Stack();
            var dynamoDbMock = new Mock<ITable>();
            dynamoDbMock
                .Setup(_ => _.TableArn)
                .Returns("TableArn");

            var contruct = new RestApiInsideLambda(_sut, "RestApiInsideLambda", new RestApiInsideLambdaProps
            {
                DynamoDbTable = dynamoDbMock.Object,
                LambdaDescription = LambdaDescription,
                LambdaHandler = LambdaHandler,
                LambdaAssembliesPath = LambdaAssembliesPath,
                RestApiName = RestApiName,
                ApiGatewayStageName = ApiGatewayStageName
            });
        }

        [Fact]
        public void IAM_LambdaRoleCreated_And_HasDynamoDbPolicyAttached()
        {
            var template = Template.FromStack(_sut);

            // Assert
            template.HasResourceProperties("AWS::IAM::Role",
                new Dictionary<string, string>
                {
                    { "RoleName", "LambdaRole"}
                });

            template.HasResourceProperties("AWS::IAM::Policy", Match.ObjectLike(
                new Dictionary<string, object>
                {
                    {
                        "PolicyDocument",
                        Match.ObjectLike(new Dictionary<string, object>
                        {
                            {
                                "Statement",
                                Match.ArrayWith(new object[]
                                {
                                    new Dictionary<string, object>
                                    {
                                        {
                                            "Action",
                                            Match.ArrayWith(new string[]
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
                                            })
                                        },
                                        { "Effect", "Allow" },
                                        { "Resource", Match.AnyValue() }
                                    }
                                })
                            }
                        })
                    },
                    {
                        "Roles",
                        Match.ArrayWith(new object[]
                        {
                            new Dictionary<string, object>
                            {
                                { "Ref", Match.StringLikeRegexp(".*LambdaRole.*") }
                            }
                        })
                    }
                }));
        }

        [Fact]
        public void Lambda_Created()
        {
            var template = Template.FromStack(_sut);

            // Assert
            template.HasResourceProperties("AWS::Lambda::Function",
                new Dictionary<string, string>
                {
                    { "Handler", LambdaHandler },
                    { "Runtime", "dotnet6" }
                });
        }

        [Fact]
        public void APIGatewayRestApi_Created()
        {
            var template = Template.FromStack(_sut);

            // Assert
            template.HasResourceProperties("AWS::ApiGateway::RestApi",
                new Dictionary<string, string>
                {
                    { "Name", RestApiName }
                });
        }

        [Fact]
        public void APIGatewayStage_Created()
        {
            var template = Template.FromStack(_sut);

            // Assert
            template.HasResourceProperties("AWS::ApiGateway::Stage",
                new Dictionary<string, string>
                {
                    { "StageName", ApiGatewayStageName }
                });
        }

        [Fact]
        public void APIGatewayProxyResource_Created()
        {
            var template = Template.FromStack(_sut);

            // Assert
            template.HasResourceProperties("AWS::ApiGateway::Resource",
                new Dictionary<string, string>
                {
                    { "PathPart", "{proxy+}"}
                });
        }


        [Fact]
        public void APIGatewayMethods_Created()
        {
            var template = Template.FromStack(_sut);

            // Assert
            template.ResourceCountIs("AWS::ApiGateway::Method", 4);
        }
    }
}