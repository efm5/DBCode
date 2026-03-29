namespace DBCode {
   static partial class Program {
      internal static class NativeMethods {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SYSLIB1054 // We intentionally keep DllImport for stability
         #region imports
         [DllImport("user32.dll")]
         public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

         [DllImport("user32.dll", SetLastError = true)]
         public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

         [DllImport("user32.dll", SetLastError = true)]
         public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

         [DllImport("dwmapi.dll", PreserveSig = true)]
         public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out int pvAttribute, int cbAttribute);

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
         public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName, out ulong lpFreeBytesAvailable,
            out ulong lpTotalNumberOfBytes, out ulong lpTotalNumberOfFreeBytes);

         [DllImport("user32.dll")]
         public static extern IntPtr WindowFromPoint(Point pnt);

         [DllImport("user32.dll")]
         public static extern bool GetIconInfo(IntPtr hIcon, out IconInfo piconinfo);

         [DllImport("user32.dll", SetLastError = true)]
         public static extern IntPtr CreateIconIndirect(ref IconInfo iconinfo);

         [DllImport("Shell32.dll")]
         public static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

         [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
         public static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, uint dwFlags, [Out] StringBuilder pszPath);

         [DllImport("shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
         public static extern string SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken = default);

         [ComImport]
         [Guid("00021401-0000-0000-C000-000000000046")]
         internal class ShellLink { }

         [ComImport]
         [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
         [Guid("000214F9-0000-0000-C000-000000000046")]
         internal interface IShellLink {
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out short pwHotkey);
            void SetHotkey(short wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, int fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
         }

         [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
         public static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

         [DllImport("kernel32", CharSet = CharSet.Unicode)]
         public static extern IntPtr CreateFile(string filename, uint desiredAccess, uint shareMode, IntPtr securityAttributes, int creationDisposition, int flagsAndAttributes,
            IntPtr templateFile);

         [DllImport("kernel32")]
         public static extern bool DeviceIoControl(IntPtr deviceHandle, uint ioControlCode, IntPtr inBuffer, int inBufferSize, IntPtr outBuffer, int outBufferSize,
            ref int bytesReturned, IntPtr overlapped);

         [DllImport("kernel32")]
         public static extern bool CloseHandle(IntPtr handle);

         [DllImport("User32.dll")]
         public static extern IntPtr MonitorFromPoint([In] Point pt, [In] uint dwFlags);

         [DllImport("Shcore.dll")]
         public static extern IntPtr GetDpiForMonitor([In] IntPtr hmonitor, [In] DpiType dpiType, [Out] out uint dpiX, [Out] out uint dpiY);

         [DllImport("dwmapi.dll")]
         public static extern int DwmGetWindowAttribute(IntPtr hwnd, int fileAttributes, out RECT ptr, int size);

         [DllImport("user32.dll", CharSet = CharSet.Unicode)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

         [DllImport("User32.dll", CharSet = CharSet.Unicode)]
         public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

         [DllImport("user32.dll")]
         public static extern IntPtr GetForegroundWindow();

         [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
         public static extern uint GetWindowModuleFileName(IntPtr hwnd, StringBuilder lpszFileName, uint cchFileNameMax);

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

         [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
         public static extern int MessageBoxTimeout(IntPtr hWnd, string lpText, string lpCaption, uint uType, short wLanguageId, int dwMilliseconds);

         [DllImport("user32")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

         [DllImport("User32.dll", EntryPoint = "PostMessage")]
         public static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

         [DllImport("user32", CharSet = CharSet.Unicode)]
         public static extern int RegisterWindowMessage(string message);

         [DllImport("User32.dll", CharSet = CharSet.Unicode)]
         public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

         [DllImport("User32.dll", EntryPoint = "SendMessage")]
         public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

         [DllImport("user32.dll", SetLastError = true)]
         public static extern IntPtr SetActiveWindow(IntPtr hWnd);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool SetCursorPos(int X, int Y);

         [DllImport("user32.dll")]
         public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

         [DllImport("User32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool SetForegroundWindow(IntPtr hwnd);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool SetProcessDPIAware();

         [DllImport("user32.dll", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

         [DllImport("user32.dll")]
         [return: MarshalAs(UnmanagedType.Bool)]
         public static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);
         #endregion

         #region enumerators
         [Flags]
         public enum RecycleFlags : uint {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000002,
            SHERB_NOSOUND = 0x00000004
         }

         public enum DpiType {
            Effective = 0,
            Angular = 1,
            Raw = 2,
         }

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

         #region structures
         [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
         public struct COPYDATASTRUCT {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpData;
         }

         [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
         public struct SHELLEXECUTEINFO {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpVerb;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpFile;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpParameters;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpDirectory;
            public int nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpClass;
            public IntPtr hkeyClass;
            public uint dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
         }

         [StructLayout(LayoutKind.Sequential)]
         public struct IconInfo {
            [MarshalAs(UnmanagedType.Bool)]
            public bool IsIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr MaskBitmap;
            public IntPtr ColorBitmap;
         };

         [StructLayout(LayoutKind.Sequential)]
         public struct WINDOWPLACEMENT {
            [Flags]
            public enum Flags : uint {
               WPF_ASYNCWINDOWPLACEMENT = 0x0004,
               WPF_RESTORETOMAXIMIZED = 0x0002,
               WPF_SETMINPOSITION = 0x0001
            }
            public uint length;
            public Flags flags;
            public uint showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public RECT rcNormalPosition;
         }

         [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
         public struct DEVMODE {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
         }

         [StructLayout(LayoutKind.Sequential)]
         public struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public readonly int Width() { return (Right - Left); }
            public readonly int Height() { return (Bottom - Top); }
         }
         #endregion

         #region Fields (variables)
         public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

         public const uint GW_HWNDPREV = 3;
         public const uint GW_HWNDNEXT = 2;

         public const int GWL_STYLE = -16;
         public const int GWL_EXSTYLE = -20;

         public const int WS_VISIBLE = 0x10000000;
         public const int WS_EX_TOOLWINDOW = 0x00000080;
         public const int WS_EX_NOACTIVATE = 0x08000000;
         public const int WS_EX_LAYERED = 0x00080000;

         public const uint SWP_NOMOVE = 0x0002;
         public const uint SWP_NOSIZE = 0x0001;
         public const uint SWP_NOACTIVATE = 0x0010;

         public const uint TIMED_MESSAGEBOX_FLAGS = 0x00000000 | 0x00010000 | 0x00001000 | 0x00000030;
         public static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);
         public static bool gBackgroundWorkerFinished = false;
         public static bool gOffset = false;
         public const int OPEN_EXISTING = 3;
         public const int SW_SHOW = 5;
         public const int FILE_SHARE_READ = 0x1;
         public const int FILE_SHARE_WRITE = 0x2;
         public const int FSCTL_LOCK_VOLUME = 0x00090018;
         public const int FSCTL_DISMOUNT_VOLUME = 0x00090020;
         public const int IOCTL_STORAGE_EJECT_MEDIA = 0x2D4808;
         public const int IOCTL_STORAGE_MEDIA_REMOVAL = 0x002D4804;
         public const int WM_USER = 0x400;
         public const int WM_COPYDATA = 0x4A;
         public const int WM_LBUTTONDBLCLK = 0x203;
         public const int WM_PASTE = 0x302;
         public const int WM_SCROLL = 276;
         public const int WM_VSCROLL = 277;
         public const int SB_LINEUP = 0;
         public const int SB_LINELEFT = 0;
         public const int SB_LINEDOWN = 1;
         public const int SB_LINERIGHT = 1;
         public const int SB_PAGEUP = 2;
         public const int SB_PAGELEFT = 2;
         public const int SB_PAGEDOWN = 3;
         public const int SB_PAGERIGHT = 3;
         public const int SB_PAGETOP = 6;
         public const int SB_LEFT = 6;
         public const int SB_PAGEBOTTOM = 7;
         public const int SB_RIGHT = 7;
         public const int SB_ENDSCROLL = 8;
         public const int MOUSEEVENTF_LEFTDOWN = 0x02;
         public const int MOUSEEVENTF_LEFTUP = 0x04;
         public const uint GENERIC_READ = 0x80000000;
         public const uint GENERIC_WRITE = 0x40000000;
         public const uint SEE_MASK_INVOKEIDLIST = 12;
         #endregion
#pragma warning restore SYSLIB1054
#pragma warning restore IDE1006
      }
   }
}
