namespace DBCode {
   namespace Themes {
      public sealed partial class ColorPickerPanel : Panel {
         private Button mBluePrefixButton, mGrayPrefixButton, mGreenPrefixButton,
            mNamedColorPrefixButton, mRedPrefixButton;
         private CheckBox mUseGrayscaleCheckBox, mUseNamedCheckBox;
         private Color mInitialColor;
         private ColorSwatch mBlueSwatch, mGraySwatch, mGreenSwatch, mRedSwatch;
         private ColorSwatchUsage mColorSwatchUsage;
         private ComboBox mNamedColorsComboBox;
         private GroupBox mCustomColorGroupBox, mNamedColorsGroupBox;
         private TwoLineHeaderLabelCluster mTitleLabel;
         private LabeledColorSwatchCluster mDemoSwatch;
         private NumericUpDown mBlueUpDown, mGrayUpDown, mGreenUpDown, mRedUpDown;
         private Panel mScrollPanel;
         internal BottomPanel mColorPickerBottomPanel;
         private Theme mTheme;
         private TrackBar mBlueSlider, mGraySlider, mGreenSlider, mRedSlider;

         public ColorPickerPanel(Theme pTheme, ColorSwatchUsage pColorSwatchUsage, Color pInitialColor) {
            mInitialColor = pInitialColor;
            mColorSwatchUsage = pColorSwatchUsage;
            mTheme = pTheme;
            Color interfaceBackground = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground],
               interfaceFont = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               groupBoxBackground = mTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
            Font interfaceTextFont = mTheme.mFonts[(int)FontUsage.Interface];
            mTitleLabel = new TwoLineHeaderLabelCluster(mTheme, "Select A Color",
               $"Use this color for {ToDescription((ColorSwatchUsage)mColorSwatchUsage)}");
            mScrollPanel = new Panel {
               Name = $"ColorPickerScrollPanel{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               AutoScroll = true,
               BackColor = interfaceBackground,
               Anchor = mAnchorTopLeftBottomRight
            };
            mNamedColorsGroupBox = new GroupBox {
               Name = $"NamedColorsGroupBox{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               Text = "Named Colors",
               Font = CreateNewBoldFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = groupBoxBackground
            };
            mUseNamedCheckBox = new CheckBox {
               Name = $"UseNamedCheckBox{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Use Named Colors Only",
               AutoSize = true,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = Color.Transparent
            };
            mNamedColorPrefixButton = new Button {
               Name = $"NamedColorPrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "Named Color &Drop-down:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont
            };
            mNamedColorsComboBox = new ComboBox {
               Name = $"NamedColorsComboBox{mTabIndex}",
               TabIndex = mTabIndex++,
               DropDownStyle = ComboBoxStyle.DropDown,
               Font = CreateNewFont(interfaceTextFont)
            };
            PropertyInfo[] predefinedColors = typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.Public)
               .Where(p => p.PropertyType == typeof(Color)).ToArray();
            foreach (PropertyInfo color in predefinedColors)
               mNamedColorsComboBox.Items.Add(MassageColorName(color.Name));
            FlattenButton(mNamedColorPrefixButton, groupBoxBackground);
            mNamedColorsGroupBox.Controls.AddRange([mUseNamedCheckBox, mNamedColorPrefixButton, mNamedColorsComboBox]);
            mCustomColorGroupBox = new GroupBox {
               Name = $"CustomColorGroupBox{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               Text = "Custom Color",
               Font = CreateNewBoldFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = groupBoxBackground
            };
            mUseGrayscaleCheckBox = new CheckBox {
               Name = $"UseGrayscaleCheckBox{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Use Grayscale Only",
               AutoSize = true,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = Color.Transparent
            };
            mGrayPrefixButton = new Button {
               Name = $"GrayPrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "Gra&y Value:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont
            };
            mGrayUpDown = new NumericUpDown {
               Name = $"GrayUpDown{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 128,
               Font = CreateNewFont(interfaceTextFont)
            };
            mGraySlider = new TrackBar {
               Name = $"GraySlider{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 128,
               TickFrequency = 16,
               Width = 200
            };
            mGraySwatch = new ColorSwatch(ColorPickerSwatchUsage.Gray, GrayFromInitialColor(), -1);
            FlattenButton(mGrayPrefixButton, groupBoxBackground);
            mRedPrefixButton = new Button {
               Name = $"RedPrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Red Value:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont
            };
            mRedUpDown = new NumericUpDown {
               Name = $"RedUpDown{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 255,
               Font = CreateNewFont(interfaceTextFont)
            };
            mRedSlider = new TrackBar {
               Name = $"RedSlider{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 255,
               TickFrequency = 16,
               Width = 200
            };
            mRedSwatch = new ColorSwatch(ColorPickerSwatchUsage.Red, RedFromInitialColor(), -1);
            FlattenButton(mRedPrefixButton, groupBoxBackground);
            mGreenPrefixButton = new Button {
               Name = $"GreenPrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Green Value:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont
            };
            mGreenUpDown = new NumericUpDown {
               Name = $"GreenUpDown{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 0,
               Font = CreateNewFont(interfaceTextFont)
            };
            mGreenSlider = new TrackBar {
               Name = $"GreenSlider{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 0,
               TickFrequency = 16,
               Width = 200
            };
            mGreenSwatch = new ColorSwatch(ColorPickerSwatchUsage.Green, GreenFromInitialColor(), -1);
            FlattenButton(mGreenPrefixButton, groupBoxBackground);
            mBluePrefixButton = new Button {
               Name = $"BluePrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Blue Value:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont
            };
            mBlueUpDown = new NumericUpDown {
               Name = $"BlueUpDown{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 0,
               Font = CreateNewFont(interfaceTextFont)
            };
            mBlueSlider = new TrackBar {
               Name = $"BlueSlider{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 0,
               TickFrequency = 16,
               Width = 200
            };
            mBlueSwatch = new ColorSwatch(ColorPickerSwatchUsage.Blue, BlueFromInitialColor(), -1);
            FlattenButton(mBluePrefixButton, groupBoxBackground);
            mCustomColorGroupBox.Controls.AddRange([mUseGrayscaleCheckBox, mGrayPrefixButton, mGrayUpDown,
               mGraySlider, mGraySwatch, mRedPrefixButton, mRedUpDown, mRedSlider, mRedSwatch,
               mGreenPrefixButton, mGreenUpDown, mGreenSlider, mGreenSwatch, mBluePrefixButton,
               mBlueUpDown, mBlueSlider, mBlueSwatch]);
            mDemoSwatch = new LabeledColorSwatchCluster(mTheme, "Example:", LabelPosition.Left, mInitialColor);
            mCustomColorGroupBox.Controls.Add(mDemoSwatch);
            mColorPickerBottomPanel = new BottomPanel(mTheme, "&Cancel") {
               Name = $"ColorPickerBottomPanel{mTabIndex++}"
            };
            mColorPickerBottomPanel.mHelpButton!.Text = "Help";
            mColorPickerBottomPanel.mHelpButton.Tag = new HelpTag(HelpContext.ColorPicker);
            mColorPickerBottomPanel.mHelpButton.Click += MainForm.Help_Click;
            Button okButton = new Button {
               Name = $"ColorPickerOkButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&OK",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mColorPickerBottomPanel.AddRightControl(okButton);
            okButton.Click += OkButton_Click;
            mColorPickerBottomPanel.mCancelButton!.Click += CancelButton_Click;
            BackColor = interfaceBackground;
            mScrollPanel.Controls.AddRange([mNamedColorsGroupBox, mCustomColorGroupBox]);
            Controls.AddRange([mScrollPanel, mColorPickerBottomPanel, mTitleLabel]);
            AttachEventHandlers();
         }

