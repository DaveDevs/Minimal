using Model.Utils;

namespace Model.Entities;

public class Artist : Entity
{
    public Artist() : base(0)
    {
    }

    public Artist(ModelContext modelContext) : base(modelContext)
    {
    }

    public Artist(int id, string name, DateOnly dateOfBirth) : base(id)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
    }

    public string Name { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; } 
}