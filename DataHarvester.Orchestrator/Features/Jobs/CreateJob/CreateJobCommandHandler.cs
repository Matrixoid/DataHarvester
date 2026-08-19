using DataHarvester.Contracts;
using DataHarvester.Orchestrator.Domain.Jobs;
using DataHarvester.Orchestrator.Domain.Repository;
using MassTransit;
using MediatR;

namespace DataHarvester.Orchestrator.Features.Jobs.CreateJob
{
    /// <summary>
    /// Обработчик команды <see cref="CreateJobCommand"/>.
    /// Отвечает за инициализацию валидной доменной сущности задачи, её сохранение в хранилище
    /// и публикацию события в RabbitMQ для передачи воркеру.
    /// </summary>
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Guid>
    {
        private readonly IHarvestingJobRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        /// <summary>
        /// Инициализирует новый экземпляр объекта с заданным параметром <paramref name="repository"/> и <paramref name="publishEndpoint"/>.
        /// </summary>
        /// <param name="repository">Хранилище задач.</param>
        /// <param name="publishEndpoint">Интерфейс MassTransit для публикации сообщений.</param>
        public CreateJobCommandHandler(IHarvestingJobRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        /// <summary>
        /// Обрабатывает команду <see cref="CreateJobCommand"/>.
        /// </summary>
        /// <param name="request">Обрабатываемая команда.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор созданной задачи.</returns>
        public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = new HarvestingJob(request.Url);
            _repository.Add(job);

            await _publishEndpoint.Publish(new JobMessage(job.Id, job.Url), cancellationToken);

            return job.Id;
        }
    }
}
