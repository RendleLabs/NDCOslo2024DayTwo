using System.Text.Json.Serialization;
// ReSharper disable ClassNeverInstantiated.Global

namespace MusicApi.Clients;

public class MbArea
{
    public Guid Id { get; set; }
    public string? Type { get; set; }
    [JsonPropertyName("type-id")] public Guid TypeId { get; set; }
    public string? Name { get; set; }
    [JsonPropertyName("sort-name")] public string? SortName { get; set; }
    [JsonPropertyName("life-span")] public MbLifeSpan? LifeSpan { get; set; }
}