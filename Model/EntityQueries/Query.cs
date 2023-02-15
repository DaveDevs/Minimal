using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Utils;

namespace Model.EntityQueries;

public abstract class EntityQueryBase
{
    protected EntityQueryBase(ModelContext context)
    {
        Context = context;
    }

    public ModelContext Context { get; protected set; }
}

public abstract class EntityQueryList<TEntity> : EntityQueryBase
    where TEntity : Entity
{
    protected EntityQueryList(ModelContext context) : base(context)
    {
    }

    public Task<List<TEntity>> Execute()
    {
        return this.Filter<TEntity>(this.Context.DataMapper.Set<TEntity>()).ToListAsync();
    }

    public virtual IQueryable<TEntity> Filter<T>(IQueryable<TEntity> queryable)
    {
        return queryable;
    }
}