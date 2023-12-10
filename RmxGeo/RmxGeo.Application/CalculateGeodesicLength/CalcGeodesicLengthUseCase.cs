﻿using RmxGeo.Application.Localization;
using RmxGeo.Domain;

namespace RmxGeo.Application.CalculateGeodesicLength
{
    public sealed class GetGeodesicLengthUseCase
    {
        private readonly IGeodesicCalculator _geoCalculator;

        public GetGeodesicLengthUseCase(IGeodesicCalculator geoCalculator)
        {
            _geoCalculator = geoCalculator;
        }

        public GeodesicLengthResultDto GetGeodesicLength(GeodesicLengthInputDto options)
        {
            var points = options.GeoPoints.Length;
            if (points < 2)
                throw new ArgumentException($"At least 2 geo points are expected, but {points} given.");

            var totalLengthInMeters = 0.0;
            for (var i = 1; i < points; i++)
                totalLengthInMeters += _geoCalculator.CalcGeodesicM(options.GeoPoints[i-1], options.GeoPoints[i]);

            DistanceUnits distanceUnits = DistanceUnitExtensions.GetByCulture(options.CultutreName);

            return new GeodesicLengthResultDto()
            {
                Units = distanceUnits.ToString(),
                Length = distanceUnits.ConvertFromMeters(totalLengthInMeters)
            };
        }
    }
}