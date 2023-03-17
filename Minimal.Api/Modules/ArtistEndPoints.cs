using Actions;
using Actions.Commands;
using Actions.Queries;
using Minimal.Api.Utils;
using Model.Entities;

namespace Minimal.Api.Modules;

public static class ArtistEndpoints
{
    public static string BaseRoute = "Artists";

    public static string ArtistQueryByIdRoute = $"{BaseRoute}/{{id:int}}";
    public static string ArtistsQuerySearchRoute = $"{BaseRoute}/Search";
    public static string ArtistCommandCreateRoute = $"{BaseRoute}/Create";
    public static string ArtistCommandUpdateRoute = $"{BaseRoute}/Update";
    public static string AlbumCommandCreateRoute = $"{BaseRoute}/Album/Create";

    public static void RegisterArtistEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.QueryById<ArtistQueryById, Artist>(ArtistQueryByIdRoute);

        routeBuilder.QueryList<ArtistsQueryAll, Artist, RequestBase>(BaseRoute);

        routeBuilder.QueryPost<ArtistsQuerySearch, Artist, ArtistsQuerySearch.Properties>(ArtistsQuerySearchRoute);

        routeBuilder.CommandPost<ArtistCommandCreate, Root, ArtistCommandCreate.ArtistCreateProperties>(ArtistCommandCreateRoute);

        routeBuilder.CommandPost<ArtistCommandUpdate, Artist, ArtistCommandUpdate.ArtistUpdateProperties>(ArtistCommandUpdateRoute);

        routeBuilder.CommandPost<AlbumCommandCreate, Artist, AlbumCommandCreate.AlbumCreateProperties>(AlbumCommandCreateRoute);
    }
}
