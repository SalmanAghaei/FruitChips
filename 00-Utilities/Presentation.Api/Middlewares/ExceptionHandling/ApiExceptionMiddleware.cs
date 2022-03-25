using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog.Events;
using System.Net;

namespace Presentation.Api.Middlewares.ExceptionHandling
{
    //Exception Handling Middleware
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiExceptionOptions _options;

        public ApiExceptionMiddleware(ApiExceptionOptions options, RequestDelegate next)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            //using IDisposable hostScope = _logger.BeginScope(scopeInfo.HostScopeInfo);
            //using IDisposable requestScope = _logger.BeginScope(scopeInfo.RequestScopeInfo);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);//, hostScope, requestScope);
            }
            finally
            {
            }
        }
      


        private Task HandleExceptionAsync(HttpContext context, Exception exception)//, IDisposable hostScopInfo, IDisposable requestScopeInfo)
        {
            var error = new ApiError
            {
                Id = Guid.NewGuid().ToString(),
                Status = (short)HttpStatusCode.InternalServerError,
                Title = "Some kind of error occurred in the API.  Please use the id and contact our " +
                        "support team if the problem persists."
            };

            _options.AddResponseDetails?.Invoke(context, exception, error);

            //Set Inner Exception
            var innerExMessage = GetInnermostExceptionMessage(exception);

            var level = _options.DetermineLogLevel?.Invoke(exception) ?? LogEventLevel.Error;
         

            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(result);
        }

        private string GetInnermostExceptionMessage(Exception exception)
        {
            if (exception.InnerException != null)
                return GetInnermostExceptionMessage(exception.InnerException);

            return exception.Message;
        }
    }
}