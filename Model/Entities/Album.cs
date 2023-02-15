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

    public string Name { get; set; } = string.Empty;

    public int ReleaseYear { get; set; }

    public Artist Artist { get; set; } = null!;
}