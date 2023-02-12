using FluentValidation;
using Model.Entities;
using Model.Utils;

namespace Actions.Commands;

public abstract class CommandBase
{
    public ModelDataContext Context { get; set; }

    public abstract Task Execute();
}

public abstract class RootCommand<TRequest> : Command<Root, TRequest>
    where TRequest : RequestBase
{
    protected override void LoadTarget()
    {
        Target = new Root(Context);
    }
}

public abstract class Command<TEntity, TRequest> : CommandBase
    where TEntity : Entity
    where TRequest : RequestBase
{
    public TRequest Props { get; set; }

    public TEntity Target { get; set; }

    public int TargetId { get; set; }

    protected virtual void LoadTarget()
    {
        Context.Set<TEntity>().Single(x => x.Id == TargetId);
    }

    public override async Task Execute()
    {
        LoadTarget();
        await Validate();
        await InvokeLogic();
    }

    public async Task Validate()
    {
        var validator = Props.NewValidator();
        var result = await validator.ValidateAsync(new ValidationContext<TRequest>(Props));
        if (!result.IsValid) throw new ValidationException(result.Errors);
    }

    protected abstract Task InvokeLogic();
}

public abstract class RequestBase
{
    public virtual IValidator NewValidator()
    {
        return new DefaultValidator();
    }

    private class DefaultValidator : AbstractValidator<RequestBase>
    {
    }
}