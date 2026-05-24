namespace DBCode {
   namespace Themes {
      internal sealed partial class ColorPickerPanel : ScrollablePanel {
         protected override void OnHandleCreated(EventArgs pEventArgs) {
            ThrowIfNull(mForm, nameof(mForm));
            base.OnHandleCreated(pEventArgs);
            Dock = DockStyle.Fill;
            mForm.Opacity = 0;
            LayoutControls();
         }

         #region Named Color Event Handlers
         private void UseNamedCheckBox_CheckedChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mUseNamedCheckBox, nameof(mUseNamedCheckBox));
            ThrowIfNull(mNamedColorPrefixButton, nameof(mNamedColorPrefixButton));
            ThrowIfNull(mNamedColorsComboBox, nameof(mNamedColorsComboBox));
            ThrowIfNull(mUseGrayscaleCheckBox, nameof(mUseGrayscaleCheckBox));
            bool useNamed = mUseNamedCheckBox.Checked;
            mNamedColorPrefixButton.Enabled = useNamed;
            mNamedColorsComboBox.Enabled = useNamed;
            if (useNamed) {
               mUseGrayscaleCheckBox.Checked = false;
               DisableCustomControls();
            }
            else
               EnableCustomControls();
            UpdateSwatches();
         }

         private void NamedColorPrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mNamedColorsComboBox, nameof(mNamedColorsComboBox));
            mNamedColorsComboBox.Focus();
            mNamedColorsComboBox.DroppedDown = true;
         }

         private void NamedColorsComboBox_SelectedValueChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mNamedColorsComboBox, nameof(mNamedColorsComboBox));
            string? colorName = mNamedColorsComboBox.Text.Replace(" ", string.Empty);
            if (IsKnownColor(colorName, out Color color))
               SetColorValues(color);
         }

         private void NamedColorsComboBox_KeyUp(object? pSender, KeyEventArgs pE) {
            ThrowIfNull(mNamedColorsComboBox, nameof(mNamedColorsComboBox));
            if (pE.KeyCode == Keys.Enter) {
               string? colorName = mNamedColorsComboBox.Text.Replace(" ", string.Empty);
               if (IsKnownColor(colorName, out Color color))
                  SetColorValues(color);
            }
         }

         private void NamedColorsComboBox_Leave(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mNamedColorsComboBox, nameof(mNamedColorsComboBox));
            string? colorName = mNamedColorsComboBox.Text.Replace(" ", string.Empty);
            if (!IsKnownColor(colorName, out Color _)) {
               mNamedColorsComboBox.SelectedIndex = -1;
               mNamedColorsComboBox.Text = string.Empty;
            }
         }
         #endregion

         #region Custom Color Event Handlers
         private void UseGrayscaleCheckBox_CheckedChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mUseGrayscaleCheckBox, nameof(mUseGrayscaleCheckBox));
            ThrowIfNull(mUseNamedCheckBox, nameof(mUseNamedCheckBox));
            ThrowIfNull(mGrayPrefixButton, nameof(mGrayPrefixButton));
            ThrowIfNull(mGrayUpDown, nameof(mGrayUpDown));
            ThrowIfNull(mGraySlider, nameof(mGraySlider));
            ThrowIfNull(mRedPrefixButton, nameof(mRedPrefixButton));
            ThrowIfNull(mRedUpDown, nameof(mRedUpDown));
            ThrowIfNull(mRedSlider, nameof(mRedSlider));
            ThrowIfNull(mGreenPrefixButton, nameof(mGreenPrefixButton));
            ThrowIfNull(mGreenUpDown, nameof(mGreenUpDown));
            ThrowIfNull(mGreenSlider, nameof(mGreenSlider));
            ThrowIfNull(mBluePrefixButton, nameof(mBluePrefixButton));
            ThrowIfNull(mBlueUpDown, nameof(mBlueUpDown));
            ThrowIfNull(mBlueSlider, nameof(mBlueSlider));
            bool useGray = mUseGrayscaleCheckBox.Checked;
            RemoveEventHandlers();
            mGrayPrefixButton.Enabled = useGray;
            mGrayUpDown.Enabled = useGray;
            mGraySlider.Enabled = useGray;
            mRedPrefixButton.Enabled = !useGray;
            mRedUpDown.Enabled = !useGray;
            mRedSlider.Enabled = !useGray;
            mGreenPrefixButton.Enabled = !useGray;
            mGreenUpDown.Enabled = !useGray;
            mGreenSlider.Enabled = !useGray;
            mBluePrefixButton.Enabled = !useGray;
            mBlueUpDown.Enabled = !useGray;
            mBlueSlider.Enabled = !useGray;
            if (useGray)
               mUseNamedCheckBox.Checked = false;
            AttachEventHandlers();
            UpdateSwatches();
         }

         private void GrayUpDown_ValueChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mGrayUpDown, nameof(mGrayUpDown));
            ThrowIfNull(mGraySlider, nameof(mGraySlider));
            int value = (int)mGrayUpDown.Value;
            if (mGraySlider.Value != value) {
               RemoveEventHandlers();
               mGraySlider.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void GraySlider_ValueChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mGraySlider, nameof(mGraySlider));
            ThrowIfNull(mGrayUpDown, nameof(mGrayUpDown));
            int value = mGraySlider.Value;
            if (mGrayUpDown.Value != value) {
               RemoveEventHandlers();
               mGrayUpDown.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void RedUpDown_ValueChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mRedUpDown, nameof(mRedUpDown));
            ThrowIfNull(mRedSlider, nameof(mRedSlider));
            int value = (int)mRedUpDown.Value;
            if (mRedSlider.Value != value) {
               RemoveEventHandlers();
               mRedSlider.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void RedSlider_ValueChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mRedSlider, nameof(mRedSlider));
            ThrowIfNull(mRedUpDown, nameof(mRedUpDown));
            int value = mRedSlider.Value;
            if (mRedUpDown.Value != value) {
               RemoveEventHandlers();
               mRedUpDown.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void GreenUpDown_ValueChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mGreenUpDown, nameof(mGreenUpDown));
            ThrowIfNull(mGreenSlider, nameof(mGreenSlider));
            int value = (int)mGreenUpDown.Value;
            if (mGreenSlider.Value != value) {
               RemoveEventHandlers();
               mGreenSlider.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void GreenSlider_ValueChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mGreenSlider, nameof(mGreenSlider));
            ThrowIfNull(mGreenUpDown, nameof(mGreenUpDown));
            int value = mGreenSlider.Value;
            if (mGreenUpDown.Value != value) {
               RemoveEventHandlers();
               mGreenUpDown.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void BlueUpDown_ValueChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mBlueUpDown, nameof(mBlueUpDown));
            ThrowIfNull(mBlueSlider, nameof(mBlueSlider));
            int value = (int)mBlueUpDown.Value;
            if (mBlueSlider.Value != value) {
               RemoveEventHandlers();
               mBlueSlider.Value = value;
               AttachEventHandlers();
            }
            UpdateSwatches();
         }

         private void BlueSlider_ValueChanged(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mBlueSlider, nameof(mBlueSlider));
            ThrowIfNull(mBlueUpDown, nameof(mBlueUpDown));
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
         private void GrayPrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            UpDownSelectAll(mGrayUpDown);
         }
         private void RedPrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            UpDownSelectAll(mRedUpDown);
         }
         private void GreenPrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            UpDownSelectAll(mGreenUpDown);
         }
         private void BluePrefixButton_Click(object? pSender, EventArgs pEventArguments) {
            UpDownSelectAll(mBlueUpDown);
         }

         private void OkButton_Click(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mTheme, nameof(mTheme));
            ThrowIfNull(mDemoSwatch, nameof(mDemoSwatch));
            if (mColorCallback != null) {
               mColorCallback(mDemoSwatch.GetColor());
               mCloseAction!();
               return;
            }
            ThemePanel.mRepaint = mDemoSwatch.GetColor() != mInitialColor;
            if (mIsSyntaxColor)
               mTheme.mHighlightColors[(int)mLanguageKind][(int)mTokenKind] = mDemoSwatch.BackColor;
            else
               mTheme.mInterfaceColors[(int)mColorSwatchUsage] = mDemoSwatch.BackColor;
            ThemePanel.RestoreFromColorPickerPanel();
         }

         private void CancelButton_Click(object? pSender, EventArgs pEventArguments) {
            if (mCloseAction != null) {
               mCloseAction();
               return;
            }
            ThemePanel.RestoreFromColorPickerPanel();
         }
         #endregion
      }
   }
}
