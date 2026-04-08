namespace DBCode {
   static partial class Program {
      internal static partial class NativeMethods {
         #region Constants
         // EM_  (Edit Control)
         public const int EM_LINESCROLL = 0x00B6;
         // FILE_  (File Share Flags)
         public const int FILE_SHARE_READ = 0x00000001,
                          FILE_SHARE_WRITE = 0x00000002;
         // FSCTL_  (File System Control Codes)
         public const int FSCTL_DISMOUNT_VOLUME = 0x00090020,
                          FSCTL_LOCK_VOLUME = 0x00090018;
         // GENERIC_  (Access Rights)
         public const uint GENERIC_READ = 0x80000000,
                           GENERIC_WRITE = 0x40000000;
         // GW_  (GetWindow Commands)
         public const uint GW_HWNDNEXT = 0x00000002,
                           GW_HWNDPREV = 0x00000003;
         // GWL_  (GetWindowLong Indexes)
         public const int GWL_EXSTYLE = unchecked((int)0xFFFFFFEC),   // -20
                          GWL_STYLE = unchecked((int)0xFFFFFFF0);   // -16
         // HWND_  (Special Window Handles)
         internal static readonly IntPtr HWND_BROADCAST = (IntPtr)0x0000FFFF;
         // IOCTL_  (Device I/O Control Codes)
         public const int IOCTL_STORAGE_EJECT_MEDIA = 0x002D4808,
                          IOCTL_STORAGE_MEDIA_REMOVAL = 0x002D4804;
         // MOUSEEVENTF_  (Mouse Events)
         public const int MOUSEEVENTF_LEFTDOWN = 0x00000002,
                          MOUSEEVENTF_LEFTUP = 0x00000004;
         // OPEN_  (File Creation Disposition)
         public const int OPEN_EXISTING = 0x00000003;
         // SB_  (Scroll Bar Commands)
         public const int SB_ENDSCROLL = 0x00000008,
                          SB_LEFT = 0x00000006,
                          SB_LINEDOWN = 0x00000001,
                          SB_LINELEFT = 0x00000000,
                          SB_LINERIGHT = 0x00000001,
                          SB_LINEUP = 0x00000000,
                          SB_PAGEBOTTOM = 0x00000007,
                          SB_PAGEDOWN = 0x00000003,
                          SB_PAGELEFT = 0x00000002,
                          SB_PAGERIGHT = 0x00000003,
                          SB_PAGETOP = 0x00000006,
                          SB_PAGEUP = 0x00000002,
                          SB_RIGHT = 0x00000007;
         // SEE_MASK_  (ShellExecuteEx Flags)
         public const uint SEE_MASK_INVOKEIDLIST = 0x0000000C;
         // SW_  (ShowWindow Flags)
         public const int SW_SHOW = 0x00000005;
         // SWP_  (SetWindowPos Flags)
         public const uint SWP_NOACTIVATE = 0x00000010,
                           SWP_NOMOVE = 0x00000002,
                           SWP_NOSIZE = 0x00000001;
         // TIMED_MESSAGEBOX_FLAGS  (Composite Flags)
         public const uint TIMED_MESSAGEBOX_FLAGS =
            0x00000000 | 0x00010000 | 0x00001000 | 0x00000030;
         // WM_  (Window Messages)
         public const int WM_COPYDATA = 0x0000004A,
                          WM_LBUTTONDBLCLK = 0x00000203,
                          WM_PASTE = 0x00000302,
                          WM_SCROLL = 0x00000114,
                          WM_USER = 0x00000400,
                          WM_VSCROLL = 0x00000115;
         // WS_ / WS_EX_  (Window Styles)
         public const int WS_EX_LAYERED = 0x00080000,
                          WS_EX_NOACTIVATE = 0x08000000,
                          WS_EX_TOOLWINDOW = 0x00000080,
                          WS_VISIBLE = 0x10000000;
         #endregion
      }
   }
}
