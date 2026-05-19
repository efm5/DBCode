namespace DBCode {
   internal partial class OptionsPanel : Panel {
      protected override void OnHandleCreated(EventArgs pEventArgs) {
         base.OnHandleCreated(pEventArgs);
         Dock = DockStyle.Fill;
         LayoutControls(pApplyFonts: false);
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

      private void GeneralTabControl_SelectedIndexChanged(object? pSender, EventArgs pArgs) {
         ThrowIfNull(mUiState, nameof(mUiState));
         ThrowIfNull(mGeneralTabControl, nameof(mGeneralTabControl));
         mUiState.mOptionsGeneralTabControlPageIndex = mGeneralTabControl.SelectedIndex;
      }

      private void IncludeExcludeTabControl_SelectedIndexChanged(object? pSender, EventArgs pArgs) {
         ThrowIfNull(mUiState, nameof(mUiState));
         ThrowIfNull(mIncludeExcludeTabControl, nameof(mIncludeExcludeTabControl));
         mUiState.mOptionsIncludeExcludeTabControlPageIndex = mIncludeExcludeTabControl.SelectedIndex;
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
         if (button == null) return;
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
         ThrowIfNull(mTabCheckBoxCluster, nameof(mTabCheckBoxCluster));
         ThrowIfNull(mSpaceCheckBoxCluster, nameof(mSpaceCheckBoxCluster));
         ThrowIfNull(mWhitespaceRadioCluster, nameof(mWhitespaceRadioCluster));
         ThrowIfNull(mTabUpDownCluster, nameof(mTabUpDownCluster));
         ThrowIfNull(mTabUpDownCluster.mNumericUpDown, nameof(mTabUpDownCluster.mNumericUpDown));
         ThrowIfNull(mSpaceUpDownCluster, nameof(mSpaceUpDownCluster));
         ThrowIfNull(mSpaceUpDownCluster.mNumericUpDown, nameof(mSpaceUpDownCluster.mNumericUpDown));
         ThrowIfNull(mSearchUpDownCluster, nameof(mSearchUpDownCluster));
         ThrowIfNull(mSearchUpDownCluster.mNumericUpDown, nameof(mSearchUpDownCluster.mNumericUpDown));
         ThrowIfNull(mReplaceUpDownCluster, nameof(mReplaceUpDownCluster));
         ThrowIfNull(mReplaceUpDownCluster.mNumericUpDown, nameof(mReplaceUpDownCluster.mNumericUpDown));
         mUiState.mTopDraggerHeight = (int)mTopDraggerHeightUpDownCluster.mNumericUpDown.Value;
         mUiState.mTopDraggerEdge = (int)mTopDraggerEdgeUpDownCluster.mNumericUpDown.Value;
         mUiState.mActivationDelayMs = (int)mActivationDelayUpDownCluster.mNumericUpDown.Value;
         mUiState.mClipboardDelayMs = (int)mClipboardDelayUpDownCluster.mNumericUpDown.Value;
         mUiState.mReactivationDelayMs = (int)mReactivationRateUpDownCluster.mNumericUpDown.Value;
         mUiState.mUseTabs = mTabCheckBoxCluster.mScalableCheckBox.Checked;
         mUiState.mUseSpaces = mSpaceCheckBoxCluster.mScalableCheckBox.Checked;
         mWhitespaceRadioCluster.mRadioPanel.GetReturnValue(out int whitespaceValue);
         mUiState.mWhitespace = whitespaceValue;
         mUiState.mSpacesPerTab = (int)mTabUpDownCluster.mNumericUpDown.Value;
         mUiState.mSpacesToBecomeTab = (int)mSpaceUpDownCluster.mNumericUpDown.Value;
         mUiState.mSearchHistoryMaxEntries = (int)mSearchUpDownCluster.mNumericUpDown.Value;
         mUiState.mReplaceHistoryMaxEntries = (int)mReplaceUpDownCluster.mNumericUpDown.Value;
         CloseOptionsPanel();
      }

      private void CancelButton_Click(object? pSender, EventArgs pEventArguments) =>
         CloseOptionsPanel();
   }
}