         public void LayoutControls() {
            ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
            ThrowIfNull(mNamedColorsGroupBox, nameof(mNamedColorsGroupBox));
            ThrowIfNull(mCustomColorGroupBox, nameof(mCustomColorGroupBox));
            RemoveEventHandlers();
            SetFontsAndColors();
            mNamedColorsGroupBox.Location = new Point(mEm, mEm);
            mUseNamedCheckBox.Location = GetGroupBoxFirstLineOffset(mNamedColorsGroupBox);
            mNamedColorPrefixButton.Location = new Point(mEm, mUseNamedCheckBox.Bottom + mEm);
            mNamedColorsComboBox.Location = new Point(mNamedColorPrefixButton.Right, mNamedColorPrefixButton.Top);
            mNamedColorsGroupBox.Width = mNamedColorsComboBox.Right + mEm;
            mNamedColorsGroupBox.Height = mNamedColorsComboBox.Bottom + mEm;
            SizeGroupBox(mNamedColorsGroupBox);
            mCustomColorGroupBox.Location = new Point(mEm, mNamedColorsGroupBox.Bottom + mEm);
            mUseGrayscaleCheckBox.Location = GetGroupBoxFirstLineOffset(mCustomColorGroupBox);
            mGrayPrefixButton.Location = new Point(mEm, mUseGrayscaleCheckBox.Bottom + mEm);
            mGrayUpDown.Location = new Point(mGrayPrefixButton.Right, mGrayPrefixButton.Top);
            mGraySlider.Location = new Point(mGrayUpDown.Right + mEm, mGrayUpDown.Top);
            mGraySwatch.Location = new Point(mGraySlider.Right + mEm, mGraySlider.Top);
            int rowTop = mGraySlider.Bottom + mEm2;
            mRedPrefixButton.Location = new Point(mEm, rowTop);
            mRedUpDown.Location = new Point(mRedPrefixButton.Right, rowTop);
            mRedSlider.Location = new Point(mRedUpDown.Right + mEm, mRedUpDown.Top);
            mRedSwatch.Location = new Point(mRedSlider.Right + mEm, mRedSlider.Top);
            rowTop = mRedSlider.Bottom + mEm;
            mGreenPrefixButton.Location = new Point(mEm, rowTop);
            mGreenUpDown.Location = new Point(mGreenPrefixButton.Right, rowTop);
            mGreenSlider.Location = new Point(mGreenUpDown.Right + mEm, mGreenUpDown.Top);
            mGreenSwatch.Location = new Point(mGreenSlider.Right + mEm, mGreenSlider.Top);
            rowTop = mGreenSlider.Bottom + mEm;
            mBluePrefixButton.Location = new Point(mEm, rowTop);
            mBlueUpDown.Location = new Point(mBluePrefixButton.Right, rowTop);
            mBlueSlider.Location = new Point(mBlueUpDown.Right + mEm, mBlueUpDown.Top);
            mBlueSwatch.Location = new Point(mBlueSlider.Right + mEm, mBlueSlider.Top);
            mDemoSwatch.Location = new Point(mEm3, mBlueSlider.Bottom + mEm2);
            SizeGroupBox(mCustomColorGroupBox);
            mScrollPanel.Location = new Point(1, mTitleLabel.Bottom);
            mScrollPanel.Size = new Size(ClientSize.Width - 2,
               ClientSize.Height - mTitleLabel.Height - mColorPickerBottomPanel.Height);
            mColorPickerBottomPanel.Top = mScrollPanel.Bottom;
            mColorPickerBottomPanel.LayoutControls();
            RestoreColorPickerHandlers();
         }

