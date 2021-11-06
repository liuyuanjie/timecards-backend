using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timecards.Application.Model;
using Timecards.Application.Query.Project;
using Timecards.Application.Query.Timecards;

namespace Timecards.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class Project : ControllerBase
    {
        private readonly IMediator _mediator;

        public Project(IMediator mediator)
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