using Microsoft.AspNetCore.Mvc;
using MusicApi.Clients;
using MusicApi.Models;

namespace MusicApi.Controllers;

[Route("artists/{artistId:guid}/releases")]
public class ArtistReleaseController : ControllerBase
{
    private static readonly Release[] EmptyReleases = [];

    private readonly MusicBrainzClient _musicBrainzClient;

    public ArtistReleaseController(MusicBrainzClient musicBrainzClient)
    {
        _musicBrainzClient = musicBrainzClient;
    }

    [HttpGet]
    public async Task<ActionResult<Release[]>> Get(Guid artistId, [FromQuery] int skip = 0, [FromQuery] int take = 100)
    {
        var list = new List<Release>();
        
        var result = await _musicBrainzClient.QueryReleasesByArtistId(artistId, skip);
        if (result?.Releases is not { Length: > 0 }) return Ok(EmptyReleases);
        
        take = Math.Min(take, result.Count);
        
        while (result?.Releases is { Length: > 0 })
        {
            list.AddRange(result.Releases.Select(r => r.ToModel()));
            skip += result.Releases.Length;
            if (skip > take) break;
            result = await _musicBrainzClient.QueryReleasesByArtistId(artistId, skip);
        }

        return Ok(list.ToArray());
    }
}