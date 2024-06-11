using System.Text.Json;
using System.Text.Json.Serialization;
using MusicApi.Models;

namespace MusicApi.Internal;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(Artist))]
internal partial class ApiJsonContext : JsonSerializerContext
{
    
}