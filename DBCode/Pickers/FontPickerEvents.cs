namespace DBCode {
   namespace Themes {
      internal sealed partial class FontPickerPanel : Panel {
         protected override void OnHandleCreated(EventArgs pEventArgs) {
            ThrowIfNull(mForm, nameof(mForm));
            base.OnHandleCreated(pEventArgs);
            Dock = DockStyle.Fill;
            mForm.Opacity = 0;
            mForm.Location = new Point(-32000, -32000);
            LayoutControls();
            PerformLayout();
         }

         private void FontFamilyNameTextBox_TextChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mFontFamilyNameTextBox, nameof(mFontFamilyNameTextBox));
            ThrowIfNull(mFontFamilyComboBox, nameof(mFontFamilyComboBox));
            string trimmedText = mFontFamilyNameTextBox.Text.Trim();
            if (trimmedText != mFontFamilyNameTextBox.Text)
               mFontFamilyNameTextBox.Text = trimmedText;
            for (int i = 0; i < mFontFamilyComboBox.Items.Count; i++) {
               if (string.Equals((string)mFontFamilyComboBox.Items[i]!, trimmedText,
                  StringComparison.OrdinalIgnoreCase)) {
                  RemoveEventHandlers();
                  mFontFamilyComboBox.SelectedIndex = i;
                  AttachEventHandlers();
                  UpdateFontDescription();
                  return;
               }
            }
         }

         private void FontFamilyNameTextBox_Leave(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mFontFamilyNameTextBox, nameof(mFontFamilyNameTextBox));
            ThrowIfNull(mFontFamilyComboBox, nameof(mFontFamilyComboBox));
            string trimmedText = mFontFamilyNameTextBox.Text.Trim();
            bool found = false;
            for (int i = 0; i < mFontFamilyComboBox.Items.Count; i++) {
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

         private void FontFamilyComboBox_SelectedIndexChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mFontFamilyComboBox, nameof(mFontFamilyComboBox));
            ThrowIfNull(mFontFamilyNameTextBox, nameof(mFontFamilyNameTextBox));
            if (mFontFamilyComboBox.SelectedIndex >= 0) {
               string familyName = (string)mFontFamilyComboBox.SelectedItem!;
               RemoveEventHandlers();
               mFontFamilyNameTextBox.Text = familyName;
               AttachEventHandlers();
               UpdateFontDescription();
            }
         }

         private void FontFamilyComboBox_DrawItem(object? pSender, DrawItemEventArgs pEventArguments) {
            if (pSender == null || pEventArguments.Index < 0)
               return; // legitimate defensive guard: DrawItem fires during control teardown
            ComboBox comboBox = (ComboBox)pSender;
            string fontFamily = (string)comboBox.Items[pEventArguments.Index]!;
            pEventArguments.DrawBackground();
            try {
               using Font font = new Font(fontFamily, comboBox.Font.SizeInPoints);
               using SolidBrush brush = new SolidBrush(pEventArguments.ForeColor);
               pEventArguments.Graphics.DrawString(font.Name, font, brush,
                  pEventArguments.Bounds.X, pEventArguments.Bounds.Y);
            }
            catch {
               using SolidBrush brush = new SolidBrush(pEventArguments.ForeColor);
               pEventArguments.Graphics.DrawString(fontFamily, comboBox.Font, brush,
                  pEventArguments.Bounds.X, pEventArguments.Bounds.Y);
            }
         }

         private void FontSizeComboBox_DrawItem(object? pSender, DrawItemEventArgs pEventArguments) {
            if (pSender == null || pEventArguments.Index < 0)
               return;
            ComboBox comboBox = (ComboBox)pSender;
            string sizeText = (string)comboBox.Items[pEventArguments.Index]!;
            pEventArguments.DrawBackground();
            using SolidBrush brush = new SolidBrush(pEventArguments.ForeColor);
            pEventArguments.Graphics.DrawString(sizeText, comboBox.Font, brush,
               pEventArguments.Bounds.X, pEventArguments.Bounds.Y);
         }

         private void NormalStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mNormalStyleCluster, nameof(mNormalStyleCluster));
            if (mNormalStyleCluster.mScalableCheckBox.Checked) {
               RemoveEventHandlers();
               mBoldStyleCluster.mScalableCheckBox.Checked = false;
               mItalicsStyleCluster.mScalableCheckBox.Checked = false;
               mUnderlineStyleCluster.mScalableCheckBox.Checked = false;
               mStrikethroughStyleCluster.mScalableCheckBox.Checked = false;
               AttachEventHandlers();
            }
            UpdateFontDescription();
         }

         private void BoldStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            RemoveEventHandlers();
            if (mBoldStyleCluster.mScalableCheckBox.Checked)
               mNormalStyleCluster.mScalableCheckBox.Checked = false;
            else
               MaybeRegularStyle();
            AttachEventHandlers();
            UpdateFontDescription();
         }

         private void ItalicsStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            RemoveEventHandlers();
            if (mItalicsStyleCluster.mScalableCheckBox.Checked)
               mNormalStyleCluster.mScalableCheckBox.Checked = false;
            else
               MaybeRegularStyle();
            AttachEventHandlers();
            UpdateFontDescription();
         }

         private void UnderlineStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            RemoveEventHandlers();
            if (mUnderlineStyleCluster.mScalableCheckBox.Checked)
               mNormalStyleCluster.mScalableCheckBox.Checked = false;
            else
               MaybeRegularStyle();
            AttachEventHandlers();
            UpdateFontDescription();
         }

         private void StrikethroughStyleCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            RemoveEventHandlers();
            if (mStrikethroughStyleCluster.mScalableCheckBox.Checked)
               mNormalStyleCluster.mScalableCheckBox.Checked = false;
            else
               MaybeRegularStyle();
            AttachEventHandlers();
            UpdateFontDescription();
         }

         private void FontSizeTextBox_TextChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mFontSizeTextBox, nameof(mFontSizeTextBox));
            ThrowIfNull(mFontSizeComboBox, nameof(mFontSizeComboBox));
            if (!int.TryParse(mFontSizeTextBox.Text, out int size))
               return;
            if (size < 1 || size > 999)
               return;
            RemoveEventHandlers();
            bool found = false;
            for (int i = 0; i < mFontSizeComboBox.Items.Count; i++) {
               if (string.Equals((string)mFontSizeComboBox.Items[i]!, size.ToString(), StringComparison.Ordinal)) {
                  mFontSizeComboBox.SelectedIndex = i;
                  found = true;
                  break;
               }
            }
            if (!found)
               mFontSizeComboBox.SelectedIndex = -1;
            AttachEventHandlers();
            UpdateFontDescription();
         }

         private void FontSizeTextBox_Leave(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mFontSizeTextBox, nameof(mFontSizeTextBox));
            if (!int.TryParse(mFontSizeTextBox.Text, out int size) || size < 1 || size > 999) {
               TimedMessage("The font size must be a number between 1 and 999.", "Invalid Font Size");
               TextBoxSelectAll(mFontSizeTextBox);
            }
         }

         private void FontSizeComboBox_SelectedIndexChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mFontSizeComboBox, nameof(mFontSizeComboBox));
            ThrowIfNull(mFontSizeTextBox, nameof(mFontSizeTextBox));
            if (mFontSizeComboBox.SelectedIndex >= 0) {
               string sizeText = (string)mFontSizeComboBox.SelectedItem!;
               RemoveEventHandlers();
               mFontSizeTextBox.Text = sizeText;
               AttachEventHandlers();
               UpdateFontDescription();
            }
         }

         private void OkButton_Click(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mTheme, nameof(mTheme));
            ThrowIfNull(mThemePanel, nameof(mThemePanel));
            ThrowIfNull(mFontSizeTextBox, nameof(mFontSizeTextBox));
            ThrowIfNull(mFontFamilyNameTextBox, nameof(mFontFamilyNameTextBox));
            ThrowIfNull(mFontFamilyComboBox, nameof(mFontFamilyComboBox));
            ThrowIfNull(mWorkingFont, nameof(mWorkingFont));
            if (!int.TryParse(mFontSizeTextBox.Text, out int size) || size < 1 || size > 999) {
               TimedMessage("The font size must be a number between 1 and 999.", "Invalid Font Size");
               TextBoxSelectAll(mFontSizeTextBox);
               return;
            }
            string familyName = mFontFamilyNameTextBox.Text;
            bool found = false;
            for (int i = 0; i < mFontFamilyComboBox.Items.Count; i++) {
               if (string.Equals((string)mFontFamilyComboBox.Items[i]!, familyName,
                  StringComparison.OrdinalIgnoreCase)) {
                  found = true;
                  break;
               }
            }
            if (!found) {
               TimedMessage("That family name is not an installed font name.", "Unrecognized Font Family");
               TextBoxSelectAll(mFontFamilyNameTextBox);
               return;
            }
            FontStyle style = GetFontStyle();
            try {
               Font newFont = new Font(familyName, size, style);
               mTheme.mFonts[(int)mFontUsage] = newFont;
               ThemePanel.mRepaint = true;
               RecalculateAssociatedOffsets(newFont);
               ThemePanel.RestoreFromFontPickerPanel(mTheme);
            }
            catch (Exception pException) {
               TimedMessage($"Could not create font: {pException.Message}", "Font Creation Error");
            }
         }

         private void CancelButton_Click(object? pSender, EventArgs pEventArguments) {
            ThemePanel.RestoreFromFontPickerPanel();
         }
      }
   }
}
