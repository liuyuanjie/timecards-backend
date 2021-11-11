using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TimecardsController> _logger;

        public TimecardsController(IMediator mediator, ILogger<TimecardsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IList<GetTimecardsResponse>> GetTimecards([FromQuery] Guid? userId,
            [FromQuery] DateTime? timecardsDate)
        {
            var getTimecardsQuery = new GetTimecardsQuery {UserId = userId, TimecardsDate = timecardsDate};
            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(getTimecardsQuery));

            var result = await _mediator.Send(
                getTimecardsQuery);

            return result;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SaveTimecards(AddTimecardsCommand command)
        {
            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(command));
            var result = await _mediator.Send(command);

            return result ? Ok() : BadRequest("Failed to save timecards.");
        }
    }
}