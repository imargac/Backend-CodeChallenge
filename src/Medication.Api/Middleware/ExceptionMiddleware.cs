namespace Medication.Api.Middleware
{
    using System.Net;
    using FluentValidation;
    using Microsoft.AspNetCore.Http;

    internal sealed class ExceptionMiddleware
    {
        private const string GenericErrorMessage = "One or more errors ocurred while sending the request";

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is ValidationException validationException)
            {
                await HandleValidationExceptionAsync(context, validationException);
                return;
            }

            if (exception is BadHttpRequestException badHttpRequestException)
            {
                await HandleBadHttpRequestExceptionAsync(context, badHttpRequestException);
                return;
            }

            await UnhandledExceptionAsync(context);
        }

        private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException validationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = Results.ValidationProblem(
                errors: validationException.Errors.ToDictionary(e => e.PropertyName, e => new string[] { e.ErrorMessage }),
                statusCode: context.Response.StatusCode,
                detail: GenericErrorMessage);

            await result.ExecuteAsync(context);
        }

        private static async Task HandleBadHttpRequestExceptionAsync(HttpContext context, BadHttpRequestException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = Results.Problem(
               statusCode: context.Response.StatusCode,
               detail: exception.Message);

            await result.ExecuteAsync(context);
        }

        private static async Task UnhandledExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = Results.Problem(
               statusCode: context.Response.StatusCode,
               detail: GenericErrorMessage);

            await result.ExecuteAsync(context);
        }
    }
}


