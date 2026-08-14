using DataHarvester.Orchestrator.Domain.Jobs;
using DataHarvester.Orchestrator.Domain.Repository;
using System.Collections.Concurrent;

namespace DataHarvester.Orchestrator.Infrastructure.Repositories
{
    public class HarvestingJobRepository : IHarvestingJobRepository
    {
        private readonly ConcurrentDictionary<Guid, HarvestingJob> _jobs = new();

        public void Add(HarvestingJob job)
        {
            _jobs.TryAdd(job.Id, job);
        }

        public HarvestingJob? GetById(Guid id)
        {
            _jobs.TryGetValue(id, out var job);
            return job;
        }
    }
}
