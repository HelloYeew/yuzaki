using System.Globalization;

namespace OsuDatabaseReader.OsuElement;

internal static class Constants
{
    //nfi so parsing works on all cultures
    public static readonly NumberFormatInfo NumberFormat = new CultureInfo(@"en-US", false).NumberFormat;
}