using System;
using System.Runtime.InteropServices;

namespace PS3Lib
{
    public static class Native
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
    }
}
