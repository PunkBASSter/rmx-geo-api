using NSubstitute;
using RmxGeo.Application.CalculateGeodesicLength;
using RmxGeo.Application.Localization;
using RmxGeo.Domain;

namespace RmxGeo.Application.Tests
{
    public class CalcGeodesicLengthTests
    {
        private readonly IGeodesicCalculator _calculatorMock;
        private readonly GetGeodesicLengthUseCase _instance;
        private readonly GeoPoint _pointA = new(53.297975, -6.372663);
        private readonly GeoPoint _pointB = new(41.385101, -81.440440);
        private readonly double _expectedLengthMeters = 5536339;

        public CalcGeodesicLengthTests()
        {
            _calculatorMock = Substitute.For<IGeodesicCalculator>();
            _calculatorMock.CalcGeodesicM(_pointA, _pointB).Returns(_expectedLengthMeters);
            _instance = new GetGeodesicLengthUseCase(_calculatorMock);
        }

        [Fact]
        public void EnUS_Culture_Result_In_Miles()
        {
            var input = new GeodesicLengthInputDto
            {
                GeoPoints = [ _pointA, _pointB ],
                CultureName = "en-US"
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
                GeoPoints = [_pointA, _pointB]
            };

            var actual = _instance.GetGeodesicLength(input);

            Assert.Multiple(() =>
            {
                Assert.Equal(_expectedLengthMeters * 0.001, actual.Length);
                Assert.Equal(DistanceUnits.Kilometers.ToString(), actual.Units);
            });
        }
    }
}