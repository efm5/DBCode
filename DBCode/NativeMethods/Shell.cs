namespace DBCode {
   static partial class Program {
#pragma warning disable IDE0079
#pragma warning disable IDE1006
#pragma warning disable SYSLIB1054
      internal static partial class NativeMethods {
         #region Shell & File System APIs
         [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

         [DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
         internal static extern int WNetGetConnection(
            [MarshalAs(UnmanagedType.LPWStr)] string localName,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder remoteName,
            ref int length);

         [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static extern bool GetDiskFreeSpaceEx(
            string lpDirectoryName,
            out ulong lpFreeBytesAvailable,
            out ulong lpTotalNumberOfBytes,
            out ulong lpTotalNumberOfFreeBytes);

         [DllImport("Shell32.dll")]
         internal static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

         [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
         internal static extern int SHGetFolderPath(
            IntPtr hwndOwner,
            int nFolder,
            IntPtr hToken,
            uint dwFlags,
            [Out] StringBuilder pszPath);

         [DllImport("shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
         internal static extern string SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
            uint dwFlags,
            IntPtr hToken = default);

         [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
         internal static extern uint SHEmptyRecycleBin(
            IntPtr hwnd,
            string pszRootPath,
            RecycleFlags dwFlags);

         #endregion

         [Flags]
         public enum RecycleFlags : uint {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000002,
            SHERB_NOSOUND = 0x00000004
         }
      }
#pragma warning restore IDE1006
#pragma warning restore SYSLIB1054
   }
}
