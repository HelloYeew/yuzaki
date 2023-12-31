﻿using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Logging;
using Yuzaki.DatabaseReader.Stable.Database;
using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Beatmaps;

namespace Yuzaki.Game.OsuElement;

/// <summary>
/// A class that contains all the databases used by osu!stable.
/// This class will inject into the game and will be used to read the osu! databases.
/// </summary>
public class OsuStableDatabase
{
    public OsuDatabase OsuDatabase = new OsuDatabase();
    public CollectionDatabase CollectionDatabase = new CollectionDatabase();
    public ScoreDatabase ScoreDatabase = new ScoreDatabase();
    public PresenceDatabase PresenceDatabase = new PresenceDatabase();

    private List<BeatmapEntry> uniqueBeatmapEntries;

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
            OsuDatabase = OsuDatabase.Read(OsuStableLocation.DefaultDatabasePath);
            CollectionDatabase = CollectionDatabase.Read(OsuStableLocation.DefaultCollectionDatabasePath);
            ScoreDatabase = ScoreDatabase.Read(OsuStableLocation.DefaultScoreDatabasePath);
            PresenceDatabase = PresenceDatabase.Read(OsuStableLocation.DefaultPresenceDatabasePath);

            Logger.Log($"Loaded osu!stable databases from {OsuStableLocation.DefaultLocation}", LoggingTarget.Database);
            Logger.Log($"Found osu!stable database of {OsuDatabase.AccountName} with {OsuDatabase.Beatmaps.Count}", LoggingTarget.Database);
        }
        catch (Exception e)
        {
            IsFailed = true;
            Logger.Error(e, "Failed to load osu!stable databases");
        }

        uniqueBeatmapEntries = getUniqueBeatmapEntries();

        IsLoaded = true;
    }

    /// <summary>
    /// Get the list of <see cref="BeatmapEntry"/> that contain all one beatmap per beatmapset.
    /// </summary>
    /// <returns></returns>
    private List<BeatmapEntry> getUniqueBeatmapEntries()
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

    /// <summary>
    /// Get the list of <see cref="BeatmapEntry"/> that contain all one beatmap per beatmapset.
    /// </summary>
    /// <returns></returns>
    public List<BeatmapEntry> GetUniqueBeatmapEntries()
    {
        return uniqueBeatmapEntries;
    }
}
