using RmxGeo.Application.Localization;
using RmxGeo.Domain;

namespace RmxGeo.Application.CalculateGeodesicLength;

public sealed class GetGeodesicLengthQuery
{
    private readonly IGeodesicCalculator _geoCalculator;

    public GetGeodesicLengthQuery(IGeodesicCalculator geoCalculator)
    {
        _geoCalculator = geoCalculator;
    }

    private GeodesicLengthResultDto GetGeodesicLength(GeodesicLengthInputDto options)
    {
        var geoPoints = ParseCoordinates(options.Coordinates);
        if (geoPoints.Length < 2)
            throw new InvalidInputException($"At least 2 geo points are expected, but {geoPoints.Length} given.");

        var totalLengthInMeters = 0.0;
        for (var i = 1; i < geoPoints.Length; i++)
            totalLengthInMeters += _geoCalculator.CalcGeodesicM(geoPoints[i - 1], geoPoints[i]);

        DistanceUnits distanceUnits = DistanceUnitExtensions.GetByCulture(options.CultureName);

        return new GeodesicLengthResultDto()
        {
            Units = distanceUnits.ToString(),
            Length = distanceUnits.ConvertFromMeters(totalLengthInMeters)
        };
    }

    public Task<GeodesicLengthResultDto> GetGeodesicLengthAsync(GeodesicLengthInputDto options)
    {
        return Task.Run(() => GetGeodesicLength(options));
    }

    private static GeoPoint[] ParseCoordinates(double[] coordinates)
    {
        if (coordinates.Length % 2 != 0)
            throw new InvalidInputException("Coordinates array must contain even number of elements.");

        var points = coordinates.Length / 2;
        var geoPoints = new GeoPoint[points];
        for (var i = 0; i < points; i++)
        {
            geoPoints[i] = new GeoPoint(coordinates[i * 2], coordinates[i * 2 + 1]);
        }

        return geoPoints;
    }
}
