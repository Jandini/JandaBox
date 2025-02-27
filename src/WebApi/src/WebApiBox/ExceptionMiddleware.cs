using Microsoft.AspNetCore.Mvc.Controllers;
using System.Net;
using System.Net.Mime;

namespace WebApiBox;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var description = context.GetEndpoint()?.Metadata.GetMetadata<ControllerActionDescriptor>();
            logger.LogError(ex, "Unhandled exception in {UnhandledExceptionThrownBy}", description?.DisplayName);

            context.Response.ContentType = MediaTypeNames.Text.Plain;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}
