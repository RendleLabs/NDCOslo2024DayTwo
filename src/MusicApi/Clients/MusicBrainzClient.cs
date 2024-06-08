using System.Net.Http.Headers;

namespace MusicApi.Clients;

public class MusicBrainzClient
{
    private readonly HttpClient _httpClient;

    public MusicBrainzClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MbArtistQueryResult?> QueryArtistAsync(string name)
    {
        name = Uri.EscapeDataString(name);
        var url = $"/ws/2/artist/?query=artist:{name}";
        var result = await _httpClient.GetFromJsonAsync<MbArtistQueryResult>(url);
        return result;
    }

    public async Task<MbReleaseQueryResult?> QueryReleasesByArtistId(Guid artistId, int offset = 0)
    {
        var url = $"/ws/2/release/?query=arid:{artistId:D}";
        if (offset > 0)
        {
            url = $"{url}&offset={offset}";
        }
        var result = await _httpClient.GetFromJsonAsync<MbReleaseQueryResult>(url);
        return result;
    }

    public static void ConfigureClient(HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri("http://musicbrainz.org");
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "NdcMusicApi/1.0.0 ( https://www.rendlelabs.com )");
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
}