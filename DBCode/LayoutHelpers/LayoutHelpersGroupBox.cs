namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool GroupBoxHasText(GroupBox pGroupBox) {
         if (pGroupBox == null)
            return false;
         if (string.IsNullOrWhiteSpace(pGroupBox.Text))
            return false;
         return true;
      }

      internal static bool GroupBoxHasNoText(GroupBox pGroupBox) {
         if (pGroupBox == null)
            return true;
         if (string.IsNullOrWhiteSpace(pGroupBox.Text))
            return true;
         return false;
      }

      internal static string GroupBoxTextOrEmpty(GroupBox pGroupBox) {
         if (pGroupBox == null)
            return string.Empty;
         if (string.IsNullOrWhiteSpace(pGroupBox.Text))
            return string.Empty;
         return pGroupBox.Text.Trim();
      }

      internal static void GroupBoxSetText(GroupBox pGroupBox, string pText) {
         if (pGroupBox == null)
            return;
         if (string.IsNullOrWhiteSpace(pText)) {
            pGroupBox.Text = string.Empty;
            return;
         }
         pGroupBox.Text = pText.Trim();
      }

      internal static void GroupBoxEnable(GroupBox pGroupBox) {
         if (pGroupBox == null)
            return;
         pGroupBox.Enabled = true;
      }

      internal static void GroupBoxDisable(GroupBox pGroupBox) {
         if (pGroupBox == null)
            return;
         pGroupBox.Enabled = false;
      }

      internal static bool GroupBoxIsEnabled(GroupBox pGroupBox) {
         if (pGroupBox == null)
            return false;
         return pGroupBox.Enabled;
      }

      internal static bool GroupBoxIsDisabled(GroupBox pGroupBox) {
         if (pGroupBox == null)
            return true;
         return !pGroupBox.Enabled;
      }

      internal static void GroupBoxShow(GroupBox pGroupBox) {
         if (pGroupBox == null)
            return;
         pGroupBox.Visible = true;
      }

      internal static void GroupBoxHide(GroupBox pGroupBox) {
         if (pGroupBox == null)
            return;
         pGroupBox.Visible = false;
      }

      internal static bool GroupBoxIsVisible(GroupBox pGroupBox) {
         if (pGroupBox == null)
            return false;
         return pGroupBox.Visible;
      }

      internal static bool GroupBoxIsHidden(GroupBox pGroupBox) {
         if (pGroupBox == null)
            return true;
         return !pGroupBox.Visible;
      }

      internal static void GroupBoxSetForeColor(GroupBox pGroupBox, Color pColor) {
         if (pGroupBox == null)
            return;
         pGroupBox.ForeColor = pColor;
      }

      internal static void GroupBoxSetBackColor(GroupBox pGroupBox, Color pColor) {
         if (pGroupBox == null)
            return;
         pGroupBox.BackColor = pColor;
      }

      internal static void GroupBoxSetPadding(GroupBox pGroupBox, Padding pPadding) {
         if (pGroupBox == null)
            return;
         pGroupBox.Padding = pPadding;
      }
   }
}
