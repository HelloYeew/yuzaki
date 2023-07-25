using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osuTK;

namespace Yuzaki.Game.Graphics.Components
{
    public partial class ProfilePictureMenu : CompositeDrawable
    {
        public static readonly int ICON_SIZE = 57;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            OnlineStore onlineStore = new OnlineStore();

            Sprite textureSprite;

            Padding = new MarginPadding()
            {
                Top = 10,
                Right = 10
            };

            InternalChildren = new Drawable[]
            {
                new Circle
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(ICON_SIZE),
                    Colour = YuzakiColour.ProfilePictureBackgroundColour
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(ICON_SIZE),
                    Masking = true,
                    CornerRadius = 28,
                    Children = new Drawable[]
                    {
                        textureSprite = new Sprite
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1f)
                        }
                    }
                }
            };

            Scheduler.Add(() =>
            {
                Stream stream = onlineStore.GetStream("https://a.ppy.sh/18735426");
                textureSprite.Texture = Texture.FromStream(host.Renderer, stream);
            });
        }
    }
}
