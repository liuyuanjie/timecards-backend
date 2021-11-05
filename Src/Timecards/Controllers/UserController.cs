using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Timecards.Application.Command.Account;
using Timecards.Application.Command.User;
using Timecards.Application.Model;
using Timecards.Application.Query.Account;

namespace Timecards.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserCommand
            {
                AccountId = id
            });
            
            return result ? Ok() : BadRequest("Failed to delete.");
        }

        [HttpGet]
        [Route("")]
        public async Task<IList<GetAllUsersResponse>> GetAll(string userName, string email, string phoneNumber)
        {
            var result = await _mediator.Send(new GetAccountQuery
            {
                FullName = userName,
                Email = email,
                PhoneNumber = phoneNumber
            });

            return result;
        }
    }
}