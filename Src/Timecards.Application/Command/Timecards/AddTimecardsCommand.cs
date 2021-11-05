using System;
using System.Collections.Generic;
using MediatR;
using Timecards.Application.Model;

namespace Timecards.Application.Command.Timecards
{
    public class AddTimecardsCommand : IRequest<bool>
    {
        public Guid AccountId { get; set; }
        public AddTimecards GetTimecards { get; set; }
    }
    
    public class AddTimecardsItem
    {
        public DateTime WorkDay { get; set; }
        public decimal Hour { get; set; }
        public string Note { get; set; }
    }

    public class AddTimecards
    {
        public Guid ProjectId { get; set; }
        public DateTime TimecardsDate { get; set; }
        public IEnumerable<AddTimecardsItem> Items { get; set; }
    }
}