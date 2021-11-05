using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Timecards.Application.Exceptions;

namespace Timecards.Middlewares
{
    public class SystemExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SystemExceptionHandlerMiddleware> _logger;

        public SystemExceptionHandlerMiddleware(RequestDelegate next,
            ILogger<SystemExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"{context.Connection.RemoteIpAddress}:{context.Request.Path}");
                await HandleException(context, exception);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            ResponseErrorMessage responseErrorMessage;
            var response = context.Response;
            response.ContentType = "application/json";

            switch (exception)
            {
                case ApiCustomException ex:
                    // custom application error
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    responseErrorMessage = new ResponseErrorMessage(ex.ErrorCode, ex.Message);
                    break;

                case KeyNotFoundException ex:
                    // not found error
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    responseErrorMessage = new ResponseErrorMessage("NotFoundKey", ex.Message);
                    break;

                default:
                    // unhandled error
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    responseErrorMessage = new ResponseErrorMessage("ApplicationServerTrouble", "Timecards Application Server Is In Trouble.");
                    break;
            }

            await response.WriteAsync(JsonSerializer.Serialize(responseErrorMessage));
        }
    }

    public class ResponseErrorMessage
    {
        public ResponseErrorMessage(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public static class SystemExceptionHandlerExtension
    {
        public static void UseSystemExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<SystemExceptionHandlerMiddleware>();
        }
    }
}