using DataHarvester.Contracts;
using DataHarvester.Orchestrator.Domain.Repository;
using MassTransit;
using MassTransit.Transports;

namespace DataHarvester.Orchestrator.Infrastructure
{
    public class HarvesterOrchestratorConsumer : IConsumer<ReportMessage>
    {
        private readonly IHarvestingJobRepository _harvestingJobRepository;
        private readonly ILogger<HarvesterOrchestratorConsumer> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр класса заданными значениями <paramref name="logger"/>.
        /// </summary>
        /// <param name="harvestingJobRepository">Репозиторий, в котором хранятся задачи.</param>
        /// <param name="logger">Логгер сервиса.</param>
        public HarvesterOrchestratorConsumer(IHarvestingJobRepository harvestingJobRepository, ILogger<HarvesterOrchestratorConsumer> logger)
        {
            _harvestingJobRepository = harvestingJobRepository;
            _logger = logger;
        }

        /// <summary>
        /// Метод обработки входящего сообщения из очереди.
        /// </summary>
        /// <param name="context">Контекст сообщения, содержащий данные задачи.</param>
        public Task Consume(ConsumeContext<ReportMessage> context)
        {
            var message = context.Message;

            _logger.LogInformation("Пришло сообщение о завершении задачи. Обновляем данные в задаче.");

            var job = _harvestingJobRepository.GetById(message.JobId);
            var result = message.IsSuccess;

            if (job == null)
                return Task.CompletedTask;

            if (!result)
                job.Fail(message.ErrorMessage);
            else
                job.Complete(message.Title, message.LinksCount);

            _harvestingJobRepository.Update(job);
            return Task.CompletedTask;
        }
    }
}
