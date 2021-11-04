using System;
using MediatR;
using Timecards.Domain;

namespace Timecards.Application.Command
{
    public class DeleteAccountCommand : IRequest<bool>
    {
        public Guid AccountId { get; set; }
    }
}