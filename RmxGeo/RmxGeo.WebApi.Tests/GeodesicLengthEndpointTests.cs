using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using RmxGeo.Application.CalculateGeodesicLength;
using System.Net;
using System.Net.Http.Headers;

namespace RmxGeo.WebApi.Tests;

public class GeodesicLengthEndpointTests : IAsyncLifetime
{
    private HttpClient _httpClient = null!;
    private WebApplicationFactory<Program> _app = null!;

    public Task InitializeAsync()
    {
        _app = new WebApplicationFactory<Program>();

        _httpClient = _app.CreateClient();
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        var appDisposeTask = _app.DisposeAsync();
        await Task.Run(() => _httpClient.Dispose());
        await appDisposeTask;
    }

    [Theory]
    [InlineData("/geodesic/length?coordinates=53.297975,-6.372663,41.385101,-81.440440", 5536.339, "Kilometers")]
    [InlineData("/geodesic/length?coordinates=53.297975,-6.372663,41.385101,-81.440440&culture=en", 5536.339, "Kilometers")]
    [InlineData("/geodesic/length?coordinates=53.297975,-6.372663,41.385101,-81.440440&culture=en-US", 3440.120, "Miles")]
    public async Task GeoEndpointHappyPath(params object[] testData)
    {
        var response = await _httpClient.GetAsync(testData[0].ToString());

        var returnedJson = await response.Content.ReadAsStringAsync();
        var geoLengthResult = JsonConvert.DeserializeObject<GeodesicLengthResultDto>(returnedJson)!;

        var expectedLength = double.Parse(testData[1].ToString()!);
        
        Assert.Multiple(() =>
        {
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedLength, geoLengthResult.Length, 0.01);
            Assert.Equal(testData[2], geoLengthResult.Units);
        });
    }

    [Theory]
    [InlineData("en-US", 3440.120, "Miles")]
    [InlineData("en-GB", 5536.339, "Kilometers")]
    [InlineData("en", 5536.339, "Kilometers")]
    public async Task GeoEndpoint_Culture_PassedViaHeader(params object[] testData)
    {
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(testData[0].ToString()!));
        var response = await _httpClient.GetAsync("/geodesic/length?coordinates=53.297975,-6.372663,41.385101,-81.440440");

        var returnedJson = await response.Content.ReadAsStringAsync();
        var geoLengthResult = JsonConvert.DeserializeObject<GeodesicLengthResultDto>(returnedJson)!;

        var expectedLength = double.Parse(testData[1].ToString()!);

        Assert.Multiple(() =>
        {
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedLength, geoLengthResult.Length, 0.01);
            Assert.Equal(testData[2], geoLengthResult.Units);
        });
    }

    [Theory]
    [InlineData("/geodesic/length?coordinates=53.297975,-6.372663,41.385101,-81.440440,53.297975,-6.372663,41.385101")]
    [InlineData("/geodesic/length?coordinates=53.297975,-6.372663,41.385101,-81.440440,53.297975")]
    [InlineData("/geodesic/length?coordinates=53.297975,-6.372663,41.385101")]
    public async Task InvalidCoordinatesCause400BadRequestErr(params object[] testData)
    {
        var response = await _httpClient.GetAsync(testData[0].ToString());
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}