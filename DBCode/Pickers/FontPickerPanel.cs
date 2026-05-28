namespace DBCode {
   namespace Themes {
      internal sealed partial class FontPickerPanel : Panel {
         internal BottomPanel mBottomPanel;
         private Button mOkButton;
         private ComboBox mFontFamilyComboBox, mFontSizeComboBox;
         private FlattenedButtonCluster mFamilyCluster, mFontDropDownCluster, mFontSizePrefixCluster, mFontSizeDropDownCluster;
         private Font? mInitialFont, mWorkingFont;
         private FontUsage mFontUsage;
         private GroupBox mFontStyleGroupBox;
         internal Label mFontDescriptionLabel;
         private List<Font> mOwnedFonts = [];
         private Panel mFontSizePanel, mPickFontPanel, mScrollPanel;
         internal Panel ScrollPanel { get { return mScrollPanel; } }
         private ScalableCheckBoxCluster mBoldStyleCluster, mItalicsStyleCluster, mNormalStyleCluster, mStrikethroughStyleCluster,
            mUnderlineStyleCluster;
         private TextBox mFontFamilyNameTextBox, mFontSizeTextBox;
         private Theme? mTheme;
         internal TwoLineHeaderLabelCluster mTitleLabel;

         public FontPickerPanel(Theme pTheme, FontUsage pFontUsage, Font pInitialFont) {
            ThrowIfNull(pTheme, nameof(pTheme));
            ThrowIfNull(pInitialFont, nameof(pInitialFont));
            mInitialFont = CreateNewFont(pInitialFont);
            mWorkingFont = CreateNewFont(pInitialFont);
            mFontUsage = pFontUsage;
            mTheme = pTheme;
            Color interfaceBackground = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground],
               interfaceFont = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               groupBoxBackground = mTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
            Font interfaceTextFont = mTheme.mFonts[(int)FontUsage.Interface];
            mTitleLabel = new TwoLineHeaderLabelCluster(mTheme, "Select A Font",
               $"Use this font for {ToDescription(mFontUsage)}");
            mScrollPanel = new Panel {
               Name = $"FontPickerScrollPanel{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               AutoScroll = true,
               BackColor = interfaceBackground
            };
            mPickFontPanel = new Panel {
               Name = $"PickFontPanel{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               BackColor = interfaceBackground
            };
            mFontFamilyNameTextBox = new TextBox {
               Name = $"FontFamilyNameTextBox{mTabIndex++}",
               Font = CreateNewFont(interfaceTextFont)
            };
            mFamilyCluster = new FlattenedButtonCluster(mTheme, "Famil&y", mFontFamilyNameTextBox, interfaceBackground);
            mFontFamilyComboBox = new ComboBox {
               Name = $"FontFamilyComboBox{mTabIndex++}",
               DropDownStyle = ComboBoxStyle.DropDownList,
               DrawMode = DrawMode.OwnerDrawFixed,
               Font = CreateNewFont(interfaceTextFont)
            };
            List<string> fontNames = [];
            foreach (FontFamily fontFamily in FontFamily.Families.ToList())
               fontNames.Add(fontFamily.Name);
            mFontFamilyComboBox.Items.AddRange(fontNames.ToArray());
            mFontFamilyComboBox.DrawItem += FontFamilyComboBox_DrawItem;
            mFontDropDownCluster = new FlattenedButtonCluster(mTheme, "&Font", mFontFamilyComboBox, interfaceBackground);
            mPickFontPanel.Controls.AddRange([mFamilyCluster, mFontDropDownCluster]);
            mFontStyleGroupBox = new GroupBox {
               Name = $"FontStyleGroupBox{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               Text = "Style",
               Font = CreateNewBoldFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = groupBoxBackground
            };
            mNormalStyleCluster = new ScalableCheckBoxCluster(mTheme, "&Normal", false, groupBoxBackground);
            mBoldStyleCluster = new ScalableCheckBoxCluster(mTheme, "&Bold", false, groupBoxBackground);
            mItalicsStyleCluster = new ScalableCheckBoxCluster(mTheme, "&Italics", false, groupBoxBackground);
            mUnderlineStyleCluster = new ScalableCheckBoxCluster(mTheme, "&Underline", false, groupBoxBackground);
            mStrikethroughStyleCluster = new ScalableCheckBoxCluster(mTheme, "&Strikethrough", false, groupBoxBackground);
            mFontStyleGroupBox.Controls.AddRange([mNormalStyleCluster, mBoldStyleCluster,
               mItalicsStyleCluster, mUnderlineStyleCluster, mStrikethroughStyleCluster]);
            mFontSizePanel = new Panel {
               Name = $"FontSizePanel{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               BackColor = groupBoxBackground
            };
            mFontSizeTextBox = new TextBox {
               Name = $"FontSizeTextBox{mTabIndex++}",
               Width = 60,
               Font = CreateNewFont(interfaceTextFont)
            };
            mFontSizePrefixCluster = new FlattenedButtonCluster(mTheme, "Font Si&ze", mFontSizeTextBox, groupBoxBackground);
            mFontSizeComboBox = new ComboBox {
               Name = $"FontSizeComboBox{mTabIndex++}",
               DropDownStyle = ComboBoxStyle.DropDownList,
               DrawMode = DrawMode.OwnerDrawFixed,
               Width = 60,
               Font = CreateNewFont(interfaceTextFont)
            };
            mFontSizeComboBox.DrawItem += FontSizeComboBox_DrawItem;
            int[] fontSizes = [6, 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72];
            foreach (int size in fontSizes)
               mFontSizeComboBox.Items.Add(size.ToString());
            mFontSizeDropDownCluster = new FlattenedButtonCluster(mTheme, "Font Size &Drop-down", mFontSizeComboBox, groupBoxBackground);
            mFontSizePanel.Controls.AddRange([mFontSizePrefixCluster, mFontSizeDropDownCluster]);
            mFontDescriptionLabel = new Label {
               Name = $"FontDescriptionLabel{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               Text = "Selected font: ",
               AutoSize = true,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = Color.Transparent
            };
            mBottomPanel = new BottomPanel(mTheme, "&Cancel") {
               Name = $"FontPickerBottomPanel{mTabIndex++}"
            };
            ThrowIfNull(mBottomPanel.mHelpButton, nameof(mBottomPanel.mHelpButton));
            ThrowIfNull(mBottomPanel.mCancelButton, nameof(mBottomPanel.mCancelButton));
            mBottomPanel.mHelpButton.Tag = new HelpTag(HelpContext.FontPicker);
            mBottomPanel.mHelpButton.Click += MainForm.Help_Click;
            mOkButton = new Button {
               Name = $"FontPickerOkButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&OK",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mBottomPanel.AddRightControl(mOkButton);
            mBottomPanel.mCancelButton.Click += CancelButton_Click;
            mOkButton.Click += OkButton_Click;
            mScrollPanel.Controls.AddRange([mPickFontPanel, mFontStyleGroupBox, mFontSizePanel,
               mFontDescriptionLabel]);
            Controls.AddRange([mScrollPanel, mBottomPanel, mTitleLabel]);
            BackColor = interfaceBackground;
            AttachEventHandlers();
         }

         public void LayoutControls() {
            ThrowIfNull(mTheme, nameof(mTheme));
            ThrowIfNull(mInitialFont, nameof(mInitialFont));
            ThrowIfNull(mForm, nameof(mForm));
            RemoveEventHandlers();
            SetFontsAndColors();
            mTitleLabel.LayoutCluster();
            mFontFamilyNameTextBox.Text = mInitialFont.Name;
            int familyIndex = 0;
            for (int i = 0; i < mFontFamilyComboBox.Items.Count; i++) {
               if (string.Equals((string)mFontFamilyComboBox.Items[i]!, mInitialFont.Name,
                  StringComparison.OrdinalIgnoreCase)) {
                  familyIndex = i;
                  break;
               }
            }
            mFontFamilyComboBox.SelectedIndex = familyIndex;
            int fontSize = (int)Math.Ceiling(mInitialFont.SizeInPoints);
            mFontSizeTextBox.Text = fontSize.ToString();
            bool foundSize = false;
            for (int i = 0; i < mFontSizeComboBox.Items.Count; i++) {
               if (string.Equals((string)mFontSizeComboBox.Items[i]!, fontSize.ToString(),
                  StringComparison.Ordinal)) {
                  mFontSizeComboBox.SelectedIndex = i;
                  foundSize = true;
                  break;
               }
            }
            if (!foundSize)
               mFontSizeComboBox.SelectedIndex = 5;
            FontStyle style = mInitialFont.Style;
            if ((style & FontStyle.Regular) == FontStyle.Regular) {
               mNormalStyleCluster.mScalableCheckBox.Checked = true;
               mBoldStyleCluster.mScalableCheckBox.Checked = false;
               mItalicsStyleCluster.mScalableCheckBox.Checked = false;
               mUnderlineStyleCluster.mScalableCheckBox.Checked = false;
               mStrikethroughStyleCluster.mScalableCheckBox.Checked = false;
            }
            else {
               mNormalStyleCluster.mScalableCheckBox.Checked = false;
               if ((style & FontStyle.Bold) == FontStyle.Bold)
                  mBoldStyleCluster.mScalableCheckBox.Checked = true;
               if ((style & FontStyle.Italic) == FontStyle.Italic)
                  mItalicsStyleCluster.mScalableCheckBox.Checked = true;
               if ((style & FontStyle.Underline) == FontStyle.Underline)
                  mUnderlineStyleCluster.mScalableCheckBox.Checked = true;
               if ((style & FontStyle.Strikeout) == FontStyle.Strikeout)
                  mStrikethroughStyleCluster.mScalableCheckBox.Checked = true;
            }
            int textBoxWidth = 200;
            Size measuredSize = TextRenderer.MeasureText(mInitialFont.Name, mFontFamilyNameTextBox.Font);
            textBoxWidth = Math.Max(textBoxWidth, measuredSize.Width + 10);
            mFontFamilyNameTextBox.Width = textBoxWidth;
            mFamilyCluster.LayoutControlsOnly();
            mFontFamilyComboBox.Width = textBoxWidth;
            mFontDropDownCluster.Location = new Point(0, mFamilyCluster.Bottom + mEm);
            mFontDropDownCluster.LayoutControlsOnly();
            mPickFontPanel.Location = new Point(mIndent, mEm);
            mFontStyleGroupBox.Left = mPickFontPanel.Right + mEm;
            mFontStyleGroupBox.Top = mPickFontPanel.Top;
            mNormalStyleCluster.Location = new Point(mIndent, mEm2);
            mNormalStyleCluster.LayoutCluster();
            mBoldStyleCluster.Location = new Point(mIndent, mNormalStyleCluster.Bottom);
            mBoldStyleCluster.LayoutCluster();
            mItalicsStyleCluster.Location = new Point(mIndent, mBoldStyleCluster.Bottom);
            mItalicsStyleCluster.LayoutCluster();
            mUnderlineStyleCluster.Location = new Point(mIndent, mItalicsStyleCluster.Bottom);
            mUnderlineStyleCluster.LayoutCluster();
            mStrikethroughStyleCluster.Location = new Point(mIndent, mUnderlineStyleCluster.Bottom);
            mStrikethroughStyleCluster.LayoutCluster();
            SizeGroupBox(mFontStyleGroupBox);
            mFontSizePanel.Left = mFontStyleGroupBox.Right + mEm;
            mFontSizePanel.Top = mFontStyleGroupBox.Top;
            mFontSizePrefixCluster.Location = new Point(mIndent, 0);
            mFontSizeTextBox.Width = 60;
            mFontSizePrefixCluster.LayoutControlsOnly();
            mFontSizeDropDownCluster.Location = new Point(mIndent, mFontSizePrefixCluster.Bottom + mEm);
            mFontSizeComboBox.Width = 60;
            mFontSizeDropDownCluster.LayoutControlsOnly();
            mFontDescriptionLabel.Left = mEmHalf;
            mFontDescriptionLabel.Top = Math.Max(mFontStyleGroupBox.Bottom, mFontSizePanel.Bottom) + mEm;
            UpdateFontDescription();
            SizePanel(mScrollPanel);
            mScrollPanel.Location = new Point(1, mTitleLabel.Bottom);
            mBottomPanel.Top = mScrollPanel.Bottom;
            mBottomPanel.LayoutControls();
            AttachEventHandlers();
            mForm.BeginInvoke(() => {
               mForm.SuspendClientSizeChanged();
               if (mUiState.mFontPickerFirstShow) {
                  Size wantedSize = GetRequiredSize();
                  Screen screen = Screen.FromPoint(mUiState.FormBounds.Location);
                  int width = Math.Min(wantedSize.Width, (int)(screen.WorkingArea.Width * 0.9));
                  int height = Math.Min(wantedSize.Height, (int)(screen.WorkingArea.Height * 0.9));
                  mForm.ClientSize = new Size(width, height);
                  CenterFormOnMonitor(mForm, screen);
                  mUiState.mFontPickerFirstShow = false;
               }
               else {
                  mForm.Bounds = mUiState.mFontPickerBounds;
                  EnsureWindowFitsMonitor(mForm);
               }
               mUiState.mFontPickerBounds = mForm.Bounds;
               mForm.Opacity = mUiState.mFormOpacity;
               mForm.ResumeClientSizeChanged();
               mBottomPanel.LayoutControls();
               mTitleLabel.LayoutCluster();
               mScrollPanel.Anchor = mAnchorTopLeftBottomRight;
            });
         }

         internal Size GetRequiredSize() {
            return new Size(mFontSizePanel.Right + mEm + SystemInformation.VerticalScrollBarWidth,
               mTitleLabel.Height + mScrollPanel.Height + mBottomPanel.Height +
               SystemInformation.HorizontalScrollBarHeight);
         }

         private void AttachEventHandlers() {
            mFontFamilyNameTextBox.TextChanged += FontFamilyNameTextBox_TextChanged;
            mFontFamilyNameTextBox.Leave += FontFamilyNameTextBox_Leave;
            mFontFamilyComboBox.SelectedIndexChanged += FontFamilyComboBox_SelectedIndexChanged;
            mFontSizeTextBox.TextChanged += FontSizeTextBox_TextChanged;
            mFontSizeTextBox.Leave += FontSizeTextBox_Leave;
            mFontSizeComboBox.SelectedIndexChanged += FontSizeComboBox_SelectedIndexChanged;
            mNormalStyleCluster.mScalableCheckBox.Click += NormalStyleCheckBox_Click;
            mBoldStyleCluster.mScalableCheckBox.Click += BoldStyleCheckBox_Click;
            mItalicsStyleCluster.mScalableCheckBox.Click += ItalicsStyleCheckBox_Click;
            mUnderlineStyleCluster.mScalableCheckBox.Click += UnderlineStyleCheckBox_Click;
            mStrikethroughStyleCluster.mScalableCheckBox.Click += StrikethroughStyleCheckBox_Click;
         }

         private void RemoveEventHandlers() {
            mFontFamilyNameTextBox.TextChanged -= FontFamilyNameTextBox_TextChanged;
            mFontFamilyNameTextBox.Leave -= FontFamilyNameTextBox_Leave;
            mFontFamilyComboBox.SelectedIndexChanged -= FontFamilyComboBox_SelectedIndexChanged;
            mFontSizeTextBox.TextChanged -= FontSizeTextBox_TextChanged;
            mFontSizeTextBox.Leave -= FontSizeTextBox_Leave;
            mFontSizeComboBox.SelectedIndexChanged -= FontSizeComboBox_SelectedIndexChanged;
            mNormalStyleCluster.mScalableCheckBox.Click -= NormalStyleCheckBox_Click;
            mBoldStyleCluster.mScalableCheckBox.Click -= BoldStyleCheckBox_Click;
            mItalicsStyleCluster.mScalableCheckBox.Click -= ItalicsStyleCheckBox_Click;
            mUnderlineStyleCluster.mScalableCheckBox.Click -= UnderlineStyleCheckBox_Click;
            mStrikethroughStyleCluster.mScalableCheckBox.Click -= StrikethroughStyleCheckBox_Click;
         }

         private FontStyle GetFontStyle() {
            FontStyle style = FontStyle.Regular;
            if (mNormalStyleCluster.mScalableCheckBox.Checked)
               return style;
            if (mBoldStyleCluster.mScalableCheckBox.Checked)
               style |= FontStyle.Bold;
            if (mItalicsStyleCluster.mScalableCheckBox.Checked)
               style |= FontStyle.Italic;
            if (mUnderlineStyleCluster.mScalableCheckBox.Checked)
               style |= FontStyle.Underline;
            if (mStrikethroughStyleCluster.mScalableCheckBox.Checked)
               style |= FontStyle.Strikeout;
            return style;
         }

         private void MaybeRegularStyle() {
            if (!mBoldStyleCluster.mScalableCheckBox.Checked && !mItalicsStyleCluster.mScalableCheckBox.Checked &&
                !mUnderlineStyleCluster.mScalableCheckBox.Checked && !mStrikethroughStyleCluster.mScalableCheckBox.Checked)
               mNormalStyleCluster.mScalableCheckBox.Checked = true;
         }

         private void UpdateFontDescription() {
            string familyName = mFontFamilyNameTextBox.Text;
            if (!int.TryParse(mFontSizeTextBox.Text, out int fontSize))
               fontSize = 12;
            FontStyle style = GetFontStyle();
            string styleText = style.ToString();
            try {
               Font oldDesc = mFontDescriptionLabel.Font;
               mFontDescriptionLabel.Font = new Font(familyName, fontSize, style);
               MainForm.DisposeFontIfOwned(oldDesc);
               mFontDescriptionLabel.Text = $"Selected font: {familyName}; {fontSize}pt; {styleText}";
            }
            catch {
               mFontDescriptionLabel.Text = $"Selected font: {familyName}; {fontSize}pt; {styleText} (unavailable)";
            }
            mFontDescriptionLabel.Refresh();
         }

         private void SetFontsAndColors() {
            ThrowIfNull(mTheme, nameof(mTheme));
            Color backColor = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground],
               foreColor = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               groupBoxBackgroundColor = mTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
            Font interfaceFont = mTheme.mFonts[(int)FontUsage.Interface];
            List<Font> previousFonts = mOwnedFonts;
            mOwnedFonts = [];
            BackColor = backColor;
            mScrollPanel.BackColor = backColor;
            mPickFontPanel.BackColor = backColor;
            mFontSizePanel.BackColor = backColor;
            mFamilyCluster.SetFontAndColor();
            mFontDropDownCluster.SetFontAndColor();
            mFontSizePrefixCluster.SetFontAndColor();
            mFontSizeDropDownCluster.SetFontAndColor();
            mNormalStyleCluster.SetFontAndColor();
            mBoldStyleCluster.SetFontAndColor();
            mItalicsStyleCluster.SetFontAndColor();
            mUnderlineStyleCluster.SetFontAndColor();
            mStrikethroughStyleCluster.SetFontAndColor();
            Font workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mFontFamilyComboBox.Font = workingFont;
            mFontFamilyComboBox.ForeColor = foreColor;
            mFontFamilyComboBox.BackColor = backColor;
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mFontSizeComboBox.Font = workingFont;
            mFontSizeComboBox.ForeColor = foreColor;
            mFontSizeComboBox.BackColor = backColor;
            workingFont = CreateNewBoldFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mFontStyleGroupBox.Font = workingFont;
            mFontStyleGroupBox.ForeColor = foreColor;
            mFontStyleGroupBox.BackColor = groupBoxBackgroundColor;
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mFontFamilyNameTextBox.Font = workingFont;
            mFontFamilyNameTextBox.ForeColor = foreColor;
            mFontFamilyNameTextBox.BackColor = backColor;
            workingFont = CreateNewFont(interfaceFont);
            mOwnedFonts.Add(workingFont);
            mFontSizeTextBox.Font = workingFont;
            mFontSizeTextBox.ForeColor = foreColor;
            mFontSizeTextBox.BackColor = backColor;
            Font oldDescLabel = mFontDescriptionLabel.Font;
            mFontDescriptionLabel.Font = CreateNewFont(interfaceFont);
            MainForm.DisposeFontIfOwned(oldDescLabel);
            mFontDescriptionLabel.ForeColor = foreColor;
            mFontDescriptionLabel.BackColor = Color.Transparent;
            mBottomPanel.SetFontAndColor();
            mTitleLabel.SetFontAndColor();
            foreach (Font font in previousFonts)
               MainForm.DisposeFontIfOwned(font);
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               RemoveEventHandlers();
               mFontFamilyComboBox.DrawItem -= FontFamilyComboBox_DrawItem;
               mFontSizeComboBox.DrawItem -= FontSizeComboBox_DrawItem;
               mOkButton.Click -= OkButton_Click;
               ThrowIfNull(mBottomPanel.mHelpButton, nameof(mBottomPanel.mHelpButton));
               ThrowIfNull(mBottomPanel.mCancelButton, nameof(mBottomPanel.mCancelButton));
               mBottomPanel.mHelpButton.Click -= MainForm.Help_Click;
               mBottomPanel.mCancelButton.Click -= CancelButton_Click;
               mInitialFont?.Dispose();
               mWorkingFont?.Dispose();
               foreach (Font font in mOwnedFonts)
                  MainForm.DisposeFontIfOwned(font);
               mOwnedFonts.Clear();
               MainForm.DisposeFontIfOwned(mFontDescriptionLabel.Font);
               mTitleLabel.Dispose();
               mBottomPanel.Dispose();
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
