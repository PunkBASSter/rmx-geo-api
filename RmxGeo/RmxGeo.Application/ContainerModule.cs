using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RmxGeo.Application.CalculateGeodesicLength;
using RmxGeo.Domain;
using RmxGeo.Domain.SphereTrigonometry;

namespace RmxGeo.Application
{
    public class ContainerModule
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<GetGeodesicLengthQuery>();

            var geoCalculatorSettings = new SphereTrigonometryGeodesicCalculatorOptions();
            configuration.GetSection("SphereTrigonometryGeodesicCalculatorOptions").Bind(geoCalculatorSettings);
            services.AddSingleton(geoCalculatorSettings);
            services.AddScoped<IGeodesicCalculator, SphereTrigonometryGeodesicCalculator>();
        }
    }
}
