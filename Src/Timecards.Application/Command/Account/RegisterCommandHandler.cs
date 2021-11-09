using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Timecards.Application.Exceptions;

namespace Timecards.Application.Command.Account
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly UserManager<Domain.Account> _userManager;

        public RegisterCommandHandler(UserManager<Domain.Account> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var account = new Domain.Account
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            var result = await _userManager.CreateAsync(account, request.Password);

            if (!result.Succeeded)
                throw IdentityFailureExceptionFactory.Create(result.Errors.ToList());

            var roleResult = await _userManager.AddToRoleAsync(account, request.RoleType.ToString());
            if (!roleResult.Succeeded)
                throw IdentityFailureExceptionFactory.Create(result.Errors.ToList());

            return new RegisterResponse()
            {
                UserId = account.Id,
                UserName = account.UserName,
                FirstName = account.LastName,
                LastName = account.LastName,
                Email = account.Email,
                RoleType = request.RoleType
            };
        }
    }
}