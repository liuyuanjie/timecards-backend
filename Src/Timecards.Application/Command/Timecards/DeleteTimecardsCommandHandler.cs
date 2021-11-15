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
    public class DeleteTimecardsCommandHandler : IRequestHandler<DeleteTimecardsCommand, bool>
    {
        private readonly IRepository<Domain.Timecards> _repository;

        public DeleteTimecardsCommandHandler(IRepository<Domain.Timecards> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteTimecardsCommand request, CancellationToken cancellationToken)
        {
            var existingTimecards = _repository.Query().First(x => x.Id == request.TimecardsId);
            if (existingTimecards == null)
            {
                throw new KeyNotFoundException("Can't find the timecards.");
            }

            _repository.Delete(existingTimecards);
            var result = await _repository.UnitOfWork.CommitAsync(cancellationToken);

            return result > 0;
        }
    }
}