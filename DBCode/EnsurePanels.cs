namespace DBCode {
   public sealed partial class MainForm : Form {
      public void EnsureThemePanel(ThemeUsage pThemeUsage) {
         ThrowIfNull(mForm, nameof(mForm));
         mPreThemeBounds = Bounds;
         mOpacity = mForm.Opacity;
         mForm.Opacity = 0;
         if (mThemePanel == null)
            mThemePanel = new ThemePanel(pThemeUsage);
         ShowThemePanel(pThemeUsage);
      }

      public void ShowThemePanel(ThemeUsage pThemeUsage) {
         ThrowIfNull(mThemePanel, nameof(mThemePanel));
         ThrowIfNull(mForm, nameof(mForm));
         if (pThemeUsage == ThemeUsage.Design) {
            ControlBox = false;
            if (Controls.Contains(mMenuStrip))
               Controls.Remove(mMenuStrip);
            if (Controls.Contains(mRichTextBox))
               Controls.Remove(mRichTextBox);
            if (Controls.Contains(mMainBottomPanel))
               Controls.Remove(mMainBottomPanel);
            if (!Controls.Contains(mThemePanel))
               Controls.Add(mThemePanel);
            mThemePanel.LayoutControls();
            if (mFirstTheme) {
               mForm.Size = mThemePanel.WantedSize();
               CenterFormOnMonitor(mForm);
               mFirstTheme = false;
            }
            else
               Bounds = new Rectangle(mUiState.mThemeLocation, mUiState.mThemeSize);
            EnsureWindowFitsMonitor(mForm, false);
            mThemePanel.Visible = true;
            mThemePanel.BringToFront();
            mThemePanel.Show();
         }
         else {
            TimedMessage("ShowThemePanel(ThemeUsage) edit is not working.", "Not Yet IMPLEMENTED");
         }
         mForm.Opacity = mOpacity;
      }

      public void RestoreFromThemePanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mThemePanel, nameof(mThemePanel));
         bool dirtyTheme = false;

         mForm.SuspendLayout();
         if (!mFirstTheme)
            mThemeBounds = mForm.Bounds;
         mUiState.mThemeLocation = Location;
         mUiState.mThemeSize = Size;
         mForm.ControlBox = true;
         dirtyTheme = mThemePanel.ThemeIsDirty();
         mThemePanel.Visible = false;
         mThemePanel.SendToBack();
         if (mForm.Controls.Contains(mThemePanel))
            mForm.Controls.Remove(mThemePanel);
         mForm.Bounds = mPreThemeBounds;
         if (!mForm.Controls.Contains(mRichTextBox))
            mForm.Controls.Add(mRichTextBox);
         if (!mForm.Controls.Contains(mMainBottomPanel))
            mForm.Controls.Add(mMainBottomPanel);
         if (mCurrentViewMode == ViewMode.Features && !mForm.Controls.Contains(mMenuStrip))
            mForm.Controls.Add(mMenuStrip);
         mRichTextBox?.Visible = true;
         mMainBottomPanel?.Visible = true;
         if (mCurrentViewMode == ViewMode.Features)
            mMenuStrip?.Visible = true;
         if (dirtyTheme)
            LayoutControls();
         mForm.ResumeLayout(true);
      }

      public void EnsureThemePickerPanel() {
         mPreThemePickerBounds = Bounds;
         Bounds = mThemePickerBounds;
         Bounds = new Rectangle(mUiState.mThemePickerLocation, mUiState.mThemePickerSize);
         mThemePickerBounds = Bounds;
         mThemePickerPanel = new ThemePickerPanel();
         ShowThemePickerPanel();
      }

      public void ShowThemePickerPanel() {
         ThrowIfNull(mThemePickerPanel, nameof(mThemePickerPanel));
         ControlBox = false;
         if (Controls.Contains(mMenuStrip))
            Controls.Remove(mMenuStrip);
         if (Controls.Contains(mRichTextBox))
            Controls.Remove(mRichTextBox);
         if (Controls.Contains(mMainBottomPanel))
            Controls.Remove(mMainBottomPanel);
         Controls.Add(mThemePickerPanel);
         PerformLayout();
         mThemePickerPanel.LayoutPanel();
         mThemePickerPanel.mClusterContainer!.LayoutClusters();
         mThemePickerPanel.Visible = true;
         mThemePickerPanel.BringToFront();
         mThemePickerPanel.Show();
      }

      public void RestoreFromThemePickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mThemePickerPanel, nameof(mThemePickerPanel));
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         ThrowIfNull(mMenuStrip, nameof(mMenuStrip));
         mForm.SuspendLayout();
         mThemePickerBounds = mForm.Bounds;
         //mUiState.mThemePickerLocation = Location;
         //mUiState.mThemePickerSize = Size;
         mForm.ControlBox = true;
         mForm.Controls.Remove(mThemePickerPanel);
         mThemePickerPanel.Dispose();
         mThemePickerPanel = null;
         mForm.Bounds = mPreThemePickerBounds;
         if (!mForm.Controls.Contains(mRichTextBox))
            mForm.Controls.Add(mRichTextBox);
         if (!mForm.Controls.Contains(mMainBottomPanel))
            mForm.Controls.Add(mMainBottomPanel);
         if (mCurrentViewMode == ViewMode.Features && !mForm.Controls.Contains(mMenuStrip))
            mForm.Controls.Add(mMenuStrip);
         mRichTextBox.Visible = true;
         mMainBottomPanel.Visible = true;
         if (mCurrentViewMode == ViewMode.Features)
            mMenuStrip.Visible = true;
         mForm.Activate();
         mRichTextBox.Focus();
         mForm.ResumeLayout(true);
      }
   }
}
