using Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Minimal.Cli.Services;
using Model.Utils;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<CommandFactory>();
        services.AddScoped<QueryFactory>();
        services.AddScoped<Populator>();
        services.AddScoped<ModelContext>();
        services.AddScoped<DataMapper>();
        services.AddDbContext<MinimalDbContext>(options =>
        {
            options.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Minimal;Integrated Security=SSPI");
        });
    }).Build();

#pragma warning disable CS8602
await host.Services.GetService<Populator>().Go();
#pragma warning restore CS8602
await host.RunAsync();