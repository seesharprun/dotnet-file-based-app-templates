#:sdk Microsoft.NET.Sdk.Web

#:package Microsoft.AspNetCore.OpenApi@10.*

#:property IsAotCompatible=true
#:property EnableRequestDelegateGenerator=true

using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddOpenApi();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(
        default, 
        DefaultJsonContext.Default
    );
});
builder.Services.AddSingleton<IDataService, DataService>();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapOpenApi();

app.MapGet("/", () => "Hello, World!")
    .WithName("Root");

app.MapGet("/data", (IDataService dataService, CancellationToken cancellationToken) =>
    dataService.GetSamplesAsync(cancellationToken))
    .WithName("Data");

await app.RunAsync();

interface IDataService
{
    Task<Item[]> GetSamplesAsync(CancellationToken cancellationToken);
}

record Item(int Id);

class DataService : IDataService
{
    public async Task<Item[]> GetSamplesAsync(CancellationToken cancellationToken) =>
        [.. Enumerable.Range(1, 5).Select(id => new Item(id))];
}

[JsonSerializable(typeof(Item[]))]
internal partial class DefaultJsonContext : JsonSerializerContext;
