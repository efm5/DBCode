namespace DBCode {
   namespace Themes {
      public sealed partial class FontPickerPanel : Panel {
         private void FamilyPrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            TextBoxSelectAll(mFontFamilyNameTextBox!);
         }

         private void FontFamilyNameTextBox_TextChanged(object? pSender, EventArgs pEventArguments) {
            string trimmedText = mFontFamilyNameTextBox!.Text.Trim();
            if (trimmedText != mFontFamilyNameTextBox.Text)
               mFontFamilyNameTextBox.Text = trimmedText;
            for (int i = 0; i < mFontFamilyComboBox!.Items.Count; i++) {
               if (string.Equals((string)mFontFamilyComboBox.Items[i]!, trimmedText,
                  StringComparison.OrdinalIgnoreCase)) {
                  RemoveEventHandlers();
                  mFontFamilyComboBox.SelectedIndex = i;
                  RestoreFontPickerHandlers();
                  UpdateFontDescription();
                  return;
               }
            }
         }

         private void FontFamilyNameTextBox_Leave(object? pSender, EventArgs pEventArguments) {
            string trimmedText = mFontFamilyNameTextBox!.Text.Trim();
            bool found = false;
            for (int i = 0; i < mFontFamilyComboBox!.Items.Count; i++) {
               if (string.Equals((string)mFontFamilyComboBox.Items[i]!, trimmedText,
                  StringComparison.OrdinalIgnoreCase)) {
                  found = true;
                  break;
               }
            }
            if (!found) {
               TimedMessage("That family name is not an installed font name.", "Unrecognized Font Family");
               TextBoxSelectAll(mFontFamilyNameTextBox);
            }
         }

         private void FontDropDownPrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            mFontFamilyComboBox!.Focus();
            mFontFamilyComboBox.DroppedDown = true;
         }

         private void FontFamilyComboBox_SelectedIndexChanged(object? pSender, EventArgs pEventArguments) {
            if (mFontFamilyComboBox!.SelectedIndex >= 0) {
               string familyName = (string)mFontFamilyComboBox.SelectedItem!;
               RemoveEventHandlers();
               mFontFamilyNameTextBox!.Text = familyName;
               RestoreFontPickerHandlers();
               UpdateFontDescription();
            }
         }

         private void FontFamilyComboBox_DrawItem(object? pSender, DrawItemEventArgs pEventArguments) {
            if ((pSender == null) || (pEventArguments.Index < 0))
               return;
            ComboBox? comboBox = (ComboBox)pSender as ComboBox;
            if (comboBox == null)
               return;
            string fontFamily = (string)comboBox.Items[pEventArguments.Index]!;
            try {
               Font font = new Font(fontFamily, comboBox.Font.SizeInPoints);
               pEventArguments.DrawBackground();
               pEventArguments.Graphics.DrawString(font.Name, font,
                  new SolidBrush(pEventArguments.ForeColor),
                  pEventArguments.Bounds.X, pEventArguments.Bounds.Y);
            }
            catch {
               pEventArguments.DrawBackground();
               pEventArguments.Graphics.DrawString(fontFamily, comboBox.Font,
                  new SolidBrush(pEventArguments.ForeColor),
                  pEventArguments.Bounds.X, pEventArguments.Bounds.Y);
            }
         }

         private void NormalStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            if (mNormalStyleCheckBox!.Checked) {
               RemoveEventHandlers();
               mBoldStyleCheckBox!.Checked = false;
               mItalicsStyleCheckBox!.Checked = false;
               mUnderlineStyleCheckBox!.Checked = false;
               mStrikethroughStyleCheckBox!.Checked = false;
               RestoreFontPickerHandlers();
            }
            UpdateFontDescription();
         }

         private void BoldStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            RemoveEventHandlers();
            if (mBoldStyleCheckBox!.Checked)
               mNormalStyleCheckBox!.Checked = false;
            else
               MaybeRegularStyle();
            RestoreFontPickerHandlers();
            UpdateFontDescription();
         }

         private void ItalicsStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            RemoveEventHandlers();
            if (mItalicsStyleCheckBox!.Checked)
               mNormalStyleCheckBox!.Checked = false;
            else
               MaybeRegularStyle();
            RestoreFontPickerHandlers();
            UpdateFontDescription();
         }

         private void UnderlineStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            RemoveEventHandlers();
            if (mUnderlineStyleCheckBox!.Checked)
               mNormalStyleCheckBox!.Checked = false;
            else
               MaybeRegularStyle();
            RestoreFontPickerHandlers();
            UpdateFontDescription();
         }

         private void StrikethroughStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            RemoveEventHandlers();
            if (mStrikethroughStyleCheckBox!.Checked)
               mNormalStyleCheckBox!.Checked = false;
            else
               MaybeRegularStyle();
            RestoreFontPickerHandlers();
            UpdateFontDescription();
         }

         private void FontSizePrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            TextBoxSelectAll(mFontSizeTextBox!);
         }

         private void FontSizeTextBox_TextChanged(object? pSender, EventArgs pEventArguments) {
            if (!int.TryParse(mFontSizeTextBox!.Text, out int size))
               return;
            if (size < 1 || size > 999)
               return;
            RemoveEventHandlers();
            bool found = false;
            for (int i = 0; i < mFontSizeComboBox!.Items.Count; i++) {
               if (string.Equals((string)mFontSizeComboBox.Items[i]!, size.ToString(), StringComparison.Ordinal)) {
                  mFontSizeComboBox.SelectedIndex = i;
                  found = true;
                  break;
               }
            }
            if (!found)
               mFontSizeComboBox.SelectedIndex = -1;
            RestoreFontPickerHandlers();
            UpdateFontDescription();
         }

         private void FontSizeTextBox_Leave(object? pSender, EventArgs pEventArguments) {
            if (!int.TryParse(mFontSizeTextBox!.Text, out int size) || size < 1 || size > 999) {
               TimedMessage("The font size must be a number between 1 and 999.", "Invalid Font Size");
               TextBoxSelectAll(mFontSizeTextBox!);
            }
         }

         private void FontSizeDropDownPrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            mFontSizeComboBox!.Focus();
            mFontSizeComboBox.DroppedDown = true;
         }

         private void FontSizeComboBox_SelectedIndexChanged(object? pSender, EventArgs pEventArguments) {
            if (mFontSizeComboBox!.SelectedIndex >= 0) {
               string sizeText = (string)mFontSizeComboBox.SelectedItem!;
               RemoveEventHandlers();
               mFontSizeTextBox!.Text = sizeText;
               RestoreFontPickerHandlers();
               UpdateFontDescription();
            }
         }

         private void HelpButton_Click(object? pSender, EventArgs pEventArguments) {
            TimedMessage("Select a font family, size, and style for the selected usage.", "Font Picker Help");
         }

         private void OkButton_Click(object? pSender, EventArgs pEventArguments) {
            if (!int.TryParse(mFontSizeTextBox!.Text, out int size) || size < 1 || size > 999) {
               TimedMessage("The font size must be a number between 1 and 999.", "Invalid Font Size");
               TextBoxSelectAll(mFontSizeTextBox!);
               return;
            }
            string familyName = mFontFamilyNameTextBox!.Text;
            bool found = false;
            for (int i = 0; i < mFontFamilyComboBox!.Items.Count; i++) {
               if (string.Equals((string)mFontFamilyComboBox.Items[i]!, familyName,
                  StringComparison.OrdinalIgnoreCase)) {
                  found = true;
                  break;
               }
            }
            if (!found) {
               TimedMessage("That family name is not an installed font name.", "Unrecognized Font Family");
               TextBoxSelectAll(mFontFamilyNameTextBox!);
               return;
            }
            FontStyle style = GetFontStyle();
            try {
               Font newFont = new Font(familyName, size, style);

               if (mFontUsage != null && mTheme != null) {
                  mTheme.mFonts[(int)mFontUsage] = newFont;
               }
               if (mWorkingFont!.Equals(mInitialFont))
                  ThemePanel.mRepaint = false;
               else
                  ThemePanel.mRepaint = true;
               RecalculateAssociatedOffsets(newFont);
               mThemePanel?.UpdateFontLabels(mFontUsage);
               ThemePanel.RestoreFromFontPickerPanel();
            }
            catch (Exception ex) {
               TimedMessage($"Could not create font: {ex.Message}", "Font Creation Error");
            }
         }

         private void CancelButton_Click(object? pSender, EventArgs pEventArguments) {
            ThemePanel.RestoreFromFontPickerPanel();
         }
      }
   }
}
