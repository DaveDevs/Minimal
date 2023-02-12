using Model.Entities;
using Model.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Commands
{
    public abstract class Command
    {
        public ModelDataContext Context { get; set; }
    }

    public abstract class Command<T> : Command
        where T : Entity
    {
        public abstract void Execute();
    }
}
