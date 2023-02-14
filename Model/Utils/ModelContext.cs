using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utils
{
    public class ModelContext
    {
        public DataMapper DataMapper { get; protected set; }

        public ModelContext(DataMapper dataMapper)
        {
            DataMapper = dataMapper;
        }
    }
}
