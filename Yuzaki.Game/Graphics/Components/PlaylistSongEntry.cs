using osu_database_reader.Components.Beatmaps;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace Yuzaki.Game.Graphics.Components;

/// <summary>
/// The row of a song in a <see cref="PlaylistDetail"/>
/// </summary>
public partial class PlaylistSongEntry : CompositeDrawable
{
    public Bindable<BeatmapEntry> BeatmapEntry { get; set; }
    public string SongName { get; set; }
    public string SongArtist { get; set; }

    [BackgroundDependencyLoader]
    private void load(TextureStore textureStore)
    {
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
                        new CircleIconButton(FontAwesome.Solid.Play)
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
                            Child = new Sprite()
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Size = new Vector2(48),
                                Texture = textureStore.Get("default_album.jpg")
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
                                new YuzakiSpriteText()
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Text = SongName,
                                    Font = YuzakiFont.GetFont(YuzakiFont.Typeface.Saira, 20)
                                },
                                new YuzakiSpriteText()
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

        BeatmapEntry.BindValueChanged(beatmapEntry =>
        {
            SongName = beatmapEntry.NewValue.Title;
            SongArtist = beatmapEntry.NewValue.Artist;
        }, true);
    }
}
