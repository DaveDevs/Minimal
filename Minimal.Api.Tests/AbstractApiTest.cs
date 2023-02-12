using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model.Utils;
using NUnit.Framework;

namespace Minimal.Api.Tests;

public class AbstractApiTest
{
    protected HttpClient Client { get; private set; }

    private TestWebAppFactory Application { get; set; }

    [OneTimeSetUp]
    public void SetupApi()
    {
        Application = new TestWebAppFactory();
        Client = Application.CreateClient();

        var context = Application.Services.CreateScope().ServiceProvider.GetService<ModelDataContext>();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    [OneTimeTearDown]
    public void CloseApi()
    {
        this.Application.Dispose();
    }
}