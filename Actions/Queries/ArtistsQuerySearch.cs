using Actions.Commands;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Actions.Queries;

public class ArtistsQuerySearch : Query<Artist, ArtistsQuerySearch.Properties>
{
    public class Properties : UserRequestBase
    {
        public string Name { get; set; }
    }

    public override Task<List<Artist>> Execute()
    {
        return Context.Artists.Where(x => x.Name.Contains(this.Props.Name)).ToListAsync();
    }
}