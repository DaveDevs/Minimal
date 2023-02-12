using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Model.Utils;

namespace Model.Entities
{
    public abstract class Entity
    {
        protected Entity(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        protected Entity(ModelDataContext modelDataContext)
        {
            ModelDataContext = modelDataContext;
        }

        [JsonIgnore]
        public ModelDataContext ModelDataContext { get; set; }
    }
}
