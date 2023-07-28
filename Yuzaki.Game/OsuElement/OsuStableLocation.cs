using System;
using System.IO;

namespace Yuzaki.Game.OsuElement;

public class OsuStableLocation
{
    /// <summary>
    /// Get the default location of the osu!stable installation.
    /// The default location is <c>%localappdata%\osu!</c>.
    /// </summary>
    /// <returns>The default location of the osu!stable installation.</returns>
    public static string DefaultLocation { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "osu!");

    public static string DefaultDatabaseName { get; } = "osu!.db";
    public static string CollectionDatabaseName { get; } = "collection.db";
    public static string PresenceDatabaseName { get; } = "presence.db";
    public static string ScoreDatabaseName { get; } = "scores.db";

    /// <summary>
    /// Get the default path of the osu!stable database.
    /// </summary>
    public static string DefaultDatabasePath { get; } = Path.Combine(DefaultLocation, DefaultDatabaseName);

    /// <summary>
    /// Get the default path of the osu!stable collection database.
    /// </summary>
    public static string DefaultCollectionDatabasePath { get; } = Path.Combine(DefaultLocation, CollectionDatabaseName);

    /// <summary>
    /// Get the default path of the osu!stable presence database.
    /// </summary>
    public static string DefaultPresenceDatabasePath { get; } = Path.Combine(DefaultLocation, PresenceDatabaseName);

    /// <summary>
    /// Get the default path of the osu!stable score database.
    /// </summary>
    public static string DefaultScoreDatabasePath { get; } = Path.Combine(DefaultLocation, ScoreDatabaseName);
}
