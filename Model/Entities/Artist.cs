using System.Text.Json.Serialization;

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

    [JsonInclude] 
    public string Name { get; protected internal set; } = string.Empty;

    [JsonInclude] 
    public DateOnly DateOfBirth { get; protected set; }

    public async Task Update(string name, DateOnly dateOfBirth)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        await DoUpdate();
    }

    public async Task CreateAlbum(string name, int releaseYear)
    {
        await ModelContext.DataMapper.Create(new Album(0, name, releaseYear, this));
    }
}