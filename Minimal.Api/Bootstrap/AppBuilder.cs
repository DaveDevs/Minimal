using Actions;
using Model.Utils;

namespace Minimal.Api.Bootstrap
{
    public static class AppBuilder
    {
        public static WebApplication Build(string[] strings)
        {
            var builder = WebApplication.CreateBuilder(strings);

            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ModelDataContext>();
            builder.Services.AddScoped<QueryFactory>();
            var app = builder.Build();

            return app;
        }
    }
}
