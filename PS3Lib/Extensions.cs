// ************************************************* //
//    --- Copyright (c) 2015 iMCS Productions ---    //
// ************************************************* //
//              PS3Lib v4 By FM|T iMCSx              //
//                                                   //
// Features v4.5 :                                   //
// - Support CCAPI v2.60+ C# by iMCSx.               //
// - Read/Write memory as 'double'.                  //
// - Read/Write memory as 'float' array.             //
// - Constructor overload for ArrayBuilder.          //
// - Some functions fixes.                           //
//                                                   //
// Credits : Enstone, Buc-ShoTz                      //
//                                                   //
// Follow me :                                       //
//                                                   //
// FrenchModdingTeam.com                             //
// Twitter.com/iMCSx                                 //
// Facebook.com/iMCSx                                //
//                                                   //
// ************************************************* //

using System.Text;

namespace PS3_MAPI
{
    public static class Extension
    {
        /// <summary>Read a signed byte.</summary>
        public static sbyte ReadSByte(this MAPI api, uint offset)
        {
            byte[] buffer = new byte[1];
            api.GetMem(offset, buffer);
            return (sbyte)buffer[0];
        }

        /// <summary>Read a byte a check if his value. This return a bool according the byte detected.</summary>
        public static bool ReadBool(this MAPI api, uint offset)
        {
            byte[] buffer = new byte[1];
            api.GetMem(offset, buffer);
            return buffer[0] != 0;
        }

        /// <summary>Read and return an integer 16 bits.</summary>
        public static short ReadInt16(this MAPI api, uint offset)
        {
            byte[] buffer = api.GetBytes(offset, 2);
            Array.Reverse(buffer, 0, 2);
            return BitConverter.ToInt16(buffer, 0);
        }

