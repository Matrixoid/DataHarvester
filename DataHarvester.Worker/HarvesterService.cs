using DataHarvester.Worker.Core.Interfaces;

namespace DataHarvester.Worker
{
    public class HarvesterService : BackgroundService
    {
        private readonly IWebNavigator _webNavigator;
        private readonly ILogger<HarvesterService> _logger;

        public HarvesterService(IWebNavigator webNavigator, ILogger<HarvesterService> logger)
        {
            _webNavigator = webNavigator;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Сервис запущен.");

            var urls = new List<string>() { "https://habr.com/ru/companies/auriga/articles/727280/", "https://playwright.dev/dotnet/docs/intro" };

            while (!stoppingToken.IsCancellationRequested)
            {

                foreach (var url in urls)
                {
                    _logger.LogInformation($"Начинаем собирать данные со страницы {url}.");
                    try
                    {
                        var result = await _webNavigator.ExtractDataAsync(url, stoppingToken);
                        _logger.LogInformation("Данные успешно собраны.");
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(ex, "В результате сбора данных случилась ошибка.");
                    }
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
