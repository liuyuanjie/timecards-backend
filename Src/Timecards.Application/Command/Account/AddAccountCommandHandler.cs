using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Timecards.Application.Command.Account
{
    public class AddAccountCommandHandler : IRequestHandler<AddAccountCommand, bool>
    {
        private readonly UserManager<Domain.Account> _userManager;

        public AddAccountCommandHandler(UserManager<Domain.Account> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            var newAccount = new Domain.Account
            {
                UserName = request.UserName,
                Email = request.Email,
            };
            var result = await _userManager.CreateAsync(newAccount, request.Password);
            
            if (!result.Succeeded) return false;
            
            var roles = new List<string> {request.RoleType.ToString()};
            var roleResult = await _userManager.AddToRolesAsync(newAccount, roles);
            
            return roleResult.Succeeded;
        }
    }
}