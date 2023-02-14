using Actions.Queries;
using Model.Utils;

namespace Actions;

public class QueryFactory
{
    public QueryFactory(ModelContext context)
    {
        Context = context;
    }

    public ModelContext Context { get; set; }

    public T Create<T>()
        where T : QueryBase, new()
    {
        var query = new T();
        query.Context = Context;
        return query;
    }
}