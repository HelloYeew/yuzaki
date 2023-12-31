using System.Diagnostics;
using OsuDatabaseReader.OsuElement;
using Yuzaki.DatabaseReader.Stable.OsuElement.Serialization;

namespace Yuzaki.DatabaseReader.Stable.OsuElement.Components.Player
{
    public class PlayerPresence : ISerializable
    {
        public int PlayerId;
        public string PlayerName;
        public byte UtcOffset;  //need to substract 24 from this to be usable
        public byte CountryByte;
        public PlayerRank PlayerRank;
        public GameMode GameMode;
        public float Longitude, Latitude;   //position in the world
        public int GlobalRank;
        public DateTime LastUpdate;

        public static PlayerPresence ReadFromReader(SerializationReader r) {
            var p = new PlayerPresence();
            p.ReadFromStream(r);
            return p;
        }

        public void ReadFromStream(SerializationReader r)
        {
            PlayerId = r.ReadInt32();
            PlayerName = r.ReadString();
            UtcOffset = r.ReadByte();
            CountryByte = r.ReadByte();   //TODO: create Country enum

            byte b = r.ReadByte();
            PlayerRank = (PlayerRank)(b & 0b0001_1111);
            GameMode = (GameMode)((b & 0b1110_0000) >> 5);
            Debug.Assert((byte)GameMode <= 3, $"GameMode is byte {(byte)GameMode}, should be between 0 and 3");

            Longitude = r.ReadSingle();
            Latitude = r.ReadSingle();
            GlobalRank = r.ReadInt32();
            LastUpdate = r.ReadDateTime();
        }

        public void WriteToStream(SerializationWriter w)
        {
            w.Write(PlayerId);
            w.Write(PlayerName);
            w.Write(UtcOffset);
            w.Write(CountryByte);

            int rankPart = (byte)PlayerRank & 0b0001_1111;
            int modePart = ((byte)GameMode << 5) & 0b1110_0000;
            w.Write((byte)(rankPart | modePart));

            w.Write(Longitude);
            w.Write(Latitude);
            w.Write(GlobalRank);
            w.Write(LastUpdate);
        }
    }
}
