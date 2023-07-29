using System.Text;

namespace Yuzaki.DatabaseReader.Stable.OsuElement.Serialization;

public class SerializationReader : BinaryReader
{
    public SerializationReader(Stream input) : base(input)
    {
    }

    public SerializationReader(Stream input, Encoding encoding) : base(input, encoding)
    {
    }

    public byte[] ReadBytes()
    {
        int count = ReadInt32();
        return count > 0 ? ReadBytes(count) : null;
    }

    public char[] ReadChars()
    {
        int count = ReadInt32();
        return count > 0 ? ReadChars(count) : null;
    }

    public override string ReadString()
    {
        switch (ReadByte())
        {
            case (byte)ByteTypes.Null:
                return null;
            case (byte)ByteTypes.String:
                return base.ReadString();
            default:
                throw new Exception($"Type byte is not {ByteTypes.Null} or {ByteTypes.String} (position: {BaseStream.Position})");
        }
    }

    public DateTime ReadDateTime() => new DateTime(ReadInt64(), DateTimeKind.Utc);

    public List<T> ReadSerializableList<T>() where T : ISerializable, new()
    {
        List<T> objList = new List<T>();
        int num = ReadInt32();
        for (int index = 0; index < num; ++index)
        {
            T obj = new T();
            obj.ReadFromStream(this);
            objList.Add(obj);
        }
        return objList;
    }

    public List<T> ReadList<T>()
    {
        List<T> objList = new List<T>();
        int num = ReadInt32();
        for (int index = 0; index < num; ++index)
        {
            objList.Add((T)ReadObject());
        }
        return objList;
    }

    public Dictionary<TKey, TValue> ReadDictionary<TKey, TValue>() where TKey : notnull
    {
        Dictionary<TKey, TValue> objList = new Dictionary<TKey, TValue>();
        int num = ReadInt32();
        for (int index = 0; index < num; ++index)
        {
            objList[(TKey)ReadObject()] = (TValue)ReadObject();
        }
        return objList;
    }

    public object ReadObject()
    {
        switch ((ByteTypes)ReadByte())
        {
            case ByteTypes.Null:
                return null;
            case ByteTypes.Bool:
                return ReadBoolean();
            case ByteTypes.Byte:
                return ReadByte();
            case ByteTypes.UShort:
                return ReadUInt16();
            case ByteTypes.UInt:
                return ReadUInt32();
            case ByteTypes.ULong:
                return ReadUInt64();
            case ByteTypes.SByte:
                return ReadSByte();
            case ByteTypes.Short:
                return ReadInt16();
            case ByteTypes.Int:
                return ReadInt32();
            case ByteTypes.Long:
                return ReadInt64();
            case ByteTypes.Char:
                return ReadChar();
            case ByteTypes.String:
                return base.ReadString();
            case ByteTypes.Float:
                return ReadSingle();
            case ByteTypes.Double:
                return ReadDouble();
            case ByteTypes.Decimal:
                return ReadDecimal();
            case ByteTypes.DateTime:
                return ReadDateTime();
            case ByteTypes.ByteArray:
                return ReadBytes();
            case ByteTypes.CharArray:
                return ReadChars();
            case ByteTypes.Unknown:
            case ByteTypes.Serializable:
            default:
                throw new NotImplementedException("Unknown or Serializable type is not implemented");
        }
    }
}
