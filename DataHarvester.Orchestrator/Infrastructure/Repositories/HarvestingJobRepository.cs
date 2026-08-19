using DataHarvester.Orchestrator.Domain.Jobs;
using DataHarvester.Orchestrator.Domain.Repository;

namespace DataHarvester.Orchestrator.Infrastructure.Repositories
{
    /// <summary>
    /// Реализация репозитория для работы с задачами сбора данных.
    /// </summary>
    public class HarvestingJobRepository : IHarvestingJobRepository
    {
        private readonly HarvesterDbContext _dbContext;

        /// <summary>
        /// Инициализирует новый экземпляр класса заданным значением <paramref name="dbContext"/>.
        /// </summary>
        /// <param name="dbContext">Контекст базы данных.</param>
        public HarvestingJobRepository(HarvesterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public void Add(HarvestingJob job)
        {
            _dbContext.Jobs.Add(job);
            _dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public HarvestingJob? GetById(Guid id)
        {
            return _dbContext.Jobs.FirstOrDefault(x => x.Id == id);
        }

        /// <inheritdoc/>
        public void Update(HarvestingJob job)
        {
            _dbContext.Jobs.Update(job);
            _dbContext.SaveChanges();
        }
    }
}
