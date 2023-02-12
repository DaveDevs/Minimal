﻿using System.Text.Json.Serialization;
using Actions.Utils;
using FluentValidation;
using Model.Utils.Json;

namespace Actions.Commands;

public class ArtistCommandCreate : RootCommand<ArtistCommandCreate.ArtistCreateProperties>
{
    public ArtistCommandCreate()
    {
        Props = new ArtistCreateProperties();
    }

    protected override async Task InvokeLogic()
    {
        await Target.CreateArtist(Props.Name, Props.DateOfBirth);
    }

    public class ArtistCreateProperties : UserRequestBase
    {
        public string Name { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DateOfBirth { get; set; }

        public override IValidator NewValidator()
        {
            return new ArtistCreateValidator();
        }

        public class ArtistCreateValidator : AbstractValidator<ArtistCreateProperties>
        {
            public ArtistCreateValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.DateOfBirth).MustBeBefore(DateTime.Today);
            }
        }
    }
}