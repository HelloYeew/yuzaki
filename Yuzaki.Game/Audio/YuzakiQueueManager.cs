using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Logging;
using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Beatmaps;
using Yuzaki.Game.OsuElement;

namespace Yuzaki.Game.Audio;

/// <summary>
/// The queue manager that manage the queue of <see cref="BeatmapEntry"/>
/// </summary>
public partial class YuzakiQueueManager : Component
{
    private List<BeatmapEntry> beatmapQueue;

    private int currentBeatmapIndex = 0;

    public YuzakiQueueManager()
    {
        beatmapQueue = new List<BeatmapEntry>();
    }

    [Resolved]
    private OsuStableDatabase database { get; set; }

    [Resolved]
    private YuzakiPlayerManager playerManager { get; set; }

    /// <summary>
    /// Add a <see cref="BeatmapEntry"/> to the last of the queue
    /// </summary>
    /// <param name="beatmapEntry"></param>
    public void AddLast(BeatmapEntry beatmapEntry)
    {
        beatmapQueue.Add(beatmapEntry);
    }

    /// <summary>
    /// Add a <see cref="BeatmapEntry"/> to the first of the queue
    /// </summary>
    /// <param name="beatmapEntry"></param>
    public void AddFirst(BeatmapEntry beatmapEntry)
    {
        beatmapQueue.Insert(0, beatmapEntry);
    }

    /// <summary>
    /// Clear the queue
    /// </summary>
    public void Clear()
    {
        beatmapQueue.Clear();
    }

    /// <summary>
    /// Return the count of the queue
    /// </summary>
    /// <returns>The count of the queue</returns>
    public int Count()
    {
        return beatmapQueue.Count;
    }

    /// <summary>
    /// Remove the first <see cref="BeatmapEntry"/> from the queue
    /// </summary>
    public void RemoveFirst()
    {
        beatmapQueue.RemoveAt(0);
    }

    /// <summary>
    /// Replace the current queue with a new queue
    /// </summary>
    /// <param name="beatmapEntries">The new queue</param>
    public void SetQueue(List<BeatmapEntry> beatmapEntries)
    {
        beatmapQueue = beatmapEntries;
        Logger.Log($"Queue set to {beatmapQueue.Count} beatmaps (Total: {beatmapQueue.Count})");
    }

    /// <summary>
    /// Set the current index to a specific index
    /// </summary>
    /// <param name="index">The index to set</param>
    public void SetIndex(int index)
    {
        currentBeatmapIndex = index;
        Logger.Log($"Current index set to {currentBeatmapIndex} (Total: {beatmapQueue.Count})");
    }

    /// <summary>
    /// Set the current index to the current beatmap
    /// </summary>
    public void SetIndexToCurrent()
    {
        SetIndex(beatmapQueue.IndexOf(playerManager.CurrentBeatmap.Value));
    }

    public void Next()
    {
        if (currentBeatmapIndex + 1 >= beatmapQueue.Count)
        {
            currentBeatmapIndex = 0;
        }
        else
        {
            currentBeatmapIndex++;
        }
    }

    public void Previous()
    {
        if (currentBeatmapIndex - 1 < 0)
        {
            currentBeatmapIndex = beatmapQueue.Count - 1;
        }
        else
        {
            currentBeatmapIndex--;
        }
    }

    public BeatmapEntry GetNext()
    {
        if (currentBeatmapIndex + 1 >= beatmapQueue.Count)
        {
            return beatmapQueue[0];
        }
        else
        {
            return beatmapQueue[currentBeatmapIndex + 1];
        }
    }

    public BeatmapEntry GetPrevious()
    {
        if (currentBeatmapIndex - 1 < 0)
        {
            return beatmapQueue[^1];
        }
        else
        {
            return beatmapQueue[currentBeatmapIndex - 1];
        }
    }
}
