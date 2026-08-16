using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataHarvester.Orchestrator.Features.Jobs.CreateJob
{
    /// <summary>
    /// Содержит конфигурацию HTTP-эндпоинтов для фичи создания задачи.
    /// </summary>
    public static class CreateJobEndpoint
    {
        /// <summary>
        /// Регистрирует POST-маршрут для создания новой задачи на сбор данных.
        /// </summary>
        /// <param name="app">Построитель маршрутов эндпойнтов.</param>
        public static void MapCreateJobEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/jobs", async ([FromBody] CreateJobCommand command, IMediator mediator) =>
            {
                var jobId = await mediator.Send(command);

                return Results.Ok(new { JobId = jobId });
            })
            .WithName("Create Job")
            .WithSummary("Создаёт новую задачу на сбор данных с интернет ресурса.");

            app.MapOpenApi();
        }
    }
}
