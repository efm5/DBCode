namespace DBCode {
   static partial class Program {
      internal static partial class NativeMethods {
         // Retrieves the DPI for the specified monitor.
         [LibraryImport("Shcore.dll", EntryPoint = "GetDpiForMonitor")]
         internal static partial int GetDpiForMonitor(nint pMonitorHandle, DPIType pDpiType, out uint pDpiX, out uint pDpiY);

         // Retrieves a handle to the display monitor nearest to the specified point.
         [LibraryImport("user32.dll", EntryPoint = "MonitorFromPoint")]
         internal static partial nint MonitorFromPoint(POINT pPoint, int pFlags);

         // Enumerates all top-level windows on the screen.
#pragma warning disable SYSLIB1054 
         // Using DllImport instead of LibraryImport is intentional for this API.
         [DllImport("user32.dll", EntryPoint = "EnumWindows", ExactSpelling = true, SetLastError = true)]
         internal static extern unsafe bool EnumWindows(delegate* unmanaged[Stdcall]<nint, nint, bool> pCallback, nint pLeftParameters);
#pragma warning restore SYSLIB1054

         // Retrieves a handle to a window that has the specified relationship to the specified window.
         [LibraryImport("user32.dll", EntryPoint = "GetWindow", SetLastError = true)]
         internal static partial nint GetWindow(nint pWindowHandle, uint pCommand);

         // Retrieves information about the specified window.
         [LibraryImport("user32.dll", EntryPoint = "GetWindowLongPtrW", SetLastError = true)]
         internal static partial nint GetWindowLongPtr(nint pWindowHandle, int pIndex);

         // Retrieves the value of a specified window attribute.
         [LibraryImport("dwmapi.dll", EntryPoint = "DwmGetWindowAttribute")]
         internal static partial int DwmGetWindowAttribute(nint pWindowHandle, int pAttribute, out RECT pORect, int pSize);

         // Retrieves the value of a specified window attribute.
         [LibraryImport("dwmapi.dll", EntryPoint = "DwmGetWindowAttribute")]
         internal static partial int DwmGetWindowAttributeInt(nint pWindowHandle, int pAttribute, out INT32 pOValue, int pSize);

         // Retrieves a handle to the window that contains the specified point.
         [LibraryImport("user32.dll", EntryPoint = "WindowFromPoint")]
         internal static partial nint WindowFromPoint(POINT pPoint);

         // Retrieves a handle to the top-level window whose class name and window name match the specified strings.
         [LibraryImport("user32.dll", EntryPoint = "FindWindowW", StringMarshalling = StringMarshalling.Utf16)]
         internal static partial nint FindWindow(string pClassName, string pWindowName);

         // Retrieves a handle to the foreground window.
         [LibraryImport("user32.dll", EntryPoint = "GetForegroundWindow")]
         internal static partial nint GetForegroundWindow();

         // Copies the text of the specified window's title bar into a buffer.
#pragma warning disable SYSLIB1054 // Using DllImport intentionally for unsupported signature.
         [DllImport("user32.dll", EntryPoint = "GetWindowTextW", ExactSpelling = true, SetLastError = true)]
         internal static extern unsafe int GetWindowText(nint pWindowHandle, char* pBuffer, int pBufferLength);
#pragma warning restore SYSLIB1054

         // Retrieves the length of the specified window's title bar text.
         [LibraryImport("user32.dll", EntryPoint = "GetWindowTextLengthW", SetLastError = true)]
         internal static partial int GetWindowTextLength(nint pWindowHandle);

         // Retrieves the placement of the specified window.
         [LibraryImport("user32.dll", EntryPoint = "GetWindowPlacement", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static partial bool GetWindowPlacement(nint pWindowHandle, ref WINDOWPLACEMENT pPlacement);

         // Retrieves the dimensions of the bounding rectangle of the specified window.
         [LibraryImport("user32.dll", EntryPoint = "GetWindowRect", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static partial bool GetWindowRect(nint pWindowHandle, out RECT pRect);

         // Determines whether the specified window is minimized.
         [LibraryImport("user32.dll", EntryPoint = "IsIconic")]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static partial bool IsIconic(nint pWindowHandle);

         // Determines whether the specified window handle identifies an existing window.
         [LibraryImport("user32.dll", EntryPoint = "IsWindow")]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static partial bool IsWindow(nint pWindowHandle);

         // Determines whether the specified window is visible.
         [LibraryImport("user32.dll", EntryPoint = "IsWindowVisible")]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static partial bool IsWindowVisible(nint pWindowHandle);

         // Changes the size, position, and Z order of a window.
         [LibraryImport("user32.dll", EntryPoint = "SetWindowPos", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static partial bool SetWindowPos(nint pWindowHandle, nint pInsertAfter, int pX, int pY, int pWidth, int pHeight, uint pFlags);

         // Sets the show state of a window.
         [LibraryImport("user32.dll", EntryPoint = "ShowWindow")]
         [return: MarshalAs(UnmanagedType.Bool)]
         internal static partial bool ShowWindow(nint pWindowHandle, ShowWindowEnum pCommand);

         // Retrieves a handle to the display monitor nearest to the specified point (uint flags overload).
         [LibraryImport("user32.dll", EntryPoint = "MonitorFromPoint")]
         internal static partial nint MonitorFromPoint(POINT pPoint, uint pFlags);

         internal enum DPIType { Effective = 0, Angular = 1, Raw = 2 }

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
         }
      }
   }
}
