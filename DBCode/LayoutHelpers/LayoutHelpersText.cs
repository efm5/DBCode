namespace DBCode {
   internal static partial class LayoutHelpers {
#pragma warning disable IDE1006
      public static void SelectPartOfText(RichTextBox pRichTextBox, float pPart = 0.5f) {
         pRichTextBox.Select(0, (int)(pRichTextBox.Text.Length * pPart));
         HomeTextBoxInsertionPoint(pRichTextBox);
         pRichTextBox.Refresh();
      }

      public static void SelectPartOfText(TextBox pTextBox, float pPart = 0.5f) {
         pTextBox.Select(0, (int)(pTextBox.Text.Length * pPart));
         HomeTextBoxInsertionPoint(pTextBox);
         pTextBox.Refresh();
      }

      public static void HomeTextBoxInsertionPoint(RichTextBox pRichTextBox) {
         _ = SendMessage(pRichTextBox.Handle, EM_LINESCROLL, 0, 0);
      }

      public static void HomeTextBoxInsertionPoint(TextBox pTextBox) {
         _ = SendMessage(pTextBox.Handle, EM_LINESCROLL, 0, 0);
      }

#pragma warning restore IDE1006
   }
}
