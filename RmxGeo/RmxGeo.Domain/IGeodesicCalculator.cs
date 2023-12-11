namespace RmxGeo.Domain;

public interface IGeodesicCalculator
{
    /// <summary>
    /// Calculates length of the geodesic curve by two points in meters.
    /// </summary>
    /// <param name="pointA">Point A with geographic coordinates.</param>
    /// <param name="pointB">Point B with geographic coordinates.</param>
    /// <returns>Geodesic length in meters.</returns>
    double CalcGeodesicM(GeoPoint pointA, GeoPoint pointB);
}
