using FluentValidation;
using FluentValidation.Validators;

namespace Actions.Utils;

public static class DateValidators
{
    public static IRuleBuilderOptions<T, DateOnly> MustBeBefore<T>(this IRuleBuilder<T, DateOnly> ruleBuilder,
        DateTime valueToCompare)
    {
        return ruleBuilder.SetValidator(new LessThanValidator<T, DateOnly>(DateOnly.FromDateTime(valueToCompare)));
    }
}