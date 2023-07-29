using System.Collections.Generic;
using System.IO;
using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Beatmaps;
using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Events;
using Yuzaki.DatabaseReader.Stable.OsuElement.TextFiles;

namespace Yuzaki.Game.OsuElement;

public class Utility
{
    /// <summary>
    /// Get a full path of the beatmap background image. Will return null if the beatmap doesn't have a background image.
    /// </summary>
    /// <param name="beatmapEntry">The <see cref="BeatmapEntry"/> to get the background image from.</param>
    /// <returns>The full path of the beatmap background image. (Will return null if the beatmap doesn't have a background image.)</returns>
    public static string GetBeatmapBackgroundPath(BeatmapEntry beatmapEntry)
    {
        string beatmapFilePath = Path.Combine(OsuStableLocation.DefaultSongsPath, beatmapEntry.FolderName, beatmapEntry.BeatmapFileName);
        BeatmapFile beatmapFile = BeatmapFile.Read(beatmapFilePath);
        List<EventBase> beatmapEvents = beatmapFile.Events;
        BackgroundEvent backgroundEvent = beatmapEvents.Find(e => e is BackgroundEvent) as BackgroundEvent;
        return backgroundEvent?.Path == null ? null : Path.Combine(OsuStableLocation.DefaultSongsPath, beatmapEntry.FolderName, backgroundEvent.Path);
    }
}
