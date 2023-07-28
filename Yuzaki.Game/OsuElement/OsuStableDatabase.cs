using System;
using System.Collections.Generic;
using System.Linq;
using osu_database_reader.BinaryFiles;
using osu_database_reader.Components.Beatmaps;
using osu.Framework.Logging;

namespace Yuzaki.Game.OsuElement;

/// <summary>
/// A class that contains all the databases used by osu!stable.
/// This class will inject into the game and will be used to read the osu! databases.
/// </summary>
public class OsuStableDatabase
{
    public OsuDb OsuDatabase = new OsuDb();
    public CollectionDb CollectionDatabase = new CollectionDb();
    public ScoresDb ScoresDatabase = new ScoresDb();
    public PresenceDb PresenceDatabase = new PresenceDb();

    /// <summary>
    /// Boolean that indicate whether the databases is finished loaded or not.
    /// </summary>
    public readonly bool IsLoaded = false;

    /// <summary>
    /// Boolean that indicate whether the databases is failed to load or not.
    /// </summary>
    public readonly bool IsFailed = false;

    public OsuStableDatabase()
    {
        try
        {
            OsuDatabase = OsuDb.Read(OsuStableLocation.DefaultDatabasePath);
            CollectionDatabase = CollectionDb.Read(OsuStableLocation.DefaultCollectionDatabasePath);
            ScoresDatabase = ScoresDb.Read(OsuStableLocation.DefaultScoreDatabasePath);
            PresenceDatabase = PresenceDb.Read(OsuStableLocation.DefaultPresenceDatabasePath);

            Logger.Log($"Loaded osu!stable databases from {OsuStableLocation.DefaultLocation}", LoggingTarget.Database);
            Logger.Log($"Found osu!stable database of {OsuDatabase.AccountName} with {OsuDatabase.Beatmaps.Count}", LoggingTarget.Database);
        }
        catch (Exception e)
        {
            IsFailed = true;
            Logger.Error(e, "Failed to load osu!stable databases");
        }

        IsLoaded = true;
    }

    /// <summary>
    /// Get the list of <see cref="BeatmapEntry"/> that contain all one beatmap per beatmapset.
    /// </summary>
    /// <returns></returns>
    public List<BeatmapEntry> GetUniqueBeatmapEntries()
    {
        List<BeatmapEntry> uniqueBeatmap = new List<BeatmapEntry>();

        foreach (BeatmapEntry beatmap in OsuDatabase.Beatmaps)
        {
            if (uniqueBeatmap.Any(x => x.BeatmapSetId == beatmap.BeatmapSetId))
            {
                continue;
            }
            else
            {
                uniqueBeatmap.Add(beatmap);
            }
        }

        return uniqueBeatmap;
    }
}
