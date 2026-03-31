using static DBCode.Program;

namespace DBCode {
   public sealed partial class MainForm : Form {
      private void UpdateColorPicker() {
         string? colorName = mNamedColorsComboBox?.Text.Replace(" ", string.Empty);
         Color color = Color.FromName(colorName);
         int red, green, blue, gray;

         RemoveColorHandlers();
         red = color.R;
         green = color.G;
         blue = color.B;
         gray = (int)((red + green + blue) / 3);
         mGrayscaleInnerExamplePanel?.BackColor = Color.FromArgb(gray, gray, gray);
         mColorPickerExampleInnerPanel?.BackColor = color;
         mGrayUpDown?.Value = gray;
         mRedUpDown?.Value = red;
         mGreenUpDown?.Value = green;
         mBlueUpDown?.Value = blue;
         mGraySlider?.Value = gray;
         mRedSlider?.Value = red;
         mGreenSlider?.Value = green;
         mBlueSlider?.Value = blue;
         if (gray > 128) {
            mColorPickerExampleMiddlePanel?.BackColor = Color.Black;
            mColorPickerExampleOuterPanel?.BackColor = Color.White;
         }
         else {
            mColorPickerExampleMiddlePanel?.BackColor = Color.White;
            mColorPickerExampleOuterPanel?.BackColor = Color.Black;
         }
         RestoreColorHandlers();
      }

      public static string MassageColorName(string pCompressedName) {
         string expandedName = string.Empty;

         if (!IsKnownColor(pCompressedName, out Color color))
            return pCompressedName;
         foreach (char c in pCompressedName) {
            if (char.IsUpper(c))
               expandedName += " " + string.Format("{0}", c);
            else
               expandedName += string.Format("{0}", c);
         }
         expandedName = expandedName.Trim(' ');
         return expandedName;
      }

      private void UsePickedColor(ColorUsage pColorUsage) {
         if (mColorPickerExampleInnerPanel != null) {
            Color chosenColor = mColorPickerExampleInnerPanel.BackColor;
            string colorName = MassageColorName(ColorTranslator.ToHtml(chosenColor));
            //DEBUG efm5 2026 03 30 implement this
            switch (pColorUsage) {
               case ColorUsage.BackgroundColor:
                  break;
               case ColorUsage.InterfaceBackgroundColor:
                  break;
               case ColorUsage.InterfaceFontColor:
                  break;
               case ColorUsage.GroupBoxBackgroundColor:
                  break;
               case ColorUsage.GroupBoxFontColor:
                  break;
               case ColorUsage.MenuBackgroundColor:
                  break;
               case ColorUsage.MenuFontColor:
                  break;
               case ColorUsage.StatusBarBackgroundColor:
                  break;
               case ColorUsage.StatusBarFontColor:
                  break;
               default:
                  TimedMessage("The “ColorUsage” was not recognized", "Coding ERROR");
                  return;
            }
            HideColorPicker();
         }
      }

      public void ColorPickerHelp() {
         //DEBUG efm5 2026 03 30 implement this
         //try {
         //   if (!File.Exists(sColorPickerHelp)) {
         //      _ = AskingAsync(new TM("Missing Help File",
         //         string.Format("DBCode's Color Picker help HTML file" +
         //            ":{0}{1}{0}" +
         //            "could not be found" + "!{0}{0}" +
         //            "You may locate and restore the missing" + " " +
         //            "file then click the “Retry” button below" + ".{0}{0}" +
         //            "Otherwise, click “Cancel” to abort this help request" + ".",
         //            Environment.NewLine, sColorPickerHelp),
         //          string.Empty,
         //          "Cancel", TIMED_MESSAGE_Cancel_Ask,
         //          "Retry", TIMED_MESSAGE_Retry_Color_Picker_Help,
         //          string.Empty, TIMED_MESSAGE_Ignore,
         //          TIMED_MESSAGE_Cancel_Ask, 0));
         //      return;
         //   }
         //   Process process = new Process();
         //   process.StartInfo = new ProcessStartInfo(sColorPickerHelp) {
         //      UseShellExecute = true
         //   };
         //   process.Start();
         //   Thread.Sleep(100);
         //}
         //catch (Exception pException) {
         //   _ = AskingAsync(new TM("ColorPickerHelp", pException));
         //}
      }

