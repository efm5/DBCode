namespace DBCode {
   static partial class Program {
      internal static partial class NativeMethods {
#pragma warning disable IDE1006
#pragma warning disable SYSLIB1054

         #region Shell & File System APIs

         [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

         [DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
         public static extern int WNetGetConnection(
            [MarshalAs(UnmanagedType.LPWStr)] string localName,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder remoteName,
            ref int length);

         [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool GetDiskFreeSpaceEx(
            string lpDirectoryName,
            out ulong lpFreeBytesAvailable,
            out ulong lpTotalNumberOfBytes,
            out ulong lpTotalNumberOfFreeBytes);

         [DllImport("Shell32.dll")]
         public static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

         [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
         public static extern int SHGetFolderPath(
            IntPtr hwndOwner,
            int nFolder,
            IntPtr hToken,
            uint dwFlags,
            [Out] StringBuilder pszPath);

         [DllImport("shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
         public static extern string SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
            uint dwFlags,
            IntPtr hToken = default);

         [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
         public static extern uint SHEmptyRecycleBin(
            IntPtr hwnd,
            string pszRootPath,
            RecycleFlags dwFlags);

         #endregion

#pragma warning restore SYSLIB1054
#pragma warning restore IDE1006
      }
   }
}
