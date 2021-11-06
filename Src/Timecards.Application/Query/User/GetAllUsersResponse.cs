using System;

namespace Timecards.Application.Query.User
{
    public class GetAllUsersResponse
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}