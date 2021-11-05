using System;
using System.Collections.Generic;
using MediatR;
using Timecards.Application.Model;

namespace Timecards.Application.Query.Timecards
{
    public class GetTimecardsQuery : IRequest<IList<GetTimecardsResponse>>
    {
        public Guid AccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}