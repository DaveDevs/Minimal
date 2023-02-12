using System.Text.Json.Serialization;
using Model.Utils;

namespace Model.Entities;

public abstract class Entity
{
    protected Entity(int id)
    {
        Id = id;
    }

    protected Entity(ModelDataContext modelDataContext)
    {
        ModelDataContext = modelDataContext;
    }

    public int Id { get; set; }

    [JsonIgnore] public ModelDataContext ModelDataContext { get; set; }
}