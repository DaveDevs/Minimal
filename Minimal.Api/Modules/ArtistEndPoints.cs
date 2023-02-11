using Actions;
using Actions.Queries;

namespace Minimal.Api.Modules
{
    public static class ArtistEndPoints
    {
        public static void RegisterArtistEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGet("/Artists", (QueryFactory queryFactory) =>
                {
                    var query = queryFactory.Create<ArtistsQueryAll>();
                    return query.Execute();
                })
                .WithName("Artists")
                .WithOpenApi();
        }
    }
}
