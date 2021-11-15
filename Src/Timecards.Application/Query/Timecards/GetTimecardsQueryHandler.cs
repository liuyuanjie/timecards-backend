using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Timecards.Application.Extensions;
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

        public async Task<IList<GetTimecardsResponse>> Handle(GetTimecardsQuery request,
            CancellationToken cancellationToken)
        {
            var timecards = _repository
                .Query()
                .AsNoTracking();
            
            if (request.UserId.HasValue)
            {
                timecards = timecards.Where(x => x.AccountId == request.UserId);
            }

            if (request.TimecardsDate.HasValue)
            {
                var mondayOfWeekOfWorkDay = request.TimecardsDate.Value.ToUniversalTime().GetFirstDayOfWeek();
                timecards = timecards.Where(x => x.TimecardsDate == mondayOfWeekOfWorkDay);
            }

            return await timecards
                .Include(x => x.Items)
                .Select(x => new GetTimecardsResponse()
                {
                    UserId = x.AccountId,
                    ProjectId = x.ProjectId,
                    TimecardsDate = x.TimecardsDate,
                    TimecardsId = x.Id,
                    StatusType = x.StatusType,
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