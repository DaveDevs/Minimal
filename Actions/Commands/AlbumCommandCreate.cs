using Actions.Utils;
using FluentValidation;
using Model.Entities;

namespace Actions.Commands;

public class AlbumCommandCreate : Command<Artist, AlbumCommandCreate.AlbumCreateProperties>
{
    public AlbumCommandCreate()
    {
        Props = new AlbumCreateProperties();
    }

    public override int TargetId => Props.ArtistId;

    protected override async Task InvokeLogic()
    {
        await Target.CreateAlbum(Props.Name, Props.ReleaseYear);
    }

    public class AlbumCreateProperties : RequestBase
    {
        public string Name { get; set; } = string.Empty;

        public int ReleaseYear { get; set; }

        public int ArtistId { get; set; }

        public override IValidator NewValidator()
        {
            return new AlbumCreateValidator();
        }

        public class AlbumCreateValidator : AbstractValidator<AlbumCreateProperties>
        {
            public AlbumCreateValidator()
            {
                RuleFor(x => x.ArtistId).IsAnId();
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.ReleaseYear)
                    .LessThanOrEqualTo(DateTime.Today.Year);
            }
        }
    }
}