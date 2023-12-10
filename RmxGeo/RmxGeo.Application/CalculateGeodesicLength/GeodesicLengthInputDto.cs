using RmxGeo.Domain;

namespace RmxGeo.Application.CalculateGeodesicLength
{
    public class GeodesicLengthInputDto
    {
        /// <summary>
        /// Currently only 2 first points are supported.
        /// </summary>
        public GeoPoint[] GeoPoints { get; set; } = new GeoPoint[0];

        /// <summary>
        /// Culture name is used to determine distance units.
        /// </summary>
        public string CultureName { get; set; } = string.Empty;
    }
}
