using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace Yuzaki.Game.Graphics.Components
{
    /// <summary>
    /// A menu that displays all the playlists at the left side of the screen.
    /// </summary>
    public partial class PlaylistMenu : CompositeDrawable
    {
        public PlaylistMenu()
        {
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.2f, 0.75f);
            Masking = true;
            CornerRadius = YuzakiStylingEnum.CORNER_RADIUS;
            AddInternal(new Box()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(1f),
                Alpha = 0.6f,
                Colour = YuzakiColour.PlaylistListBackgroundColour
            });
        }
    }
}
