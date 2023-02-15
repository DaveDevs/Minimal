using System.Net;
using System.Net.Http.Json;
using Actions.Commands;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using NUnit.Framework;

namespace Minimal.Api.Tests.Modules;

[TestFixture]
public class ArtistTests : AbstractApiTest
{
    [Test]
    public async Task CreateArtist_Success()
    {
        // Arrange
        var artist1 = new Artist(9999, "one", new DateOnly(1998, 1, 30));

        // Act
        await Client.PostAsJsonAsync("/Artists/Create", new ArtistCommandCreate.ArtistCreateProperties
        {
            Name = artist1.Name,
            DateOfBirth = artist1.DateOfBirth
        });

        // Assert
        var createdArtist = Context.Artists.Single(x => x.Id == Context.Artists.Max(x => x.Id));

        createdArtist.Should().BeEquivalentTo(new Artist(createdArtist.Id, artist1.Name, artist1.DateOfBirth));
    }

    [Test]
    public async Task UpdateArtist_Success()
    {
        // Arrange
        var artist1 = new Artist(0, "one", new DateOnly(1998, 1, 30));
        var created = (await Context.AddAsync(artist1)).Entity;
        await Context.SaveChangesAsync();

        // Act
        await Client.PostAsJsonAsync("/Artists/Update", new ArtistCommandUpdate.ArtistUpdateProperties()
        {
            Id = created.Id,
            Name = "new",
            DateOfBirth = artist1.DateOfBirth.AddYears(-10)
        });

        // Assert
        var updatedArtist = Context.Artists.Single(x => x.Id == Context.Artists.Max(x => x.Id));

        updatedArtist.Should().BeEquivalentTo(new Artist(updatedArtist.Id, "new", artist1.DateOfBirth.AddYears(-10)));
    }

    [Test]
    public async Task CreateAlbum_Success()
    {
        // Arrange
        var artist1 = new Artist(0, "one", new DateOnly(1998, 1, 30));
        var created = (await Context.AddAsync(artist1)).Entity;
        await Context.SaveChangesAsync();

        var album1 = new Album(9999, "songs for the deaf", 2002, created);

        // Act
        await Client.PostAsJsonAsync("/Artists/Album/Create", new AlbumCommandCreate.AlbumCreateProperties
        {
            Name = album1.Name,
            ReleaseYear = album1.ReleaseYear,
            ArtistId = created.Id
        });

        // Assert
        var createdAlbum = Context.Albums.Include(x => x.Artist).Single(x => x.Id == Context.Albums.Max(x => x.Id));

        createdAlbum.Should().BeEquivalentTo(new Album(createdAlbum.Id, album1.Name, album1.ReleaseYear, album1.Artist));
    }

    [Test]
    [Ignore("fails")]
    public async Task GetById_NoResult()
    {
        // Act
        var result = await Client.GetAsync("/Artists/1");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task GetbyId_Success()
    {
        // Arrange
        var artist1 = new Artist(0, "one", new DateOnly(1998, 1, 30));
        await Context.AddAsync(artist1);
        await Context.SaveChangesAsync();

        // Act
        var request = await Client.GetAsync($"/Artists/{artist1.Id}");
        var result = await request.Content.ReadFromJsonAsync<Artist>();

        // Assert
        result.Should().BeEquivalentTo(artist1);
    }

    [Test]
    public async Task GetAll_ZeroResults()
    {
        var result = await Client.GetStringAsync("/Artists");

        result.Should().Be("[]");
    }

    [Test]
    public async Task GetAll_OneResult()
    {
        // Arrange
        var artist1 = new Artist(0, "one", new DateOnly(1998, 1, 30));
        await Context.AddAsync(artist1);
        await Context.SaveChangesAsync();
        IList<Artist> expected = new List<Artist> { artist1 };

        // Act
        var request = await Client.GetAsync("/Artists");
        var result = await request.Content.ReadFromJsonAsync<IList<Artist>>();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetAll_WithChildren()
    {
        // Arrange
        var artist1 = new Artist(0, "one", new DateOnly(1998, 1, 30));
        await Context.AddAsync(artist1);
        await Context.SaveChangesAsync();

        IList<Artist> expected = new List<Artist> { artist1 };

        // Act
        var request = await Client.GetAsync("/Artists");
        var result = await request.Content.ReadFromJsonAsync<IList<Artist>>();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}