using Actions.Commands;
using Model.Utils;

namespace Actions;

public class CommandFactory
{
    public CommandFactory(ModelDataContext context)
    {
        Context = context;
    }

    public ModelDataContext Context { get; set; }

    public T Create<T>()
        where T : Command, new()
    {
        var query = new T();
        query.Context = Context;
        return query;
    }
}