using Actions.Commands;
using Model.Utils;

namespace Actions;

public class CommandFactory
{
    public CommandFactory(ModelContext context)
    {
        Context = context;
    }

    public ModelContext Context { get; set; }

    public T Create<T>()
        where T : CommandBase, new()
    {
        var command = new T();
        command.ModelContext = Context;
        return command;
    }
}