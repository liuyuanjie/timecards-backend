using System;
using System.Collections.Generic;
using Timecards.Domain;
using Timecards.Domain.Enum;

namespace Timecards.Application.Model
{
    public class TimecardsItemModel
    {
        public DateTime WorkDay { get; set; }
        public decimal Hour { get; set; }
        public string Note { get; set; }
        public TimecardsType TimecardsType { get; set; }
    }

    public class TimecardsModel
    {
        public Guid ProjectId { get; set; }
        public DateTime TimecardsDate { get; set; }
        public IEnumerable<TimecardsItemModel> Items { get; set; }
    }
}