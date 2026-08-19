using DataHarvester.Orchestrator.Domain.Repository;
using MediatR;

namespace DataHarvester.Orchestrator.Features.Jobs.GetJobStatus
{
    /// <summary>
    /// Обработчик запроса получения информации по задаче.
    /// </summary>
    public class GetJobStatusQueryHandler : IRequestHandler<GetJobStatusQuery, JobStatusResponse?>
    {
        private readonly IHarvestingJobRepository _repository;

        /// <summary>
        /// Инициализирует новый экземпляр класса заданным значением <paramref name="repository"/>.
        /// </summary>
        /// <param name="repository">Репозиторий, в котором хранятся заведённые задачи.</param>
        public GetJobStatusQueryHandler(IHarvestingJobRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Обработчик запроса получения информации по задаче.
        /// </summary>
        /// <param name="request">Запрос, который нужно обработать.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Ответ сервиса, в котором хранятся данные по задаче.</returns>
        public Task<JobStatusResponse?> Handle(GetJobStatusQuery request, CancellationToken cancellationToken)
        {
            var job = _repository.GetById(request.JobId);

            if (job == null)
                return Task.FromResult<JobStatusResponse?>(null);

            var response = new JobStatusResponse(job.Id, job.Url, job.Title, job.LinksCount, job.Status);
            return Task.FromResult<JobStatusResponse?>(response);
        }
    }
}
