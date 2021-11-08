using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timecards.Application.Query.Project;

namespace Timecards.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IList<GetProjectResponse>> GetProjects(string name)
        {
            var result = await _mediator.Send(new GetProjectQuery {Name = name});

            return result;
        }
    }
}