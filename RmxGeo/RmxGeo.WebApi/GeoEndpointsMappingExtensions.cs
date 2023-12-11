using Microsoft.AspNetCore.Mvc;
using RmxGeo.Application.CalculateGeodesicLength;

namespace RmxGeo.WebApi;

public static class GeoEndpointsMappingExtensions
{
    public static RouteHandlerBuilder MapGetGeodesicLengthEndpoint(this WebApplication app)
    {
        return app.MapGet("/geodesic/length", async ([FromServices] GetGeodesicLengthUseCase getGeodesicLengthUseCase,
            string coordinates,
            string? culture,
            [FromHeader(Name = "Accept-Language")] string? acceptLanguage) =>
        {
            GeodesicLengthResultDto res = await getGeodesicLengthUseCase.GetGeodesicLengthAsync(new GeodesicLengthInputDto()
            {
                Coordinates = coordinates.Split([',', ';']).Select(x => Convert.ToDouble(x)).ToArray(),
                CultureName = culture ?? acceptLanguage ?? string.Empty
            });

            return Results.Ok(res);
        }).Produces<GeodesicLengthResultDto>();
    }
}
