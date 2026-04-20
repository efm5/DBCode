namespace DBCode {
   public sealed partial class MainForm : Form {
      public void FontPickerHelp() {
         //try {
         //   if (!File.Exists(sFontPickerHelp)) {
         //      _ = AskingAsync(new TM("Missing Help File",
         //         string.Format("DBCode's Font Picker help HTML file" +
         //            ":{0}{1}{0}" +
         //            "could not be found" + "!{0}{0}" +
         //            "You may locate and restore the missing" + " " +
         //            "file then click the “Retry” button below" + ".{0}{0}" +
         //            "Otherwise, click “Cancel” to abort this help request" + ".",
         //            Environment.NewLine, sFontPickerHelp),
         //          string.Empty,
         //          "Cancel", TIMED_MESSAGE_Cancel_Ask,
         //          "Retry", TIMED_MESSAGE_Retry_Font_Picker_Help,
         //          string.Empty, TIMED_MESSAGE_Ignore,
         //          TIMED_MESSAGE_Cancel_Ask, 0));
         //      return;
         //   }
         //   Process process = new Process();
         //   process.StartInfo = new ProcessStartInfo(sFontPickerHelp) {
         //      UseShellExecute = true
         //   };
         //   process.Start();
         //   Thread.Sleep(100);
         //}
         //catch (Exception pException) {
         //   _ = AskingAsync(new TM("FontPickerHelp", pException));
         //}
      }

      private void LayoutFontPicker() {
         //DEBUG efm5 2026 04 10 this method was too long to pass The chunkerIt has been moved into a text File: LayoutFontPicker.TXT
      }

      private void ShowFontPicker(FontUsage pFontUsage) {
         if ((mFontPickerBottomPanel == null) || (mPickFontPanel == null) || (mPickFontTitleLabel == null) ||
           (mFontPickerOkButton == null) || (mFontPickerCancelButton == null) || (mFontFamilyNameTextBox == null))
            return;
         mFontFamilyComboBox?.SelectedIndexChanged -= FontFamilyComboBox_SelectedIndexChanged;
         mFontSizeComboBox?.SelectedIndexChanged -= FontSizeComboBox_SelectedIndexChanged;
         mFontUsage = pFontUsage;
         //PrepareOptionsDialog(UIContext.FontPicker);
         //mFromFontPicker = true;
         mPickFontPanel?.Size = CreateSizeFromFloats(Size.Width * 0.8f, (Size.Height * 0.8f) - mTitleBarHeight);
         mPickFontTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
         mFontPickerBottomPanel.Dock = DockStyle.None;
         LayoutFontPicker();
         //ShowPanel(sFontPickerBorderPanel);
         mFontPickerCancelButton.Left = mFontPickerBottomPanel.Width - mFontPickerCancelButton.Width - mCancelOffset;
         mFontPickerOkButton.Left = mFontPickerCancelButton.Left - mFontPickerOkButton.Width - mOkOffset;
         mFontPickerCancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
         mFontPickerOkButton?.Anchor = AnchorStyles.Top | AnchorStyles.Right;
         mFontPickerBottomPanel?.Dock = DockStyle.Bottom;
         CenterControlHorizontally(mPickFontPanel, mPickFontTitleLabel);
         TextBoxSelectAll(mFontFamilyNameTextBox);
         mFontSizeComboBox?.SelectedIndexChanged += FontSizeComboBox_SelectedIndexChanged;
         mFontFamilyComboBox?.SelectedIndexChanged += FontFamilyComboBox_SelectedIndexChanged;
      }

      private FontStyle GetFontStyle() {
         return FontStyle.Regular;//DEBUG efm5 2026 03 30 temporary
         //FontStyle style = FontStyle.Regular;

         //if (normalStyleCheckBox.Checked)
         //   return style;
         //else {
         //   if (boldStyleCheckBox.Checked)
         //      style |= FontStyle.Bold;
         //   if (italicsStyleCheckBox.Checked)
         //      style |= FontStyle.Italic;
         //   if (underlineStyleCheckBox.Checked)
         //      style |= FontStyle.Underline;
         //   if (strikethroughStyleCheckBox.Checked)
         //      style |= FontStyle.Strikeout;
         //   return style;
         //}
      }

      private void StyleHandlersOn() {
         //normalStyleCheckBox.Click += NormalStyleCheckBox_Click;
         //boldStyleCheckBox.Click += BoldStyleCheckBox_CheckedChanged;
         //italicsStyleCheckBox.Click += ItalicsStyleCheckBox_Click;
         //underlineStyleCheckBox.Click += UnderlineStyleCheckBox_Click;
         //strikethroughStyleCheckBox.Click += StrikethroughStyleCheckBox_Click;
      }

      private void StyleHandlersOff() {
         //normalStyleCheckBox.Click -= NormalStyleCheckBox_Click;
         //boldStyleCheckBox.Click -= BoldStyleCheckBox_CheckedChanged;
         //italicsStyleCheckBox.Click -= ItalicsStyleCheckBox_Click;
         //underlineStyleCheckBox.Click -= UnderlineStyleCheckBox_Click;
         //strikethroughStyleCheckBox.Click -= StrikethroughStyleCheckBox_Click;
      }

      private void MaybeRegularStyle() {
         //if ((boldStyleCheckBox.Checked == false) &&
         //(italicsStyleCheckBox.Checked == false) &&
         //(underlineStyleCheckBox.Checked == false) &&
         //(strikethroughStyleCheckBox.Checked == false))
         //   normalStyleCheckBox.Checked = true;
      }

      private void RemoveFontPickerHandlers() {
         //fontFamilyNameTextBox.TextChanged -= (FontFamilyNameTextBox_TextChanged);
         //fontFamilyComboBox.SelectedIndexChanged -= (FontFamilyComboBox_SelectedIndexChanged);
         //fontSizeTextBox.TextChanged -= (FontSizeTextBox_TextChanged);
         //fontSizeComboBox.SelectedIndexChanged -= (FontSizeComboBox_SelectedIndexChanged);
         //normalStyleCheckBox.Click -= (NormalStyleCheckBox_Click);
         //boldStyleCheckBox.CheckedChanged -= (BoldStyleCheckBox_CheckedChanged);
         //underlineStyleCheckBox.Click -= (UnderlineStyleCheckBox_Click);
         //italicsStyleCheckBox.Click -= (ItalicsStyleCheckBox_Click);
         //strikethroughStyleCheckBox.Click -= (StrikethroughStyleCheckBox_Click);
      }

      private void RestoreFontPickerHandlers() {
         //fontFamilyNameTextBox.TextChanged += (FontFamilyNameTextBox_TextChanged);
         //fontFamilyComboBox.SelectedIndexChanged += (FontFamilyComboBox_SelectedIndexChanged);
         //fontSizeTextBox.TextChanged += (FontSizeTextBox_TextChanged);
         //fontSizeComboBox.SelectedIndexChanged += (FontSizeComboBox_SelectedIndexChanged);
         //normalStyleCheckBox.Click += (NormalStyleCheckBox_Click);
         //boldStyleCheckBox.CheckedChanged += (BoldStyleCheckBox_CheckedChanged);
         //underlineStyleCheckBox.Click += (UnderlineStyleCheckBox_Click);
         //italicsStyleCheckBox.Click += (ItalicsStyleCheckBox_Click);
         //strikethroughStyleCheckBox.Click += (StrikethroughStyleCheckBox_Click);
      }
   }
}
