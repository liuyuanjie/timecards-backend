using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Timecards.Application.Interfaces;
using Timecards.Application.Model;
using Timecards.Domain;

namespace Timecards.Application.Query.Timecards
{
    public class GetTimecardsQueryHandler : IRequestHandler<GetTimecardsQuery, IList<GetTimecardsResponse>>
    {
        private readonly IRepository<Domain.Timecards> _repository;

        public GetTimecardsQueryHandler(IRepository<Domain.Timecards> repository)
        {
            _repository = repository;
        }

        public async Task<IList<GetTimecardsResponse>> Handle(GetTimecardsQuery request, CancellationToken cancellationToken)
        {
            return await _repository
                .Query()
                .AsNoTracking()
                .Where(x => x.AccountId == request.AccountId && x.TimecardsDate == request.StartDate)
                .Include(x => x.Items)
                .Select(x => new GetTimecardsResponse()
                {
                    ProjectId = x.ProjectId,
                    TimecardsDate = x.TimecardsDate,
                    Items = x.Items.Select(t => new GetTimecardsItemResponse
                    {
                        Hour = t.Hour,
                        WorkDay = t.WorkDay,
                        Note = t.Note
                    })
                })
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}