using RmxGeo.Application.Localization;

namespace RmxGeo.Application.CalculateGeodesicLength
{
    public class GeodesicLengthResultDto
    {
        public double Length { get; set; }
        public string Units { get; set; } = DistanceUnits.Kilometers.ToString();
    }
}
