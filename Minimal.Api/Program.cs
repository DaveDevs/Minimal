using Minimal.Api.Bootstrap;
using Minimal.Api.Modules;
using Model.Utils;

var app = AppBuilder.Build(args);
app.BuildPipeline();

app.RegisterWeatherEndpoints();
app.RegisterArtistEndpoints();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ModelDataContext>();
    db.Database.EnsureCreated();
}

app.Run();