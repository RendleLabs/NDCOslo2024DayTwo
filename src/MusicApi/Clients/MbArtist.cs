using System.Text.Json.Serialization;
// ReSharper disable ClassNeverInstantiated.Global

namespace MusicApi.Clients;

public class MbArtist
{
    public Guid Id { get; set; }
    public string? Type { get; set; }
    [JsonPropertyName("type-id")] public Guid TypeId { get; set; }
    public int Score { get; set; }
    public string? Name { get; set; }
    [JsonPropertyName("sort-name")] public string? SortName { get; set; }
    public string? Country { get; set; }
    public string? Disambiguation { get; set; }
    public MbArea? Area { get; set; }
    [JsonPropertyName("begin-area")] public MbArea? BeginArea { get; set; }
    [JsonPropertyName("life-span")] public MbLifeSpan? LifeSpan { get; set; }
    public MbTag[]? Tags { get; set; }
}