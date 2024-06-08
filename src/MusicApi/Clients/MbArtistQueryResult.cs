
// ReSharper disable ClassNeverInstantiated.Global

namespace MusicApi.Clients;

public class MbArtistQueryResult
{
    public DateTimeOffset Created { get; set; }
    public int Count { get; set; }
    public int Offset { get; set; }
    public MbArtist[]? Artists { get; set; }
}