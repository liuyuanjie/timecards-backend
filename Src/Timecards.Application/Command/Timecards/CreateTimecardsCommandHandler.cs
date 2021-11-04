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
            var timecards = request.Timecardses.Select(x =>
                Domain.Timecards.CreateTimecardsRecord(request.AccountId, x.WorkDay, x.Hour, x.Note, x.TimecardsType));

            await _repository.CreateAsync(timecards);
            var result = await _repository.UnitOfWork.CommitAsync(cancellationToken);
            
            return result > 0;
        }
    }
}