using DataHarvester.Orchestrator.Domain.Jobs;
using DataHarvester.Orchestrator.Domain.Repository;
using MediatR;

namespace DataHarvester.Orchestrator.Features.Jobs.CreateJob
{
    /// <summary>
    /// Обработчик команды <see cref="CreateJobCommand"/>.
    /// Отвечает за инициализацию валидной доменной сущности задачи и её сохранение в хранилище.
    /// </summary>
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Guid>
    {
        private readonly IHarvestingJobRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр объекта с заданным параметром <paramref name="repository"/>.
        /// </summary>
        /// <param name="repository"></param>
        public CreateJobCommandHandler(IHarvestingJobRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Обрабатывает команду <see cref="CreateJobCommand"/>.
        /// </summary>
        /// <param name="request">Обрабатываемая команда.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор созданной задачи.</returns>
        public Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = new HarvestingJob(request.Url);
            _repository.Add(job);

            return Task.FromResult(job.Id);
        }
    }
}
