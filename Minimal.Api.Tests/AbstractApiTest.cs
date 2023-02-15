using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Minimal.Api.Tests.Utils;
using Model.Utils;
using NUnit.Framework;

namespace Minimal.Api.Tests;

[TestFixture]
public class AbstractApiTest
{
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
        Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();
    }

    [OneTimeTearDown]
    public void CloseApi()
    {
        Application.Dispose();
    }

    private TestWebAppFactory Application { get; set; } = null!;

    protected HttpClient Client { get; private protected set; } = null!;

    protected MinimalDbContext Context { get; private protected set; } = null!;
}