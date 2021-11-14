using System;
using Timecards.Domain.Enum;

namespace Timecards.Domain
{
    public class TimecardsItem : EntityBase
    {
        public Guid TimecardsId { get; set; }
        public DateTime WorkDay { get; set; }
        public decimal Hour { get; set; }
        public string Note { get; set; }

        public TimecardsItem(DateTime workDay, decimal hour, string note)
        {
            WorkDay = workDay;
            Hour = hour;
            Note = note;
        }
        
        public void UpdateTimecardsItem(DateTime workDay, decimal hour, string note)
        {
            WorkDay = workDay;
            Hour = hour;
            Note = note;
        }
    }
}