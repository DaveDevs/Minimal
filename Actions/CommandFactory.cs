using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actions.Commands;
using Actions.Queries;
using Model.Utils;

namespace Actions
{
    public class CommandFactory
    {
        public ModelDataContext Context { get; set; }

        public CommandFactory(ModelDataContext context)
        {
            Context = context;
        }

        public T Create<T>()
            where T : Command, new()
        {
            var query = new T();
            query.Context = this.Context;
            return query;
        }
    }
}
