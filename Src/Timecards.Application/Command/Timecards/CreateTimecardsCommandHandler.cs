using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timecards.Application.Interfaces;
using Timecards.Domain;

namespace Timecards.Application.Command.Timecards
{
    public class CreateTimecardsCommandHandler : IRequestHandler<CreateTimecardsCommand, bool>
    {
        private readonly IRepository<Domain.Timecards> _repository;
        private readonly IRepository<Project> _projectRepository;

        public CreateTimecardsCommandHandler(IRepository<Domain.Timecards> repository,
            IRepository<Project> projectRepository)
        {
            _repository = repository;
            _projectRepository = projectRepository;
        }

        public async Task<bool> Handle(CreateTimecardsCommand request, CancellationToken cancellationToken)
        {
            if (!_projectRepository.Query().Any(x => x.Id == request.Timecards.ProjectId))
            {
                throw new KeyNotFoundException("Can't find the Project.");
            }

            var timecards = Domain.Timecards.CreateTimecards(request.AccountId, request.Timecards.ProjectId,
                request.Timecards.TimecardsDate);
            request.Timecards.Items.ToList().ForEach(x =>
                timecards.AddTimecardsRecord(x.WorkDay, x.Hour, x.Note));

            _repository.Add(timecards);
            var result = await _repository.UnitOfWork.CommitAsync(cancellationToken);

            return result > 0;
        }
    }
}