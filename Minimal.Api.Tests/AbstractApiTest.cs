using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Minimal.Api.Tests.Utils;
using Model.Utils;
using NUnit.Framework;

namespace Minimal.Api.Tests;

[TestFixture]
public class AbstractApiTest
{
    protected HttpClient Client { get; private set; } = null!;

    private TestWebAppFactory Application { get; set; } = null!;

    protected MinimalDbContext Context { get; private set; } = null!;

    [OneTimeSetUp]
    public void SetupApi()
    {
        FluentAssertionsBootstrapper.Bootstrap();
        Application = new TestWebAppFactory();
        Client = Application.CreateClient();
    }

    [SetUp]
    public void BeforeEachTest()
    {
        Context = Application.Services.CreateScope().ServiceProvider.GetService<MinimalDbContext>()!;
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();
    }

    [OneTimeTearDown]
    public void CloseApi()
    {
        Application.Dispose();
    }
}