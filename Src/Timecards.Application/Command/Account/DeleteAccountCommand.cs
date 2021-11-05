using System;
using MediatR;

namespace Timecards.Application.Command.Account
{
    public class DeleteAccountCommand : IRequest<bool>
    {
        public Guid AccountId { get; set; }
    }
}