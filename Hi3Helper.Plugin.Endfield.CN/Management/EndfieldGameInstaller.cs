using System.Runtime.InteropServices.Marshalling;
using System.Threading;
using System.Threading.Tasks;
using Hi3Helper.Plugin.Core.Management;

namespace Hi3Helper.Plugin.Endfield.CN.Management;

[GeneratedComClass]
internal partial class EndfieldGameInstaller : GameInstallerBase
{
    public EndfieldGameInstaller(IGameManager gameManager) : base(gameManager)
    {
    }

    protected override Task<long> GetGameSizeAsyncInner(GameInstallerKind kind, CancellationToken token)
    {
        // 后续根据 pkg.packs 的总大小计算
        return Task.FromResult(0L);
    }

    // 获取已下载大小（字节）
    protected override Task<long> GetGameDownloadedSizeAsyncInner(GameInstallerKind kind, CancellationToken token)
    {
        return Task.FromResult(0L);
    }

    // 开始安装
    protected override Task StartInstallAsyncInner(InstallProgressDelegate? progressDelegate,
        InstallProgressStateDelegate? stateDelegate, CancellationToken token)
    {
        // 后续实现下载 pkg.packs 并解压的逻辑
        return Task.CompletedTask;
    }

    // 开始更新
    protected override Task StartUpdateAsyncInner(InstallProgressDelegate? progressDelegate,
        InstallProgressStateDelegate? stateDelegate, CancellationToken token)
    {
        return Task.CompletedTask;
    }

    // 开始预下载
    protected override Task StartPreloadAsyncInner(InstallProgressDelegate? progressDelegate,
        InstallProgressStateDelegate? stateDelegate, CancellationToken token)
    {
        return Task.CompletedTask;
    }

    // 卸载
    protected override Task UninstallAsyncInner(CancellationToken token)
    {
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
    }
}