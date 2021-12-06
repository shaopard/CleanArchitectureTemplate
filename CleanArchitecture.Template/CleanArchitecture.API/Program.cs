using CleanArchitecture.API.Filters;
using CleanArchitecture.API.Utility;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Profiles;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Persistence;
using FluentValidation.AspNetCore;
using Serilog;

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<FileResultContentTypeOperationFilter>();
});


builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration);

builder.Services.AddControllers(options => options.Filters.Add(typeof(ApiExceptionFilterAttribute)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// might need some NuGet (FlentValidation.AspnetCore) but allows Validations based on the validator that extends AbstractValidator<ValidatedType> without 
// explicitly creating the validator and calling .Validate() on it
builder.Services.AddFluentValidation(service => service.RegisterValidatorsFromAssemblyContaining<MappingProfile>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
