using System.Text.Json;
using System.Text.Json.Serialization;
using MusicApi.Models;

namespace MusicApi.Clients;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(MbArtistQueryResult))]
[JsonSerializable(typeof(MbReleaseQueryResult))]
internal partial class MbSerializerContext : JsonSerializerContext
{
    
}

public static class Conversions
{
    public static Release ToModel(this MbRelease mbRelease)
    {
        var media = mbRelease.Media?.FirstOrDefault();
        var discCount = media?.DiscCount ?? 1;
        var trackCount = media?.TrackCount ?? 1;
        return new Release
        {
            Id = mbRelease.Id,
            Title = mbRelease.Title,
            Status = mbRelease.Status,
            Country = mbRelease.Country,
            Date = mbRelease.Date,
            DiscCount = discCount,
            TrackCount = trackCount,
            PrimaryType = mbRelease.ReleaseGroup?.PrimaryType,
            SecondaryTypes = mbRelease.ReleaseGroup?.SecondaryTypes,
        };
    }

    public static Artist ToModel(this MbArtist mbArtist)
    {
        return new Artist
        {
            Id = mbArtist.Id,
            Type = mbArtist.Type,
            Name = mbArtist.Name,
            Country = mbArtist.Country,
            Disambiguation = mbArtist.Disambiguation,
            Tags = mbArtist.Tags?.Select(t => t.Name).ToArray() ?? [],
        };
    }
}