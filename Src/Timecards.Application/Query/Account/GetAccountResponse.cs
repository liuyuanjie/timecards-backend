using System;

namespace Timecards.Application.Query.Account
{
    public class GetAccountResponse
    {
        public Guid AccountId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}