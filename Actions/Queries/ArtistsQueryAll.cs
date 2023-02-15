using Actions.Commands;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.EntityQueries;

namespace Actions.Queries;

public class ArtistsQueryAll : QueryList<Artist, RequestBase>
{
    public override Task<List<Artist>> Execute()
    {
        return new ArtistEntityQueryAll(this.ModelContext).Execute();
    }
}