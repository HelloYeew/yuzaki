using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Beatmaps;
using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Events;
using Yuzaki.DatabaseReader.Stable.OsuElement.Components.HitObjects;

namespace OsuDatabaseReader.OsuElement;

internal static class Extensions
{
    public static string GetValueOrNull(this Dictionary<string, string> dic, string key)
    {
        return dic.ContainsKey(key)
            ? dic[key]
            : null;
    }

    //for StreamReader
    public static BeatmapSection? ReadUntilSectionStart(this StreamReader sr)
    {
        while (!sr.EndOfStream)
        {
            string str = sr.ReadLine();
            if (string.IsNullOrWhiteSpace(str)) continue;

            string stripped = str.TrimStart('[').TrimEnd(']');
            if (!Enum.TryParse(stripped, out BeatmapSection a))
                throw new Exception("Unrecognized beatmap section: " + stripped);
            return a;
        }

        //we reached an end of stream
        return null;
    }

    public static Dictionary<string, string> ReadBasicSection(this StreamReader sr, bool extraSpaceAfterColon = true, bool extraSpaceBeforeColon = false)
    {
        var dic = new Dictionary<string, string>();

        string line;
        while (!string.IsNullOrWhiteSpace(line = sr.ReadLine()))
        {
            if (!line.Contains(':'))
                throw new Exception("Invalid key/value line: " + line);

            int i = line.IndexOf(':');

            string key = line.Substring(0, i);
            string value = line.Substring(i + 1);

            //This is just so we can recreate files properly in the future.
            //It is very likely not needed at all, but it makes me sleep
            //better at night knowing everything is 100% correct.
            if (extraSpaceBeforeColon && key.EndsWith(" ")) key = key.Substring(0, key.Length - 1);
            if (extraSpaceAfterColon && value.StartsWith(" ")) value = value.Substring(1);

            dic.Add(key, value);
        }

        return dic;
    }

    public static IEnumerable<HitObject> ReadHitObjects(this StreamReader sr)
    {
        string line;
        while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            yield return HitObject.FromString(line);
    }

    public static IEnumerable<TimingPoint> ReadTimingPoints(this StreamReader sr)
    {
        string line;
        while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            yield return TimingPoint.FromString(line);
    }

    public static IEnumerable<EventBase> ReadEvents(this StreamReader sr)
    {
        string line;
        while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            if (!line.StartsWith("//"))
                yield return EventBase.FromString(line);
    }
}