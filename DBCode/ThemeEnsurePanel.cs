namespace DBCode {
   public sealed partial class MainForm : Form {
      private void EnsureThemePanel() {
         if (mThemePanel == null)
            mThemePanel = new Panel();
         ShowThemePanel();
      }

      public void ShowThemePanel() {
         EnsureThemePanel();
         if (mThemePanel == null)
            return;
         Rectangle screenBounds = Screen.FromControl(this).WorkingArea;
         int newWidth = mThemePanel.Width;
         int newHeight = mThemePanel.Height;
         int newX = screenBounds.Left + (screenBounds.Width - newWidth) / 2;
         int newY = screenBounds.Top + (screenBounds.Height - newHeight) / 2;

         mPreThemeBounds = Bounds;
         Bounds = new Rectangle(newX, newY, newWidth, newHeight);
         if (Controls.Contains(mMenuStrip))
            Controls.Remove(mMenuStrip);
         if (Controls.Contains(mRichTextBox))
            Controls.Remove(mRichTextBox);
         if (Controls.Contains(mStatusStrip))
            Controls.Remove(mStatusStrip);
         if (!Controls.Contains(mThemePanel))
            Controls.Add(mThemePanel);

         mThemePanel.Visible = true;
         mThemePanel.BringToFront();
      }

      private void RestoreFromThemePanel() {
         Bounds = mPreThemeBounds;

         if (mThemePanel != null) {
            mThemePanel.Visible = false;
            mThemePanel.SendToBack();
            if (Controls.Contains(mThemePanel))
               Controls.Remove(mThemePanel);
         }

         if (!Controls.Contains(mRichTextBox))
            Controls.Add(mRichTextBox);
         if (!Controls.Contains(mStatusStrip))
            Controls.Add(mStatusStrip);
         if (mCurrentViewMode == ViewMode.Features && !Controls.Contains(mMenuStrip))
            Controls.Add(mMenuStrip);

         mRichTextBox?.Visible = true;
         mStatusStrip?.Visible = true;
         if (mCurrentViewMode == ViewMode.Features)
            mMenuStrip?.Visible = true;
      }
   }
}
