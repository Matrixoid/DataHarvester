using DataHarvester.Worker.Core.Interfaces;
using DataHarvester.Worker.Infrastructure.Harvester;
using Microsoft.Playwright;
using System.Runtime.InteropServices;

namespace DataHarvester.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddHostedService<HarvesterService>();
            builder.Services.AddSingleton<IWebNavigator, HarvesterEngine>();

            var host = builder.Build();
            host.Run();
        }
    }
}