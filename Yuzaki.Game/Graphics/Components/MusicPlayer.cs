using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

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
                    Width = 0.98f,
                    Height = 100,
                    Masking = true,
                    CornerRadius = 10,
                    Margin = new MarginPadding(YuzakiStylingEnum.SCREEN_PADDING),
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
                        new FillFlowContainer()
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Direction = FillDirection.Horizontal,
                            Spacing = new Vector2(10),
                            Children = new Drawable[]
                            {
                                new Container()
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Size = new Vector2(100),
                                    Child = new Container()
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Size = new(80),
                                        Masking = true,
                                        CornerRadius = YuzakiStylingEnum.CORNER_RADIUS,
                                        Child = new Sprite()
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            RelativeSizeAxes = Axes.Both,
                                            Texture = textureStore.Get("default_album.jpg")
                                        }
                                    }
                                },
                                new Container()
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(0.2f, 1),
                                    Child = new FillFlowContainer()
                                    {
                                        Anchor = Anchor.CentreLeft,
                                        Origin = Anchor.CentreLeft,
                                        RelativeSizeAxes = Axes.Both,
                                        Direction = FillDirection.Vertical,
                                        Spacing = new Vector2(5),
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
                                                Font = YuzakiFont.GetFont(size: 20f)
                                            }
                                        }
                                    }
                                },
                                new Container
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(0.35f, 0.6f),
                                    Child = new FillFlowContainer()
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        RelativeSizeAxes = Axes.Both,
                                        Height = 0.7f,
                                        Direction = FillDirection.Vertical,
                                        Children = new Drawable[]
                                        {
                                            // Controls
                                            new FillFlowContainer()
                                            {
                                                Anchor = Anchor.Centre,
                                                Origin = Anchor.Centre,
                                                RelativeSizeAxes = Axes.Both,
                                                Direction = FillDirection.Horizontal,
                                                Spacing = new Vector2(30),
                                                Children = new Drawable[]
                                                {
                                                    new IconButton(FontAwesome.Solid.StepBackward)
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        Size = new Vector2(35)
                                                    },
                                                    new CircleIconButton(FontAwesome.Solid.Play)
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        Size = new Vector2(40)
                                                    },
                                                    new IconButton(FontAwesome.Solid.StepForward)
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        Size = new Vector2(35)
                                                    },
                                                },
                                            },
                                            // Progress bar
                                            new FillFlowContainer()
                                            {
                                                Anchor = Anchor.Centre,
                                                Origin = Anchor.Centre,
                                                RelativeSizeAxes = Axes.Both,
                                                Direction = FillDirection.Horizontal,
                                                Spacing = new Vector2(5),
                                                Children = new Drawable[]
                                                {
                                                    new YuzakiSpriteText()
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        Text = "0:00",
                                                        Colour = YuzakiColour.MusicPlayerTimeTextColour,
                                                        Font = YuzakiFont.GetFont(size: 16f)
                                                    },
                                                    new Container()
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        RelativeSizeAxes = Axes.X,
                                                        Height = 10,
                                                        Masking = true,
                                                        CornerRadius = 5,
                                                        Children = new Drawable[]
                                                        {
                                                            new Box()
                                                            {
                                                                Anchor = Anchor.CentreLeft,
                                                                Origin = Anchor.CentreLeft,
                                                                RelativeSizeAxes = Axes.X,
                                                                Height = 10,
                                                                Colour = YuzakiColour.MusicPlayerProgressBarBackgroundColour
                                                            },
                                                            new Box()
                                                            {
                                                                Anchor = Anchor.CentreLeft,
                                                                Origin = Anchor.CentreLeft,
                                                                RelativeSizeAxes = Axes.X,
                                                                Width = 0.5f,
                                                                Height = 10,
                                                                Colour = YuzakiColour.MusicPlayerProgressBarForegroundColour
                                                            },
                                                        }
                                                    },
                                                    new YuzakiSpriteText()
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        Text = "0:00",
                                                        Colour = YuzakiColour.MusicPlayerTimeTextColour,
                                                        Font = YuzakiFont.GetFont(size: 16f)
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                new Container
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Size = new Vector2(100),
                                    Child = new FillFlowContainer()
                                    {
                                        Anchor = Anchor.CentreLeft,
                                        Origin = Anchor.CentreLeft,
                                        RelativeSizeAxes = Axes.Both,
                                        Direction = FillDirection.Horizontal,
                                        Spacing = new Vector2(5),
                                        Children = new Drawable[]
                                        {
                                            new IconButton(FontAwesome.Solid.VolumeUp)
                                            {
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                Size = new Vector2(50)
                                            },
                                            new Container()
                                            {
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                RelativeSizeAxes = Axes.X,
                                                Height = 10,
                                                Masking = true,
                                                CornerRadius = 5,
                                                Children = new Drawable[]
                                                {
                                                    new Box()
                                                    {
                                                        Anchor = Anchor.CentreLeft,
                                                        Origin = Anchor.CentreLeft,
                                                        RelativeSizeAxes = Axes.X,
                                                        Height = 10,
                                                        Colour = YuzakiColour.MusicPlayerVolumeSliderBackgroundColour
                                                    },
                                                    new Box()
                                                    {
                                                        Anchor = Anchor.CentreLeft,
                                                        Origin = Anchor.CentreLeft,
                                                        RelativeSizeAxes = Axes.X,
                                                        Width = 0.5f,
                                                        Height = 10,
                                                        Colour = YuzakiColour.MusicPlayerVolumeSliderForegroundColour
                                                    },
                                                }
                                            }
                                        },
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
