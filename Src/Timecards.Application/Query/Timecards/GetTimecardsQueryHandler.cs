using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Timecards.Application.Extensions;
using Timecards.Application.Interfaces;
using Timecards.Application.Model;

namespace Timecards.Application.Query.Timecards
{
    public class GetTimecardsQueryHandler : IRequestHandler<GetTimecardsQuery, IList<GetTimecardsResponse>>
    {
        private readonly IRepository<Domain.Timecards> _repository;
        private readonly IRepository<Domain.Project> _projectRepository;

        public GetTimecardsQueryHandler(IRepository<Domain.Timecards> repository,
            IRepository<Domain.Project> projectRepository)
        {
            _repository = repository;
            _projectRepository = projectRepository;
        }

        public async Task<IList<GetTimecardsResponse>> Handle(GetTimecardsQuery request,
            CancellationToken cancellationToken)
        {
            var timecards = _repository
                .Query()
                .OrderBy(x => x.CreatedDate)
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
                .GroupJoin(
                    _projectRepository.Query(),
                    timecards => timecards.ProjectId,
                    project => project.Id,
                    (timecards, project) => new {Timecards = timecards, project}
                ).SelectMany(
                    x => x.project.DefaultIfEmpty(), (timecards, project) =>
                        new GetTimecardsResponse()
                        {
                            UserId = timecards.Timecards.AccountId,
                            ProjectId = timecards.Timecards.ProjectId,
                            ProjectName = project != null ? project.Name : null,
                            TimecardsDate = timecards.Timecards.TimecardsDate,
                            TimecardsId = timecards.Timecards.Id,
                            StatusType = timecards.Timecards.StatusType,
                            Items = timecards.Timecards.Items.Select(t => new GetTimecardsItemResponse
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