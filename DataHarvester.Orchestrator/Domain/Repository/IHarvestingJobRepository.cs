using DataHarvester.Orchestrator.Domain.Jobs;

namespace DataHarvester.Orchestrator.Domain.Repository
{
    /// <summary>
    /// Интерфейс репозитория задач сбора данных.
    /// </summary>
    public interface IHarvestingJobRepository
    {
        /// <summary>
        /// Добавляет новую задачу по сбору данных в базу данных.
        /// </summary>
        /// <param name="job">Задача сбора данных с интернет-ресурса.</param>
        void Add(HarvestingJob job);
        /// <summary>
        /// Получает задачу по её идентификатору из базы данных.
        /// </summary>
        /// <param name="id">Идентификатор задачи.</param>
        /// <returns>Задача сбора данных.</returns>
        HarvestingJob? GetById(Guid id);
        /// <summary>
        /// Обновляет задачу в базе данных.
        /// </summary>
        /// <param name="job">Задача, которую нужно обновить.</param>
        void Update(HarvestingJob job);
    }
}
