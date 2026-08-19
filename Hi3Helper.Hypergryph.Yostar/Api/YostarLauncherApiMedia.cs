using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.Marshalling;
using System.Threading;
using System.Threading.Tasks;
using Hi3Helper.Plugin.Core;
using Hi3Helper.Plugin.Core.Management.Api;
using Hi3Helper.Plugin.Core.Utility;

namespace Hi3Helper.Hypergryph.Yostar.Api;

/// <summary>
/// Implements ILauncherApiMedia on top of Yostar's launcher API.
/// The background image comes from the signed <c>GET /api/launcher/base/config</c>
/// endpoint (<c>launcher_background_img</c>, a plain image URL with no video variant).
/// </summary>
[GeneratedComClass]
public partial class YostarLauncherApiMedia : LauncherApiMediaBase
{
    private readonly YostarLauncherOptions _options;

    private YostarApiClient? _apiClient;
    private string? _backgroundUrl;

    public YostarLauncherApiMedia(YostarLauncherOptions options) => _options = options;

    [field: AllowNull]
    [field: MaybeNull]
    protected override HttpClient ApiResponseHttpClient { get; set; } = new PluginHttpClientBuilder()
        .SetAllowedDecompression(DecompressionMethods.None)
        .AllowCookies()
        .AllowRedirections()
        .Create();

    protected override string? ApiResponseBaseUrl => _options.ApiBaseUri.ToString();

    protected override async Task<int> InitAsync(CancellationToken token)
    {
        try
        {
            _apiClient = new YostarApiClient(_options);
            YostarBaseConfig config = await _apiClient.GetBaseConfigAsync(token).ConfigureAwait(false);
            _backgroundUrl = config.LauncherBackgroundImg;
            SharedStatic.InstanceLogger.LogDebug($"[YostarMedia] Background image: {_backgroundUrl}");
            return 0;
        }
        catch (Exception ex)
        {
            SharedStatic.InstanceLogger.LogError($"[YostarMedia] Failed to init media: {ex}");
            return -1;
        }
    }

    public override void GetBackgroundEntries(out nint handle, out int count, out bool isDisposable,
        out bool isAllocated)
    {
        if (string.IsNullOrEmpty(_backgroundUrl))
        {
            handle = nint.Zero;
            count = 0;
            isDisposable = false;
            isAllocated = false;
            return;
        }

        var memory = PluginDisposableMemory<LauncherPathEntry>.Alloc();
        ref var entry = ref memory[0];
        entry.Write(_backgroundUrl, Span<byte>.Empty);

        handle = memory.AsSafePointer();
        count = 1;
        isDisposable = true;
        isAllocated = true;
    }

    public override void GetBackgroundFlag(out LauncherBackgroundFlag result) =>
        result = LauncherBackgroundFlag.TypeIsImage;

    public override void GetLogoFlag(out LauncherBackgroundFlag result) => result = LauncherBackgroundFlag.None;

    public override void GetLogoOverlayEntries(out nint handle, out int count, out bool isDisposable,
        out bool isAllocated)
    {
        handle = nint.Zero;
        count = 0;
        isDisposable = false;
        isAllocated = false;
    }

    public override void Dispose()
    {
        if (IsDisposed) return;
        _apiClient?.Dispose();
        ApiResponseHttpClient?.Dispose();
        base.Dispose();
    }
}
