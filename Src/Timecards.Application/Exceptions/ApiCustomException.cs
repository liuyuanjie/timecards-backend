using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Timecards.Application.Exceptions
{
    public class ApiCustomException : Exception
    {
        public string ErrorCode { get; set; }

        public ApiCustomException(string code, string message) : base(message)
        {
            ErrorCode = code;
        }
    }
}