using osu.Framework.Graphics;

namespace Yuzaki.Game.Audio;

public partial class YuzakiPlaylistManager : Component
{
    /// <summary>
    /// The default playlist that will contain all the beatmap.
    /// </summary>
    public Playlist AllBeatmapPlaylist { get; set; }

    public YuzakiPlaylistManager()
    {
        AllBeatmapPlaylist = new Playlist();
    }
}
