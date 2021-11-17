using System;
using Timecards.Domain.Enum;

namespace Timecards.Application.Model
{
    public class SearchTimecardsResponse
    {
        public Guid TimecardsId { get; set; }
        public string ProjectName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public TimecardsStatusType StatusType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalHour { get; set; }
    }
}