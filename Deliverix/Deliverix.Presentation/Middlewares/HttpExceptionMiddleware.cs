using System.Net;
using System.Text.Json;
using Deliverix.Common.Exceptions;
using FluentValidation;

namespace DispensaryGreen.Presentation.Middlewares;

public class HttpExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HttpExceptionMiddleware> _logger;
    
    public HttpExceptionMiddleware(RequestDelegate next, ILogger<HttpExceptionMiddleware> logger)
    {
        _logger = logger;
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
            _logger.LogError($"Something went wrong: {ex}");
            
            int statusCode = (int)HttpStatusCode.InternalServerError;
            int errorCode = 0;
            string message = "Internal Server Error";
            
            HandleException(ex, ref statusCode, ref errorCode, ref message);

            await WriteExceptionAsync(httpContext, statusCode, errorCode, message);
        }
    }
    private async Task WriteExceptionAsync(HttpContext context, int statusCode, int errorCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(new ErrorDetailsDTO()
        {
            StatusCode = statusCode,
            ErrorCode = errorCode,
            Message = message
        }.ToString());
    }
    
    private void HandleException(Exception exception, ref int statusCode, ref int errorCode, ref string message)
    {
        bool isInternalServerError = true;

        if (exception is BusinessException)
        {
            BusinessException businessException = exception as BusinessException;

            statusCode = businessException.StatusCode;
            errorCode = businessException.ErrorCode;
            message = businessException.Message;
        }
        
        if (exception is ValidationException)
        {
            ValidationException validationException = exception as ValidationException;

            statusCode = 400;
            errorCode = 1;
            message = validationException.Message;
        }
    }
}

public class ErrorDetailsDTO
{
    public int StatusCode { get; set; }
    public int ErrorCode { get; set; }
    public string Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}