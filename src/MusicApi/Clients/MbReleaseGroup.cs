using System.Text.Json.Serialization;
// ReSharper disable ClassNeverInstantiated.Global

namespace MusicApi.Clients;

public class MbReleaseGroup
{
    public string? Title { get; set; }
    [JsonPropertyName("primary-type")] public string? PrimaryType { get; set; }
    [JsonPropertyName("secondary-types")] public string[]? SecondaryTypes { get; set; }
}