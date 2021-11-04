using System;
using Timecards.Domain.Enum;

namespace Timecards.Domain
{
    public class Timecards : EntityBase
    {
        public Guid AccountId { get; set; }
        public DateTime WorkDay { get; set; }
        public decimal Hour { get; set; }
        public string Note { get; set; }
        public TimecardsType TimecardsType { get; set; }

        public static Timecards CreateTimecardsRecord(Guid accountId, DateTime workDay, decimal hour, string note,
            TimecardsType timecardsType)
        {
            return new Timecards
            {
                AccountId = accountId,
                WorkDay = workDay,
                Hour = hour,
                Note = note,
                TimecardsType = timecardsType
            };
        }
    }
}