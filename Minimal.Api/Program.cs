using Minimal.Api.Bootstrap;
using Minimal.Api.Modules;

var app = AppBuilder.Build(args);

app.BuildPipeline();

app.RegisterBasicEndpoints();
app.RegisterArtistEndpoints();

app.Run();

public partial class Program { }