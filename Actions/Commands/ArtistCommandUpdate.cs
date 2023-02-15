using System.Text.Json.Serialization;
using Actions.Utils;
using FluentValidation;
using Model.Entities;
using Model.Utils.Json;

namespace Actions.Commands;

public class ArtistCommandUpdate : Command<Artist, ArtistCommandUpdate.ArtistUpdateProperties>
{
    public override int TargetId => Props.Id;

    public ArtistCommandUpdate()
    {
        Props = new ArtistUpdateProperties();
    }

    protected override async Task InvokeLogic()
    {
        await Target.Update(Props.Name, Props.DateOfBirth);
    }

    public class ArtistUpdateProperties : RequestBase
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DateOfBirth { get; set; }

        public override IValidator NewValidator()
        {
            return new ArtistUpdateValidator();
        }

        public class ArtistUpdateValidator : AbstractValidator<ArtistUpdateProperties>
        {
            public ArtistUpdateValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.DateOfBirth).MustBeBefore(DateTime.Today);
            }
        }
    }
}