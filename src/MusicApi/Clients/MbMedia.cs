using System.Text.Json.Serialization;
// ReSharper disable ClassNeverInstantiated.Global

namespace MusicApi.Clients;

public class MbMedia
{
    public string? Format { get; set; }
    [JsonPropertyName("disc-count")] public int DiscCount { get; set; }
    [JsonPropertyName("track-count")] public int TrackCount { get; set; }
}