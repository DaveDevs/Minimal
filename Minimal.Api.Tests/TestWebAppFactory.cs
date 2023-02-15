using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model.Utils;

namespace Minimal.Api.Tests;

public class TestWebAppFactory : WebApplicationFactory<Program>
{
    public const string TestConnectionString =
        "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MinimalTest;Integrated Security=SSPI";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<MinimalDbContext>));

            services.Remove(dbContextDescriptor!);

            services.AddDbContext<MinimalDbContext>(options =>
            {
                options.UseSqlServer(TestConnectionString);
            });
        });

        builder.UseEnvironment("Development");
    }
}