namespace RmxGeo.Domain.Tests
{
    public class SphereTrigonometryGeodesicCalculatorTests
    {
        public const double EARTH_EQUATOR_RADIUS_M = 6371000;
        public const double CALCULATION_TOLERANCE = 1;

        public IGeodesicCalculator GeodesicCalculator { get; set; }

        public SphereTrigonometryGeodesicCalculatorTests()
        {
            GeodesicCalculator = new SphereTrigonometryGeodesicCalculator(EARTH_EQUATOR_RADIUS_M);
        }

        //Reference values were taken from
        //https://geographiclib.sourceforge.io/cgi-bin/GeodSolve?type=I&input=0%2C+0%2C+0%2C+180&format=g&azi2=f&unroll=r&prec=0&radius=6371000&flattening=0&option=Submit
        [Theory] //lat1, lon1, lat2, lon2, expected_value, tolerance
        [InlineData(53.297975, -6.372663, 41.385101, -81.440440, 5536339)]
        [InlineData(53, -90, 41, 90, 9562764)]
        [InlineData(0, 0, 0, 180, 20015087)]
        [InlineData(0, -90, 0, 90, 20015087)]
        public void ValidInputHasValidResult(params object[] testData)
        {
            var data = testData.Select(Convert.ToDouble).ToArray();
            var a = new GeoPoint(data[0], data[1]);
            var b = new GeoPoint(data[2], data[3]);

            var actual = GeodesicCalculator.CalcGeodesicM(a, b);
            var expected = data[4];

            Assert.Equal(expected, actual, CALCULATION_TOLERANCE);
        }
    }
}