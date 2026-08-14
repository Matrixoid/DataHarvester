namespace DataHarvester.Orchestrator.Domain.Jobs
{
    /// <summary>
    /// Статус задачи.
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// Задача была создана.
        /// </summary>
        Created,

        /// <summary>
        /// Задача добавлена в очередь на исполнение.
        /// </summary>
        Queued,

        /// <summary>
        /// Задача выполняется.
        /// </summary>
        InProgress,

        /// <summary>
        /// Задача успешно завершена.
        /// </summary>
        Completed,

        /// <summary>
        /// Задача была отменена.
        /// </summary>
        Canceled,

        /// <summary>
        /// Задача завершилась с ошибкой.
        /// </summary>
        Failed,

        /// <summary>
        /// Задача завершилась по истечении времени.
        /// </summary>
        TimedOut
    }
}
