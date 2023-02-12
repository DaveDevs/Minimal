using Model.Entities;
using Model.Utils;

namespace Actions.Commands;

public abstract class Command
{
    public ModelDataContext Context { get; set; }
}

public abstract class Command<T> : Command
    where T : Entity
{
    public abstract Task Execute();
}