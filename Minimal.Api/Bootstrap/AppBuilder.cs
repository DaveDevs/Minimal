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

            var app = builder.Build();

            return app;
        }
    }
}
