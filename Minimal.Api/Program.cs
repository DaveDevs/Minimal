using Microsoft.EntityFrameworkCore;
using Minimal.Api.Bootstrap;
using Minimal.Api.Modules;
using Model.Utils;

var app = AppBuilder.Build(args);

app.BuildPipeline();

app.RegisterWeatherEndpoints();
app.RegisterArtistEndpoints();

app.Run();