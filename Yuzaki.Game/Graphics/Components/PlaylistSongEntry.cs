using System.IO;
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
    private Texture albumArtTexture;
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
            playerManager.PlayNewBeatmap(BeatmapEntry.Value);
        };
    }

    // private bool lastUpdateState = true;

    protected override void Update()
    {
        base.Update();

        // This is an easy pooling for the album art, it's not the best but it works for now
        // This still make GPU usage go up by a lot so I'll have to find a better way to do this
        // if (RequiresChildrenUpdate != lastUpdateState)
        // {
        //     if (RequiresChildrenUpdate)
        //     {
        //         UpdateAlbumArt(BeatmapEntry.Value);
        //     }
        //     else
        //     {
        //         DisposeAlbumTexture();
        //     }
        //
        //     lastUpdateState = RequiresChildrenUpdate;
        // }
    }

    /// <summary>
    /// Update the album art of the song entry
    /// </summary>
    /// <param name="beatmap">The beatmap to update the album art from</param>
    public void UpdateAlbumArt(BeatmapEntry beatmap)
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
            albumArtTexture = Texture.FromStream(host.Renderer, fileStream);
            albumArt.Texture = albumArtTexture;
            fileStream.Dispose();
        }
    }

    public void DisposeAlbumTexture()
    {
        albumArt.Texture = defaultAlbumArtTexture;
        albumArtTexture?.Dispose();
        Logger.Log("Disposing album art texture.");
    }
}
