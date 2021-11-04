using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Timecards.Domain;

namespace Timecards.Application.Command
{
    public class LoginCommand : IRequest<IList<Claim>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, IList<Claim>>
    {
        private readonly UserManager<Account> _userManager;

        public LoginCommandHandler(UserManager<Account> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<Claim>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return new List<Claim>();
            
            var valid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!valid) return new List<Claim>();
            
            var roleResult = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.MobilePhone, user.PhoneNumber),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Sid, user.Id.ToString()),
            };
            claims.AddRange(roleResult.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}