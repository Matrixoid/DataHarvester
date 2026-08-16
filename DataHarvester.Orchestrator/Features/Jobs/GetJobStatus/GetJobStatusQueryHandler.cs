using DataHarvester.Orchestrator.Domain.Repository;
using MediatR;

namespace DataHarvester.Orchestrator.Features.Jobs.GetJobStatus
{
    public class GetJobStatusQueryHandler : IRequestHandler<GetJobStatusQuery, JobStatusResponse?>
    {
        private readonly IHarvestingJobRepository _repository;

        public GetJobStatusQueryHandler(IHarvestingJobRepository repository)
        {
            _repository = repository;
        }

        public Task<JobStatusResponse?> Handle(GetJobStatusQuery request, CancellationToken cancellationToken)
        {
            var job = _repository.GetById(request.JobId);

            if (job == null)
                return Task.FromResult<JobStatusResponse?>(null);

            var response = new JobStatusResponse(job.Id, job.Url, job.Status);
            return Task.FromResult<JobStatusResponse?>(response);
        }
    }
}
