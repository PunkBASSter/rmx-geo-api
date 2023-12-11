namespace RmxGeo.Domain.Tests;

public class GeoPointValidationTests
{
    [Theory] //lat, lon
    [InlineData(-91, 120)]
    [InlineData(91, 120)]
    [InlineData(90, 182)]
    [InlineData(90, -182)]
    public void GeoPoint_ThrowsInvalidInputException_ForInvalidData(params object[] testData)
    {
        (double lat, double lon) = (Convert.ToDouble(testData[0]), Convert.ToDouble(testData[1]));

        Assert.Throws<InvalidInputException>(() => new GeoPoint(lat, lon));
    }
}
