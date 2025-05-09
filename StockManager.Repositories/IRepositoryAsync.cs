using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace StockManager.Repositories;

public interface IRepositoryAsync<T> where T: class
{
    ValueTask<T?> FindAsync(params object[] param);
    ValueTask<T?> SingleOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object?>>? includes = null,
        CancellationToken cancellationToken = default
    );
    Task<PaginatedResponse<T>> ListPaginateAsync(
        Expression<Func<T, bool>>? predicate = null,
        string? orderBy = null,
        bool? ascending = true,
        Func<IQueryable<T>, IIncludableQueryable<T, object?>>? includes = null, 
        int page = 0,
        int size = 10,
        CancellationToken cancellationToken = default
        );
    ValueTask<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    ValueTask<EntityEntry<T>> InsertAsync(T entity);
    IQueryable<T> GetQueryable();
    void Update(T entity);
    void Update(T[] entities);
    void Update(ICollection<T> entities);
    void Delete(T entity);
    void Delete(T[] entities);
    void Delete(ICollection<T> entities);
}