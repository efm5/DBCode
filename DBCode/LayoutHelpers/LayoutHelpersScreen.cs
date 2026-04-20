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
#pragma warning restore CS8602
   }
}
