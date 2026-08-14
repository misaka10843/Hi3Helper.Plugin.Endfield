using System;

namespace Hi3Helper.Hypergryph.Yostar;

public sealed record YostarLauncherOptions(
    string GameTag,
    string ApiBaseUrl,
    string GameDirectoryName,
    string DefaultExecutableName,
    string LauncherVersion = "1.8.1",
    string AuthorizationSalt = "DE7108E9B2842FD460F4777702727869")
{
    public Uri ApiBaseUri { get; } = new(ApiBaseUrl.TrimEnd('/') + '/', UriKind.Absolute);
}
