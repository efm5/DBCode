using DBCode.Themes;

namespace DBCode {
   public sealed partial class MainForm : Form {
      public void EnsureThemePanel(ThemeUsage pThemeUsage) {
         if (pThemeUsage == ThemeUsage.Pick) { }//DEBUG efm5 2026 04 6 implement
         else {
            mPreThemeBounds = Bounds;
            Bounds = new Rectangle(Settings.Default.ThemeLocation, Settings.Default.ThemeSize);
            if (mThemePanel == null)
               mThemePanel = new ThemePanel(pThemeUsage);
            ShowThemePanel(pThemeUsage);
         }
      }

      public void ShowThemePanel(ThemeUsage pThemeUsage) {
         if (pThemeUsage == ThemeUsage.Design) {
            if (mThemePanel == null)
               return;
            ControlBox = false;
            if (Controls.Contains(mMenuStrip))
               Controls.Remove(mMenuStrip);
            if (Controls.Contains(mRichTextBox))
               Controls.Remove(mRichTextBox);
            if (Controls.Contains(mStatusStrip))
               Controls.Remove(mStatusStrip);
            if (!Controls.Contains(mThemePanel))
               Controls.Add(mThemePanel);
            mThemePanel.PerformLayout();
            mThemePanel.Dock = DockStyle.Fill;
            mThemePanel.Visible = true;
            mThemePanel.BringToFront();
            mThemePanel.Show();
         }
         else {
            TimedMessage("ShowThemePanel(ThemeUsage) neither edit nor pick are working.", "Not Yet IMPLEMENTED");
         }
      }

      public static void RestoreFromThemePanel() {
         if (mForm == null)
            return;
         mForm.ControlBox = true;
         if (mThemePanel != null) {
            mThemePanel.SaveSettings();
            mThemePanel.Visible = false;
            mThemePanel.SendToBack();
            if (mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Remove(mThemePanel);
         }
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
      }
   }
}
