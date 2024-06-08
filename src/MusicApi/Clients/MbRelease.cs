using System.Text.Json.Serialization;
// ReSharper disable ClassNeverInstantiated.Global

namespace MusicApi.Clients;

public class MbRelease
{
    public Guid Id { get; set; }
    public int Score { get; set; }
    public string? Title { get; set; }
    public string? Status { get; set; }
    public string? Disambiguation { get; set; }
    public string? Packaging { get; set; }
    public string? Date { get; set; }
    public string? Country { get; set; }
    [JsonPropertyName("release-group")] public MbReleaseGroup? ReleaseGroup { get; set; }

    public MbMedia[]? Media { get; set; }
    public MbTag[]? Tags { get; set; }
}