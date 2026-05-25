namespace DBCode {
   internal sealed class PlainRichTextBox : RichTextBox {
      internal bool mAllowFormatting { get; set; }
      internal event EventHandler? ViewChanged;
      private static bool mIsProtectionActive {
         get {
            return mUiState.mEnforceFormattingProtection
               && mCurrentLanguage != LanguageKind.PlainText;
         }
      }
      protected override void WndProc(ref Message pMessage) {
         if (pMessage.Msg == EM_SETCHARFORMAT && !mAllowFormatting && mIsProtectionActive) {
            pMessage.Result = IntPtr.Zero;
            return;
         }
         if (pMessage.Msg == WM_PASTE && mIsProtectionActive) {
            if (Clipboard.ContainsText(TextDataFormat.UnicodeText))
               Paste(DataFormats.GetFormat(DataFormats.UnicodeText));
            else if (Clipboard.ContainsText(TextDataFormat.Text))
               Paste(DataFormats.GetFormat(DataFormats.Text));
            return;
         }
         base.WndProc(ref pMessage);
         if (pMessage.Msg == WM_PAINT)
            ViewChanged?.Invoke(this, EventArgs.Empty);
      }
   }
}
