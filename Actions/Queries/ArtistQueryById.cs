using Actions.Commands;
using Model.Entities;

namespace Actions.Queries;

public class ArtistQueryById : QuerySingle<Artist, ArtistQueryById.Properties>
{
    public override Task<Artist> Execute()
    {
        return this.ModelContext.DataMapper.GetById<Artist>(this.Props.Id);
    }

    public class Properties : RequestBase
    {
        public int Id { get; protected set; }
    }
}