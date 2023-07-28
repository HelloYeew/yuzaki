using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace Yuzaki.Game.Graphics.Components;

public partial class PlaylistInfo : CompositeDrawable
{
    [BackgroundDependencyLoader]
    private void load(TextureStore textureStore)
    {
        InternalChild = new FillFlowContainer()
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.X,
            AutoSizeAxes = Axes.Y,
            Direction = FillDirection.Vertical,
            Spacing = new Vector2(0, 28),
            Children = new Drawable[]
            {
                new Container()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.X,
                    Size = new Vector2(1, 220),
                    Name = "PlaylistCoverContainer",
                    Children = new Drawable[]
                    {
                        new FillFlowContainer()
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Direction = FillDirection.Horizontal,
                            Spacing = new Vector2(8),
                            Children = new Drawable[]
                            {
                                // playlist cover
                                new Sprite()
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Size = new Vector2(220, 220),
                                    Texture = textureStore.Get("default_playlist_cover.png")
                                },
                                new FillFlowContainer()
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Direction = FillDirection.Vertical,
                                    Spacing = new Vector2(8),
                                    Children = new Drawable[]
                                    {
                                        new YuzakiSpriteText()
                                        {
                                            Anchor = Anchor.CentreLeft,
                                            Origin = Anchor.CentreLeft,
                                            Text = "Playlist",
                                            Font = YuzakiFont.GetFont(YuzakiFont.Typeface.Saira, 16, YuzakiFont.FontWeight.Light)
                                        },
                                        new YuzakiSpriteText()
                                        {
                                            Anchor = Anchor.CentreLeft,
                                            Origin = Anchor.CentreLeft,
                                            Text = "Playlist Name",
                                            Font = YuzakiFont.GetFont(YuzakiFont.Typeface.Saira, 48, YuzakiFont.FontWeight.Bold)
                                        },
                                        new YuzakiSpriteText()
                                        {
                                            Anchor = Anchor.CentreLeft,
                                            Origin = Anchor.CentreLeft,
                                            Text = "Playlist Description",
                                            Font = YuzakiFont.GetFont(YuzakiFont.Typeface.Saira, 20)
                                        },
                                        new YuzakiSpriteText()
                                        {
                                            Anchor = Anchor.CentreLeft,
                                            Origin = Anchor.CentreLeft,
                                            Text = "200 songs ∙ 2 hours 22 minutes",
                                            Font = YuzakiFont.GetFont(YuzakiFont.Typeface.Saira, 12, YuzakiFont.FontWeight.Light)
                                        },
                                    }
                                }
                            }
                        }
                    }
                },
            }
        };
    }
}
