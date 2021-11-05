using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timecards.Application.Interfaces;

namespace Timecards.Application.Command.Timecards
{
    public class CreateTimecardsCommandHandler : IRequestHandler<CreateTimecardsCommand, bool>
    {
        private readonly IRepository<Domain.Timecards> _repository;

        public CreateTimecardsCommandHandler(IRepository<Domain.Timecards> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreateTimecardsCommand request, CancellationToken cancellationToken)
        {
            var timecards = Domain.Timecards.CreateTimecards(request.AccountId, request.Timecards.ProjectId,
                request.Timecards.TimecardsDate);
            request.Timecards.Items.ToList().ForEach(x =>
                timecards.AddTimecardsRecord(x.WorkDay, x.Hour, x.Note));

            await _repository.CreateAsync(timecards);
            var result = await _repository.UnitOfWork.CommitAsync(cancellationToken);

            return result > 0;
        }
    }
}