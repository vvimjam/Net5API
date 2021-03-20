using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Net5_Core.Models;
using System.Net;

namespace Net5_API.Extensions
{

    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        //If the error throw is a client visible error (business use case) return actual error
                        if (contextFeature.Error is ClientError clientVisibleError)
                        {
                            await context.Response.WriteAsJsonAsync(new { clientVisibleError.Message, clientVisibleError.StatusCode });
                        }
                        else
                        {
                            //If an unexpected error, swallow actual error, log it & show user a simple message
                            logger.LogError($"Something went wrong: {contextFeature.Error}");
                            await context.Response.WriteAsJsonAsync(new ClientError("Internal Server Error."));
                        }
                    }
                });
            });
        }
    }
}
