using DataHarvester.Worker.Core.Interfaces;
using DataHarvester.Worker.Infrastructure.Harvester;
using MassTransit;

namespace DataHarvester.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddSingleton<IWebNavigator, HarvesterEngine>();
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<HarvestJobConsumer>();
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

                    cfg.ReceiveEndpoint("created-job-queue", e =>
                    {
                        e.ConfigureConsumer<HarvestJobConsumer>(context);
                    });
                });
            });

            var host = builder.Build();
            host.Run();
        }
    }
}