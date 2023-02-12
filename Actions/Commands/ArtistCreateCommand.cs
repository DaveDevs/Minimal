using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Model.Entities;
using Model.Utils.Json;

namespace Actions.Commands
{
    public class ArtistCreateCommand : Command<Artist>
    {
        public class Properties
        {
            public string Name { get; set; }

            [JsonConverter(typeof(DateOnlyJsonConverter))]
            public DateOnly DateOfBirth { get; set; }
        }

        public ArtistCreateCommand()
        {
            Props = new Properties();
        }

        public Properties Props { get; set; }

        public override Task Execute()
        {
            var artist = new Artist(0, this.Props.Name, this.Props.DateOfBirth);
            this.Context.Add(artist);
            return this.Context.SaveChangesAsync();
        }
    }
}
