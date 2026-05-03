namespace DBCode {
   public sealed partial class MainForm : Form {
      public void EnsureThemePanel(ThemeUsage pThemeUsage) {
         ThrowIfNull(mForm, nameof(mForm));
         mUiState.FormBounds = mForm.Bounds;
         SuspendClientSizeChanged();
         if (mThemePanel == null)
            mThemePanel = new ThemePanel(pThemeUsage);
         mForm.Bounds = mUiState.ThemeBounds;
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

      public void EnsureThemePickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         mUiState.FormBounds = mForm.Bounds;
         mForm.SuspendClientSizeChanged();
         mThemePickerPanel = new ThemePickerPanel();
         mForm.Bounds = mUiState.ThemePickerBounds;
         mForm.ResumeClientSizeChanged();
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
         mThemePickerPanel.ApplyTheme();
         mThemePickerPanel.LayoutPanel();
         mThemePickerPanel.mClusterContainer.LayoutClusters();
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
   }
}
