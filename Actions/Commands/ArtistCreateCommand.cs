using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;

namespace Actions.Commands
{
    public class ArtistCreateCommand : Command<Artist>
    {
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public override void Execute()
        {
            var artist = new Artist(0, this.Name, this.DateOfBirth);
            this.Context.Add(artist);
            this.Context.SaveChangesAsync();
        }
    }
}
