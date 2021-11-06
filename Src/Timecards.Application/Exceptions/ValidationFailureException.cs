using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using FluentValidation.Results;

namespace Timecards.Application.Exceptions
{
    public class ValidationFailureException : ApiCustomException
    {
        public ValidationFailureException(string code, string message) : base(code, message)
        {
        }
    }

    public static class ValidationFailureExceptionFactory
    {
        public static ValidationFailureException Create(List<ValidationFailure> errors)
        {
            return new ValidationFailureException("ValidationFailure",
                JsonSerializer.Serialize(errors.Select(x =>
                    new
                    {
                        SubErrorCode = x.ErrorCode,
                        SubErrorMessage = x.ErrorMessage
                    })
                )
            );
        }
    }
}