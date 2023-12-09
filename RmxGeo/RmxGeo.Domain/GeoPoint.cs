namespace RmxGeo.Domain
{
    public struct GeoPoint
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public GeoPoint(double lat, double lon)
        {
            if (lat < -90 || lat > 90)
                throw new ArgumentException($"Latitude must be in range [-90, 90] degrees.");
            if (lon < -180 || lon > 180)
                throw new ArgumentException($"Longitude must be in range [-180, 180] degrees.");

            Latitude = lat;
            Longitude = lon;
        }
    }
}
