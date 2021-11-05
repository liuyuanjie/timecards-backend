using MediatR;
using Timecards.Domain.Enum;

namespace Timecards.Application.Command.Account
{
    public class AddAccountCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleType RoleType { get; set; }
    }
}