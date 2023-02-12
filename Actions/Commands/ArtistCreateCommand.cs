using System.Text.Json.Serialization;
using Model.Entities;
using Model.Utils.Json;

namespace Actions.Commands;

public class ArtistCreateCommand : Command<Artist>
{
    public ArtistCreateCommand()
    {
        Props = new Properties();
    }

    public Properties Props { get; set; }

    public override Task Execute()
    {
        var artist = new Artist(0, Props.Name, Props.DateOfBirth);
        Context.Add(artist);
        return Context.SaveChangesAsync();
    }

    public class Properties
    {
        public string Name { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DateOfBirth { get; set; }
    }
}