      private bool LayoutColorPicker(ColorUsage pColorUsage) {
         if (mCurrentTheme != null) {
            Color usageColor, interfaceBackgroundColor = mCurrentTheme.mGroupBoxBackgroundColor,
               interfaceFontColor = mCurrentTheme.mGroupBoxFontColor;
            Font interfaceTextFont = mCurrentTheme.mInterfaceFont;
            float interfaceTextFontSize = mCurrentTheme.mInterfaceFont.SizeInPoints;
            int red, green, blue, gray;
            bool knownColor = false;
            List<Control> controlList = [];

            #region assignments
            mPickColorUsageLabel?.Text = "Use this color for the " + string.Format("{0}", ToDescription((ColorUsage)pColorUsage));
            mEscapeFrom = EscapeFrom.ColorPicker;
            switch (pColorUsage) {
               case ColorUsage.BackgroundColor:
                  usageColor = mCurrentTheme.mMainWindowBackgroundColor;
                  break;
               case ColorUsage.InterfaceBackgroundColor:
                  usageColor = mCurrentTheme.mInterfaceBackgroundColor;
                  break;
               case ColorUsage.InterfaceFontColor:
                  usageColor = mCurrentTheme.mInterfaceFontColor;
                  break;
               case ColorUsage.GroupBoxBackgroundColor:
                  usageColor = mCurrentTheme.mGroupBoxBackgroundColor;
                  break;
               case ColorUsage.GroupBoxFontColor:
                  usageColor = mCurrentTheme.mGroupBoxFontColor;
                  break;
               case ColorUsage.MenuBackgroundColor:
                  usageColor = mCurrentTheme.mMenuBackgroundColor;
                  break;
               case ColorUsage.MenuFontColor:
                  usageColor = mCurrentTheme.mMenuFontColor;
                  break;
               case ColorUsage.StatusBarFontColor:
                  usageColor = mCurrentTheme.mStatusBarFontColor;
                  break;
               default:
                  TimedMessage("The “ColorUsage” was not recognized", "Coding ERROR");
                  return false;
            }
            if (IsKnownColor(usageColor) && !ColorIsGray(usageColor)) {
               UseNamedColor(true, false);
               knownColor = true;
            }
            else if (ColorIsGray(usageColor)) {
               UseGray(true, false);
            }
            else {
               UseNamedColor(false, false);
               UseGray(false, false);
               mUseNamedCheckBox?.Checked = false;
               mUseGrayscaleCheckBox?.Checked = false;
               mNamedColorsComboBox?.SelectedIndex = -1;
               mNamedColorsComboBox?.SelectedValue = string.Empty;
               mGrayUpDown?.Value = (usageColor.R + usageColor.G + usageColor.B) / 3;
            }
            mNamedColorsComboBox?.Items.Clear();
            foreach (PropertyInfo color in mPredefinedColors)
               mNamedColorsComboBox?.Items.Add(MassageColorName(color.Name));
            string colorName = GetColorName(usageColor);
            if ((colorName != string.Empty) && knownColor)
               mNamedColorsComboBox?.Text = MassageColorName(colorName);
            red = usageColor.R;
            green = usageColor.G;
            blue = usageColor.B;
            gray = (int)((red + green + blue) / 3);
            mGrayscaleInnerExamplePanel?.BackColor = Color.FromArgb(gray, gray, gray);
            mGrayUpDown?.Value = gray;
            mRedUpDown?.Value = red;
            mGreenUpDown?.Value = green;
            mBlueUpDown?.Value = blue;
            mGraySlider?.Value = gray;
            mRedSlider?.Value = red;
            mGreenSlider?.Value = green;
            mBlueSlider?.Value = blue;
            if (gray > 128) {
               mColorPickerExampleMiddlePanel?.BackColor = Color.Black;
               mColorPickerExampleOuterPanel?.BackColor = Color.White;
            }
            else {
               mColorPickerExampleMiddlePanel?.BackColor = Color.White;
               mColorPickerExampleOuterPanel?.BackColor = Color.Black;
            }
            #endregion

            #region fonts and colors
            mPickColorPanel?.BackColor = interfaceBackgroundColor;
            mColorPickerNamedColorPanel?.BackColor = mCurrentTheme.mGroupBoxBackgroundColor;
            mColorPickerSliderPanel?.BackColor = mCurrentTheme.mGroupBoxBackgroundColor;
            mColorPickerExampleInnerPanel?.BackColor = usageColor;
            mPickColorTitleLabel?.Font = CreateNewTitleFont();
            mPickColorUsageLabel?.Font = CreateNewFont(interfaceTextFont);
            mColorPickerBottomPanel?.BackColor = Color.Transparent;
            mUseNamedCheckBox?.Font = CreateNewFont(interfaceTextFont);
            mUseNamedCheckBox?.ForeColor = interfaceFontColor;
            mUseNamedCheckBox?.BackColor = Color.Transparent;
            mNamedColorPrefixButton?.Font = CreateNewFont(interfaceTextFont);
            mNamedColorsComboBox?.Font = CreateNewFont(interfaceTextFont);
            mGrayPrefixButton?.Font = CreateNewFont(interfaceTextFont);
            mRedPrefixButton?.Font = CreateNewFont(interfaceTextFont);
            mGreenPrefixButton?.Font = CreateNewFont(interfaceTextFont);
            mBluePrefixButton?.Font = CreateNewFont(interfaceTextFont);
            FlattenButton(mNamedColorPrefixButton, mCurrentTheme?.mGroupBoxBackgroundColor);
            FlattenButton(mGrayPrefixButton, mCurrentTheme?.mGroupBoxBackgroundColor);
            FlattenButton(mRedPrefixButton, mCurrentTheme?.mGroupBoxBackgroundColor);
            FlattenButton(mGreenPrefixButton, mCurrentTheme?.mGroupBoxBackgroundColor);
            FlattenButton(mBluePrefixButton, mCurrentTheme?.mGroupBoxBackgroundColor);
            mUseGrayscaleCheckBox?.Font = CreateNewFont(interfaceTextFont);
            mUseGrayscaleCheckBox?.ForeColor = interfaceFontColor;
            mUseGrayscaleCheckBox?.BackColor = Color.Transparent;
            if (mPickColorPanel != null)
               foreach (Label label in mPickColorPanel.Controls.OfType<Label>()) {
                  label.ForeColor = interfaceFontColor;
                  label.BackColor = Color.Transparent;
               }
            if ((mColorPickerBottomPanel != null) && (mCurrentTheme != null))
               foreach (Button button in mColorPickerBottomPanel.Controls.OfType<Button>()) {
                  button.Font = CreateNewFont(interfaceTextFont);
                  button.ForeColor = mCurrentTheme.mInterfaceFontColor;
                  button.BackColor = Color.Transparent;
               }
            if (mColorPickerSliderPanel != null) {
               foreach (NumericUpDown numericUpDown in mColorPickerSliderPanel.Controls.OfType<NumericUpDown>()) {
                  numericUpDown.Font = CreateNewFont(interfaceTextFont);
                  numericUpDown.ForeColor = interfaceFontColor;
               }
               foreach (TrackBar trackBar in mColorPickerSliderPanel.Controls.OfType<TrackBar>()) {
                  trackBar.Font = CreateNewFont(interfaceTextFont);
                  trackBar.ForeColor = interfaceFontColor;
               }
            }
            #endregion

            //#region location and size
            //SetComboBoxSize(out SizeF oSizeF, namedColorsComboBox);
            //namedColorsComboBox.Size = SizeFromSizeF(oSizeF);
            //SetComboBoxDropDownWidth(namedColorsComboBox, namedColorsComboBox.Width);
            //pickColorUsageLabel.Top = pickColorTitleLabel.Bottom + sWidgetBigVerticalOffset;
            //colorPickerNamedColorPanel.Top = pickColorUsageLabel.Bottom + sWidgetBigVerticalOffset;
            //namedColorPrefixButton.Top = useNamedCheckBox.Bottom + sWidgetVerticalOffset;
            //namedColorsComboBox.Location = new Point(
            //   namedColorPrefixButton.Right,
            //   namedColorPrefixButton.Top);
            //SizePanel(colorPickerNamedColorPanel);
            //colorPickerSliderPanel.Top = colorPickerNamedColorPanel.Bottom + sWidgetBigVerticalOffset;
            //grayPrefixButton.Left = useGrayscaleCheckBox.Right;
            //grayUpDown.Left = grayPrefixButton.Right;
            //graySlider.Left = grayUpDown.Right + sWidgetHorizontalSpace;
            //grayscaleOuterExamplePanel.Left = graySlider.Right + sWidgetBigHorizontalSpace;
            //grayscaleInnerExamplePanel.Size = new Size(grayPrefixButton.Height, grayPrefixButton.Height);
            //grayscaleMiddleExamplePanel.Size = new Size(grayscaleInnerExamplePanel.Height + 4, grayscaleInnerExamplePanel.Height + 4);
            //grayscaleOuterExamplePanel.Size = new Size(grayscaleMiddleExamplePanel.Height + 4, grayscaleMiddleExamplePanel.Height + 4);

            //controlList.Clear();
            //controlList.Add(useGrayscaleCheckBox);
            //controlList.Add(grayPrefixButton);
            //controlList.Add(grayUpDown);
            //controlList.Add(graySlider);
            //controlList.Add(grayscaleOuterExamplePanel);
            //redPrefixButton.Top = Bottommost(controlList) + sWidgetBigVerticalOffset;
            //redUpDown.Location = new Point(redPrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
            //   redPrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
            //redSlider.Location = new Point(redUpDown.Right + sAssociatedSliderPostUpDownHorizontalSpace,
            //   redUpDown.Top + sAssociatedSliderPostUpDownVerticalOffset);

            //greenPrefixButton.Top = redPrefixButton.Bottom + sWidgetBigVerticalOffset;
            //greenUpDown.Location = new Point(greenPrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
            //   greenPrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
            //greenSlider.Location = new Point(greenUpDown.Right + sAssociatedSliderPostUpDownHorizontalSpace,
            //   greenUpDown.Top + sAssociatedSliderPostUpDownVerticalOffset);

            //bluePrefixButton.Top = greenPrefixButton.Bottom + sWidgetBigVerticalOffset;
            //blueUpDown.Location = new Point(bluePrefixButton.Right + sAssociatedUpDownPostButtonHorizontalSpace,
            //   bluePrefixButton.Top + sAssociatedUpDownPostButtonVerticalOffset);
            //blueSlider.Location = new Point(blueUpDown.Right + sAssociatedSliderPostUpDownHorizontalSpace,
            //   blueUpDown.Top + sAssociatedSliderPostUpDownVerticalOffset);
            //controlList.Clear();
            //controlList.Add(bluePrefixButton);
            //controlList.Add(blueUpDown);
            //controlList.Add(blueSlider);
            //colorPickerExampleOuterPanel.Top = Bottommost(controlList) + sWidgetBigVerticalOffset;
            //SizePanel(colorPickerSliderPanel);
            //colorPickerBottomPanel.Top = colorPickerSliderPanel.Bottom + sWidgetBigVerticalOffset;
            //controlList.Clear();
            //controlList.Add(pickColorTitleLabel);
            //controlList.Add(pickColorUsageLabel);
            //controlList.Add(colorPickerNamedColorPanel);
            //controlList.Add(colorPickerSliderPanel);
            //pickColorPanel.Size =
            //   new Size(Rightmost(controlList) + SystemInformation.VerticalScrollBarWidth, colorPickerBottomPanel.Bottom);
            //colorPickerExampleOuterPanel.Width = colorPickerSliderPanel.Width - (colorPickerExampleOuterPanel.Left * 2);
            //colorPickerExampleMiddlePanel.Width = colorPickerExampleOuterPanel.Width - (colorPickerExampleMiddlePanel.Left * 2);
            //colorPickerExampleInnerPanel.Width = colorPickerExampleMiddlePanel.Width - (colorPickerExampleInnerPanel.Left * 2);
            //#endregion
            return knownColor;
         }
         return false;
      }

