using System;
using MediatR;

namespace Timecards.Application.Query.Account
{
    public class GetAccountQuery: IRequest<GetAccountResponse>
    {
        public Guid AccountId { get; set; }
    }
}