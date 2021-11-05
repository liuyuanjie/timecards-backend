using System;
using MediatR;

namespace Timecards.Application.Command.User
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid AccountId { get; set; }
    }
}