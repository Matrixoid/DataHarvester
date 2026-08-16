using DataHarvester.Orchestrator.Domain.Repository;
using DataHarvester.Orchestrator.Features.Jobs.CreateJob;
using DataHarvester.Orchestrator.Features.Jobs.GetJobStatus;
using DataHarvester.Orchestrator.Infrastructure.Repositories;

namespace DataHarvester.Orchestrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });
            builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddSingleton<IHarvestingJobRepository, HarvestingJobRepository>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapCreateJobEndpoint();
            app.MapGetJobStatusEndpoint();
            app.Run();
        }
    }
}
