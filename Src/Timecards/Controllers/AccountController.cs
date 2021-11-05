using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Timecards.Application.Command.Account;
using Timecards.Application.Model;
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
        [Route("")]
        public async Task<IActionResult> Create(AddAccountCommand addAccountCommand)
        {
            var result = await _mediator.Send(addAccountCommand);
            
            return result ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Route("{accountId}")]
        public async Task<IActionResult> Delete(Guid accountId)
        {
            var result = await _mediator.Send(new DeleteAccountCommand()
            {
                AccountId = accountId
            });
            
            return result ? Ok() : BadRequest();
        }

        [HttpGet]
        [Route("search")]
        public async Task<IList<AccountModel>> GetAll(string userName, string email, string phoneNumber)
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