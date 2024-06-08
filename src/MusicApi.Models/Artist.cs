namespace MusicApi.Models;

public class Artist
{
    public Guid Id { get; set; }
    public string? Type { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? Disambiguation { get; set; }
    public string[]? Tags { get; set; }
    public Release[]? Releases { get; set; }
}