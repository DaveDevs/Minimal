using System.Net.Http.Json;
using System.Text;
using Actions.Commands;
using FluentAssertions;
using Model.Entities;
using Newtonsoft.Json;
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
        var createdArtist = this.Context.Artists.Single(x => x.Id == this.Context.Artists.Max(x => x.Id));

        artist1.Id = createdArtist.Id;
        createdArtist.Should().BeEquivalentTo(artist1);
    }

    [Test]
    public async Task CreateAlbum_Success()
    {
        // Arrange
        var artist1 = new Artist(0, "one", new DateOnly(1998, 1, 30));
        var created = (await this.Context.AddAsync(artist1)).Entity;
        await this.Context.SaveChangesAsync();

        var album1 = new Album(9999, "songs for the deaf", 2002, created);

        // Act
        await Client.PostAsJsonAsync("/Artists/Album/Create", new AlbumCommandCreate.AlbumCreateProperties()
        {
            Name = album1.Name,
            ReleaseYear = album1.ReleaseYear,
            ArtistId = created.Id
        });

        // Assert
        var createdAlbum = this.Context.Albums.Single(x => x.Id == this.Context.Albums.Max(x => x.Id));

        album1.Id = createdAlbum.Id;
        createdAlbum.Should().BeEquivalentTo(album1);
    }

    [Test]
    [Ignore("throws - revisit")]
    public async Task GetbyId_NoResult()
    {
        var result = await Client.GetStringAsync("/Artists/1");

        result.Should().Be("[]");
    }

    [Test]
    public async Task GetbyId_Success()
    {
        // Arrange
        var artist1 = new Artist(0, "one", new DateOnly(1998, 1, 30));
        await this.Context.AddAsync(artist1);
        await this.Context.SaveChangesAsync();

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
        await this.Context.AddAsync(artist1);
        await this.Context.SaveChangesAsync();
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
        await this.Context.AddAsync(artist1);
        await this.Context.SaveChangesAsync();

        IList<Artist> expected = new List<Artist> { artist1 };

        // Act
        var request = await Client.GetAsync("/Artists");
        var result = await request.Content.ReadFromJsonAsync<IList<Artist>>();

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}