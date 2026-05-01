namespace DBCode {
   public sealed partial class MainForm : Form {
      public void EnsureThemePanel(ThemeUsage pThemeUsage) {
         ThrowIfNull(mForm, nameof(mForm));
         mUiState.FormBounds = mForm.Bounds;
         SuspendClientSizeChanged();
         if (mThemePanel == null)
            mThemePanel = new ThemePanel(pThemeUsage);
         if (mFirstTheme) {
            mForm.Size = mThemePanel.WantedSize();
            CenterFormOnMonitor(mForm);
            EnsureWindowFitsMonitor(mForm, false);
            mFirstTheme = false;
         }
         else
            mForm.Bounds = mUiState.ThemeBounds;
         ResumeClientSizeChanged();
         ShowThemePanel(pThemeUsage);
      }

      public void ShowThemePanel(ThemeUsage pThemeUsage) {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mThemePanel, nameof(mThemePanel));
         ThrowIfNull(mMainPanel, nameof(mMainPanel));
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mForm.ControlBox = false;
         if (mForm.Controls.Contains(mMainPanel)) {
            mMainPanel.Visible = false;
            mMainPanel.SendToBack();
            mForm.Controls.Remove(mMainPanel);
         }
         mThemePanel.SetThemeUsage(pThemeUsage);
         mForm.Controls.Add(mThemePanel);
         EnsureWindowFitsMonitor(mForm, false);
         mThemePanel.ApplyTheme(mCurrentTheme);
         mThemePanel.LayoutControls();
         mThemePanel.BringToFront();
         mThemePanel.Visible = true;
         mThemePanel.Show();
         mForm.Opacity = savedOpacity;
      }

      public void RestoreFromThemePanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mThemePanel, nameof(mThemePanel));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         ThrowIfNull(mMainPanel, nameof(mMainPanel));
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
         mForm.Controls.Add(mMainPanel);
         mForm.ApplyTheme();
         if (dirtyTheme)
            LayoutControls();
         mMainBottomPanel.LayoutControls();
         mMainPanel.BringToFront();
         mMainPanel.Visible = true;
         mMainPanel.Show();
         mForm.ControlBox = true;
         mForm.Opacity = savedOpacity;
      }

      public void EnsureThemePickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         mUiState.FormBounds = mForm.Bounds;
         mForm.SuspendClientSizeChanged();
         mForm.Bounds = mUiState.ThemePickerBounds;
         mThemePickerPanel = new ThemePickerPanel();
         mForm.ResumeClientSizeChanged();
         ShowThemePickerPanel();
      }

      public void ShowThemePickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mThemePickerPanel, nameof(mThemePickerPanel));
         ThrowIfNull(mMainPanel, nameof(mMainPanel));
         double savedOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         mForm.ControlBox = false;
         if (mForm.Controls.Contains(mMainPanel)) {
            mMainPanel.Visible = false;
            mMainPanel.SendToBack();
            mForm.Controls.Remove(mMainPanel);
         }
         mForm.Controls.Add(mThemePickerPanel);
         mThemePickerPanel.ApplyTheme();
         mThemePickerPanel.LayoutPanel();
         mThemePickerPanel.mClusterContainer!.LayoutClusters();
         mThemePickerPanel.BringToFront();
         mThemePickerPanel.Visible = true;
         mThemePickerPanel.Show();
         mForm.Opacity = savedOpacity;
      }

      public void RestoreFromThemePickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mMainPanel, nameof(mMainPanel));
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
         mForm.Controls.Add(mMainPanel);
         mForm.ApplyTheme();
         mMainBottomPanel.LayoutControls();
         mMainPanel.BringToFront();
         mMainPanel.Visible = true;
         mMainPanel.Show();
         mForm.ControlBox = true;
         mForm.Activate();
         mMainPanel.Focus();
         mForm.Opacity = savedOpacity;
      }
   }
}
