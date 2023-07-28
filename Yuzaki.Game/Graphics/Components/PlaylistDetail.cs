using System.Collections.Generic;
using osu_database_reader.Components.Beatmaps;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace Yuzaki.Game.Graphics.Components
{
    public partial class PlaylistDetail : CompositeDrawable
    {
        private List<Drawable> playlistDetailChildren;

        public PlaylistDetail()
        {
            playlistDetailChildren = new List<Drawable>
            {
                new PlaylistInfo()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.X,
                    Size = new Vector2(1, 220),
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textureStore)
        {
            Anchor = Anchor.TopRight;
            Origin = Anchor.TopRight;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.765f, 0.75f);
            Masking = true;
            CornerRadius = YuzakiStylingEnum.CORNER_RADIUS;
            InternalChildren = new Drawable[]
            {
                new Box()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(1f),
                    Alpha = 0.6f,
                    Colour = YuzakiColour.MusicListBackgroundColour
                },
                new Container()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new BasicScrollContainer()
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            ScrollbarVisible = true,
                            Margin = new MarginPadding(28),
                            Size = new Vector2(0.9f),
                            Children = new Drawable[]
                            {
                                new FillFlowContainer()
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Direction = FillDirection.Vertical,
                                    Spacing = new Vector2(0, 8),
                                    Name = "FillFlowPlaylistContainer",
                                    Children = playlistDetailChildren
                                }
                            }
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Add a <see cref="BeatmapEntry"/> to the playlist.
        /// </summary>
        /// <param name="entry">The beatmap entry to add.</param>
        public void AddSongEntry(BeatmapEntry entry)
        {
            playlistDetailChildren.Add(new PlaylistSongEntry()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.X,
                BeatmapEntry = new Bindable<BeatmapEntry>(entry),
                SongName = entry.Title,
                SongArtist = entry.Artist,
                Size = new Vector2(1, 64),
            });
        }
    }
}
