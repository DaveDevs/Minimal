using Model.Entities;
using Model.Utils;
using static Actions.Commands.ArtistCreateCommand;

namespace Actions.Commands;

public abstract class CommandBase
{
    public ModelDataContext Context { get; set; }
}

public abstract class Command<TEntity, TRequest> : CommandBase
    where TEntity : Entity
    where TRequest : UserRequestBase
{
    public TRequest Props { get; set; }

    public abstract Task Execute();
}

public abstract class UserRequestBase
{
}