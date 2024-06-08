

// ReSharper disable ClassNeverInstantiated.Global

namespace MusicApi.Clients;

public class MbReleaseQueryResult
{
    public DateTimeOffset Created { get; set; }
    public int Count { get; set; }
    public int Offset { get; set; }
    public MbRelease[]? Releases { get; set; }
}