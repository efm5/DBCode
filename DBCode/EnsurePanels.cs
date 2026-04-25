using DBCode.Themes;

namespace DBCode {
   public sealed partial class MainForm : Form {
      public void EnsureThemePanel(ThemeUsage pThemeUsage) {
         mPreThemeBounds = Bounds;
         if (mThemePanel == null)
            mThemePanel = new ThemePanel(pThemeUsage, mUiState);
         ShowThemePanel(pThemeUsage);
      }

      public void ShowThemePanel(ThemeUsage pThemeUsage) {
         if (pThemeUsage == ThemeUsage.Design) {
            ThrowIfNull(mThemePanel, nameof(mThemePanel));
            ControlBox = false;
            if (Controls.Contains(mMenuStrip))
               Controls.Remove(mMenuStrip);
            if (Controls.Contains(mRichTextBox))
               Controls.Remove(mRichTextBox);
            if (Controls.Contains(mStatusStrip))
               Controls.Remove(mStatusStrip);
            if (!Controls.Contains(mThemePanel))
               Controls.Add(mThemePanel);
            mThemePanel.LayoutControls();
            if (mFirstTheme) {
               ThrowIfNull(mForm, nameof(mForm));
               mForm.Size = mThemePanel.WantedSize();
               CenterFormOnMonitor(mForm);
               mFirstTheme = false;
            }
            else {
               mThemePanel.LayoutControls();
               Bounds = new Rectangle(mUiState.mThemeLocation, mUiState.mThemeSize);
            }
            EnsureWindowFitsMonitor(mForm, false);
            mThemePanel.Visible = true;
            mThemePanel.BringToFront();
            mThemePanel.Show();
         }
         else {
            TimedMessage("ShowThemePanel(ThemeUsage) edit is not working.", "Not Yet IMPLEMENTED");
         }
      }

      public void RestoreFromThemePanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mThemePanel, nameof(mThemePanel));
         bool dirtyTheme = false;

