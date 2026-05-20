namespace DBCode {
   namespace Themes {
      internal sealed partial class ColorPickerPanel : ScrollablePanel {
         internal BottomPanel mBottomPanel;
         private bool mIsSyntaxColor;
         private Button mBluePrefixButton, mGrayPrefixButton, mGreenPrefixButton,
            mNamedColorPrefixButton, mRedPrefixButton, mOKButton;
         private ScalableCheckBox mUseGrayscaleCheckBox, mUseNamedCheckBox;
         private Color mInitialColor;
         private ColorSwatch mBlueSwatch, mGraySwatch, mGreenSwatch, mRedSwatch;
         private ColorSwatchUsage mColorSwatchUsage;
         private ComboBox mNamedColorsComboBox;
         private List<Font> mOwnedFonts = [];
         private GroupBox mCustomColorGroupBox, mNamedColorsGroupBox;
         private LabeledColorSwatchCluster mDemoSwatch;
         private LanguageKind mLanguageKind;
         private NumericUpDown mBlueUpDown, mGrayUpDown, mGreenUpDown, mRedUpDown;
         private Panel mScrollPanel;
         internal Theme mTheme;
         private TokenKind mTokenKind;
         private TrackBar mBlueSlider, mGraySlider, mGreenSlider, mRedSlider;
         internal TwoLineHeaderLabelCluster mTitleLabel;

         public ColorPickerPanel(Theme pTheme, ColorSwatchUsage pColorSwatchUsage, Color pInitialColor) {
            mInitialColor = pInitialColor;
            mColorSwatchUsage = pColorSwatchUsage;
            mTheme = pTheme;
            Color interfaceBackground = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground],
               interfaceFont = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               groupBoxBackground = mTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
            Font interfaceTextFont = mTheme.mFonts[(int)FontUsage.Interface];
            Name = "colorPickerPanel";
            TabIndex = mTabIndex++;
            mTitleLabel = new TwoLineHeaderLabelCluster(mTheme, "Select A Color",
               $"Use this color for {ToDescription(mColorSwatchUsage)}");
            mScrollPanel = new Panel {
               Name = $"ColorPickerScrollPanel{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               AutoScroll = true,
               BackColor = interfaceBackground
            };
            mNamedColorsGroupBox = new GroupBox {
               Name = $"NamedColorsGroupBox{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               Text = "Named Colors",
               ForeColor = interfaceFont,
               BackColor = groupBoxBackground
            };
            mUseNamedCheckBox = new ScalableCheckBox("&Use Named Colors Only", mTheme, false) {
               Name = $"UseNamedCheckBox{mTabIndex}",
               TabIndex = mTabIndex++
            };
            mNamedColorPrefixButton = new Button {
               Name = $"NamedColorPrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "Named Color &Drop-down:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               ForeColor = interfaceFont
            };
            mNamedColorsComboBox = new ComboBox {
               Name = $"NamedColorsComboBox{mTabIndex}",
               TabIndex = mTabIndex++,
               DropDownStyle = ComboBoxStyle.DropDown,
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
               ForeColor = interfaceFont,
               BackColor = groupBoxBackground
            };
            mUseGrayscaleCheckBox = new ScalableCheckBox("&Use Grayscale Only", mTheme, false) {
               Name = $"UseGrayscaleCheckBox{mTabIndex}",
               TabIndex = mTabIndex++
            };
            mGrayPrefixButton = new Button {
               Name = $"GrayPrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "Gra&y Value:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               ForeColor = interfaceFont
            };
            mGrayUpDown = new NumericUpDown {
               Name = $"GrayUpDown{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 128
            };
            SetUpDownBoxWidth(mGrayUpDown);
            mGraySlider = new TrackBar {
               Name = $"GraySlider{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 128,
               TickFrequency = 16,
               Width = 200
            };
            mGraySwatch = new ColorSwatch(ColorPickerSwatchUsage.Gray, GraySwatchColor(mInitialColor), -1);
            FlattenButton(mGrayPrefixButton, groupBoxBackground);
            mRedPrefixButton = new Button {
               Name = $"RedPrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Red Value:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               ForeColor = interfaceFont
            };
            mRedUpDown = new NumericUpDown {
               Name = $"RedUpDown{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 255
            };
            SetUpDownBoxWidth(mRedUpDown);
            mRedSlider = new TrackBar {
               Name = $"RedSlider{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 255,
               TickFrequency = 16,
               Width = 200
            };
            mRedSwatch = new ColorSwatch(ColorPickerSwatchUsage.Red, RedSwatchColor(mInitialColor), -1);
            FlattenButton(mRedPrefixButton, groupBoxBackground);
            mGreenPrefixButton = new Button {
               Name = $"GreenPrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Green Value:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               ForeColor = interfaceFont
            };
            mGreenUpDown = new NumericUpDown {
               Name = $"GreenUpDown{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 0
            };
            SetUpDownBoxWidth(mGreenUpDown);
            mGreenSlider = new TrackBar {
               Name = $"GreenSlider{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 0,
               TickFrequency = 16,
               Width = 200
            };
            mGreenSwatch = new ColorSwatch(ColorPickerSwatchUsage.Green, GreenSwatchColor(mInitialColor), -1);
            FlattenButton(mGreenPrefixButton, groupBoxBackground);
            mBluePrefixButton = new Button {
               Name = $"BluePrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Blue Value:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               ForeColor = interfaceFont
            };
            mBlueUpDown = new NumericUpDown {
               Name = $"BlueUpDown{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 0
            };
            SetUpDownBoxWidth(mBlueUpDown);
            mBlueSlider = new TrackBar {
               Name = $"BlueSlider{mTabIndex}",
               TabIndex = mTabIndex++,
               Minimum = 0,
               Maximum = 255,
               Value = 0,
               TickFrequency = 16,
               Width = 200
            };
            mBlueSwatch = new ColorSwatch(ColorPickerSwatchUsage.Blue, BlueSwatchColor(mInitialColor), -1);
            FlattenButton(mBluePrefixButton, groupBoxBackground);
            mCustomColorGroupBox.Controls.AddRange([mUseGrayscaleCheckBox, mGrayPrefixButton, mGrayUpDown,
               mGraySlider, mGraySwatch, mRedPrefixButton, mRedUpDown, mRedSlider, mRedSwatch,
               mGreenPrefixButton, mGreenUpDown, mGreenSlider, mGreenSwatch, mBluePrefixButton,
               mBlueUpDown, mBlueSlider, mBlueSwatch]);
            mDemoSwatch = new LabeledColorSwatchCluster(mTheme, "Example:", LabelPosition.Left, mInitialColor);
            mCustomColorGroupBox.Controls.Add(mDemoSwatch);
            mBottomPanel = new BottomPanel(mTheme, "&Cancel") {
               Name = $"ColorPickerBottomPanel{mTabIndex++}"
            };
            ThrowIfNull(mBottomPanel.mHelpButton, nameof(mBottomPanel.mHelpButton));
            ThrowIfNull(mBottomPanel.mCancelButton, nameof(mBottomPanel.mCancelButton));
            mBottomPanel.mHelpButton.Text = "Help";
            mBottomPanel.mHelpButton.Tag = new HelpTag(HelpContext.ColorPicker);
            mBottomPanel.mHelpButton.Click += MainForm.Help_Click;
            mOKButton = new Button {
               Name = $"ColorPickerOkButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&OK",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mBottomPanel.AddRightControl(mOKButton);
            mOKButton.Click += OkButton_Click;
            mBottomPanel.mCancelButton.Click += CancelButton_Click;
            BackColor = interfaceBackground;
            mScrollPanel.Controls.AddRange([mNamedColorsGroupBox, mCustomColorGroupBox]);
            Controls.AddRange([mScrollPanel, mBottomPanel, mTitleLabel]);
            AttachEventHandlers();
         }

         public ColorPickerPanel(Theme pTheme, TokenKind pTokenKind, LanguageKind pLanguageKind, Color pInitialColor)
            : this(pTheme, ColorSwatchUsage.InterfaceBackground, pInitialColor) {
            mTokenKind = pTokenKind;
            mLanguageKind = pLanguageKind;
            mIsSyntaxColor = true;
            mTitleLabel.ResetBottomLabel($"Use this color for {ToDescription(pTokenKind)} ({pLanguageKind})");
         }

         public void LayoutControls() {
            ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
            ThrowIfNull(mNamedColorsGroupBox, nameof(mNamedColorsGroupBox));
            ThrowIfNull(mCustomColorGroupBox, nameof(mCustomColorGroupBox));
            ThrowIfNull(mColorPickerPanel, nameof(mColorPickerPanel));
            ThrowIfNull(mForm, nameof(mForm));
            RemoveEventHandlers();
            SetFontsAndColors();
            SetColorValues(mInitialColor);
            mTitleLabel.LayoutCluster();
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
            SizePanel(mScrollPanel);
            mBottomPanel.Top = mScrollPanel.Bottom;
            mBottomPanel.LayoutControls();
            AttachEventHandlers();
            mForm.BeginInvoke(() => {
               mForm.SuspendClientSizeChanged();
               if (mUiState.mColorPickerFirstShow) {
                  Size wantedSize = GetRequiredSize();
                  Screen screen = Screen.FromPoint(mUiState.FormBounds.Location);
                  int width = Math.Min(wantedSize.Width, (int)(screen.WorkingArea.Width * 0.9));
                  int height = Math.Min(wantedSize.Height, (int)(screen.WorkingArea.Height * 0.9));
                  mForm.ClientSize = new Size(width, height);
                  CenterFormOnMonitor(mForm, screen);
                  mUiState.mColorPickerFirstShow = false;
               }
               else {
                  mForm.Bounds = mUiState.mColorPickerBounds;
                  EnsureWindowFitsMonitor(mForm);
               }
               mUiState.mColorPickerBounds = mForm.Bounds;
               mForm.Opacity = mUiState.mFormOpacity;
               mForm.ResumeClientSizeChanged();
               mBottomPanel.LayoutControls();
               mTitleLabel.LayoutCluster();
               mScrollPanel.Anchor = mAnchorTopLeftBottomRight;
            });
         }

         internal Size GetRequiredSize() {
            int contentWidth = mCustomColorGroupBox.Right + (mEm * 2);
            int contentHeight = mCustomColorGroupBox.Bottom + (mEm * 2);
            int totalWidth = contentWidth + SystemInformation.VerticalScrollBarWidth + (mEm * 2);
            int totalHeight = mTitleLabel.Height + contentHeight + mBottomPanel.Height + (mEm * 2);
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
            mGrayPrefixButton.Click += GrayPrefixButton_Click;
            mRedPrefixButton.Click += RedPrefixButton_Click;
            mGreenPrefixButton.Click += GreenPrefixButton_Click;
            mBluePrefixButton.Click += BluePrefixButton_Click;
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
            mGrayPrefixButton.Click -= GrayPrefixButton_Click;
            mRedPrefixButton.Click -= RedPrefixButton_Click;
            mGreenPrefixButton.Click -= GreenPrefixButton_Click;
            mBluePrefixButton.Click -= BluePrefixButton_Click;
         }

         private void SetColorValues(Color pColor) {
            int gray = (pColor.R + pColor.G + pColor.B) / 3;
            RemoveEventHandlers();
            mGrayUpDown.Value = gray;
            mGraySlider.Value = gray;
            mRedUpDown.Value = pColor.R;
            mRedSlider.Value = pColor.R;
            mGreenUpDown.Value = pColor.G;
            mGreenSlider.Value = pColor.G;
            mBlueUpDown.Value = pColor.B;
            mBlueSlider.Value = pColor.B;
            UpdateSwatches();
            AttachEventHandlers();
         }

         private static Color GraySwatchColor(Color pColor) {
            int gray = (int)(0.2126 * pColor.R + 0.7152 * pColor.G + 0.0722 * pColor.B);
            return Color.FromArgb(gray, gray, gray);
         }

         private static Color RedSwatchColor(Color pColor) {
            return Color.FromArgb(pColor.R, 0, 0);
         }

         private static Color GreenSwatchColor(Color pColor) {
            return Color.FromArgb(0, pColor.G, 0);
         }

         private static Color BlueSwatchColor(Color pColor) {
            return Color.FromArgb(0, 0, pColor.B);
         }

         private void UpdateSwatches() {
            int r = (int)mRedUpDown.Value;
            int g = (int)mGreenUpDown.Value;
            int b = (int)mBlueUpDown.Value;
            int gray = (int)mGrayUpDown.Value;
            Color workingColor = Color.FromArgb(r, g, b);

            mRedSwatch.SetColor(RedSwatchColor(workingColor));
            mGreenSwatch.SetColor(GreenSwatchColor(workingColor));
            mBlueSwatch.SetColor(BlueSwatchColor(workingColor));
            mGraySwatch.SetColor(Color.FromArgb(gray, gray, gray));
            if (mUseGrayscaleCheckBox.Checked)
               mDemoSwatch.SetColor(Color.FromArgb(gray, gray, gray));
            else
               mDemoSwatch.SetColor(Color.FromArgb(r, g, b));
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
            Font interfaceFont = mTheme.mFonts[(int)FontUsage.Interface], workingFont;
            List<Font> previousFonts = mOwnedFonts;
            mOwnedFonts = [];
            BackColor = backColor;
            mScrollPanel.BackColor = backColor;
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mBluePrefixButton.Font = workingFont;
            mBluePrefixButton.ForeColor = foreColor;
            mBluePrefixButton.BackColor = backColor;
            FlattenButton(mBluePrefixButton, groupBoxBackgroundColor);
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mGrayPrefixButton.Font = workingFont;
            mGrayPrefixButton.ForeColor = foreColor;
            mGrayPrefixButton.BackColor = backColor;
            FlattenButton(mGrayPrefixButton, groupBoxBackgroundColor);
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mGreenPrefixButton.Font = workingFont;
            mGreenPrefixButton.ForeColor = foreColor;
            mGreenPrefixButton.BackColor = backColor;
            FlattenButton(mGreenPrefixButton, groupBoxBackgroundColor);
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mNamedColorPrefixButton.Font = workingFont;
            mNamedColorPrefixButton.ForeColor = foreColor;
            mNamedColorPrefixButton.BackColor = backColor;
            FlattenButton(mNamedColorPrefixButton, groupBoxBackgroundColor);
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mRedPrefixButton.Font = workingFont;
            mRedPrefixButton.ForeColor = foreColor;
            mRedPrefixButton.BackColor = backColor;
            FlattenButton(mRedPrefixButton, groupBoxBackgroundColor);
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mOKButton.Font = workingFont;
            mOKButton.ForeColor = foreColor;
            mOKButton.BackColor = backColor;
            mUseGrayscaleCheckBox.SetFontAndColors(mTheme);
            mUseNamedCheckBox.SetFontAndColors(mTheme);
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mNamedColorsComboBox.Font = workingFont;
            mNamedColorsComboBox.ForeColor = foreColor;
            mNamedColorsComboBox.BackColor = backColor;
            workingFont = CreateNewBoldFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mCustomColorGroupBox.Font = workingFont;
            mCustomColorGroupBox.ForeColor = foreColor;
            mCustomColorGroupBox.BackColor = groupBoxBackgroundColor;
            workingFont = CreateNewBoldFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mNamedColorsGroupBox.Font = workingFont;
            mNamedColorsGroupBox.ForeColor = foreColor;
            mNamedColorsGroupBox.BackColor = groupBoxBackgroundColor;
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mBlueUpDown.Font = workingFont;
            mBlueUpDown.ForeColor = foreColor;
            mBlueUpDown.BackColor = backColor;
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mGrayUpDown.Font = workingFont;
            mGrayUpDown.ForeColor = foreColor;
            mGrayUpDown.BackColor = backColor;
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mGreenUpDown.Font = workingFont;
            mGreenUpDown.ForeColor = foreColor;
            mGreenUpDown.BackColor = backColor;
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mRedUpDown.Font = workingFont;
            mRedUpDown.ForeColor = foreColor;
            mRedUpDown.BackColor = backColor;
            mBottomPanel.SetFontAndColor();
            mTitleLabel.SetFontAndColor();
            mDemoSwatch.SetFontAndColor();
            foreach (Font font in previousFonts)
               MainForm.DisposeFontIfOwned(font);
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               RemoveEventHandlers();
               ThrowIfNull(mBottomPanel.mHelpButton, nameof(mBottomPanel.mHelpButton));
               ThrowIfNull(mBottomPanel.mCancelButton, nameof(mBottomPanel.mCancelButton));
               mBottomPanel.mHelpButton.Click -= MainForm.Help_Click;
               mBottomPanel.mCancelButton.Click -= CancelButton_Click;
               mOKButton.Click -= OkButton_Click;
               foreach (Font font in mOwnedFonts)
                  MainForm.DisposeFontIfOwned(font);
               mOwnedFonts.Clear();
               mTitleLabel.Dispose();
               MainForm.DisposeFontIfOwned(mUseNamedCheckBox.Font);
               mUseNamedCheckBox.Dispose();
               mNamedColorPrefixButton.Dispose();
               mNamedColorsComboBox.Dispose();
               mNamedColorsGroupBox.Dispose();
               MainForm.DisposeFontIfOwned(mUseGrayscaleCheckBox.Font);
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
               mBottomPanel.Dispose();
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
