using System.Text.Json.Serialization;

namespace Hi3Helper.Hypergryph.Yostar.Api;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(YostarApiResponse<YostarGameConfig>))]
[JsonSerializable(typeof(YostarApiResponse<YostarManifestUrl>))]
[JsonSerializable(typeof(YostarApiResponse<YostarCdnConfig>))]
[JsonSerializable(typeof(YostarRemoteManifest))]
internal partial class YostarJsonContext : JsonSerializerContext;
