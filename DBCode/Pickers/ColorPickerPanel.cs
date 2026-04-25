namespace DBCode {
   namespace Themes {
      public sealed partial class ColorPickerPanel : Panel {
         private Button? mBluePrefixButton, mCancelButton, mGrayPrefixButton, mGreenPrefixButton, mHelpButton,
            mNamedColorPrefixButton, mOkButton, mRedPrefixButton;
         private CheckBox? mUseGrayscaleCheckBox, mUseNamedCheckBox;
         private Color mInitialColor;
         private ColorSwatch? mBlueSwatch, mGraySwatch, mGreenSwatch, mRedSwatch;
         private ColorUsage mColorUsage;
         private ComboBox? mNamedColorsComboBox;
         private GroupBox? mCustomColorGroupBox, mNamedColorsGroupBox;
         private TwoLineHeaderLabelCluster? mTitleLabel;
         private LabeledColorSwatchCluster? mDemoSwatch;
         private NumericUpDown? mBlueUpDown, mGrayUpDown, mGreenUpDown, mRedUpDown;
         private Panel? mScrollPanel;
         private StatusStrip? mStatusStrip;
         private Theme? mTheme;
         private ToolStripControlHost? mCancelHost, mHelpHost, mOkHost;
         private ToolStripStatusLabel? mSpringLabel;
         private TrackBar? mBlueSlider, mGraySlider, mGreenSlider, mRedSlider;

         public ColorPickerPanel(Theme pTheme, ColorUsage pColorUsage, Color pInitialColor) {
            mInitialColor = pInitialColor;
            mColorUsage = pColorUsage;
            mTheme = pTheme;
            InitializeUI();
            LayoutControls();
            AttachEventHandlers();
         }

         private void InitializeUI() {
            if (mTheme == null)
               return;
            Color interfaceBackground = mTheme.mInterfaceColors[(int)ColorUsage.InterfaceBackground],
               interfaceFont = mTheme.mInterfaceColors[(int)ColorUsage.InterfaceFont],
               groupBoxBackground = mTheme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground];
            Font interfaceTextFont = mTheme.mFonts[(int)FontUsage.Interface];

            mTitleLabel = new TwoLineHeaderLabelCluster("Select A Color",
               $"Use this color for {ToDescription((ColorUsage)mColorUsage)}");
            mScrollPanel = new Panel {
               Name = $"ColorPickerScrollPanel{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               AutoScroll = true,
               BackColor = interfaceBackground,
               Dock = DockStyle.Fill,
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
            mNamedColorsGroupBox.Controls.AddRange([mUseNamedCheckBox, mNamedColorPrefixButton,
               mNamedColorsComboBox]);
            mCustomColorGroupBox = new GroupBox {
               Name = $"CustomColorGroupBox{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               Text = "Custom Color",
               Font = CreateNewBoldFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = groupBoxBackground
            };
            // Grayscale row
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
            mGraySwatch = new ColorSwatch(ColorSwatchUsage.Gray, GrayFromInitialColor(), -1);
            FlattenButton(mGrayPrefixButton, groupBoxBackground);
            // Red row
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
            mRedSwatch = new ColorSwatch(ColorSwatchUsage.Red, RedFromInitialColor(), -1);
            FlattenButton(mRedPrefixButton, groupBoxBackground);
            // Green row
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
            mGreenSwatch = new ColorSwatch(ColorSwatchUsage.Green, GreenFromInitialColor(), -1);
            FlattenButton(mGreenPrefixButton, groupBoxBackground);
            // Blue row
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
            mBlueSwatch = new ColorSwatch(ColorSwatchUsage.Blue, BlueFromInitialColor(), -1);
            FlattenButton(mBluePrefixButton, groupBoxBackground);
            mCustomColorGroupBox.Controls.AddRange([
               mUseGrayscaleCheckBox, mGrayPrefixButton, mGrayUpDown, mGraySlider, mGraySwatch,
               mRedPrefixButton, mRedUpDown, mRedSlider, mRedSwatch,
               mGreenPrefixButton, mGreenUpDown, mGreenSlider, mGreenSwatch,
               mBluePrefixButton, mBlueUpDown, mBlueSlider, mBlueSwatch
            ]);
            mDemoSwatch = new LabeledColorSwatchCluster("Example:", LabelPosition.Left, mInitialColor);
            mCustomColorGroupBox.Controls.Add(mDemoSwatch);
            // Status strip with buttons
            mStatusStrip = new StatusStrip {
               Name = $"ColorPickerStatusStrip{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               SizingGrip = true,
               Dock = DockStyle.Bottom
            };
            mHelpButton = new Button {
               Name = $"ColorPickerHelpButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Help",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont,
               Tag = new HelpTag(HelpContext.ColorPicker)
            };
            mOkButton = new Button {
               Name = $"ColorPickerOkButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&OK",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont,
               Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            mCancelButton = new Button {
               Name = $"ColorPickerCancelButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Cancel",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont,
               Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            mHelpHost = new ToolStripControlHost(mHelpButton);
            mOkHost = new ToolStripControlHost(mOkButton);
            mCancelHost = new ToolStripControlHost(mCancelButton);
            mSpringLabel = new ToolStripStatusLabel {
               Spring = true
            };
            mStatusStrip.Items.AddRange([mHelpHost, mSpringLabel, mOkHost, mCancelHost]);
            this.BackColor = interfaceBackground;
            mScrollPanel.Controls.AddRange([mNamedColorsGroupBox, mCustomColorGroupBox]);
            Controls.AddRange([mScrollPanel, mTitleLabel, mStatusStrip]);
         }

         public void LayoutControls() {
            if (mTitleLabel == null || mNamedColorsGroupBox == null ||
                mCustomColorGroupBox == null || mStatusStrip == null)
               return;
            RemoveEventHandlers();
            SetFontsAndColors();
            mNamedColorsGroupBox.Location = new Point(mEm, mEm);
            if (mUseNamedCheckBox != null && mNamedColorPrefixButton != null && mNamedColorsComboBox != null) {
               mUseNamedCheckBox.Location = GetGroupBoxFirstLineOffset(mNamedColorsGroupBox);
               mNamedColorPrefixButton.Location = new Point(mEm, mUseNamedCheckBox.Bottom + mEm);
               mNamedColorsComboBox.Location = new Point(mNamedColorPrefixButton.Right, mNamedColorPrefixButton.Top);
               mNamedColorsGroupBox.Width = mNamedColorsComboBox.Right + mEm;
               mNamedColorsGroupBox.Height = mNamedColorsComboBox.Bottom + mEm;
            }
            SizeGroupBox(mNamedColorsGroupBox);
            mCustomColorGroupBox.Location = new Point(mEm, mNamedColorsGroupBox.Bottom + mEm);
            if (mUseGrayscaleCheckBox != null && mGrayPrefixButton != null && mGrayUpDown != null &&
                mGraySlider != null && mGraySwatch != null &&
                mRedPrefixButton != null && mRedUpDown != null && mRedSlider != null && mRedSwatch != null &&
                mGreenPrefixButton != null && mGreenUpDown != null && mGreenSlider != null && mGreenSwatch != null &&
                mBluePrefixButton != null && mBlueUpDown != null && mBlueSlider != null && mBlueSwatch != null &&
                mDemoSwatch != null) {
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
            }
            this.Width = Math.Max(mNamedColorsGroupBox.Right, mCustomColorGroupBox.Right) + mEm +
               SystemInformation.VerticalScrollBarWidth;
            this.Height = mCustomColorGroupBox.Bottom + mStatusStrip.Height + mEm + SystemInformation.HorizontalScrollBarHeight;
            AttachEventHandlers();
         }

         internal Size GetRequiredSize() {
            if (mScrollPanel == null || mTitleLabel == null || mStatusStrip == null || mCustomColorGroupBox == null)
               return new Size(800, 600);
            int contentWidth = mCustomColorGroupBox.Right + (mEm * 2);
            int contentHeight = mCustomColorGroupBox.Bottom + (mEm * 2);
            int totalWidth = contentWidth + SystemInformation.VerticalScrollBarWidth + (mEm * 2);
            int totalHeight = mTitleLabel.Height + contentHeight + mStatusStrip.Height + (mEm * 2);
            return new Size(totalWidth, totalHeight);
         }

         private void AttachEventHandlers() {
            if (mUseNamedCheckBox != null)
               mUseNamedCheckBox.CheckedChanged += UseNamedCheckBox_CheckedChanged;
            if (mNamedColorPrefixButton != null)
               mNamedColorPrefixButton.Click += NamedColorPrefixButton_Click;
            if (mNamedColorsComboBox != null) {
               mNamedColorsComboBox.SelectedValueChanged += NamedColorsComboBox_SelectedValueChanged;
               mNamedColorsComboBox.KeyUp += NamedColorsComboBox_KeyUp;
               mNamedColorsComboBox.Leave += NamedColorsComboBox_Leave;
            }
            if (mUseGrayscaleCheckBox != null)
               mUseGrayscaleCheckBox.CheckedChanged += UseGrayscaleCheckBox_CheckedChanged;
            if (mGrayUpDown != null)
               mGrayUpDown.ValueChanged += GrayUpDown_ValueChanged;
            if (mGraySlider != null)
               mGraySlider.ValueChanged += GraySlider_ValueChanged;
            if (mRedUpDown != null)
               mRedUpDown.ValueChanged += RedUpDown_ValueChanged;
            if (mRedSlider != null)
               mRedSlider.ValueChanged += RedSlider_ValueChanged;
            if (mGreenUpDown != null)
               mGreenUpDown.ValueChanged += GreenUpDown_ValueChanged;
            if (mGreenSlider != null)
               mGreenSlider.ValueChanged += GreenSlider_ValueChanged;
            if (mBlueUpDown != null)
               mBlueUpDown.ValueChanged += BlueUpDown_ValueChanged;
            if (mBlueSlider != null)
               mBlueSlider.ValueChanged += BlueSlider_ValueChanged;
            if (mHelpButton != null)
               mHelpButton.Click += MainForm.Help_Click;
            if (mOkButton != null)
               mOkButton.Click += OkButton_Click;
            if (mCancelButton != null)
               mCancelButton.Click += CancelButton_Click;
         }

         private void RemoveEventHandlers() {
            if (mUseNamedCheckBox != null)
               mUseNamedCheckBox.CheckedChanged -= UseNamedCheckBox_CheckedChanged;
            if (mNamedColorPrefixButton != null)
               mNamedColorPrefixButton.Click -= NamedColorPrefixButton_Click;
            if (mNamedColorsComboBox != null) {
               mNamedColorsComboBox.SelectedValueChanged -= NamedColorsComboBox_SelectedValueChanged;
               mNamedColorsComboBox.KeyUp -= NamedColorsComboBox_KeyUp;
               mNamedColorsComboBox.Leave -= NamedColorsComboBox_Leave;
            }
            if (mUseGrayscaleCheckBox != null)
               mUseGrayscaleCheckBox.CheckedChanged -= UseGrayscaleCheckBox_CheckedChanged;
            if (mGrayUpDown != null)
               mGrayUpDown.ValueChanged -= GrayUpDown_ValueChanged;
            if (mGraySlider != null)
               mGraySlider.ValueChanged -= GraySlider_ValueChanged;
            if (mRedUpDown != null)
               mRedUpDown.ValueChanged -= RedUpDown_ValueChanged;
            if (mRedSlider != null)
               mRedSlider.ValueChanged -= RedSlider_ValueChanged;
            if (mGreenUpDown != null)
               mGreenUpDown.ValueChanged -= GreenUpDown_ValueChanged;
            if (mGreenSlider != null)
               mGreenSlider.ValueChanged -= GreenSlider_ValueChanged;
            if (mBlueUpDown != null)
               mBlueUpDown.ValueChanged -= BlueUpDown_ValueChanged;
            if (mBlueSlider != null)
               mBlueSlider.ValueChanged -= BlueSlider_ValueChanged;
            if (mHelpButton != null)
               mHelpButton.Click -= MainForm.Help_Click;
            if (mOkButton != null)
               mOkButton.Click -= OkButton_Click;
            if (mCancelButton != null)
               mCancelButton.Click -= CancelButton_Click;
         }

         private void SetColorValues(Color pColor) {
            int gray = (pColor.R + pColor.G + pColor.B) / 3;

            RemoveEventHandlers();
            if (mGrayUpDown != null)
               mGrayUpDown.Value = gray;
            if (mGraySlider != null)
               mGraySlider.Value = gray;
            if (mRedUpDown != null)
               mRedUpDown.Value = pColor.R;
            if (mGreenUpDown != null)
               mGreenUpDown.Value = pColor.G;
            if (mBlueUpDown != null)
               mBlueUpDown.Value = pColor.B;
            if (mRedSlider != null)
               mRedSlider.Value = pColor.R;
            if (mGreenSlider != null)
               mGreenSlider.Value = pColor.G;
            if (mBlueSlider != null)
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
            if (mRedUpDown == null || mGreenUpDown == null || mBlueUpDown == null)
               return;
            int r = (int)mRedUpDown.Value;
            int g = (int)mGreenUpDown.Value;
            int b = (int)mBlueUpDown.Value;
            int gray = (int)(mGrayUpDown?.Value ?? 128);

            if (mRedSwatch != null)
               mRedSwatch.BackColor = Color.FromArgb(r, 0, 0);
            if (mGreenSwatch != null)
               mGreenSwatch.BackColor = Color.FromArgb(0, g, 0);
            if (mBlueSwatch != null)
               mBlueSwatch.BackColor = Color.FromArgb(0, 0, b);
            if (mGraySwatch != null)
               mGraySwatch.BackColor = Color.FromArgb(gray, gray, gray);
            if (mUseGrayscaleCheckBox?.Checked == true) {
               if (mDemoSwatch != null)
                  mDemoSwatch.BackColor = Color.FromArgb(gray, gray, gray);
            }
            else {
               if (mDemoSwatch != null)
                  mDemoSwatch.BackColor = Color.FromArgb(r, g, b);
            }
         }

         private void DisableCustomControls() {
            if (mUseGrayscaleCheckBox != null)
               mUseGrayscaleCheckBox.Enabled = false;
            if (mGrayPrefixButton != null)
               mGrayPrefixButton.Enabled = false;
            if (mGrayUpDown != null)
               mGrayUpDown.Enabled = false;
            if (mGraySlider != null)
               mGraySlider.Enabled = false;
            if (mRedPrefixButton != null)
               mRedPrefixButton.Enabled = false;
            if (mRedUpDown != null)
               mRedUpDown.Enabled = false;
            if (mRedSlider != null)
               mRedSlider.Enabled = false;
            if (mGreenPrefixButton != null)
               mGreenPrefixButton.Enabled = false;
            if (mGreenUpDown != null)
               mGreenUpDown.Enabled = false;
            if (mGreenSlider != null)
               mGreenSlider.Enabled = false;
            if (mBluePrefixButton != null)
               mBluePrefixButton.Enabled = false;
            if (mBlueUpDown != null)
               mBlueUpDown.Enabled = false;
            if (mBlueSlider != null)
               mBlueSlider.Enabled = false;
         }

         private void EnableCustomControls() {
            bool isGray = mUseGrayscaleCheckBox?.Checked ?? false;

            if (mUseGrayscaleCheckBox != null)
               mUseGrayscaleCheckBox.Enabled = true;
            if (mGrayPrefixButton != null)
               mGrayPrefixButton.Enabled = isGray;
            if (mGrayUpDown != null)
               mGrayUpDown.Enabled = isGray;
            if (mGraySlider != null)
               mGraySlider.Enabled = isGray;
            if (mRedPrefixButton != null)
               mRedPrefixButton.Enabled = !isGray;
            if (mRedUpDown != null)
               mRedUpDown.Enabled = !isGray;
            if (mRedSlider != null)
               mRedSlider.Enabled = !isGray;
            if (mGreenPrefixButton != null)
               mGreenPrefixButton.Enabled = !isGray;
            if (mGreenUpDown != null)
               mGreenUpDown.Enabled = !isGray;
            if (mGreenSlider != null)
               mGreenSlider.Enabled = !isGray;
            if (mBluePrefixButton != null)
               mBluePrefixButton.Enabled = !isGray;
            if (mBlueUpDown != null)
               mBlueUpDown.Enabled = !isGray;
            if (mBlueSlider != null)
               mBlueSlider.Enabled = !isGray;
         }

         private void SetFontsAndColors() {
            Color backColor = mTheme!.mInterfaceColors[(int)ColorUsage.InterfaceBackground],
               foreColor = mTheme.mInterfaceColors[(int)ColorUsage.InterfaceFont],
               groupBoxBackgroundColor = mTheme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground],
               statusBackground = mTheme.mInterfaceColors[(int)ColorUsage.StatusBackground],
               statusForeColor = mTheme.mInterfaceColors[(int)ColorUsage.StatusFont];
            Font interfaceFont = mTheme.mFonts[(int)FontUsage.Interface],
               statusFont = mTheme.mFonts[(int)FontUsage.Status];

            this.BackColor = backColor;
            mScrollPanel!.BackColor = backColor;
            mBluePrefixButton!.Font = interfaceFont;
            mBluePrefixButton.ForeColor = foreColor;
            mBluePrefixButton.BackColor = backColor;
            FlattenButton(mBluePrefixButton, groupBoxBackgroundColor);
            mCancelButton!.Font = statusFont;
            mCancelButton.ForeColor = statusForeColor;
            mCancelButton.BackColor = statusBackground;
            mGrayPrefixButton!.Font = interfaceFont;
            mGrayPrefixButton.ForeColor = foreColor;
            mGrayPrefixButton.BackColor = backColor;
            FlattenButton(mGrayPrefixButton, groupBoxBackgroundColor);
            mGreenPrefixButton!.Font = interfaceFont;
            mGreenPrefixButton.ForeColor = foreColor;
            mGreenPrefixButton.BackColor = backColor;
            FlattenButton(mGreenPrefixButton, groupBoxBackgroundColor);
            mHelpButton!.Font = statusFont;
            mHelpButton.ForeColor = statusForeColor;
            mHelpButton.BackColor = statusBackground;
            mNamedColorPrefixButton!.Font = interfaceFont;
            mNamedColorPrefixButton.ForeColor = foreColor;
            mNamedColorPrefixButton.BackColor = backColor;
            FlattenButton(mNamedColorPrefixButton, groupBoxBackgroundColor);
            mOkButton!.Font = statusFont;
            mOkButton.ForeColor = statusForeColor;
            mOkButton.BackColor = statusBackground;
            mRedPrefixButton!.Font = interfaceFont;
            mRedPrefixButton.ForeColor = foreColor;
            mRedPrefixButton.BackColor = backColor;
            FlattenButton(mRedPrefixButton, groupBoxBackgroundColor);
            mUseGrayscaleCheckBox!.Font = interfaceFont;
            mUseGrayscaleCheckBox.ForeColor = foreColor;
            mUseGrayscaleCheckBox.BackColor = Color.Transparent;
            mUseNamedCheckBox!.Font = interfaceFont;
            mUseNamedCheckBox.ForeColor = foreColor;
            mUseNamedCheckBox.BackColor = Color.Transparent;
            mNamedColorsComboBox!.Font = interfaceFont;
            mNamedColorsComboBox.ForeColor = foreColor;
            mNamedColorsComboBox.BackColor = backColor;
            mCustomColorGroupBox!.Font = CreateNewBoldFont(interfaceFont);
            mCustomColorGroupBox.ForeColor = foreColor;
            mCustomColorGroupBox.BackColor = groupBoxBackgroundColor;
            mNamedColorsGroupBox!.Font = CreateNewBoldFont(interfaceFont);
            mNamedColorsGroupBox.ForeColor = foreColor;
            mNamedColorsGroupBox.BackColor = groupBoxBackgroundColor;
            mBlueUpDown!.Font = interfaceFont;
            mBlueUpDown.ForeColor = foreColor;
            mBlueUpDown.BackColor = backColor;
            mGrayUpDown!.Font = interfaceFont;
            mGrayUpDown.ForeColor = foreColor;
            mGrayUpDown.BackColor = backColor;
            mGreenUpDown!.Font = interfaceFont;
            mGreenUpDown.ForeColor = foreColor;
            mGreenUpDown.BackColor = backColor;
            mRedUpDown!.Font = interfaceFont;
            mRedUpDown.ForeColor = foreColor;
            mRedUpDown.BackColor = backColor;
            mStatusStrip!.BackColor = statusBackground;
            mStatusStrip.ForeColor = statusForeColor;
            mStatusStrip.Font = statusFont;
            mTitleLabel!.SetFontAndColor(mCurrentTheme!);
            mDemoSwatch!.SetFontAndColor(mCurrentTheme!);
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               if (mUseNamedCheckBox != null)
                  mUseNamedCheckBox.CheckedChanged -= UseNamedCheckBox_CheckedChanged;
               if (mNamedColorPrefixButton != null)
                  mNamedColorPrefixButton.Click -= NamedColorPrefixButton_Click;
               if (mNamedColorsComboBox != null) {
                  mNamedColorsComboBox.SelectedValueChanged -= NamedColorsComboBox_SelectedValueChanged;
                  mNamedColorsComboBox.KeyUp -= NamedColorsComboBox_KeyUp;
                  mNamedColorsComboBox.Leave -= NamedColorsComboBox_Leave;
               }
               if (mUseGrayscaleCheckBox != null)
                  mUseGrayscaleCheckBox.CheckedChanged -= UseGrayscaleCheckBox_CheckedChanged;
               if (mGrayUpDown != null)
                  mGrayUpDown.ValueChanged -= GrayUpDown_ValueChanged;
               if (mGraySlider != null)
                  mGraySlider.ValueChanged -= GraySlider_ValueChanged;
               if (mRedUpDown != null)
                  mRedUpDown.ValueChanged -= RedUpDown_ValueChanged;
               if (mRedSlider != null)
                  mRedSlider.ValueChanged -= RedSlider_ValueChanged;
               if (mGreenUpDown != null)
                  mGreenUpDown.ValueChanged -= GreenUpDown_ValueChanged;
               if (mGreenSlider != null)
                  mGreenSlider.ValueChanged -= GreenSlider_ValueChanged;
               if (mBlueUpDown != null)
                  mBlueUpDown.ValueChanged -= BlueUpDown_ValueChanged;
               if (mBlueSlider != null)
                  mBlueSlider.ValueChanged -= BlueSlider_ValueChanged;
               if (mHelpButton != null)
                  mHelpButton.Click -= MainForm.Help_Click;
               if (mOkButton != null)
                  mOkButton.Click -= OkButton_Click;
               if (mCancelButton != null)
                  mCancelButton.Click -= CancelButton_Click;
               mTitleLabel?.Dispose();
               mUseNamedCheckBox?.Dispose();
               mNamedColorPrefixButton?.Dispose();
               mNamedColorsComboBox?.Dispose();
               mNamedColorsGroupBox?.Dispose();
               mUseGrayscaleCheckBox?.Dispose();
               mGrayPrefixButton?.Dispose();
               mGrayUpDown?.Dispose();
               mGraySlider?.Dispose();
               mGraySwatch?.Dispose();
               mRedPrefixButton?.Dispose();
               mRedUpDown?.Dispose();
               mRedSlider?.Dispose();
               mRedSwatch?.Dispose();
               mGreenPrefixButton?.Dispose();
               mGreenUpDown?.Dispose();
               mGreenSlider?.Dispose();
               mGreenSwatch?.Dispose();
               mBluePrefixButton?.Dispose();
               mBlueUpDown?.Dispose();
               mBlueSlider?.Dispose();
               mBlueSwatch?.Dispose();
               mDemoSwatch?.Dispose();
               mCustomColorGroupBox?.Dispose();
               mHelpButton?.Dispose();
               mOkButton?.Dispose();
               mCancelButton?.Dispose();
               mStatusStrip?.Dispose();
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
