namespace DBCode {
   internal partial class OptionsPanel : Panel {
      private readonly List<BaseCluster>? mBaseClusters;
      internal readonly BottomPanel? mBottomPanel;
      private readonly Button? mOKButton, mClearSearchButton, mClearReplaceButton;
      internal readonly DataGridView mIncludeDataGridView, mExcludeDataGridView;
      private readonly List<Font> mOwnedFonts = [];
      internal readonly GroupBox? mMagicNumbersGroupBox, mWhitespaceGroupBox, mPastingGroupBox, mHistoryGroupBox,
         mCodingGroupBox;
      internal readonly HeaderLabelCluster? mTitleLabel;
      private Panel? mScrollPanel, mGeneralScrollPanel, mIncludeScrollPanel, mExcludeScrollPanel, mCFamilyScrollablePanel, mBasicScrollablePanel,
         mFSharpScrollablePanel, mHTMLScrollablePanel, mCSSScrollablePanel, mXMLScrollablePanel, mJSONScrollablePanel, mPowerShellScrollablePanel,
         mBatchScrollablePanel, mSQLScrollablePanel, mMarkdownScrollablePanel, mPythonScrollablePanel;
      private readonly ScalableCheckBoxCluster? mTabCheckBoxCluster, mSpaceCheckBoxCluster, mAllIfNothingCheckBoxCluster,
         mCommentConcatenateFirstCheckBoxCluster;
      private readonly ScalableRadioButtonCluster? mWhitespaceRadioCluster, mPastingRadioCluster;
      internal readonly TwoLineHeaderLabelCluster? mCFamilyTitleLabel, mBasicTitleLabel, mFSharpTitleLabel, mHTMLTitleLabel, mCSSTitleLabel,
         mXMLTitleLabel, mJSONTitleLabel, mPowerShellTitleLabel, mBatchTitleLabel, mSQLTitleLabel, mMarkdownTitleLabel, mPythonTitleLabel;
      private readonly UpDownCluster? mTopDraggerHeightUpDownCluster, mTopDraggerEdgeUpDownCluster,
         mActivationDelayUpDownCluster, mReactivationRateUpDownCluster, mClipboardDelayUpDownCluster,
         mSearchUpDownCluster, mReplaceUpDownCluster, mTabUpDownCluster, mSpaceUpDownCluster, mCommentWidthUpDownCluster;
      private readonly VariableWidthTabControl? mGeneralTabControl, mIncludeExcludeTabControl, mCodingTabControl;

      private Font TrackFont(Font pFont) {
         mOwnedFonts.Add(pFont);
         return pFont;
      }

