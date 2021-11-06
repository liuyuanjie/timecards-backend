using System.ComponentModel.DataAnnotations;
using MediatR;
using Timecards.Domain.Enum;

namespace Timecards.Application.Command.Account
{
    public class RegisterCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [EnumDataType(typeof(RoleType))]
        public RoleType RoleType { get; set; }
    }
}