using MusicApi.Clients;

namespace MusicApi.Tests;

public class MusicBrainzClientTests
{
    [Fact]
    public async Task FindsNirvana()
    {
        var httpClient = new HttpClient();
        MusicBrainzClient.ConfigureClient(httpClient);
        var target = new MusicBrainzClient(httpClient);
        var result = await target.QueryArtistAsync("Nirvana");
        Assert.NotNull(result);
        Assert.NotEmpty(result.Artists);
    }
}