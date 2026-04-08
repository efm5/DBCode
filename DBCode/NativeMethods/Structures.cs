namespace DBCode {
   static partial class Program {
      internal static partial class NativeMethods {
         #region Structures
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
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
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

            public RECT(int pLeft, int pTop, int pRight, int pBottom) {
               Left = pLeft;
               Top = pTop;
               Right = pRight;
               Bottom = pBottom;
            }

            public RECT(Point pLocation, Size pSize) {
               Left = pLocation.X;
               Top = pLocation.Y;
               Right = pLocation.X + pSize.Width;
               Bottom = pLocation.Y + pSize.Height;
            }

            public RECT(Rectangle pRectangle) {
               Left = pRectangle.Left;
               Top = pRectangle.Top;
               Right = pRectangle.Right;
               Bottom = pRectangle.Bottom;
            }

            public readonly int Width() {
               return (Right - Left);
            }

            public readonly int Height() {
               return (Bottom - Top);
            }
         }


         [StructLayout(LayoutKind.Sequential)]
         public struct POINT {
            public int X;
            public int Y;

            public POINT(int pX, int pY) {
               X = pX;
               Y = pY;
            }

            public POINT(Point pPoint) {
               X = pPoint.X;
               Y = pPoint.Y;
            }
         }

         [StructLayout(LayoutKind.Sequential)]
         public struct INT32 {
            public int Value;
         }
         #endregion
      }
   }
}
