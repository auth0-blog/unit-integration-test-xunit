This repository contains three folders with the .NET Core projects showing how to use xUnit to create unit and integration tests for C# applications.

The following article describes the implementation details: [Using xUnit to Test your C# Code](https://auth0.com/blog/xunit-to-test-csharp-code/)

## Running the projects

1. Clone the repo: `git clone https://github.com/auth0-blog/unit-integration-test-xunit.git`
2. Move to the `unit-integration-test-xunit` folder (*root folder of the repository*)



### Running the unit test project

1. Move to the `/unit-test/PasswordValidator.Tests` folder
2. Type `dotnet test` in a terminal window to run the tests



### Running the integration test project

1. Move to the `/integration-tests/glossary-web-api-aspnet-core` folder
2. Add Auth0 configuration data to the `appsettings.json` configuration file.
3. Move to the `/integration-tests/Glossary.IntegrationTests` folder
4. Add Auth0 configuration data to the `appsettings.json` configuration file.
5. Type `dotnet test` in a terminal window to run the tests



### Running the integration test with mock project

1. Move to the `/integration-tests-mock/glossary-web-api-aspnet-core` folder
2. Add Auth0 configuration data to the `appsettings.json` configuration file.
3. Move to the `/integration-tests-mock/Glossary.IntegrationTests` folder
4. Add Auth0 configuration data to the `appsettings.json` configuration file.
5. Type `dotnet test` in a terminal window to run the tests



## Requirements:

- [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) installed on your machine
- An [Auth0](https://auth0.com/) account.

