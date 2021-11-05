using System.Collections.Generic;
using System.Security.Claims;
using MediatR;

namespace Timecards.Application.Command.Login
{
    public class LoginCommand : IRequest<IList<Claim>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}