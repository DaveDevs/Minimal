using System.Text.Json.Serialization;

namespace Model.Entities;

public class Album : Entity
{
    public Album() : base(0)
    {
    }

    public Album(int id, string name, int releaseYear, Artist artist) : base(id)
    {
        Name = name;
        ReleaseYear = releaseYear;
        Artist = artist;
    }
    
    [JsonInclude]
    public string Name { get; protected set; } = string.Empty;
    
    [JsonInclude]
    public int ReleaseYear { get; protected set; }
    
    [JsonInclude]
    public Artist Artist { get; protected set; } = null!;
}