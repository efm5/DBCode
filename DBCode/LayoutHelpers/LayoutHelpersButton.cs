namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static void ButtonEnable(Button pButton) {
         if (pButton == null)
            return;
         pButton.Enabled = true;
      }

      internal static void ButtonDisable(Button pButton) {
         if (pButton == null)
            return;
         pButton.Enabled = false;
      }

      internal static bool ButtonIsEnabled(Button pButton) {
         if (pButton == null)
            return false;
         return pButton.Enabled;
      }

      internal static bool ButtonIsDisabled(Button pButton) {
         if (pButton == null)
            return true;
         return !pButton.Enabled;
      }

      internal static void ButtonShow(Button pButton) {
         if (pButton == null)
            return;
         pButton.Visible = true;
      }

      internal static void ButtonHide(Button pButton) {
         if (pButton == null)
            return;
         pButton.Visible = false;
      }

      internal static bool ButtonIsVisible(Button pButton) {
         if (pButton == null)
            return false;
         return pButton.Visible;
      }

      internal static bool ButtonIsHidden(Button pButton) {
         if (pButton == null)
            return true;
         return !pButton.Visible;
      }

      internal static void ButtonSetText(Button pButton, string pText) {
         if (pButton == null)
            return;
         if (string.IsNullOrWhiteSpace(pText)) {
            pButton.Text = string.Empty;
            return;
         }
         pButton.Text = pText.Trim();
      }

      internal static string ButtonTextOrEmpty(Button pButton) {
         if (pButton == null)
            return string.Empty;
         if (string.IsNullOrWhiteSpace(pButton.Text))
            return string.Empty;
         return pButton.Text.Trim();
      }

      internal static bool ButtonHasText(Button pButton) {
         if (pButton == null)
            return false;
         if (string.IsNullOrWhiteSpace(pButton.Text))
            return false;
         return true;
      }

      internal static bool ButtonHasNoText(Button pButton) {
         if (pButton == null)
            return true;
         if (string.IsNullOrWhiteSpace(pButton.Text))
            return true;
         return false;
      }

      internal static void ButtonSetForeColor(Button pButton, Color pColor) {
         if (pButton == null)
            return;
         pButton.ForeColor = pColor;
      }

      internal static void ButtonSetBackColor(Button pButton, Color pColor) {
         if (pButton == null)
            return;
         pButton.BackColor = pColor;
      }

      internal static void ButtonSetImage(Button pButton, Image pImage) {
         if (pButton == null)
            return;
         pButton.Image = pImage;
      }

      internal static void ButtonClearImage(Button pButton) {
         if (pButton == null)
            return;
         pButton.Image = null;
      }

      internal static void ButtonSetPadding(Button pButton, Padding pPadding) {
         if (pButton == null)
            return;
         pButton.Padding = pPadding;
      }

      internal static void ButtonSetFlatStyle(Button pButton, FlatStyle pStyle) {
         if (pButton == null)
            return;
         pButton.FlatStyle = pStyle;
      }

      internal static void ButtonSetAutoSize(Button pButton, bool pAutoSize) {
         if (pButton == null)
            return;
         pButton.AutoSize = pAutoSize;
      }
   }
}
