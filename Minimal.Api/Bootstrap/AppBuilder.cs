using Actions;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Model.Utils;
using Model.Utils.Json;

namespace Minimal.Api.Bootstrap;

public static class AppBuilder
{
    public static WebApplication Build(string[] strings)
    {
        var builder = WebApplication.CreateBuilder(strings);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            //Custom Ids because swaggergen can't handle duplicate class names
            //Remove the + from full name (nested classes) cos it also breaks swaggergen
            options.CustomSchemaIds(type => type.FullName.Replace("+", ""));

            // see: https://storck.io/posts/serializing-net-6-dateonly-to-json/
            options.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date",
                Example = new OpenApiString("2022-01-01")
            });
        });

        builder.Services.AddDbContext<ModelDataContext>();
        builder.Services.AddScoped<QueryFactory>();
        builder.Services.AddScoped<CommandFactory>();

        builder.Services.Configure<JsonOptions>(options =>
        {
            // see: https://storck.io/posts/serializing-net-6-dateonly-to-json/
            options.SerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        });

        var app = builder.Build();
        return app;
    }
}