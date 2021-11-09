using System;
using Timecards.Domain.Enum;

namespace Timecards.Application.Command.Account
{
    public class RegisterResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public RoleType RoleType { get; set; }
        
    }
}