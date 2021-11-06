using System;
using System.Collections.Generic;
using Timecards.Domain.Enum;

namespace Timecards.Domain
{
    public class Timecards : EntityBase
    {
        public Guid AccountId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime TimecardsDate { get; set; }
        
        public virtual List<TimecardsItem> Items { get; set; }

        public static Timecards CreateTimecards(Guid accountId, Guid projectId, DateTime timecardsDate)
        {
            return new Timecards()
            {
                AccountId = accountId,
                ProjectId = projectId,
                TimecardsDate = timecardsDate
            };
        }

        public void AddTimecardsRecord(DateTime workDay, decimal hour, string note)
        {
            Items ??= new List<TimecardsItem>();

            Items.Add(new TimecardsItem(workDay, hour, note));
        }
    }
}