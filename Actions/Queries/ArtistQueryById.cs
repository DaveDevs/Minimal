using Actions.Commands;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Actions.Queries;

public class ArtistQueryById : QuerySingle<Artist, ArtistQueryById.Properties>
{
    public override Task<Artist> Execute()
    {
        return Context.Artists.SingleAsync(x => x.Id == Props.Id);
    }

    public class Properties : UserRequestBase
    {
        public int Id { get; set; }
    }
}