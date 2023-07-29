using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Player;
using Yuzaki.DatabaseReader.Stable.OsuElement.Serialization;

namespace Yuzaki.DatabaseReader.Stable.Database;

public class PresenceDatabase : ISerializable
{
    public int OsuVersion;
    public readonly List<PlayerPresence> Players = new();

    public static PresenceDatabase Read(string path)
    {
        using var stream = File.OpenRead(path);
        return Read(stream);
    }

    public static PresenceDatabase Read(Stream stream)
    {
        var db = new PresenceDatabase();
        using var r = new SerializationReader(stream);
        db.ReadFromStream(r);
        return db;
    }

    public void ReadFromStream(SerializationReader r)
    {
        OsuVersion = r.ReadInt32();
        int amount = r.ReadInt32();

        for (int i = 0; i < amount; i++)
            Players.Add(PlayerPresence.ReadFromReader(r));
    }

    public void WriteToStream(SerializationWriter w)
    {
        w.Write(OsuVersion);
        w.Write(Players.Count);

        foreach (var playerPresence in Players)
        {
            playerPresence.WriteToStream(w);
        }
    }
}
