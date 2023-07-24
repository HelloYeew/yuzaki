using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace Yuzaki.Game.Graphics.Components
{
    /// <summary>
    /// Button with an icon.
    /// </summary>
    public partial class IconButton : Button
    {
        private IconUsage icon;

        public Colour4 IconColour { get; set; } = YuzakiColour.IconButtonIconColour;

        public IconButton(IconUsage icon)
        {
            this.icon = icon;

            Children = new Drawable[]
            {
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
