namespace DBCode {
   internal static partial class LayoutHelpers {
#pragma warning disable IDE1006
      public static bool IsOffScreen(Form pForm) {
         Screen[] screens = Screen.AllScreens;
         foreach (Screen screen in screens) {
            Point formTopLeft = new Point(pForm.Left, pForm.Top);

            if (screen.WorkingArea.Contains(formTopLeft))
               return false;
         }
         return true;
      }

      public static bool IsPartiallyHidden(Form pForm) {
         Screen[] screens = Screen.AllScreens;
         foreach (Screen screen in screens) {
            if (screen.WorkingArea.Contains(new Point(pForm.Right, pForm.Bottom)))
               return false;
         }
         return true;
      }

      public static bool BooleanFromString(string pInput, out bool oBoolean) {
         oBoolean = false;
         if (string.Equals("True", pInput, StringComparison.OrdinalIgnoreCase)) {
            oBoolean = true;
            return true;
         }
         if (string.Equals("False", pInput, StringComparison.OrdinalIgnoreCase)) {
            oBoolean = false;
            return true;
         }
         return false;
      }

#pragma warning restore IDE1006
   }
}