        /// <summary>Read and return an integer 32 bits.</summary>
        public static int ReadInt32(this MAPI api, uint offset)
        {
            byte[] buffer = api.GetBytes(offset, 4);
            Array.Reverse(buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>Read and return an integer 64 bits.</summary>
        public static long ReadInt64(this MAPI api, uint offset)
        {
            byte[] buffer = api.GetBytes(offset, 8);
            Array.Reverse(buffer, 0, 8);
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>Read and return a byte.</summary>
        public static byte ReadByte(this MAPI api, uint offset)
        {
            byte[] buffer = api.GetBytes(offset, 1);
            return buffer[0];
        }

        /// <summary>Read a string with a length to the first byte equal to an value null (0x00).</summary>
        public static byte[] ReadBytes(this MAPI api, uint offset, int length)
        {
            byte[] buffer = api.GetBytes(offset, (uint)length);
            return buffer;
        }

        /// <summary>Read and return an unsigned integer 16 bits.</summary>
        public static ushort ReadUInt16(this MAPI api, uint offset)
        {
            byte[] buffer = api.GetBytes(offset, 2);
            Array.Reverse(buffer, 0, 2);
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>Read and return an unsigned integer 32 bits.</summary>
        public static uint ReadUInt32(this MAPI api, uint offset)
        {
            byte[] buffer = api.GetBytes(offset, 4);
            Array.Reverse(buffer, 0, 4);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>Read and return an unsigned integer 64 bits.</summary>
        public static ulong ReadUInt64(this MAPI api, uint offset)
        {
            byte[] buffer = api.GetBytes(offset, 8);
            Array.Reverse(buffer, 0, 8);
            return BitConverter.ToUInt64(buffer, 0);
        }

        /// <summary>Read and return a Float.</summary>
        public static float ReadFloat(this MAPI api, uint offset)
        {
            byte[] buffer = api.GetBytes(offset, 4);
            Array.Reverse(buffer, 0, 4);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>Read and return an array of Floats.</summary>
        public static float[] ReadFloats(this MAPI api, uint offset, int arrayLength = 3)
        {
            float[] vec = new float[arrayLength];
            for (int i = 0; i < arrayLength; i++)
            {
                byte[] buffer = api.GetBytes(offset + ((uint)i*4), 4);
                Array.Reverse(buffer, 0, 4);
                vec[i] = BitConverter.ToSingle(buffer, 0);
            }
            return vec;
        }

        /// <summary>Read and return a Double.</summary>
        public static double ReadDouble(this MAPI api, uint offset)
        {
            byte[] buffer = api.GetBytes(offset, 8);
            Array.Reverse(buffer, 0, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>Read a string very fast by buffer and stop only when a byte null is detected (0x00).</summary>
        public static string ReadString(this MAPI api, uint offset)
        {
            int blocksize = 40;
            int scalesize = 0;
            string str = string.Empty;

            while (!str.Contains('\0'))
            {
                byte[] buffer = api.ReadBytes(offset + (uint)scalesize, blocksize);
                str += Encoding.UTF8.GetString(buffer);
                scalesize += blocksize;
            }

            return str.Substring(0, str.IndexOf('\0'));
        }

        /// <summary>Write a signed byte.</summary>
        public static void WriteSByte(this MAPI api, uint offset, sbyte input)
        {
            byte[] buff = new byte[1];
            buff[0] = (byte)input;
            api.SetMem(offset, buff);
        }

        /// <summary>Write a boolean.</summary>
        public static void WriteBool(this MAPI api, uint offset, bool input)
        {
            byte[] buff = new byte[1];
            buff[0] = input ? (byte)1 : (byte)0;
            api.SetMem(offset, buff);
        }

        /// <summary>Write an interger 16 bits.</summary>
        public static void WriteInt16(this MAPI api, uint offset, short input)
        {
            byte[] buff = new byte[2];
            BitConverter.GetBytes(input).CopyTo(buff, 0);
            Array.Reverse(buff, 0, 2);
            api.SetMem(offset, buff);
        }

        /// <summary>Write an integer 32 bits.</summary>
        public static void WriteInt32(this MAPI api, uint offset, int input)
        {
            byte[] buff = new byte[4];
            BitConverter.GetBytes(input).CopyTo(buff, 0);
            Array.Reverse(buff, 0, 4);
            api.SetMem(offset, buff);
        }

        /// <summary>Write an integer 64 bits.</summary>
        public static void WriteInt64(this MAPI api, uint offset, long input)
        {
            byte[] buff = new byte[8];
            BitConverter.GetBytes(input).CopyTo(buff, 0);
            Array.Reverse(buff, 0, 8);
            api.SetMem(offset, buff);
        }

        /// <summary>Write a byte.</summary>
        public static void WriteByte(this MAPI api, uint offset, byte input)
        {
            byte[] buff = new byte[1];
            buff[0] = input;
            api.SetMem(offset, buff);
        }

        /// <summary>Write a byte array.</summary>
        public static void WriteBytes(this MAPI api, uint offset, byte[] input)
        {
            byte[] buff = input;
            api.SetMem(offset, buff);
        }

        /// <summary>Write a string.</summary>
        public static void WriteString(this MAPI api, uint offset, string input)
        {
            byte[] buff = Encoding.UTF8.GetBytes(input);
            Array.Resize(ref buff, buff.Length + 1);
            api.SetMem(offset, buff);
        }

        /// <summary>Write an unsigned integer 16 bits.</summary>
        public static void WriteUInt16(this MAPI api, uint offset, ushort input)
        {
            byte[] buff = new byte[2];
            BitConverter.GetBytes(input).CopyTo(buff, 0);
            Array.Reverse(buff, 0, 2);
            api.SetMem(offset, buff);
        }

        /// <summary>Write an unsigned integer 32 bits.</summary>
        public static void WriteUInt32(this MAPI api, uint offset, uint input)
        {
            byte[] buff = new byte[4];
            BitConverter.GetBytes(input).CopyTo(buff, 0);
            Array.Reverse(buff, 0, 4);
            api.SetMem(offset, buff);
        }

        /// <summary>Write an unsigned integer 64 bits.</summary>
        public static void WriteUInt64(this MAPI api, uint offset, ulong input)
        {
            byte[] buff = new byte[8];
            BitConverter.GetBytes(input).CopyTo(buff, 0);
            Array.Reverse(buff, 0, 8);
            api.SetMem(offset, buff);
        }

        /// <summary>Write a Float.</summary>
        public static void WriteFloat(this MAPI api, uint offset, float input)
        {
            byte[] buff = new byte[4];
            BitConverter.GetBytes(input).CopyTo(buff, 0);
            Array.Reverse(buff, 0, 4);
            api.SetMem(offset, buff);
        }

        /// <summary>Write an array of Floats.</summary>
        public static void WriteFloats(this MAPI api, uint offset, float[] input)
        {
            byte[] buff = new byte[4];
            for (int i = 0; i < input.Length; i++)
            {
                BitConverter.GetBytes(input[i]).CopyTo(buff, 0);
                Array.Reverse(buff, 0, 4);
                api.SetMem(offset+((uint)i*4), buff);
            }
        }

        /// <summary>Write a double.</summary>
        public static void WriteDouble(this MAPI api, uint offset, double input)
        {
            byte[] buff = new byte[8];
            BitConverter.GetBytes(input).CopyTo(buff, 0);
            Array.Reverse(buff, 0, 8);
            api.SetMem(offset, buff);
        }

        private static void SetMem(this MAPI api, uint Address, byte[] buffer)
        {
            api.Process.Memory.Set(api.Process.Process_Pid, Address, buffer);
        }

        private static void GetMem(this MAPI api, uint offset, byte[] buffer)
        {
            api.Process.Memory.Get(api.Process.Process_Pid, offset, buffer);
        }

        private static byte[] GetBytes(this MAPI api, uint offset, uint length)
        {
            byte[] buffer = new byte[length];
            api.Process.Memory.Get(api.Process.Process_Pid, offset, buffer);
            return buffer;
        }
    }
}