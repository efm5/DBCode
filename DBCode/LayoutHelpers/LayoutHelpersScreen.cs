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
         NativeMathMethods.APPBARDATA data = new NativeMathMethods.APPBARDATA {
            cbSize = Marshal.SizeOf(typeof(NativeMathMethods.APPBARDATA))
         };
         uint result = NativeMathMethods.SHAppBarMessage(NativeMathMethods.ABM_GETTASKBARPOS, ref data);
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

      internal static bool EnsureWindowFitsMonitor(Form? pForm, bool pControlBox = true) {
         if (pForm == null)
            return false;
         Screen formScreen = Screen.FromControl(pForm);
         Rectangle workingArea = formScreen.WorkingArea;
         Size size = pForm.Size;
         int controlBoxSpace = pControlBox ? 4 : 1;
         bool changed = false;

         SizeF titleSize;
         using (Graphics graphics = pForm.CreateGraphics()) {
            titleSize = graphics.MeasureString(pForm.Text, CreateNewFont());
         }
         int wantedTitleWidth = (int)((titleSize.Width * 0.86f) +
            (SystemInformation.CaptionButtonSize.Width * controlBoxSpace));

         if (size.Width < wantedTitleWidth) {
            size.Width = wantedTitleWidth;
            changed = true;
         }
         if (size.Width > workingArea.Width) {
            size.Width = workingArea.Width - 10;
            changed = true;
         }
         if (size.Height > workingArea.Height) {
            size.Height = workingArea.Height - 10;
            changed = true;
         }
         pForm.Size = size;

         int x = pForm.Left;
         int y = pForm.Top;

         if (pForm.Right > workingArea.Right)
            x = workingArea.Right - size.Width - 5;
         if (pForm.Bottom > workingArea.Bottom)
            y = workingArea.Bottom - size.Height - 5;

         pForm.Location = new Point(x, y);

         if (IsOffScreen(pForm))
            pForm.Location = new Point(workingArea.Left + 5, workingArea.Top + 5);

         if (IsPartiallyHidden(pForm)) {
            pForm.Location = new Point(workingArea.Left + 5, workingArea.Top + 5);
            if (pForm.Width > workingArea.Width - 10)
               pForm.Width = workingArea.Width - 10;
            if (pForm.Height > workingArea.Height - 10)
               pForm.Height = workingArea.Height - 10;
            changed = true;
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
