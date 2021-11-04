using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Timecards.Application.Model;

namespace Timecards.Application.Query.Account
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, IList<AccountModel>>
    {
        private readonly UserManager<Domain.Account> _userManager;

        public GetAccountQueryHandler(UserManager<Domain.Account> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<AccountModel>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.Users
                .Select(x => new AccountModel
                {
                    AccountId = x.Id,
                    FullName = x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                })
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}