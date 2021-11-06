using System;
using System.Collections.Generic;
using Timecards.Domain;

namespace Timecards.Application.Tests
{
    public class MockTimecards
    {
        public static Domain.Timecards CreateTimecardsMockData(Guid accountId, DateTime timecardsDate, Guid projectId)
        {
            var mondayInCurrentWeek = DateTime.Today.AddDays(-(byte) DateTime.Today.DayOfWeek);
            
            return new Domain.Timecards
            {
                AccountId = accountId,
                ProjectId = projectId,
                TimecardsDate = timecardsDate,
                Items = new List<TimecardsItem>
                {
                    new TimecardsItem(mondayInCurrentWeek, 8, null),
                    new TimecardsItem(mondayInCurrentWeek.AddDays(1), 8, null),
                    new TimecardsItem(mondayInCurrentWeek.AddDays(2), 8, null),
                    new TimecardsItem(mondayInCurrentWeek.AddDays(3), 8, null),
                    new TimecardsItem(mondayInCurrentWeek.AddDays(4), 8, null),
                    new TimecardsItem(mondayInCurrentWeek.AddDays(5), 8, null),
                    new TimecardsItem(mondayInCurrentWeek.AddDays(6), 8, null),
                }
            };
        }
    }
}