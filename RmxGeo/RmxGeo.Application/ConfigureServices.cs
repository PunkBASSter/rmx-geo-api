using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RmxGeo.Application.CalculateGeodesicLength;
using RmxGeo.Domain;

namespace RmxGeo.Application
{
    public static class ConfigureServices
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<GetGeodesicLengthUseCase>();
            services.AddTransient<IGeodesicCalculator>(_ => new SphereTrigonometryGeodesicCalculator(6371000.0));
        }
    }
}
