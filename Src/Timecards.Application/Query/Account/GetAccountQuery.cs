using System.Collections.Generic;
using MediatR;
using Timecards.Application.Model;

namespace Timecards.Application.Query.Account
{
    public class GetAccountQuery : IRequest<IList<GetAllUsersResponse>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}