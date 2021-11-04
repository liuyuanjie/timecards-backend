using System;
using System.Collections.Generic;
using MediatR;
using Timecards.Application.Model;

namespace Timecards.Application.Command.Timecards
{
    public class CreateTimecardsCommand : IRequest<bool>
    {
        public Guid AccountId { get; set; }
        public List<TimecardsModel> Timecardses { get; set; }
    }
}