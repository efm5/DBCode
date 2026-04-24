namespace DBCode {
   static partial class Program {
#pragma warning disable IDE0079
#pragma warning disable IDE1006
#pragma warning disable SYSLIB1054
      internal static partial class NativeMethods {
         #region Device I/O, Volume Control & File Handles
         [DllImport("kernel32", CharSet = CharSet.Unicode)]
         internal static extern IntPtr CreateFile(
            string filename,
            uint desiredAccess,
            uint shareMode,
            IntPtr securityAttributes,
            int creationDisposition,
            int flagsAndAttributes,
            IntPtr templateFile);

         [DllImport("kernel32")]
         internal static extern bool DeviceIoControl(
            IntPtr deviceHandle,
            uint ioControlCode,
            IntPtr inBuffer,
            int inBufferSize,
            IntPtr outBuffer,
            int outBufferSize,
            ref int bytesReturned,
            IntPtr overlapped);

         [DllImport("kernel32")]
         internal static extern bool CloseHandle(IntPtr handle);

         #endregion
      }
#pragma warning restore IDE0079
#pragma warning restore IDE1006
#pragma warning restore SYSLIB1054
   }
}
