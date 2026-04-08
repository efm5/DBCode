namespace DBCode {
   static partial class Program {
      internal static partial class NativeMethods {
#pragma warning disable IDE1006
#pragma warning disable SYSLIB1054

         #region Messaging, Input & Window Interaction

         [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
         internal static extern int MessageBoxTimeout(
            IntPtr hWnd,
            string lpText,
            string lpCaption,
            uint uType,
            short wLanguageId,
            int dwMilliseconds);

         [DllImport("user32")]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static extern bool PostMessage(
            IntPtr hwnd,
            int msg,
            IntPtr wparam,
            IntPtr lparam);

         [DllImport("User32.dll", EntryPoint = "PostMessage")]
         internal static extern int PostMessage(
            IntPtr hWnd,
            int Msg,
            int wParam,
            ref COPYDATASTRUCT lParam);

         [DllImport("user32", CharSet = CharSet.Unicode)]
         internal static extern int RegisterWindowMessage(string message);

         [DllImport("User32.dll", CharSet = CharSet.Unicode)]
         internal static extern IntPtr SendMessage(
            IntPtr hWnd,
            int msg,
            int wParam,
            int lParam);

         [DllImport("User32.dll", EntryPoint = "SendMessage")]
         internal static extern int SendMessage(
            IntPtr hWnd,
            int Msg,
            int wParam,
            ref COPYDATASTRUCT lParam);

         [DllImport("user32.dll", SetLastError = true)]
         internal static extern IntPtr SetActiveWindow(IntPtr hWnd);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static extern bool SetCursorPos(int X, int Y);

         [DllImport("user32.dll")]
         internal static extern void mouse_event(
            int dwFlags,
            int dx,
            int dy,
            int cButtons,
            int dwExtraInfo);

         [DllImport("User32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static extern bool SetForegroundWindow(IntPtr hwnd);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static extern bool SetProcessDPIAware();

         #endregion

#pragma warning restore SYSLIB1054
#pragma warning restore IDE1006
      }
   }
}
