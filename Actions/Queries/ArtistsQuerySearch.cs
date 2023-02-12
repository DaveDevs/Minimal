using Actions.Commands;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Actions.Queries;

public class ArtistsQuerySearch : QueryList<Artist, ArtistsQuerySearch.Properties>
{
    public class Properties : RequestBase
    {
        public string Name { get; set; }
    }

    public override Task<List<Artist>> Execute()
    {
        return this.Context.Artists.Where(x => x.Name.Contains(this.Props.Name)).ToListAsync();
    }
}