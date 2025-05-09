using Microsoft.EntityFrameworkCore;

namespace StockManager.Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepositoryAsync<T> GetRepositoryAsync<T>() where T : class;
    Task<bool> CommitAsync();
    
}

public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    TContext Context { get; }
}