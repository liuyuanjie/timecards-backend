using System;
using Microsoft.AspNetCore.Identity;

namespace Timecards.Domain
{
    public class Account : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}