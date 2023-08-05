using System.Collections.Generic;
using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Beatmaps;

namespace Yuzaki.Game.Audio;

/// <summary>
/// A class act as playlist contain list of <see cref="BeatmapEntry"/>
/// </summary>
public class Playlist
{
    private List<BeatmapEntry> beatmapEntries = new List<BeatmapEntry>();

    /// <summary>
    /// The list of <see cref="BeatmapEntry"/> in the playlist
    /// </summary>
    public List<BeatmapEntry> List
    {
        get => beatmapEntries;
        set => beatmapEntries = value;
    }

    /// <summary>
    /// Add a <see cref="BeatmapEntry"/> to the playlist
    /// </summary>
    /// <param name="beatmapEntry">The <see cref="BeatmapEntry"/> to add</param>
    public void Add(BeatmapEntry beatmapEntry)
    {
        beatmapEntries.Add(beatmapEntry);
    }

    /// <summary>
    /// Remove a <see cref="BeatmapEntry"/> from the playlist
    /// </summary>
    /// <param name="beatmapEntry">The <see cref="BeatmapEntry"/> to remove</param>
    public void Remove(BeatmapEntry beatmapEntry)
    {
        beatmapEntries.Remove(beatmapEntry);
    }

    /// <summary>
    /// Count the number of <see cref="BeatmapEntry"/> in the playlist
    /// </summary>
    /// <returns></returns>
    public int Count()
    {
        return beatmapEntries.Count;
    }

    /// <summary>
    /// Set the entire list of <see cref="BeatmapEntry"/> in the playlist to a new list
    /// </summary>
    /// <param name="beatmapEntries">A new list of <see cref="BeatmapEntry"/></param>
    public void SetList(List<BeatmapEntry> beatmapEntries)
    {
        this.beatmapEntries = beatmapEntries;
    }
}
