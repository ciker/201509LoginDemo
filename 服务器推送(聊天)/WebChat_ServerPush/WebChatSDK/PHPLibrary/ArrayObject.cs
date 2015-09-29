using System;
using System.Collections.Generic;
using System.Text;

namespace WebChatSDK.PHPLibrary
{
    public class ArrayObject
    {
        public object Value { get; private set; }

        public ArrayObject(object s)
        {
            Value = s;
        }

        #region T to ArrayObject
        public static implicit operator ArrayObject(string s)
        {
            return new ArrayObject(s);
        }

        public static implicit operator ArrayObject(int s)
        {
            return new ArrayObject(s);
        }

        public static implicit operator ArrayObject(uint s)
        {
            return new ArrayObject(s);
        }

        public static implicit operator ArrayObject(long s)
        {
            return new ArrayObject(s);
        }

        public static implicit operator ArrayObject(ulong s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(float s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(double s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(bool s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(short s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(ushort s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(char s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(byte s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(sbyte s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(decimal s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(DateTime s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(Guid s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(TimeSpan s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(int? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(uint? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(long? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(ulong? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(float? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(double? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(bool? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(short? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(ushort? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(char? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(byte? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(sbyte? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(decimal? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(DateTime? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(Guid? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(TimeSpan? s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(byte[] s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(Uri s)
        {
            return new ArrayObject(s);
        }
        public static implicit operator ArrayObject(PHPArray s)
        {
            return new ArrayObject(s);
        }
        #endregion

        #region ArrayObject to T
        public static implicit operator string(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(string);
            }
            return obj.Value.ToString();
        }

        public static implicit operator int(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(int);
            }
            else if (obj.Value is int)
            {
                return (int)obj.Value;
            }
            else
            {
                int result;
                int.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }

        public static implicit operator uint(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(uint);
            }
            else if (obj.Value is uint)
            {
                return (uint)obj.Value;
            }
            else
            {
                uint result;
                uint.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }

        public static implicit operator long(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(long);
            }
            else if (obj.Value is long)
            {
                return (long)obj.Value;
            }
            else
            {
                long result;
                long.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }

        public static implicit operator ulong(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(ulong);
            }
            else if (obj.Value is ulong)
            {
                return (ulong)obj.Value;
            }
            else
            {
                ulong result;
                ulong.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }
        public static implicit operator float(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(float);
            }
            else if (obj.Value is float)
            {
                return (float)obj.Value;
            }
            else
            {
                float result;
                float.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }
        public static implicit operator double(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(double);
            }
            else if (obj.Value is double)
            {
                return (double)obj.Value;
            }
            else
            {
                double result;
                double.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }
        public static implicit operator bool(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(bool);
            }
            else if (obj.Value is bool)
            {
                return (bool)obj.Value;
            }
            else
            {
                if (string.IsNullOrEmpty(obj.Value.ToString()) || obj.Value.ToString().ToLower() == "false")
                {
                    return false;
                }
                int result;
                if (int.TryParse(obj.Value.ToString(), out result))
                {
                    return result > 0 ? true : false;
                }
                return false;
            }
        }
        public static implicit operator short(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(short);
            }
            else if (obj.Value is short)
            {
                return (short)obj.Value;
            }
            else
            {
                short result;
                short.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }
        public static implicit operator ushort(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(ushort);
            }
            else if (obj.Value is ushort)
            {
                return (ushort)obj.Value;
            }
            else
            {
                ushort result;
                ushort.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }
        public static implicit operator char(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(char);
            }
            else if (obj.Value is char)
            {
                return (char)obj.Value;
            }
            else
            {
                char result;
                char.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }
        public static implicit operator byte(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(byte);
            }
            else if (obj.Value is byte)
            {
                return (byte)obj.Value;
            }
            else
            {
                byte result;
                byte.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }
        public static implicit operator sbyte(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(sbyte);
            }
            else if (obj.Value is sbyte)
            {
                return (sbyte)obj.Value;
            }
            else
            {
                sbyte result;
                sbyte.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }
        public static implicit operator decimal(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(decimal);
            }
            else if (obj.Value is decimal)
            {
                return (decimal)obj.Value;
            }
            else
            {
                decimal result;
                decimal.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }
        public static implicit operator DateTime(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(DateTime);
            }
            else if (obj.Value is DateTime)
            {
                return (DateTime)obj.Value;
            }
            else
            {
                DateTime result;
                DateTime.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }
        public static implicit operator Guid(ArrayObject obj)
        {
            return obj.Get<Guid>();
        }
        public static implicit operator TimeSpan(ArrayObject obj)
        {
            if (obj.Value == null)
            {
                return default(TimeSpan);
            }
            else if (obj.Value is TimeSpan)
            {
                return (TimeSpan)obj.Value;
            }
            else
            {
                TimeSpan result;
                TimeSpan.TryParse(obj.Value.ToString(), out result);
                return result;
            }
        }
        public static implicit operator int?(ArrayObject obj)
        {
            return obj.Get<int?>();
        }
        public static implicit operator uint?(ArrayObject obj)
        {
            return obj.Get<uint?>();
        }
        public static implicit operator long?(ArrayObject obj)
        {
            return obj.Get<long?>();
        }
        public static implicit operator ulong?(ArrayObject obj)
        {
            return obj.Get<ulong?>();
        }
        public static implicit operator float?(ArrayObject obj)
        {
            return obj.Get<float?>();
        }
        public static implicit operator double?(ArrayObject obj)
        {
            return obj.Get<double?>();
        }
        public static implicit operator bool?(ArrayObject obj)
        {
            return obj.Get<bool?>();
        }
        public static implicit operator short?(ArrayObject obj)
        {
            return obj.Get<short?>();
        }
        public static implicit operator ushort?(ArrayObject obj)
        {
            return obj.Get<ushort?>();
        }
        public static implicit operator char?(ArrayObject obj)
        {
            return obj.Get<char?>();
        }
        public static implicit operator byte?(ArrayObject obj)
        {
            return obj.Get<byte?>();
        }
        public static implicit operator sbyte?(ArrayObject obj)
        {
            return obj.Get<sbyte?>();
        }
        public static implicit operator decimal?(ArrayObject obj)
        {
            return obj.Get<decimal?>();
        }
        public static implicit operator DateTime?(ArrayObject obj)
        {
            return obj.Get<DateTime?>();
        }
        public static implicit operator Guid?(ArrayObject obj)
        {
            return obj.Get<Guid?>();
        }
        public static implicit operator TimeSpan?(ArrayObject obj)
        {
            return obj.Get<TimeSpan?>();
        }
        public static implicit operator byte[](ArrayObject obj)
        {
            return obj.Get<byte[]>();
        }
        public static implicit operator Uri(ArrayObject obj)
        {
            return obj.Get<Uri>();
        }
        public static implicit operator PHPArray(ArrayObject obj)
        {
            return obj.Get<PHPArray>();
        }
        #endregion

        public T Get<T>()
        {
            if (this.Value != null && this.Value is T)
            {
                return (T)this.Value;
            }
            else
            {
                return default(T);
            }
        }
    }
}