      private void ShowColorPicker(ColorUsage pColorUsage) {
         //sFirstGray = true;
         //RemoveColorHandlers();
         //pColorUsage = pColorUsage;
         //NewColorPickerBorderPanel();
         //PrepareOptionsDialog(EscapeFrom.ColorPicker);
         //sFromColorPicker = true;
         //sFromOptions = false;
         //pickColorPanel.Size = SizeFromFloats(Size.Width * 0.8f, (Size.Height * 0.8f) - sTitleBarHeight);
         //pickColorTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
         //colorPickerBottomPanel.Dock = DockStyle.None;
         //bool knownColor = LayoutColorPicker();
         //ShowPanel(sColorPickerBorderPanel);
         //colorPickerCancelButton.Left = colorPickerBottomPanel.Width - colorPickerCancelButton.Width - sCancelOffset;
         //colorPickerOkButton.Left = colorPickerCancelButton.Left - colorPickerOkButton.Width - sOkOffset;
         //colorPickerCancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
         //colorPickerOkButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
         //colorPickerBottomPanel.Dock = DockStyle.Bottom;
         //CenterControlHorizontally(pickColorPanel, pickColorTitleLabel);
         //if (knownColor)
         //   ComboBoxSelectAll(namedColorsComboBox);
         //else
         //   colorPickerCancelButton.Focus();
         //RestoreColorHandlers();
      }

