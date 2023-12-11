using NSubstitute;
using RmxGeo.Application.CalculateGeodesicLength;
using RmxGeo.Application.Localization;
using RmxGeo.Domain;

namespace RmxGeo.Application.Tests
{
    public class GetGeodesicLengthQueryTests
    {
        private readonly GeoPoint A = new(53.297975, -6.372663);
        private readonly GeoPoint B = new(41.385101, -81.440440);

        private readonly IGeodesicCalculator _calculatorMock;
        private readonly GetGeodesicLengthQuery _instance;
        
        private readonly double _expectedLengthMeters = 5536339;

        public GetGeodesicLengthQueryTests()
        {
            _calculatorMock = Substitute.For<IGeodesicCalculator>();
            _calculatorMock.CalcGeodesicM(A, B).Returns(_expectedLengthMeters);
            _calculatorMock.CalcGeodesicM(B, A).Returns(_expectedLengthMeters);
            _instance = new GetGeodesicLengthQuery(_calculatorMock);
        }

        [Fact]
        public void EnUS_Culture_Result_In_Miles()
        {
            var input = new GeodesicLengthInputDto
            {
                Coordinates = [ A.Latitude, A.Longitude, B.Latitude, B.Longitude],
                CultureName = "en-US,en"
            };

            var actual = _instance.GetGeodesicLength(input);

            Assert.Multiple(() =>
            {
                Assert.Equal(_expectedLengthMeters * 0.000621371, actual.Length);
                Assert.Equal(DistanceUnits.Miles.ToString(), actual.Units);
            });
        }

        [Fact]
        public void Default_Culture_Result_In_Kilometers()
        {
            var input = new GeodesicLengthInputDto
            {
                Coordinates = [A.Latitude, A.Longitude, B.Latitude, B.Longitude],
                CultureName = "en-GB,en"
            };

            var actual = _instance.GetGeodesicLength(input);

            Assert.Multiple(() =>
            {
                Assert.Equal(_expectedLengthMeters * 0.001, actual.Length);
                Assert.Equal(DistanceUnits.Kilometers.ToString(), actual.Units);
            });
        }

        [Fact]
        public void MoreThanTwoPointsValidLength()
        {
            var input = new GeodesicLengthInputDto
            {
                Coordinates = [A.Latitude, A.Longitude, B.Latitude, B.Longitude,
                    B.Latitude, B.Longitude,  A.Latitude, A.Longitude]
            };

            var actual = _instance.GetGeodesicLength(input);

            Assert.Multiple(() =>
            {
                Assert.Equal(2 * _expectedLengthMeters * 0.001, actual.Length);
                Assert.Equal(DistanceUnits.Kilometers.ToString(), actual.Units);
            });
        }
    }
}