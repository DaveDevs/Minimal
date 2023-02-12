using FluentAssertions;
using Minimal.Api.Modules;
using NUnit.Framework;

namespace Minimal.Api.Tests.Modules;

[TestFixture]
public class ArtistTests : AbstractApiTest
{
    [Test]
    public async Task GetByAll()
    {
        var result = await Client.GetStringAsync("/Artists");

        result.Should().Be("[]");
    }
}