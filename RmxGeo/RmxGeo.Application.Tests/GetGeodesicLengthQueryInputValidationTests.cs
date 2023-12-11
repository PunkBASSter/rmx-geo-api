using NSubstitute;
using RmxGeo.Application.CalculateGeodesicLength;
using RmxGeo.Domain;

namespace RmxGeo.Application.Tests
{
    public class GetGeodesicLengthQueryInputValidationTests
    {
        private readonly GetGeodesicLengthQuery _instance;

        public GetGeodesicLengthQueryInputValidationTests()
        {
            _instance = new GetGeodesicLengthQuery(Substitute.For<IGeodesicCalculator>());
        }

        [Theory]
        [InlineData(53.297975, -6.372663, 41.385101, -81.440440, 53.297975)]
        [InlineData(53.297975, -6.372663, 41.385101)]
        [InlineData(53.297975)]

        public void Uneven_Coordinates_Cause_Exception(params object[] testData)
        {
            var input = new GeodesicLengthInputDto
            {
                Coordinates = [.. testData.Select(Convert.ToDouble)]
            };

            Assert.Throws<InvalidInputException>(() => _instance.GetGeodesicLength(input));
        }
    }
}
