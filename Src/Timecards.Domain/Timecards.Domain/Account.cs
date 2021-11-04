using System;
using Microsoft.AspNetCore.Identity;

namespace Timecards.Domain
{
    public class Account : IdentityUser<Guid>
    {
    }
}