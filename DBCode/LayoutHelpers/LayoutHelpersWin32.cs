namespace DBCode {
   internal static partial class LayoutHelpers {
#pragma warning disable IDE1006

      public const int EM_LINESCROLL = 0x00B6;

      [DllImport("user32.dll", CharSet = CharSet.Auto)]
      public static extern nint SendMessage(nint hWnd, int msg, int wParam, int lParam);

      [DllImport("Shcore.dll")]
      private static extern int GetDpiForMonitor(nint hmonitor, DpiType dpiType, out uint dpiX, out uint dpiY);

      [DllImport("user32.dll")]
      private static extern nint MonitorFromPoint(Point pt, int flags);

      public static void GetDpi(Screen pScreen, DpiType pDpiType, out uint pODpiX, out uint pODpiY) {
         Point location = new Point(pScreen.Bounds.Left + 1, pScreen.Bounds.Top + 1);
         nint monitor = MonitorFromPoint(location, 2);
         _ = GetDpiForMonitor(monitor, pDpiType, out pODpiX, out pODpiY);
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct RECT {
         public int Left;
         public int Top;
         public int Right;
         public int Bottom;
      }

#pragma warning restore IDE1006
   }

   internal enum DpiType {
      Effective = 0,
      Angular = 1,
      Raw = 2
   }
}
