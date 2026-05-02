namespace DBCode {
   namespace Themes {
      public sealed partial class FontPickerPanel : Panel {
         private static Font? mInitialFont, mWorkingFont;
         private static FontUsage mFontUsage;
         private static Theme? mTheme;
         private Button mFamilyPrefixButton, mFontDropDownPrefixButton, mFontSizeDropDownPrefixButton,
            mFontSizePrefixButton, mOkButton;
         private CheckBox mBoldStyleCheckBox, mItalicsStyleCheckBox, mNormalStyleCheckBox, mStrikethroughStyleCheckBox,
            mUnderlineStyleCheckBox;
         private ComboBox mFontFamilyComboBox, mFontSizeComboBox;
         private GroupBox mFontStyleGroupBox;
         private Label mFontDescriptionLabel;
         private Panel mFontSizePanel, mPickFontPanel, mScrollPanel;
         internal BottomPanel mFontPickerBottomPanel;
         private TextBox mFontFamilyNameTextBox, mFontSizeTextBox;
         private TwoLineHeaderLabelCluster mTitleLabel;

         public FontPickerPanel(Theme pTheme, FontUsage pFontUsage, Font pInitialFont) {
            mInitialFont = pInitialFont;
            mFontUsage = pFontUsage;
            mTheme = pTheme;
            ThrowIfNull(mTheme, nameof(mTheme));
            Color interfaceBackground = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground],
               interfaceFont = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               groupBoxBackground = mTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
            Font interfaceTextFont = mTheme.mFonts[(int)FontUsage.Interface];
            mTitleLabel = new TwoLineHeaderLabelCluster(mTheme, "Select A Font",
               $"Use this font for {ToDescription((FontUsage)mFontUsage)}");
            mScrollPanel = new Panel {
               Name = $"FontPickerScrollPanel{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               AutoScroll = true,
               BackColor = interfaceBackground,
               Anchor = mAnchorTopLeftBottomRight
            };
            mPickFontPanel = new Panel {
               Name = $"PickFontPanel{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               BackColor = interfaceBackground
            };
            mFamilyPrefixButton = new Button {
               Name = $"FamilyPrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "Famil&y:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont
            };
            mFontFamilyNameTextBox = new TextBox {
               Name = $"FontFamilyNameTextBox{mTabIndex}",
               TabIndex = mTabIndex++,
               Font = CreateNewFont(interfaceTextFont)
            };
            mFontDropDownPrefixButton = new Button {
               Name = $"FontDropDownPrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Font:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont
            };
            mFontFamilyComboBox = new ComboBox {
               Name = $"FontFamilyComboBox{mTabIndex}",
               TabIndex = mTabIndex++,
               DropDownStyle = ComboBoxStyle.DropDownList,
               DrawMode = DrawMode.OwnerDrawFixed,
               Font = CreateNewFont(interfaceTextFont)
            };
            List<string> fontNames = [];
            foreach (FontFamily fontFamily in FontFamily.Families.ToList())
               fontNames.Add(fontFamily.Name);
            mFontFamilyComboBox.Items.AddRange(fontNames.ToArray());
            FlattenButton(mFamilyPrefixButton, interfaceBackground);
            FlattenButton(mFontDropDownPrefixButton, interfaceBackground);
            mPickFontPanel.Controls.AddRange([mFamilyPrefixButton, mFontFamilyNameTextBox, mFontDropDownPrefixButton, mFontFamilyComboBox]);
            mFontStyleGroupBox = new GroupBox {
               Name = $"FontStyleGroupBox{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               Text = "Style",
               Font = CreateNewBoldFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = groupBoxBackground
            };
            mNormalStyleCheckBox = new CheckBox {
               Name = $"NormalStyleCheckBox{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Normal",
               AutoSize = true,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = Color.Transparent
            };
            mBoldStyleCheckBox = new CheckBox {
               Name = $"BoldStyleCheckBox{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Bold",
               AutoSize = true,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = Color.Transparent
            };
            mItalicsStyleCheckBox = new CheckBox {
               Name = $"ItalicsStyleCheckBox{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Italics",
               AutoSize = true,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = Color.Transparent
            };
            mUnderlineStyleCheckBox = new CheckBox {
               Name = $"UnderlineStyleCheckBox{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Underline",
               AutoSize = true,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = Color.Transparent
            };
            mStrikethroughStyleCheckBox = new CheckBox {
               Name = $"StrikethroughCheckBox{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Strikethrough",
               AutoSize = true,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = Color.Transparent
            };
            mFontStyleGroupBox.Controls.AddRange([mNormalStyleCheckBox, mBoldStyleCheckBox, mItalicsStyleCheckBox,
               mUnderlineStyleCheckBox, mStrikethroughStyleCheckBox]);
            mFontSizePanel = new Panel {
               Name = $"FontSizePanel{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               BackColor = groupBoxBackground
            };
            mFontSizePrefixButton = new Button {
               Name = $"FontSizePrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "Font Si&ze:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont
            };
            mFontSizeTextBox = new TextBox {
               Name = $"FontSizeTextBox{mTabIndex}",
               TabIndex = mTabIndex++,
               Font = CreateNewFont(interfaceTextFont)
            };
            mFontSizeDropDownPrefixButton = new Button {
               Name = $"FontSizeDropDownPrefixButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "Font Size &Drop-down:",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont
            };
            mFontSizeComboBox = new ComboBox {
               Name = $"FontSizeComboBox{mTabIndex}",
               TabIndex = mTabIndex++,
               DropDownStyle = ComboBoxStyle.DropDownList,
               Font = CreateNewFont(interfaceTextFont)
            };
            int[] fontSizes = [6, 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72];
            foreach (int size in fontSizes)
               mFontSizeComboBox.Items.Add(size.ToString());
            FlattenButton(mFontSizePrefixButton, groupBoxBackground);
            FlattenButton(mFontSizeDropDownPrefixButton, groupBoxBackground);
            mFontSizePanel.Controls.AddRange([mFontSizePrefixButton, mFontSizeTextBox, mFontSizeDropDownPrefixButton,
               mFontSizeComboBox]);
            mFontDescriptionLabel = new Label {
               Name = $"FontDescriptionLabel{mTabIndex++}",
               TabIndex = TAB_INDEX_IGNORED,
               Text = "Selected font: ",
               AutoSize = true,
               Font = CreateNewFont(interfaceTextFont),
               ForeColor = interfaceFont,
               BackColor = Color.Transparent
            };
            mFontPickerBottomPanel = new BottomPanel(mTheme, "&Cancel") {
               Name = $"FontPickerBottomPanel{mTabIndex++}"
            };
            mFontPickerBottomPanel.mHelpButton.Tag = new HelpTag(HelpContext.FontPicker);
            mFontPickerBottomPanel.mHelpButton.Click += HelpButton_Click;
            mFontPickerBottomPanel.AddLeftControl(mFontDescriptionLabel);
            mOkButton = new Button {
               Name = $"FontPickerOkButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&OK",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mFontPickerBottomPanel.AddRightControl(mOkButton);
            mFontPickerBottomPanel.mCancelButton!.Click += CancelButton_Click;
            mOkButton.Click += OkButton_Click;
            Controls.AddRange([mScrollPanel, mFontPickerBottomPanel, mTitleLabel]);
            mScrollPanel.Controls.AddRange([mPickFontPanel, mFontStyleGroupBox, mFontSizePanel]);
            BackColor = interfaceBackground;
            AttachEventHandlers();
         }

         public void LayoutControls() {
            ThrowIfNull(mTheme, nameof(mTheme));
            ThrowIfNull(mInitialFont, nameof(mInitialFont));
            RemoveEventHandlers();
            SetFontsAndColors();
            UpdateFontDescription();
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
               mNormalStyleCheckBox.Checked = true;
               mBoldStyleCheckBox.Checked = false;
               mItalicsStyleCheckBox.Checked = false;
               mUnderlineStyleCheckBox.Checked = false;
               mStrikethroughStyleCheckBox.Checked = false;
            }
            else {
               mNormalStyleCheckBox.Checked = false;
               if ((style & FontStyle.Bold) == FontStyle.Bold)
                  mBoldStyleCheckBox.Checked = true;
               if ((style & FontStyle.Italic) == FontStyle.Italic)
                  mItalicsStyleCheckBox.Checked = true;
               if ((style & FontStyle.Underline) == FontStyle.Underline)
                  mUnderlineStyleCheckBox.Checked = true;
               if ((style & FontStyle.Strikeout) == FontStyle.Strikeout)
                  mStrikethroughStyleCheckBox.Checked = true;
            }
            mScrollPanel.Location = new Point(1, mTitleLabel.Bottom);
            mFamilyPrefixButton.Top = mEm;
            mFamilyPrefixButton.Left = mIndent;
            mFontFamilyNameTextBox.Left = mFamilyPrefixButton.Right + mEm;
            mFontFamilyNameTextBox.Top = mFamilyPrefixButton.Top + ((mFamilyPrefixButton.Height -
               mFontFamilyNameTextBox.Height) / 2);
            int textBoxWidth = 200;
            using (Graphics g = CreateGraphics()) {
               SizeF size = g.MeasureString(mInitialFont.Name, mFontFamilyNameTextBox.Font);
               textBoxWidth = Math.Max(textBoxWidth, (int)size.Width + 10);
            }
            mFontFamilyNameTextBox.Width = textBoxWidth;
            mFontDropDownPrefixButton.Top = mFamilyPrefixButton.Bottom + mEm;
            mFontDropDownPrefixButton.Left = mIndent;
            mFontFamilyComboBox.Left = mFontDropDownPrefixButton.Right + mEm;
            mFontFamilyComboBox.Top = mFontDropDownPrefixButton.Top + ((mFontDropDownPrefixButton.Height -
               mFontFamilyComboBox.Height) / 2);
            mFontFamilyComboBox.Width = textBoxWidth;
            mPickFontPanel.Width = mFontFamilyComboBox.Right + mEm;
            mPickFontPanel.Height = mFontFamilyComboBox.Bottom + mEm;
            mPickFontPanel.Top = mEm;
            mPickFontPanel.Left = mIndent;
            mFontStyleGroupBox.Left = mPickFontPanel.Right + mEm;
            mFontStyleGroupBox.Top = mPickFontPanel.Top;
            mNormalStyleCheckBox.Left = mIndent;
            mNormalStyleCheckBox.Top = mEm2;
            mBoldStyleCheckBox.Left = mIndent;
            mBoldStyleCheckBox.Top = mNormalStyleCheckBox.Bottom;
            mItalicsStyleCheckBox.Left = mIndent;
            mItalicsStyleCheckBox.Top = mBoldStyleCheckBox.Bottom;
            mUnderlineStyleCheckBox.Left = mIndent;
            mUnderlineStyleCheckBox.Top = mItalicsStyleCheckBox.Bottom;
            mStrikethroughStyleCheckBox.Left = mIndent;
            mStrikethroughStyleCheckBox.Top = mUnderlineStyleCheckBox.Bottom;
            int maxCheckBoxWidth = Math.Max(mNormalStyleCheckBox.Width,
               Math.Max(mBoldStyleCheckBox.Width,
               Math.Max(mItalicsStyleCheckBox.Width,
               Math.Max(mUnderlineStyleCheckBox.Width, mStrikethroughStyleCheckBox.Width))));
            mFontStyleGroupBox.Width = maxCheckBoxWidth + mEm2;
            mFontStyleGroupBox.Height = mStrikethroughStyleCheckBox.Bottom + mEm2;
            mFontSizePanel.Left = mFontStyleGroupBox.Right + mEm;
            mFontSizePanel.Top = mFontStyleGroupBox.Top;
            mFontSizePrefixButton.Top = 0;
            mFontSizePrefixButton.Left = mIndent;
            mFontSizeTextBox.Left = mIndent;
            mFontSizeTextBox.Top = mFontSizePrefixButton.Bottom;
            mFontSizeTextBox.Width = 60;
            mFontSizeDropDownPrefixButton.Left = mIndent;
            mFontSizeDropDownPrefixButton.Top = mFontSizeTextBox.Bottom + mEm2;
            mFontSizeComboBox.Left = mIndent;
            mFontSizeComboBox.Top = mFontSizeDropDownPrefixButton.Bottom;
            mFontSizeComboBox.Width = 60;
            int max = Math.Max(mFontSizeTextBox.Width,
               Math.Max(mFontSizeComboBox.Width, Math.Max(mFontSizePrefixButton.Width,
               mFontSizeDropDownPrefixButton.Width)));
            mFontSizePanel.Width = max + mEm2;
            mFontSizePanel.Height = mFontSizeComboBox.Bottom + mEm;
            mScrollPanel.Size = new Size(ClientSize.Width - 2,
               ClientSize.Height - mTitleLabel.Height - mFontPickerBottomPanel.Height);
            mFontPickerBottomPanel.Top = mScrollPanel.Bottom;
            mFontPickerBottomPanel.LayoutControls();
            RestoreFontPickerHandlers();
         }

         internal Size GetRequiredSize() {
            int contentWidth = 0, contentHeight = 0;
            contentWidth = mFontSizePanel.Right + (mEm * 2);
            contentHeight = Math.Max(mPickFontPanel.Bottom, Math.Max(mFontStyleGroupBox.Bottom,
               mFontSizePanel.Bottom)) + (mEm * 2);
            int totalWidth = contentWidth + SystemInformation.VerticalScrollBarWidth + (mEm * 2);
            int totalHeight = mTitleLabel.Height + contentHeight + mFontPickerBottomPanel.Height + (mEm * 2);
            return new Size(totalWidth, totalHeight);
         }

         private void AttachEventHandlers() {
            mFamilyPrefixButton.Click += FamilyPrefixButton_Click;
            mFontFamilyNameTextBox.TextChanged += FontFamilyNameTextBox_TextChanged;
            mFontFamilyNameTextBox.Leave += FontFamilyNameTextBox_Leave;
            mFontDropDownPrefixButton.Click += FontDropDownPrefixButton_Click;
            mFontFamilyComboBox.SelectedIndexChanged += FontFamilyComboBox_SelectedIndexChanged;
            mFontFamilyComboBox.DrawItem += FontFamilyComboBox_DrawItem;
            mNormalStyleCheckBox.Click += NormalStyleCheckBox_Click;
            mBoldStyleCheckBox.Click += BoldStyleCheckBox_Click;
            mItalicsStyleCheckBox.Click += ItalicsStyleCheckBox_Click;
            mUnderlineStyleCheckBox.Click += UnderlineStyleCheckBox_Click;
            mStrikethroughStyleCheckBox.Click += StrikethroughStyleCheckBox_Click;
            mFontSizePrefixButton.Click += FontSizePrefixButton_Click;
            mFontSizeTextBox.TextChanged += FontSizeTextBox_TextChanged;
            mFontSizeTextBox.Leave += FontSizeTextBox_Leave;
            mFontSizeDropDownPrefixButton.Click += FontSizeDropDownPrefixButton_Click;
            mFontSizeComboBox.SelectedIndexChanged += FontSizeComboBox_SelectedIndexChanged;
         }

         private void RemoveEventHandlers() {
            mFontFamilyNameTextBox.TextChanged -= FontFamilyNameTextBox_TextChanged;
            mFontFamilyComboBox.SelectedIndexChanged -= FontFamilyComboBox_SelectedIndexChanged;
            mFontSizeTextBox.TextChanged -= FontSizeTextBox_TextChanged;
            mFontSizeComboBox.SelectedIndexChanged -= FontSizeComboBox_SelectedIndexChanged;
            mNormalStyleCheckBox.Click -= NormalStyleCheckBox_Click;
            mBoldStyleCheckBox.Click -= BoldStyleCheckBox_Click;
            mItalicsStyleCheckBox.Click -= ItalicsStyleCheckBox_Click;
            mUnderlineStyleCheckBox.Click -= UnderlineStyleCheckBox_Click;
            mStrikethroughStyleCheckBox.Click -= StrikethroughStyleCheckBox_Click;
         }

         private void RestoreFontPickerHandlers() {
            mFontFamilyNameTextBox.TextChanged += FontFamilyNameTextBox_TextChanged;
            mFontFamilyComboBox.SelectedIndexChanged += FontFamilyComboBox_SelectedIndexChanged;
            mFontSizeTextBox.TextChanged += FontSizeTextBox_TextChanged;
            mFontSizeComboBox.SelectedIndexChanged += FontSizeComboBox_SelectedIndexChanged;
            mNormalStyleCheckBox.Click += NormalStyleCheckBox_Click;
            mBoldStyleCheckBox.Click += BoldStyleCheckBox_Click;
            mItalicsStyleCheckBox.Click += ItalicsStyleCheckBox_Click;
            mUnderlineStyleCheckBox.Click += UnderlineStyleCheckBox_Click;
            mStrikethroughStyleCheckBox.Click += StrikethroughStyleCheckBox_Click;
         }

         private FontStyle GetFontStyle() {
            FontStyle style = FontStyle.Regular;
            if (mNormalStyleCheckBox.Checked)
               return style;
            if (mBoldStyleCheckBox.Checked)
               style |= FontStyle.Bold;
            if (mItalicsStyleCheckBox.Checked)
               style |= FontStyle.Italic;
            if (mUnderlineStyleCheckBox.Checked)
               style |= FontStyle.Underline;
            if (mStrikethroughStyleCheckBox.Checked)
               style |= FontStyle.Strikeout;
            return style;
         }

         private void MaybeRegularStyle() {
            if (!mBoldStyleCheckBox.Checked && !mItalicsStyleCheckBox.Checked &&
                !mUnderlineStyleCheckBox.Checked && !mStrikethroughStyleCheckBox.Checked)
               mNormalStyleCheckBox.Checked = true;
         }

         private void UpdateFontDescription() {
            string familyName = mFontFamilyNameTextBox.Text;
            if (!int.TryParse(mFontSizeTextBox.Text, out int fontSize))
               fontSize = 12;
            FontStyle style = GetFontStyle();
            string styleText = style.ToString();
            try {
               mWorkingFont = new Font(familyName, fontSize, style);
               mFontDescriptionLabel.Font = mWorkingFont;
               mFontDescriptionLabel.Text = $"Selected font: {familyName}; {fontSize}pt; {styleText}";
            }
            catch {
               mFontDescriptionLabel.Text = $"Selected font: {familyName}; {fontSize}pt; {styleText} (unavailable)";
            }
            mFontPickerBottomPanel.LayoutControls();
         }

         private void SetFontsAndColors() {
            ThrowIfNull(mTheme, nameof(mTheme));
            Color backColor = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground],
               foreColor = mTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               groupBoxBackgroundColor = mTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
            Font interfaceFont = mTheme.mFonts[(int)FontUsage.Interface];
            BackColor = backColor;
            mScrollPanel.BackColor = backColor;
            mPickFontPanel.BackColor = backColor;
            mFontSizePanel.BackColor = backColor;
            mFamilyPrefixButton.Font = interfaceFont;
            mFamilyPrefixButton.ForeColor = foreColor;
            mFamilyPrefixButton.BackColor = backColor;
            FlattenButton(mFamilyPrefixButton, backColor);
            mFontDropDownPrefixButton.Font = interfaceFont;
            mFontDropDownPrefixButton.ForeColor = foreColor;
            mFontDropDownPrefixButton.BackColor = backColor;
            FlattenButton(mFontDropDownPrefixButton, backColor);
            mFontSizeDropDownPrefixButton.Font = interfaceFont;
            mFontSizeDropDownPrefixButton.ForeColor = foreColor;
            mFontSizeDropDownPrefixButton.BackColor = backColor;
            FlattenButton(mFontSizeDropDownPrefixButton, backColor);
            mFontSizePrefixButton.Font = interfaceFont;
            mFontSizePrefixButton.ForeColor = foreColor;
            mFontSizePrefixButton.BackColor = backColor;
            FlattenButton(mFontSizePrefixButton, backColor);
            mBoldStyleCheckBox.Font = interfaceFont;
            mBoldStyleCheckBox.ForeColor = foreColor;
            mBoldStyleCheckBox.BackColor = Color.Transparent;
            mItalicsStyleCheckBox.Font = interfaceFont;
            mItalicsStyleCheckBox.ForeColor = foreColor;
            mItalicsStyleCheckBox.BackColor = Color.Transparent;
            mNormalStyleCheckBox.Font = interfaceFont;
            mNormalStyleCheckBox.ForeColor = foreColor;
            mNormalStyleCheckBox.BackColor = Color.Transparent;
            mStrikethroughStyleCheckBox.Font = interfaceFont;
            mStrikethroughStyleCheckBox.ForeColor = foreColor;
            mStrikethroughStyleCheckBox.BackColor = Color.Transparent;
            mUnderlineStyleCheckBox.Font = interfaceFont;
            mUnderlineStyleCheckBox.ForeColor = foreColor;
            mUnderlineStyleCheckBox.BackColor = Color.Transparent;
            mFontFamilyComboBox.Font = interfaceFont;
            mFontFamilyComboBox.ForeColor = foreColor;
            mFontFamilyComboBox.BackColor = backColor;
            mFontSizeComboBox.Font = interfaceFont;
            mFontSizeComboBox.ForeColor = foreColor;
            mFontSizeComboBox.BackColor = backColor;
            mFontStyleGroupBox.Font = CreateNewBoldFont(interfaceFont);
            mFontStyleGroupBox.ForeColor = foreColor;
            mFontStyleGroupBox.BackColor = groupBoxBackgroundColor;
            mFontFamilyNameTextBox.Font = interfaceFont;
            mFontFamilyNameTextBox.ForeColor = foreColor;
            mFontFamilyNameTextBox.BackColor = backColor;
            mFontSizeTextBox.Font = interfaceFont;
            mFontSizeTextBox.ForeColor = foreColor;
            mFontSizeTextBox.BackColor = backColor;
            mFontPickerBottomPanel.SetFontAndColor();
            mTitleLabel.SetFontAndColor();
         }

         public bool FontHasChanged() {
            if (mWorkingFont!.Equals(mInitialFont))
               return false;
            return true;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               RemoveEventHandlers();
               mFamilyPrefixButton.Click -= FamilyPrefixButton_Click;
               mFontDropDownPrefixButton.Click -= FontDropDownPrefixButton_Click;
               mFontSizePrefixButton.Click -= FontSizePrefixButton_Click;
               mFontSizeDropDownPrefixButton.Click -= FontSizeDropDownPrefixButton_Click;
               mFontPickerBottomPanel.mHelpButton!.Click -= HelpButton_Click;
               mFontPickerBottomPanel.mCancelButton!.Click -= CancelButton_Click;
               mFontFamilyComboBox.DrawItem -= FontFamilyComboBox_DrawItem;
               mFontFamilyNameTextBox.Leave -= FontFamilyNameTextBox_Leave;
               mFontSizeTextBox.Leave -= FontSizeTextBox_Leave;
               mFontPickerBottomPanel.Dispose();
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
