using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;


namespace Core.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.ContentType = "application/json";
            var errorDetails = new ErrorDetails();
            string message = e.Message;
            var exceptionType = e.GetType();
            if (exceptionType == typeof(ValidationException))
            {
                errorDetails.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(AuthenticationException))
            {
                errorDetails.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(AdminSecurityException))
            {
                errorDetails.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            else if (exceptionType == typeof(TransactionScopeException))
            {
                errorDetails.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else
            {
                errorDetails.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorDetails.Message = "Internal Server Error";
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorDetails.StatusCode;

            return context.Response.WriteAsync(new ErrorDetails
            {
                Message = message,
                StatusCode = errorDetails.StatusCode
            }.ToString());
        }
    }
}