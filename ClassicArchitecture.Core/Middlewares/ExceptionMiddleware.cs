using Castle.Core.Logging;
using ClassicArchitecture.Core.CrossCuttingConcerns.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ValidationProblemDetails = ClassicArchitecture.Core.CrossCuttingConcerns.Exceptions.ValidationProblemDetails;

namespace ClassicArchitecture.Core.Middlewares
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        private ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }


        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            if (exception.GetType() == typeof(ValidationException)) return CreateValidationException(context, exception);
            if (exception.GetType() == typeof(BusinessException)) return CreateBusinessException(context, exception);
            if (exception.GetType() == typeof(AuthorizationException))
                return CreateAuthorizationException(context, exception);
            return CreateInternalException(context, exception);
        }

        private Task HandleExceptionAsyncMvc(HttpContext context, Exception exception)
        {            
            if (exception.GetType() == typeof(BusinessException)) return CreateBusinessExceptionMvc(context, exception);
            if (exception.GetType() == typeof(AuthorizationException)) return CreateAuthorizationExceptionMvc(context, exception);
            return CreateInternalException(context, exception);
        }

        private Task CreateAuthorizationException(HttpContext context, Exception exception)
        {
            _logger.LogError(exception.Message);
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);

            return context.Response.WriteAsync(new AuthorizationProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Type = "https://example.com/probs/authorization",
                Title = "Authorization exception",
                Detail = exception.Message,
                Instance = ""
            }.ToString());
        }

        private Task CreateAuthorizationExceptionMvc(HttpContext context, Exception exception)
        {
            _logger.LogError(exception.Message);
            var redirectUrl = "/Home/AuthorizationError";
            context.Response.Redirect(redirectUrl);
            return Task.CompletedTask;
        }

        private Task CreateBusinessException(HttpContext context, Exception exception)
        {
            _logger.LogError(exception.Message);
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

            return context.Response.WriteAsync(new BusinessProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://example.com/probs/business",
                Title = "Business exception",
                Detail = exception.Message,
                Instance = ""
            }.ToString());
        }

        private Task CreateBusinessExceptionMvc(HttpContext context, Exception exception)
        {
            _logger.LogError(exception.Message);
            var redirectUrl = "/Home/ApplicationError";
            context.Response.Redirect(redirectUrl);
            return Task.CompletedTask;

        }

        private Task CreateValidationException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
            object errors = ((ValidationException)exception).Errors;

            return context.Response.WriteAsync(new ValidationProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://example.com/probs/validation",
                Title = "Validation error(s)",
                Detail = "",
                Instance = "",
                Errors = errors
            }.ToString());
        }

        private Task CreateValidationExceptionMvc(HttpContext context, Exception exception)
        {
            _logger.LogError(exception.Message);
            var redirectUrl = "/Home/ValidationError";
            context.Response.Redirect(redirectUrl);
            return Task.CompletedTask;
        }

        private Task CreateInternalException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);

            return context.Response.WriteAsync(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://example.com/probs/internal",
                Title = "Internal exception",
                Detail = exception.Message,
                Instance = ""
            }.ToString());
        }

    }

}
