using PCRE;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class SearchReplacePanel : DraggablePanel {
         private bool mHasControlBox;
         private readonly BottomPanel mSearchReplaceBottomPanel;
         private readonly Button mReplaceAllButton, mReplaceButton;
         private readonly ClusterContainer mOptionsClusterContainer, mScopeClusterContainer;
         private readonly ComboBoxCluster mReplaceWithCluster, mSearchForCluster;
         private readonly HeaderLabelCluster mOptionsHeaderCluster, mScopeHeaderCluster, mTitleCluster;
         private readonly Panel mInnerPanel;
         private readonly ScalableCheckBoxCluster mMatchCaseCluster, mRegularExpressionsCluster,
            mUsePcreCluster, mWholeWordCluster;
         private readonly ScalableRadioButtonCluster mScopeCluster;
         private readonly ScrollablePanel mOuterPanel;

         public SearchReplacePanel() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mHasControlBox = mForm.ControlBox;
            if (mHasControlBox)
               mForm.ControlBox = false;
            mOuterPanel = new ScrollablePanel {
               Name = $"SearchReplace_OuterPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Location = new Point(mEmHalf, mEm),
               Dock = DockStyle.None
            };
            mInnerPanel = new Panel {
               Name = $"SearchReplace_InnerPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Location = new Point(mEmHalf, mEmHalf),
               AutoScroll = false
            };
            mTitleCluster = new HeaderLabelCluster(mCurrentTheme, "Search And Replace", HeaderLabelSize.Normal);
            Color groupBoxBackground = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
            mSearchForCluster = new ComboBoxCluster(mCurrentTheme, "Search &For", groupBoxBackground);
            mReplaceWithCluster = new ComboBoxCluster(mCurrentTheme, "R&eplace With", groupBoxBackground);
            mOptionsHeaderCluster = new HeaderLabelCluster(mCurrentTheme, "Options", HeaderLabelSize.Small);
            mMatchCaseCluster = new ScalableCheckBoxCluster(mCurrentTheme!, "&Match Case", false, groupBoxBackground);
            mWholeWordCluster = new ScalableCheckBoxCluster(mCurrentTheme!, "&Whole Word", false, groupBoxBackground);
            mRegularExpressionsCluster = new ScalableCheckBoxCluster(mCurrentTheme!, "&Regular Expressions", false,
               groupBoxBackground);
            mUsePcreCluster = new ScalableCheckBoxCluster(mCurrentTheme!, "Use &PCRE", false, groupBoxBackground);
            List<BaseCluster> optionClusters = [mOptionsHeaderCluster, mMatchCaseCluster,
               mWholeWordCluster, mRegularExpressionsCluster, mUsePcreCluster];
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
            mReplaceButton = new Button {
               Name = $"SearchReplace_ReplaceButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "&Replace",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mReplaceAllButton = new Button {
               Name = $"SearchReplace_ReplaceAllButton{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = "Replace &All",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mSearchReplaceBottomPanel = new BottomPanel(mCurrentTheme, "&Cancel");
            ThrowIfNull(mSearchReplaceBottomPanel.mHelpButton, nameof(mSearchReplaceBottomPanel.mHelpButton));
            ThrowIfNull(mSearchReplaceBottomPanel.mCancelButton, nameof(mSearchReplaceBottomPanel.mCancelButton));
            mSearchReplaceBottomPanel.mHelpButton.Tag = new HelpTag(HelpContext.Main, "SearchReplace");
            // Replace sits next to Cancel; Replace All sits left of Replace.
            mSearchReplaceBottomPanel.AddRightControl(mReplaceButton);
            mSearchReplaceBottomPanel.AddRightControl(mReplaceAllButton);
            mSearchReplaceBottomPanel.mCancelButton.Click += CancelButton_Click;
            mReplaceButton.Click += ReplaceButton_Click;
            mReplaceAllButton.Click += ReplaceAllButton_Click;
            ThrowIfNull(mSearchForCluster.mComboBox, nameof(mSearchForCluster.mComboBox));
            mSearchForCluster.mComboBox.KeyDown += SearchComboBox_KeyDown;
            ThrowIfNull(mReplaceWithCluster.mComboBox, nameof(mReplaceWithCluster.mComboBox));
            mReplaceWithCluster.mComboBox.KeyDown += ReplaceComboBox_KeyDown;
            mUsePcreCluster.mScalableCheckBox.Click += UsePcreCheckBox_Click;
            mInnerPanel.Controls.AddRange([mSearchReplaceBottomPanel, mOptionsClusterContainer,
               mScopeClusterContainer, mReplaceWithCluster, mSearchForCluster, mTitleCluster]);
            mOuterPanel.Controls.Add(mInnerPanel);
            Controls.Add(mOuterPanel);
         }

         protected override void OnHandleCreated(EventArgs pEventArgs) {
            base.OnHandleCreated(pEventArgs);
            LayoutClusters();
            mSearchReplaceBottomPanel.LayoutControls();
            mOptionsHeaderCluster.LayoutCluster();
            mScopeHeaderCluster.LayoutCluster();
            mTitleCluster.LayoutCluster();
         }

         public static void ShowMe() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
            if (mForm.Controls.Count != 1 || mForm.Controls[0] is not ScrollablePanel)
               throw new InvalidOperationException(
                  "SearchReplacePanel.ShowMe: Form must have exactly one direct child control and it must be a ScrollablePanel.");
            mUiState.FormBounds = mForm.Bounds;
            mSearchReplacePanel = new SearchReplacePanel();
            mSearchReplacePanel.mMatchCaseCluster.mScalableCheckBox.Checked = mUiState.mSRMatchCase;
            mSearchReplacePanel.mWholeWordCluster.mScalableCheckBox.Checked = mUiState.mSRWholeWord;
            mSearchReplacePanel.mRegularExpressionsCluster.mScalableCheckBox.Checked = mUiState.mSRRegularExpressions;
            mSearchReplacePanel.mUsePcreCluster.mScalableCheckBox.Checked = mUsePCRE;
            if (mUiState.mSRScopeSelection)
               mSearchReplacePanel.mScopeCluster.mRadioPanel.SetCheckedByTag("Selection");
            mSearchReplacePanel.PopulateHistories();
            List<string> searchHistory = mUsePCRE ? mUiState.mSrPcreSearchHistory : mUiState.mSrSearchHistory;
            List<string> replaceHistory = mUsePCRE ? mUiState.mSrPcreReplaceHistory : mUiState.mSrReplaceHistory;
            bool searchFromSelection = mRichTextBox.SelectionLength > 0;
            string searchText = searchFromSelection
               ? mRichTextBox.SelectedText
               : (searchHistory.Count > 0 ? searchHistory[0] : string.Empty);
            mSearchReplacePanel.SetSearchText(searchText);
            string replaceText;
            if (!searchFromSelection && replaceHistory.Count > 0)
               replaceText = replaceHistory[0];
            else if (!string.IsNullOrEmpty(searchText))
               replaceText = ClipboardReplaceCandidate(searchText);
            else
               replaceText = string.Empty;
            if (!string.IsNullOrEmpty(replaceText))
               mSearchReplacePanel.SetReplaceText(replaceText);
            mSearchReplacePanel.CreateControl();
            if (mForm.ClientSize.Width < mSearchReplacePanel.Width + mEm2 * 2 ||
               mForm.ClientSize.Height < mSearchReplacePanel.Height + mEm2 * 2)
               mForm.ClientSize = new Size(
                  Math.Max(mForm.ClientSize.Width, mSearchReplacePanel.Width + mEm2 * 2),
                  Math.Max(mForm.ClientSize.Height, mSearchReplacePanel.Height + mEm2 * 2));
            mSearchReplacePanel.Attach(mForm);
            mSearchReplacePanel.FocusSearchComboBox();
         }

         public static void Restore() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mSearchReplacePanel, nameof(mSearchReplacePanel));
            ThrowIfNull(mActiveLayoutable, nameof(mActiveLayoutable));
            mUiState.mSRMatchCase = mSearchReplacePanel.mMatchCaseCluster.mScalableCheckBox.Checked;
            mUiState.mSRWholeWord = mSearchReplacePanel.mWholeWordCluster.mScalableCheckBox.Checked;
            mUiState.mSRRegularExpressions = mSearchReplacePanel.mRegularExpressionsCluster.mScalableCheckBox.Checked;
            mUiState.mSRScopeSelection = mSearchReplacePanel.mScopeCluster.mRadioPanel.GetChecked()?.Tag?.ToString() == "Selection";
            mUsePCRE = mSearchReplacePanel.mUsePcreCluster.mScalableCheckBox.Checked;
            mUiState.mUsePCRE = mUsePCRE;
            bool hadControlBox = mSearchReplacePanel.mHasControlBox;
            mSearchReplacePanel.Detach();
            mSearchReplacePanel.Dispose();
            mSearchReplacePanel = null;
            if (mForm.Size != mUiState.FormBounds.Size)
               mForm.Bounds = mUiState.FormBounds;
            if (hadControlBox)
               mForm.ControlBox = true;
            mActiveLayoutable.LayoutControls();
         }

         private void LayoutClusters() {
            int titleHeight, searchForHeight, replaceWithHeight, groupAreaTop, groupAreaHeight,
               optionsContainerWidth, optionsContainerHeight,
               scopeContainerWidth, scopeContainerHeight,
               panelWidth, panelHeight;

            SuspendLayout();
            mInnerPanel.SuspendLayout();
            ApplyTheme();

            mTitleCluster.LayoutCluster();
            titleHeight = mTitleCluster.Height;
            mSearchForCluster.Location = new Point(0, titleHeight);
            mSearchForCluster.LayoutCluster();
            searchForHeight = mSearchForCluster.Height;
            mReplaceWithCluster.Location = new Point(0, titleHeight + searchForHeight);
            mReplaceWithCluster.LayoutCluster();
            replaceWithHeight = mReplaceWithCluster.Height;

            mOptionsHeaderCluster.LayoutCluster();
            mMatchCaseCluster.LayoutCluster();
            mWholeWordCluster.LayoutCluster();
            mRegularExpressionsCluster.LayoutCluster();
            mUsePcreCluster.LayoutCluster();
            mOptionsClusterContainer.LayoutClusters();

            mScopeHeaderCluster.LayoutCluster();
            mScopeCluster.LayoutCluster();
            mScopeClusterContainer.LayoutClusters();

            groupAreaTop = titleHeight + searchForHeight + replaceWithHeight + mEm;
            optionsContainerWidth = mIndent + Math.Max(mMatchCaseCluster.Width,
               Math.Max(mWholeWordCluster.Width,
               Math.Max(mRegularExpressionsCluster.Width, mUsePcreCluster.Width)));
            optionsContainerHeight = mOptionsHeaderCluster.Height + mMatchCaseCluster.Height
               + mWholeWordCluster.Height + mRegularExpressionsCluster.Height + mUsePcreCluster.Height;
            scopeContainerWidth = mIndent + mScopeCluster.Width;
            scopeContainerHeight = mScopeHeaderCluster.Height + mScopeCluster.Height + mBottomPad;
            groupAreaHeight = Math.Max(optionsContainerHeight, scopeContainerHeight);
            panelWidth = Math.Max(mTitleCluster.Width, mSearchForCluster.Width);
            panelWidth = Math.Max(panelWidth, mReplaceWithCluster.Width);
            panelWidth = Math.Max(panelWidth, mIndent + optionsContainerWidth + mEm + scopeContainerWidth);
            panelWidth = Math.Max(panelWidth, mSearchReplaceBottomPanel.NeededWidth);
            panelHeight = groupAreaTop + groupAreaHeight + mEm + mSearchReplaceBottomPanel.Height;

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

         // Scope radio buttons govern Replace All only. Sequential Replace always advances from
         // the current cursor position regardless of scope, so no wrapping occurs.
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
               int searchFrom = mRichTextBox.SelectionStart;
               for (int index = 0; index < pcreMatches.Count; index++) {
                  if (pcreMatches[index].Index >= searchFrom) {
                     mCurrentSearchRecord = new FindRecord(pSearchText, pcreMatches, pcreRegex) {
                        mPosition = index
                     };
                     mRichTextBox.SelectionStart = pcreMatches[index].Index;
                     mRichTextBox.SelectionLength = pcreMatches[index].Length;
                     mRichTextBox.ScrollToCaret();
                     return true;
                  }
               }
               return false;
            }
            RegexOptions regexOptions = mMatchCaseCluster.mScalableCheckBox.Checked
               ? RegexOptions.None
               : RegexOptions.IgnoreCase;
            MatchCollection matches = Regex.Matches(mRichTextBox.Text, pattern, regexOptions);
            if (matches.Count == 0)
               return false;
            int searchFromDotNet = mRichTextBox.SelectionStart;
            for (int index = 0; index < matches.Count; index++) {
               if (matches[index].Index >= searchFromDotNet) {
                  mCurrentSearchRecord = new FindRecord(pSearchText, matches) {
                     mPosition = index
                  };
                  mRichTextBox.SelectionStart = matches[index].Index;
                  mRichTextBox.SelectionLength = matches[index].Length;
                  mRichTextBox.ScrollToCaret();
                  return true;
               }
            }
            return false;
         }

         private void ReplaceCurrentAndFindNext() {
            ThrowIfNull(mSearchForCluster.mComboBox, nameof(mSearchForCluster.mComboBox));
            ThrowIfNull(mReplaceWithCluster.mComboBox, nameof(mReplaceWithCluster.mComboBox));
            ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
            ThrowIfNull(mCurrentSearchRecord, nameof(mCurrentSearchRecord));
            string searchText = mSearchForCluster.mComboBox.Text;
            string replaceText = mReplaceWithCluster.mComboBox.Text;
            if (string.IsNullOrEmpty(searchText))
               return;
            bool selectionIsCurrentMatch = mCurrentSearchRecord != null
               && mCurrentSearchRecord.mPosition < mCurrentSearchRecord.Count
               && mRichTextBox.SelectionStart == mCurrentSearchRecord.GetIndex(mCurrentSearchRecord.mPosition)
               && mRichTextBox.SelectionLength == mCurrentSearchRecord.GetLength(mCurrentSearchRecord.mPosition);
            if (selectionIsCurrentMatch) {
               int matchIndex = mCurrentSearchRecord!.GetIndex(mCurrentSearchRecord.mPosition);
               int matchLength = mCurrentSearchRecord.GetLength(mCurrentSearchRecord.mPosition);
               string replacedText;
               if (mCurrentSearchRecord.mPcreMatches != null) {
                  string matchText = mRichTextBox.Text.Substring(matchIndex, matchLength);
                  replacedText = mCurrentSearchRecord.mPcreRegex!.Replace(matchText, replaceText, 1);
               }
               else {
                  Match currentMatch = mCurrentSearchRecord.mMatches![mCurrentSearchRecord.mPosition];
                  replacedText = currentMatch.Result(replaceText);
               }
               mRichTextBox.SelectionStart = matchIndex;
               mRichTextBox.SelectionLength = matchLength;
               mRichTextBox.SelectedText = replacedText;
               mCurrentSearchRecord = null;
               UpdateSearchHistory(searchText);
               UpdateReplaceHistory(replaceText);
            }
            if (FindAndSelect(searchText))
               return;
            if (selectionIsCurrentMatch)
               TimedMessage($"Replaced. No more occurrences of \"{searchText}\".", "Replace");
            else
               TimedMessage($"The text \"{searchText}\" could not be found.", "Warning", 3000);
            ComboBoxSelectAll(mSearchForCluster.mComboBox);
         }

         private void ExecuteReplaceAll() {
            ThrowIfNull(mSearchForCluster.mComboBox, nameof(mSearchForCluster.mComboBox));
            ThrowIfNull(mReplaceWithCluster.mComboBox, nameof(mReplaceWithCluster.mComboBox));
            ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
            string searchText = mSearchForCluster.mComboBox.Text;
            string replaceText = mReplaceWithCluster.mComboBox.Text;
            if (string.IsNullOrEmpty(searchText))
               return;
            string pattern = mRegularExpressionsCluster.mScalableCheckBox.Checked
               ? searchText
               : Regex.Escape(searchText);
            if (mWholeWordCluster.mScalableCheckBox.Checked)
               pattern = @"\b" + pattern + @"\b";
            string originalText = mRichTextBox.Text;
            bool scopeIsSelection = mScopeCluster.mRadioPanel.GetChecked()?.Tag?.ToString() == "Selection";
            string scopeText = scopeIsSelection
               ? originalText.Substring(mRichTextBox.SelectionStart)
               : originalText;
            string newText;
            int replacementCount;
            if (mUsePCRE) {
               PcreOptions pcreOptions = mMatchCaseCluster.mScalableCheckBox.Checked
                  ? PcreOptions.None
                  : PcreOptions.IgnoreCase;
               PcreRegex pcreRegex = new PcreRegex(pattern, pcreOptions);
               List<PcreMatch> pcreMatches = pcreRegex.Matches(scopeText).ToList();
               if (pcreMatches.Count == 0) {
                  TimedMessage($"The text \"{searchText}\" could not be found.", "Warning", 3000);
                  ComboBoxSelectAll(mSearchForCluster.mComboBox);
                  return;
               }
               replacementCount = pcreMatches.Count;
               if (scopeIsSelection) {
                  int selectionStart = mRichTextBox.SelectionStart;
                  string before = originalText.Substring(0, selectionStart);
                  string after = originalText.Substring(selectionStart);
                  newText = before + pcreRegex.Replace(after, replaceText);
               }
               else {
                  newText = pcreRegex.Replace(originalText, replaceText);
               }
            }
            else {
               RegexOptions regexOptions = mMatchCaseCluster.mScalableCheckBox.Checked
                  ? RegexOptions.None
                  : RegexOptions.IgnoreCase;
               if (Regex.Matches(scopeText, pattern, regexOptions).Count == 0) {
                  TimedMessage($"The text \"{searchText}\" could not be found.", "Warning", 3000);
                  ComboBoxSelectAll(mSearchForCluster.mComboBox);
                  return;
               }
               replacementCount = Regex.Matches(scopeText, pattern, regexOptions).Count;
               if (scopeIsSelection) {
                  int selectionStart = mRichTextBox.SelectionStart;
                  string before = originalText.Substring(0, selectionStart);
                  string after = originalText.Substring(selectionStart);
                  newText = before + Regex.Replace(after, pattern, replaceText, regexOptions);
               }
               else {
                  newText = Regex.Replace(originalText, pattern, replaceText, regexOptions);
               }
            }
            UpdateSearchHistory(searchText);
            UpdateReplaceHistory(replaceText);
            mRichTextBox.Text = newText;
            mCurrentSearchRecord = null;
            Restore();
            TimedMessage($"\"{searchText}\" was replaced {replacementCount} time(s).", "Replace All");
         }

         private static string ClipboardReplaceCandidate(string pSearchText) {
            string? clipboardText = ClipboardHelper.TryGetClipboardText();
            if (string.IsNullOrWhiteSpace(clipboardText))
               return string.Empty;
            if (clipboardText == pSearchText)
               return string.Empty;
            if (clipboardText.Contains('\n'))
               return string.Empty;
            if (clipboardText.Length > pSearchText.Length * 3)
               return string.Empty;
            return clipboardText;
         }

         private static void UpdateSearchHistory(string pSearchText) {
            List<string> history = mUsePCRE ? mUiState.mSrPcreSearchHistory : mUiState.mSrSearchHistory;
            history.Remove(pSearchText);
            history.Insert(0, pSearchText);
            while (history.Count > mUiState.mSearchHistoryMaxEntries)
               history.RemoveAt(history.Count - 1);
         }

         private static void UpdateReplaceHistory(string pReplaceText) {
            List<string> history = mUsePCRE ? mUiState.mSrPcreReplaceHistory : mUiState.mSrReplaceHistory;
            history.Remove(pReplaceText);
            history.Insert(0, pReplaceText);
            while (history.Count > mUiState.mReplaceHistoryMaxEntries)
               history.RemoveAt(history.Count - 1);
         }

         internal void PopulateHistories() {
            ThrowIfNull(mSearchForCluster.mComboBox, nameof(mSearchForCluster.mComboBox));
            ThrowIfNull(mReplaceWithCluster.mComboBox, nameof(mReplaceWithCluster.mComboBox));
            List<string> searchHistory = mUsePCRE ? mUiState.mSrPcreSearchHistory : mUiState.mSrSearchHistory;
            List<string> replaceHistory = mUsePCRE ? mUiState.mSrPcreReplaceHistory : mUiState.mSrReplaceHistory;
            mSearchForCluster.mComboBox.Items.Clear();
            foreach (string item in searchHistory)
               mSearchForCluster.mComboBox.Items.Add(item);
            mReplaceWithCluster.mComboBox.Items.Clear();
            foreach (string item in replaceHistory)
               mReplaceWithCluster.mComboBox.Items.Add(item);
         }

         internal void SetSearchText(string pText) {
            ThrowIfNull(mSearchForCluster.mComboBox, nameof(mSearchForCluster.mComboBox));
            mSearchForCluster.mComboBox.Text = pText;
         }

         internal void SetReplaceText(string pText) {
            ThrowIfNull(mReplaceWithCluster.mComboBox, nameof(mReplaceWithCluster.mComboBox));
            mReplaceWithCluster.mComboBox.Text = pText;
         }

         internal void FocusSearchComboBox() {
            ThrowIfNull(mSearchForCluster.mComboBox, nameof(mSearchForCluster.mComboBox));
            ComboBoxSelectAll(mSearchForCluster.mComboBox);
         }

         private void ReplaceButton_Click(object? pSender, EventArgs pEventArguments) {
            ReplaceCurrentAndFindNext();
         }

         private void ReplaceAllButton_Click(object? pSender, EventArgs pEventArguments) {
            ExecuteReplaceAll();
         }

         private void CancelButton_Click(object? pSender, EventArgs pEventArguments) {
            Restore();
         }

         private void UsePcreCheckBox_Click(object? pSender, EventArgs pEventArguments) {
            mUsePCRE = mUsePcreCluster.mScalableCheckBox.Checked;
            PopulateHistories();
         }

         private void SearchComboBox_KeyDown(object? pSender, KeyEventArgs pEventArguments) {
            ThrowIfNull(mSearchForCluster.mComboBox, nameof(mSearchForCluster.mComboBox));
            if (pEventArguments.KeyCode == Keys.Enter) {
               pEventArguments.Handled = true;
               pEventArguments.SuppressKeyPress = true;
               ReplaceCurrentAndFindNext();
            }
            else if (pEventArguments.KeyCode == Keys.Escape) {
               pEventArguments.Handled = true;
               pEventArguments.SuppressKeyPress = true;
               if (mSearchForCluster.mComboBox.DroppedDown)
                  mSearchForCluster.mComboBox.DroppedDown = false;
               else
                  mMatchCaseCluster.mScalableCheckBox.Focus();
            }
         }

         private void ReplaceComboBox_KeyDown(object? pSender, KeyEventArgs pEventArguments) {
            ThrowIfNull(mReplaceWithCluster.mComboBox, nameof(mReplaceWithCluster.mComboBox));
            if (pEventArguments.KeyCode == Keys.Enter) {
               pEventArguments.Handled = true;
               pEventArguments.SuppressKeyPress = true;
               ReplaceCurrentAndFindNext();
            }
            else if (pEventArguments.KeyCode == Keys.Escape) {
               pEventArguments.Handled = true;
               pEventArguments.SuppressKeyPress = true;
               if (mReplaceWithCluster.mComboBox.DroppedDown)
                  mReplaceWithCluster.mComboBox.DroppedDown = false;
               else
                  mMatchCaseCluster.mScalableCheckBox.Focus();
            }
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               mReplaceButton.Click -= ReplaceButton_Click;
               mReplaceAllButton.Click -= ReplaceAllButton_Click;
               ThrowIfNull(mSearchReplaceBottomPanel.mCancelButton, nameof(mSearchReplaceBottomPanel.mCancelButton));
               mSearchReplaceBottomPanel.mCancelButton.Click -= CancelButton_Click;
               ThrowIfNull(mSearchForCluster.mComboBox, nameof(mSearchForCluster.mComboBox));
               mSearchForCluster.mComboBox.KeyDown -= SearchComboBox_KeyDown;
               ThrowIfNull(mReplaceWithCluster.mComboBox, nameof(mReplaceWithCluster.mComboBox));
               mReplaceWithCluster.mComboBox.KeyDown -= ReplaceComboBox_KeyDown;
               mUsePcreCluster.mScalableCheckBox.Click -= UsePcreCheckBox_Click;
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
