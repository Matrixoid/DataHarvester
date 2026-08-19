using DataHarvester.Orchestrator.Domain.Repository;
using DataHarvester.Orchestrator.Features.Jobs.CreateJob;
using DataHarvester.Orchestrator.Features.Jobs.GetJobStatus;
using DataHarvester.Orchestrator.Infrastructure;
using DataHarvester.Orchestrator.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddDbContext<HarvesterDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddScoped<IHarvestingJobRepository, HarvestingJobRepository>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<HarvesterOrchestratorConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var host = builder.Configuration["RabbitMQ:Host"];
                    var username = builder.Configuration["RabbitMQ:Username"];
                    var password = builder.Configuration["RabbitMQ:Password"];

                    cfg.Host(host, "/", h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    cfg.ReceiveEndpoint("completed-job-queue", e =>
                    {
                        e.ConfigureConsumer<HarvesterOrchestratorConsumer>(context);
                    });
                });
            });

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
