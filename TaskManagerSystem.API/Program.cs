using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using System.Text.Json.Serialization;
using TaskManagerSystem.API.Middleware;
using TaskManagerSystem.Application.AutoMapper;
using TaskManagerSystem.Domain.Interfaces.Repository;
using TaskManagerSystem.Infra.Data.Context;
using TaskManagerSystem.Infra.Data.Repository;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(cfg =>
                {
                    cfg.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    cfg.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    cfg.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<TaskManagerSystemContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("TaskManagerSystem.Application")));
ConfigureLogging();
builder.Services.AddLogging();
builder.Host.UseSerilog();

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DomainToDtoProfile());
    cfg.AddProfile(new DtoToDomainProfile());
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);



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

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();


void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        //.WriteTo.Debug()
        //.WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}