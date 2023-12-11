namespace RmxGeo.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        new Application.ContainerModule().ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();
        app.UseHttpsRedirection();
                
        app.UseExceptionHandler(b => InvalidInputExceptionHandler.HandleException(b));

        app.UseStatusCodePages(async statusCodeContext
            => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
                 .ExecuteAsync(statusCodeContext.HttpContext));

        app.MapGetGeodesicLengthEndpoint();
        app.Run();
    }
}
