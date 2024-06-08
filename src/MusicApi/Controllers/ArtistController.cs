using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MusicApi.Clients;
using MusicApi.Models;

namespace MusicApi.Controllers;

[Route("artists")]
public class ArtistController : ControllerBase
{
    private readonly MusicBrainzClient _musicBrainzClient;

    public ArtistController(MusicBrainzClient musicBrainzClient)
    {
        _musicBrainzClient = musicBrainzClient;
    }

    [OutputCache(VaryByQueryKeys = ["name", "skip", "include"])]
    [HttpGet("query")]
    public async Task<ActionResult<Artist[]>> Query([FromQuery]string? name, [FromQuery]int? skip, [FromQuery]string? include)
    {
        include ??= string.Empty;
        if (name is not { Length: > 0 }) return BadRequest();
        var result = await _musicBrainzClient.QueryArtistAsync(name);
        if (result?.Artists is not {Length: > 0}) return Ok(Array.Empty<Artist>());
        var artists = await ToModels(result, include.Contains("releases", StringComparison.OrdinalIgnoreCase));
        return Ok(artists);
    }

    private async Task<Artist[]> ToModels(MbArtistQueryResult? result, bool includeReleases, int take = 10)
    {
        if (result?.Artists is not { Length: > 0 }) return [];

        var artists = result.Artists.OrderByDescending(a => a.Score)
            .Take(take);
        
        var list = new List<Artist>();
        foreach (var mbArtist in artists)
        {
            var artist = mbArtist.ToModel();
            
            if (includeReleases)
            {
                var releasesResult = await _musicBrainzClient.QueryReleasesByArtistId(mbArtist.Id);
                artist.Releases = ToModels(releasesResult).ToArray();
            }

            list.Add(artist);
        }

        return list.ToArray();
    }

    private IEnumerable<Release> ToModels(MbReleaseQueryResult? result, int take = 10)
    {
        if (result?.Releases is not { Length: > 0 }) return [];
        
        return result.Releases
            .OrderByDescending(r => r.Score)
            .Take(take)
            .Select(Conversions.ToModel);
    }
}