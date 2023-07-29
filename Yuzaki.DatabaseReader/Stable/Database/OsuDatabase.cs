using System.Diagnostics;
using OsuDatabaseReader;
using OsuDatabaseReader.OsuElement;
using Yuzaki.DatabaseReader.Stable.OsuElement.Components.Beatmaps;
using Yuzaki.DatabaseReader.Stable.OsuElement.Serialization;

namespace Yuzaki.DatabaseReader.Stable.Database;

public class OsuDatabase : ISerializable
{
    public int OsuVersion;
    public int FolderCount;
    public bool AccountUnlocked;
    public DateTime AccountUnlockDate;
    public string AccountName;
    public List<BeatmapEntry> Beatmaps;
    public PlayerRank AccountRank;

    public static OsuDatabase Read(string path)
    {
        using var stream = File.OpenRead(path);
        return Read(stream);
    }

    public static OsuDatabase Read(Stream stream) {
        var db = new OsuDatabase();
        using var r = new SerializationReader(stream);
        db.ReadFromStream(r);
        return db;
    }

    public void ReadFromStream(SerializationReader r)
    {
        bool hasEntryLength = OsuVersion
            is >= OsuVersions.EntryLengthInOsuDatabaseMin
            and < OsuVersions.EntryLengthInOsuDatabaseMax;

        OsuVersion = r.ReadInt32();
        FolderCount = r.ReadInt32();
        AccountUnlocked = r.ReadBoolean();
        AccountUnlockDate = r.ReadDateTime();
        AccountName = r.ReadString();

        Beatmaps = new List<BeatmapEntry>();

        int length = r.ReadInt32();

        for (int i = 0; i < length; i++) {
            int currentIndex = (int)r.BaseStream.Position;
            int entryLength = 0;

            if (hasEntryLength)
                entryLength = r.ReadInt32();

            Beatmaps.Add(BeatmapEntry.ReadFromReader(r, OsuVersion));

            if (OsuVersion < OsuVersions.EntryLengthInOsuDatabaseMax && r.BaseStream.Position != currentIndex + entryLength + 4) {
                Debug.Fail($"Length doesn't match, {r.BaseStream.Position} instead of expected {currentIndex + entryLength + 4}");
            }
        }

        AccountRank = (PlayerRank)r.ReadByte();
    }

    public void WriteToStream(SerializationWriter w)
    {
        bool hasEntryLength = OsuVersion
            is >= OsuVersions.EntryLengthInOsuDatabaseMin
            and < OsuVersions.EntryLengthInOsuDatabaseMax;

        w.Write(OsuVersion);
        w.Write(FolderCount);
        w.Write(AccountUnlocked);
        w.Write(AccountUnlockDate);
        w.Write(AccountName);

        w.Write(Beatmaps.Count);

        foreach (var beatmap in Beatmaps)
        {
            if (hasEntryLength)
            {
                using var ms = new MemoryStream();
                using var w2 = new SerializationWriter(ms);
                beatmap.WriteToStream(w2);

                var bytes = ms.ToArray();
                w.Write(bytes.Length);
                w.WriteRaw(bytes);
            }
            else
            {
                beatmap.WriteToStream(w);
            }
        }

        w.Write((byte)AccountRank);
    }
}
