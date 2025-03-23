using FluentValidation;
using System.Net;
using System.Text.Json;
using TestOnlineStore.Persistence.Common.Exceptions;

namespace TestOnlineStore.WebApi.Middleware;

public class CustomExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        if (ex is ValidationException validation)
        {
            code = HttpStatusCode.BadRequest;
            result = JsonSerializer.Serialize(validation.Errors);
        }

        if (ex is NotFoundException notFound)
        {
            code = HttpStatusCode.NotFound;
            result = JsonSerializer.Serialize(notFound.Message);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(ex.Message);
        }

        return context.Response.WriteAsync(result);
    }
}