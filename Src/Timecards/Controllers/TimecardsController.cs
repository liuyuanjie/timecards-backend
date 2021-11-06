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
        [Route("{userId}")]
        public async Task<IList<GetTimecardsResponse>> GetTimecards(Guid userId, [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var result = await _mediator.Send(
                new GetTimecardsQuery {AccountId = userId, StartDate = startDate, EndDate = endDate});

            return result;
        }

        [HttpPost]
        [Route("{userId}")]
        public async Task<IActionResult> SaveTimecards(Guid userId, AddTimecardsCommand command)
        {
            command.UserId = userId;
            var result = await _mediator.Send(command);

            return result ? Ok() : BadRequest("Failed to save timecards.");
        }
    }
}