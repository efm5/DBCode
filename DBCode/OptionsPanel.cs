namespace DBCode {
   internal partial class OptionsPanel : Panel {
      private readonly List<BaseCluster>? mBaseClusters;
      internal readonly BottomPanel? mBottomPanel;
      private readonly Button? mOKButton, mClearSearchButton, mClearReplaceButton;
      internal readonly DataGridView mIncludeDataGridView, mExcludeDataGridView;
      private readonly List<Font> mOwnedFonts = [];
      internal readonly GroupBox? mMagicNumbersGroupBox, mWhitespaceGroupBox, mPastingGroupBox, mHistoryGroupBox;
      internal readonly HeaderLabelCluster? mTitleLabel;
      private Panel? mScrollPanel, mGeneralScrollPanel, mIncludeScrollPanel, mExcludeScrollPanel;
      private readonly ScalableCheckBoxCluster? mTabCheckBoxCluster, mSpaceCheckBoxCluster;
      private readonly ScalableRadioButtonCluster? mWhitespaceRadioCluster, mPastingRadioCluster;
      private readonly UpDownCluster? mTopDraggerHeightUpDownCluster, mTopDraggerEdgeUpDownCluster,
         mActivationDelayUpDownCluster, mReactivationRateUpDownCluster, mClipboardDelayUpDownCluster,
         mSearchUpDownCluster, mReplaceUpDownCluster, mTabUpDownCluster, mSpaceUpDownCluster;
      private readonly VariableWidthTabControl? mGeneralTabControl, mIncludeExcludeTabControl;

      private Font TrackFont(Font pFont) {
         mOwnedFonts.Add(pFont);
         return pFont;
      }

      public OptionsPanel() {
         ThrowIfNull(mUiState, nameof(mUiState));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         Name = "optionsPanel";
         TabIndex = mTabIndex++;
         BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.PanelBackground];
         Dock = DockStyle.Fill;
         SuspendLayout();
         mTitleLabel = new HeaderLabelCluster(mCurrentTheme, "Options", HeaderLabelSize.Large);
         mGeneralScrollPanel = new Panel {
            Name = $"OptionsPrimaryTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mIncludeScrollPanel = new Panel {
            Name = $"OptionsIncludeTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mExcludeScrollPanel = new Panel {
            Name = $"OptionsExcludeTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mGeneralTabControl = new VariableWidthTabControl {
            Name = $"OptionsGeneralTabControl{mTabIndex}",
            TabIndex = mTabIndex++,
            DrawMode = TabDrawMode.OwnerDrawFixed,
            Dock = DockStyle.Fill
         };
         mGeneralTabControl.DrawItem += GeneralTabControl_DrawItem;
         mGeneralTabControl.SetStripBackColor(mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]);
         mGeneralTabControl.SelectedIndexChanged += GeneralTabControl_SelectedIndexChanged;
         mGeneralTabControl.TabPages.AddRange([new TabPage("General"), new TabPage("Targeting")]);
         mIncludeExcludeTabControl = new VariableWidthTabControl {
            Name = $"OptionsIncludeExcludeTabControl{mTabIndex}",
            TabIndex = mTabIndex++,
            DrawMode = TabDrawMode.OwnerDrawFixed,
            Dock = DockStyle.Fill
         };
         mIncludeExcludeTabControl.DrawItem += IncludeExcludeTabControl_DrawItem;
         mIncludeExcludeTabControl.SetStripBackColor(mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]);
         mIncludeExcludeTabControl.SelectedIndexChanged += IncludeExcludeTabControl_SelectedIndexChanged;
         mIncludeExcludeTabControl.TabPages.AddRange([new TabPage("Inclusions"), new TabPage("Exclusions")]);
         mIncludeDataGridView = new DataGridView {
            Name = "IncludeDataGridView",
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
            AllowUserToAddRows = true,
            AllowUserToDeleteRows = true,
            AllowUserToResizeRows = false,
            RowHeadersVisible = false,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = true,
            Dock = DockStyle.Fill
         };
         mExcludeDataGridView = new DataGridView {
            Name = "ExcludeDataGridView",
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
            AllowUserToAddRows = true,
            AllowUserToDeleteRows = true,
            AllowUserToResizeRows = false,
            RowHeadersVisible = false,
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = true,
            Dock = DockStyle.Fill
         };
         mIncludeDataGridView.ColumnHeadersVisible = false;
         mExcludeDataGridView.ColumnHeadersVisible = false;
         mIncludeDataGridView.RowTemplate.Height = 25;
         mExcludeDataGridView.RowTemplate.Height = 25;
         mIncludeDataGridView.Columns.Add(new DataGridViewTextBoxColumn {
            HeaderText = "Include",
            Name = "IncludeColumn"
         });
         mExcludeDataGridView.Columns.Add(new DataGridViewTextBoxColumn {
            HeaderText = "Exclude",
            Name = "ExcludeColumn"
         });
         ApplyThemeToDataGridView(mIncludeDataGridView, mOwnedFonts);
         ApplyThemeToDataGridView(mExcludeDataGridView, mOwnedFonts);
         mTopDraggerHeightUpDownCluster = new UpDownCluster(mCurrentTheme, "Top &Dragger Height", 3, 50,
            mUiState.mTopDraggerHeight, 1, mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            "3–50");
         mTopDraggerEdgeUpDownCluster = new UpDownCluster(mCurrentTheme, "Top Dragger &Edge", 1, 10,
            mUiState.mTopDraggerEdge, 1, mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            "1–10");
         mActivationDelayUpDownCluster = new UpDownCluster(mCurrentTheme, "&Activation Delay", 10, 700,
            mUiState.mActivationDelayMs, 10, mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            "10–700, Steps = 10");
         mReactivationRateUpDownCluster = new UpDownCluster(mCurrentTheme, "Reactivation Rate", 10, 700,
            mUiState.mReactivationDelayMs, 10, mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            "10–700, Steps = 10");
         mClipboardDelayUpDownCluster = new UpDownCluster(mCurrentTheme, "C&lipboard Delay", 100, 500,
            mUiState.mClipboardDelayMs, 10, mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            "100–500, Steps = 10");
         mSearchUpDownCluster = new UpDownCluster(mCurrentTheme, "&Search History", 10, 50,
            mUiState.mSearchHistoryMaxEntries, 1, mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            "10–50, Steps = 1");
         mReplaceUpDownCluster = new UpDownCluster(mCurrentTheme, "&Replace History", 10, 50,
            mUiState.mReplaceHistoryMaxEntries, 1, mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            "10–50, Steps = 1");
         mBaseClusters = [];
         mMagicNumbersGroupBox = new GroupBox() {
            Name = "clusterGroupBox",
            TabIndex = mTabIndex++,
            Location = new Point(mTabControlLeftPad, mTabControlTopPad),
            Text = "Magic Numbers",
            Font = TrackFont(CreateNewBoldFont(mCurrentTheme.mFonts[(int)FontUsage.Interface])),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
         };
         mWhitespaceGroupBox = new GroupBox() {
            Name = "whitespaceGroupBox",
            TabIndex = mTabIndex++,
            Text = "Whitespace",
            Font = TrackFont(CreateNewBoldFont(mCurrentTheme.mFonts[(int)FontUsage.Interface])),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mPastingGroupBox = new GroupBox() {
            Name = "pastingGroupBox",
            TabIndex = mTabIndex++,
            Text = "Target Pasting Shortcut",
            Font = TrackFont(CreateNewBoldFont(mCurrentTheme.mFonts[(int)FontUsage.Interface])),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mHistoryGroupBox = new GroupBox() {
            Name = "historyGroupBox",
            TabIndex = mTabIndex++,
            Text = "Search",
            Font = TrackFont(CreateNewBoldFont(mCurrentTheme.mFonts[(int)FontUsage.Interface])),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         Color groupBoxBackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
         mWhitespaceRadioCluster = new ScalableRadioButtonCluster(mCurrentTheme, [
            new ScalableRadioButtons.RadioButtonQuad("&Both", "", mUiState.mWhitespace == (int)Whitespace.Both, (int)Whitespace.Both),
            new ScalableRadioButtons.RadioButtonQuad("&Tabs", "", mUiState.mWhitespace == (int)Whitespace.Tabs, (int)Whitespace.Tabs),
            new ScalableRadioButtons.RadioButtonQuad("&Spaces", "", mUiState.mWhitespace == (int)Whitespace.Spaces, (int)Whitespace.Spaces),
         ], false, false);
         mWhitespaceRadioCluster.mRadioPanel.mScalableRadioButtons[1].Click += TabRadioButton_Click;
         mWhitespaceRadioCluster.mRadioPanel.mScalableRadioButtons[2].Click += SpaceRadioButton_Click;
         mTabUpDownCluster = new UpDownCluster(mCurrentTheme, "Spaces to become a tab", 2, 12,
            mUiState.mSpacesPerTab, 1, groupBoxBackColor, "2–12");
         mTabCheckBoxCluster = new ScalableCheckBoxCluster(mCurrentTheme, "&Keep Tabs", false, groupBoxBackColor);
         mTabCheckBoxCluster.mScalableCheckBox.Checked = mUiState.mUseTabs;
         mSpaceUpDownCluster = new UpDownCluster(mCurrentTheme, "Spaces per tab", 2, 12,
            mUiState.mSpacesToBecomeTab, 1, groupBoxBackColor, "2–12");
         mSpaceCheckBoxCluster = new ScalableCheckBoxCluster(mCurrentTheme, "Kee&p Spaces", false, groupBoxBackColor);
         mSpaceCheckBoxCluster.mScalableCheckBox.Checked = mUiState.mUseSpaces;
         mWhitespaceGroupBox.Controls.AddRange([mWhitespaceRadioCluster, mTabUpDownCluster, mTabCheckBoxCluster,
            mSpaceUpDownCluster, mSpaceCheckBoxCluster]);
         mBottomPanel = new BottomPanel(mCurrentTheme) {
            Anchor = AnchorStyles.None,
            Dock = DockStyle.Bottom
         };
         mOKButton = new Button {
            Name = $"OptionsOKButton{mTabIndex}",
            TabIndex = mTabIndex++,
            Text = "&OK",
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
         };
         mOKButton.Click += OKButton_Click;
         mClearSearchButton = new Button {
            Name = $"OptionsClearSearchButton{mTabIndex}",
            TabIndex = mTabIndex++,
            Text = "Clear &Search",
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
         };
         mClearSearchButton.Click += ClearSearchButton_Click;
         mClearReplaceButton = new Button {
            Name = $"OptionsClearReplaceButton{mTabIndex}",
            TabIndex = mTabIndex++,
            Text = "Clear &Replace",
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
         };
         mClearReplaceButton.Click += ClearReplaceButton_Click;
         mPastingRadioCluster = new ScalableRadioButtonCluster(mCurrentTheme, [
            new ScalableRadioButtons.RadioButtonQuad("Control &V", "", mUseControlPasting, 0),
            new ScalableRadioButtons.RadioButtonQuad("Alternate &V", "", !mUseControlPasting, 1),
         ], false, false);
         foreach (ScalableRadioButtons.ScalableRadioButton button in mPastingRadioCluster.mRadioPanel.mScalableRadioButtons)
            button.Click += CtrlVAltV_Click;
         mPastingGroupBox.Controls.AddRange([mPastingRadioCluster]);
         mBottomPanel.AddRightControl(mOKButton);
         mBaseClusters.AddRange([mTopDraggerHeightUpDownCluster,
            mTopDraggerEdgeUpDownCluster, mActivationDelayUpDownCluster, mReactivationRateUpDownCluster,
            mClipboardDelayUpDownCluster, mSearchUpDownCluster, mReplaceUpDownCluster,
            mWhitespaceRadioCluster, mPastingRadioCluster,
            mTabUpDownCluster, mTabCheckBoxCluster, mSpaceUpDownCluster, mSpaceCheckBoxCluster]);
         mMagicNumbersGroupBox.Controls.AddRange([mTopDraggerHeightUpDownCluster,
            mTopDraggerEdgeUpDownCluster, mActivationDelayUpDownCluster, mReactivationRateUpDownCluster,
            mClipboardDelayUpDownCluster]);
         mHistoryGroupBox.Controls.AddRange([mSearchUpDownCluster, mReplaceUpDownCluster,
            mClearSearchButton, mClearReplaceButton]);
         mGeneralScrollPanel.Controls.AddRange([mMagicNumbersGroupBox, mWhitespaceGroupBox, mPastingGroupBox,
            mHistoryGroupBox]);
         mGeneralTabControl.TabPages[(int)OptionsTabPageUsage.General].Controls.Add(mGeneralScrollPanel);
         mIncludeExcludeTabControl.TabPages[(int)TargetingTabPageUsage.Include].Controls.Add(mIncludeScrollPanel);
         mIncludeScrollPanel.Controls.Add(mIncludeDataGridView);
         mIncludeExcludeTabControl.TabPages[(int)TargetingTabPageUsage.Exclude].Controls.Add(mExcludeScrollPanel);
         mExcludeScrollPanel.Controls.Add(mExcludeDataGridView);
         mGeneralTabControl.TabPages[(int)OptionsTabPageUsage.Targeting].Controls.Add(mIncludeExcludeTabControl);
         mScrollPanel = new Panel {
            Name = $"OptionsScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.PanelBackground],
            Dock = DockStyle.Fill
         };
         mScrollPanel.Controls.Add(mGeneralTabControl);
         Controls.AddRange([mScrollPanel, mBottomPanel, mTitleLabel]);
         ThrowIfNull(mBottomPanel.mHelpButton, nameof(mBottomPanel.mHelpButton));
         ThrowIfNull(mBottomPanel.mCancelButton, nameof(mBottomPanel.mCancelButton));
         mBottomPanel.mHelpButton.Tag = new HelpTag(HelpContext.Options);
         mBottomPanel.mCancelButton.Click += CancelButton_Click;
         mBottomPanel.mHelpButton.Click += MainForm.Help_Click;
         ResumeLayout(true);
      }

      internal void ApplyFontsAndColors() {
         ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
         ThrowIfNull(mBaseClusters, nameof(mBaseClusters));
         mTitleLabel.SetFontAndColor();
         foreach (BaseCluster cluster in mBaseClusters)
            cluster.SetFontAndColor();
      }

      internal void LayoutControls(bool pApplyFonts = true) {
         ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
         ThrowIfNull(mMagicNumbersGroupBox, nameof(mMagicNumbersGroupBox));
         ThrowIfNull(mBottomPanel, nameof(mBottomPanel));
         ThrowIfNull(mBaseClusters, nameof(mBaseClusters));
         ThrowIfNull(mTopDraggerHeightUpDownCluster, nameof(mTopDraggerHeightUpDownCluster));
         ThrowIfNull(mTopDraggerEdgeUpDownCluster, nameof(mTopDraggerEdgeUpDownCluster));
         ThrowIfNull(mActivationDelayUpDownCluster, nameof(mActivationDelayUpDownCluster));
         ThrowIfNull(mReactivationRateUpDownCluster, nameof(mReactivationRateUpDownCluster));
         ThrowIfNull(mClipboardDelayUpDownCluster, nameof(mClipboardDelayUpDownCluster));
         ThrowIfNull(mWhitespaceGroupBox, nameof(mWhitespaceGroupBox));
         ThrowIfNull(mWhitespaceRadioCluster, nameof(mWhitespaceRadioCluster));
         ThrowIfNull(mTabUpDownCluster, nameof(mTabUpDownCluster));
         ThrowIfNull(mTabCheckBoxCluster, nameof(mTabCheckBoxCluster));
         ThrowIfNull(mSpaceUpDownCluster, nameof(mSpaceUpDownCluster));
         ThrowIfNull(mSpaceCheckBoxCluster, nameof(mSpaceCheckBoxCluster));
         ThrowIfNull(mPastingGroupBox, nameof(mPastingGroupBox));
         ThrowIfNull(mPastingRadioCluster, nameof(mPastingRadioCluster));
         ThrowIfNull(mHistoryGroupBox, nameof(mHistoryGroupBox));
         ThrowIfNull(mSearchUpDownCluster, nameof(mSearchUpDownCluster));
         ThrowIfNull(mReplaceUpDownCluster, nameof(mReplaceUpDownCluster));
         ThrowIfNull(mClearSearchButton, nameof(mClearSearchButton));
         ThrowIfNull(mClearReplaceButton, nameof(mClearReplaceButton));
         ThrowIfNull(mGeneralTabControl, nameof(mGeneralTabControl));
         ThrowIfNull(mIncludeExcludeTabControl, nameof(mIncludeExcludeTabControl));
         SuspendLayout();
         mTitleLabel.LayoutCluster(pApplyFonts);
         foreach (BaseCluster cluster in mBaseClusters)
            cluster.LayoutCluster(pApplyFonts);
         //efm5 - switching to each tab forces the controls to be created and laid out, which is necessary
         //to get accurate measurements for the wanted size of the panel; without this, the panel may report
         //a smaller wanted size than it actually needs
         mGeneralTabControl.SelectedIndexChanged -= GeneralTabControl_SelectedIndexChanged;
         mGeneralTabControl.DrawItem -= GeneralTabControl_DrawItem;
         mIncludeExcludeTabControl.SelectedIndexChanged -= IncludeExcludeTabControl_SelectedIndexChanged;
         mIncludeExcludeTabControl.DrawItem -= IncludeExcludeTabControl_DrawItem;
         int savedGeneral = mUiState.mOptionsGeneralTabControlPageIndex;
         int savedIncludeExclude = mUiState.mOptionsIncludeExcludeTabControlPageIndex;
         mGeneralTabControl.SelectedIndex = (int)OptionsTabPageUsage.General;
         mIncludeExcludeTabControl.SelectedIndex = (int)OptionsTabPageUsage.Targeting;
         for (int i = 0; i < mGeneralTabControl.TabPages.Count; i++)
            mGeneralTabControl.SelectedIndex = i;
         for (int i = 0; i < mIncludeExcludeTabControl.TabPages.Count; i++)
            mIncludeExcludeTabControl.SelectedIndex = i;
         //efm5 - now that all controls have been visited, restore the persisted selections
         mGeneralTabControl.SelectedIndex = savedGeneral;
         mIncludeExcludeTabControl.SelectedIndex = savedIncludeExclude;
         mGeneralTabControl.SelectedIndexChanged += GeneralTabControl_SelectedIndexChanged;
         mGeneralTabControl.DrawItem += GeneralTabControl_DrawItem;
         mIncludeExcludeTabControl.SelectedIndexChanged += IncludeExcludeTabControl_SelectedIndexChanged;
         mIncludeExcludeTabControl.DrawItem += IncludeExcludeTabControl_DrawItem;
         mTopDraggerHeightUpDownCluster.Location = GetGroupBoxFirstLineOffset(mMagicNumbersGroupBox);
         mTopDraggerEdgeUpDownCluster.Location =
            new Point(mTopDraggerHeightUpDownCluster.Left, mTopDraggerHeightUpDownCluster.Bottom + mEm);
         mActivationDelayUpDownCluster.Location =
            new Point(mTopDraggerHeightUpDownCluster.Left, mTopDraggerEdgeUpDownCluster.Bottom + mEm);
         mReactivationRateUpDownCluster.Location =
            new Point(mTopDraggerHeightUpDownCluster.Left, mActivationDelayUpDownCluster.Bottom + mEm);
         mClipboardDelayUpDownCluster.Location =
            new Point(mTopDraggerHeightUpDownCluster.Left, mReactivationRateUpDownCluster.Bottom + mEm);
         SizeGroupBox(mMagicNumbersGroupBox);
         mWhitespaceGroupBox.Location = new Point(mIndent, mMagicNumbersGroupBox.Bottom + mEm);
         mWhitespaceRadioCluster.Location = GetGroupBoxFirstLineOffset(mWhitespaceGroupBox);
         int radioRight = mWhitespaceRadioCluster.Right + mEm;
         int radioPanelOffsetY = mWhitespaceRadioCluster.Top + mWhitespaceRadioCluster.mRadioPanel.Top;
         int tabRowY = radioPanelOffsetY + mWhitespaceRadioCluster.mRadioPanel.mScalableRadioButtons[1].Top;
         int spaceRowY = radioPanelOffsetY + mWhitespaceRadioCluster.mRadioPanel.mScalableRadioButtons[2].Top;
         mTabUpDownCluster.Location = new Point(radioRight, tabRowY);
         mTabCheckBoxCluster.Location = new Point(mTabUpDownCluster.Right + mEm, tabRowY);
         mSpaceUpDownCluster.Location = new Point(radioRight, spaceRowY);
         mSpaceCheckBoxCluster.Location = new Point(mSpaceUpDownCluster.Right + mEm, spaceRowY);
         SizeGroupBox(mWhitespaceGroupBox);
         mPastingGroupBox.Location = new Point(mMagicNumbersGroupBox.Right + mEm, mMagicNumbersGroupBox.Top);
         mPastingRadioCluster.Location = GetGroupBoxFirstLineOffset(mPastingGroupBox);
         SizeGroupBox(mPastingGroupBox);
         Size textSize = TextRenderer.MeasureText(mPastingGroupBox.Text, mPastingGroupBox.Font);
         mPastingGroupBox.Size = new Size(Math.Max(mPastingRadioCluster.Right, textSize.Width) + mGroupRightPad,
            mPastingGroupBox.Height);
         mHistoryGroupBox.Location = new Point(mPastingGroupBox.Left, mPastingGroupBox.Bottom + mEm);
         mSearchUpDownCluster.Location = GetGroupBoxFirstLineOffset(mHistoryGroupBox);
         mClearSearchButton.Location = new Point(mSearchUpDownCluster.Right + mEm, mSearchUpDownCluster.Top);
         mReplaceUpDownCluster.Location =
            new Point(mSearchUpDownCluster.Left, mSearchUpDownCluster.Bottom + mEm);
         mClearReplaceButton.Location = new Point(mReplaceUpDownCluster.Right + mEm, mReplaceUpDownCluster.Top);
         SizeGroupBox(mHistoryGroupBox);
         mBottomPanel.LayoutControls();
         mGeneralTabControl.SelectedIndexChanged -= GeneralTabControl_SelectedIndexChanged;
         mGeneralTabControl.DrawItem -= GeneralTabControl_DrawItem;
         savedGeneral = mUiState.mOptionsGeneralTabControlPageIndex;
         mGeneralTabControl.SelectedIndex = (int)OptionsTabPageUsage.General;
         for (int i = 0; i < mGeneralTabControl.TabPages.Count; i++)
            mGeneralTabControl.SelectedIndex = i;
         mGeneralTabControl.SelectedIndex = savedGeneral;
         mGeneralTabControl.SelectedIndexChanged += GeneralTabControl_SelectedIndexChanged;
         mGeneralTabControl.DrawItem += GeneralTabControl_DrawItem;
         Rectangle client = mGeneralTabControl.ClientRectangle;
         Rectangle display = mGeneralTabControl.DisplayRectangle;
         int tabStripHeight = display.Top - client.Top;
         Size wantedSize = new Size(mHistoryGroupBox.Right + (mTabControlLeftPad * 2) +
            SystemInformation.VerticalScrollBarWidth + mEm,
            mTitleLabel.Height + mTitleBarHeight + mBottomPanel.Height + tabStripHeight + (mTabControlTopPad * 2) +
            mWhitespaceGroupBox.Bottom + mEm3 + SystemInformation.HorizontalScrollBarHeight);
         if (mUiState.mOptionsFirstShow) {
            Screen screen = Screen.FromPoint(mUiState.FormBounds.Location);
            Point location = new Point(((screen.WorkingArea.Width - wantedSize.Width) / 2) + screen.WorkingArea.Left,
               ((screen.WorkingArea.Height - wantedSize.Height) / 2) + screen.WorkingArea.Top);
            mUiState.mOptionsBounds = new Rectangle(location, wantedSize);
            mUiState.mOptionsFirstShow = false;
         }
         else {
            mUiState.mOptionsBounds = new Rectangle(mUiState.mOptionsBounds.Location, wantedSize);
         }
         ResumeLayout(true);
      }

      internal Size GetRequiredSize() {
         ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
         ThrowIfNull(mPastingGroupBox, nameof(mPastingGroupBox));
         ThrowIfNull(mBottomPanel, nameof(mBottomPanel));
         ThrowIfNull(mGeneralTabControl, nameof(mGeneralTabControl));
         return new Size(mPastingGroupBox.Right + mEm + SystemInformation.VerticalScrollBarWidth,
            mTitleLabel.Height + mGeneralTabControl.Height + mBottomPanel.Height +
            SystemInformation.HorizontalScrollBarHeight);
      }

      private static void ApplyThemeToDataGridView(DataGridView pGrid, List<Font> pOwnedFonts) {
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         Color backColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground];
         Color foreColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont];
         Color selectedBackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
         Color selectedForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont];
         Font interfaceFont = mCurrentTheme.mFonts[(int)FontUsage.Interface];
         Font boldFont = CreateNewBoldFont(interfaceFont);
         pOwnedFonts.Add(boldFont);
         pGrid.EnableHeadersVisualStyles = false;
         pGrid.BackgroundColor = backColor;
         pGrid.GridColor = foreColor;
         pGrid.DefaultCellStyle.BackColor = backColor;
         pGrid.DefaultCellStyle.ForeColor = foreColor;
         pGrid.DefaultCellStyle.SelectionBackColor = selectedBackColor;
         pGrid.DefaultCellStyle.SelectionForeColor = selectedForeColor;
         pGrid.DefaultCellStyle.Font = interfaceFont;
         pGrid.ColumnHeadersDefaultCellStyle.BackColor = selectedBackColor;
         pGrid.ColumnHeadersDefaultCellStyle.ForeColor = selectedForeColor;
         pGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = selectedBackColor;
         pGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = selectedForeColor;
         pGrid.ColumnHeadersDefaultCellStyle.Font = boldFont;
      }

      private static void CloseOptionsPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         mForm.RestoreFromOptionsPanel();
      }

      protected override void Dispose(bool pDisposing) {
         if (pDisposing) {
            ThrowIfNull(mBottomPanel, nameof(mBottomPanel));
            ThrowIfNull(mBottomPanel.mCancelButton, nameof(mBottomPanel.mCancelButton));
            ThrowIfNull(mBottomPanel.mHelpButton, nameof(mBottomPanel.mHelpButton));
            ThrowIfNull(mGeneralTabControl, nameof(mGeneralTabControl));
            ThrowIfNull(mIncludeExcludeTabControl, nameof(mIncludeExcludeTabControl));
            mOKButton?.Click -= OKButton_Click;
            mClearSearchButton?.Click -= ClearSearchButton_Click;
            mClearReplaceButton?.Click -= ClearReplaceButton_Click;
            if (mWhitespaceRadioCluster != null) {
               mWhitespaceRadioCluster.mRadioPanel.mScalableRadioButtons[1].Click -= TabRadioButton_Click;
               mWhitespaceRadioCluster.mRadioPanel.mScalableRadioButtons[2].Click -= SpaceRadioButton_Click;
            }
            if (mPastingRadioCluster != null)
               foreach (ScalableRadioButtons.ScalableRadioButton button in mPastingRadioCluster.mRadioPanel.mScalableRadioButtons)
                  button.Click -= CtrlVAltV_Click;
            mBottomPanel.mCancelButton.Click -= CancelButton_Click;
            mBottomPanel.mHelpButton.Click -= MainForm.Help_Click;
            mGeneralTabControl.DrawItem -= GeneralTabControl_DrawItem;
            mIncludeExcludeTabControl.DrawItem -= IncludeExcludeTabControl_DrawItem;
            mGeneralTabControl.SelectedIndexChanged -= GeneralTabControl_SelectedIndexChanged;
            mIncludeExcludeTabControl.SelectedIndexChanged -= IncludeExcludeTabControl_SelectedIndexChanged;
            foreach (Font font in mOwnedFonts)
               font.Dispose();
            mOwnedFonts.Clear();
            mIncludeExcludeTabControl.Dispose();
            mGeneralTabControl.Dispose();
         }
         base.Dispose(pDisposing);
      }
   }
}
