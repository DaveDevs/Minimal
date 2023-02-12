using FluentValidation;
using FluentValidation.Results;
using Model.Entities;
using Model.Utils;
using static Actions.Commands.ArtistCreateCommand;

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
        await this.Validate();
        await this.InvokeLogic();
    }

    public async Task Validate()
    {
        var validator = this.Props.NewValidator();
        ValidationResult result = await validator.ValidateAsync(new ValidationContext<TRequest>(this.Props));
        if (result != null)
        {
            throw new ValidationException(result.Errors);
        }
    }

    protected abstract Task InvokeLogic();
}

public abstract class UserRequestBase
{
    private class DefaultValidator : AbstractValidator<UserRequestBase>
    {
    }

    public virtual IValidator NewValidator()
    {
        return new DefaultValidator();
    }
}