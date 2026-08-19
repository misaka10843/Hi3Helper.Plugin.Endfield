using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices.Marshalling;
using System.Threading;
using System.Threading.Tasks;
using Hi3Helper.Hypergryph.Yostar;
using Hi3Helper.Hypergryph.Yostar.Management;
using Hi3Helper.Plugin.Core.Management;
using Hi3Helper.Plugin.Core.Management.Api;
using Hi3Helper.Plugin.Core.Management.PresetConfig;

namespace Hi3Helper.Plugin.Arknights.Management.PresetConfig;

[GeneratedComClass]
public partial class ArknightsKrPresetConfig : PluginPresetConfigBase
{
    private static readonly YostarLauncherOptions LauncherOptions = new(
        "Arknights_KR",
        "https://api-launcher-kr.yo-star.com",
        "Arknights_KR",
        "Arknights.exe");

    [field: AllowNull] [field: MaybeNull] public override string GameName => field ??= "Arknights";
    [field: AllowNull] [field: MaybeNull] public override string GameExecutableName => field ??= "Arknights.exe";

    public override string GameAppDataPath
    {
        get
        {
            string? gamePath = null;
            GameManager?.GetGamePath(out gamePath);
            return string.IsNullOrWhiteSpace(gamePath) ? string.Empty : Path.Combine(gamePath, "Arknights_Data");
        }
    }

    [field: AllowNull] [field: MaybeNull] public override string GameLogFileName => field ??= null!;
    [field: AllowNull] [field: MaybeNull] public override string GameVendorName => field ??= "Yostar";
    [field: AllowNull] [field: MaybeNull] public override string GameRegistryKeyName => field ??= "Arknights_KR";
    [field: AllowNull] [field: MaybeNull] public override string ProfileName => field ??= "ArknightsKr";
    [field: AllowNull] [field: MaybeNull] public override string ZoneDescription => field ??=
        "The Korean release of Arknights, published by Yostar.";
    [field: AllowNull] [field: MaybeNull] public override string ZoneName => field ??= "Korea";
    [field: AllowNull] [field: MaybeNull] public override string ZoneFullName => field ??= "Arknights (Korea)";
    [field: AllowNull] [field: MaybeNull] public override string ZoneLogoUrl => field ??= string.Empty;
    [field: AllowNull] [field: MaybeNull] public override string ZonePosterUrl => field ??= string.Empty;
    [field: AllowNull] [field: MaybeNull] public override string ZoneHomePageUrl => field ??= "https://arknights.kr/";
    public override GameReleaseChannel ReleaseChannel => GameReleaseChannel.Public;
    [field: AllowNull] [field: MaybeNull] public override string GameMainLanguage => field ??= "ko-KR";
    [field: AllowNull] [field: MaybeNull] public override string LauncherGameDirectoryName => field ??= "Arknights_KR";
    [field: AllowNull] [field: MaybeNull] public override List<string> SupportedLanguages => field ??= ["Korean"];
    public override ILauncherApiMedia? LauncherApiMedia
    {
        get => field ??= new Hi3Helper.Hypergryph.Yostar.Api.YostarLauncherApiMedia(LauncherOptions);
        set;
    }
    public override ILauncherApiNews? LauncherApiNews
    {
        get => field ??= new Hi3Helper.Hypergryph.Yostar.Api.YostarLauncherApiNews(LauncherOptions);
        set;
    }

    public override IGameManager? GameManager
    {
        get => field ??= new YostarGameManager(LauncherOptions);
        set;
    }

    public override IGameInstaller? GameInstaller
    {
        get => field ??= new YostarGameInstaller(GameManager);
        set;
    }

    protected override Task<int> InitAsync(CancellationToken token) => Task.FromResult(0);
}
