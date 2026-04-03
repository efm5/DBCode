namespace DBCode {
   internal static partial class LayoutHelpers {
#pragma warning disable IDE1006

      public static bool LocateUpDownLine(out int pONextTop, int pTop, Button? pPrefixButton, NumericUpDown? pUpDown,
         Label? pSuffixLabel = null, int pLeft = -1) {
         pONextTop = pTop;
         if ((pPrefixButton == null) || (pUpDown == null)) {
            TimedMessage("LocateUpDownLine() some variable was illegally null.", "Code VIOLATION", 0);
            return false;
         }
         if (pLeft == -1)
            pLeft = pPrefixButton.Left;
         pPrefixButton.Location = new Point(pLeft, pTop);
         pUpDown.Location = new Point(
            pPrefixButton.Right + mAssociatedUpDownPostButtonHorizontalSpace,
            pPrefixButton.Top + mAssociatedUpDownPostButtonVerticalOffset);
         pONextTop = Math.Max(pPrefixButton.Bottom, pUpDown.Bottom);
         if (pSuffixLabel != null) {
            pSuffixLabel.Location = new Point(
               pUpDown.Right + mAssociatedLabelPostUpDownHorizontalSpace,
               pUpDown.Top + mAssociatedLabelPostUpDownVerticalOffset);
            pONextTop = Math.Max(pONextTop, pSuffixLabel.Bottom);
         }
         return true;
      }

      public static bool LocateCheckBoxLine(out int oNextTop, int pTop, CheckBox? pCheckBox, Label? pSuffixLabel = null,
         int pLeft = 20) {
         oNextTop = pTop;
         if (pCheckBox == null) {
            TimedMessage("LocateCheckBoxLine() some variable was illegally null.", "Code VIOLATION", 0);
            return false;
         }
         pCheckBox.Location = new Point(pLeft, pTop);
         if (pSuffixLabel != null) {
            pSuffixLabel.Location = new Point(
               pCheckBox.Right + mAssociatedLabelPostCheckBoxHorizontalSpace,
               pCheckBox.Top + mAssociatedLabelPostCheckBoxVerticalOffset);
            oNextTop = (int)Math.Max(pCheckBox.Bottom, pSuffixLabel.Bottom);
         }
         else
            oNextTop = pCheckBox.Bottom;
         return true;
      }

      public static bool LocatePrefixedTextBox(out Point oNextLocation, int pTop, Button? pPrefixButton, TextBox? pTextBox,
         int pLeft = 20) {
         oNextLocation = new Point(pLeft, pTop);
         if ((pPrefixButton == null) || (pTextBox == null)) {
            TimedMessage("LocatePrefixedTextBoxLine() some variable was illegally null.", "Code VIOLATION", 0);
            return false;
         }
         pPrefixButton.Location = new Point(pLeft, pTop);
         pTextBox.Location = new Point(
            pPrefixButton.Right + mAssociatedTextBoxPostButtonHorizontalSpace,
            pPrefixButton.Top + mAssociatedTextBoxPostButtonVerticalOffset);
         oNextLocation = new Point(pTextBox.Right, Math.Max(pPrefixButton.Bottom, pTextBox.Bottom));
         return true;
      }

      public static bool LocateControls(out Point oNextPaddedLocation, Control? pAnchorControl, List<Control>? pControlList,
         bool pHorizontal, int pPadding) {
         Control? lastControl = pAnchorControl;
         oNextPaddedLocation = new Point(0, 0);
         if ((pAnchorControl == null) || (pControlList == null) || (lastControl == null)) {
            TimedMessage("LocateControls() some variable was illegally null.", "Code VIOLATION", 0);
            return false;
         }
         foreach (Control control in pControlList.OfType<Control>()) {
            if (pHorizontal)
               control.Location = new Point(lastControl.Right + pPadding, lastControl.Top);
            else
               control.Location = new Point(lastControl.Left, lastControl.Bottom + pPadding);
            lastControl = control;
         }
         oNextPaddedLocation = new Point(lastControl.Right, lastControl.Bottom);
         return true;
      }

      public static void SetBottomPanelHeight(Panel pPanel) {
         int top = 1;
         int height = 1;
         foreach (Button button in pPanel.Controls.OfType<Button>()) {
            if (button.Top > top)
               top = button.Top;
            if (button.Height > height)
               height = button.Height;
         }
         foreach (MenuStrip menu in pPanel.Controls.OfType<MenuStrip>()) {
            if (menu.Top > top)
               top = menu.Top;
            if (menu.Height > height)
               height = menu.Height;
         }
         pPanel.Height = height + (top * 2);
      }

#pragma warning restore IDE1006
   }
}
