using MusicApi.Clients;
using MusicApi.Models;

namespace MusicApi.Internal;

internal static class Mappings
{
    public static Artist[] ToModels(MbArtistQueryResult? result, bool includeReleases, int take = 10)
    {
        if (result?.Artists is not { Length: > 0 }) return [];

        var artists = result.Artists.OrderByDescending(a => a.Score)
            .Take(take);
        
        var list = new List<Artist>();
        foreach (var mbArtist in artists)
        {
            var artist = mbArtist.ToModel();
            
            // if (includeReleases)
            // {
            //     var releasesResult = await _musicBrainzClient.QueryReleasesByArtistId(mbArtist.Id);
            //     artist.Releases = ToModels(releasesResult).ToArray();
            // }

            list.Add(artist);
        }

        return list.ToArray();
    }
}