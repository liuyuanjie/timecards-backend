using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timecards.Application.Command.Timecards;
using Timecards.Application.Model;
using Timecards.Application.Query.Timecards;

namespace Timecards.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TimecardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimecardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IList<GetTimecardsResponse>> GetTimecards([FromQuery] Guid userId,
            [FromQuery] DateTime workDay)
        {
            var result = await _mediator.Send(
                new GetTimecardsQuery {UserId = userId, WorkDay = workDay});

            return result;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SaveTimecards(AddTimecardsCommand command)
        {
            var result = await _mediator.Send(command);

            return result ? Ok() : BadRequest("Failed to save timecards.");
        }
    }
}