﻿using OsuDatabaseReader;
using OsuDatabaseReader.OsuElement;
using Yuzaki.DatabaseReader.Stable.OsuElement.Serialization;

namespace Yuzaki.DatabaseReader.Stable.OsuElement.Components.Beatmaps;

public class BeatmapEntry : ISerializable
{
    public string Artist, ArtistUnicode;
    public string Title, TitleUnicode;
    public string Creator;  //mapper
    public string Version;  //difficulty name
    public string AudioFileName;
    public string BeatmapChecksum;
    public string BeatmapFileName;
    public SubmissionStatus RankedStatus;
    public ushort CountHitCircles, CountSliders, CountSpinners;
    public DateTime LastModifiedTime;
    public float ApproachRate, CircleSize, HPDrainRate, OveralDifficulty;
    public double SliderVelocity;
    public Dictionary<Mods, double> DiffStarRatingStandard, DiffStarRatingTaiko, DiffStarRatingCtB, DiffStarRatingMania;
    public int DrainTimeSeconds;    // In seconds
    public int TotalTime;           // In milliseconds
    public int AudioPreviewTime;    // In milliseconds
    public List<TimingPoint> TimingPoints;
    public int BeatmapId, BeatmapSetId, ThreadId;
    public Ranking GradeStandard, GradeTaiko, GradeCtB, GradeMania;
    public short OffsetLocal;
    public float StackLeniency;
    public GameMode GameMode;
    public string SongSource, SongTags;
    public short OffsetOnline;
    public string TitleFont;
    public bool Unplayed;
    public DateTime LastPlayed;
    public bool IsOsz2;
    public string FolderName;
    public DateTime LastCheckAgainstOsuRepo;
    public bool IgnoreBeatmapSounds, IgnoreBeatmapSkin, DisableStoryBoard, DisableVideo, VisualOverride;
    public short OldUnknown1;   //unused
    public int LastEditTime;
    public byte ManiaScrollSpeed;

    private int _version;

    public static BeatmapEntry ReadFromReader(SerializationReader r, int version) {
        BeatmapEntry entry = new BeatmapEntry {
            _version = version,
        };
        entry.ReadFromStream(r);
        return entry;
    }

    public void ReadFromStream(SerializationReader r)
    {
        Artist = r.ReadString();
        if (_version >= OsuVersions.FirstOsz2)
            ArtistUnicode = r.ReadString();
        Title = r.ReadString();
        if (_version >= OsuVersions.FirstOsz2)
            TitleUnicode = r.ReadString();
        Creator = r.ReadString();
        Version = r.ReadString();
        AudioFileName = r.ReadString();
        BeatmapChecksum = r.ReadString();   //always 32 in length, so the 2 preceding bytes in the file are practically wasting space
        BeatmapFileName = r.ReadString();

        RankedStatus = (SubmissionStatus)r.ReadByte();
        CountHitCircles = r.ReadUInt16();
        CountSliders = r.ReadUInt16();
        CountSpinners = r.ReadUInt16();
        LastModifiedTime = r.ReadDateTime();

        if (_version >= OsuVersions.FloatDifficultyValues)
        {
            ApproachRate = r.ReadSingle();
            CircleSize = r.ReadSingle();
            HPDrainRate = r.ReadSingle();
            OveralDifficulty = r.ReadSingle();
        }
        else
        {
            ApproachRate = r.ReadByte();
            CircleSize = r.ReadByte();
            HPDrainRate = r.ReadByte();
            OveralDifficulty = r.ReadByte();
        }

        SliderVelocity = r.ReadDouble();

        if (_version >= OsuVersions.FloatDifficultyValues)
        {
            DiffStarRatingStandard = r.ReadDictionary<Mods, double>();
            DiffStarRatingTaiko = r.ReadDictionary<Mods, double>();
            DiffStarRatingCtB = r.ReadDictionary<Mods, double>();
            DiffStarRatingMania = r.ReadDictionary<Mods, double>();

            // TODO: there may be different reading behavior for versions before 20190204, 20200916, 20200504 and 20191024 here.
        }

        DrainTimeSeconds = r.ReadInt32();
        TotalTime = r.ReadInt32();
        AudioPreviewTime = r.ReadInt32();

        TimingPoints = r.ReadSerializableList<TimingPoint>();
        BeatmapId = r.ReadInt32();
        BeatmapSetId = r.ReadInt32();
        ThreadId = r.ReadInt32();

        GradeStandard = (Ranking)r.ReadByte();
        GradeTaiko = (Ranking)r.ReadByte();
        GradeCtB = (Ranking)r.ReadByte();
        GradeMania = (Ranking)r.ReadByte();

        OffsetLocal = r.ReadInt16();
        StackLeniency = r.ReadSingle();
        GameMode = (GameMode)r.ReadByte();

        SongSource = r.ReadString();
        SongTags = r.ReadString();
        OffsetOnline = r.ReadInt16();
        TitleFont = r.ReadString();
        Unplayed = r.ReadBoolean();
        LastPlayed = r.ReadDateTime();

        IsOsz2 = r.ReadBoolean();
        FolderName = r.ReadString();
        LastCheckAgainstOsuRepo = r.ReadDateTime();

        IgnoreBeatmapSounds = r.ReadBoolean();
        IgnoreBeatmapSkin = r.ReadBoolean();
        DisableStoryBoard = r.ReadBoolean();
        DisableVideo = r.ReadBoolean();
        VisualOverride = r.ReadBoolean();
        if (_version < OsuVersions.FloatDifficultyValues)
            OldUnknown1 = r.ReadInt16();
        LastEditTime = r.ReadInt32();
        ManiaScrollSpeed = r.ReadByte();
    }

