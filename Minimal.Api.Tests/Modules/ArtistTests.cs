using System.Net.Http.Json;
using FluentAssertions;
using Model.Entities;
using NUnit.Framework;

namespace Minimal.Api.Tests.Modules;

[TestFixture]
public class ArtistTests : AbstractApiTest
{
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
}