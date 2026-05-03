namespace DBCode {
   internal static partial class LayoutHelpers {
#pragma warning disable CS8602
      internal static Rectangle ScreenBoundsPrimary() {
         return Screen.PrimaryScreen.Bounds;
      }

      internal static Rectangle ScreenWorkingAreaPrimary() {
         return Screen.PrimaryScreen.WorkingArea;
      }

      internal static int ScreenWidthPrimary() {
         return Screen.PrimaryScreen.Bounds.Width;
      }

      internal static int ScreenHeightPrimary() {
         return Screen.PrimaryScreen.Bounds.Height;
      }

      internal static Point ScreenCenterPrimary() {
         int centerX = Screen.PrimaryScreen.Bounds.Left + (Screen.PrimaryScreen.Bounds.Width / 2);
         int centerY = Screen.PrimaryScreen.Bounds.Top + (Screen.PrimaryScreen.Bounds.Height / 2);
         return new Point(centerX, centerY);
      }

      internal static Rectangle ScreenBoundsOf(Control? pControl) {
         if (pControl == null)
            return Screen.PrimaryScreen.Bounds;
         return Screen.FromControl(pControl).Bounds;
      }

      internal static Rectangle ScreenWorkingAreaOf(Control? pControl) {
         if (pControl == null)
            return Screen.PrimaryScreen.WorkingArea;
         return Screen.FromControl(pControl).WorkingArea;
      }

      internal static Point ScreenCenterOf(Control? pControl) {
         Rectangle bounds;
         if (pControl == null)
            bounds = Screen.PrimaryScreen.Bounds;
         else
            bounds = Screen.FromControl(pControl).Bounds;

         int centerX = bounds.Left + (bounds.Width / 2);
         int centerY = bounds.Top + (bounds.Height / 2);
         return new Point(centerX, centerY);
      }

      internal static Rectangle GetTaskbarBounds() {
         LayoutHelpersNativeMethods.APPBARDATA data = new LayoutHelpersNativeMethods.APPBARDATA {
            cbSize = Marshal.SizeOf<LayoutHelpersNativeMethods.APPBARDATA>()
         };
         uint result = LayoutHelpersNativeMethods.SHAppBarMessage(LayoutHelpersNativeMethods.ABM_GETTASKBARPOS, ref data);
         if (result != 0) {
            int width = data.rc.Right - data.rc.Left;
            int height = data.rc.Bottom - data.rc.Top;
            return new Rectangle(data.rc.Left, data.rc.Top, width, height);
         }
         Rectangle screenBounds = ScreenBoundsPrimary();
         Rectangle workingArea = ScreenWorkingAreaPrimary();
         int left = 0;
         int top = 0;
         int widthFallback = 0;
         int heightFallback = 0;

         if (workingArea.Top > screenBounds.Top) {
            left = screenBounds.Left;
            top = screenBounds.Top;
            widthFallback = screenBounds.Width;
            heightFallback = workingArea.Top - screenBounds.Top;
         }
         else if (workingArea.Bottom < screenBounds.Bottom) {
            left = screenBounds.Left;
            top = workingArea.Bottom;
            widthFallback = screenBounds.Width;
            heightFallback = screenBounds.Bottom - workingArea.Bottom;
         }
         else if (workingArea.Left > screenBounds.Left) {
            left = screenBounds.Left;
            top = screenBounds.Top;
            widthFallback = workingArea.Left - screenBounds.Left;
            heightFallback = screenBounds.Height;
         }
         else if (workingArea.Right < screenBounds.Right) {
            left = workingArea.Right;
            top = screenBounds.Top;
            widthFallback = screenBounds.Right - workingArea.Right;
            heightFallback = screenBounds.Height;
         }
         else {
            return Rectangle.Empty;
         }
         return new Rectangle(left, top, widthFallback, heightFallback);
      }

      internal static bool EnsureWindowFitsMonitor(Form? pForm) {
         if (pForm == null)
            return false;
         Screen formScreen = Screen.FromControl(pForm);
         Rectangle workingArea = formScreen.WorkingArea;
         Size size = pForm.Size;
         int controlBoxSpace = pForm.ControlBox ? 4 : 1; // derived from Form state; no longer a parameter
         bool changed = false;
         const int margin = 10;
         SizeF titleSize;
         using (Graphics graphics = pForm.CreateGraphics()) {
            titleSize = graphics.MeasureString(pForm.Text, SystemFonts.CaptionFont!);
         }
         int wantedTitleWidth = (int)((titleSize.Width * 0.86f) +
            (SystemInformation.CaptionButtonSize.Width * controlBoxSpace) + (margin * 2));
         int maxAllowedWidth = workingArea.Width - margin;
         if (wantedTitleWidth > maxAllowedWidth)
            wantedTitleWidth = maxAllowedWidth;
         if (size.Width < wantedTitleWidth) {
            size.Width = wantedTitleWidth;
            changed = true;
         }
         if (size.Width > maxAllowedWidth) {
            size.Width = maxAllowedWidth;
            changed = true;
         }
         int maxAllowedHeight = workingArea.Height - margin;
         if (size.Height > maxAllowedHeight) {
            size.Height = maxAllowedHeight;
            changed = true;
         }
         if (changed)
            pForm.Size = size;
         int x = pForm.Left;
         int y = pForm.Top;
         bool positionChanged = false;
         if (pForm.Left < workingArea.Left) {
            x = workingArea.Left + (margin / 2);
            positionChanged = true;
         }
         else if (pForm.Right > workingArea.Right) {
            x = workingArea.Right - size.Width - (margin / 2);
            positionChanged = true;
         }
         if (pForm.Top < workingArea.Top) {
            y = workingArea.Top + (margin / 2);
            positionChanged = true;
         }
         else if (pForm.Bottom > workingArea.Bottom) {
            y = workingArea.Bottom - size.Height - (margin / 2);
            positionChanged = true;
         }
         if (positionChanged) {
            pForm.Location = new Point(x, y);
            changed = true;
         }
         if (IsOffScreen(pForm)) {
            pForm.Location = new Point(workingArea.Left + (margin / 2), workingArea.Top + (margin / 2));
            changed = true;
         }
         if (IsPartiallyHidden(pForm)) {
            if (!positionChanged)
               pForm.Location = new Point(workingArea.Left + (margin / 2), workingArea.Top + (margin / 2));
            if (pForm.Width > maxAllowedWidth) {
               pForm.Width = maxAllowedWidth;
               changed = true;
            }
            if (pForm.Height > maxAllowedHeight) {
               pForm.Height = maxAllowedHeight;
               changed = true;
            }
         }
         return changed;
      }

      internal static bool IsOffScreen(Form? pForm) {
         if (pForm == null)
            return true;
         Screen[] screens = Screen.AllScreens;
         for (int i = 0; i < screens.Length; i++) {
            if (screens[i].WorkingArea.Contains(new Point(pForm.Left, pForm.Top)))
               return false;
         }
         return true;
      }

      internal static bool IsPartiallyHidden(Form? pForm) {
         if (pForm == null)
            return true;
         Screen[] screens = Screen.AllScreens;
         for (int i = 0; i < screens.Length; i++) {
            if (screens[i].WorkingArea.Contains(new Point(pForm.Right, pForm.Bottom)))
               return false;
         }
         return true;
      }

      internal static void CenterFormOnMonitor(Form? pForm) {
         if (pForm == null)
            return;
         Screen screen = Screen.FromControl(pForm);
         Rectangle workingArea = screen.WorkingArea;
         pForm.Left = workingArea.X + (workingArea.Width - pForm.Width) / 2;
         pForm.Top = workingArea.Y + (workingArea.Height - pForm.Height) / 2;
      }

      internal static void GetDPI(Screen pScreen, DPIType pDpiType, out uint pODpiX, out uint pODpiY) {
         POINT location = new POINT(pScreen.Bounds.Left + 1, pScreen.Bounds.Top + 1);
         nint monitor = MonitorFromPoint(location, 2);
         _ = GetDpiForMonitor(monitor, pDpiType, out pODpiX, out pODpiY);
      }
#pragma warning restore CS8602
   }
}
