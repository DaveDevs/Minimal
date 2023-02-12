using Actions;
using Actions.Commands;
using Actions.Queries;
using Minimal.Api.Utils;
using Model.Entities;

namespace Minimal.Api.Modules;

public static class ArtistEndpoints
{
    public static string BaseRoute = "/Artists";

    public static void RegisterArtistEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.QueryList<ArtistsQueryAll, Artist, UserRequestBase>(BaseRoute);

        routeBuilder.QuerySingle<ArtistQueryById, Artist, ArtistQueryById.Properties>($"{BaseRoute}/{{id:int}}");

        routeBuilder.QueryPost<ArtistsQuerySearch, Artist, ArtistsQuerySearch.Properties>($"{BaseRoute}/Search");

        routeBuilder.CommandPost<ArtistCreateCommand, Root, ArtistCreateCommand.ArtistCreateProperties>($"{BaseRoute}/Create");
    }
}