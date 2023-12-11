using RmxGeo.Domain;

namespace RmxGeo.Application.CalculateGeodesicLength;

public class GeodesicLengthInputDto
{
    /// <summary>
    /// Array of coordinates in format [lat1, lon1, lat2, lon2, ...]
    /// </summary>
    public double[] Coordinates { get; set; } = new double[0];

    /// <summary>
    /// Culture name is used to determine distance units.
    /// </summary>
    public string CultureName { get; set; } = string.Empty;
}
