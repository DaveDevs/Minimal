using FluentValidation;
using Model.Entities;
using Model.Utils;

namespace Actions.Commands;

public abstract class CommandBase
{
    public ModelDataContext Context { get; set; }

    public abstract Task Execute();
}

public abstract class Command<TEntity, TRequest> : CommandBase
    where TEntity : Entity
    where TRequest : UserRequestBase
{
    public TRequest Props { get; set; }

    public override async Task Execute()
    {
        await Validate();
        await InvokeLogic();
    }

    public async Task Validate()
    {
        var validator = Props.NewValidator();
        var result = await validator.ValidateAsync(new ValidationContext<TRequest>(Props));
        if (result != null) throw new ValidationException(result.Errors);
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