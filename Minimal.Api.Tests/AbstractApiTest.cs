using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Minimal.Api.Tests.Utils;
using Model.Utils;
using NUnit.Framework;

namespace Minimal.Api.Tests;

[TestFixture]
public class AbstractApiTest
{
    protected HttpClient Client { get; private set; }

    private TestWebAppFactory Application { get; set; }

    protected ModelDataContext Context { get; private set; }

    [OneTimeSetUp]
    public void SetupApi()
    {
        FluentAssertionsBootstrapper.Bootstrap();
        Application = new TestWebAppFactory();
        Client = Application.CreateClient();

        Context = Application.Services.CreateScope().ServiceProvider.GetService<ModelDataContext>();
    }

    [SetUp]
    public void BeforeEachTest()
    {
        FluentAssertionsBootstrapper.Bootstrap();
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();
    }

    [OneTimeTearDown]
    public void CloseApi()
    {
        Application.Dispose();
    }
}