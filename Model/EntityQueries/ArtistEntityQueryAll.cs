using Model.Entities;
using Model.Utils;

namespace Model.EntityQueries
{
    public class ArtistEntityQueryAll : EntityQueryList<Artist>
    {
        public ArtistEntityQueryAll(ModelContext context) : base(context)
        {
        }
    }
}
