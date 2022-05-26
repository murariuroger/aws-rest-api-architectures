using Microsoft.AspNetCore.Mvc;
using Rest.Api.Core.Storage.Extensions;
using Rest.Api.Core.Storage.Models;
using Rest.Api.Core.Storage.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
    .AddEnvironmentVariables();

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStorage(builder.Configuration);

// Add AWS Lambda support.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

var app = builder.Build();

app.MapGet("/transactions", 
    async ([FromServices] ITransactionRepository transactionRepository, 
           [FromQuery(Name = "username")] string username,
           [FromQuery(Name = "startDate")] DateTime startDate,
           [FromQuery(Name = "endDate")] DateTime endDate) 
    => await transactionRepository.GetUserTransactionsAsync(username, startDate, endDate)
);

app.MapPost("/transactions", 
    async ([FromServices] ITransactionRepository transactionRepository, [FromBody] TransactionDto request) 
    => await transactionRepository.UpsertAsync(request)
);

app.Lifetime.ApplicationStarted.Register(() => app.Logger.LogInformation("REST API is now online. (Environment: {Environment})", builder.Environment.EnvironmentName));
app.Lifetime.ApplicationStopped.Register(() => app.Logger.LogInformation("REST API was stopped. (Environment: {Environment})", builder.Environment.EnvironmentName));

app.Run();