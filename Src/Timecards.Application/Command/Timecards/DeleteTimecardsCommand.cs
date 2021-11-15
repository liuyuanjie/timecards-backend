using System;
using MediatR;

namespace Timecards.Application.Command.Timecards
{
    public class DeleteTimecardsCommand : IRequest<bool>
    {
        public Guid TimecardsId { get; set; }
    }
}