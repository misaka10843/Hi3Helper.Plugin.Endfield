using System.Text.Json.Serialization;

namespace Hi3Helper.Plugin.Endfield.Management.Api;

[JsonSourceGenerationOptions(
    WriteIndented = false,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(EndfieldBatchRequest))]
[JsonSerializable(typeof(EndfieldBatchResponse))]
public partial class EndfieldApiContext : JsonSerializerContext
{
}