         internal Size GetRequiredSize() {
            int contentWidth = mCustomColorGroupBox.Right + (mEm * 2);
            int contentHeight = mCustomColorGroupBox.Bottom + (mEm * 2);
            int totalWidth = contentWidth + SystemInformation.VerticalScrollBarWidth + (mEm * 2);
            int totalHeight = mTitleLabel.Height + contentHeight + mColorPickerBottomPanel.Height + (mEm * 2);
            return new Size(totalWidth, totalHeight);
         }

         private void AttachEventHandlers() {
            mUseNamedCheckBox.CheckedChanged += UseNamedCheckBox_CheckedChanged;
            mNamedColorPrefixButton.Click += NamedColorPrefixButton_Click;
            mNamedColorsComboBox.SelectedValueChanged += NamedColorsComboBox_SelectedValueChanged;
            mNamedColorsComboBox.KeyUp += NamedColorsComboBox_KeyUp;
            mNamedColorsComboBox.Leave += NamedColorsComboBox_Leave;
            mUseGrayscaleCheckBox.CheckedChanged += UseGrayscaleCheckBox_CheckedChanged;
            mGrayUpDown.ValueChanged += GrayUpDown_ValueChanged;
            mGraySlider.ValueChanged += GraySlider_ValueChanged;
            mRedUpDown.ValueChanged += RedUpDown_ValueChanged;
            mRedSlider.ValueChanged += RedSlider_ValueChanged;
            mGreenUpDown.ValueChanged += GreenUpDown_ValueChanged;
            mGreenSlider.ValueChanged += GreenSlider_ValueChanged;
            mBlueUpDown.ValueChanged += BlueUpDown_ValueChanged;
            mBlueSlider.ValueChanged += BlueSlider_ValueChanged;
         }

