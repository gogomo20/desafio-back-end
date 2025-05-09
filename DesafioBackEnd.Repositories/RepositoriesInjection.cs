
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace StockManager.Repositories;

public static class RepositoriesInjection
{
    public static void AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        services.AddScoped<IUnitOfWork, IUnitOfWork<TContext>>();
        services.AddScoped<IUnitOfWork<TContext>, IUnitOfWork<TContext>>();
    }
}