using StockManager.Aplication;
using StockManager.Persistense;
using StockManager.Repositories;

namespace StockManager.StartupInstaller;

public class ApiInstallService : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAplication(configuration);
        services.ConfigureConnectionContext(configuration);

        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
    }
}