         private void RemoveEventHandlers() {
            mUseNamedCheckBox.CheckedChanged -= UseNamedCheckBox_CheckedChanged;
            mNamedColorPrefixButton.Click -= NamedColorPrefixButton_Click;
            mNamedColorsComboBox.SelectedValueChanged -= NamedColorsComboBox_SelectedValueChanged;
            mNamedColorsComboBox.KeyUp -= NamedColorsComboBox_KeyUp;
            mNamedColorsComboBox.Leave -= NamedColorsComboBox_Leave;
            mUseGrayscaleCheckBox.CheckedChanged -= UseGrayscaleCheckBox_CheckedChanged;
            mGrayUpDown.ValueChanged -= GrayUpDown_ValueChanged;
            mGraySlider.ValueChanged -= GraySlider_ValueChanged;
            mRedUpDown.ValueChanged -= RedUpDown_ValueChanged;
            mRedSlider.ValueChanged -= RedSlider_ValueChanged;
            mGreenUpDown.ValueChanged -= GreenUpDown_ValueChanged;
            mGreenSlider.ValueChanged -= GreenSlider_ValueChanged;
            mBlueUpDown.ValueChanged -= BlueUpDown_ValueChanged;
            mBlueSlider.ValueChanged -= BlueSlider_ValueChanged;
         }

         private void RestoreColorPickerHandlers() {
            mUseNamedCheckBox.CheckedChanged += UseNamedCheckBox_CheckedChanged;
            mNamedColorPrefixButton.Click += NamedColorPrefixButton_Click;
            mNamedColorsComboBox.SelectedValueChanged += NamedColorsComboBox_SelectedValueChanged;
            mNamedColorsComboBox.KeyUp += NamedColorsComboBox_KeyUp;
            mNamedColorsComboBox.Leave += NamedColorsComboBox_Leave;
            mUseGrayscaleCheckBox.CheckedChanged += UseGrayscaleCheckBox_CheckedChanged;
            mGrayUpDown.ValueChanged += GrayUpDown_ValueChanged;
            mGraySlider.ValueChanged += GraySlider_ValueChanged;
            mRedUpDown.ValueChanged += RedUpDown_ValueChanged;
            mRedSlider.ValueChanged += RedSlider_ValueChanged;
            mGreenUpDown.ValueChanged += GreenUpDown_ValueChanged;
            mGreenSlider.ValueChanged += GreenSlider_ValueChanged;
            mBlueUpDown.ValueChanged += BlueUpDown_ValueChanged;
            mBlueSlider.ValueChanged += BlueSlider_ValueChanged;
         }

