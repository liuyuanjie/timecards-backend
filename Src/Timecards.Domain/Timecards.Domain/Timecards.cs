using System;
using System.Collections.Generic;
using System.Linq;
using Timecards.Domain.Enum;

namespace Timecards.Domain
{
    public class Timecards : EntityBase
    {
        public Guid AccountId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime TimecardsDate { get; set; }

        public virtual List<TimecardsItem> Items { get; set; }
        
        public TimecardsStatusType StatusType { get; set; }
        
        public static Timecards CreateTimecards(Guid accountId, Guid projectId, DateTime timecardsDate)
        {
            return new Timecards()
            {
                AccountId = accountId,
                ProjectId = projectId,
                TimecardsDate = timecardsDate,
                StatusType = TimecardsStatusType.Saved
            };
        }

        public void UpdateTimecardsItem(DateTime workDay, decimal hour, string note)
        {
            var existingItem = Items.FirstOrDefault(x => x.WorkDay == workDay);
            existingItem?.UpdateTimecardsItem(workDay, hour, note);
        }

        public void AddTimecardsRecord(DateTime workDay, decimal hour, string note)
        {
            Items ??= new List<TimecardsItem>();

            Items.Add(new TimecardsItem(workDay, hour, note));
        }

        public void Approve()
        {
            if (StatusType == TimecardsStatusType.Submitted)
            {
                StatusType = TimecardsStatusType.Approved;
            }
        }
        
        public void Submit()
        {
            StatusType = TimecardsStatusType.Submitted;
        }
        
        public void Decline()
        {
            if (StatusType == TimecardsStatusType.Submitted)
            {
                StatusType = TimecardsStatusType.Denied;
            }
        }
    }
}