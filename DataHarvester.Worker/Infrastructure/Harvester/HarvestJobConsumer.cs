using DataHarvester.Contracts;
using DataHarvester.Worker.Core.Interfaces;
using MassTransit;

namespace DataHarvester.Worker.Infrastructure.Harvester
{
    /// <summary>
    /// Консьюмер сообщений. 
    /// Слушает очередь RabbitMQ, принимает задачи на сбор данных и запускает браузерный движок.
    /// </summary>
    public class HarvestJobConsumer : IConsumer<JobMessage>
    {
        private readonly IWebNavigator _webNavigator;
        private readonly ILogger<HarvestJobConsumer> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса заданными значениями <paramref name="webNavigator"/> и <paramref name="logger"/>.
        /// </summary>
        /// <param name="webNavigator">Движок навигации и сбора данных.</param>
        /// <param name="logger">Логгер сервиса.</param>
        public HarvestJobConsumer(IWebNavigator webNavigator, ILogger<HarvestJobConsumer> logger)
        {
            _webNavigator = webNavigator;
            _logger = logger;
        }

        /// <summary>
        /// Метод обработки входящего сообщения из очереди.
        /// </summary>
        /// <param name="context">Контекст сообщения, содержащий данные задачи.</param>
        public async Task Consume(ConsumeContext<JobMessage> context)
        {
            var message = context.Message;

            _logger.LogInformation($"Начинаем собирать данные со страницы {message.Url}.");

            try
            {
                var harvestData = await _webNavigator.ExtractDataAsync(message.Url, context.CancellationToken);
                _logger.LogInformation("Данные успешно собраны.");
                await context.Publish(new ReportMessage(message.JobId, true, harvestData.Title, harvestData.Links.Count));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "В результате сбора данных случилась ошибка.");
                await context.Publish(new ReportMessage(message.JobId, false, ErrorMessage: ex.Message));
            }
        }
    }
}
