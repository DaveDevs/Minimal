using Actions;
using Actions.Queries;
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
        }
    }
}
