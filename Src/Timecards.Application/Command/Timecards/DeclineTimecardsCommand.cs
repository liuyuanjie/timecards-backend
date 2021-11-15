using System;
using System.Collections.Generic;
using MediatR;

namespace Timecards.Application.Command.Timecards
{
    public class DeclineTimecardsCommand : IRequest<bool> {
        public List<Guid> TimecardsIds { get; set; }
    }
}