         private void SetColorValues(Color pColor) {
            int gray = (pColor.R + pColor.G + pColor.B) / 3;
            RemoveEventHandlers();
            mGrayUpDown.Value = gray;
            mGraySlider.Value = gray;
            mRedUpDown.Value = pColor.R;
            mGreenUpDown.Value = pColor.G;
            mBlueUpDown.Value = pColor.B;
            mRedSlider.Value = pColor.R;
            mGreenSlider.Value = pColor.G;
            mBlueSlider.Value = pColor.B;
            UpdateSwatches();
            AttachEventHandlers();
         }

         private Color GrayFromInitialColor() {
            int gray = (mInitialColor.R + mInitialColor.G + mInitialColor.B) / 3;
            return Color.FromArgb(gray, gray, gray);
         }

         private Color RedFromInitialColor() {
            return Color.FromArgb(mInitialColor.R, 0, 0);
         }

         private Color GreenFromInitialColor() {
            return Color.FromArgb(0, mInitialColor.G, 0);
         }

         private Color BlueFromInitialColor() {
            return Color.FromArgb(0, 0, mInitialColor.B);
         }

         private void UpdateSwatches() {
            int r = (int)mRedUpDown.Value;
            int g = (int)mGreenUpDown.Value;
            int b = (int)mBlueUpDown.Value;
            int gray = (int)mGrayUpDown.Value;
            mRedSwatch.BackColor = Color.FromArgb(r, 0, 0);
            mGreenSwatch.BackColor = Color.FromArgb(0, g, 0);
            mBlueSwatch.BackColor = Color.FromArgb(0, 0, b);
            mGraySwatch.BackColor = Color.FromArgb(gray, gray, gray);
            if (mUseGrayscaleCheckBox.Checked)
               mDemoSwatch.BackColor = Color.FromArgb(gray, gray, gray);
            else
               mDemoSwatch.BackColor = Color.FromArgb(r, g, b);
         }

         private void DisableCustomControls() {
            mUseGrayscaleCheckBox.Enabled = false;
            mGrayPrefixButton.Enabled = false;
            mGrayUpDown.Enabled = false;
            mGraySlider.Enabled = false;
            mRedPrefixButton.Enabled = false;
            mRedUpDown.Enabled = false;
            mRedSlider.Enabled = false;
            mGreenPrefixButton.Enabled = false;
            mGreenUpDown.Enabled = false;
            mGreenSlider.Enabled = false;
            mBluePrefixButton.Enabled = false;
            mBlueUpDown.Enabled = false;
            mBlueSlider.Enabled = false;
         }

         private void EnableCustomControls() {
            bool isGray = mUseGrayscaleCheckBox.Checked;
            mUseGrayscaleCheckBox.Enabled = true;
            mGrayPrefixButton.Enabled = isGray;
            mGrayUpDown.Enabled = isGray;
            mGraySlider.Enabled = isGray;
            mRedPrefixButton.Enabled = !isGray;
            mRedUpDown.Enabled = !isGray;
            mRedSlider.Enabled = !isGray;
            mGreenPrefixButton.Enabled = !isGray;
            mGreenUpDown.Enabled = !isGray;
            mGreenSlider.Enabled = !isGray;
            mBluePrefixButton.Enabled = !isGray;
            mBlueUpDown.Enabled = !isGray;
            mBlueSlider.Enabled = !isGray;
         }

