using Microsoft.EntityFrameworkCore;

namespace StockManager.Repositories;

public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IUnitOfWork where TContext: DbContext,IDisposable
{
    private Dictionary<Type, object> _repositories;
    public TContext Context { get; }
    public UnitOfWork(TContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public void Dispose()
    {
        Context?.Dispose();
    }

    public IRepositoryAsync<T> GetRepositoryAsync<T>() where T : class
    {
        if(_repositories == null) _repositories = new Dictionary<Type, object>();
        var type = typeof(IRepositoryAsync<T>);
        if (!_repositories.ContainsKey(type)) _repositories[type] = new RepositoryAsync<T>(Context);
        return (IRepositoryAsync<T>)_repositories[type];
    }

    public async Task<bool> CommitAsync()
    {
        return await Context.SaveChangesAsync() > 0;
    }

    
}