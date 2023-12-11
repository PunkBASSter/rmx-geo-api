using Microsoft.AspNetCore.Diagnostics;
using RmxGeo.Domain;

namespace RmxGeo.WebApi;

public class InvalidInputExceptionHandler
{
    public static IApplicationBuilder HandleException(IApplicationBuilder builder)
    {
        builder.Run(async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;

            if (exception is InvalidInputException)
            {
                var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = exception.Message
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        });
        return builder;
    }
}
