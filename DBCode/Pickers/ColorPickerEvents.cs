namespace DBCode {
   namespace Themes {
      public sealed partial class ColorPickerPanel : Panel {
         #region Named Color Event Handlers
         private void UseNamedCheckBox_CheckedChanged(object? pSender, EventArgs pEventArguments) {
            if (mUseNamedCheckBox == null)
               return;
            bool useNamed = mUseNamedCheckBox.Checked;

            if (mNamedColorPrefixButton != null)
               mNamedColorPrefixButton.Enabled = useNamed;
            if (mNamedColorsComboBox != null)
               mNamedColorsComboBox.Enabled = useNamed;
            if (useNamed) {
               if (mUseGrayscaleCheckBox != null)
                  mUseGrayscaleCheckBox.Checked = false;
               DisableCustomControls();
            }
            else
               EnableCustomControls();
            UpdateSwatches();
         }

         private void NamedColorPrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            if (mNamedColorsComboBox != null) {
               mNamedColorsComboBox.Focus();
               mNamedColorsComboBox.DroppedDown = true;
            }
         }

         private void NamedColorsComboBox_SelectedValueChanged(object? pSender, EventArgs pEventArguments) {
            if (mNamedColorsComboBox == null)
               return;
            string? colorName = mNamedColorsComboBox.Text.Replace(" ", string.Empty);

            if (IsKnownColor(colorName, out Color color)) {
               SetColorValues(color);
            }
         }

         private void NamedColorsComboBox_KeyUp(object? pSender, KeyEventArgs pE) {
            if (pE.KeyCode == Keys.Enter && mNamedColorsComboBox != null) {
               string? colorName = mNamedColorsComboBox.Text.Replace(" ", string.Empty);
               if (IsKnownColor(colorName, out Color color)) {
                  SetColorValues(color);
               }
            }
         }

         private void NamedColorsComboBox_Leave(object? pSender, EventArgs pEventArguments) {
            if (mNamedColorsComboBox == null)
               return;
            string? colorName = mNamedColorsComboBox.Text.Replace(" ", string.Empty);

            if (!IsKnownColor(colorName, out Color color)) {
               mNamedColorsComboBox.SelectedIndex = -1;
               mNamedColorsComboBox.Text = string.Empty;
            }
         }
         #endregion

         #region Custom Color Event Handlers
         private void UseGrayscaleCheckBox_CheckedChanged(object? pSender, EventArgs pEventArguments) {
            if (mUseGrayscaleCheckBox == null)
               return;
            bool useGray = mUseGrayscaleCheckBox.Checked;

            RemoveEventHandlers();
            if (mGrayPrefixButton != null)
               mGrayPrefixButton.Enabled = useGray;
            if (mGrayUpDown != null)
               mGrayUpDown.Enabled = useGray;
            if (mGraySlider != null)
               mGraySlider.Enabled = useGray;
            if (mRedPrefixButton != null)
               mRedPrefixButton.Enabled = !useGray;
            if (mRedUpDown != null)
               mRedUpDown.Enabled = !useGray;
            if (mRedSlider != null)
               mRedSlider.Enabled = !useGray;
            if (mGreenPrefixButton != null)
               mGreenPrefixButton.Enabled = !useGray;
            if (mGreenUpDown != null)
               mGreenUpDown.Enabled = !useGray;
            if (mGreenSlider != null)
               mGreenSlider.Enabled = !useGray;
            if (mBluePrefixButton != null)
               mBluePrefixButton.Enabled = !useGray;
            if (mBlueUpDown != null)
               mBlueUpDown.Enabled = !useGray;
            if (mBlueSlider != null)
               mBlueSlider.Enabled = !useGray;
            if (useGray && mUseNamedCheckBox != null)
               mUseNamedCheckBox.Checked = false;
            AttachEventHandlers();
            UpdateSwatches();
         }

         private void GrayUpDown_ValueChanged(object? pSender, EventArgs pEventArguments) {
            if (mGrayUpDown == null || mGraySlider == null)
               return;
            int value = (int)mGrayUpDown.Value;

            if (mGraySlider.Value != value) {
               RemoveEventHandlers();
               mGraySlider.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void GraySlider_ValueChanged(object? pSender, EventArgs pEventArguments) {
            if (mGraySlider == null || mGrayUpDown == null)
               return;
            int value = mGraySlider.Value;

            if (mGrayUpDown.Value != value) {
               RemoveEventHandlers();
               mGrayUpDown.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void RedUpDown_ValueChanged(object? pSender, EventArgs pEventArguments) {
            if (mRedUpDown == null || mRedSlider == null)
               return;
            int value = (int)mRedUpDown.Value;

            if (mRedSlider.Value != value) {
               RemoveEventHandlers();
               mRedSlider.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void RedSlider_ValueChanged(object? pSender, EventArgs pEventArguments) {
            if (mRedSlider == null || mRedUpDown == null)
               return;
            int value = mRedSlider.Value;

            if (mRedUpDown.Value != value) {
               RemoveEventHandlers();
               mRedUpDown.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void GreenUpDown_ValueChanged(object? pSender, EventArgs pEventArguments) {
            if (mGreenUpDown == null || mGreenSlider == null)
               return;
            int value = (int)mGreenUpDown.Value;

            if (mGreenSlider.Value != value) {
               RemoveEventHandlers();
               mGreenSlider.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void GreenSlider_ValueChanged(object? pSender, EventArgs pEventArguments) {
            if (mGreenSlider == null || mGreenUpDown == null)
               return;
            int value = mGreenSlider.Value;

            if (mGreenUpDown.Value != value) {
               RemoveEventHandlers();
               mGreenUpDown.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void BlueUpDown_ValueChanged(object? pSender, EventArgs pEventArguments) {
            if (mBlueUpDown == null || mBlueSlider == null)
               return;
            int value = (int)mBlueUpDown.Value;

            if (mBlueSlider.Value != value) {
               RemoveEventHandlers();
               mBlueSlider.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void BlueSlider_ValueChanged(object? pSender, EventArgs pEventArguments) {
            if (mBlueSlider == null || mBlueUpDown == null)
               return;
            int value = mBlueSlider.Value;

            if (mBlueUpDown.Value != value) {
               RemoveEventHandlers();
               mBlueUpDown.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }
         #endregion

         #region Button Event Handlers
         protected override void OnHandleCreated(EventArgs pEventArgs) {
            base.OnHandleCreated(pEventArgs);
            mColorPickerBottomPanel.LayoutControls();
         }

         private void OkButton_Click(object? pSender, EventArgs pEventArguments) {
            if (mTheme == null || mDemoSwatch == null)
               return;
            if (mDemoSwatch!.GetColor() == mInitialColor)
               ThemePanel.mRepaint = false;
            else
               ThemePanel.mRepaint = true;
            mTheme.mInterfaceColors[(int)mColorSwatchUsage] = mDemoSwatch.BackColor;
            ThemePanel.RestoreFromColorPickerPanel();
         }

         private void CancelButton_Click(object? pSender, EventArgs pEventArguments) {
            ThemePanel.RestoreFromColorPickerPanel();
         }
         #endregion
      }
   }
}
