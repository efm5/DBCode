namespace DBCode {
   public sealed partial class MainForm : Form {
      private void EnsurePreferencesPanel() {
         if (mPreferencesPanel != null)
            return;

         mPreferencesPanel = new Panel();
         mPreferencesCloseButton = new Button();

         mPreferencesPanel.Size = new Size(1000, 800);
         mPreferencesPanel.BackColor = SystemColors.ControlDark;
         mPreferencesPanel.Name = "preferencesPanel";

         mPreferencesCloseButton.Text = "&Close";
         mPreferencesCloseButton.AutoSize = true;
         mPreferencesCloseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
         mPreferencesCloseButton.Name = "preferencesCloseButton";
         mPreferencesCloseButton.Click += PreferencesCloseButton_Click;

         mPreferencesPanel.Controls.Add(mPreferencesCloseButton);
         mPreferencesPanel.Layout += PreferencesPanel_Layout;
      }

      private void ShowPreferencesPanel() {
         Rectangle screenBounds = Screen.FromControl(this).WorkingArea;
         int newWidth = mPreferencesPanel.Width;
         int newHeight = mPreferencesPanel.Height;
         int newX = screenBounds.Left + (screenBounds.Width - newWidth) / 2;
         int newY = screenBounds.Top + (screenBounds.Height - newHeight) / 2;

         EnsurePreferencesPanel();

         mPrePreferencesBounds = Bounds;
         Bounds = new Rectangle(newX, newY, newWidth, newHeight);

         if (Controls.Contains(mMenuStrip))
            Controls.Remove(mMenuStrip);
         if (Controls.Contains(mMainTextBox))
            Controls.Remove(mMainTextBox);
         if (Controls.Contains(mStatusStrip))
            Controls.Remove(mStatusStrip);
         if (!Controls.Contains(mPreferencesPanel))
            Controls.Add(mPreferencesPanel);

         mPreferencesPanel.Visible = true;
         mPreferencesPanel.BringToFront();
      }

      private void RestoreFromPreferencesPanel() {
         Bounds = mPrePreferencesBounds;

         if (mPreferencesPanel != null) {
            mPreferencesPanel.Visible = false;
            mPreferencesPanel.SendToBack();
            if (Controls.Contains(mPreferencesPanel))
               Controls.Remove(mPreferencesPanel);
         }

         if (!Controls.Contains(mMainTextBox))
            Controls.Add(mMainTextBox);
         if (!Controls.Contains(mStatusStrip))
            Controls.Add(mStatusStrip);
         if (mCurrentViewMode == ViewMode.Features && !Controls.Contains(mMenuStrip))
            Controls.Add(mMenuStrip);

         mMainTextBox.Visible = true;
         mStatusStrip.Visible = true;
         if (mCurrentViewMode == ViewMode.Features)
            mMenuStrip.Visible = true;
      }
   }
}
