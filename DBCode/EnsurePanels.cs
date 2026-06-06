namespace DBCode {
   public sealed partial class MainForm : Form {
      public void EnsureThemePanel(ThemeUsage pThemeUsage) {
         ThrowIfNull(mForm, nameof(mForm));
         SuspendClientSizeChanged();
         if (mThemePanel == null)
            mThemePanel = new ThemePanel(pThemeUsage);
         ResumeClientSizeChanged();
         ShowThemePanel(pThemeUsage);
      }

      public static void ShowThemePanel(ThemeUsage pThemeUsage) {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mThemePanel, nameof(mThemePanel));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mUiState.FormBounds = mForm.Bounds;
         mForm.Bounds = mUiState.ThemeBounds;
         mForm.ControlBox = false;
         if (mForm.Controls.Contains(mScrollableMainPanel)) {
            mScrollableMainPanel.Visible = false;
            mScrollableMainPanel.SendToBack();
            mForm.Controls.Remove(mScrollableMainPanel);
            mActiveScrollablePanel = null;
         }
         mThemePanel.SetThemeUsage(pThemeUsage);
         mForm.Controls.Add(mThemePanel);
         mThemePanel.ContextMenuStrip = mGeneralContextMenuStrip;
         EnsureWindowFitsMonitor(mForm);
         mThemePanel.LayoutControls();
         mThemePanel.UpdateActiveScrollablePanel();
         mActiveLayoutable = mThemePanel.mBottomPanel;
         mThemePanel.mTitleLabel.CenterTitle();
         mThemePanel.mBottomPanel.PositionRightControls();
         mThemePanel.BringToFront();
         mThemePanel.Visible = true;
         mThemePanel.Show();
         mForm.Opacity = savedOpacity;
      }

      public void RestoreFromThemePanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mThemePanel, nameof(mThemePanel));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mUiState.mThemeLocation = mForm.Location;
         mUiState.mThemeSize = mForm.Size;
         bool dirtyTheme = mThemePanel.ThemeIsDirty();
         mThemePanel.Visible = false;
         mThemePanel.SendToBack();
         mForm.Controls.Remove(mThemePanel);
         mThemePanel.ContextMenuStrip = null;
         SuspendClientSizeChanged();
         mForm.Bounds = mUiState.FormBounds;
         ResumeClientSizeChanged();
         mForm.Controls.Add(mScrollableMainPanel);
         mForm.ApplyTheme();
         if (dirtyTheme)
            LayoutControls();
         mMainBottomPanel.LayoutControls();
         mScrollableMainPanel.BringToFront();
         mScrollableMainPanel.Visible = true;
         mScrollableMainPanel.Show();
         mActiveScrollablePanel = mScrollableMainPanel;
         mForm.ControlBox = true;
         mForm.Activate();
         mRichTextBox.Focus();
         mForm.Opacity = savedOpacity;
         mActiveLayoutable = mMainBottomPanel;
         mMainBottomPanel.LayoutControls();
      }

      public static void EnsureThemePickerPanel(PickMode pPickMode) {
         ThrowIfNull(mForm, nameof(mForm));
         mUiState.FormBounds = mForm.Bounds;
         mThemePickerPanel?.Dispose();
         mThemePickerPanel = new ThemePickerPanel(pPickMode);
         ShowThemePickerPanel();
      }

      public static void ShowThemePickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mThemePickerPanel, nameof(mThemePickerPanel));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mThemePickerPanel.mClusterContainer, nameof(mThemePickerPanel.mClusterContainer));
         ThrowIfNull(mThemePickerPanel.mTitleLabel, nameof(mThemePickerPanel.mTitleLabel));
         ThrowIfNull(mThemePickerPanel.mBottomPanel, nameof(mThemePickerPanel.mBottomPanel));
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mForm.ControlBox = false;
         if (mForm.Controls.Contains(mScrollableMainPanel)) {
            mScrollableMainPanel.Visible = false;
            mScrollableMainPanel.SendToBack();
            mForm.Controls.Remove(mScrollableMainPanel);
            mActiveScrollablePanel = null;
         }
         mForm.Controls.Add(mThemePickerPanel);
         mThemePickerPanel.ContextMenuStrip = mGeneralContextMenuStrip;
         mActiveScrollablePanel = mThemePickerPanel;
         mActiveLayoutable = mThemePickerPanel.mBottomPanel;
         mThemePickerPanel.mTitleLabel.CenterTitle();
         mThemePickerPanel.mBottomPanel.PositionRightControls();
         mThemePickerPanel.BringToFront();
         mThemePickerPanel.Visible = true;
         mThemePickerPanel.Show();
         mForm.Opacity = savedOpacity;
      }

      public void RestoreFromThemePickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mThemePickerPanel, nameof(mThemePickerPanel));
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mUiState.ThemePickerBounds = mForm.Bounds;
         mThemePickerPanel.Visible = false;
         mThemePickerPanel.SendToBack();
         mForm.Controls.Remove(mThemePickerPanel);
         mThemePickerPanel.ContextMenuStrip = null;
         mThemePickerPanel.Dispose();
         mThemePickerPanel = null;
         SuspendClientSizeChanged();
         mForm.Bounds = mUiState.FormBounds;
         ResumeClientSizeChanged();
         mForm.Controls.Add(mScrollableMainPanel);
         mForm.ApplyTheme();
         mMainBottomPanel.LayoutControls();
         mScrollableMainPanel.BringToFront();
         mScrollableMainPanel.Visible = true;
         mScrollableMainPanel.Show();
         mActiveScrollablePanel = mScrollableMainPanel;
         mForm.ControlBox = true;
         mForm.Activate();
         mRichTextBox.Focus();
         mForm.Opacity = savedOpacity;
         mActiveLayoutable = mMainBottomPanel;
         mMainBottomPanel.LayoutControls();
      }

      public static void EnsureOptionsPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         mUiState.FormBounds = mForm.Bounds;
         mOptionsPanel?.Dispose();
         mOptionsPanel = new OptionsPanel();
         ShowOptionsPanel();
      }

      public static void ShowOptionsPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mOptionsPanel, nameof(mOptionsPanel));
         ThrowIfNull(mOptionsPanel.mTitleLabel, nameof(mOptionsPanel.mTitleLabel));
         ThrowIfNull(mOptionsPanel.mBottomPanel, nameof(mOptionsPanel.mBottomPanel));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mOptionsPanel.mMagicNumbersGroupBox, nameof(mOptionsPanel.mMagicNumbersGroupBox));
         ThrowIfNull(mOptionsPanel.mCFamilyTitleLabel, nameof(mOptionsPanel.mCFamilyTitleLabel));
         ThrowIfNull(mOptionsPanel.mBasicTitleLabel, nameof(mOptionsPanel.mBasicTitleLabel));
         ThrowIfNull(mOptionsPanel.mFSharpTitleLabel, nameof(mOptionsPanel.mFSharpTitleLabel));
         ThrowIfNull(mOptionsPanel.mHTMLTitleLabel, nameof(mOptionsPanel.mHTMLTitleLabel));
         ThrowIfNull(mOptionsPanel.mCSSTitleLabel, nameof(mOptionsPanel.mCSSTitleLabel));
         ThrowIfNull(mOptionsPanel.mXMLTitleLabel, nameof(mOptionsPanel.mXMLTitleLabel));
         ThrowIfNull(mOptionsPanel.mJSONTitleLabel, nameof(mOptionsPanel.mJSONTitleLabel));
         ThrowIfNull(mOptionsPanel.mPowerShellTitleLabel, nameof(mOptionsPanel.mPowerShellTitleLabel));
         ThrowIfNull(mOptionsPanel.mBatchTitleLabel, nameof(mOptionsPanel.mBatchTitleLabel));
         ThrowIfNull(mOptionsPanel.mSQLTitleLabel, nameof(mOptionsPanel.mSQLTitleLabel));
         ThrowIfNull(mOptionsPanel.mMarkdownTitleLabel, nameof(mOptionsPanel.mMarkdownTitleLabel));
         ThrowIfNull(mOptionsPanel.mPythonTitleLabel, nameof(mOptionsPanel.mPythonTitleLabel));
         ThrowIfNull(mOptionsPanel.mBraceMatchingTitleLabel, nameof(mOptionsPanel.mBraceMatchingTitleLabel));
         ThrowIfNull(mOptionsPanel.mBraceMatchingContainer, nameof(mOptionsPanel.mBraceMatchingContainer));
         ThrowIfNull(mOptionsPanel.mBraceExampleGroupBox, nameof(mOptionsPanel.mBraceExampleGroupBox));
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mForm.ControlBox = false;
         if (mForm.Controls.Contains(mScrollableMainPanel)) {
            mScrollableMainPanel.Visible = false;
            mScrollableMainPanel.SendToBack();
            mForm.Controls.Remove(mScrollableMainPanel);
            mActiveScrollablePanel = null;
         }
         mForm.Controls.Add(mOptionsPanel);
         mOptionsPanel.ContextMenuStrip = mGeneralContextMenuStrip;
         mOptionsPanel.ApplyFontsAndColors();
         mOptionsPanel.LayoutControls();
         mForm.SuspendClientSizeChanged();
         mActiveLayoutable = mOptionsPanel.mBottomPanel;
         mForm.Bounds = mUiState.OptionsBounds;
         EnsureWindowFitsMonitor(mForm);
         TargetListManager.PopulateGrid(mOptionsPanel.mIncludeDataGridView, mAllowedTargetWindows);
         TargetListManager.PopulateGrid(mOptionsPanel.mExcludeDataGridView, mDisallowedTargetWindows);
         mOptionsPanel.mTitleLabel.CenterTitle();
         mOptionsPanel.mBottomPanel.PositionRightControls();
         mOptionsPanel.mCFamilyTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mBasicTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mFSharpTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mHTMLTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mCSSTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mXMLTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mJSONTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mPowerShellTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mBatchTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mSQLTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mMarkdownTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mPythonTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mBraceMatchingTitleLabel.CenterTitle(mOptionsPanel);
         mOptionsPanel.mBraceMatchingContainer.Location = new Point(mIndent, mOptionsPanel.mTitleLabel.Bottom);
         mOptionsPanel.mBraceExampleGroupBox.Location = new Point(mOptionsPanel.mBraceMatchingContainer.Right + mEm, mOptionsPanel.mBraceMatchingContainer.Top);
         mForm.ResumeClientSizeChanged();
         mOptionsPanel.BringToFront();
         mOptionsPanel.Visible = true;
         mOptionsPanel.Show();
         mForm.Opacity = savedOpacity;
      }

      public void RestoreFromOptionsPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mOptionsPanel, nameof(mOptionsPanel));
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mUiState.OptionsBounds = mForm.Bounds;
         TargetListManager.SaveGrid(mOptionsPanel.mIncludeDataGridView, mAllowedTargetWindows);
         TargetListManager.SaveGrid(mOptionsPanel.mExcludeDataGridView, mDisallowedTargetWindows);
         mOptionsPanel.Visible = false;
         mOptionsPanel.SendToBack();
         mForm.Controls.Remove(mOptionsPanel);
         mOptionsPanel.ContextMenuStrip = null;
         mOptionsPanel.Dispose();
         mOptionsPanel = null;
         SuspendClientSizeChanged();
         mForm.Bounds = mUiState.FormBounds;
         ResumeClientSizeChanged();
         mForm.Controls.Add(mScrollableMainPanel);
         mForm.ApplyTheme();
         mMainBottomPanel.LayoutControls();
         mScrollableMainPanel.BringToFront();
         mScrollableMainPanel.Visible = true;
         mScrollableMainPanel.Show();
         mActiveScrollablePanel = mScrollableMainPanel;
         mForm.ControlBox = true;
         mForm.Activate();
         mRichTextBox.Focus();
         mForm.Opacity = savedOpacity;
         mActiveLayoutable = mMainBottomPanel;
         mMainBottomPanel.LayoutControls();
      }

      public static void EnsureTargetPickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         mUiState.FormBounds = mForm.Bounds;
         TargetListManager.InvalidateCache();
         TargetListManager.EnsureDataFiles();
         PopulateTargets();
         mTargetPickerPanel = new TargetPickerPanel();
         ShowTargetPickerPanel();
      }

      public static void ShowTargetPickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mTargetPickerPanel, nameof(mTargetPickerPanel));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mTargetPickerPanel.mClusterContainer, nameof(mTargetPickerPanel.mClusterContainer));
         ThrowIfNull(mTargetPickerPanel.mTitleLabel, nameof(mTargetPickerPanel.mTitleLabel));
         ThrowIfNull(mTargetPickerPanel.mBottomPanel, nameof(mTargetPickerPanel.mBottomPanel));
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mForm.ControlBox = false;
         if (mForm.Controls.Contains(mScrollableMainPanel)) {
            mScrollableMainPanel.Visible = false;
            mScrollableMainPanel.SendToBack();
            mForm.Controls.Remove(mScrollableMainPanel);
            mActiveScrollablePanel = null;
         }
         mForm.Controls.Add(mTargetPickerPanel);
         mTargetPickerPanel.ContextMenuStrip = mGeneralContextMenuStrip;
         mActiveScrollablePanel = mTargetPickerPanel;
         mActiveLayoutable = mTargetPickerPanel.mBottomPanel;
         mTargetPickerPanel.mTitleLabel.CenterTitle();
         mTargetPickerPanel.mBottomPanel.PositionRightControls();
         mTargetPickerPanel.BringToFront();
         mTargetPickerPanel.Visible = true;
         mTargetPickerPanel.Show();
         mForm.Opacity = savedOpacity;
      }

      public void RestoreFromTargetPickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mTargetPickerPanel, nameof(mTargetPickerPanel));
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mUiState.mTargetPickerBounds = mForm.Bounds;
         bool targetSelected = mTargetPickerPanel.mTargetSelected;
         mTargetPickerPanel.Visible = false;
         mTargetPickerPanel.SendToBack();
         mForm.Controls.Remove(mTargetPickerPanel);
         mTargetPickerPanel.ContextMenuStrip = null;
         mTargetPickerPanel.Dispose();
         mTargetPickerPanel = null;
         if (mTargetingTargetedTSMI != null)
            mTargetingTargetedTSMI.Checked = targetSelected;
         if (!targetSelected)
            EnterUntargetedMode();
         SuspendClientSizeChanged();
         mForm.Bounds = mUiState.FormBounds;
         ResumeClientSizeChanged();
         mForm.Controls.Add(mScrollableMainPanel);
         UpdateTargetingStatusLabel();
         mMainBottomPanel.LayoutControls();
         mScrollableMainPanel.BringToFront();
         mScrollableMainPanel.Visible = true;
         mScrollableMainPanel.Show();
         mActiveScrollablePanel = mScrollableMainPanel;
         mForm.ControlBox = true;
         mForm.Activate();
         mRichTextBox.Focus();
         mForm.Opacity = savedOpacity;
         mActiveLayoutable = mMainBottomPanel;
         mMainBottomPanel.LayoutControls();
      }
   }
}
