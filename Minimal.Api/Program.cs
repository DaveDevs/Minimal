using Minimal.Api.Bootstrap;
using Minimal.Api.Modules;

var app = AppBuilder.Build(args);

app.BuildPipeline();

app.RegisterWeatherEndpoints();
app.RegisterArtistEndpoints();

app.Run();