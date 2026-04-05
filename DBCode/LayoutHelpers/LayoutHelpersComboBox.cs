namespace DBCode {
   internal static partial class LayoutHelpers {

      internal static bool ComboHasSelection(ComboBox? pCombo) {
         if (pCombo == null)
            return false;
         if (pCombo.SelectedIndex < 0)
            return false;
         return true;
      }

      internal static bool ComboHasNoSelection(ComboBox? pCombo) {
         if (pCombo == null)
            return true;
         if (pCombo.SelectedIndex < 0)
            return true;
         return false;
      }

      internal static string ComboSelectedTextOrEmpty(ComboBox? pCombo) {
         if (pCombo == null)
            return string.Empty;
         object? selectedItem = pCombo.SelectedItem;
         if (selectedItem == null)
            return string.Empty;
         return selectedItem.ToString() ?? string.Empty;
      }

      internal static int ComboSelectedIndexOrMinusOne(ComboBox? pCombo) {
         if (pCombo == null)
            return -1;
         return pCombo.SelectedIndex;
      }

      internal static void ComboSelectFirstIfAny(ComboBox? pCombo) {
         if (pCombo == null)
            return;
         if (pCombo.Items == null)
            return;
         if (pCombo.Items.Count == 0)
            return;
         pCombo.SelectedIndex = 0;
      }

      internal static void ComboSelectLastIfAny(ComboBox? pCombo) {
         if (pCombo == null)
            return;
         if (pCombo.Items == null)
            return;
         int count = pCombo.Items.Count;
         if (count == 0)
            return;
         pCombo.SelectedIndex = count - 1;
      }

      internal static bool ComboContainsText(ComboBox? pCombo, string? pText) {
         if (pCombo == null)
            return false;
         if (pCombo.Items == null)
            return false;
         if (string.IsNullOrWhiteSpace(pText))
            return false;
         string trimmed = pText.Trim();
         int count = pCombo.Items.Count;
         for (int index = 0; index < count; index++) {
            object? nextItem = pCombo.Items[index];
            if (nextItem == null)
               continue;
            string? itemText = nextItem.ToString();
            if (string.IsNullOrWhiteSpace(itemText))
               continue;
            if (itemText.Equals(trimmed, StringComparison.Ordinal))
               return true;
         }
         return false;
      }

      internal static int ComboFindExactIndex(ComboBox? pCombo, string? pText) {
         if (pCombo == null)
            return -1;
         if (pCombo.Items == null)
            return -1;
         if (string.IsNullOrWhiteSpace(pText))
            return -1;
         string trimmed = pText.Trim();
         int count = pCombo.Items.Count;
         for (int index = 0; index < count; index++) {
            object? nextItem = pCombo.Items[index];
            if (nextItem == null)
               continue;
            string? itemText = nextItem.ToString();
            if (string.IsNullOrWhiteSpace(itemText))
               continue;
            if (itemText.Equals(trimmed, StringComparison.Ordinal))
               return index;
         }
         return -1;
      }

      internal static void ComboSelectExactText(ComboBox? pCombo, string? pText) {
         if (pCombo == null)
            return;
         int index = ComboFindExactIndex(pCombo, pText);
         if (index >= 0)
            pCombo.SelectedIndex = index;
      }

      public static void ComboBoxSelectAll(ComboBox? pComboBox) {
         if (pComboBox == null)
            return;
         pComboBox.Focus();
         pComboBox.SelectAll();
         pComboBox.DroppedDown = true;
      }

   }
}
