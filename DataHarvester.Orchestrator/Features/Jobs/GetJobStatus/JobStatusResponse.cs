using DataHarvester.Orchestrator.Domain.Jobs;

namespace DataHarvester.Orchestrator.Features.Jobs.GetJobStatus
{
    /// <summary>
    /// Модель ответа, содержащая публичную информацию о задаче.
    /// </summary>
    /// <param name="JobId">Идентификатор задачи.</param>
    /// <param name="Url">URL адрес страницы, с которой собираются данные.</param>
    /// <param name="Status">Текущий статус выполнения задачи.</param>
    public record JobStatusResponse(Guid JobId, string Url, JobStatus Status);
}
