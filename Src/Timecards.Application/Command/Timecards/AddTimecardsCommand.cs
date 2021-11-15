using System;
using System.Collections.Generic;
using MediatR;

namespace Timecards.Application.Command.Timecards
{
    public class AddTimecards
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime TimecardsDate { get; set; }
        public IEnumerable<AddTimecardsItem> Items { get; set; }
    }

    public class AddTimecardsCommand : IRequest<bool>
    {
        public List<AddTimecards> Timecardses { get; set; }
    }
    
    public class AddTimecardsItem
    {
        public DateTime WorkDay { get; set; }
        public decimal Hour { get; set; }
        public string Note { get; set; }
    }
}