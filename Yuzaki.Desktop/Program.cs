using osu.Framework.Platform;
using osu.Framework;
using Yuzaki.Game;

namespace Yuzaki.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost(@"Yuzaki"))
            using (osu.Framework.Game game = new YuzakiGame())
                host.Run(game);
        }
    }
}
