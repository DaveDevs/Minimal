using Actions.Commands;
using Model.Entities;

namespace Actions.Queries;

public abstract class QueryBase : Action
{
}

public abstract class Query<TReturn, TRequest> : QueryBase
{
    public TRequest Props { get; protected set; } = default!;

    public void SetProps(TRequest props) => Props = props;

    public abstract Task<TReturn> Execute();
}

public abstract class QueryList<TEntity, TRequest> : Query<List<TEntity>, TRequest>
    where TEntity : Entity
    where TRequest : RequestBase
{
    public abstract override Task<List<TEntity>> Execute();
}

public abstract class QuerySingle<TEntity> : Query<TEntity, QuerySingle<TEntity>.Properties>
    where TEntity : Entity
{
    public override Task<TEntity> Execute()
    {
        return this.ModelContext.DataMapper.GetById<TEntity>(this.Props.Id);
    }

    public class Properties : RequestBase
    {
        public int Id { get; protected set; }
    }
}