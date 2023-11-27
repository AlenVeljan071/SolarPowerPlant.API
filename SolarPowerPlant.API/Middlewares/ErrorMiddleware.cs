using FluentValidation;
using SolarPowerPlant.API.Errors;
using System.Net;
using System.Text.Json;

namespace SolarPowerPlant.Api.Middlewares
{
    public class ErrorMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                HttpStatusCode status;
                string message;
                string code = string.Empty;
                var stackTrace = string.Empty;

                if (exception is UnauthorizedException unauthorizedException)
                {
                    message = unauthorizedException.Message;
                    code = unauthorizedException.Code;
                    status = HttpStatusCode.Unauthorized;
                }
                else if (exception is BadRequestException badRequestException) 
                {
                    message = badRequestException.Message;
                    code = badRequestException.Code;
                    status = HttpStatusCode.BadRequest;
                }
                else if (exception is NotFoundException notFoundException)
                {
                    message = notFoundException.Message;
                    code = notFoundException.Code;
                    status = HttpStatusCode.NotFound;
                }
                else if (exception is ValidationException validationException)
                {
                    var error = validationException.Errors.FirstOrDefault();
                    if (error != null)
                    {
                        message = error.ErrorMessage;
                    }
                    else
                    {
                        message = validationException.Message;
                    }
                    code = "422";
                    status = HttpStatusCode.UnprocessableEntity;
                }
                else
                {
                    status = HttpStatusCode.InternalServerError;
                    message = exception.InnerException != null ? string.Concat(exception.Message, exception.InnerException.Message) : exception.Message;
                    if (_env.IsDevelopment())
                    {
                        stackTrace = exception.StackTrace;
                    }
                }

                var result = JsonSerializer.Serialize(new { error = message, stackTrace, errorCode = code });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)status;
                await context.Response.WriteAsync(result);
            }
        }
    }
}
