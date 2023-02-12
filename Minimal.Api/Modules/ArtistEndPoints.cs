using Actions;
using Actions.Commands;
using Actions.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Utils;

namespace Minimal.Api.Modules
{
    public static class ArtistEndPoints
    {
        public static void RegisterArtistEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGet("/Artists", async (QueryFactory queryFactory) =>
            {
                var query = queryFactory.Create<ArtistsQueryAll>();
                var result = await query.Execute();
                return result;
            });

            routeBuilder.MapPost("/Artists/Create", async (ArtistCreateCommand.Properties request, CommandFactory commandFactory) =>
            {
                var command = commandFactory.Create<ArtistCreateCommand>();
                command.Props = request;
                await command.Execute();
                return Results.Ok();
            });
        }
    }
}
