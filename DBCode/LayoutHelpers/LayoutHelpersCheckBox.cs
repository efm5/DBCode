namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool CheckBoxIsChecked(CheckBox pCheckBox) {
         if (pCheckBox == null)
            return false;
         return pCheckBox.Checked;
      }

      internal static bool CheckBoxIsUnchecked(CheckBox pCheckBox) {
         if (pCheckBox == null)
            return true;
         return !pCheckBox.Checked;
      }

      internal static void CheckBoxSetChecked(CheckBox pCheckBox) {
         if (pCheckBox == null)
            return;
         pCheckBox.Checked = true;
      }

      internal static void CheckBoxSetUnchecked(CheckBox pCheckBox) {
         if (pCheckBox == null)
            return;
         pCheckBox.Checked = false;
      }

      internal static void CheckBoxToggle(CheckBox pCheckBox) {
         if (pCheckBox == null)
            return;
         pCheckBox.Checked = !pCheckBox.Checked;
      }

      internal static bool CheckBoxHasText(CheckBox pCheckBox) {
         if (pCheckBox == null)
            return false;
         if (string.IsNullOrWhiteSpace(pCheckBox.Text))
            return false;
         return true;
      }

      internal static bool CheckBoxHasNoText(CheckBox pCheckBox) {
         if (pCheckBox == null)
            return true;
         if (string.IsNullOrWhiteSpace(pCheckBox.Text))
            return true;
         return false;
      }

      internal static string CheckBoxTextOrEmpty(CheckBox pCheckBox) {
         if (pCheckBox == null)
            return string.Empty;
         if (string.IsNullOrWhiteSpace(pCheckBox.Text))
            return string.Empty;
         return pCheckBox.Text.Trim();
      }

      internal static void CheckBoxSetText(CheckBox pCheckBox, string pText) {
         if (pCheckBox == null)
            return;
         if (string.IsNullOrWhiteSpace(pText)) {
            pCheckBox.Text = string.Empty;
            return;
         }
         pCheckBox.Text = pText.Trim();
      }

      internal static void CheckBoxEnable(CheckBox pCheckBox) {
         if (pCheckBox == null)
            return;
         pCheckBox.Enabled = true;
      }

      internal static void CheckBoxDisable(CheckBox pCheckBox) {
         if (pCheckBox == null)
            return;
         pCheckBox.Enabled = false;
      }

      internal static bool CheckBoxIsEnabled(CheckBox pCheckBox) {
         if (pCheckBox == null)
            return false;
         return pCheckBox.Enabled;
      }

      internal static bool CheckBoxIsDisabled(CheckBox pCheckBox) {
         if (pCheckBox == null)
            return true;
         return !pCheckBox.Enabled;
      }
   }
}
