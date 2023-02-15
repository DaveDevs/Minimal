using FluentValidation;
using Model.Entities;

namespace Actions.Commands;

public abstract class CommandBase : Action
{
    public abstract Task Execute();
}

public abstract class RootCommand<TRequest> : Command<Root, TRequest>
    where TRequest : RequestBase
{
    public override int TargetId => throw new NotImplementedException("Target = root. No Id.");

    protected override Task LoadTarget()
    {
        Target = new Root();
        Target.SetModelContext(ModelContext);
        return Task.CompletedTask;
    }
}

public abstract class Command<TEntity, TRequest> : CommandBase
    where TEntity : Entity
    where TRequest : RequestBase
{
    public TRequest Props { get; protected set; } = null!;

    public TEntity Target { get; protected set; } = null!;

    public abstract int TargetId { get; }

    public void SetProps(TRequest props)
    {
        Props = props;
    }

    protected virtual async Task LoadTarget()
    {
        Target = await ModelContext.DataMapper.GetById<TEntity>(TargetId);
    }

    public override async Task Execute()
    {
        try
        {
            await ModelContext.DataMapper.StartTransaction();
            await LoadTarget();
            await Validate();
            await InvokeLogic();
            await ModelContext.DataMapper.CommitTransaction();
        }
        catch
        {
            await ModelContext.DataMapper.RollbackTransaction();
            throw;
        }
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