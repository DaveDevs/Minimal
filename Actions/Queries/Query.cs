using Actions.Commands;
using Model.Entities;
using Model.Utils;

namespace Actions.Queries;

public abstract class QueryBase
{
    public ModelDataContext Context { get; set; }
}

public abstract class Query<TEntity, TRequest> : QueryBase
    where TEntity : Entity
    where TRequest : UserRequestBase
{
    public TRequest Props { get; set; }

    public abstract Task<List<TEntity>> Execute();
}