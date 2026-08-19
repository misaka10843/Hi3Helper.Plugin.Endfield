using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hi3Helper.Hypergryph.Yostar.Api;

internal sealed class YostarApiResponse<T>
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("msg")]
    public string? Message { get; set; }
}

internal sealed class YostarGameConfig
{
    [JsonPropertyName("game_lowest_version")]
    public string? LowestVersion { get; set; }

    [JsonPropertyName("game_latest_version")]
    public string? LatestVersion { get; set; }

    [JsonPropertyName("game_latest_file_path")]
    public string? LatestFilePath { get; set; }

    [JsonPropertyName("game_start_exe_name")]
    public string? StartExecutableName { get; set; }

    [JsonPropertyName("game_start_params")]
    public List<string>? StartParameters { get; set; }

    [JsonPropertyName("game_uninstall_script")]
    public string? UninstallScript { get; set; }

    [JsonPropertyName("decompression_size")]
    public string? DecompressionSize { get; set; }
}

internal sealed class YostarBaseConfig
{
    [JsonPropertyName("launcher_background_img")]
    public string? LauncherBackgroundImg { get; set; }
}

internal sealed class YostarOperationsResource
{
    [JsonPropertyName("operations_banner_list")]
    public List<YostarBanner>? Banners { get; set; }

    [JsonPropertyName("news_list")]
    public YostarNewsList? NewsList { get; set; }
}

internal sealed class YostarBanner
{
    [JsonPropertyName("banner_img")]
    public string? Image { get; set; }

    [JsonPropertyName("jump_url")]
    public string? JumpUrl { get; set; }
}

internal sealed class YostarNewsList
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("data")]
    public YostarNewsData? Data { get; set; }
}

internal sealed class YostarNewsData
{
    [JsonPropertyName("news")]
    public List<YostarNewsCategory>? News { get; set; }
}

internal sealed class YostarNewsCategory
{
    [JsonPropertyName("typeLabel")]
    public string? TypeLabel { get; set; }

    [JsonPropertyName("rows")]
    public List<YostarNewsItem>? Rows { get; set; }
}

internal sealed class YostarNewsItem
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("link")]
    public string? Link { get; set; }

    [JsonPropertyName("publishTime")]
    public long? PublishTime { get; set; }
}

internal sealed class YostarSocialMediaResource
{
    [JsonPropertyName("social_media_resource_list")]
    public List<YostarSocialMediaItem>? Items { get; set; }
}

internal sealed class YostarSocialMediaItem
{
    [JsonPropertyName("social_media_channel")]
    public string? Channel { get; set; }

    [JsonPropertyName("jump_url")]
    public string? JumpUrl { get; set; }

    [JsonPropertyName("qr_img")]
    public string? QrImage { get; set; }
}

internal sealed class YostarManifestUrl
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

internal sealed class YostarCdnConfig
{
    [JsonPropertyName("primary_cdn")]
    public string? PrimaryCdn { get; set; }

    [JsonPropertyName("back_up_cdn")]
    public string? BackupCdn { get; set; }
}

internal sealed class YostarRemoteManifest
{
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("file")]
    public List<YostarManifestFile> Files { get; set; } = [];
}

internal sealed class YostarManifestFile
{
    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;

    [JsonPropertyName("hash")]
    public string Hash { get; set; } = string.Empty;

    [JsonPropertyName("size")]
    public string Size { get; set; } = string.Empty;

    public long SizeValue => long.TryParse(Size, out long value) ? value : 0L;
}

internal sealed record YostarTargetPackage(
    YostarGameConfig Config,
    YostarRemoteManifest Manifest,
    YostarCdnConfig Cdn);
