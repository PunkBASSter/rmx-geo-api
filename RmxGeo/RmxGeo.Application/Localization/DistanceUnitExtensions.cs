namespace RmxGeo.Application.Localization
{
    public static class DistanceUnitExtensions
    {
        public static Dictionary<DistanceUnits, double> UnitMultipliers = new Dictionary<DistanceUnits, double>()
        {
            { DistanceUnits.Meters, 1.0 },
            { DistanceUnits.Kilometers, 0.001 },
            { DistanceUnits.Miles, 0.000621371 }
        };

        public static double ConvertFromMeters(this DistanceUnits targetValue, double valueInMeters)
        {
            return UnitMultipliers[targetValue] * valueInMeters;
        }

        public static DistanceUnits GetByCulture(string cultureName)
        {
            if (cultureName.Equals("en-us", StringComparison.CurrentCultureIgnoreCase))
                return DistanceUnits.Miles;

            return DistanceUnits.Kilometers;
        }
    }
}
