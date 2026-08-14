using DataHarvester.Orchestrator.Domain.Primitives;

namespace DataHarvester.Orchestrator.Domain.Jobs
{
    public class HarvestingJob
    {
        /// <summary>
        /// Идентификационный номер задачи.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Url страницы, с которой происходит сбор данных в рамках задачи.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Статус исполнения задачи.
        /// </summary>
        public JobStatus Status { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса, задавая ему id, url обрабатываемой страницы и статус задачи.
        /// </summary>
        /// <param name="url">Url обрабатываемой страницы</param>
        /// <exception cref="ArgumentNullException">Бросается, если url пустой.</exception>
        public HarvestingJob(string url)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(url);

            Id = Guid.NewGuid();
            Url = url;
            Status = JobStatus.Created;
        }

        /// <summary>
        /// Переводит задачу в очередь на исполнение.
        /// </summary>
        /// <returns><see cref="Result"/>.</returns>
        public Result AddInOrder()
        {
            if (Status != JobStatus.Created)
            {
                return Result.Failure($"Задача не может быть добавлена в очередь на исполнения, так как она находится в статусе {Status}.");
            }
            Status = JobStatus.Queued;
            return Result.Success();
        }

        /// <summary>
        /// Запускает выполнение задачи.
        /// </summary>
        /// <returns><see cref="Result"/>.</returns>
        public Result StartJob()
        {
            if (Status != JobStatus.Queued)
            {
                return Result.Failure("Задача не может быть исполнена, так как не находится в очереди.");
            }
            Status = JobStatus.InProgress;
            return Result.Success();
        }

        /// <summary>
        /// Завершает выполнение задачи, в котором возникли ошибки.
        /// </summary>
        /// <param name="errorMessage">Сообщение об ошибке.</param>
        public void Fail(string errorMessage)
        {
            Status = JobStatus.Failed;
        }

        /// <summary>
        /// Отменяет выполнение задачи.
        /// </summary>
        public void Cancel()
        {
            Status = JobStatus.Canceled;
        }

        /// <summary>
        /// Успешно завершает выполнение задачи.
        /// </summary>
        public void Complete()
        {
            Status = JobStatus.Completed;
        }

        /// <summary>
        /// Завершает выполнение задачи по истечении времени.
        /// </summary>
        public void TimeOut()
        {
            Status = JobStatus.TimedOut;
        }

    }
}
