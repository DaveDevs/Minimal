using Model.Entities;
using Model.Utils;

namespace Model.EntityQueries
{
    public class ArtistEntityQueryByName : EntityQueryList<Artist>
    {
        public string Name { get; set; }

        public ArtistEntityQueryByName(ModelContext context, string name) : base(context)
        {
            Name = name;
        }

        public override IQueryable<Artist> Filter<T>(IQueryable<Artist> queryable)
        {
            return queryable.Where(x => x.Name.Contains(this.Name));
        }
    }
}