         mForm.SuspendLayout();
         if (!mFirstTheme)
            mThemeBounds = mForm.Bounds;
         mForm.ControlBox = true;
         dirtyTheme = mThemePanel.ThemeIsDirty();
         mThemePanel.Visible = false;
         mThemePanel.SendToBack();
         if (mForm.Controls.Contains(mThemePanel))
            mForm.Controls.Remove(mThemePanel);
         mForm.Bounds = mPreThemeBounds;
         if (!mForm.Controls.Contains(mRichTextBox))
            mForm.Controls.Add(mRichTextBox);
         if (!mForm.Controls.Contains(mStatusStrip))
            mForm.Controls.Add(mStatusStrip);
         if (mCurrentViewMode == ViewMode.Features && !mForm.Controls.Contains(mMenuStrip))
            mForm.Controls.Add(mMenuStrip);
         mRichTextBox?.Visible = true;
         mStatusStrip?.Visible = true;
         if (mCurrentViewMode == ViewMode.Features)
            mMenuStrip?.Visible = true;
         if (dirtyTheme)
            LayoutControls();
         mForm.ResumeLayout(true);
      }

      public void EnsureThemePickerPanel() {
         mPreThemePickerBounds = Bounds;
         Bounds = new Rectangle(mUiState.mThemePickerLocation, mUiState.mThemePickerSize);
         if (mThemePickerPanel == null) {
            ThrowIfNull(mForm, nameof(mForm));
            mThemePickerPanel = new ThemePickerPanel(mForm as MainForm);
         }
         if (!mFirstThemePicker)
            Bounds = mThemePickerBounds;
         ShowThemePickerPanel();
      }

      public void ShowThemePickerPanel() {
         ThrowIfNull(mThemePickerPanel, nameof(mThemePickerPanel));
         ControlBox = false;
         if (Controls.Contains(mMenuStrip))
            Controls.Remove(mMenuStrip);
         if (Controls.Contains(mRichTextBox))
            Controls.Remove(mRichTextBox);
         if (Controls.Contains(mStatusStrip))
            Controls.Remove(mStatusStrip);
         if (!Controls.Contains(mThemePickerPanel))
            Controls.Add(mThemePickerPanel);
         mThemePickerPanel.PerformLayout();
         mThemePickerPanel.Dock = DockStyle.Fill;
         mThemePickerPanel.Visible = true;
         mThemePickerPanel.BringToFront();
         mThemePickerPanel.Show();
      }

      public void RestoreFromThemePickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mThemePickerPanel, nameof(mThemePickerPanel));
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         ThrowIfNull(mStatusStrip, nameof(mStatusStrip));
         ThrowIfNull(mMenuStrip, nameof(mMenuStrip));
         mForm.SuspendLayout();
         mForm.ControlBox = true;
         mThemePickerPanel.Visible = false;
         mThemePickerPanel.SendToBack();
         if (mForm.Controls.Contains(mThemePickerPanel))
            mForm.Controls.Remove(mThemePickerPanel);
         mThemePickerPanel.Dispose();
         mThemePickerPanel = null;
         mForm.Bounds = mPreThemePickerBounds;
         if (!mForm.Controls.Contains(mRichTextBox))
            mForm.Controls.Add(mRichTextBox);
         if (!mForm.Controls.Contains(mStatusStrip))
            mForm.Controls.Add(mStatusStrip);
         if (mCurrentViewMode == ViewMode.Features && !mForm.Controls.Contains(mMenuStrip))
            mForm.Controls.Add(mMenuStrip);
         mRichTextBox.Visible = true;
         mStatusStrip.Visible = true;
         if (mCurrentViewMode == ViewMode.Features)
            mMenuStrip.Visible = true;
         mForm.Activate();
         mRichTextBox.Focus();
         mForm.ResumeLayout(true);
      }

      public void EnsureGetStringPanel(string pTitle, string pPrompt, string pInitialValue, Action<string?, bool> pCallback) {
         mPreGetStringBounds = Bounds;
         if (mGetStringPanel == null) {
            ThrowIfNull(mForm, nameof(mForm));
            mGetStringPanel = new GetString(pTitle, pPrompt, pInitialValue) {
               OnClose = pCallback
            };
         }
         ShowGetStringPanel();
      }

      public void ShowGetStringPanel() {
         ThrowIfNull(mGetStringPanel, nameof(mGetStringPanel));
         ThrowIfNull(mForm, nameof(mForm));
         mForm.SuspendLayout();
         mForm.ControlBox = false;
         if (mForm.Controls.Contains(mMenuStrip))
            mForm.Controls.Remove(mMenuStrip);
         if (mForm.Controls.Contains(mRichTextBox))
            mForm.Controls.Remove(mRichTextBox);
         if (mForm.Controls.Contains(mStatusStrip))
            mForm.Controls.Remove(mStatusStrip);
         if (!mForm.Controls.Contains(mGetStringPanel))
            mForm.Controls.Add(mGetStringPanel);

         mGetStringPanel.PerformLayout();
         mForm.ResumeLayout(true);

         // Center the panel in the form
         int centerX = (mForm.ClientSize.Width - mGetStringPanel.Width) / 2;
         int centerY = (mForm.ClientSize.Height - mGetStringPanel.Height) / 2;
         mGetStringPanel.Location = new Point(Math.Max(0, centerX), Math.Max(0, centerY));

         // Resize form if GetString panel doesn't fit
         Size requiredSize = new Size(mGetStringPanel.Width + 20, mGetStringPanel.Height + 40);
         if (mForm.ClientSize.Width < requiredSize.Width || mForm.ClientSize.Height < requiredSize.Height) {
            mForm.ClientSize = requiredSize;
            // Re-center after resize
            centerX = (mForm.ClientSize.Width - mGetStringPanel.Width) / 2;
            centerY = (mForm.ClientSize.Height - mGetStringPanel.Height) / 2;
            mGetStringPanel.Location = new Point(Math.Max(0, centerX), Math.Max(0, centerY));
         }
         EnsureWindowFitsMonitor(mForm, pControlBox: false);

         mGetStringPanel.Visible = true;
         mGetStringPanel.BringToFront();
         mGetStringPanel.Show();
         mGetStringPanel.FocusInputTextBox();
      }

      public void RestoreFromGetStringPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mGetStringPanel, nameof(mGetStringPanel));
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         ThrowIfNull(mStatusStrip, nameof(mStatusStrip));
         ThrowIfNull(mMenuStrip, nameof(mMenuStrip));
         mForm.SuspendLayout();
         mForm.ControlBox = true;
         mGetStringPanel.Visible = false;
         mGetStringPanel.SendToBack();
         if (mForm.Controls.Contains(mGetStringPanel))
            mForm.Controls.Remove(mGetStringPanel);
         mGetStringPanel.Dispose();
         mGetStringPanel = null;
         mForm.Bounds = mPreGetStringBounds;
         if (!mForm.Controls.Contains(mRichTextBox))
            mForm.Controls.Add(mRichTextBox);
         if (!mForm.Controls.Contains(mStatusStrip))
            mForm.Controls.Add(mStatusStrip);
         if (mCurrentViewMode == ViewMode.Features && !mForm.Controls.Contains(mMenuStrip))
            mForm.Controls.Add(mMenuStrip);
         mRichTextBox.Visible = true;
         mStatusStrip.Visible = true;
         if (mCurrentViewMode == ViewMode.Features)
            mMenuStrip.Visible = true;
         mForm.Activate();
         mRichTextBox.Focus();
         mForm.ResumeLayout(true);
      }
   }
}
