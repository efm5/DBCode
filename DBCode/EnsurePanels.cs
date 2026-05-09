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

      public void ShowThemePanel(ThemeUsage pThemeUsage) {
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
         mThemePanel.ApplyTheme(mCurrentTheme);
         mThemePanel.LayoutControls();
         mActiveLayoutable = mThemePanel.mThemeBottomPanel;
         TargetListManager.PopulateGrid(mThemePanel.mIncludeDataGridView, mAllowedTargetWindows);
         TargetListManager.PopulateGrid(mThemePanel.mExcludeDataGridView, mDisallowedTargetWindows);
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
         TargetListManager.SaveGrid(mThemePanel.mIncludeDataGridView, mAllowedTargetWindows);
         TargetListManager.SaveGrid(mThemePanel.mExcludeDataGridView, mDisallowedTargetWindows);
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

      public void EnsureThemePickerPanel(PickMode pPickMode) {
         ThrowIfNull(mForm, nameof(mForm));
         mUiState.FormBounds = mForm.Bounds;
         mThemePickerPanel = new ThemePickerPanel(pPickMode);
         ShowThemePickerPanel();
      }

      public void ShowThemePickerPanel() {
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

      public void EnsureOptionsPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         mUiState.FormBounds = mForm.Bounds;
         mOptionsPanel = new OptionsPanel();
         ShowOptionsPanel();
      }

      public void ShowOptionsPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mOptionsPanel, nameof(mOptionsPanel));
         ThrowIfNull(mOptionsPanel.mOptionsBottomPanel, nameof(mOptionsPanel.mOptionsBottomPanel));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mOptionsPanel.mClusterGroupBox, nameof(mOptionsPanel.mClusterGroupBox));
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
         mOptionsPanel.LayoutControls();
         mActiveLayoutable = mOptionsPanel.mOptionsBottomPanel;
         mForm.Bounds = mUiState.mOptionsBounds;
         EnsureWindowFitsMonitor(mForm);
         mOptionsPanel.mOptionsBottomPanel.LayoutControls();
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
   }
}
