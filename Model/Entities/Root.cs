using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities;

[NotMapped]
public class Root : Entity
{
    public Root() : base(0)
    {
    }

    public async Task<Artist> CreateArtist(string name, DateOnly dateOfBirth)
    {
        var artist = new Artist(0, name, dateOfBirth);
        artist.SetModelContext(ModelContext);

        await artist.DoCreate();
        return artist;
    }
}