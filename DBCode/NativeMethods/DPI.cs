namespace DBCode {
   static partial class Program {
      internal static partial class NativeMethods {
#pragma warning disable IDE1006
#pragma warning disable SYSLIB1054

         #region DPI & Monitor APIs

         [DllImport("Shcore.dll")]
         public static extern IntPtr GetDpiForMonitor(
            [In] IntPtr hmonitor,
            [In] DpiType dpiType,
            [Out] out uint dpiX,
            [Out] out uint dpiY);

         public enum DpiType {
            Effective = 0,
            Angular = 1,
            Raw = 2,
         }

         #endregion

#pragma warning restore SYSLIB1054
#pragma warning restore IDE1006
      }
   }
}
