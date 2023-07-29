using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Player;
using Yuzaki.DatabaseReader.Stable.OsuElement.Serialization;

namespace Yuzaki.DatabaseReader.Stable.Database;

public class ScoreDatabase : ISerializable
{
    public int OsuVersion;
    public readonly Dictionary<string, List<Replay>> Beatmaps = new();
    public IEnumerable<Replay> Scores => Beatmaps.SelectMany(a => a.Value);

    public static ScoreDatabase Read(string path)
    {
        using var stream = File.OpenRead(path);
        return Read(stream);
    }

    public static ScoreDatabase Read(Stream stream)
    {
        var db = new ScoreDatabase();
        using var r = new SerializationReader(stream);
        db.ReadFromStream(r);

        return db;
    }

    public void ReadFromStream(SerializationReader r)
    {
        OsuVersion = r.ReadInt32();
        int amount = r.ReadInt32();

        for (int i = 0; i < amount; i++)
        {
            string md5 = r.ReadString();

            var list = new List<Replay>();

            int amount2 = r.ReadInt32();
            for (int j = 0; j < amount2; j++)
                list.Add(Replay.ReadFromReader(r));

            Beatmaps.Add(md5, list);
        }
    }

    public void WriteToStream(SerializationWriter w)
    {
        w.Write(OsuVersion);
        w.Write(Beatmaps.Count);

        foreach (var kvp in Beatmaps)
        {
            w.Write(kvp.Key);
            w.Write(kvp.Value.Count);

            foreach (var replay in kvp.Value)
            {
                replay.WriteToStream(w);
            }
        }
    }
}
