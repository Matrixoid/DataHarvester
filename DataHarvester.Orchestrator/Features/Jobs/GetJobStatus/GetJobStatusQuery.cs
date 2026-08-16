using MediatR;

namespace DataHarvester.Orchestrator.Features.Jobs.GetJobStatus
{
    /// <summary>
    /// Запрос на получение текущего статуса задачи.
    /// </summary>
    /// <param name="JobId">Идентификатор задачи.</param>
    public record GetJobStatusQuery(Guid JobId) : IRequest<JobStatusResponse?>;
}
