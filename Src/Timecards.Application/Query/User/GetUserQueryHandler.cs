using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Timecards.Application.Query.User
{
    public class GetAccountQueryHandler : IRequestHandler<GetUserQuery, IList<GetAllUsersResponse>>
    {
        private readonly UserManager<Domain.Account> _userManager;

        public GetAccountQueryHandler(UserManager<Domain.Account> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<GetAllUsersResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.Users
                .Select(x => new GetAllUsersResponse
                {
                    UserId = x.Id,
                    FullName = x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                })
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}