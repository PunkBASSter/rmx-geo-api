namespace RmxGeo.Domain
{
    public sealed class SphereTrigonometryGeodesicCalculator : IGeodesicCalculator
    {
        public double RadiusM { get; init; }
        private double DegToRad(double angle) => Math.PI * angle / 180.0;

        public SphereTrigonometryGeodesicCalculator(double radiusM)
        {
            RadiusM = radiusM;
        }

        public double CalcGeodesicRad(GeoPoint pointA, GeoPoint pointB)
        {
            double a = DegToRad(90 - pointB.Latitude);
            double b = DegToRad(90 - pointA.Latitude);
            double phi = DegToRad(pointA.Longitude - pointB.Longitude);

            double cosP = Math.Cos(a) * Math.Cos(b) + Math.Sin(a) * Math.Sin(b) * Math.Cos(phi);
            double angle = Math.Acos(cosP);

            return angle;
        }

        public double CalcGeodesicM(GeoPoint pointA, GeoPoint pointB)
        {
            return CalcGeodesicRad(pointA, pointB) * RadiusM;
        }
    }
}
