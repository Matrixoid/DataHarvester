using DataHarvester.Orchestrator.Domain.Jobs;

namespace DataHarvester.Orchestrator.Domain.Repository
{
    public interface IHarvestingJobRepository
    {
        void Add(HarvestingJob job);
        HarvestingJob? GetById(Guid id);
    }
}
