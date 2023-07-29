using System.Text;

namespace Yuzaki.DatabaseReader.Stable.OsuElement.Serialization;

public class SerializationWriter : BinaryWriter
{
    public SerializationWriter(Stream input) : base(input)
    {
    }

    public SerializationWriter(Stream input, Encoding encoding) : base(input, encoding)
    {
    }

    public override void Write(string? str)
    {
        if (str == null)
        {
            Write((byte)ByteTypes.Null);
        }
        else
        {
            Write((byte)ByteTypes.String);
            base.Write(str);
        }
    }

    public override void Write(byte[]? bytes)
    {
        if (bytes == null)
        {
            Write(0);
        }
        else
        {
            Write(bytes.Length);
            if (bytes.Length > 0)
                Write(bytes);
        }
    }

    public void WriteRaw(byte[] bytes)
    {
        Write(bytes.Length);
        if (bytes.Length > 0)
            Write(bytes);
    }

    public override void Write(char[]? chars)
    {
        if (chars == null)
        {
            Write(-1);
        }
        else
        {
            Write(chars.Length);
            if (chars.Length > 0)
                base.Write(chars);
        }
    }

    public void WriteRaw(char[] chars) => base.Write(chars);

    public void Write(DateTime dateTime) => Write(dateTime.ToUniversalTime().Ticks);

    public void Write(ISerializable? serializable) => serializable?.WriteToStream(this);

    public void WriteSerializableList<T>(List<T>? list) where T : ISerializable
    {
        if (list == null)
        {
            Write(-1);
        }
        else
        {
            Write(list.Count);
            foreach (T obj in list)
                obj.WriteToStream(this);
        }
    }

    public void Write<T>(List<T?>? list)
    {
        if (list == null)
        {
            Write(-1);
        }
        else
        {
            Write(list.Count);
            foreach (T? obj in list)
                WriteObject(obj);
        }
    }

    public void Write<TKey, TValue>(Dictionary<TKey?, TValue>? dictionary)
    {
        if (dictionary == null)
        {
            Write(-1);
        }
        else
        {
            Write(dictionary.Count);
            foreach (KeyValuePair<TKey?, TValue> keyValuePair in dictionary)
            {
                WriteObject(keyValuePair.Key);
                WriteObject(keyValuePair.Value);
            }
        }
    }

    public void WriteObject(object? obj)
    {
        if (obj == null)
            Write((byte)ByteTypes.Null);
        else
            switch (obj)
            {
                case bool b:
                    Write((byte)ByteTypes.Bool);
                    Write(b);
                    break;
                case byte b:
                    Write((byte)ByteTypes.Byte);
                    Write(b);
                    break;
                case ushort us:
                    Write((byte)ByteTypes.UShort);
                    Write(us);
                    break;
                case uint ui:
                    Write((byte)ByteTypes.UInt);
                    Write(ui);
                    break;
                case ulong ul:
                    Write((byte)ByteTypes.ULong);
                    Write(ul);
                    break;
                case sbyte sb:
                    Write((byte)ByteTypes.SByte);
                    Write(sb);
                    break;
                case short s:
                    Write((byte)ByteTypes.Short);
                    Write(s);
                    break;
                case int i:
                    Write((byte)ByteTypes.Int);
                    Write(i);
                    break;
                case long l:
                    Write((byte)ByteTypes.Long);
                    Write(l);
                    break;
                case char c:
                    Write((byte)ByteTypes.Char);
                    Write(c);
                    break;
                case string s:
                    Write((byte)ByteTypes.String);
                    Write(s);
                    break;
                case float f:
                    Write((byte)ByteTypes.Float);
                    Write(f);
                    break;
                case double d:
                    Write((byte)ByteTypes.Double);
                    Write(d);
                    break;
                case decimal d:
                    Write((byte)ByteTypes.Decimal);
                    Write(d);
                    break;
                case DateTime dt:
                    Write((byte)ByteTypes.DateTime);
                    Write(dt);
                    break;
                case byte[] ba:
                    Write((byte)ByteTypes.ByteArray);
                    Write(ba);
                    break;
                case char[] ca:
                    Write((byte)ByteTypes.CharArray);
                    Write(ca);
                    break;
                default:
                    throw new NotImplementedException("Serialization of type " + obj.GetType() + " is not implemented.");
            }
    }
}
