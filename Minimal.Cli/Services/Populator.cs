using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actions;
using Actions.Commands;
using Model.Utils;

namespace Minimal.Cli.Services
{
    public class Populator
    {
        public CommandFactory CommandFactory { get; set; }

        public ModelDataContext DataContext { get; set; }

        public Populator(CommandFactory commandFactory, ModelDataContext dataContext)
        {
            CommandFactory = commandFactory;
            DataContext = dataContext;
        }

        public void Go()
        {
            var command = this.CommandFactory.Create<ArtistCreateCommand>();
            command.Name = "Dave";
            command.DateOfBirth = new DateOnly(1980, 11, 20);
            command.Execute();
        }
    }
}
