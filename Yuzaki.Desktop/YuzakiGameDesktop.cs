using Yuzaki.Game;

namespace Yuzaki.Desktop;

public partial class YuzakiGameDesktop : YuzakiGame
{
    protected override void LoadComplete()
    {
        base.LoadComplete();

        LoadComponentAsync(new DiscordRichPresence(), Add);
    }
}
