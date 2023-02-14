using Actions.Commands;
using Model.Entities;
using Model.Utils;

namespace Actions.Queries;

public abstract class QueryBase : Action
{
}

public abstract class Query<TReturn, TRequest> : QueryBase
{
    public TRequest Props { get; set; } = default!;

    public abstract Task<TReturn> Execute();
}

public abstract class QueryList<TEntity, TRequest> : Query<List<TEntity>, TRequest>
    where TEntity : Entity
    where TRequest : RequestBase
{
    public abstract override Task<List<TEntity>> Execute();
}

public abstract class QuerySingle<TEntity, TRequest> : Query<TEntity, TRequest>
    where TEntity : Entity
    where TRequest : RequestBase
{
    public abstract override Task<TEntity> Execute();
}