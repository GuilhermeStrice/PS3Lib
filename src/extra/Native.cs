namespace PS3Lib
{
    public static class Native
    {
        #region Win32
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int MultiByteToWideChar(int codepage, int flags, IntPtr utf8, int utf8len, StringBuilder buffer, int buflen);
        #endregion
    }
}
