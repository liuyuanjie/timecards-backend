using System;
using Timecards.Domain.Enum;

namespace Timecards.Application.Model
{
    public class TimecardsModel
    {
        public DateTime WorkDay { get; set; }
        public decimal Hour { get; set; }
        public string Note { get; set; }
        public TimecardsType TimecardsType { get; set; }
    }
}