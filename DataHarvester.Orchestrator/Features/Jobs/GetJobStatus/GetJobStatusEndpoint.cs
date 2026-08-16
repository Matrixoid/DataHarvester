using MediatR;

namespace DataHarvester.Orchestrator.Features.Jobs.GetJobStatus
{
    /// <summary>
    /// Содержит конфигурацию HTTP-эндпоинтов для получения информации о задаче.
    /// </summary>
    public static class GetJobStatusEndpoint
    {
        /// <summary>
        /// Регистрирует GET-маршрут для получения статуса задачи по её идентификатору.
        /// </summary>
        public static void MapGetJobStatusEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/jobs/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var command = new GetJobStatusQuery(id);
                var jobStatus = await mediator.Send(command);

                if (jobStatus == null)
                {
                    return Results.NotFound(new { Message = $"Задача с ID {id} не найдена." });
                }

                return Results.Ok(jobStatus);
            })
            .WithName("GetJobStatus")
            .WithSummary("Получает статус задачи")
            .WithDescription("Возвращает текущее состояние задачи по её уникальному идентификатору.")
            .Produces<JobStatusResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);
        }
    }
}
