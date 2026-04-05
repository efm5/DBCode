namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool LabelHasText(Label? pLabel) {
         if (pLabel == null)
            return false;
         if (string.IsNullOrWhiteSpace(pLabel.Text))
            return false;
         return true;
      }

      internal static bool LabelHasNoText(Label? pLabel) {
         if (pLabel == null)
            return true;
         if (string.IsNullOrWhiteSpace(pLabel.Text))
            return true;
         return false;
      }

      internal static string LabelTextOrEmpty(Label? pLabel) {
         if (pLabel == null)
            return string.Empty;
         if (string.IsNullOrWhiteSpace(pLabel.Text))
            return string.Empty;
         return pLabel.Text.Trim();
      }

      internal static void LabelSetText(Label? pLabel, string? pText) {
         if (pLabel == null)
            return;
         if (string.IsNullOrWhiteSpace(pText)) {
            pLabel.Text = string.Empty;
            return;
         }
         pLabel.Text = pText.Trim();
      }

      internal static void LabelEnable(Label? pLabel) {
         if (pLabel == null)
            return;
         pLabel.Enabled = true;
      }

      internal static void LabelDisable(Label? pLabel) {
         if (pLabel == null)
            return;
         pLabel.Enabled = false;
      }

      internal static bool LabelIsEnabled(Label? pLabel) {
         if (pLabel == null)
            return false;
         return pLabel.Enabled;
      }

      internal static bool LabelIsDisabled(Label? pLabel) {
         if (pLabel == null)
            return true;
         return !pLabel.Enabled;
      }

      internal static void LabelShow(Label? pLabel) {
         if (pLabel == null)
            return;
         pLabel.Visible = true;
      }

      internal static void LabelHide(Label? pLabel) {
         if (pLabel == null)
            return;
         pLabel.Visible = false;
      }

      internal static bool LabelIsVisible(Label? pLabel) {
         if (pLabel == null)
            return false;
         return pLabel.Visible;
      }

      internal static bool LabelIsHidden(Label? pLabel) {
         if (pLabel == null)
            return true;
         return !pLabel.Visible;
      }

      internal static void LabelSetForeColor(Label? pLabel, Color pColor) {
         if (pLabel == null)
            return;
         pLabel.ForeColor = pColor;
      }

      internal static void LabelSetBackColor(Label? pLabel, Color pColor) {
         if (pLabel == null)
            return;
         pLabel.BackColor = pColor;
      }

      internal static void LabelSetPadding(Label? pLabel, Padding pPadding) {
         if (pLabel == null)
            return;
         pLabel.Padding = pPadding;
      }

      internal static void LabelSetTextAlign(Label? pLabel, ContentAlignment pAlignment) {
         if (pLabel == null)
            return;
         pLabel.TextAlign = pAlignment;
      }

      internal static void LabelSetAutoSize(Label? pLabel, bool pAutoSize) {
         if (pLabel == null)
            return;
         pLabel.AutoSize = pAutoSize;
      }
   }
}