         private void SetFontsAndColors() {
            Color backColor = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground],
               foreColor = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               groupBoxBackgroundColor = mTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
            Font interfaceFont = mTheme.mFonts[(int)FontUsage.Interface];
            BackColor = backColor;
            mScrollPanel.BackColor = backColor;
            mBluePrefixButton.Font = interfaceFont;
            mBluePrefixButton.ForeColor = foreColor;
            mBluePrefixButton.BackColor = backColor;
            FlattenButton(mBluePrefixButton, groupBoxBackgroundColor);
            mGrayPrefixButton.Font = interfaceFont;
            mGrayPrefixButton.ForeColor = foreColor;
            mGrayPrefixButton.BackColor = backColor;
            FlattenButton(mGrayPrefixButton, groupBoxBackgroundColor);
            mGreenPrefixButton.Font = interfaceFont;
            mGreenPrefixButton.ForeColor = foreColor;
            mGreenPrefixButton.BackColor = backColor;
            FlattenButton(mGreenPrefixButton, groupBoxBackgroundColor);
            mNamedColorPrefixButton.Font = interfaceFont;
            mNamedColorPrefixButton.ForeColor = foreColor;
            mNamedColorPrefixButton.BackColor = backColor;
            FlattenButton(mNamedColorPrefixButton, groupBoxBackgroundColor);
            mRedPrefixButton.Font = interfaceFont;
            mRedPrefixButton.ForeColor = foreColor;
            mRedPrefixButton.BackColor = backColor;
            FlattenButton(mRedPrefixButton, groupBoxBackgroundColor);
            mUseGrayscaleCheckBox.Font = interfaceFont;
            mUseGrayscaleCheckBox.ForeColor = foreColor;
            mUseGrayscaleCheckBox.BackColor = Color.Transparent;
            mUseNamedCheckBox.Font = interfaceFont;
            mUseNamedCheckBox.ForeColor = foreColor;
            mUseNamedCheckBox.BackColor = Color.Transparent;
            mNamedColorsComboBox.Font = interfaceFont;
            mNamedColorsComboBox.ForeColor = foreColor;
            mNamedColorsComboBox.BackColor = backColor;
            mCustomColorGroupBox.Font = CreateNewBoldFont(interfaceFont);
            mCustomColorGroupBox.ForeColor = foreColor;
            mCustomColorGroupBox.BackColor = groupBoxBackgroundColor;
            mNamedColorsGroupBox.Font = CreateNewBoldFont(interfaceFont);
            mNamedColorsGroupBox.ForeColor = foreColor;
            mNamedColorsGroupBox.BackColor = groupBoxBackgroundColor;
            mBlueUpDown.Font = interfaceFont;
            mBlueUpDown.ForeColor = foreColor;
            mBlueUpDown.BackColor = backColor;
            mGrayUpDown.Font = interfaceFont;
            mGrayUpDown.ForeColor = foreColor;
            mGrayUpDown.BackColor = backColor;
            mGreenUpDown.Font = interfaceFont;
            mGreenUpDown.ForeColor = foreColor;
            mGreenUpDown.BackColor = backColor;
            mRedUpDown.Font = interfaceFont;
            mRedUpDown.ForeColor = foreColor;
            mRedUpDown.BackColor = backColor;
            mColorPickerBottomPanel.SetFontAndColor();
            mTitleLabel.SetFontAndColor();
            mDemoSwatch.SetFontAndColor();
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               RemoveEventHandlers();
               mColorPickerBottomPanel.mHelpButton!.Click -= MainForm.Help_Click;
               mColorPickerBottomPanel.mCancelButton!.Click -= CancelButton_Click;
               mTitleLabel.Dispose();
               mUseNamedCheckBox.Dispose();
               mNamedColorPrefixButton.Dispose();
               mNamedColorsComboBox.Dispose();
               mNamedColorsGroupBox.Dispose();
               mUseGrayscaleCheckBox.Dispose();
               mGrayPrefixButton.Dispose();
               mGrayUpDown.Dispose();
               mGraySlider.Dispose();
               mGraySwatch.Dispose();
               mRedPrefixButton.Dispose();
               mRedUpDown.Dispose();
               mRedSlider.Dispose();
               mRedSwatch.Dispose();
               mGreenPrefixButton.Dispose();
               mGreenUpDown.Dispose();
               mGreenSlider.Dispose();
               mGreenSwatch.Dispose();
               mBluePrefixButton.Dispose();
               mBlueUpDown.Dispose();
               mBlueSlider.Dispose();
               mBlueSwatch.Dispose();
               mDemoSwatch.Dispose();
               mCustomColorGroupBox.Dispose();
               mColorPickerBottomPanel.Dispose();
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
