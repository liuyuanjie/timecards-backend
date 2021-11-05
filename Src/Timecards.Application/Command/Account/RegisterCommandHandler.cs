using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Timecards.Application.Command.Account
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly UserManager<Domain.Account> _userManager;

        public RegisterCommandHandler(UserManager<Domain.Account> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var newAccount = new Domain.Account
            {
                UserName = request.UserName,
                Email = request.Email,
            };
            var result = await _userManager.CreateAsync(newAccount, request.Password);

            if (!result.Succeeded) return false;

            var roleResult = await _userManager.AddToRoleAsync(newAccount,request.RoleType.ToString());

            return roleResult.Succeeded;
        }
    }
}