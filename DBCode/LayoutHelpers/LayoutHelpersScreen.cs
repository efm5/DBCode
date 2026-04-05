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
#pragma warning restore CS8602
   }
}
