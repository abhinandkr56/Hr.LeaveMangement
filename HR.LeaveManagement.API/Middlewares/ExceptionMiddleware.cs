using System.Net;
using HR.LeaveManagement.API.Models;
using HR.LeaveManagement.Application.Exceptions;

namespace HR.LeaveManagement.API.Middlewares;

public class ExceptionMiddleware
{
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
        catch (Exception e)
        {
            await HandleExceptions(httpContext, e);
        }
    }

    private async Task HandleExceptions(HttpContext httpContext, Exception exception)
    {
        CustomValidationProblemDetails problem = new CustomValidationProblemDetails();
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        switch (exception)
        {
            case BadRequestException badRequestException:
                statusCode= HttpStatusCode.BadRequest;
                problem = new CustomValidationProblemDetails()
                {
                    Title = exception.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Errors = badRequestException.ValidationErrors,
                    Type = nameof(BadRequestException)
                };
                break;
            case NotFoundException notFoundException:
                statusCode= HttpStatusCode.NotFound;
                problem = new CustomValidationProblemDetails()
                {
                    Title = exception.Message,
                    Status = (int)statusCode,
                    Detail = notFoundException.InnerException?.Message,
                    Type = nameof(BadRequestException)
                };
                break;
            default:
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}