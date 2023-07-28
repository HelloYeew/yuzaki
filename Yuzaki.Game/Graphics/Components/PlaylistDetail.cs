using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace Yuzaki.Game.Graphics.Components
{
    public partial class PlaylistDetail : CompositeDrawable
    {
        public PlaylistDetail()
        {
            Anchor = Anchor.TopRight;
            Origin = Anchor.TopRight;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.765f, 0.75f);
            Masking = true;
            CornerRadius = YuzakiStylingEnum.CORNER_RADIUS;
            AddInternal(new Box()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(1f),
                Alpha = 0.6f,
                Colour = YuzakiColour.MusicListBackgroundColour
            });
        }
    }
}
