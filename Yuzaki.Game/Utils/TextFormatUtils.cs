namespace Yuzaki.Game.Utils;

/// <summary>
/// Helper class for formatting text.
/// </summary>
public class TextFormatUtils
{
    /// <summary>
    /// Format the time in millisecond to mm:ss format.
    /// </summary>
    /// <param name="time">The time in second.</param>
    /// <returns>The formatted time in mm:ss format.</returns>
    public static string GetFormattedTime(double time)
    {
        double minute = time >= 60 ? time / 60 : 0;
        double second = time % 60;
        return $"{minute:00}:{second:00}";
    }
}
