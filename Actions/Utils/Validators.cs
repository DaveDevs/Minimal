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

    public static IRuleBuilderOptions<T, int> IsAnId<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new GreaterThanValidator<T, int>(0));
    }
}