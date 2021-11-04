using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Timecards.Application.Command;
using Timecards.Common;

namespace Timecards.Controller
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
                return Ok(JwtTokenGenerator.Generator(command));
            }
            return BadRequest();
        }
    }
}