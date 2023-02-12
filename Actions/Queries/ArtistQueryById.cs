using Actions.Commands;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Actions.Queries;

public class ArtistQueryById : QuerySingle<Artist, ArtistQueryById.Properties>
{
    public class Properties : UserRequestBase
    {
        public int Id { get; set; }
    }

    public override Task<Artist> Execute()
    {
        return this.Context.Artists.SingleAsync(x => x.Id == this.Props.Id);
    }
}