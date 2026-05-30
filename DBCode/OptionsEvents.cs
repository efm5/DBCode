using static DBCode.ScalableRadioButtons;

namespace DBCode {
   internal partial class OptionsPanel : Panel {
      protected override void OnHandleCreated(EventArgs pEventArgs) {
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
         ThrowIfNull(mBottomPanel, nameof(mBottomPanel));
         base.OnHandleCreated(pEventArgs);
         Dock = DockStyle.Fill;
         LayoutControls(false);
         GeneralTabControl_SelectedIndexChanged(null, EventArgs.Empty);
         if (mUiState.mShortcutsDgvAutoSize)
            mShortcutsDgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
      }
      private void GeneralTabControl_DrawItem(object? pSender, DrawItemEventArgs pArgs) {
         ThrowIfNull(mGeneralTabControl, nameof(mGeneralTabControl));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         MainForm.DrawTabControlItem(mGeneralTabControl, pArgs, mCurrentTheme);
      }

      private void IncludeExcludeTabControl_DrawItem(object? pSender, DrawItemEventArgs pArgs) {
         ThrowIfNull(mIncludeExcludeTabControl, nameof(mIncludeExcludeTabControl));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         MainForm.DrawTabControlItem(mIncludeExcludeTabControl, pArgs, mCurrentTheme);
      }

      private void CodingTabControl_DrawItem(object? pSender, DrawItemEventArgs pArgs) {
         ThrowIfNull(mCodingTabControl, nameof(mCodingTabControl));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         MainForm.DrawTabControlItem(mCodingTabControl, pArgs, mCurrentTheme);
      }

      private void GeneralTabControl_SelectedIndexChanged(object? pSender, EventArgs pArgs) {
         ThrowIfNull(mUiState, nameof(mUiState));
         ThrowIfNull(mGeneralTabControl, nameof(mGeneralTabControl));
         ThrowIfNull(mBottomPanel, nameof(mBottomPanel));
         ThrowIfNull(mShortcutsResetButton, nameof(mShortcutsResetButton));
         ThrowIfNull(mShortcutsAutoSizeCheckBox, nameof(mShortcutsAutoSizeCheckBox));
         ThrowIfNull(mShortcutsSortCheckBox, nameof(mShortcutsSortCheckBox));
         ThrowIfNull(mShortcutsAllCheckBox, nameof(mShortcutsAllCheckBox));
         mUiState.mOptionsGeneralTabControlPageIndex = mGeneralTabControl.SelectedIndex;
         bool onShortcuts = mGeneralTabControl.SelectedIndex == (int)OptionsTabPageUsage.Shortcuts;
         if (onShortcuts) {
            mBottomPanel.ShowLeftControl(mShortcutsResetButton);
            mBottomPanel.ShowLeftControl(mShortcutsAutoSizeCheckBox);
            mBottomPanel.ShowLeftControl(mShortcutsSortCheckBox);
            mBottomPanel.ShowLeftControl(mShortcutsAllCheckBox);
            mBottomPanel.LayoutControls();
         }
         else {
            mBottomPanel.HideLeftControl(mShortcutsResetButton);
            mBottomPanel.HideLeftControl(mShortcutsAutoSizeCheckBox);
            mBottomPanel.HideLeftControl(mShortcutsSortCheckBox);
            mBottomPanel.HideLeftControl(mShortcutsAllCheckBox);
            mBottomPanel.Refresh();
         }
         UpdateActiveScrollablePanel();
      }

      private void IncludeExcludeTabControl_SelectedIndexChanged(object? pSender, EventArgs pArgs) {
         ThrowIfNull(mUiState, nameof(mUiState));
         ThrowIfNull(mIncludeExcludeTabControl, nameof(mIncludeExcludeTabControl));
         mUiState.mOptionsIncludeExcludeTabControlPageIndex = mIncludeExcludeTabControl.SelectedIndex;
         UpdateActiveScrollablePanel();
      }

