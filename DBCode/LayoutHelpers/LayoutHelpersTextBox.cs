namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool TextBoxHasText(TextBox pTextBox) {
         if (pTextBox == null)
            return false;
         if (string.IsNullOrWhiteSpace(pTextBox.Text))
            return false;
         return true;
      }

      internal static bool TextBoxHasNoText(TextBox pTextBox) {
         if (pTextBox == null)
            return true;
         if (string.IsNullOrWhiteSpace(pTextBox.Text))
            return true;
         return false;
      }

      internal static string TextBoxTextOrEmpty(TextBox pTextBox) {
         if (pTextBox == null)
            return string.Empty;
         if (string.IsNullOrWhiteSpace(pTextBox.Text))
            return string.Empty;
         return pTextBox.Text.Trim();
      }

      internal static void TextBoxSetText(TextBox pTextBox, string pText) {
         if (pTextBox == null)
            return;
         if (string.IsNullOrWhiteSpace(pText)) {
            pTextBox.Text = string.Empty;
            return;
         }
         pTextBox.Text = pText.Trim();
      }

      internal static void TextBoxClear(TextBox pTextBox) {
         if (pTextBox == null)
            return;
         pTextBox.Clear();
      }

      internal static void TextBoxEnable(TextBox pTextBox) {
         if (pTextBox == null)
            return;
         pTextBox.Enabled = true;
      }

      internal static void TextBoxDisable(TextBox pTextBox) {
         if (pTextBox == null)
            return;
         pTextBox.Enabled = false;
      }

      internal static bool TextBoxIsEnabled(TextBox pTextBox) {
         if (pTextBox == null)
            return false;
         return pTextBox.Enabled;
      }

      internal static bool TextBoxIsDisabled(TextBox pTextBox) {
         if (pTextBox == null)
            return true;
         return !pTextBox.Enabled;
      }

      internal static void TextBoxShow(TextBox pTextBox) {
         if (pTextBox == null)
            return;
         pTextBox.Visible = true;
      }

      internal static void TextBoxHide(TextBox pTextBox) {
         if (pTextBox == null)
            return;
         pTextBox.Visible = false;
      }

      internal static bool TextBoxIsVisible(TextBox pTextBox) {
         if (pTextBox == null)
            return false;
         return pTextBox.Visible;
      }

      internal static bool TextBoxIsHidden(TextBox pTextBox) {
         if (pTextBox == null)
            return true;
         return !pTextBox.Visible;
      }

      internal static void TextBoxSetReadOnly(TextBox pTextBox, bool pReadOnly) {
         if (pTextBox == null)
            return;
         pTextBox.ReadOnly = pReadOnly;
      }

      internal static bool TextBoxIsReadOnly(TextBox pTextBox) {
         if (pTextBox == null)
            return false;
         return pTextBox.ReadOnly;
      }

      internal static void TextBoxSetMaxLength(TextBox pTextBox, int pLength) {
         if (pTextBox == null)
            return;
         if (pLength < 0)
            return;
         pTextBox.MaxLength = pLength;
      }

      internal static void TextBoxSelectAll(TextBox pTextBox) {
         if (pTextBox == null)
            return;
         pTextBox.SelectAll();
      }

      internal static void TextBoxSetSelection(TextBox pTextBox, int pStart, int pLength) {
         if (pTextBox == null)
            return;
         if (pStart < 0)
            pStart = 0;
         if (pLength < 0)
            pLength = 0;
         pTextBox.Select(pStart, pLength);
      }

      internal static void TextBoxAppendLine(TextBox pTextBox, string pText) {
         if (pTextBox == null)
            return;
         if (string.IsNullOrWhiteSpace(pText)) {
            pTextBox.AppendText(string.Empty);
            return;
         }
         pTextBox.AppendText(pText.Trim());
         pTextBox.AppendText("\r\n");
      }

      internal static void TextBoxAppend(TextBox pTextBox, string pText) {
         if (pTextBox == null)
            return;
         if (string.IsNullOrWhiteSpace(pText)) {
            pTextBox.AppendText(string.Empty);
            return;
         }
         pTextBox.AppendText(pText.Trim());
      }
   }
}
