using Actions.Commands;
using Model.Utils;

namespace Actions;

public class CommandFactory
{
    public CommandFactory(ModelContext modelContext)
    {
        ModelContext = modelContext;
    }

    public ModelContext ModelContext { get; protected set; }

    public T Create<T>()
        where T : CommandBase, new()
    {
        var command = new T();
        command.SetModelContext(ModelContext);
        return command;
    }
}