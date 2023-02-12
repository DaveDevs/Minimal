using FluentAssertions;
using FluentAssertions.Equivalency;
using Model.Utils;

namespace Minimal.Api.Tests.Utils;

internal class CustomRules : IMemberSelectionRule
{
    public bool IncludesMembers => false;

    public IEnumerable<IMember> SelectMembers(INode currentNode, IEnumerable<IMember> selectedMembers,
        MemberSelectionContext context)
    {
        var props = context.Type.Properties().Where(x => x.PropertyType != typeof(ModelDataContext));

        return props.Select(x => new Property(x, currentNode)).ToList();
    }

    public override string ToString()
    {
        return "Include all non-private fields";
    }
}

public static class FluentAssertionsBootstrapper
{
    public static void Bootstrap()
    {
        AssertionOptions.AssertEquivalencyUsing(o => o.Using(new CustomRules()));
    }
}