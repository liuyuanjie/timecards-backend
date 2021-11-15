using System;
using System.Collections.Generic;
using MediatR;

namespace Timecards.Application.Command.Timecards
{
    public class ApproveTimecardsCommand : IRequest<bool>
    {
        public List<Guid> TimecardsIds { get; set; }
    }
}