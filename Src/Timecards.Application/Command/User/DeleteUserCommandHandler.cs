using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Timecards.Application.Command.User
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly UserManager<Domain.Account> _userManager;

        public DeleteUserCommandHandler(UserManager<Domain.Account> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.AccountId.ToString());
            if (user == null) throw new KeyNotFoundException();

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }
    }
}