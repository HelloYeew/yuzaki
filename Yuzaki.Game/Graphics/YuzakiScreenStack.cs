using System.Threading;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK;
using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Beatmaps;
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

        public PlaylistDetail PlaylistDetailComponent;

        public PlaylistMenu PlaylistMenuComponent;

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
                PlaylistMenuComponent = new PlaylistMenu()
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    RelativeSizeAxes = Axes.Both,
                    Margin = new MarginPadding()
                    {
                        Top = YuzakiStylingEnum.SCREEN_PADDING + ProfilePictureMenu.ICON_SIZE + YuzakiStylingEnum.SCREEN_PADDING,
                        Left = YuzakiStylingEnum.SCREEN_PADDING
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            PlaylistDetailComponent = new PlaylistDetail()
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
                RelativeSizeAxes = Axes.Both,
                Margin = new MarginPadding()
                {
                    Top = YuzakiStylingEnum.SCREEN_PADDING + ProfilePictureMenu.ICON_SIZE + YuzakiStylingEnum.SCREEN_PADDING,
                    Right = YuzakiStylingEnum.SCREEN_PADDING
                }
            };

            Thread thread = new Thread(() =>
            {
                foreach (BeatmapEntry entry in playlistManager.AllBeatmapPlaylist.List)
                {
                    PlaylistDetailComponent.AddSongEntry(entry);
                }

                Scheduler.Add(() => AddInternal(PlaylistDetailComponent));
            });

            thread.Start();
        }
    }
}
