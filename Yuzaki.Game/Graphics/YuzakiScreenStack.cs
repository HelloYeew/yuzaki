using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;
using Yuzaki.Game.Graphics.Components;
using Yuzaki.Game.Graphics.Screens;

namespace Yuzaki.Game.Graphics
{
    public partial class YuzakiScreenStack : ScreenStack
    {
        public ScreenStack MainScreenStack;

        public MainPlayerScreenStack MainPlayerScreenStack;

        public ProfilePictureMenu ProfilePicture;

        public CircleIconButton SettingsButton;

        public YuzakiScreenStack()
        {
            InternalChildren = new Drawable[]
            {
                new YuzakiBackground
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                },
                MainPlayerScreenStack = new MainPlayerScreenStack
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                },
                ProfilePicture = new ProfilePictureMenu
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Size = new Vector2(ProfilePictureMenu.ICON_SIZE),
                    Margin = new MarginPadding()
                    {
                        Top = YuzakiStylingEnum.SCREEN_PADDING,
                        Right = YuzakiStylingEnum.SCREEN_PADDING
                    }
                },
                SettingsButton = new CircleIconButton(FontAwesome.Solid.Cog)
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Size = new Vector2(ProfilePictureMenu.ICON_SIZE),
                    Margin = new MarginPadding()
                    {
                        Top = YuzakiStylingEnum.SCREEN_PADDING,
                        Left = YuzakiStylingEnum.SCREEN_PADDING
                    }
                },
                new Container()
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.765f, 0.75f),
                    Masking = true,
                    CornerRadius = YuzakiStylingEnum.CORNER_RADIUS,
                    // Relative margin to the profile picture
                    Margin = new MarginPadding()
                    {
                        Top = YuzakiStylingEnum.SCREEN_PADDING + ProfilePictureMenu.ICON_SIZE + YuzakiStylingEnum.SCREEN_PADDING,
                        Right = YuzakiStylingEnum.SCREEN_PADDING
                    },
                    Children = new Drawable[]
                    {
                        new Box()
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1f),
                            Alpha = 0.6f,
                            Colour = YuzakiColour.MusicListBackgroundColour
                        }
                    }
                },
                new Container()
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.2f, 0.75f),
                    Masking = true,
                    CornerRadius = YuzakiStylingEnum.CORNER_RADIUS,
                    Margin = new MarginPadding()
                    {
                        Top = YuzakiStylingEnum.SCREEN_PADDING + ProfilePictureMenu.ICON_SIZE + YuzakiStylingEnum.SCREEN_PADDING,
                        Left = YuzakiStylingEnum.SCREEN_PADDING
                    },
                    Children = new Drawable[]
                    {
                        new Box()
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1f),
                            Alpha = 0.6f,
                            Colour = YuzakiColour.PlaylistListBackgroundColour
                        }
                    }
                }
            };
        }
    }
}
