using System;
using Serilog.Events;
using Microsoft.AspNetCore.Http;

namespace Presentation.Api.Middlewares.ExceptionHandling
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }
        public Func<Exception, LogEventLevel> DetermineLogLevel { get; set; }
    }
}