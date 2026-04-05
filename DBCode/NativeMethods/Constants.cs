namespace DBCode {
   static partial class Program {
      internal static partial class NativeMethods {
#pragma warning disable IDE1006
#pragma warning disable SYSLIB1054
         #region Miscellaneous & Icon / Display APIs
         [DllImport("dwmapi.dll", PreserveSig = true)]
         public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out int pvAttribute,
            int cbAttribute);

         [DllImport("user32.dll")]
         public static extern bool GetIconInfo(IntPtr hIcon, out IconInfo piconinfo);

         [DllImport("user32.dll", SetLastError = true)]
         public static extern IntPtr CreateIconIndirect(ref IconInfo iconinfo);

         [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
         public static extern uint GetWindowModuleFileName(IntPtr hwnd, StringBuilder lpszFileName, uint cchFileNameMax);

         [DllImport("user32.dll", CharSet = CharSet.Unicode)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);
         #endregion

         #region Delegates
         public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
         #endregion

         #region Enums
         [Flags]
         public enum RecycleFlags : uint {
            SHERB_NOCONFIRMATION = 0x00000001, SHERB_NOPROGRESSUI = 0x00000002,
            SHERB_NOSOUND = 0x00000004
         }

         internal enum DpiType { Effective = 0, Angular = 1, Raw = 2 }

         public enum DWMWINDOWATTRIBUTE {
            DWMWA_NCRENDERING_ENABLED,
            DWMWA_NCRENDERING_POLICY,
            DWMWA_TRANSITIONS_FORCEDISABLED,
            DWMWA_ALLOW_NCPAINT,
            DWMWA_CAPTION_BUTTON_BOUNDS,
            DWMWA_NONCLIENT_RTL_LAYOUT,
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            DWMWA_FLIP3D_POLICY,
            DWMWA_EXTENDED_FRAME_BOUNDS,
            DWMWA_HAS_ICONIC_BITMAP,
            DWMWA_DISALLOW_PEEK,
            DWMWA_EXCLUDED_FROM_PEEK,
            DWMWA_CLOAK,
            DWMWA_CLOAKED,
            DWMWA_FREEZE_REPRESENTATION,
            DWMWA_LAST
         }

         public enum ShowWindowEnum {
            Hide = 0,
            ShowNormal = 1,
            ShowMinimized = 2,
            ShowMaximized = 3,
            ShowNormalNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActivate = 7,
            ShowNoActivate = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimized = 11
         };
         #endregion

         #region Constants & Static Fields
         public const uint GW_HWNDPREV = 3;
         public const int EM_LINESCROLL = 0x00B6;
         public const int FILE_SHARE_READ = 0x1;
         public const int FILE_SHARE_WRITE = 0x2;
         public const int FSCTL_DISMOUNT_VOLUME = 0x00090020;
         public const int FSCTL_LOCK_VOLUME = 0x00090018;
         public const int GWL_EXSTYLE = -20;
         public const int GWL_STYLE = -16;
         public const int IOCTL_STORAGE_EJECT_MEDIA = 0x2D4808;
         public const int IOCTL_STORAGE_MEDIA_REMOVAL = 0x002D4804;
         public const int MOUSEEVENTF_LEFTDOWN = 0x02;
         public const int MOUSEEVENTF_LEFTUP = 0x04;
         public const int OPEN_EXISTING = 3;
         public const int SB_ENDSCROLL = 8;
         public const int SB_LEFT = 6;
         public const int SB_LINEDOWN = 1;
         public const int SB_LINELEFT = 0;
         public const int SB_LINERIGHT = 1;
         public const int SB_LINEUP = 0;
         public const int SB_PAGEBOTTOM = 7;
         public const int SB_PAGEDOWN = 3;
         public const int SB_PAGELEFT = 2;
         public const int SB_PAGERIGHT = 3;
         public const int SB_PAGETOP = 6;
         public const int SB_PAGEUP = 2;
         public const int SB_RIGHT = 7;
         public const int SW_SHOW = 5;
         public const int WM_COPYDATA = 0x4A;
         public const int WM_LBUTTONDBLCLK = 0x203;
         public const int WM_PASTE = 0x302;
         public const int WM_SCROLL = 276;
         public const int WM_USER = 0x400;
         public const int WM_VSCROLL = 277;
         public const int WS_EX_LAYERED = 0x00080000;
         public const int WS_EX_NOACTIVATE = 0x08000000;
         public const int WS_EX_TOOLWINDOW = 0x00000080;
         public const int WS_VISIBLE = 0x10000000;
         public const uint GENERIC_READ = 0x80000000;
         public const uint GENERIC_WRITE = 0x40000000;
         public const uint GW_HWNDNEXT = 2;
         public const uint SEE_MASK_INVOKEIDLIST = 12;
         public const uint SWP_NOACTIVATE = 0x0010;
         public const uint SWP_NOMOVE = 0x0002;
         public const uint SWP_NOSIZE = 0x0001;
         public const uint TIMED_MESSAGEBOX_FLAGS = 0x00000000 | 0x00010000 | 0x00001000 | 0x00000030;
         public static bool gBackgroundWorkerFinished = false;
         public static bool gOffset = false;
         public static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);
         #endregion
#pragma warning restore SYSLIB1054
#pragma warning restore IDE1006
      }
   }
}
