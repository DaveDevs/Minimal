using System.Text.Json.Serialization;
using Model.Utils;

namespace Model.Entities;

public abstract class Entity
{
    protected Entity(int id)
    {
        Id = id;
    }

    protected Entity(ModelContext modelContext)
    {
        ModelContext = modelContext;
    }

    public int Id { get; set; }
    
    [JsonIgnore] public ModelContext ModelContext { get; set; } = null!;
}