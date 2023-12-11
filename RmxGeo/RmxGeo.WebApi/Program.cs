namespace RmxGeo.WebApi;

public class Program
{
    /*
* ����������� � ������ �������� ���������

6. ����� �� ��� ���.

     */
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        new Application.ContainerModule().ConfigureServices(builder.Services, configuration);

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.MapGetGeodesicLengthEndpoint();

        app.Run();
    }
}
