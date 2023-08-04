using System.IO;
using System.Threading;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osuTK;
using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Beatmaps;
using Yuzaki.Game.Audio;
using Yuzaki.Game.OsuElement;
using Yuzaki.Game.Store;

namespace Yuzaki.Game.Graphics.Components;

/// <summary>
/// The row of a song in a <see cref="PlaylistDetail"/>
/// </summary>
public partial class PlaylistSongEntry : CompositeDrawable
{
    [Resolved]
    private YuzakiPlayerManager playerManager { get; set; }

    [Resolved]
    private YuzakiTextureStore textureStore { get; set; }

    [Resolved]
    private GameHost host { get; set; }

    public Bindable<BeatmapEntry> BeatmapEntry { get; set; }
    public string SongName { get; set; }
    public string SongArtist { get; set; }

    private CircleIconButton playButton;

    private Texture defaultAlbumArtTexture;
    private YuzakiSpriteText songNameText;
    private YuzakiSpriteText artistNameText;
    private Sprite albumArt;

    [BackgroundDependencyLoader]
    private void load()
    {
        defaultAlbumArtTexture = textureStore.Get("default_album.jpg");

        InternalChild = new Container()
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.X,
            AutoSizeAxes = Axes.Y,
            Masking = true,
            CornerRadius = YuzakiStylingEnum.CORNER_RADIUS,
            Name = "PlaylistSongsContainer",
            Children = new Drawable[]
            {
                new Box()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Colour = YuzakiColour.PlaylistEntryBackgroundColour,
                    Alpha = 0.6f
                },
                new FillFlowContainer()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(8),
                    Padding = new MarginPadding(8),
                    Children = new Drawable[]
                    {
                        playButton = new CircleIconButton(FontAwesome.Solid.Play)
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Size = new Vector2(20),
                        },
                        new Container()
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            Size = new Vector2(48),
                            Masking = true,
                            CornerRadius = 6,
                            Child = albumArt = new Sprite()
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                RelativeSizeAxes = Axes.Both,
                                Texture = defaultAlbumArtTexture,
                                FillMode = FillMode.Fill
                            }
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
                                songNameText = new YuzakiSpriteText()
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Text = SongName,
                                    Font = YuzakiFont.GetFont(YuzakiFont.Typeface.Saira, 20)
                                },
                                artistNameText = new YuzakiSpriteText()
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Text = SongArtist,
                                    Font = YuzakiFont.GetFont()
                                },
                            }
                        }
                    }
                }
            }
        };

        playButton.Action = () =>
        {
            playerManager.Play(BeatmapEntry.Value);
        };
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();

        // Currently not bind due to memory consumption
        // BeatmapEntry.BindValueChanged(beatmap =>
        // {
        //     Thread updateAlbumArtThread = new Thread(() =>
        //     {
        //         UpdateAlbumArt(BeatmapEntry.Value);
        //     });
        //     updateAlbumArtThread.IsBackground = true;
        //     updateAlbumArtThread.Start();
        // }, true);
    }

    /// <summary>
    /// Update the album art of the song entry
    /// </summary>
    /// <param name="beatmap">The beatmap to update the album art from</param>
    private void UpdateAlbumArt(BeatmapEntry beatmap)
    {
        if (beatmap == null)
        {
            Logger.Log("No beatmap selected or beatmap is null, using default cover art instead.");
            songNameText.Text = "No beatmap selected";
            artistNameText.Text = "Please select a beatmap to play!";
            albumArt.Texture = defaultAlbumArtTexture;
        }

        SongName = beatmap.Title;
        SongArtist = beatmap.Artist;

        string albumArtPath = Utility.GetBeatmapBackgroundPath(beatmap);

        if (albumArtPath == null)
        {
            Logger.Log($"Beatmap {beatmap.Artist} - {beatmap.Title} ({beatmap.BeatmapId}) doesn't have a background, using default album art instead.");
            albumArt.Texture = defaultAlbumArtTexture;
        }
        else
        {
            Logger.Log($"Found album art for beatmap {beatmap.Artist} - {beatmap.Title} ({beatmap.BeatmapId}) at {albumArtPath}.");
            FileStream fileStream = new FileStream(albumArtPath, FileMode.Open, FileAccess.Read);
            albumArt.Texture = Texture.FromStream(host.Renderer, fileStream);
            fileStream.Dispose();
        }
    }
}
