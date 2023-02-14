using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;
using Model.Utils;

namespace Model.Entities;

[NotMapped]
public class Root : Entity
{
    public Root(int id) : base(0)
    {
    }

    public Root(ModelContext modelContext) : base(modelContext)
    {
    }

    public async Task<Artist> CreateArtist(string name, DateOnly dateOfBirth)
    {
        var artist = new Artist(0, name, dateOfBirth);
        await this.ModelContext.DataMapper.Create(artist);
        return artist;
    }
}

