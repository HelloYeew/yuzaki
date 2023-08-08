using System;
using System.Collections.Generic;
using System.IO;
using osu.Framework.Logging;
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
        try
        {
            string beatmapFilePath = Path.Combine(OsuStableLocation.DefaultSongsPath, beatmapEntry.FolderName, beatmapEntry.BeatmapFileName);
            BeatmapFile beatmapFile = BeatmapFile.Read(beatmapFilePath);
            List<EventBase> beatmapEvents = beatmapFile.Events;
            BackgroundEvent backgroundEvent = beatmapEvents.Find(e => e is BackgroundEvent) as BackgroundEvent;
            return backgroundEvent?.Path == null ? null : Path.Combine(OsuStableLocation.DefaultSongsPath, beatmapEntry.FolderName, backgroundEvent.Path);
        }
        catch (Exception e)
        {
            Logger.Error(e, "Failed to get beatmap background path.");
            return null;
        }
    }

    /// <summary>
    /// Get a full path of the beatmap video. Will return null if the beatmap doesn't have a video.
    /// </summary>
    /// <param name="beatmapEntry">The <see cref="BeatmapEntry"/> to get the video from.</param>
    /// <returns>The full path of the beatmap video. (Will return null if the beatmap doesn't have a video.)</returns>
    public static string GetVideoPath(BeatmapEntry beatmapEntry)
    {
        try
        {
            string beatmapFilePath = Path.Combine(OsuStableLocation.DefaultSongsPath, beatmapEntry.FolderName, beatmapEntry.BeatmapFileName);
            BeatmapFile beatmapFile = BeatmapFile.Read(beatmapFilePath);
            List<EventBase> beatmapEvents = beatmapFile.Events;
            VideoEvent videoEvent = beatmapEvents.Find(e => e is VideoEvent) as VideoEvent;
            return videoEvent?.Path == null ? null : Path.Combine(OsuStableLocation.DefaultSongsPath, beatmapEntry.FolderName, videoEvent.Path);
        }
        catch (Exception e)
        {
            Logger.Error(e, "Failed to get beatmap video path.");
            return null;
        }
    }
}
