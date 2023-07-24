using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace Yuzaki.Game.Graphics.Components
{
    /// <summary>
    /// Button with a circular background and an icon.
    /// </summary>
    public partial class CircleIconButton : Button
    {
        private IconUsage icon;

        public Colour4 BackgroundColour { get; set; } = YuzakiColour.CircleButtonBackgroundColour;
        public Colour4 IconColour { get; set; } = YuzakiColour.CircleButtonIconColour;

        public CircleIconButton(IconUsage icon)
        {
            this.icon = icon;

            Children = new Drawable[]
            {
                new Circle
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Colour = BackgroundColour,
                    RelativePositionAxes = Axes.Both,
                    Size = new Vector2(1f)
                },
                new SpriteIcon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Icon = icon,
                    Colour = IconColour,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.5f)
                }
            };
        }
    }
}
