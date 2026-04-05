namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool RadioButtonIsChecked(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return false;
         return pRadioButton.Checked;
      }

      internal static bool RadioButtonIsUnchecked(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return true;
         return !pRadioButton.Checked;
      }

      internal static void RadioButtonSetChecked(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return;
         pRadioButton.Checked = true;
      }

      internal static void RadioButtonSetUnchecked(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return;
         pRadioButton.Checked = false;
      }

      internal static bool RadioButtonHasText(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return false;
         if (string.IsNullOrWhiteSpace(pRadioButton.Text))
            return false;
         return true;
      }

      internal static bool RadioButtonHasNoText(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return true;
         if (string.IsNullOrWhiteSpace(pRadioButton.Text))
            return true;
         return false;
      }

      internal static string RadioButtonTextOrEmpty(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return string.Empty;
         if (string.IsNullOrWhiteSpace(pRadioButton.Text))
            return string.Empty;
         return pRadioButton.Text.Trim();
      }

      internal static void RadioButtonSetText(RadioButton? pRadioButton, string? pText) {
         if (pRadioButton == null)
            return;
         if (string.IsNullOrWhiteSpace(pText)) {
            pRadioButton.Text = string.Empty;
            return;
         }
         pRadioButton.Text = pText.Trim();
      }

      internal static void RadioButtonEnable(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return;
         pRadioButton.Enabled = true;
      }

      internal static void RadioButtonDisable(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return;
         pRadioButton.Enabled = false;
      }

      internal static bool RadioButtonIsEnabled(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return false;
         return pRadioButton.Enabled;
      }

      internal static bool RadioButtonIsDisabled(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return true;
         return !pRadioButton.Enabled;
      }

      internal static void RadioButtonShow(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return;
         pRadioButton.Visible = true;
      }

      internal static void RadioButtonHide(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return;
         pRadioButton.Visible = false;
      }

      internal static bool RadioButtonIsVisible(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return false;
         return pRadioButton.Visible;
      }

      internal static bool RadioButtonIsHidden(RadioButton? pRadioButton) {
         if (pRadioButton == null)
            return true;
         return !pRadioButton.Visible;
      }

      internal static void RadioButtonSetForeColor(RadioButton? pRadioButton, Color pColor) {
         if (pRadioButton == null)
            return;
         pRadioButton.ForeColor = pColor;
      }

      internal static void RadioButtonSetBackColor(RadioButton? pRadioButton, Color pColor) {
         if (pRadioButton == null)
            return;
         pRadioButton.BackColor = pColor;
      }

      internal static void RadioButtonSetPadding(RadioButton? pRadioButton, Padding pPadding) {
         if (pRadioButton == null)
            return;
         pRadioButton.Padding = pPadding;
      }
   }
}
