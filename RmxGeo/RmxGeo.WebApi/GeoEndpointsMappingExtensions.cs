using Microsoft.AspNetCore.Mvc;
using RmxGeo.Application.CalculateGeodesicLength;
using System.Globalization;

namespace RmxGeo.WebApi;

public static class GeoEndpointsMappingExtensions
{
    public static RouteHandlerBuilder MapGetGeodesicLengthEndpoint(this WebApplication app)
    {
        return app.MapGet("/geodesic/length", async ([FromServices] GetGeodesicLengthQuery getGeodesicLengthUseCase,
            string coordinates,
            string? culture,
            [FromHeader(Name = "Accept-Language")] string? acceptLanguage) =>
        {
            var coords = coordinates.Split([',', ';']).Select(x => double.Parse(x.Trim(), CultureInfo.InvariantCulture)).ToArray();
            GeodesicLengthResultDto res = await getGeodesicLengthUseCase.GetGeodesicLengthAsync(new GeodesicLengthInputDto()
            {
                Coordinates = coords,
                CultureName = culture ?? acceptLanguage ?? string.Empty
            });

            return Results.Ok(res);
        })
        .Produces<GeodesicLengthResultDto>();
    }
}
