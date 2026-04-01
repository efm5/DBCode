namespace DBCode {
   public sealed partial class MainForm : Form {
      private void ThemeCloseButton_Click(object? pSender, EventArgs pEventArgs) {
         RestoreFromThemePanel();
      }

      private void ThemePanel_Layout(object? pSender, LayoutEventArgs pEventArgs) {
         int buttonX = 0;
         int buttonY = 0;

         if ((mThemeCloseButton == null) || (mThemePanel == null))
            return;
         buttonX = mThemePanel.ClientSize.Width - mThemeCloseButton.Width - 16;
         buttonY = mThemePanel.ClientSize.Height - mThemeCloseButton.Height - 16;
         mThemeCloseButton.Location = new Point(buttonX, buttonY);
      }
   }
}
