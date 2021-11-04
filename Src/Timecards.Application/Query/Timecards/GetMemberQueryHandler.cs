using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Timecards.Application.Interfaces;
using Timecards.Application.Model;

namespace Timecards.Application.Query.Timecards
{
    public class GetMemberQueryHandler : IRequestHandler<GetTimecardsQuery, IList<TimecardsModel>>
    {
        private readonly IRepository<Domain.Timecards> _repository;

        public GetMemberQueryHandler(IRepository<Domain.Timecards> repository)
        {
            _repository = repository;
        }

        public async Task<IList<TimecardsModel>> Handle(GetTimecardsQuery request, CancellationToken cancellationToken)
        {
            return await _repository
                .Query()
                .Where(x => x.WorkDay >= request.StartDate && x.WorkDay <= request.EndDate &&
                            x.AccountId == request.AccountId)
                .Select(x => new TimecardsModel
                {
                    WorkDay = x.WorkDay,
                    Hour = x.Hour,
                    Note = x.Note,
                    TimecardsType = x.TimecardsType
                })
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}