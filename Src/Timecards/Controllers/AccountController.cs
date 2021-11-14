using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timecards.Application.Command.Account;
using Timecards.Application.Query.Account;

namespace Timecards.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand)
        {
            var result = await _mediator.Send(registerCommand);

            return Created(result.UserId.ToString(), result);
        }

        [HttpGet]
        [Route("{accountId}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid accountId)
        {
            var result = await _mediator.Send(new GetAccountQuery()
            {
                AccountId = accountId
            });

            return Ok(result);
        }
    }
}