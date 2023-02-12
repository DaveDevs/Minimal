using Model.Utils;

namespace Model.Entities;

public class Album : Entity
{
    public Album(ModelDataContext modelDataContext) : base(modelDataContext)
    {
    }

    public Album(int id, string name, int releaseYear, Artist artist) : base(id)
    {
        Name = name;
        ReleaseYear = releaseYear;
        Artist = artist;
    }

    public string Name { get; set; }

    public int ReleaseYear { get; set; }

    public Artist Artist { get; set; }
}