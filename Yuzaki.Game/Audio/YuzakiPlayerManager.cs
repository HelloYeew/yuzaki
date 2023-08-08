using System.IO;
using ManagedBass;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;
using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Beatmaps;
using Yuzaki.Game.OsuElement;

namespace Yuzaki.Game.Audio;

/// <summary>
/// The global audio player manager and control all operation of the player.
/// </summary>
public partial class YuzakiPlayerManager : Component
{
    public Bindable<BeatmapEntry> CurrentBeatmap = new Bindable<BeatmapEntry>();
    public BindableBool Playing = new BindableBool(false);
    public BindableDouble CurrentTime = new BindableDouble(0);
    private int fileStream;

    [Resolved]
    private YuzakiQueueManager queueManager { get; set; }

    public YuzakiPlayerManager()
    {
        CurrentBeatmap.ValueChanged += (e) => Logger.Log($"Current beatmap changed to {e.NewValue?.Artist} - {e.NewValue?.Title} (Beatmap ID: {e.NewValue?.BeatmapId})");
    }

    /// <summary>
    /// Plays the audio of the <see cref="BeatmapEntry"/>
    /// </summary>
    /// <param name="beatmapEntry">The <see cref="BeatmapEntry"/> to play the audio from.</param>
    public void PlayNewBeatmap(BeatmapEntry beatmapEntry)
    {
        if (fileStream != 0)
        {
            Bass.ChannelStop(fileStream);
            Bass.StreamFree(fileStream);
        }

        string audioPath = Path.Combine(OsuStableLocation.DefaultSongsPath, beatmapEntry.FolderName, beatmapEntry.AudioFileName);

        fileStream = Bass.CreateStream(audioPath);
        Bass.ChannelPlay(fileStream);
        Playing.Value = true;
        CurrentBeatmap.Value = beatmapEntry;

        queueManager.SetIndexToCurrent();
    }

    /// <summary>
    /// Plays the current audio
    /// </summary>
    public void Play()
    {
        if (fileStream == 0) return;

        Bass.ChannelPlay(fileStream);
        Playing.Value = true;
    }

    /// <summary>
    /// Pauses the current audio
    /// </summary>
    public void Pause()
    {
        if (fileStream == 0) return;

        Bass.ChannelPause(fileStream);
        Playing.Value = false;
    }

    public void Next()
    {
        PlayNewBeatmap(queueManager.GetNext());
    }

    public void Previous()
    {
        PlayNewBeatmap(queueManager.GetPrevious());
    }

    /// <summary>
    /// Checks if the audio is playing
    /// </summary>
    /// <returns>True if the audio is playing, false otherwise.</returns>
    public bool IsPlaying()
    {
        if (fileStream == 0) return false;

        return Bass.ChannelIsActive(fileStream) == PlaybackState.Playing;
    }

    /// <summary>
    /// Return the current audio time in seconds
    /// </summary>
    /// <returns>The current audio time in seconds</returns>
    public double GetCurrentTime()
    {
        if (fileStream == 0) return 0;

        return Bass.ChannelBytes2Seconds(fileStream, Bass.ChannelGetPosition(fileStream));
    }

    /// <summary>
    /// Return the total audio time in seconds
    /// </summary>
    /// <returns>The total audio time in seconds</returns>
    public double GetTotalTime()
    {
        if (fileStream == 0) return 0;

        return Bass.ChannelBytes2Seconds(fileStream, Bass.ChannelGetLength(fileStream));
    }

    /// <summary>
    /// Update the <see cref="CurrentTime"/> bindable to the current audio time.
    /// </summary>
    public void UpdateBindableTime()
    {
        CurrentTime.Value = GetCurrentTime();
    }

    /// <summary>
    /// Seek the audio to the specified time in seconds.
    /// </summary>
    /// <param name="time">The time in seconds to seek to.</param>
    public void Seek(double time)
    {
        if (fileStream == 0) return;

        if (time < 0) time = 0;
        if (time > GetTotalTime()) time = GetTotalTime();
        Bass.ChannelSetPosition(fileStream, Bass.ChannelSeconds2Bytes(fileStream, time));
        Logger.Log($"Seek to {time} seconds");
    }

    /// <summary>
    /// Checks if the audio has ended
    /// </summary>
    /// <returns>True if the audio has ended, false otherwise.</returns>
    public bool HasEnded()
    {
        if (fileStream == 0) return false;

        // Sometime the time that BASS returns is not accurate, so we also check the time in 0.2 seconds gap.
        return (GetCurrentTime() >= GetTotalTime() || GetTotalTime() - GetCurrentTime() < 0.2) && !IsPlaying();
    }
}
