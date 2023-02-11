using Model.Entities;
using Model.Utils;

namespace Actions.Queries
{
    public abstract class Query 
    {
        public ModelDataContext Context { get; set; }
    }

    public abstract class Query<T> : Query 
        where T : Entity
    {
        public abstract Task<List<Artist>> Execute();
    }
}