      private void HideColorPicker(bool pFadeIn = true) {
         //HidePanel(sColorPickerBorderPanel);
         //RestoreOptions();
         //mFromColorPicker = false;
      }

      private static String GetColorName(Color color) {
#pragma warning disable CS8605
         var match = (from p in mPredefinedColors
                      where ((Color)p.GetValue(null, null)).ToArgb() == color.ToArgb()
                      select (Color)p.GetValue(null, null));
         if (match.Any())
            return match.First().Name;
         return String.Empty;
#pragma warning restore CS8605
      }

      private void RemoveColorHandlers() {
         mUseNamedCheckBox?.CheckedChanged -= UseNamedCheckBox_CheckedChanged;
         mNamedColorsComboBox?.SelectedValueChanged -= NamedColorsComboBox_SelectedValueChanged;
         mNamedColorsComboBox?.KeyUp -= NamedColorsComboBox_KeyUp;
         mNamedColorsComboBox?.Leave -= NamedColorsComboBox_Leave;
         mUseGrayscaleCheckBox?.CheckedChanged -= UseGrayscaleCheckBox_CheckedChanged;
         mGrayUpDown?.ValueChanged -= GrayscaleUpDown_ValueChanged;
         mRedUpDown?.ValueChanged -= RedUpDown_ValueChanged;
         mGreenUpDown?.ValueChanged -= GreenUpDown_ValueChanged;
         mBlueUpDown?.ValueChanged -= BlueUpDown_ValueChanged;
         mGraySlider?.ValueChanged -= GrayscaleSlider_ValueChanged;
         mRedSlider?.ValueChanged -= RedSlider_ValueChanged;
         mGreenSlider?.ValueChanged -= GreenSlider_ValueChanged;
         mBlueSlider?.ValueChanged -= BlueSlider_ValueChanged;
      }

