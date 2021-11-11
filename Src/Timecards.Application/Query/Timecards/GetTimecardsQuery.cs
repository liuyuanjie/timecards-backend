using System;
using System.Collections.Generic;
using MediatR;
using Timecards.Application.Model;

namespace Timecards.Application.Query.Timecards
{
    public class GetTimecardsQuery : IRequest<IList<GetTimecardsResponse>>
    {
        public Guid? UserId { get; set; }
        public DateTime? TimecardsDate { get; set; }
    }
}