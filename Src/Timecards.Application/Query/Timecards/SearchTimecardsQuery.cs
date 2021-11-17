using System;
using System.Collections.Generic;
using MediatR;
using Timecards.Application.Model;

namespace Timecards.Application.Query.Timecards
{
    public class SearchTimecardsQuery : IRequest<IList<SearchTimecardsResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProjectName { get; set; }
    }
}