using Model.Utils;

namespace Model.Entities;

public class Artist : Entity
{
    public Artist(ModelDataContext modelDataContext) : base(modelDataContext)
    {
    }

    public Artist(int id, string name, DateOnly dateOfBirth) : base(id)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
    }

    public string Name { get; set; }

    public DateOnly DateOfBirth { get; set; }
}