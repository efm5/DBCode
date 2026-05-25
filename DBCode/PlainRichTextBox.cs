namespace DBCode {
   internal sealed class PlainRichTextBox : RichTextBox {
      private const int EM_SETCHARFORMAT = 0x0444;
      private const int WM_PASTE = 0x0302;
      internal bool mAllowFormatting { get; set; }
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
      }
   }
}
