using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

            var utcTimecardsDate = request.TimecardsDate.Date.GetFirstDayOfWeek().ToUniversalTime();
            var existingTimecards = GetTimecards(request, utcTimecardsDate);
            if (existingTimecards != null)
            {
                UpdateTimecards(request, existingTimecards);
            }
            else
            {
                AddTimecards(request, utcTimecardsDate);
            }

            var result = await _repository.UnitOfWork.CommitAsync(cancellationToken);

            return result > 0;
        }

        private Domain.Timecards GetTimecards(AddTimecardsCommand request, DateTime utcTimecardsDate)
        {
            var existingTimecards = _repository.Query()
                .Include(x => x.Items)
                .FirstOrDefault(x =>
                    x.AccountId == request.UserId &&
                    x.ProjectId == request.ProjectId &&
                    x.TimecardsDate == utcTimecardsDate);
            
            return existingTimecards;
        }

        private void AddTimecards(AddTimecardsCommand request, DateTime utcTimecardsDate)
        {
            var timecards = Domain.Timecards.CreateTimecards(request.UserId, request.ProjectId,
                utcTimecardsDate);
            request.Items.ToList().ForEach(x =>
                timecards.AddTimecardsRecord(x.WorkDay.Date.ToUniversalTime(), x.Hour, x.Note));

            _repository.Add(timecards);
        }

        private static void UpdateTimecards(AddTimecardsCommand request, Domain.Timecards existingTimecards)
        {
            request.Items.ToList().ForEach(x =>
            {
                existingTimecards.UpdateTimecardsItem(x.WorkDay.Date.ToUniversalTime(), x.Hour, x.Note);
            });
        }
    }
}