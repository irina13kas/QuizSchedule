using Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next, 
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошло исключение: {Message}", ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (statusCode, message) = ex switch
            {
                NotFoundException notFound => (HttpStatusCode.NotFound, notFound.Message),
                ForbiddenException forbidden => (HttpStatusCode.Forbidden, forbidden.Message),
                NotBusinessSuitableException notBusinessSuitableException =>
                    (HttpStatusCode.Conflict, notBusinessSuitableException.Message),
                UnauthorizedException unauthorizedException => (HttpStatusCode.Unauthorized,
                    unauthorizedException.Message),
                ExistInDBException existInDBException => (HttpStatusCode.Conflict, 
                    existInDBException.Message),
                _ => (HttpStatusCode.InternalServerError, "Произошла внутренняя ошибка сервера")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                code = statusCode.ToString().ToUpper(),
                message
            };

            var jsonSerialize = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonSerialize);
        }
    }

   

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