      private void CodingTabControl_SelectedIndexChanged(object? pSender, EventArgs pArgs) {
         ThrowIfNull(mUiState, nameof(mUiState));
         ThrowIfNull(mCodingTabControl, nameof(mCodingTabControl));
         mUiState.mOptionsCodingTabControlPageIndex = mCodingTabControl.SelectedIndex;
         UpdateActiveScrollablePanel();
      }

      internal void UpdateActiveScrollablePanel() {
         ThrowIfNull(mGeneralTabControl, nameof(mGeneralTabControl));
         ThrowIfNull(mIncludeExcludeTabControl, nameof(mIncludeExcludeTabControl));
         ThrowIfNull(mCodingTabControl, nameof(mCodingTabControl));
         mActiveScrollableDataGridView = null;
         int generalIndex = mGeneralTabControl.SelectedIndex;
         if (generalIndex == (int)OptionsTabPageUsage.General)
            mActiveScrollablePanel = mGeneralScrollPanel;
         else if (generalIndex == (int)OptionsTabPageUsage.BraceMatching)
            mActiveScrollablePanel = mBraceMatchingScrollPanel;
         else if (generalIndex == (int)OptionsTabPageUsage.Shortcuts) {
            mActiveScrollablePanel = null;
            mActiveScrollableDataGridView = mShortcutsDgv;
         }
         else if (generalIndex == (int)OptionsTabPageUsage.Targeting) {
            if (mIncludeExcludeTabControl.SelectedIndex == (int)TargetingTabPageUsage.Include)
               mActiveScrollablePanel = mIncludeScrollPanel;
            else
               mActiveScrollablePanel = mExcludeScrollPanel;
         }
         else
            mActiveScrollablePanel = mCodingScrollPanels[mCodingTabControl.SelectedIndex];
      }

      private void TabRadioButton_Click(object? pSender, EventArgs pEventArguments) {
         ThrowIfNull(mTabUpDownCluster, nameof(mTabUpDownCluster));
         ThrowIfNull(mTabUpDownCluster.mNumericUpDown, nameof(mTabUpDownCluster.mNumericUpDown));
         mTabUpDownCluster.mNumericUpDown.Focus();
         mTabUpDownCluster.mNumericUpDown.Select();
      }

      private void SpaceRadioButton_Click(object? pSender, EventArgs pEventArguments) {
         ThrowIfNull(mSpaceUpDownCluster, nameof(mSpaceUpDownCluster));
         ThrowIfNull(mSpaceUpDownCluster.mNumericUpDown, nameof(mSpaceUpDownCluster.mNumericUpDown));
         mSpaceUpDownCluster.mNumericUpDown.Focus();
         mSpaceUpDownCluster.mNumericUpDown.Select();
      }

      private void CtrlVAltV_Click(object? pSender, EventArgs pEventArguments) {
         ScalableRadioButtons.ScalableRadioButton? button = pSender as ScalableRadioButtons.ScalableRadioButton;
         if (button == null)
            return;
         mUseControlPasting = button.mReturnValue == 1;
      }

      private void ClearSearchButton_Click(object? pSender, EventArgs pEventArguments) {
         mUiState.mFindSearchHistory.Clear();
         mUiState.mFindPcreSearchHistory.Clear();
         mUiState.mSrSearchHistory.Clear();
         mUiState.mSrPcreSearchHistory.Clear();
      }

