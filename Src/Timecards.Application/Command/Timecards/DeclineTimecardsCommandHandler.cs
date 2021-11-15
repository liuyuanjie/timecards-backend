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
    public class DeclineTimecardsCommandHandler : IRequestHandler<DeclineTimecardsCommand, bool>
    {
        private readonly IRepository<Domain.Timecards> _repository;

        public DeclineTimecardsCommandHandler(IRepository<Domain.Timecards> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeclineTimecardsCommand request, CancellationToken cancellationToken)
        {
            var existingTimecardses = _repository.Query().Where(x => request.TimecardsIds.Contains(x.Id)).ToList();
            existingTimecardses.ForEach(x => x.Decline());

            var result = await _repository.UnitOfWork.CommitAsync(cancellationToken);

            return result > 0;
        }
    }
}