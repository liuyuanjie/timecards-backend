using System;

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