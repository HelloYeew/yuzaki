namespace OsuDatabaseReader;
 
internal static class OsuVersions
{
    public const int EntryLengthInOsuDatabaseMin = 20160408;
    public const int EntryLengthInOsuDatabaseMax = 20191107;

    /// <summary>
    /// First version where osu.db:
    /// - uses floats for difficulty values
    /// - contains pre-calculated difficulty ratings
    /// </summary>
    public const int FloatDifficultyValues = 20140609;

    /// <summary>
    /// First version where:
    /// - Replays contain a 32-bit score id
    /// - osu.db contains ArtistUnicode and TitleUnicode
    /// </summary>
    public const int FirstOsz2 = 20121008;

    /// <summary>
    /// First version where replays contain a 64-bit score id, instead of 32-bit
    /// </summary>
    public const int ReplayScoreId64Bit = 20140721;
}