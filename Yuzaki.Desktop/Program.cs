using osu.Framework.Platform;
using osu.Framework;

namespace Yuzaki.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost(@"Yuzaki"))
            using (osu.Framework.Game game = new YuzakiGameDesktop())
                host.Run(game);
        }
    }
}
