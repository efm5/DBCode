namespace DBCode {
   public sealed partial class MainForm : Form {
      private void PreferencesCloseButton_Click(object pSender, EventArgs pEventArgs) {
         RestoreFromPreferencesPanel();
      }

      private void PreferencesPanel_Layout(object pSender, LayoutEventArgs pEventArgs) {
         int buttonX = 0;
         int buttonY = 0;

         if (mPreferencesCloseButton == null)
            return;

         buttonX = mPreferencesPanel.ClientSize.Width - mPreferencesCloseButton.Width - 16;
         buttonY = mPreferencesPanel.ClientSize.Height - mPreferencesCloseButton.Height - 16;
         mPreferencesCloseButton.Location = new Point(buttonX, buttonY);
      }
   }
}
