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
        where T : CommandBase, new()
    {
        var command = new T();
        command.Context = Context;
        return command;
    }
}