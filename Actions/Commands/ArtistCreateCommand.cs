﻿using System.Text.Json.Serialization;
using Actions.Utils;
using FluentValidation;
using Model.Entities;
using Model.Utils.Json;

namespace Actions.Commands;

public class ArtistCreateCommand : Command<Artist, ArtistCreateCommand.ArtistCreateProperties>
{
    public ArtistCreateCommand()
    {
        Props = new ArtistCreateProperties();
    }

    protected override Task InvokeLogic()
    {
        var artist = new Artist(0, Props.Name, Props.DateOfBirth);
        Context.Add(artist);
        return Context.SaveChangesAsync();
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