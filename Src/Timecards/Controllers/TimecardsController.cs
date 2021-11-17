using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timecards.Application.Command.Timecards;
using Timecards.Application.Model;
using Timecards.Application.Query.Timecards;
using Timecards.Identity;
using Timecards.Middlewares;

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

            var result = await _mediator.Send(getTimecardsQuery);

            return result;
        }

        [HttpGet]
        [Route("search")]
        public async Task<IList<SearchTimecardsResponse>> SearchTimecards()
        {
            var searchTimecardsQuery = new SearchTimecardsQuery();

            var result = await _mediator.Send(searchTimecardsQuery);

            return result;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SaveTimecards(List<AddTimecards> timecardses)
        {
            _logger.LogInformation(System.Text.Json.JsonSerializer.Serialize(timecardses));
            var result = await _mediator.Send(new AddTimecardsCommand
            {
                Timecardses = timecardses
            });

            return result
                ? Ok()
                : BadRequest(
                    new ResponseErrorMessage(HttpStatusCode.BadRequest, "Failed to save timecards."));
        }

        [HttpDelete]
        [Route("{timecardsId}")]
        public async Task<IActionResult> DeleteTimecards(Guid timecardsId)
        {
            var result = await _mediator.Send(new DeleteTimecardsCommand
            {
                TimecardsId = timecardsId
            });

            return result
                ? Ok()
                : BadRequest(
                    new ResponseErrorMessage(HttpStatusCode.BadRequest, "Failed to delete timecards."));
        }

        [HttpPost]
        [Route("submit")]
        public async Task<IActionResult> SubmitTimecards(List<Guid> timecardsIds)
        {
            var result = await _mediator.Send(new SubmitTimecardsCommand
            {
                TimecardsIds = timecardsIds
            });

            return result
                ? Ok()
                : BadRequest(
                    new ResponseErrorMessage(HttpStatusCode.BadRequest, "Failed to submit timecards."));
        }

        [HttpPost]
        [Route("approve")]
        [Authorize(ApplicationAuthorization.HasAdminPermission)]
        public async Task<IActionResult> ApproveTimecards(List<Guid> timecardsIds)
        {
            var result = await _mediator.Send(new ApproveTimecardsCommand
            {
                TimecardsIds = timecardsIds
            });

            return result
                ? Ok()
                : BadRequest(
                    new ResponseErrorMessage(HttpStatusCode.BadRequest, "Failed to approve timecards."));
        }

        [HttpPost]
        [Route("decline")]
        [Authorize(ApplicationAuthorization.HasAdminPermission)]
        public async Task<IActionResult> DeclineTimecards(List<Guid> timecardsIds)
        {
            var result = await _mediator.Send(new DeclineTimecardsCommand
            {
                TimecardsIds = timecardsIds
            });

            return result
                ? Ok()
                : BadRequest(
                    new ResponseErrorMessage(HttpStatusCode.BadRequest, "Failed to decline timecards."));
        }
    }
}