    public void WriteToStream(SerializationWriter w)
    {
        w.Write(Artist);
            if (_version >= OsuVersions.FirstOsz2)
                w.Write(ArtistUnicode);
            w.Write(Title);
            if (_version >= OsuVersions.FirstOsz2)
                w.Write(TitleUnicode);
            w.Write(Creator);
            w.Write(Version);
            w.Write(AudioFileName);
            w.Write(BeatmapChecksum);
            w.Write(BeatmapFileName);

            w.Write((byte)RankedStatus);
            w.Write(CountHitCircles);
            w.Write(CountSliders);
            w.Write(CountSpinners);
            w.Write(LastModifiedTime);

            if (_version >= OsuVersions.FloatDifficultyValues)
            {
                w.Write(ApproachRate);
                w.Write(CircleSize);
                w.Write(HPDrainRate);
                w.Write(OveralDifficulty);
            }
            else
            {
                w.Write(ApproachRate);
                w.Write(CircleSize);
                w.Write(HPDrainRate);
                w.Write(OveralDifficulty);
            }

            w.Write(SliderVelocity);

            if (_version >= OsuVersions.FloatDifficultyValues)
            {
                static Dictionary<int, double> ConvertToWritableDictionary(IDictionary<Mods, double> dic)
                    => dic.ToDictionary(pair => (int) pair.Key, pair => pair.Value);

                w.Write(ConvertToWritableDictionary(DiffStarRatingStandard));
                w.Write(ConvertToWritableDictionary(DiffStarRatingTaiko));
                w.Write(ConvertToWritableDictionary(DiffStarRatingCtB));
                w.Write(ConvertToWritableDictionary(DiffStarRatingMania));

                // TODO: there may be different reading behavior for versions before 20190204, 20200916, 20200504 and 20191024 here.
            }

            w.Write(DrainTimeSeconds);
            w.Write(TotalTime);
            w.Write(AudioPreviewTime);

            w.WriteSerializableList(TimingPoints);
            w.Write(BeatmapId);
            w.Write(BeatmapSetId);
            w.Write(ThreadId);

            w.Write((byte)GradeStandard);
            w.Write((byte)GradeTaiko);
            w.Write((byte)GradeCtB);
            w.Write((byte)GradeMania);

            w.Write(OffsetLocal);
            w.Write(StackLeniency);
            w.Write((byte)GameMode);

            w.Write(SongSource);
            w.Write(SongTags);
            w.Write(OffsetOnline);
            w.Write(TitleFont);
            w.Write(Unplayed);
            w.Write(LastPlayed);

            w.Write(IsOsz2);
            w.Write(FolderName);
            w.Write(LastCheckAgainstOsuRepo);

            w.Write(IgnoreBeatmapSounds);
            w.Write(IgnoreBeatmapSkin);
            w.Write(DisableStoryBoard);
            w.Write(DisableVideo);
            w.Write(VisualOverride);
            if (_version < OsuVersions.FloatDifficultyValues)
                w.Write(OldUnknown1);
            w.Write(LastEditTime);
            w.Write(ManiaScrollSpeed);
    }
}
