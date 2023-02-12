using Actions;
using Actions.Commands;
using Actions.Queries;

namespace Minimal.Api.Modules;

public static class ArtistEndPoints
{
    public static void RegisterArtistEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/Artists", GetArtists);

        routeBuilder.MapPost("/Artists/Create", CreateArtists);
    }

    internal static async Task<IResult> GetArtists(QueryFactory queryFactory)
    {
        var query = queryFactory.Create<ArtistsQueryAll>();
        var result = await query.Execute();
        return TypedResults.Ok(result);
    }

    internal static async Task<IResult> CreateArtists(ArtistCreateCommand.Properties request,
        CommandFactory commandFactory)
    {
        var command = commandFactory.Create<ArtistCreateCommand>();
        command.Props = request;
        await command.Execute();
        return TypedResults.Ok();
    }
}