      private void ClearReplaceButton_Click(object? pSender, EventArgs pEventArguments) {
         mUiState.mSrReplaceHistory.Clear();
         mUiState.mSrPcreReplaceHistory.Clear();
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
         ThrowIfNull(mWhitespaceRadioCluster, nameof(mWhitespaceRadioCluster));
         ThrowIfNull(mTabUpDownCluster, nameof(mTabUpDownCluster));
         ThrowIfNull(mTabUpDownCluster.mNumericUpDown, nameof(mTabUpDownCluster.mNumericUpDown));
         ThrowIfNull(mSpaceUpDownCluster, nameof(mSpaceUpDownCluster));
         ThrowIfNull(mSpaceUpDownCluster.mNumericUpDown, nameof(mSpaceUpDownCluster.mNumericUpDown));
         ThrowIfNull(mSearchUpDownCluster, nameof(mSearchUpDownCluster));
         ThrowIfNull(mSearchUpDownCluster.mNumericUpDown, nameof(mSearchUpDownCluster.mNumericUpDown));
         ThrowIfNull(mReplaceUpDownCluster, nameof(mReplaceUpDownCluster));
         ThrowIfNull(mReplaceUpDownCluster.mNumericUpDown, nameof(mReplaceUpDownCluster.mNumericUpDown));
         ThrowIfNull(mAllIfNothingCheckBoxCluster, nameof(mAllIfNothingCheckBoxCluster));
         ThrowIfNull(mCommentWidthUpDownCluster, nameof(mCommentWidthUpDownCluster));
         ThrowIfNull(mCommentWidthUpDownCluster.mNumericUpDown, nameof(mCommentWidthUpDownCluster.mNumericUpDown));
         ThrowIfNull(mCommentConcatenateFirstCheckBoxCluster, nameof(mCommentConcatenateFirstCheckBoxCluster));
         ThrowIfNull(mCommentOutBlankLinesCheckBoxCluster, nameof(mCommentOutBlankLinesCheckBoxCluster));
         ThrowIfNull(mUseThreeSpacesCheckBoxCluster, nameof(mUseThreeSpacesCheckBoxCluster));
         ThrowIfNull(mEnforceFormattingProtectionCheckBoxCluster, nameof(mEnforceFormattingProtectionCheckBoxCluster));
         mUiState.mTopDraggerHeight = (int)mTopDraggerHeightUpDownCluster.mNumericUpDown.Value;
         mUiState.mTopDraggerEdge = (int)mTopDraggerEdgeUpDownCluster.mNumericUpDown.Value;
         mUiState.mActivationDelayMs = (int)mActivationDelayUpDownCluster.mNumericUpDown.Value;
         mUiState.mClipboardDelayMs = (int)mClipboardDelayUpDownCluster.mNumericUpDown.Value;
         mUiState.mReactivationDelayMs = (int)mReactivationRateUpDownCluster.mNumericUpDown.Value;
         mUiState.mUseTabs = false;
         mUiState.mUseSpaces = false;
         ScalableRadioButton? checkedWhitespaceButton = mWhitespaceRadioCluster.mRadioPanel.GetChecked();
         if (checkedWhitespaceButton != null && !string.IsNullOrEmpty(checkedWhitespaceButton.Tag as string)) {
            if (checkedWhitespaceButton.Tag.ToString() == "Tabs")
               mUiState.mUseTabs = true;
            else if (checkedWhitespaceButton.Tag.ToString() == "Spaces")
               mUiState.mUseSpaces = true;
            else if (checkedWhitespaceButton.Tag.ToString() == "Both") {
               mUiState.mUseTabs = true;
               mUiState.mUseSpaces = true;
            }
         }
         mUiState.mAllIfNothing = mAllIfNothingCheckBoxCluster.mScalableCheckBox.Checked;
         mUiState.mMaximumCommentWidth = (int)mCommentWidthUpDownCluster.mNumericUpDown.Value;
         mUiState.mConcatenateCommentFirst = mCommentConcatenateFirstCheckBoxCluster.mScalableCheckBox.Checked;
         mUiState.mCommentOutBlankLines = mCommentOutBlankLinesCheckBoxCluster.mScalableCheckBox.Checked;
         mUiState.mUseThreeSpaces = mUseThreeSpacesCheckBoxCluster.mScalableCheckBox.Checked;
         mUiState.mEnforceFormattingProtection = mEnforceFormattingProtectionCheckBoxCluster.mScalableCheckBox.Checked;
         mWhitespaceRadioCluster.mRadioPanel.GetReturnValue(out int whitespaceValue);
         mUiState.mWhitespace = whitespaceValue;
         mUiState.mSpacesPerTab = (int)mTabUpDownCluster.mNumericUpDown.Value;
         mUiState.mSpacesToBecomeTab = (int)mSpaceUpDownCluster.mNumericUpDown.Value;
         mUiState.mSearchHistoryMaxEntries = (int)mSearchUpDownCluster.mNumericUpDown.Value;
         mUiState.mReplaceHistoryMaxEntries = (int)mReplaceUpDownCluster.mNumericUpDown.Value;
         mUiState.mBracePairColor0 = mBracePairColors[0];
         mUiState.mBracePairColor1 = mBracePairColors[1];
         mUiState.mBracePairColor2 = mBracePairColors[2];
         mUiState.mBracePairColor3 = mBracePairColors[3];
         mUiState.mBracePairColor4 = mBracePairColors[4];
         mUiState.mBracePairColor5 = mBracePairColors[5];
         mUiState.mBracePairColor6 = mBracePairColors[6];
         mUiState.mBracePairColor7 = mBracePairColors[7];
         mUiState.mBracePairColor8 = mBracePairColors[8];
         mUiState.mBracePairColor9 = mBracePairColors[9];
         if (mShortcutEntries.Any(x => x.HasConflict)) {
            MessageBox.Show("Please resolve all shortcut conflicts before saving.",
               "Shortcut Conflicts", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
         }
         ThrowIfNull(mMenuStrip, nameof(mMenuStrip));
         ThrowIfNull(mMainContextMenuStrip, nameof(mMainContextMenuStrip));
         ShortcutManager.SyncLockedScrollingEntries(mShortcutEntries);
         ShortcutManager.Save(ShortcutManager.DefaultFilePath, mShortcutEntries);
         Dictionary<string, ToolStripMenuItem> menuDictionary = ShortcutManager.BuildMenuDictionary(mMenuStrip!, mMainContextMenuStrip!);
         ShortcutManager.Apply(mShortcutEntries, menuDictionary);
         ShortcutManager.SyncContextMenuShortcutDisplays();
         ShortcutManager.SyncScrollingContextMenuKeys();
         SaveColumnWidths();
         CloseOptionsPanel();
      }

      private void OnBracePairSwatchClicked(LabeledButtonColorSwatchCluster pSender) {
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         if (pSender.Tag is not int index)
            ThrowBadCode($"Tag was not an int in {nameof(OnBracePairSwatchClicked)}.");
         else
            ThemePanel.EnsureColorPickerPanel(mCurrentTheme, mBracePairColors[index],
               pPickedColor => ApplyBracePairColor(pSender, index, pPickedColor));
      }

      private void ApplyBracePairColor(LabeledButtonColorSwatchCluster pCluster, int pIndex, Color pColor) {
         pCluster.SetColor(pColor);
         mBracePairColors[pIndex] = pColor;
      }

      private void ShortcutsDgv_CurrentCellDirtyStateChanged(object? sender, EventArgs e) {
         if (mShortcutsDgv == null)
            return;
         if (mShortcutsDgv.IsCurrentCellDirty && mShortcutsDgv.CurrentCell is DataGridViewCheckBoxCell)
            mShortcutsDgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
      }

      private void ShortcutsDgv_CellValueChanged(object? sender, DataGridViewCellEventArgs e) {
         if (mShortcutsDgv == null || e.RowIndex < 0)
            return;
         DataGridViewRow row = mShortcutsDgv.Rows[e.RowIndex];
         if (row.Tag is not ShortcutEntry entry)
            return;
         switch (e.ColumnIndex) {
            case ShortColTop:
               entry.Top = row.Cells[ShortColTop].Value?.ToString() ?? "";
               break;
            case ShortColChild:
               entry.Child = row.Cells[ShortColChild].Value?.ToString() ?? "";
               break;
            case ShortColGrandchild:
               entry.Grandchild = row.Cells[ShortColGrandchild].Value?.ToString() ?? "";
               break;
            case ShortColCtrl:
               entry.Ctrl = row.Cells[ShortColCtrl].Value is true;
               break;
            case ShortColShift:
               entry.Shift = row.Cells[ShortColShift].Value is true;
               break;
            case ShortColAlt:
               entry.Alt = row.Cells[ShortColAlt].Value is true;
               break;
            case ShortColKey:
               entry.Key = row.Cells[ShortColKey].Value?.ToString() ?? "";
               break;
            case ShortColDisplayString:
               entry.DisplayString = row.Cells[ShortColDisplayString].Value?.ToString() ?? "";
               break;
            case ShortColNotes:
               entry.Notes = row.Cells[ShortColNotes].Value?.ToString() ?? "";
               break;
         }
         if (e.ColumnIndex >= ShortColCtrl && e.ColumnIndex <= ShortColKey) {
            ShortcutManager.CheckConflicts(mShortcutEntries);
            mShortcutsDgv.Invalidate();
         }
      }

      private void ShortcutsDgv_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e) {
         if (mShortcutsDgv == null || e.RowIndex < 0)
            return;
         DataGridViewRow row = mShortcutsDgv.Rows[e.RowIndex];
         if (row.Tag is not ShortcutEntry entry)
            return;
         if (e.CellStyle == null)
            return;
         if (entry.HasConflict && e.ColumnIndex >= ShortColCtrl && e.ColumnIndex <= ShortColKey) {
            e.CellStyle.BackColor = Color.Salmon;
            e.CellStyle.ForeColor = Color.Black;
            e.FormattingApplied = true;
         }
         else if (entry.ShortcutLocked && e.ColumnIndex >= ShortColCtrl && e.ColumnIndex <= ShortColKey) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            e.CellStyle.BackColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
            e.FormattingApplied = true;
         }
         if (!entry.ItemText.Contains('&')) {
            int ownCol = !string.IsNullOrEmpty(entry.Grandchild) ? ShortColGrandchild
               : !string.IsNullOrEmpty(entry.Child) ? ShortColChild
               : ShortColTop;
            if (e.ColumnIndex == ownCol) {
               e.CellStyle.BackColor = ContrastingColor(e.CellStyle.BackColor);
               e.CellStyle.ForeColor = ContrastingColor(e.CellStyle.ForeColor);
               e.FormattingApplied = true;
            }
         }
         if (string.IsNullOrEmpty(entry.Key) && !entry.ShortcutLocked && e.ColumnIndex == ShortColId) {
            e.CellStyle.BackColor = ContrastingColor(e.CellStyle.BackColor);
            e.CellStyle.ForeColor = ContrastingColor(e.CellStyle.ForeColor);
            e.FormattingApplied = true;
         }
      }

