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
    public class SubmitTimecardsCommandHandler : IRequestHandler<SubmitTimecardsCommand, bool>
    {
        private readonly IRepository<Domain.Timecards> _repository;

        public SubmitTimecardsCommandHandler(IRepository<Domain.Timecards> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(SubmitTimecardsCommand request, CancellationToken cancellationToken)
        {
            var existingTimecardses = _repository.Query().Where(x => request.TimecardsIds.Contains(x.Id)).ToList();
            existingTimecardses.ForEach(x => x.Submit());

            var result = await _repository.UnitOfWork.CommitAsync(cancellationToken);

            return result > 0;
        }
    }
}