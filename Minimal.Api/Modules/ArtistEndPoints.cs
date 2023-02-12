using Actions;
using Actions.Commands;
using Actions.Queries;
using Minimal.Api.Utils;
using Model.Entities;

namespace Minimal.Api.Modules;

public static class ArtistEndPoints
{
    public static string BaseRoute = "/Artists";

    public static void RegisterArtistEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(BaseRoute, EndpointUtils.ForQueryGet<ArtistsQueryAll, Artist, UserRequestBase>);

        routeBuilder.MapGet($"{BaseRoute}/{{id:int}}", EndpointUtils.ForQueryGetSingle<ArtistQueryById, Artist, ArtistQueryById.Properties>);

        routeBuilder.MapPost($"{BaseRoute}/Search", EndpointUtils.ForQueryPost<ArtistsQuerySearch, Artist, ArtistsQuerySearch.Properties>);

        routeBuilder.MapPost($"{BaseRoute}/Create", EndpointUtils.ForCommand<ArtistCreateCommand, Artist, ArtistCreateCommand.ArtistCreateProperties>);
    }
}