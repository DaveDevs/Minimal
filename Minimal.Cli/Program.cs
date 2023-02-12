using Actions;
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
        services.AddDbContext<ModelDataContext>();
    }).Build();

host.Services.GetService<Populator>().Go();
await host.RunAsync();