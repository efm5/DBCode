namespace DBCode {
   namespace Themes {
      public sealed partial class FontPickerPanel : Panel {
         private void FontFamilyTextBoxPrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            TextBoxSelectAll(mFontFamilyNameTextBox);
         }

         private void FontFamilyDropDownPrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            fontFamilyComboBox.Focus();
            fontFamilyComboBox.DroppedDown = true;
         }

         private void FontSizePrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            TextBoxSelectAll(fontSizeTextBox);
         }

         private void FontSizeDropDownPrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            fontSizeComboBox.Focus();
            fontSizeComboBox.DroppedDown = true;
         }

         private void NormalStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            if (normalStyleCheckBox.Checked) {
               StyleHandlersOff();
               boldStyleCheckBox.Checked = false;
               italicsStyleCheckBox.Checked = false;
               underlineStyleCheckBox.Checked = false;
               strikethroughStyleCheckBox.Checked = false;
               StyleHandlersOn();
            }
         }

         private void BoldStyleCheckBox_CheckedChanged(object? pSender, EventArgs pEventArguments) {
            StyleHandlersOff();
            if (boldStyleCheckBox.Checked)
               normalStyleCheckBox.Checked = false;
            else
               MaybeRegularStyle();
            StyleHandlersOn();
         }

         private void ItalicsStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            StyleHandlersOff();
            if (italicsStyleCheckBox.Checked)
               normalStyleCheckBox.Checked = false;
            else
               MaybeRegularStyle();
            StyleHandlersOn();
         }

         private void UnderlineStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            StyleHandlersOff();
            if (underlineStyleCheckBox.Checked)
               normalStyleCheckBox.Checked = false;
            else
               MaybeRegularStyle();
            StyleHandlersOn();
         }

         private void StrikethroughStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            StyleHandlersOff();
            if (strikethroughStyleCheckBox.Checked)
               normalStyleCheckBox.Checked = false;
            else
               MaybeRegularStyle();
            StyleHandlersOn();
         }

         private void FontFamilyNameTextBox_TextChanged(object? pSender, EventArgs pEventArguments) {
            fontFamilyNameTextBox.Text = fontFamilyNameTextBox.Text.Trim();
            if (fontFamilyComboBox.Items.Contains(fontFamilyNameTextBox.Text)) {
               fontFamilyComboBox.Text = fontFamilyNameTextBox.Text;
            }
            else {
               _ = AskingAsync(new TM("That family name is not an installed font name.", "Unrecognized Font Family"));
               TextBoxSelectAll(fontFamilyNameTextBox);
            }
         }

         private void FontFamilyComboBox_SelectedIndexChanged(object? pSender, EventArgs pEventArguments) {
            if (int.TryParse(fontSizeTextBox.Text, out int oSize)) {
               string family = fontFamilyComboBox.SelectedItem.ToString();

               family = family.Replace("[FontFamily: Name=", string.Empty);
               family = family.Replace("]", string.Empty);
               fontDescriptionLabel.Text = string.Format("Selected font: {0}; {1}pt; {2}", family, oSize, GetFontStyle());
               fontFamilyNameTextBox.Text = family;
               fontFamilyNameTextBox.Refresh();
            }
            else
               fontDescriptionLabel.Text = string.Format("Selected font: {0} (size undetermined) {1}",
                  fontFamilyComboBox.DisplayMember, GetFontStyle());
            fontDescriptionLabel.Refresh();
         }

         private void FontSizeComboBox_SelectedIndexChanged(object? pSender, EventArgs pEventArguments) {
            string originalFontSize = fontSizeTextBox.Text;

            fontSizeTextBox.Text = fontSizeComboBox.Text;
            if (int.TryParse(fontSizeTextBox.Text, out int oSize)) {
               string family = fontFamilyComboBox.SelectedItem.ToString();

               family = family.Replace("[FontFamily: Name=", string.Empty);
               family = family.Replace("]", string.Empty);
               fontDescriptionLabel.Text = string.Format("Selected font: {0}; {1}pt; {2}", family, oSize, GetFontStyle());
            }
            else {
               fontSizeTextBox.Text = originalFontSize;
               fontDescriptionLabel.Text = string.Format("Selected font: {0} (size undetermined) {1}",
                  fontFamilyComboBox.DisplayMember, GetFontStyle());
               _ = AskingAsync(new TM("The font size could not be parsed.", "Parse ERROR"));
               ComboBoxSelectAll(fontSizeComboBox);
            }
            fontDescriptionLabel.Refresh();
            fontSizeTextBox.Refresh();
         }

         private void FontSizeTextBox_TextChanged(object? pSender, EventArgs pEventArguments) {
            string originalFontSize = fontFamilyComboBox.Text;

            if (int.TryParse(fontSizeTextBox.Text, out int oSize)) {
               string family = fontFamilyComboBox.SelectedItem.ToString();

               family = family.Replace("[FontFamily: Name=", string.Empty);
               family = family.Replace("]", string.Empty);
               fontDescriptionLabel.Text = string.Format("Selected font: {0}; {1}pt; {2}", family, oSize, GetFontStyle());
               if (!fontSizeComboBox.Items.Contains(fontSizeTextBox.Text))
                  fontSizeComboBox.Items.Add(fontSizeTextBox.Text);
               fontSizeComboBox.Text = fontSizeTextBox.Text;
            }
            else {
               fontSizeTextBox.Text = originalFontSize;
               fontDescriptionLabel.Text = string.Format("Selected font: {0} (size undetermined) {1}",
                  fontFamilyComboBox.DisplayMember, GetFontStyle());
               _ = AskingAsync(new TM("The font size could not be parsed.", "Parse ERROR"));
               TextBoxSelectAll(fontSizeTextBox);
            }
            fontDescriptionLabel.Refresh();
            fontSizeTextBox.Refresh();
         }

         private void FontFamilyComboBox_DrawItem(object? pSender, DrawItemEventArgs pEventArguments) {
            ComboBox comboBox = (ComboBox)sender;
            string fontFamily = (string)comboBox.Items[e.Index];
            Font font = new Font(fontFamily, comboBox.Font.SizeInPoints);

            e.DrawBackground();
            e.Graphics.DrawString(font.Name, font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
         }

         private void FontPickerCancelButton_Click(object? pSender, EventArgs pEventArguments) {
            HideFontPicker();
         }

         private void FontPickerOkButton_Click(object? pSender, EventArgs pEventArguments) {
            if (int.TryParse(fontSizeTextBox.Text, out int oSize)) {
               string family = fontFamilyNameTextBox.Text;
               Font font = new Font(family, oSize, GetFontStyle());

               switch (sFontUsage) {
                  case FontUsage.InterfaceFont:
                     sOptionsTheme.mInterfaceFont = CreateNewFont(font);
                     RecalculateAssociatedOffsets(sEditingTheme.mInterfaceFont);
                     break;
                  case FontUsage.MenuFont:
                     break;
                  case FontUsage.StatusBarFont:
                     sOptionsTheme.mStatusBarFont = CreateNewFont(font);
                     break;
                  case FontUsage.TextBoxFont:
                     sOptionsTheme.mTextBoxFont = CreateNewFont(font);
                     usages
                     break;
               }
            }
            HideFontPicker();
         }
      }
   }
}