      private void ShortcutsResetButton_Click(object? sender, EventArgs e) {
         ConfirmationDialog.ShowMe(
            "Reset Shortcuts",
            "Reset all shortcuts and menu text to shipped defaults?",
            "&Yes, Reset", "&No, Keep",
            ShortcutsResetCallback);
      }

      private void ShortcutsResetCallback(bool pConfirmed) {
         ConfirmationDialog.Restore();
         if (!pConfirmed)
            return;
         string defaultPath = ShortcutManager.DefaultFilePath;
         if (File.Exists(defaultPath))
            File.Delete(defaultPath);
         mShortcutEntries = ShortcutManager.LoadWithFallback();
         LoadShortcutEntries();
      }

      private void ShortcutsAutoSizeCheckBox_CheckedChanged(object? sender, EventArgs e) {
         ThrowIfNull(mUiState, nameof(mUiState));
         ThrowIfNull(mShortcutsAutoSizeCheckBox, nameof(mShortcutsAutoSizeCheckBox));
         mUiState.mShortcutsDgvAutoSize = mShortcutsAutoSizeCheckBox.Checked;
         if (mShortcutsAutoSizeCheckBox.Checked)
            mShortcutsDgv?.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
      }

      private void ShortcutsSortCheckBox_CheckedChanged(object? sender, EventArgs e) {
         LoadShortcutEntries();
      }

      private void ShortcutsAllCheckBox_CheckedChanged(object? sender, EventArgs e) {
         LoadShortcutEntries();
      }

      private void CancelButton_Click(object? pSender, EventArgs pEventArguments) {
         SaveColumnWidths();
         CloseOptionsPanel();
      }
   }
}
