using PCRE;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class FindPanel : DraggablePanel {
         private bool mHasControlBox;
         private readonly BottomPanel mFindBottomPanel;
         private readonly Button mFindButton;
         private readonly ClusterContainer mOptionsClusterContainer, mScopeClusterContainer;
         private readonly ComboBoxCluster mFindWhatCluster;
         private readonly HeaderLabelCluster mOptionsHeaderCluster, mScopeHeaderCluster, mTitleCluster;
         private readonly Panel mInnerPanel;
         private readonly ScalableCheckBoxCluster mMatchCaseCluster, mRegularExpressionsCluster,
            mUsePcreCluster, mVerboseCluster, mWholeWordCluster;
         private readonly ScalableRadioButtonCluster mScopeCluster;
         private readonly ScrollablePanel mOuterPanel;

         public FindPanel() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mHasControlBox = mForm.ControlBox;
            if (mHasControlBox)
               mForm.ControlBox = false;
            mOuterPanel = new ScrollablePanel {
               Name = $"Find_OuterPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Location = new Point(mEmHalf, mEm),
               Dock = DockStyle.None
            };
            mInnerPanel = new Panel {
               Name = $"Find_InnerPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Location = new Point(mEmHalf, mEmHalf),
               AutoScroll = false
            };
            mTitleCluster = new HeaderLabelCluster(mCurrentTheme, "Find", HeaderLabelSize.Normal);
            Color groupBoxBackground = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
            mFindWhatCluster = new ComboBoxCluster(mCurrentTheme, "&Find What", groupBoxBackground);
            mOptionsHeaderCluster = new HeaderLabelCluster(mCurrentTheme, "Options", HeaderLabelSize.Small);
            mMatchCaseCluster = new ScalableCheckBoxCluster(mCurrentTheme!, "&Match Case", false, groupBoxBackground);
            mWholeWordCluster = new ScalableCheckBoxCluster(mCurrentTheme!, "&Whole Word", false, groupBoxBackground);
            mRegularExpressionsCluster = new ScalableCheckBoxCluster(mCurrentTheme!, "&Regular Expressions", false,
               groupBoxBackground);
            mVerboseCluster = new ScalableCheckBoxCluster(mCurrentTheme!, "&Verbose", false, groupBoxBackground);
            mUsePcreCluster = new ScalableCheckBoxCluster(mCurrentTheme!, "Use &PCRE", false, groupBoxBackground);
            List<BaseCluster> optionClusters = [mOptionsHeaderCluster, mMatchCaseCluster,
               mWholeWordCluster, mRegularExpressionsCluster, mVerboseCluster, mUsePcreCluster];
            mOptionsClusterContainer = new ClusterContainer(mInnerPanel, optionClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 1) {
               BackColor = groupBoxBackground
            };
            mScopeHeaderCluster = new HeaderLabelCluster(mCurrentTheme, "Scope", HeaderLabelSize.Small);
            List<ScalableRadioButtons.RadioButtonQuad> scopeQuads = [
               new ScalableRadioButtons.RadioButtonQuad("&Selection", "Selection", false, 0),
               new ScalableRadioButtons.RadioButtonQuad("&Global", "Global", true, 1)
            ];
            mScopeCluster = new ScalableRadioButtonCluster(mCurrentTheme!, scopeQuads, false, false, 0,
               groupBoxBackground);
            List<BaseCluster> scopeClusters = [mScopeHeaderCluster, mScopeCluster];
            mScopeClusterContainer = new ClusterContainer(mInnerPanel, scopeClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 1) {
               BackColor = groupBoxBackground
            };
            mFindButton = new Button {
               Name = $"Find_FindButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Find",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mFindBottomPanel = new BottomPanel(mCurrentTheme, "&Cancel");
            ThrowIfNull(mFindBottomPanel.mHelpButton, nameof(mFindBottomPanel.mHelpButton));
            ThrowIfNull(mFindBottomPanel.mCancelButton, nameof(mFindBottomPanel.mCancelButton));
            mFindBottomPanel.mHelpButton.Tag = new HelpTag(HelpContext.Main, "Find");
            mFindBottomPanel.AddRightControl(mFindButton);
            mFindBottomPanel.mCancelButton.Click += CancelButton_Click;
            mFindButton.Click += FindButton_Click;
            ThrowIfNull(mFindWhatCluster.mComboBox, nameof(mFindWhatCluster.mComboBox));
            mFindWhatCluster.mComboBox.KeyDown += FindComboBox_KeyDown;
            mUsePcreCluster.mScalableCheckBox.Click += UsePcreCheckBox_Click;
            mInnerPanel.Controls.AddRange([mFindBottomPanel, mOptionsClusterContainer, mScopeClusterContainer,
               mFindWhatCluster, mTitleCluster]);
            mOuterPanel.Controls.Add(mInnerPanel);
            Controls.Add(mOuterPanel);
         }

         protected override void OnHandleCreated(EventArgs pEventArgs) {
            base.OnHandleCreated(pEventArgs);
            LayoutClusters();
            mFindBottomPanel.LayoutControls();
            mOptionsHeaderCluster.LayoutCluster();
            mScopeHeaderCluster.LayoutCluster();
            mTitleCluster.LayoutCluster();
         }

         public static void ShowMe() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
            if (mForm.Controls.Count != 1 || mForm.Controls[0] is not ScrollablePanel)
               throw new InvalidOperationException(
                  "FindPanel.ShowMe: Form must have exactly one direct child control and it must be a ScrollablePanel.");
            mUiState.FormBounds = mForm.Bounds;
            mFindPanel = new FindPanel();
            mFindPanel.mMatchCaseCluster.mScalableCheckBox.Checked = mUiState.mFindMatchCase;
            mFindPanel.mWholeWordCluster.mScalableCheckBox.Checked = mUiState.mFindWholeWord;
            mFindPanel.mRegularExpressionsCluster.mScalableCheckBox.Checked = mUiState.mFindRegularExpressions;
            mFindPanel.mUsePcreCluster.mScalableCheckBox.Checked = mUsePCRE;
            if (mUiState.mFindScopeSelection)
               mFindPanel.mScopeCluster.mRadioPanel.SetCheckedByTag("Selection");
            mFindPanel.PopulateHistory();
            List<string> findHistory = mUsePCRE ? mUiState.mFindPcreSearchHistory : mUiState.mFindSearchHistory;
            if (mRichTextBox.SelectionLength > 0)
               mFindPanel.SetSearchText(mRichTextBox.SelectedText);
            else if (findHistory.Count > 0)
               mFindPanel.SetSearchText(findHistory[0]);
            // Clipboard is not consulted: selection is explicit intent; clipboard is contextually ambiguous.
            mFindPanel.CreateControl();
            if (mForm.ClientSize.Width < mFindPanel.Width + mEm2 * 2 || mForm.ClientSize.Height < mFindPanel.Height + mEm2 * 2)
               mForm.ClientSize = new Size(
                  Math.Max(mForm.ClientSize.Width, mFindPanel.Width + mEm2 * 2),
                  Math.Max(mForm.ClientSize.Height, mFindPanel.Height + mEm2 * 2));
            mFindPanel.Attach(mForm);
            mFindPanel.FocusComboBox();
         }

         public static void Restore() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mFindPanel, nameof(mFindPanel));
            ThrowIfNull(mActiveLayoutable, nameof(mActiveLayoutable));
            mUiState.mFindMatchCase = mFindPanel.mMatchCaseCluster.mScalableCheckBox.Checked;
            mUiState.mFindWholeWord = mFindPanel.mWholeWordCluster.mScalableCheckBox.Checked;
            mUiState.mFindRegularExpressions = mFindPanel.mRegularExpressionsCluster.mScalableCheckBox.Checked;
            mUiState.mFindScopeSelection = mFindPanel.mScopeCluster.mRadioPanel.GetChecked()?.Tag?.ToString() == "Selection";
            mUsePCRE = mFindPanel.mUsePcreCluster.mScalableCheckBox.Checked;
            mUiState.mUsePCRE = mUsePCRE;
            bool hadControlBox = mFindPanel.mHasControlBox;
            mFindPanel.Detach();
            mFindPanel.Dispose();
            mFindPanel = null;
            if (mForm.Size != mUiState.FormBounds.Size)
               mForm.Bounds = mUiState.FormBounds;
            if (hadControlBox)
               mForm.ControlBox = true;
            mActiveLayoutable.LayoutControls();
         }

         private void LayoutClusters() {
            int titleHeight, findWhatHeight, groupAreaTop, groupAreaHeight,
               optionsContainerWidth, optionsContainerHeight,
               scopeContainerWidth, scopeContainerHeight,
               panelWidth, panelHeight;

            SuspendLayout();
            mInnerPanel.SuspendLayout();
            ApplyTheme();

            mTitleCluster.LayoutCluster();
            titleHeight = mTitleCluster.Height;
            mFindWhatCluster.Location = new Point(0, titleHeight);
            mFindWhatCluster.LayoutCluster();
            findWhatHeight = mFindWhatCluster.Height;

            mOptionsHeaderCluster.LayoutCluster();
            mMatchCaseCluster.LayoutCluster();
            mWholeWordCluster.LayoutCluster();
            mRegularExpressionsCluster.LayoutCluster();
            mVerboseCluster.LayoutCluster();
            mUsePcreCluster.LayoutCluster();
            mOptionsClusterContainer.LayoutClusters();

            mScopeHeaderCluster.LayoutCluster();
            mScopeCluster.LayoutCluster();
            mScopeClusterContainer.LayoutClusters();

            groupAreaTop = titleHeight + findWhatHeight + mEm;
            optionsContainerWidth = mIndent + Math.Max(mMatchCaseCluster.Width,
               Math.Max(mWholeWordCluster.Width,
               Math.Max(mRegularExpressionsCluster.Width,
               Math.Max(mVerboseCluster.Width, mUsePcreCluster.Width))));
            optionsContainerHeight = mOptionsHeaderCluster.Height + mMatchCaseCluster.Height
               + mWholeWordCluster.Height + mRegularExpressionsCluster.Height + mVerboseCluster.Height
               + mUsePcreCluster.Height;
            scopeContainerWidth = mIndent + mScopeCluster.Width;
            scopeContainerHeight = mScopeHeaderCluster.Height + mScopeCluster.Height + mBottomPad;
            groupAreaHeight = Math.Max(optionsContainerHeight, scopeContainerHeight);
            panelWidth = Math.Max(mTitleCluster.Width, mFindWhatCluster.Width);
            panelWidth = Math.Max(panelWidth, mIndent + optionsContainerWidth + mEm + scopeContainerWidth);
            panelWidth = Math.Max(panelWidth, mFindBottomPanel.NeededWidth);
            panelHeight = groupAreaTop + groupAreaHeight + mEm + mFindBottomPanel.Height;

            mOptionsClusterContainer.Location = new Point(mIndent, groupAreaTop);
            mScopeClusterContainer.Location = new Point(mIndent + optionsContainerWidth + mEm, groupAreaTop);

            mInnerPanel.Size = new Size(panelWidth + mEm, panelHeight);
            mOuterPanel.Size = new Size(mInnerPanel.Width + (mEmHalf * 2), mInnerPanel.Height + (mEmHalf * 2));
            Size = new Size(mOuterPanel.Width + (mEmHalf * 2), mOuterPanel.Height + mEm + mEmHalf);
            mInnerPanel.ResumeLayout(true);
            ResumeLayout(true);
         }

         protected override void ApplyDragTone(ColorTones pTone) {
            bool isDarkBackground = pTone == ColorTones.Dark || pTone == ColorTones.MediumDark;
            BackColor = isDarkBackground ? Color.White : Color.Black;
            ColorTones outerTone = ColorTone.GetTone(BackColor);
            bool isOuterDark = outerTone == ColorTones.Dark || outerTone == ColorTones.MediumDark;
            mOuterPanel.BackColor = isOuterDark ? Color.White : Color.Black;
         }

         private void ApplyTheme() {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mInnerPanel.BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.PanelBackground];
         }

         private bool FindAndSelect(string pSearchText) {
            ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
            string pattern = mRegularExpressionsCluster.mScalableCheckBox.Checked
               ? pSearchText
               : Regex.Escape(pSearchText);
            if (mWholeWordCluster.mScalableCheckBox.Checked)
               pattern = @"\b" + pattern + @"\b";
            if (mUsePCRE) {
               PcreOptions pcreOptions = mMatchCaseCluster.mScalableCheckBox.Checked
                  ? PcreOptions.None
                  : PcreOptions.IgnoreCase;
               PcreRegex pcreRegex = new PcreRegex(pattern, pcreOptions);
               List<PcreMatch> pcreMatches = pcreRegex.Matches(mRichTextBox.Text).ToList();
               if (pcreMatches.Count == 0)
                  return false;
               if (mScopeCluster.mRadioPanel.GetChecked()?.Tag?.ToString() == "Selection") {
                  int selectionStart = mRichTextBox.SelectionStart;
                  for (int index = 0; index < pcreMatches.Count; index++) {
                     if (pcreMatches[index].Index >= selectionStart) {
                        mCurrentFindRecord = new FindRecord(pSearchText, pcreMatches, pcreRegex) {
                           mPosition = index
                        };
                        mRichTextBox.SelectionStart = pcreMatches[index].Index;
                        mRichTextBox.SelectionLength = pcreMatches[index].Length;
                        return true;
                     }
                  }
                  return false;
               }
               mCurrentFindRecord = new FindRecord(pSearchText, pcreMatches, pcreRegex) {
                  mPosition = 0
               };
               mRichTextBox.SelectionStart = pcreMatches[0].Index;
               mRichTextBox.SelectionLength = pcreMatches[0].Length;
               return true;
            }
            RegexOptions regexOptions = mMatchCaseCluster.mScalableCheckBox.Checked
               ? RegexOptions.None
               : RegexOptions.IgnoreCase;
            MatchCollection matches = Regex.Matches(mRichTextBox.Text, pattern, regexOptions);
            if (matches.Count == 0)
               return false;
            if (mScopeCluster.mRadioPanel.GetChecked()?.Tag?.ToString() == "Selection") {
               int selectionStart = mRichTextBox.SelectionStart;
               for (int index = 0; index < matches.Count; index++) {
                  if (matches[index].Index >= selectionStart) {
                     mCurrentFindRecord = new FindRecord(pSearchText, matches) {
                        mPosition = index
                     };
                     mRichTextBox.SelectionStart = matches[index].Index;
                     mRichTextBox.SelectionLength = matches[index].Length;
                     return true;
                  }
               }
               return false;
            }
            mCurrentFindRecord = new FindRecord(pSearchText, matches) {
               mPosition = 0
            };
            mRichTextBox.SelectionStart = matches[0].Index;
            mRichTextBox.SelectionLength = matches[0].Length;
            return true;
         }

         private static void UpdateFindHistory(string pSearchText) {
            List<string> history = mUsePCRE ? mUiState.mFindPcreSearchHistory : mUiState.mFindSearchHistory;
            history.Remove(pSearchText);
            history.Insert(0, pSearchText);
            while (history.Count > mUiState.mSearchHistoryMaxEntries)
               history.RemoveAt(history.Count - 1);
         }

         internal void PopulateHistory() {
            ThrowIfNull(mFindWhatCluster.mComboBox, nameof(mFindWhatCluster.mComboBox));
            mFindWhatCluster.mComboBox.Items.Clear();
            List<string> history = mUsePCRE ? mUiState.mFindPcreSearchHistory : mUiState.mFindSearchHistory;
            foreach (string item in history)
               mFindWhatCluster.mComboBox.Items.Add(item);
         }

         internal void SetSearchText(string pText) {
            ThrowIfNull(mFindWhatCluster.mComboBox, nameof(mFindWhatCluster.mComboBox));
            mFindWhatCluster.mComboBox.Text = pText;
         }

         internal void FocusComboBox() {
            ThrowIfNull(mFindWhatCluster.mComboBox, nameof(mFindWhatCluster.mComboBox));
            ComboBoxSelectAll(mFindWhatCluster.mComboBox);
         }

         private void FindButton_Click(object? pSender, EventArgs pEventArguments) {
            ThrowIfNull(mFindWhatCluster.mComboBox, nameof(mFindWhatCluster.mComboBox));
            ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
            string searchText = mFindWhatCluster.mComboBox.Text;
            if (string.IsNullOrEmpty(searchText))
               return;
            if (FindAndSelect(searchText)) {
               bool isVerbose = mVerboseCluster.mScalableCheckBox.Checked;
               UpdateFindHistory(searchText);
               Restore();
               ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
               mRichTextBox.ScrollToCaret();
               if (isVerbose && mCurrentFindRecord != null)
                  TimedMessage($"\"{searchText}\" was found {mCurrentFindRecord.Count} time(s).");
            }
            else {
               TimedMessage($"The text \"{searchText}\" could not be found.", "Warning", 3000);
               ComboBoxSelectAll(mFindWhatCluster.mComboBox);
            }
         }

         private void CancelButton_Click(object? pSender, EventArgs pEventArguments) {
            Restore();
         }

         private void UsePcreCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            mUsePCRE = mUsePcreCluster.mScalableCheckBox.Checked;
            PopulateHistory();
         }

         private void FindComboBox_KeyDown(object? pSender, KeyEventArgs pEventArguments) {
            ThrowIfNull(mFindWhatCluster.mComboBox, nameof(mFindWhatCluster.mComboBox));
            if (pEventArguments.KeyCode == Keys.Enter) {
               pEventArguments.Handled = true;
               pEventArguments.SuppressKeyPress = true;
               FindButton_Click(pSender, pEventArguments);
            }
            else if (pEventArguments.KeyCode == Keys.Escape) {
               pEventArguments.Handled = true;
               pEventArguments.SuppressKeyPress = true;
               if (mFindWhatCluster.mComboBox.DroppedDown)
                  mFindWhatCluster.mComboBox.DroppedDown = false;
               else
                  mMatchCaseCluster.mScalableCheckBox.Focus();
            }
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               mFindButton.Click -= FindButton_Click;
               ThrowIfNull(mFindBottomPanel.mCancelButton, nameof(mFindBottomPanel.mCancelButton));
               mFindBottomPanel.mCancelButton.Click -= CancelButton_Click;
               ThrowIfNull(mFindWhatCluster.mComboBox, nameof(mFindWhatCluster.mComboBox));
               mFindWhatCluster.mComboBox.KeyDown -= FindComboBox_KeyDown;
               mUsePcreCluster.mScalableCheckBox.Click -= UsePcreCheckBox_Click;
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
