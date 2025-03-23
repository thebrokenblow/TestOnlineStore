namespace TestOnlineStore.WebApi.Middleware;

public static class UseCustomExceptionHandlerExtension
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<CustomExceptionHandlerMiddleware>();

        return builder;
    }
}
