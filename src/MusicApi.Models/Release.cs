namespace MusicApi.Models;

public class Release
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Status { get; set; }
    public string? Date { get; set; }
    public string? Country { get; set; }
    public int DiscCount { get; set; }
    public int TrackCount { get; set; }
    public string? PrimaryType { get; set; }
    public string[]? SecondaryTypes { get; set; }
}