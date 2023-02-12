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
    where TRequest : UserRequestBase
{
    protected override void LoadTarget()
    {
        this.Target = new Root(this.Context);
    }
}

public abstract class Command<TEntity, TRequest> : CommandBase
    where TEntity : Entity
    where TRequest : UserRequestBase
{
    public TRequest Props { get; set; }

    public TEntity Target { get; set; }

    public int TargetId { get; set; }

    protected virtual void LoadTarget()
    {
        this.Context.Set<TEntity>().Single(x => x.Id == this.TargetId);
    }

    public override async Task Execute()
    {   
        this.LoadTarget();
        await Validate();
        await InvokeLogic();
    }

    public async Task Validate()
    {
        var validator = Props.NewValidator();
        var result = await validator.ValidateAsync(new ValidationContext<TRequest>(Props));
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }

    protected abstract Task InvokeLogic();
}

public abstract class UserRequestBase
{
    public virtual IValidator NewValidator()
    {
        return new DefaultValidator();
    }

    private class DefaultValidator : AbstractValidator<UserRequestBase>
    {
    }
}