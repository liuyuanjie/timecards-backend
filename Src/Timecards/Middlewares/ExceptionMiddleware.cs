using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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
            var response = context.Response;
            response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(exception.Message);
            await response.WriteAsync(result);
        }
    }

    public static class SystemExceptionHandlerExtension
    {
        public static void UseSystemExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<SystemExceptionHandlerMiddleware>();
        }
    }
}