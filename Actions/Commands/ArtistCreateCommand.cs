using System.Text.Json.Serialization;
using Model.Entities;
using Model.Utils.Json;

namespace Actions.Commands;

public class ArtistCreateCommand : Command<Artist, ArtistCreateCommand.Properties>
{
    public class Properties : UserRequestBase
    {
        public string Name { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DateOfBirth { get; set; }
    }

    public ArtistCreateCommand()
    {
        Props = new Properties();
    }

    public override Task Execute()
    {
        var artist = new Artist(0, Props.Name, Props.DateOfBirth);
        Context.Add(artist);
        return Context.SaveChangesAsync();
    }
}