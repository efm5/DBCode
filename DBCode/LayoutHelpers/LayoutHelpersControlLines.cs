namespace DBCode {
   internal static partial class LayoutHelpers {
#pragma warning disable IDE1006
      public static bool LocateUpDownLine(out int pONextTop, int pTop, Button? pPrefixButton, NumericUpDown? pUpDown,
         Label? pSuffixLabel = null, int pLeft = -1) {
         pONextTop = pTop;
         ThrowIfNull(pPrefixButton, nameof(pPrefixButton));
         ThrowIfNull(pUpDown, nameof(pUpDown));
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
         ThrowIfNull(pCheckBox, nameof(pCheckBox));
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
         ThrowIfNull(pPrefixButton, nameof(pPrefixButton));
         ThrowIfNull(pTextBox, nameof(pTextBox));
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
         ThrowIfNull(pAnchorControl, nameof(pAnchorControl));
         ThrowIfNull(pControlList, nameof(pControlList));
         ThrowIfNull(lastControl, nameof(lastControl));
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

      public static void SetBottomPanelHeight(Panel pPanel, int pVerticalPadding = -1) {
         int contentTop = int.MaxValue;
         int contentBottom = 0;

         if (pVerticalPadding < 0)
            pVerticalPadding = mEmFifth;
         foreach (Control control in pPanel.Controls) {
            if (control.Top < contentTop)
               contentTop = control.Top;
            if (control.Bottom > contentBottom)
               contentBottom = control.Bottom;
         }
         if (contentTop == int.MaxValue)
            return;
         pPanel.Height = (contentBottom - contentTop) + (pVerticalPadding * 2);
         foreach (Control control in pPanel.Controls)
            CenterControlVertically(pPanel, control);
      }

      internal static bool ControlHasLines(Control? pControl) {
         if (pControl == null)
            return false;
         return pControl.Text.Contains('\n');
      }

      internal static bool ControlHasNoLines(Control? pControl) {
         if (pControl == null)
            return true;
         return !pControl.Text.Contains('\n');
      }

      internal static int ControlLineCount(Control? pControl) {
         if (pControl == null)
            return 0;
         if (string.IsNullOrEmpty(pControl.Text))
            return 0;
         return pControl.Text.Split('\n').Length;
      }

      internal static string[] ControlLinesOrEmpty(Control? pControl) {
         if (pControl == null)
            return [];
         if (string.IsNullOrEmpty(pControl.Text))
            return [];
         return pControl.Text.Split('\n');
      }

      internal static string ControlFirstLineOrEmpty(Control? pControl) {
         if (pControl == null)
            return string.Empty;
         if (string.IsNullOrEmpty(pControl.Text))
            return string.Empty;
         return pControl.Text.Split('\n')[0].Trim();
      }

      internal static string ControlLastLineOrEmpty(Control? pControl) {
         if (pControl == null)
            return string.Empty;
         if (string.IsNullOrEmpty(pControl.Text))
            return string.Empty;
         string[] lines = pControl.Text.Split('\n');
         return lines[lines.Length - 1].Trim();
      }
#pragma warning restore IDE1006
   }
}
