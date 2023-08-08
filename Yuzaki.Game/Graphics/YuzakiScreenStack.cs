using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;
using Yuzaki.Game.Audio;
using Yuzaki.Game.Graphics.Components;
using Yuzaki.Game.Graphics.Screens;
using Yuzaki.Game.OsuElement;

namespace Yuzaki.Game.Graphics
{
    public partial class YuzakiScreenStack : ScreenStack
    {
        public ScreenStack MainScreenStack;

        public MainPlayerScreenStack MainPlayerScreenStack;

        public ProfilePictureMenu ProfilePicture;

        public CircleIconButton SettingsButton;

        [Resolved]
        private OsuStableDatabase database { get; set; }

        [Resolved]
        private YuzakiPlaylistManager playlistManager { get; set; }

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
                MainScreenStack = new ScreenStack
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                }
            };

            MainScreenStack.Push(new PlaylistScreen()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both
            });
        }
    }
}
