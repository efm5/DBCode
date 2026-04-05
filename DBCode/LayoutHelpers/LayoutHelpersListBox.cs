namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool SelectionHasItems(ListBox? pListBox) {
         if (pListBox == null)
            return false;
         return pListBox.Items.Count > 0;
      }

      internal static bool SelectionHasNoItems(ListBox? pListBox) {
         if (pListBox == null)
            return true;
         return pListBox.Items.Count == 0;
      }

      internal static bool SelectionHasSelection(ListBox? pListBox) {
         if (pListBox == null)
            return false;
         return pListBox.SelectedIndex >= 0;
      }

      internal static bool SelectionHasNoSelection(ListBox? pListBox) {
         if (pListBox == null)
            return true;
         return pListBox.SelectedIndex < 0;
      }

      internal static object? SelectionSelectedItemOrNull(ListBox? pListBox) {
         if (pListBox == null)
            return null;
         return pListBox.SelectedItem;
      }

      internal static int SelectionSelectedIndexOrMinusOne(ListBox? pListBox) {
         if (pListBox == null)
            return -1;
         return pListBox.SelectedIndex;
      }

      internal static void SelectionClear(ListBox? pListBox) {
         if (pListBox == null)
            return;
         pListBox.ClearSelected();
      }

      internal static void SelectionSelectIndex(ListBox? pListBox, int pIndex) {
         if (pListBox == null)
            return;
         if (pIndex < 0)
            return;
         if (pIndex >= pListBox.Items.Count)
            return;
         pListBox.SelectedIndex = pIndex;
      }

      internal static void SelectionSelectFirst(ListBox? pListBox) {
         if (pListBox == null)
            return;
         if (pListBox.Items.Count == 0)
            return;
         pListBox.SelectedIndex = 0;
      }

      internal static void SelectionSelectLast(ListBox? pListBox) {
         if (pListBox == null)
            return;
         int count = pListBox.Items.Count;
         if (count == 0)
            return;
         pListBox.SelectedIndex = count - 1;
      }

      internal static bool ListBoxHasItems(ListBox? pListBox) {
         if (pListBox == null)
            return false;
         if (pListBox.Items == null)
            return false;
         return pListBox.Items.Count > 0;
      }

      internal static bool ListBoxHasNoItems(ListBox? pListBox) {
         if (pListBox == null)
            return true;
         if (pListBox.Items == null)
            return true;
         return pListBox.Items.Count == 0;
      }

      internal static int ListBoxItemCount(ListBox? pListBox) {
         if (pListBox == null)
            return 0;
         if (pListBox.Items == null)
            return 0;
         return pListBox.Items.Count;
      }

      internal static void ListBoxClear(ListBox? pListBox) {
         if (pListBox == null)
            return;
         if (pListBox.Items == null)
            return;
         pListBox.Items.Clear();
      }

      internal static void ListBoxAddItem(ListBox? pListBox, object? pItem) {
         if (pListBox == null)
            return;
         if (pItem == null)
            return;
         pListBox.Items.Add(pItem);
      }

      internal static void ListBoxRemoveItem(ListBox? pListBox, object? pItem) {
         if (pListBox == null)
            return;
         if (pItem == null)
            return;
         pListBox.Items.Remove(pItem);
      }

      internal static bool ListBoxContainsItem(ListBox? pListBox, object? pItem) {
         if (pListBox == null)
            return false;
         if (pItem == null)
            return false;
         if (pListBox.Items == null)
            return false;
         return pListBox.Items.Contains(pItem);
      }

      internal static bool ListBoxHasSelection(ListBox? pListBox) {
         if (pListBox == null)
            return false;
         return pListBox.SelectedIndex >= 0;
      }

      internal static bool ListBoxHasNoSelection(ListBox? pListBox) {
         if (pListBox == null)
            return true;
         return pListBox.SelectedIndex < 0;
      }

      internal static int ListBoxSelectedIndexOrMinusOne(ListBox? pListBox) {
         if (pListBox == null)
            return -1;
         return pListBox.SelectedIndex;
      }

      internal static object? ListBoxSelectedItemOrNull(ListBox? pListBox) {
         if (pListBox == null)
            return null;
         return pListBox.SelectedItem;
      }

      internal static string ListBoxSelectedTextOrEmpty(ListBox? pListBox) {
         if (pListBox == null)
            return string.Empty;
         if (pListBox.SelectedItem == null)
            return string.Empty;
         string text = pListBox.SelectedItem.ToString() ?? string.Empty;
         if (string.IsNullOrWhiteSpace(text))
            return string.Empty;
         return text.Trim();
      }

      internal static void ListBoxSelectIndex(ListBox? pListBox, int pIndex) {
         if (pListBox == null)
            return;
         if (pListBox.Items == null)
            return;
         if (pIndex < 0)
            return;
         if (pIndex >= pListBox.Items.Count)
            return;
         pListBox.SelectedIndex = pIndex;
      }

      internal static void ListBoxSelectFirst(ListBox? pListBox) {
         if (pListBox == null)
            return;
         if (pListBox.Items == null)
            return;
         if (pListBox.Items.Count == 0)
            return;
         pListBox.SelectedIndex = 0;
      }

      internal static void ListBoxSelectLast(ListBox? pListBox) {
         if (pListBox == null)
            return;
         if (pListBox.Items == null)
            return;
         int count = pListBox.Items.Count;
         if (count == 0)
            return;
         pListBox.SelectedIndex = count - 1;
      }

      internal static int ListBoxFindExactIndex(ListBox? pListBox, string? pText) {
         if (pListBox == null)
            return -1;
         if (pListBox.Items == null)
            return -1;
         if (string.IsNullOrWhiteSpace(pText))
            return -1;
         string trimmed = pText.Trim();
         for (int i = 0; i < pListBox.Items.Count; i++) {
            object? item = pListBox.Items[i];
            if (item == null)
               continue;
            string text = item.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(text))
               continue;
            if (text.Equals(trimmed, StringComparison.Ordinal))
               return i;
         }
         return -1;
      }

      internal static void ListBoxSelectExactText(ListBox? pListBox, string? pText) {
         if (pListBox == null)
            return;
         int index = ListBoxFindExactIndex(pListBox, pText);
         if (index >= 0)
            pListBox.SelectedIndex = index;
      }

      internal static void ListBoxEnable(ListBox? pListBox) {
         if (pListBox == null)
            return;
         pListBox.Enabled = true;
      }

      internal static void ListBoxDisable(ListBox? pListBox) {
         if (pListBox == null)
            return;
         pListBox.Enabled = false;
      }

      internal static bool ListBoxIsEnabled(ListBox? pListBox) {
         if (pListBox == null)
            return false;
         return pListBox.Enabled;
      }

      internal static bool ListBoxIsDisabled(ListBox? pListBox) {
         if (pListBox == null)
            return true;
         return !pListBox.Enabled;
      }

      internal static void ListBoxShow(ListBox? pListBox) {
         if (pListBox == null)
            return;
         pListBox.Visible = true;
      }

      internal static void ListBoxHide(ListBox? pListBox) {
         if (pListBox == null)
            return;
         pListBox.Visible = false;
      }

      internal static bool ListBoxIsVisible(ListBox? pListBox) {
         if (pListBox == null)
            return false;
         return pListBox.Visible;
      }

      internal static bool ListBoxIsHidden(ListBox? pListBox) {
         if (pListBox == null)
            return true;
         return !pListBox.Visible;
      }

      internal static void ListBoxSetForeColor(ListBox? pListBox, Color pColor) {
         if (pListBox == null)
            return;
         pListBox.ForeColor = pColor;
      }

      internal static void ListBoxSetBackColor(ListBox? pListBox, Color pColor) {
         if (pListBox == null)
            return;
         pListBox.BackColor = pColor;
      }

      internal static void ListBoxSetPadding(ListBox? pListBox, Padding pPadding) {
         if (pListBox == null)
            return;
         pListBox.Padding = pPadding;
      }
   }
}
