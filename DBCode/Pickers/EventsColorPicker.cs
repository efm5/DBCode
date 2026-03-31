namespace DBCode {
   public sealed partial class MainForm : Form {
      #region named
      private void NamedColorsComboBox_KeyUp(object? pSender, KeyEventArgs pE) {
         //DEBUG efm5 2026 03 30 implement
         //if (e.KeyCode == Keys.Enter)
         //UsePickedColor();
      }

      private void UseNamedCheckBox_CheckedChanged(object? pSender, EventArgs pE) {
         CheckBox? checkBox = pSender as CheckBox;
         if (checkBox == null)
            return;
         UseNamedColor(checkBox.Checked, true);
         UpdateColorSwatch();
      }

      private void NamedColorPrefixButton_Click(object? pSender, EventArgs pE) {
         mNamedColorsComboBox?.Focus();
         mNamedColorsComboBox?.DroppedDown = true;
      }

      private void NamedColorsComboBox_SelectedValueChanged(object? pSender, EventArgs pE) {
         UpdateColorPicker();
      }

      private void NamedColorsComboBox_Leave(object? pSender, EventArgs pE) {
         if (mNamedColorsComboBox == null)
            return;
         if (IsKnownColor(mNamedColorsComboBox?.Text.Replace(" ", string.Empty), out Color opColor))
            UpdateColorPicker();
         else {
            mNamedColorsComboBox?.SelectedValueChanged -= NamedColorsComboBox_SelectedValueChanged;
            mNamedColorsComboBox?.SelectedValue = -1;
            mNamedColorsComboBox?.Text = string.Empty;
            mNamedColorsComboBox?.SelectedValueChanged += NamedColorsComboBox_SelectedValueChanged;
         }
      }
      #endregion

      #region gray
      private void UseGrayscaleCheckBox_CheckedChanged(object? pSender, EventArgs pE) {
         CheckBox? checkBox = pSender as CheckBox;
         if (checkBox == null)
            return;
         UseGray(checkBox.Checked, true);
         UpdateColorSwatch();
      }

      private void GrayscalePrefixButton_Click(object? pSender, EventArgs pE) {
         UpDownSelectAll(mGrayUpDown);
      }

      private void GrayscaleUpDown_ValueChanged(object? pSender, EventArgs pE) {
         NumericUpDown? grayUpDown = pSender as NumericUpDown;
         if (grayUpDown == null)
            return;
         int gray = (int)grayUpDown.Value;

         mGrayscaleInnerExamplePanel?.BackColor = Color.FromArgb(gray, gray, gray);
         mColorPickerExampleInnerPanel?.BackColor = Color.FromArgb(gray, gray, gray);
         mGraySlider?.ValueChanged -= GrayscaleSlider_ValueChanged;
         mGraySlider?.Value = gray;
         mGraySlider?.ValueChanged += GrayscaleSlider_ValueChanged;
      }

      private void GrayscaleSlider_ValueChanged(object? pSender, EventArgs pE) {
         if (mGraySlider == null)
            return;
         int gray = mGraySlider.Value;

         mGrayscaleInnerExamplePanel?.BackColor = Color.FromArgb(gray, gray, gray);
         mColorPickerExampleInnerPanel?.BackColor = Color.FromArgb(gray, gray, gray);
         mGrayUpDown?.ValueChanged -= GrayscaleUpDown_ValueChanged;
         mGrayUpDown?.Value = gray;
         mGrayUpDown?.ValueChanged += GrayscaleUpDown_ValueChanged;
      }
      #endregion

      #region red
      private void RedPrefixButton_Click(object? pSender, EventArgs pE) {
         UpDownSelectAll(mRedUpDown);
      }

      private void RedUpDown_ValueChanged(object? pSender, EventArgs pE) {
         NumericUpDown? redUpDown = pSender as NumericUpDown;
         if ((redUpDown == null) || (mColorPickerExampleInnerPanel == null))
            return;
         int red = (int)redUpDown.Value;

         mColorPickerExampleInnerPanel?.BackColor = Color.FromArgb(
            red,
            mColorPickerExampleInnerPanel.BackColor.G,
            mColorPickerExampleInnerPanel.BackColor.B);
         mRedSlider?.ValueChanged -= RedSlider_ValueChanged;
         mRedSlider?.Value = red;
         mRedSlider?.ValueChanged += RedSlider_ValueChanged;
      }

      private void RedSlider_ValueChanged(object? pSender, EventArgs pE) {
         if ((mRedSlider == null) || (mColorPickerExampleInnerPanel == null))
            return;
         int red = mRedSlider.Value;

         mColorPickerExampleInnerPanel?.BackColor = Color.FromArgb(
            red,
            mColorPickerExampleInnerPanel.BackColor.G,
            mColorPickerExampleInnerPanel.BackColor.B);
         mRedUpDown?.ValueChanged -= RedUpDown_ValueChanged;
         mRedUpDown?.Value = red;
         mRedUpDown?.ValueChanged += RedUpDown_ValueChanged;
      }
      #endregion

      #region green
      private void GreenPrefixButton_Click(object? pSender, EventArgs pE) {
         UpDownSelectAll(mGreenUpDown);
      }

      private void GreenUpDown_ValueChanged(object? pSender, EventArgs pE) {
         if ((mGreenUpDown == null) || (mColorPickerExampleInnerPanel == null))
            return;
         int green = (int)mGreenUpDown.Value;

         mColorPickerExampleInnerPanel?.BackColor = Color.FromArgb(
            green,
            mColorPickerExampleInnerPanel.BackColor.G,
            mColorPickerExampleInnerPanel.BackColor.B);
         mGreenSlider?.ValueChanged -= GreenSlider_ValueChanged;
         mGreenSlider?.Value = green;
         mGreenSlider?.ValueChanged += GreenSlider_ValueChanged;
      }

      private void GreenSlider_ValueChanged(object? pSender, EventArgs pE) {
         if ((mGreenSlider == null) || (mColorPickerExampleInnerPanel == null))
            return;
         int green = mGreenSlider.Value;

         mColorPickerExampleInnerPanel?.BackColor = Color.FromArgb(
            green,
            mColorPickerExampleInnerPanel.BackColor.G,
            mColorPickerExampleInnerPanel.BackColor.B);
         mGreenUpDown?.ValueChanged -= GreenUpDown_ValueChanged;
         mGreenUpDown?.Value = green;
         mGreenUpDown?.ValueChanged += GreenUpDown_ValueChanged;
      }
      #endregion

      #region blue
      private void BluePrefixButton_Click(object? pSender, EventArgs pE) {
         UpDownSelectAll(mBlueUpDown);
      }

      private void BlueUpDown_ValueChanged(object? pSender, EventArgs pE) {
         if ((mBlueUpDown == null) || (mColorPickerExampleInnerPanel == null))
            return;
         int blue = (int)mBlueUpDown.Value;

         mColorPickerExampleInnerPanel?.BackColor = Color.FromArgb(
            blue,
            mColorPickerExampleInnerPanel.BackColor.G,
            mColorPickerExampleInnerPanel.BackColor.B);
         mBlueSlider?.ValueChanged -= BlueSlider_ValueChanged;
         mBlueSlider?.Value = blue;
         mBlueSlider?.ValueChanged += BlueSlider_ValueChanged;
      }

      private void BlueSlider_ValueChanged(object? pSender, EventArgs pE) {
         if ((mBlueSlider == null) || (mColorPickerExampleInnerPanel == null))
            return;
         int blue = mBlueSlider.Value;

         mColorPickerExampleInnerPanel?.BackColor = Color.FromArgb(
            blue,
            mColorPickerExampleInnerPanel.BackColor.G,
            mColorPickerExampleInnerPanel.BackColor.B);
         mBlueUpDown?.ValueChanged -= BlueUpDown_ValueChanged;
         mBlueUpDown?.Value = blue;
         mBlueUpDown?.ValueChanged += BlueUpDown_ValueChanged;
      }
      #endregion

      #region Bottom Buttons
      private void ColorPickerHelpButton_Click(object? pSender, EventArgs pE) {
         ColorPickerHelp();
      }

      private void ColorPickerOkButton_Click(object? pSender, EventArgs pE) {
         //DEBUG efm5 2026 03 30 implement
         //UsePickedColor();
      }

      private void ColorPickerCancelButton_Click(object? pSender, EventArgs pE) {
         HideColorPicker();
      }
      #endregion
   }
}