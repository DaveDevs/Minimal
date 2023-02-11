using Minimal.Api.Bootstrap;
using Minimal.Api.Modules;

var app = AppBuilder.Build(args);
app.BuildPipeline();

app.Register();

app.Run();