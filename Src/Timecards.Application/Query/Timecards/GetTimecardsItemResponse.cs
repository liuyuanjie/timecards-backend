using System;
using System.Collections.Generic;
using Timecards.Domain;
using Timecards.Domain.Enum;

namespace Timecards.Application.Model
{
    public class GetTimecardsItemResponse
    {
        public DateTime WorkDay { get; set; }
        public decimal Hour { get; set; }
        public string Note { get; set; }
    }

    public class GetTimecardsResponse
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid TimecardsId { get; set; }
        public DateTime TimecardsDate { get; set; }
        public IEnumerable<GetTimecardsItemResponse> Items { get; set; }
    }
}