      public OptionsPanel() {
         ThrowIfNull(mUiState, nameof(mUiState));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         Color groupBoxBackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
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
         mCFamilyScrollablePanel = new Panel {
            Name = $"OptionsCFamilyTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mBasicScrollablePanel = new Panel {
            Name = $"OptionsBasicTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mFSharpScrollablePanel = new Panel {
            Name = $"OptionsFSharpTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mHTMLScrollablePanel = new Panel {
            Name = $"OptionsHTMLTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mCSSScrollablePanel = new Panel {
            Name = $"OptionsCSSTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mXMLScrollablePanel = new Panel {
            Name = $"OptionsXMLTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mJSONScrollablePanel = new Panel {
            Name = $"OptionsJSONTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mPowerShellScrollablePanel = new Panel {
            Name = $"OptionsPowerShellTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mBatchScrollablePanel = new Panel {
            Name = $"OptionsBatchTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mSQLScrollablePanel = new Panel {
            Name = $"OptionsSQLTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mMarkdownScrollablePanel = new Panel {
            Name = $"OptionsMarkdownTabControlTabPageScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
            Dock = DockStyle.Fill,
            AutoScroll = true
         };
         mPythonScrollablePanel = new Panel {
            Name = $"OptionsPythonTabControlTabPageScrollPanel{mTabIndex}",
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
         mGeneralTabControl.TabPages.AddRange([new TabPage("General"), new TabPage("Targeting"), new TabPage("Coding")]);
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
         mCodingTabControl = new VariableWidthTabControl {
            Name = $"OptionsCodingTabControl{mTabIndex}",
            TabIndex = mTabIndex++,
            DrawMode = TabDrawMode.OwnerDrawFixed,
            Dock = DockStyle.Fill
         };
         mCodingTabControl.DrawItem += CodingTabControl_DrawItem;
         mCodingTabControl.SetStripBackColor(mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]);
         mCodingTabControl.SelectedIndexChanged += CodingTabControl_SelectedIndexChanged;
         mCodingTabControl.TabPages.AddRange([new TabPage("C Family"), new TabPage("Basic"),
            new TabPage("F#"), new TabPage("HTML"), new TabPage("CSS"), new TabPage("XML"), new TabPage("JSON"),
            new TabPage("Power Shell"), new TabPage("Batch"), new TabPage("SQL"), new TabPage("Markdown"),
            new TabPage("Python")]);
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
         mCodingGroupBox = new GroupBox() {
            Name = "codingGroupBox",
            TabIndex = mTabIndex++,
            Text = "Coding Settings",
            Font = TrackFont(CreateNewBoldFont(mCurrentTheme.mFonts[(int)FontUsage.Interface])),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mCFamilyTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "C Family-Specific Options", "C, C++, C#", HeaderLabelSize.Normal,
            HeaderLabelSize.Small);
         mBasicTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "Basic Options", "Basic-based Scripting Languages", HeaderLabelSize.Normal, HeaderLabelSize.Small);
         mFSharpTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "F# Options", "Functional Programming", HeaderLabelSize.Normal, HeaderLabelSize.Small);
         mHTMLTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "HTML Options", "Web Markup", HeaderLabelSize.Normal, HeaderLabelSize.Small);
         mCSSTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "CSS Options", "Web Stylesheet", HeaderLabelSize.Normal, HeaderLabelSize.Small);
         mXMLTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "XML Options", "Structured Data Markup", HeaderLabelSize.Normal, HeaderLabelSize.Small);
         mJSONTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "JSON Options", "Structured Data", HeaderLabelSize.Normal, HeaderLabelSize.Small);
         mPowerShellTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "PowerShell Options", "Power Shell Scripting", HeaderLabelSize.Normal, HeaderLabelSize.Small);
         mBatchTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "Batch Options", "Command Prompt Scripting", HeaderLabelSize.Normal, HeaderLabelSize.Small);
         mSQLTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "SQL Options", "Database Querying", HeaderLabelSize.Normal, HeaderLabelSize.Small);
         mMarkdownTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "Markdown Options", "Text Formatting", HeaderLabelSize.Normal, HeaderLabelSize.Small);
         mPythonTitleLabel = new TwoLineHeaderLabelCluster(mCurrentTheme, "Python Options", "Python-based Scripting Languages", HeaderLabelSize.Normal, HeaderLabelSize.Small);
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
         mCommentWidthUpDownCluster = new UpDownCluster(mCurrentTheme, "Maximum Line Width:", 40, 300,
            mUiState.mCommentWidth, 1, groupBoxBackColor, "40–300");
         mSpaceCheckBoxCluster = new ScalableCheckBoxCluster(mCurrentTheme, "Kee&p Spaces", false, groupBoxBackColor);
         mSpaceCheckBoxCluster.mScalableCheckBox.Checked = mUiState.mUseSpaces;
         mAllIfNothingCheckBoxCluster = new ScalableCheckBoxCluster(mCurrentTheme, "All if &Nothing", false, groupBoxBackColor);
         mAllIfNothingCheckBoxCluster.mScalableCheckBox.Checked = mUiState.mAllIfNothing;
         mCommentConcatenateFirstCheckBoxCluster = new ScalableCheckBoxCluster(mCurrentTheme, "Concatenate First", false, groupBoxBackColor);
         mCommentConcatenateFirstCheckBoxCluster.mScalableCheckBox.Checked = mUiState.mCommentConcatenateFirst;
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
         mCodingGroupBox.Controls.AddRange([mAllIfNothingCheckBoxCluster, mCommentWidthUpDownCluster,
            mCommentConcatenateFirstCheckBoxCluster]);
         mGeneralScrollPanel.Controls.AddRange([mMagicNumbersGroupBox, mWhitespaceGroupBox, mPastingGroupBox,
            mHistoryGroupBox, mCodingGroupBox]);
         mGeneralTabControl.TabPages[(int)OptionsTabPageUsage.General].Controls.Add(mGeneralScrollPanel);
         mIncludeExcludeTabControl.TabPages[(int)TargetingTabPageUsage.Include].Controls.Add(mIncludeScrollPanel);
         mIncludeScrollPanel.Controls.Add(mIncludeDataGridView);
         mIncludeExcludeTabControl.TabPages[(int)TargetingTabPageUsage.Exclude].Controls.Add(mExcludeScrollPanel);
         mExcludeScrollPanel.Controls.Add(mExcludeDataGridView);
         mGeneralTabControl.TabPages[(int)OptionsTabPageUsage.Targeting].Controls.Add(mIncludeExcludeTabControl);
         mGeneralTabControl.TabPages[(int)OptionsTabPageUsage.Coding].Controls.Add(mCodingTabControl);
         mScrollPanel = new Panel {
            Name = $"OptionsScrollPanel{mTabIndex}",
            TabIndex = mTabIndex++,
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.PanelBackground],
            Dock = DockStyle.Fill
         };
         mScrollPanel.Controls.Add(mGeneralTabControl);
         mCodingTabControl.TabPages[(int)CodingTabPageUsage.CFamily].Controls.Add(mCFamilyScrollablePanel);
         mCodingTabControl.TabPages[(int)CodingTabPageUsage.Basic].Controls.Add(mBasicScrollablePanel);
         mCodingTabControl.TabPages[(int)CodingTabPageUsage.FSharp].Controls.Add(mFSharpScrollablePanel);
         mCodingTabControl.TabPages[(int)CodingTabPageUsage.HTML].Controls.Add(mHTMLScrollablePanel);
         mCodingTabControl.TabPages[(int)CodingTabPageUsage.CSS].Controls.Add(mCSSScrollablePanel);
         mCodingTabControl.TabPages[(int)CodingTabPageUsage.XML].Controls.Add(mXMLScrollablePanel);
         mCodingTabControl.TabPages[(int)CodingTabPageUsage.JSON].Controls.Add(mJSONScrollablePanel);
         mCodingTabControl.TabPages[(int)CodingTabPageUsage.PowerShell].Controls.Add(mPowerShellScrollablePanel);
         mCodingTabControl.TabPages[(int)CodingTabPageUsage.Batch].Controls.Add(mBatchScrollablePanel);
         mCodingTabControl.TabPages[(int)CodingTabPageUsage.SQL].Controls.Add(mSQLScrollablePanel);
         mCodingTabControl.TabPages[(int)CodingTabPageUsage.Markdown].Controls.Add(mMarkdownScrollablePanel);
         mCodingTabControl.TabPages[(int)CodingTabPageUsage.Python].Controls.Add(mPythonScrollablePanel);
         mCFamilyScrollablePanel.Controls.Add(mCFamilyTitleLabel);
         mBasicScrollablePanel.Controls.Add(mBasicTitleLabel);
         mFSharpScrollablePanel.Controls.Add(mFSharpTitleLabel);
         mHTMLScrollablePanel.Controls.Add(mHTMLTitleLabel);
         mCSSScrollablePanel.Controls.Add(mCSSTitleLabel);
         mXMLScrollablePanel.Controls.Add(mXMLTitleLabel);
         mJSONScrollablePanel.Controls.Add(mJSONTitleLabel);
         mPowerShellScrollablePanel.Controls.Add(mPowerShellTitleLabel);
         mBatchScrollablePanel.Controls.Add(mBatchTitleLabel);
         mSQLScrollablePanel.Controls.Add(mSQLTitleLabel);
         mMarkdownScrollablePanel.Controls.Add(mMarkdownTitleLabel);
         mPythonScrollablePanel.Controls.Add(mPythonTitleLabel);
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
         ThrowIfNull(mCodingTabControl, nameof(mCodingTabControl));
         ThrowIfNull(mCodingGroupBox, nameof(mCodingGroupBox));
         ThrowIfNull(mAllIfNothingCheckBoxCluster, nameof(mAllIfNothingCheckBoxCluster));
         ThrowIfNull(mCommentWidthUpDownCluster, nameof(mCommentWidthUpDownCluster));
         ThrowIfNull(mCommentConcatenateFirstCheckBoxCluster, nameof(mCommentConcatenateFirstCheckBoxCluster));
         ThrowIfNull(mCFamilyTitleLabel, nameof(mCFamilyTitleLabel));
         ThrowIfNull(mBasicTitleLabel, nameof(mBasicTitleLabel));
         ThrowIfNull(mFSharpTitleLabel, nameof(mFSharpTitleLabel));
         ThrowIfNull(mHTMLTitleLabel, nameof(mHTMLTitleLabel));
         ThrowIfNull(mCSSTitleLabel, nameof(mCSSTitleLabel));
         ThrowIfNull(mXMLTitleLabel, nameof(mXMLTitleLabel));
         ThrowIfNull(mJSONTitleLabel, nameof(mJSONTitleLabel));
         ThrowIfNull(mPowerShellTitleLabel, nameof(mPowerShellTitleLabel));
         ThrowIfNull(mBatchTitleLabel, nameof(mBatchTitleLabel));
         ThrowIfNull(mSQLTitleLabel, nameof(mSQLTitleLabel));
         ThrowIfNull(mMarkdownTitleLabel, nameof(mMarkdownTitleLabel));
         ThrowIfNull(mPythonTitleLabel, nameof(mPythonTitleLabel));
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
         mCodingTabControl.SelectedIndexChanged -= CodingTabControl_SelectedIndexChanged;
         mCodingTabControl.DrawItem -= CodingTabControl_DrawItem;
         int savedGeneral = mUiState.mOptionsGeneralTabControlPageIndex;
         int savedIncludeExclude = mUiState.mOptionsIncludeExcludeTabControlPageIndex;
         int savedCoding = mUiState.mOptionsCodingTabControlPageIndex;
         mGeneralTabControl.SelectedIndex = (int)OptionsTabPageUsage.General;
         mIncludeExcludeTabControl.SelectedIndex = (int)OptionsTabPageUsage.Targeting;
         mCodingTabControl.SelectedIndex = 0;
         for (int i = 0; i < mGeneralTabControl.TabPages.Count; i++)
            mGeneralTabControl.SelectedIndex = i;
         for (int i = 0; i < mIncludeExcludeTabControl.TabPages.Count; i++)
            mIncludeExcludeTabControl.SelectedIndex = i;
         for (int i = 0; i < mCodingTabControl.TabPages.Count; i++)
            mCodingTabControl.SelectedIndex = i;
         //efm5 - now that all controls have been visited, restore the persisted selections
         mGeneralTabControl.SelectedIndex = savedGeneral;
         mIncludeExcludeTabControl.SelectedIndex = savedIncludeExclude;
         mCodingTabControl.SelectedIndex = savedCoding;
         mGeneralTabControl.SelectedIndexChanged += GeneralTabControl_SelectedIndexChanged;
         mGeneralTabControl.DrawItem += GeneralTabControl_DrawItem;
         mIncludeExcludeTabControl.SelectedIndexChanged += IncludeExcludeTabControl_SelectedIndexChanged;
         mIncludeExcludeTabControl.DrawItem += IncludeExcludeTabControl_DrawItem;
         mCodingTabControl.SelectedIndexChanged += CodingTabControl_SelectedIndexChanged;
         mCodingTabControl.DrawItem += CodingTabControl_DrawItem;
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
         mCodingGroupBox.Location = new Point(mWhitespaceGroupBox.Right + mEm, mHistoryGroupBox.Bottom + mEm);
         mAllIfNothingCheckBoxCluster.Location = GetGroupBoxFirstLineOffset(mCodingGroupBox);
         mCommentWidthUpDownCluster.Location = new Point(mAllIfNothingCheckBoxCluster.Left, mAllIfNothingCheckBoxCluster.Bottom + mEm);
         mCommentConcatenateFirstCheckBoxCluster.Location = new Point(mAllIfNothingCheckBoxCluster.Left, mCommentWidthUpDownCluster.Bottom + mEm);
         SizeGroupBox(mCodingGroupBox);
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
         mCFamilyTitleLabel.LayoutCluster();
         mBasicTitleLabel.LayoutCluster();
         mFSharpTitleLabel.LayoutCluster();
         mHTMLTitleLabel.LayoutCluster();
         mCSSTitleLabel.LayoutCluster();
         mXMLTitleLabel.LayoutCluster();
         mJSONTitleLabel.LayoutCluster();
         mPowerShellTitleLabel.LayoutCluster();
         mBatchTitleLabel.LayoutCluster();
         mSQLTitleLabel.LayoutCluster();
         mMarkdownTitleLabel.LayoutCluster();
         mPythonTitleLabel.LayoutCluster();
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
            ThrowIfNull(mCodingTabControl, nameof(mCodingTabControl));
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
            mCodingTabControl.DrawItem -= CodingTabControl_DrawItem;
            mGeneralTabControl.SelectedIndexChanged -= GeneralTabControl_SelectedIndexChanged;
            mIncludeExcludeTabControl.SelectedIndexChanged -= IncludeExcludeTabControl_SelectedIndexChanged;
            mCodingTabControl.SelectedIndexChanged -= CodingTabControl_SelectedIndexChanged;
            foreach (Font font in mOwnedFonts)
               font.Dispose();
            mOwnedFonts.Clear();
            mCodingTabControl.Dispose();
            mIncludeExcludeTabControl.Dispose();
            mGeneralTabControl.Dispose();
         }
         base.Dispose(pDisposing);
      }
   }
}
