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
         }
         mThemePanel.SetThemeUsage(pThemeUsage);
         mForm.Controls.Add(mThemePanel);
         EnsureWindowFitsMonitor(mForm);
         mThemePanel.LayoutControls();
         mActiveLayoutable = mThemePanel.mBottomPanel;
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
         mForm.ControlBox = true;
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
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mForm.ControlBox = false;
         if (mForm.Controls.Contains(mScrollableMainPanel)) {
            mScrollableMainPanel.Visible = false;
            mScrollableMainPanel.SendToBack();
            mForm.Controls.Remove(mScrollableMainPanel);
         }
         mForm.Controls.Add(mThemePickerPanel);
         mActiveLayoutable = mThemePickerPanel.mThemePickerBottomPanel;
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
         mForm.ControlBox = true;
         mForm.Activate();
         mScrollableMainPanel.Focus();
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
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mForm.ControlBox = false;
         if (mForm.Controls.Contains(mScrollableMainPanel)) {
            mScrollableMainPanel.Visible = false;
            mScrollableMainPanel.SendToBack();
            mForm.Controls.Remove(mScrollableMainPanel);
         }
         mForm.Controls.Add(mOptionsPanel);
         mForm.SuspendClientSizeChanged();
         mActiveLayoutable = mOptionsPanel.mBottomPanel;
         mOptionsPanel.LayoutControls();
         mForm.Bounds = mUiState.mOptionsBounds;
         EnsureWindowFitsMonitor(mForm);
         TargetListManager.PopulateGrid(mOptionsPanel.mIncludeDataGridView, mAllowedTargetWindows);
         TargetListManager.PopulateGrid(mOptionsPanel.mExcludeDataGridView, mDisallowedTargetWindows);
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
         mUiState.mOptionsBounds = mForm.Bounds;
         TargetListManager.SaveGrid(mOptionsPanel.mIncludeDataGridView, mAllowedTargetWindows);
         TargetListManager.SaveGrid(mOptionsPanel.mExcludeDataGridView, mDisallowedTargetWindows);
         mOptionsPanel.Visible = false;
         mOptionsPanel.SendToBack();
         mForm.Controls.Remove(mOptionsPanel);
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
         mForm.ControlBox = true;
         mForm.Activate();
         mScrollableMainPanel.Focus();
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
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mForm.ControlBox = false;
         if (mForm.Controls.Contains(mScrollableMainPanel)) {
            mScrollableMainPanel.Visible = false;
            mScrollableMainPanel.SendToBack();
            mForm.Controls.Remove(mScrollableMainPanel);
         }
         mForm.Controls.Add(mTargetPickerPanel);
         mActiveLayoutable = mTargetPickerPanel.mTargetPickerBottomPanel;
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
         mTargetPickerPanel.Dispose();
         mTargetPickerPanel = null;
         if (mTargetedTSMI != null)
            mTargetedTSMI.Checked = targetSelected;
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
         mForm.ControlBox = true;
         mForm.Activate();
         mScrollableMainPanel.Focus();
         mForm.Opacity = savedOpacity;
         mActiveLayoutable = mMainBottomPanel;
         mMainBottomPanel.LayoutControls();
      }
   }
}