      private void RestoreColorHandlers() {
         mUseNamedCheckBox?.CheckedChanged += UseNamedCheckBox_CheckedChanged;
         mNamedColorsComboBox?.SelectedValueChanged += NamedColorsComboBox_SelectedValueChanged;
         mNamedColorsComboBox?.KeyUp += NamedColorsComboBox_KeyUp;
         mNamedColorsComboBox?.Leave += NamedColorsComboBox_Leave;
         mUseGrayscaleCheckBox?.CheckedChanged += UseGrayscaleCheckBox_CheckedChanged;
         mGrayUpDown?.ValueChanged += GrayscaleUpDown_ValueChanged;
         mRedUpDown?.ValueChanged += RedUpDown_ValueChanged;
         mGreenUpDown?.ValueChanged += GreenUpDown_ValueChanged;
         mBlueUpDown?.ValueChanged += BlueUpDown_ValueChanged;
         mGraySlider?.ValueChanged += GrayscaleSlider_ValueChanged;
         mRedSlider?.ValueChanged += RedSlider_ValueChanged;
         mGreenSlider?.ValueChanged += GreenSlider_ValueChanged;
         mBlueSlider?.ValueChanged += BlueSlider_ValueChanged;
      }

      private void DisableAllColors(bool pDisableAll) {
         mUseGrayscaleCheckBox?.Checked = pDisableAll;
         mGrayPrefixButton?.Enabled = pDisableAll;
         mGrayUpDown?.Enabled = pDisableAll;
         mGraySlider?.Enabled = pDisableAll;
         mRedPrefixButton?.Enabled = pDisableAll;
         mGreenPrefixButton?.Enabled = pDisableAll;
         mBluePrefixButton?.Enabled = pDisableAll;
         mRedUpDown?.Enabled = pDisableAll;
         mGreenUpDown?.Enabled = pDisableAll;
         mBlueUpDown?.Enabled = pDisableAll;
         mRedSlider?.Enabled = pDisableAll;
         mGreenSlider?.Enabled = pDisableAll;
         mBlueSlider?.Enabled = pDisableAll;
      }

