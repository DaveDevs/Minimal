using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Actions.Queries;

public class ArtistsQueryAll : Query<Artist>
{
    public override Task<List<Artist>> Execute()
    {
        return Context.Artists.ToListAsync();
    }
}