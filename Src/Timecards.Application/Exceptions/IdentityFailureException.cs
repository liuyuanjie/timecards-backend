using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Timecards.Application.Exceptions
{
    public class IdentityFailureException : ApiCustomException
    {
        public IdentityFailureException(string code, string message) : base(code, message)
        {
        }
    }

    public static class IdentityFailureExceptionFactory
    {
        public static IdentityFailureException Create(List<IdentityError> errors)
        {
            return new IdentityFailureException("IdentityFailure", "Invalid input.")
            {
                SubErrorMessages = errors.Select(x =>
                    new KeyValuePair<string, string>(x.Code, x.Description)).ToList()
            };
        }
    }
}