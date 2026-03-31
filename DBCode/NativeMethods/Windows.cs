namespace DBCode {
   static partial class Program {
      internal static partial class NativeMethods {
#pragma warning disable IDE1006
#pragma warning disable SYSLIB1054

         #region Window APIs

         [DllImport("user32.dll")]
         public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

         [DllImport("user32.dll", SetLastError = true)]
         public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

         [DllImport("user32.dll", SetLastError = true)]
         public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

         [DllImport("dwmapi.dll")]
         public static extern int DwmGetWindowAttribute(IntPtr hwnd, int fileAttributes, out RECT ptr, int size);

         [DllImport("user32.dll")]
         public static extern IntPtr WindowFromPoint(Point pnt);

         [DllImport("User32.dll", CharSet = CharSet.Unicode)]
         public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

         [DllImport("user32.dll")]
         public static extern IntPtr GetForegroundWindow();

         [DllImport("user32.dll", CharSet = CharSet.Unicode)]
         public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

         [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
         public static extern int GetWindowTextLength(IntPtr hWnd);

         [DllImport("user32.dll", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

         [DllImport("user32.dll", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool IsIconic(IntPtr hWnd);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool IsWindow(IntPtr hWnd);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool IsWindowVisible(IntPtr hWnd);

         [DllImport("user32.dll", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X,
            int Y,
            int cx,
            int cy,
            uint uFlags);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

         [DllImport("User32.dll")]
         public static extern IntPtr MonitorFromPoint([In] Point pt, [In] uint dwFlags);

         #endregion

#pragma warning restore SYSLIB1054
#pragma warning restore IDE1006
      }
   }
}
