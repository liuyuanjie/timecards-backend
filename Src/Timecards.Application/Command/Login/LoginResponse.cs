using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Timecards.Application.Command.Login
{
    public class LoginResponse
    {
        public Guid AccountId { get; set; }
        public List<Claim> Claims { get; set; }
    }
}