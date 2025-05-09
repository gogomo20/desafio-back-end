using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Dynamic.Core;

namespace StockManager.Repositories;

public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public RepositoryAsync(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
        _dbSet.AsNoTracking();
    }

    public virtual ValueTask<T?> FindAsync(params object[] param)
    {
        return _dbSet.FindAsync(param);
    }

    public async ValueTask<T?> SingleOrDefaultAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object?>>? includes = null,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<T> query = _dbSet.AsNoTracking();
        query = query.Where(predicate);
        if (includes != null)
            query = includes(query);

        return await query.FirstOrDefaultAsync(cancellationToken);

    }
    
    public async Task<PaginatedResponse<T>> ListPaginateAsync(
        Expression<Func<T, bool>>? predicate = null,
        string? orderBy = null,
        bool? ascending = true,
        Func<IQueryable<T>, IIncludableQueryable<T, object?>>? include = null,
        int page = 1,
        int size = 10,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        if (predicate != null)
            query = query.Where(predicate);

        if (include != null)
            query = include(query);

        int total = await query.CountAsync(cancellationToken); // Contagem antes da paginação

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            string sortExpression = $"{orderBy} {(ascending == true ? "ascending" : "descending")}";
            query = query.OrderBy(sortExpression);
        }

        if (size > 0)
        {
            query = query.Skip((page - 1) * size).Take(size);
        }

        var data = await query.ToListAsync(cancellationToken);

        return new PaginatedResponse<T>
        {
            Page = page,
            Size = size,
            TotalItems = total,
            TotalPages = size > 0 ? (int)Math.Ceiling((double)total / size) : 1,
            Data = data
        };
    }

    public async ValueTask<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public IQueryable<T> GetQueryable()
    {
        IQueryable<T> query = _dbSet.AsNoTracking();
        return query;
    }

    public ValueTask<EntityEntry<T>> InsertAsync(T entity)
    {
        return _dbSet.AddAsync(entity);
    }
    public void Update(T entity) => _dbSet.Update(entity);

    public void Update(T[] entities) => _dbSet.UpdateRange(entities);

    public void Update(ICollection<T> entities) => _dbSet.UpdateRange(entities);
    public void Delete(T entity) => _dbSet.Remove(entity);
    public void Delete(T[] entities) => _dbSet.RemoveRange(entities);
    public void Delete(ICollection<T> entities) => _dbSet.RemoveRange(entities);
}