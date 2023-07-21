using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace Yuzaki.Game.Graphics
{
    public partial class YuzakiScreenStack : ScreenStack
    {
        public YuzakiScreenStack()
        {
            InternalChildren = new Drawable[]
            {
                new YuzakiBackground
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                }
            };
        }
    }
}
