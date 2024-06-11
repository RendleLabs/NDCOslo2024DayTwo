using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Clients;
using MusicApi.Internal;
using MusicApi.Models;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

var resourceBuilder = ResourceBuilder.CreateEmpty()
    .AddService("music_api", "workshop", "1.0.0")
    .AddTelemetrySdk();

if (!builder.Environment.IsDevelopment())
{
    builder.Logging.ClearProviders();
}

builder.Logging.AddOpenTelemetry(o =>
{
    o.IncludeScopes = true;
    o.SetResourceBuilder(resourceBuilder);
    o.AddOtlpExporter();
});

builder.Services.AddOpenTelemetry()
    .WithTracing(b =>
    {
        b.SetResourceBuilder(resourceBuilder)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddSource("music_api")
            .AddOtlpExporter();
    })
    .WithMetrics(b =>
    {
        b.SetResourceBuilder(resourceBuilder)
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddMeter("music_api")
            .AddOtlpExporter();
    });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.TypeInfoResolverChain.Insert(0, ApiJsonContext.Default);
    });

builder.Services.ConfigureHttpJsonOptions(o =>
{
    o.SerializerOptions.TypeInfoResolverChain.Insert(0, ApiJsonContext.Default);
});

builder.Services.AddHttpClient<MusicBrainzClient>()
    .ConfigureHttpClient(MusicBrainzClient.ConfigureClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/min/artists/query",
    async ([FromQuery] string name, [FromServices] MusicBrainzClient mbClient) =>
    {
        if (name is not { Length: > 0 }) return Results.BadRequest();
        var result = await mbClient.QueryArtistAsync(name);
        if (result?.Artists is not { Length: > 0 }) return Results.Ok(Array.Empty<Artist>());
        var artists = Mappings.ToModels(result, false, 10);
        return Results.Ok(artists);
    })
    .CacheOutput(policy =>
    {
        policy.SetVaryByQuery("name", "skip", "include");
    });

app.MapControllers();

app.Run();