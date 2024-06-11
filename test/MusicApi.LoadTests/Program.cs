using NBomber.CSharp;

var client = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7042/")
};

string[] artists = ["Muse", "Nirvana", "Tool", "U2"];

var controllerScenario = Scenario.Create("controller", async context =>
    {
        var i = Random.Shared.Next(4);
        var artist = artists[i];
        var response = await client.GetAsync($"/artists/query?name={artist}");
        return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
    })
    .WithWarmUpDuration(TimeSpan.FromSeconds(10))
    .WithLoadSimulations(
        Simulation.Inject(rate: 1,
            interval: TimeSpan.FromSeconds(1),
            during: TimeSpan.FromSeconds(30))
    );

var minApiScenario = Scenario.Create("minapi", async context =>
    {
        var i = Random.Shared.Next(4);
        var artist = artists[i];
        var response = await client.GetAsync($"/min/artists/query?name={artist}");
        return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
    })
    .WithWarmUpDuration(TimeSpan.FromSeconds(10))
    .WithLoadSimulations(
        Simulation.Inject(rate: 1,
            interval: TimeSpan.FromSeconds(1),
            during: TimeSpan.FromSeconds(30))
    );

NBomberRunner.RegisterScenarios(controllerScenario, minApiScenario).Run();