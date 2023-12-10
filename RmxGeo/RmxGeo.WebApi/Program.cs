using Microsoft.AspNetCore.Mvc;
using RmxGeo.Application.CalculateGeodesicLength;
using RmxGeo.Application;

namespace RmxGeo.WebApi
{
    public class Program
    {
        /*
* Разобраться с глобал эксепшен хэндлером
* миддлвара для анализа Accept-Language заголовка?
* ЧТО С НАСТРОЙКАМИ РАДИУСА ЗЕМЛИ?

6. Тесты на веб апи.

         */
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();
            builder.Services.ConfigureApplicationServices();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            //https://localhost:7181/geodesic/length?coordinates=53.297975,-6.372663,41.385101,-81.440440
            //https://localhost:7181/geodesic/length?coordinates=53.297975,-6.372663,41.385101,-81.440440&culture=en-US
            app.MapGet("/geodesic/length", async ([FromServices] GetGeodesicLengthUseCase getGeodesicLengthUseCase,
                string coordinates,
                string? culture,
                [FromHeader(Name = "Accept-Language")] string? acceptLanguage) =>
            {
                GeodesicLengthResultDto res = await getGeodesicLengthUseCase.GetGeodesicLengthAsync(new GeodesicLengthInputDto()
                {
                    Coordinates = coordinates.Split([',',';']).Select(x => Convert.ToDouble(x)).ToArray(),
                    CultureName = culture ?? acceptLanguage ?? string.Empty
                });

                return Results.Ok(res);
            }).Produces<GeodesicLengthResultDto>();

            app.Run();
        }
    }
}
