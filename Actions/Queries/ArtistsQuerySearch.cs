using Actions.Commands;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.EntityQueries;

namespace Actions.Queries;

public class ArtistsQuerySearch : QueryList<Artist, ArtistsQuerySearch.Properties>
{
    public class Properties : RequestBase
    {
        public string Name { get; set; }

        public Properties(string name)
        {
            Name = name;
        }
    }

    public override Task<List<Artist>> Execute()
    {
        return new ArtistEntityQueryByName(this.ModelContext, this.Props.Name).Execute();
    }
}