      private void UseGray(bool pUseGray, bool pRemoveRestore) {
         if (pRemoveRestore)
            RemoveColorHandlers();
         int gray;
         if ((mRedUpDown == null) || (mGreenUpDown == null) || (mBlueUpDown == null))
            gray = 128;
         else
            gray = (int)Math.Ceiling(((int)mRedUpDown.Value + (int)mGreenUpDown.Value + (int)mBlueUpDown.Value) / 3f);
         if (gray > 255)
            gray = 255;
         else if (gray < 0)
            gray = 0;
         if (pUseGray) {
            mUseNamedCheckBox?.Checked = false;
            if (mFirstGray) {
               mFirstGray = false;
               mGraySlider?.Value = gray;
               mGrayUpDown?.Value = gray;
               mColorPickerExampleInnerPanel?.BackColor = Color.FromArgb(gray, gray, gray);
               mGrayscaleInnerExamplePanel?.BackColor = Color.FromArgb(gray, gray, gray);
            }
         }
         mGrayPrefixButton?.Enabled = pUseGray;
         mUseGrayscaleCheckBox?.Checked = pUseGray;
         mGrayUpDown?.Enabled = pUseGray;
         mGraySlider?.Enabled = pUseGray;
         mRedPrefixButton?.Enabled = !pUseGray;
         mGreenPrefixButton?.Enabled = !pUseGray;
         mBluePrefixButton?.Enabled = !pUseGray;
         mRedUpDown?.Enabled = !pUseGray;
         mGreenUpDown?.Enabled = !pUseGray;
         mBlueUpDown?.Enabled = !pUseGray;
         mRedSlider?.Enabled = !pUseGray;
         mGreenSlider?.Enabled = !pUseGray;
         mBlueSlider?.Enabled = !pUseGray;
         mNamedColorPrefixButton?.Enabled = !pUseGray;
         mNamedColorsComboBox?.Enabled = !pUseGray;
         if (pRemoveRestore)
            RestoreColorHandlers();
      }

      private void UseNamedColor(bool pUseNamedColor, bool pRemoveRestore) {
         if (pRemoveRestore)
            RemoveColorHandlers();
         if (pUseNamedColor)
            mUseGrayscaleCheckBox?.Checked = false;
         mUseNamedCheckBox?.Checked = pUseNamedColor;
         mNamedColorPrefixButton?.Enabled = pUseNamedColor;
         mNamedColorsComboBox?.Enabled = pUseNamedColor;
         mUseNamedCheckBox?.Checked = pUseNamedColor;
         DisableAllColors(!pUseNamedColor);
         if (pRemoveRestore)
            RestoreColorHandlers();
      }

      private static bool ColorIsGray(Color pColor) {
         if ((pColor.R == pColor.G) && (pColor.R == pColor.B) && (pColor.R != 0) && (pColor.R != 255))
            return true;
         return false;
      }

      private void UpdateColorSwatch() {
         if ((mUseNamedCheckBox == null) || (mUseGrayscaleCheckBox == null) || (mRedUpDown == null) || (mGreenUpDown == null) ||
            (mBlueUpDown == null) || (mNamedColorsComboBox == null)) {
#pragma warning disable CS8602
#pragma warning disable CS8604
#pragma warning disable CS8629
            if (!mUseNamedCheckBox.Checked && !mUseGrayscaleCheckBox.Checked)
               mColorPickerExampleInnerPanel?.BackColor =
                  Color.FromArgb((int)mRedUpDown.Value, (int)mGreenUpDown.Value, (int)mBlueUpDown?.Value);
            else if (mUseGrayscaleCheckBox.Checked)
               mColorPickerExampleInnerPanel?.BackColor =
                  Color.FromArgb((int)mGrayUpDown?.Value, (int)mGrayUpDown?.Value, (int)mGrayUpDown?.Value);
            else if (mUseNamedCheckBox.Checked) {
               if (IsKnownColor(mNamedColorsComboBox?.Text, out Color opColor))
                  mColorPickerExampleInnerPanel?.BackColor = opColor;
            }
#pragma warning restore CS8629
#pragma warning disable CS8604
#pragma warning restore CS8602
         }
      }
   }
}
