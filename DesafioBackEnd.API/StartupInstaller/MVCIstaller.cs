using Microsoft.AspNetCore.Mvc;
using StockManager.Filters;

namespace StockManager.StartupInstaller;

public class MVCIstaller : IInstaller
{
    /// <summary>
    /// install the MVC 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);
        services.AddMvc(options => options.Filters.Add(new CustomExeptionFilter()));
        services.AddMvc(options => options.Filters.Add<PermissionsFilter>());
    }
}