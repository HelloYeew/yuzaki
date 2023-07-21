using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace Yuzaki.Game.Graphics.Components
{
    public partial class MusicPlayer : CompositeDrawable
    {
        [BackgroundDependencyLoader]
        private void load(TextureStore textureStore)
        {
            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.X,
                    Width = 1,
                    Height = 100,
                    Masking = true,
                    CornerRadius = 10,
                    Margin = new MarginPadding(10),
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Colour = YuzakiColour.MusicPlayerBackgroundColour,
                            Alpha = 0.8f
                        },
                        new GridContainer()
                        {
                            RelativeSizeAxes = Axes.Both,
                            Content = new[]
                            {
                                new Drawable[]
                                {
                                    new Sprite()
                                    {
                                        Anchor = Anchor.CentreLeft,
                                        Origin = Anchor.CentreLeft,
                                        Texture = textureStore.Get("default_album.jpg"),
                                        Size = new(100),
                                    },
                                    new FillFlowContainer()
                                    {
                                        Anchor = Anchor.CentreLeft,
                                        Origin = Anchor.CentreLeft,
                                        RelativeSizeAxes = Axes.Both,
                                        Direction = FillDirection.Vertical,
                                        Spacing = new(5),
                                        Children = new Drawable[]
                                        {
                                            new YuzakiSpriteText()
                                            {
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                Text = "Song Name",
                                                Font = YuzakiFont.GetFont(size: 30f, weight: YuzakiFont.FontWeight.Bold),
                                            },
                                            new YuzakiSpriteText()
                                            {
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                Text = "Artist Name",
                                                Font = new FontUsage(size: 20),
                                            },
                                        }
                                    },
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
