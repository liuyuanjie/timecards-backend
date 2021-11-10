using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timecards.Application.Command.Login;
using Timecards.Identity;

namespace Timecards.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Token(LoginCommand loginCommand)
        {
            var command = await _mediator.Send(loginCommand);
            if (command.Any())
            {
                return Ok(new
                {
                    Token = JwtTokenGenerator.Generator(command)
                });
            }

            return BadRequest("Failed to login.");
        }
    }
}