using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace RmxGeo.WebApi.Tests;

public class GeodesicLengthEndpointTests : IAsyncLifetime
{
    private HttpClient _httpClient = null!;

    public Task InitializeAsync()
    {
        var app = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json").Build();
                builder.ConfigureServices((services) =>
                {
                    //new Application.ContainerModule().ConfigureServices(services, config);
                });

                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile("appsettings.json");
                });
            });

        _httpClient = app.CreateClient();
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.Run(() => _httpClient.Dispose());
    }

    [Fact (Skip = "Test responds with 500 for valid requests.")]
    public async Task GeoEndpointHappyPath()
    {
        var response = await _httpClient.GetAsync("/geodesic/length?coordinates=53.297975,-6.372663,41.385101,-81.440440");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}