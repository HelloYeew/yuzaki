﻿using System.Threading;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Beatmaps;
using Yuzaki.Game.Audio;
using Yuzaki.Game.Graphics.Components;
using Yuzaki.Game.OsuElement;

namespace Yuzaki.Game.Graphics.Screens;

public partial class PlaylistScreen : Screen
{
    public PlaylistMenu PlaylistMenuComponent;

    public PlaylistDetail PlaylistDetailComponent;

    [Resolved]
    private OsuStableDatabase database { get; set; }

    [Resolved]
    private YuzakiPlaylistManager playlistManager { get; set; }

    public PlaylistScreen()
    {
        InternalChildren = new Drawable[]
        {
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
