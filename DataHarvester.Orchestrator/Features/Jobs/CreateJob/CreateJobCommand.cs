using MediatR;

namespace DataHarvester.Orchestrator.Features.Jobs.CreateJob
{
    /// <summary>
    /// Команда для создания новой задачи на сбор данных.
    /// </summary>
    /// <param name="Url">URL страницы, с которой мы будем собирать данные.</param>
    public record CreateJobCommand(string Url) : IRequest<Guid>;
}
