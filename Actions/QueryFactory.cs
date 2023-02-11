using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actions.Queries;
using Model.Utils;

namespace Actions
{
    public class QueryFactory
    {
        public ModelDataContext Context { get; set; }

        public QueryFactory(ModelDataContext context)
        {
            Context = context;
        }

        public T Create<T>()
            where T : Query, new()
        {
            var query = new T();
            query.Context = this.Context;
            return query;
        }
    }
}
