# Running REST APIs inside AWS
[![CI](https://github.com/murariuroger/aws-rest-api-architectures/actions/workflows/dotnet.yml/badge.svg)](https://github.com/murariuroger/aws-rest-api-architectures/actions/workflows/dotnet.yml)
# Description
WIP

# Deploy to AWS 
### Prerequisites:
- [CDK Setup](https://docs.aws.amazon.com/cdk/v2/guide/work-with.html#work-with-prerequisites)
### Deployment steps:
1. ```cd /src/[ProjectFolder]```
1. ```dotnet publish -o ../../iac/Assets/[ProjectFolder]```
1. ```cd /iac```
1. ```cdk deploy MainStack```