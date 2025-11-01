using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;
using RCPS.Infrastructure.Data;

namespace RCPS.Infrastructure.Repositories;

public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly RcpsDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public RepositoryBase(RcpsDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAsync(
        System.Linq.Expressions.Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbSet.AsQueryable();
        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return;
        }

        DbSet.Remove(entity);
    }

    public IQueryable<TEntity> Query() => DbSet.AsQueryable();
}
