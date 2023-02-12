using Actions.Queries;
using Model.Utils;

namespace Actions;

public class QueryFactory
{
    public QueryFactory(ModelDataContext context)
    {
        Context = context;
    }

    public ModelDataContext Context { get; set; }

    public T Create<T>()
        where T : Query, new()
    {
        var query = new T();
        query.Context = Context;
        return query;
    }
}