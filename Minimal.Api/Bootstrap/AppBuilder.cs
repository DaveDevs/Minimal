using Actions;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
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
            // see: https://stackoverflow.com/questions/73162922/how-do-you-sort-endpoints-in-swagger-with-the-minimal-api-net-6
            options.TagActionsBy(d =>
            {
                var rootSegment = d.RelativePath?
                    .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault() ?? "Home";
                return new List<string> { rootSegment! };
            });

            // Custom Ids because swaggergen can't handle duplicate class names
            // Remove the + from full name (nested classes) cos it also breaks swaggergen
            options.CustomSchemaIds(type => type.FullName?.Replace("+", ""));

            // see: https://storck.io/posts/serializing-net-6-dateonly-to-json/
            options.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date",
                Example = new OpenApiString("2022-01-01")
            });
        });

        var connectionString = builder.Configuration.GetConnectionString("Default");

        builder.Services.AddDbContext<MinimalDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        builder.Services.Configure<JsonOptions>(options =>
        {
            // see: https://storck.io/posts/serializing-net-6-dateonly-to-json/
            options.SerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        });

        builder.Services.AddScoped<QueryFactory>();
        builder.Services.AddScoped<CommandFactory>();
        builder.Services.AddScoped<ModelContext>();
        builder.Services.AddScoped<DataMapper>();

        var app = builder.Build();
        return app;
    }
}