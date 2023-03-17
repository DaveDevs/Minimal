using Actions;
using Actions.Commands;
using Model.Utils;

namespace Minimal.Cli.Services;

public class Populator
{
    public Populator(CommandFactory commandFactory, MinimalDbContext dataContext)
    {
        CommandFactory = commandFactory;
        DataContext = dataContext;
    }

    public CommandFactory CommandFactory { get; protected set; }

    public MinimalDbContext DataContext { get; protected set; }

    public async Task Go()
    {
        await this.DataContext.Database.EnsureDeletedAsync();
        await this.DataContext.Database.EnsureCreatedAsync();
        var command = CommandFactory.Create<ArtistCommandCreate>();
        command.Props.Name = "Dave";
        command.Props.DateOfBirth = new DateOnly(1980, 11, 20);
        await command.Execute();
    }
}