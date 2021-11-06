using System.Collections.Generic;
using MediatR;

namespace Timecards.Application.Query.Project
{
    public class GetProjectQuery: IRequest<IList<GetProjectResponse>>
    {
        public string Name { get; set; }
    }
}