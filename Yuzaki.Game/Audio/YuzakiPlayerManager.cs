using System.IO;
using ManagedBass;
using osu_database_reader.Components.Beatmaps;
using osu.Framework.Bindables;
using Yuzaki.Game.OsuElement;

namespace Yuzaki.Game.Audio;

/// <summary>
/// The global audio player manager and control all operation of the player.
/// </summary>
public class YuzakiPlayerManager
{
    public Bindable<BeatmapEntry> CurrentBeatmap = new Bindable<BeatmapEntry>();

    private int fileStream;

    /// <summary>
    /// Plays the audio of the <see cref="BeatmapEntry"/>
    /// </summary>
    /// <param name="beatmapEntry">The <see cref="BeatmapEntry"/> to play the audio from.</param>
    public void Play(BeatmapEntry beatmapEntry)
    {
        if (fileStream != 0)
        {
            Bass.ChannelStop(fileStream);
            Bass.StreamFree(fileStream);
        }

        string audioPath = Path.Combine(OsuStableLocation.DefaultSongsPath, beatmapEntry.FolderName, beatmapEntry.AudioFileName);

        fileStream = Bass.CreateStream(audioPath);
        Bass.ChannelPlay(fileStream);
        CurrentBeatmap.Value = beatmapEntry;
    }

    /// <summary>
    /// Pauses the current audio
    /// </summary>
    public void Pause()
    {
        Bass.ChannelPause(fileStream);
    }
}
