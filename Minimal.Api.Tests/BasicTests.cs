using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace Minimal.Api.Tests;

[TestFixture]
public class BasicTests
{
    [Test]
    public void Simple_Assert()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public async Task HelloWorld_Success()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.GetStringAsync("/hello");

        Assert.AreEqual("Hello World!", response);
    }
}