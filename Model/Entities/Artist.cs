namespace Model.Entities;

public class Artist : Entity
{
    public Artist() : base(0)
    {
    }

    public Artist(int id, string name, DateOnly dateOfBirth) : base(id)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
    }

    public string Name { get; set; } = string.Empty;

    public DateOnly DateOfBirth { get; set; }

    public async Task CreateAlbum(string name, int releaseYear)
    {
        await ModelContext.DataMapper.Create(new Album(0, name, releaseYear, this));
    }
}