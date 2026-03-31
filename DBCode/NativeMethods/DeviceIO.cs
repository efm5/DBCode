namespace DBCode {
   static partial class Program {
      internal static partial class NativeMethods {
#pragma warning disable IDE1006
#pragma warning disable SYSLIB1054

         #region Device I/O, Volume Control & File Handles

         [DllImport("kernel32", CharSet = CharSet.Unicode)]
         public static extern IntPtr CreateFile(
            string filename,
            uint desiredAccess,
            uint shareMode,
            IntPtr securityAttributes,
            int creationDisposition,
            int flagsAndAttributes,
            IntPtr templateFile);

         [DllImport("kernel32")]
         public static extern bool DeviceIoControl(
            IntPtr deviceHandle,
            uint ioControlCode,
            IntPtr inBuffer,
            int inBufferSize,
            IntPtr outBuffer,
            int outBufferSize,
            ref int bytesReturned,
            IntPtr overlapped);

         [DllImport("kernel32")]
         public static extern bool CloseHandle(IntPtr handle);

         #endregion

#pragma warning restore SYSLIB1054
#pragma warning restore IDE1006
      }
   }
}
