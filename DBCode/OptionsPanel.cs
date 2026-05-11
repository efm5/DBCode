namespace DBCode {
   internal sealed partial class OptionsPanel : ScrollablePanel {
      internal UpDownCluster mTopDraggerHeightUpDownCluster;
      internal UpDownCluster mTopDraggerEdgeUpDownCluster;
      internal UpDownCluster mActivationDelayUpDownCluster;
      internal UpDownCluster mReactivationRateUpDownCluster;
      internal UpDownCluster mClipboardDelayUpDownCluster;
      internal HeaderLabelCluster mTitleLabel;
      private readonly Button mOKButton;
      internal readonly BottomPanel? mOptionsBottomPanel;
      internal GroupBox? mClusterGroupBox;
      private List<BaseCluster>? mBaseClusters;
      internal GroupBox? mWhitespaceGroupBox;
      internal RadioButton? mBothRadioButton;
      internal RadioButton? mTabRadioButton;
      internal RadioButton? mSpaceRadioButton;
      internal CheckBox? mTabCheckBox;
      internal CheckBox? mSpaceCheckBox;
      internal NumericUpDown? mTabUpDown;
      internal NumericUpDown? mSpaceUpDown;
      internal Label? mTabSuffixUpDownLabel;
      internal Label? mSpaceSuffixUpDownLabel;

      public OptionsPanel() {
         ThrowIfNull(mUiState, nameof(mUiState));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         Name = "optionsPanel";
         TabIndex = mTabIndex++;
         SuspendLayout();
         mTitleLabel = new HeaderLabelCluster(mCurrentTheme, "Options", HeaderLabelSize.Large);
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
         mBaseClusters = [];
         mClusterGroupBox = new GroupBox() {
            Name = "clusterGroupBox",
            TabIndex = mTabIndex++,
            Text = "Magic Numbers",
            Font = CreateNewBoldFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground],
         };
         mWhitespaceGroupBox = new GroupBox() {
            Name = "whitespaceGroupBox",
            TabIndex = mTabIndex++,
            Text = "Whitespace",
            Font = CreateNewBoldFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mBothRadioButton = new RadioButton() {
            Name = "bothRadioButton",
            TabIndex = mTabIndex++,
            Text = "&Both",
            AutoSize = true,
            Font = CreateNewFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mTabRadioButton = new RadioButton() {
            Name = "tabRadioButton",
            TabIndex = mTabIndex++,
            Text = "&Tabs",
            AutoSize = true,
            Font = CreateNewFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mTabRadioButton.Click += Tab_Click;
         mTabUpDown = new NumericUpDown() {
            Name = "tabUpDownCluster",
            TabIndex = mTabIndex++,
            Minimum = 2,
            Maximum = 12,
            Font = CreateNewFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mTabCheckBox = new CheckBox() {
            Name = "tabCheckBox",
            TabIndex = mTabIndex++,
            Text = "&Keep Tabs",
            AutoSize = true,
            Checked = mUiState.mUseTabs,
            Font = CreateNewFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mTabSuffixUpDownLabel = new Label() {
            Name = "tabSuffixUpDownLabel",
            TabIndex = mTabIndex++,
            Text = "Spaces to become a tab",
            AutoSize = true,
            Font = CreateNewFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mSpaceRadioButton = new RadioButton() {
            Name = "spaceRadioButton",
            TabIndex = mTabIndex++,
            Text = "&Spaces",
            AutoSize = true,
            Font = CreateNewFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mSpaceRadioButton.Click += Space_Click;
         mSpaceUpDown = new NumericUpDown() {
            Name = "spaceUpDownCluster",
            TabIndex = mTabIndex++,
            Minimum = 2,
            Maximum = 12,
            Font = CreateNewFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mSpaceCheckBox = new CheckBox() {
            Name = "spaceCheckBox",
            TabIndex = mTabIndex++,
            Text = "Kee&p Spaces",
            Checked = mUiState.mUseSpaces,
            AutoSize = true,
            Font = CreateNewFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mSpaceSuffixUpDownLabel = new Label() {
            Name = "spaceSuffixUpDownLabel",
            TabIndex = mTabIndex++,
            Text = "Spaces per tab",
            AutoSize = true,
            Font = CreateNewFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]),
            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont],
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]
         };
         mWhitespaceGroupBox.Controls.AddRange([mBothRadioButton, mTabRadioButton, mSpaceRadioButton,
            mTabCheckBox, mSpaceCheckBox, mTabUpDown, mTabSuffixUpDownLabel, mSpaceUpDown, mSpaceSuffixUpDownLabel]);
         mOptionsBottomPanel = new BottomPanel(mCurrentTheme);
         mOKButton = new Button {
            Name = $"OptionsOKButton{mTabIndex}",
            TabIndex = mTabIndex++,
            Text = "&OK",
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink
         };
         mBothRadioButton.Checked = false;
         mTabRadioButton.Checked = false;
         mSpaceRadioButton.Checked = false;
         switch (mUiState.mWhitespace) {
            case (int)Whitespace.Both:
               mBothRadioButton.Checked = true;
               break;
            case (int)Whitespace.Tabs:
               mTabRadioButton.Checked = true;
               break;
            case (int)Whitespace.Spaces:
               mSpaceRadioButton.Checked = true;
               break;
         }
         mTabUpDown.Value = mUiState.mSpacesPerTab;
         mSpaceUpDown.Value = mUiState.mSpacesToBecomeTab;
         mOptionsBottomPanel.AddRightControl(mOKButton);
         mBaseClusters.AddRange([mTopDraggerHeightUpDownCluster,
            mTopDraggerEdgeUpDownCluster, mActivationDelayUpDownCluster, mReactivationRateUpDownCluster,
            mClipboardDelayUpDownCluster]);
         mClusterGroupBox.Controls.AddRange([mTopDraggerHeightUpDownCluster,
            mTopDraggerEdgeUpDownCluster, mActivationDelayUpDownCluster, mReactivationRateUpDownCluster,
            mClipboardDelayUpDownCluster]);
         Controls.AddRange([mClusterGroupBox, mWhitespaceGroupBox, mOptionsBottomPanel, mTitleLabel]);
         ThrowIfNull(mOptionsBottomPanel.mHelpButton, nameof(mOptionsBottomPanel.mHelpButton));
         ThrowIfNull(mOptionsBottomPanel.mCancelButton, nameof(mOptionsBottomPanel.mCancelButton));
         mOptionsBottomPanel.mHelpButton.Tag = new HelpTag(HelpContext.Main, "Options");
         mOKButton.Click += OKButton_Click;
         mOptionsBottomPanel.mCancelButton.Click += CancelButton_Click;
         mOptionsBottomPanel.mHelpButton.Click += MainForm.Help_Click;
         ResumeLayout(true);
      }

      internal void LayoutControls() {
         ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
         ThrowIfNull(mClusterGroupBox, nameof(mClusterGroupBox));
         ThrowIfNull(mOptionsBottomPanel, nameof(mOptionsBottomPanel));
         ThrowIfNull(mBaseClusters, nameof(mBaseClusters));
         ThrowIfNull(mTopDraggerHeightUpDownCluster, nameof(mTopDraggerHeightUpDownCluster));
         ThrowIfNull(mTopDraggerEdgeUpDownCluster, nameof(mTopDraggerEdgeUpDownCluster));
         ThrowIfNull(mActivationDelayUpDownCluster, nameof(mActivationDelayUpDownCluster));
         ThrowIfNull(mReactivationRateUpDownCluster, nameof(mReactivationRateUpDownCluster));
         ThrowIfNull(mClipboardDelayUpDownCluster, nameof(mClipboardDelayUpDownCluster));
         ThrowIfNull(mWhitespaceGroupBox, nameof(mWhitespaceGroupBox));
         ThrowIfNull(mBothRadioButton, nameof(mBothRadioButton));
         ThrowIfNull(mTabRadioButton, nameof(mTabRadioButton));
         ThrowIfNull(mSpaceRadioButton, nameof(mSpaceRadioButton));
         ThrowIfNull(mTabUpDown, nameof(mTabUpDown));
         ThrowIfNull(mSpaceUpDown, nameof(mSpaceUpDown));
         ThrowIfNull(mTabSuffixUpDownLabel, nameof(mTabSuffixUpDownLabel));
         ThrowIfNull(mSpaceSuffixUpDownLabel, nameof(mSpaceSuffixUpDownLabel));
         ThrowIfNull(mTabCheckBox, nameof(mTabCheckBox));
         ThrowIfNull(mSpaceCheckBox, nameof(mSpaceCheckBox));
         ThrowIfNull(mForm, nameof(mForm));
         mTitleLabel.LayoutCluster();
         foreach (BaseCluster cluster in mBaseClusters)
            cluster.LayoutCluster();
         mClusterGroupBox.Location = new Point(mIndent, mTitleLabel.Bottom);
         mTopDraggerHeightUpDownCluster.Location = GetGroupBoxFirstLineOffset(mClusterGroupBox);
         mTopDraggerEdgeUpDownCluster.Location =
            new Point(mTopDraggerHeightUpDownCluster.Left, mTopDraggerHeightUpDownCluster.Bottom + mEm);
         mActivationDelayUpDownCluster.Location =
            new Point(mTopDraggerHeightUpDownCluster.Left, mTopDraggerEdgeUpDownCluster.Bottom + mEm);
         mReactivationRateUpDownCluster.Location =
            new Point(mTopDraggerHeightUpDownCluster.Left, mActivationDelayUpDownCluster.Bottom + mEm);
         mClipboardDelayUpDownCluster.Location =
            new Point(mTopDraggerHeightUpDownCluster.Left, mReactivationRateUpDownCluster.Bottom + mEm);
         SizeGroupBox(mClusterGroupBox);
         mWhitespaceGroupBox.Location = new Point(mIndent, mClusterGroupBox.Bottom + mEm);
         mBothRadioButton.Location = GetGroupBoxFirstLineOffset(mWhitespaceGroupBox);
         mTabRadioButton.Location = new Point(mBothRadioButton.Left, mBothRadioButton.Bottom + mEmHalf);
         mTabUpDown.Location = new Point(mTabRadioButton.Right + mEm, mTabRadioButton.Top);
         mTabSuffixUpDownLabel.Location = new Point(mTabUpDown.Right + mEmHalf, mTabRadioButton.Top);
         mTabCheckBox.Location = new Point(mTabSuffixUpDownLabel.Right + mEm, mTabRadioButton.Top);
         mSpaceRadioButton.Location = new Point(mBothRadioButton.Left, mTabRadioButton.Bottom + mEmHalf);
         mSpaceUpDown.Location = new Point(mSpaceRadioButton.Right + mEm, mSpaceRadioButton.Top);
         mSpaceSuffixUpDownLabel.Location = new Point(mSpaceUpDown.Right + mEmHalf, mSpaceRadioButton.Top);
         mSpaceCheckBox.Location = new Point(mSpaceSuffixUpDownLabel.Right + mEm, mSpaceRadioButton.Top);
         SizeGroupBox(mWhitespaceGroupBox);
         mOptionsBottomPanel.LayoutControls();
         Size wantedSize = new Size(Math.Max(mClusterGroupBox.Right, mWhitespaceGroupBox.Right) +
            mEm + SystemInformation.VerticalScrollBarWidth,
            mTitleLabel.Height + mWhitespaceGroupBox.Height + mClusterGroupBox.Height +
            mOptionsBottomPanel.Height + mEm3 + SystemInformation.HorizontalScrollBarHeight);
         Screen screen = Screen.FromPoint(mUiState.FormBounds.Location) as Screen;
         Point location = new Point(((screen.WorkingArea.Width - wantedSize.Width) / 2) + screen.WorkingArea.Left,
            ((screen.WorkingArea.Height - wantedSize.Height) / 2) + screen.WorkingArea.Top);
         mUiState.mOptionsBounds = new Rectangle(location, wantedSize);
      }

      private void Tab_Click(object? pSender, EventArgs pEventArguments) {
         ThrowIfNull(mTabUpDown, nameof(mTabUpDown));
         mTabUpDown.Focus();
         mTabUpDown.Select();
      }

      private void Space_Click(object? pSender, EventArgs pEventArguments) {
         ThrowIfNull(mSpaceUpDown, nameof(mSpaceUpDown));
         mSpaceUpDown.Focus();
         mSpaceUpDown.Select();
      }

      private void OKButton_Click(object? pSender, EventArgs pEventArguments) {
         ThrowIfNull(mUiState, nameof(mUiState));
         ThrowIfNull(mTopDraggerHeightUpDownCluster, nameof(mTopDraggerHeightUpDownCluster));
         ThrowIfNull(mTopDraggerEdgeUpDownCluster, nameof(mTopDraggerEdgeUpDownCluster));
         ThrowIfNull(mActivationDelayUpDownCluster, nameof(mActivationDelayUpDownCluster));
         ThrowIfNull(mClipboardDelayUpDownCluster, nameof(mClipboardDelayUpDownCluster));
         ThrowIfNull(mReactivationRateUpDownCluster, nameof(mReactivationRateUpDownCluster));
         ThrowIfNull(mTopDraggerHeightUpDownCluster.mNumericUpDown, nameof(mTopDraggerHeightUpDownCluster.mNumericUpDown));
         ThrowIfNull(mTopDraggerEdgeUpDownCluster.mNumericUpDown, nameof(mTopDraggerEdgeUpDownCluster.mNumericUpDown));
         ThrowIfNull(mActivationDelayUpDownCluster.mNumericUpDown, nameof(mActivationDelayUpDownCluster.mNumericUpDown));
         ThrowIfNull(mClipboardDelayUpDownCluster.mNumericUpDown, nameof(mClipboardDelayUpDownCluster.mNumericUpDown));
         ThrowIfNull(mReactivationRateUpDownCluster.mNumericUpDown, nameof(mReactivationRateUpDownCluster.mNumericUpDown));
         ThrowIfNull(mTabCheckBox, nameof(mTabCheckBox));
         ThrowIfNull(mSpaceCheckBox, nameof(mSpaceCheckBox));
         ThrowIfNull(mActivationDelayUpDownCluster.mNumericUpDown, nameof(mActivationDelayUpDownCluster.mNumericUpDown));
         ThrowIfNull(mTabUpDown, nameof(mTabUpDown));
         ThrowIfNull(mSpaceUpDown, nameof(mSpaceUpDown));
         mUiState.mTopDraggerHeight = (int)mTopDraggerHeightUpDownCluster.mNumericUpDown.Value;
         mUiState.mTopDraggerEdge = (int)mTopDraggerEdgeUpDownCluster.mNumericUpDown.Value;
         mUiState.mActivationDelayMs = (int)mActivationDelayUpDownCluster.mNumericUpDown.Value;
         mUiState.mClipboardDelayMs = (int)mClipboardDelayUpDownCluster.mNumericUpDown.Value;
         mUiState.mReactivationDelayMs = (int)mReactivationRateUpDownCluster.mNumericUpDown.Value;
         mUiState.mUseTabs = mTabCheckBox.Checked;
         mUiState.mUseSpaces = mSpaceCheckBox.Checked;
         mUiState.mWhitespace = mTabCheckBox.Checked && mSpaceCheckBox.Checked ? (int)Whitespace.Both :
            mTabCheckBox.Checked ? (int)Whitespace.Tabs : (int)Whitespace.Spaces;
         mUiState.mSpacesPerTab = (int)mTabUpDown.Value;
         mUiState.mSpacesToBecomeTab = (int)mSpaceUpDown.Value;
         CloseThemePickerPanel();
      }

      private void CancelButton_Click(object? pSender, EventArgs pEventArguments) =>
         CloseThemePickerPanel();

      private void CloseThemePickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         mForm.RestoreFromOptionsPanel();
      }
   }
}
