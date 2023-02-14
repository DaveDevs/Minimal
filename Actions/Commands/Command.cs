using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Actions.Commands;

public abstract class CommandBase : Action
{
    public abstract Task Execute();
}

public abstract class RootCommand<TRequest> : Command<Root, TRequest>
    where TRequest : RequestBase
{
    protected override Task LoadTarget()
    {
        Target = new Root(Context);
        return Task.CompletedTask;
    }
}

public abstract class Command<TEntity, TRequest> : CommandBase
    where TEntity : Entity
    where TRequest : RequestBase
{
    public TRequest Props { get; set; } = null!;

    public TEntity Target { get; set; } = null!;

    public int TargetId { get; set; }

    protected virtual async Task LoadTarget()
    {
        await Context.DataMapper.GetById<TEntity>(this.TargetId);
    }

    public override async Task Execute()
    {
        await LoadTarget();
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