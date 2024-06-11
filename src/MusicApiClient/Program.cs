
using System.Net.Http.Json;
using MusicApi.Models;

var client = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7042/"),
};

await foreach (var artist in client.GetFromJsonAsAsyncEnumerable<Artist>("/min/artists/query?name=Nirvana"))
{
    Console.WriteLine(artist!.Name);
}