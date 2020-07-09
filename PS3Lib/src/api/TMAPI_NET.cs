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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PS3Lib.NET
{
    public class PS3TMAPI
    {
        private class ScopedGlobalHeapPtr
        {
            private IntPtr m_intPtr = IntPtr.Zero;

            public ScopedGlobalHeapPtr(IntPtr intPtr)
            {
                this.m_intPtr = intPtr;
            }

            ~ScopedGlobalHeapPtr()
            {
                if (this.m_intPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(this.m_intPtr);
                }
            }

            public IntPtr Get()
            {
                return this.m_intPtr;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TCPIPConnectProperties
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0xff)]
            public string IPAddress;
            public uint Port;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct TargetInfoPriv
        {
            public TargetInfoFlag Flags;
            public int Target;
            public IntPtr Name;
            public IntPtr Type;
            public IntPtr Info;
            public IntPtr HomeDir;
            public IntPtr FSDir;
            public BootParameter Boot;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TargetInfo
        {
            public TargetInfoFlag Flags;
            public int Target;
            public string Name;
            public string Type;
            public string Info;
            public string HomeDir;
            public string FSDir;
            public BootParameter Boot;
        }

        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3InitTargetComms", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT InitTargetCommsX64();
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3InitTargetComms", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT InitTargetCommsX86();
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3PowerOn", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT PowerOnX64(int target);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3PowerOn", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT PowerOnX86(int target);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3PowerOff", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT PowerOffX64(int target, uint force);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3PowerOff", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT PowerOffX86(int target, uint force);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Connect", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT ConnectX64(int target, string application);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Connect", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT ConnectX86(int target, string application);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetConnectionInfo", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT GetConnectionInfoX64(int target, IntPtr connectProperties);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetConnectionInfo", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT GetConnectionInfoX86(int target, IntPtr connectProperties);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetConnectStatus", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT GetConnectStatusX64(int target, out uint status, out IntPtr usage);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetConnectStatus", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT GetConnectStatusX86(int target, out uint status, out IntPtr usage);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MultiByteToWideChar(int codepage, int flags, IntPtr utf8, int utf8len, StringBuilder buffer, int buflen);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessList", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT GetProcessListX64(int target, ref uint count, IntPtr processIdArray);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessList", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT GetProcessListX86(int target, ref uint count, IntPtr processIdArray);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessContinue", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT ProcessContinueX64(int target, uint processId);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessContinue", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT ProcessContinueX86(int target, uint processId);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessAttach", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT ProcessAttachX64(int target, uint unitId, uint processId);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessAttach", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT ProcessAttachX86(int target, uint unitId, uint processId);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessGetMemory", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT ProcessGetMemoryX64(int target, UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessGetMemory", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT ProcessGetMemoryX86(int target, UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetTargetFromName", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT GetTargetFromNameX64(IntPtr name, out int target);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetTargetFromName", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT GetTargetFromNameX86(IntPtr name, out int target);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Reset", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT ResetX64(int target, ulong resetParameter);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Reset", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT ResetX86(int target, ulong resetParameter);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessSetMemory", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT ProcessSetMemoryX64(int target, UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessSetMemory", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT ProcessSetMemoryX86(int target, UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetTargetInfo", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT GetTargetInfoX64(ref TargetInfoPriv targetInfoPriv);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetTargetInfo", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT GetTargetInfoX86(ref TargetInfoPriv targetInfoPriv);
        [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Disconnect", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT DisconnectX64(int target);
        [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Disconnect", CallingConvention = CallingConvention.Cdecl)]
        private static extern SNRESULT DisconnectX86(int target);
        private static bool Is32Bit()
        {
            return (IntPtr.Size == 4);
        }

        public static bool FAILED(SNRESULT res)
        {
            return !SUCCEEDED(res);
        }

        public static bool SUCCEEDED(SNRESULT res)
        {
            return (res >= SNRESULT.SN_S_OK);
        }

        private static IntPtr AllocUtf8FromString(string wcharString)
        {
            if (wcharString == null)
            {
                return IntPtr.Zero;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(wcharString);
            IntPtr destination = Marshal.AllocHGlobal((int)(bytes.Length + 1));
            Marshal.Copy(bytes, 0, destination, bytes.Length);
            Marshal.WriteByte((IntPtr)(destination.ToInt64() + bytes.Length), 0);
            return destination;
        }

        public static string Utf8ToString(IntPtr utf8, uint maxLength)
        {
            int len = MultiByteToWideChar(65001, 0, utf8, -1, null, 0);
            if (len == 0) throw new System.ComponentModel.Win32Exception();
            var buf = new StringBuilder(len);
            len = MultiByteToWideChar(65001, 0, utf8, -1, buf, len);
            return buf.ToString();
        }

        private static IntPtr ReadDataFromUnmanagedIncPtr<T>(IntPtr unmanagedBuf, ref T storage)
        {
            storage = (T)Marshal.PtrToStructure(unmanagedBuf, typeof(T));
            return new IntPtr(unmanagedBuf.ToInt64() + Marshal.SizeOf((T)storage));
        }

        public static SNRESULT InitTargetComms()
        {
            if (!Is32Bit())
            {
                return InitTargetCommsX64();
            }
            return InitTargetCommsX86();
        }

        public static SNRESULT Connect(int target, string application)
        {
            if (!Is32Bit())
            {
                return ConnectX64(target, application);
            }
            return ConnectX86(target, application);
        }

        public static SNRESULT PowerOn(int target)
        {
            if (!Is32Bit())
            {
                return PowerOnX64(target);
            }
            return PowerOnX86(target);
        }

        public static SNRESULT PowerOff(int target, bool bForce)
        {
            uint force = bForce ? (uint)1 : 0;
            if (!Is32Bit())
            {
                return PowerOffX64(target, force);
            }
            return PowerOffX86(target, force);
        }

        public static SNRESULT GetProcessList(int target, out uint[] processIDs)
        {
            processIDs = null;
            uint count = 0;
            SNRESULT res = Is32Bit() ? GetProcessListX86(target, ref count, IntPtr.Zero) : GetProcessListX64(target, ref count, IntPtr.Zero);
            if (!FAILED(res))
            {
                ScopedGlobalHeapPtr ptr = new ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)(4 * count)));
                res = Is32Bit() ? GetProcessListX86(target, ref count, ptr.Get()) : GetProcessListX64(target, ref count, ptr.Get());
                if (FAILED(res))
                {
                    return res;
                }
                IntPtr unmanagedBuf = ptr.Get();
                processIDs = new uint[count];
                for (uint i = 0; i < count; i++)
                {
                    unmanagedBuf = ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref processIDs[i]);
                }
            }
            return res;
        }

        public static SNRESULT ProcessAttach(int target, UnitType unit, uint processID)
        {
            if (!Is32Bit())
            {
                return ProcessAttachX64(target, (uint)unit, processID);
            }
            return ProcessAttachX86(target, (uint)unit, processID);
        }

        public static SNRESULT ProcessContinue(int target, uint processID)
        {
            if (!Is32Bit())
            {
                return ProcessContinueX64(target, processID);
            }
            return ProcessContinueX86(target, processID);
        }

        public static SNRESULT GetTargetInfo(ref TargetInfo targetInfo)
        {
            TargetInfoPriv targetInfoPriv = new TargetInfoPriv
            {
                Flags = targetInfo.Flags,
                Target = targetInfo.Target
            };
            SNRESULT res = Is32Bit() ? GetTargetInfoX86(ref targetInfoPriv) : GetTargetInfoX64(ref targetInfoPriv);
            if (!FAILED(res))
            {
                targetInfo.Flags = targetInfoPriv.Flags;
                targetInfo.Target = targetInfoPriv.Target;
                targetInfo.Name = Utf8ToString(targetInfoPriv.Name, uint.MaxValue);
                targetInfo.Type = Utf8ToString(targetInfoPriv.Type, uint.MaxValue);
                targetInfo.Info = Utf8ToString(targetInfoPriv.Info, uint.MaxValue);
                targetInfo.HomeDir = Utf8ToString(targetInfoPriv.HomeDir, uint.MaxValue);
                targetInfo.FSDir = Utf8ToString(targetInfoPriv.FSDir, uint.MaxValue);
                targetInfo.Boot = targetInfoPriv.Boot;
            }
            return res;
        }

        public static SNRESULT GetTargetFromName(string name, out int target)
        {
            ScopedGlobalHeapPtr ptr = new ScopedGlobalHeapPtr(AllocUtf8FromString(name));
            if (!Is32Bit())
            {
                return GetTargetFromNameX64(ptr.Get(), out target);
            }
            return GetTargetFromNameX86(ptr.Get(), out target);
        }

        public static SNRESULT GetConnectionInfo(int target, out TCPIPConnectProperties connectProperties)
        {
            connectProperties = null;
            ScopedGlobalHeapPtr ptr = new ScopedGlobalHeapPtr(Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TCPIPConnectProperties))));
            SNRESULT res = Is32Bit() ? GetConnectionInfoX86(target, ptr.Get()) : GetConnectionInfoX64(target, ptr.Get());
            if (SUCCEEDED(res))
            {
                connectProperties = new TCPIPConnectProperties();
                Marshal.PtrToStructure(ptr.Get(), connectProperties);
            }
            return res;
        }

        public static SNRESULT GetConnectStatus(int target, out ConnectStatus status, out string usage)
        {
            IntPtr ptr;
            uint num;
            SNRESULT snresult = Is32Bit() ? GetConnectStatusX86(target, out num, out ptr) : GetConnectStatusX64(target, out num, out ptr);
            status = (ConnectStatus)num;
            usage = Utf8ToString(ptr, uint.MaxValue);
            return snresult;
        }

        public static SNRESULT Reset(int target, ResetParameter resetParameter)
        {
            if (!Is32Bit())
            {
                return ResetX64(target, (ulong)resetParameter);
            }
            return ResetX86(target, (ulong)resetParameter);
        }

        public static SNRESULT ProcessGetMemory(int target, UnitType unit, uint processID, ulong threadID, ulong address, ref byte[] buffer)
        {
            if (!Is32Bit())
            {
                return ProcessGetMemoryX64(target, unit, processID, threadID, address, buffer.Length, buffer);
            }
            return ProcessGetMemoryX86(target, unit, processID, threadID, address, buffer.Length, buffer);
        }

        public static SNRESULT ProcessSetMemory(int target, UnitType unit, uint processID, ulong threadID, ulong address, byte[] buffer)
        {
            if (!Is32Bit())
            {
                return ProcessSetMemoryX64(target, unit, processID, threadID, address, buffer.Length, buffer);
            }
            return ProcessSetMemoryX86(target, unit, processID, threadID, address, buffer.Length, buffer);
        }

        public static SNRESULT Disconnect(int target)
        {
            if (!Is32Bit())
            {
                return DisconnectX64(target);
            }
            return DisconnectX86(target);
        }
    }
}
