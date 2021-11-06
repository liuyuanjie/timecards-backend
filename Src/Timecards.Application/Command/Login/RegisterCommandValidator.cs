using FluentValidation;

namespace Timecards.Application.Command.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.Email).EmailAddress().NotEmpty();
        }
    }
}