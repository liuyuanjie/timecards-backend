using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Timecards.Application.Extensions;
using Timecards.Application.Interfaces;
using Timecards.Domain;

namespace Timecards.Application.Command.Timecards
{
    public class AddTimecardsCommandHandler : IRequestHandler<AddTimecardsCommand, bool>
    {
        private readonly IRepository<Domain.Timecards> _repository;
        private readonly IRepository<Project> _projectRepository;
        private readonly UserManager<Domain.Account> _userManager;

        public AddTimecardsCommandHandler(IRepository<Domain.Timecards> repository,
            IRepository<Project> projectRepository,
            UserManager<Domain.Account> userManager)
        {
            _repository = repository;
            _projectRepository = projectRepository;
            _userManager = userManager;
        }

        public async Task<bool> Handle(AddTimecardsCommand request, CancellationToken cancellationToken)
        {
            if (!_projectRepository.Query().Any(x => x.Id == request.ProjectId))
            {
                throw new KeyNotFoundException("Can't find the Project.");
            }

            var existingUser = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (existingUser == null)
            {
                throw new KeyNotFoundException("Can't find the User.");
            }

            var timecards = Domain.Timecards.CreateTimecards(request.UserId, request.ProjectId,
                request.TimecardsDate.Date.GetFirstDayOfWeek());
            request.Items.ToList().ForEach(x =>
                timecards.AddTimecardsRecord(x.WorkDay.Date, x.Hour, x.Note));

            _repository.Add(timecards);
            var result = await _repository.UnitOfWork.CommitAsync(cancellationToken);

            return result > 0;
        }
    }
}