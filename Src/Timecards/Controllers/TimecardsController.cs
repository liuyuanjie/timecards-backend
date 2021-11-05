using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timecards.Application.Command.Timecards;
using Timecards.Application.Model;
using Timecards.Application.Query.Timecards;

namespace Timecards.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TimecardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimecardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{accountId}")]
        public async Task<IList<TimecardsModel>> GetTimecards(Guid accountId, [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var result = await _mediator.Send(
                new GetTimecardsQuery {AccountId = accountId, StartDate = startDate, EndDate = endDate});

            return result;
        }

        [HttpPost]
        [Route("{accountId}")]
        public async Task<IActionResult> SaveTimecards(Guid accountId, List<TimecardsModel> timecards)
        {
            var result = await _mediator.Send(
                new CreateTimecardsCommand() {AccountId = accountId, Timecardses = timecards});

            return result ? Ok() : BadRequest();
        }
    }
}