using osu.Framework.Development;
using osu.Framework.Platform;
using Yuzaki.Game;

namespace Yuzaki.Desktop;

public partial class YuzakiGameDesktop : YuzakiGame
{
    public override void SetHost(GameHost host)
    {
        base.SetHost(host);
        var desktopWindow = host.Window;

        desktopWindow.Title = "Yuzaki";
        if (DebugUtils.IsDebugBuild)
            desktopWindow.Title += " development";
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        LoadComponentAsync(new DiscordRichPresence(), Add);
    }
}
