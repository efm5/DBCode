namespace DBCode {
   public sealed partial class MainForm : Form {
      #region main form
      private void MainForm_Load(object pSender, EventArgs pEventArgs) {
         Size savedSize = Settings.Default.FormSize;
         Point savedLocation = Settings.Default.FormLocation;
         double savedOpacity = Settings.Default.FormOpacity;

         if (!savedSize.IsEmpty)
            Size = savedSize;
         if (!savedLocation.IsEmpty) {
            StartPosition = FormStartPosition.Manual;
            Location = savedLocation;
         }
         if (savedOpacity < 0.0 || savedOpacity > 1.0)
            savedOpacity = 1.0;

         Opacity = savedOpacity;
         UpdateOpacityMenuChecks(savedOpacity);
         ApplyViewMode(ViewMode.Features);
      }

      private void MainForm_FormClosing(object pSender, FormClosingEventArgs pEventArgs) {
         Settings.Default.FormOpacity = Opacity;
         Settings.Default.FormSize = Size;
         Settings.Default.FormLocation = Location;
         Settings.Default.Save();
      }
      #endregion

      private void TargetedTSMI_Click(object pSender, EventArgs pEventArgs) {
         if (pSender == null)
            return;

         if (mTargetedTSMI.Checked)
            EnterTargetedMode();
         else
            EnterUntargetedMode();
      }

      private void RetargetTSMI_Click(object pSender, EventArgs pEventArgs) {
         mTargetWindowName = "Retargeted Window";
         mIsTargetingEnabled = true;
         mTargetedTSMI.Checked = true;
         UpdateTargetingStatusLabel();
      }

      private void VisibilityTSMI_Click(object pSender, EventArgs pEventArgs) {
         ToolStripMenuItem clickedTSMI = pSender as ToolStripMenuItem;
         object tagObject = clickedTSMI == null ? null : clickedTSMI.Tag;
         double opacityValue = 0.0;

         if (clickedTSMI == null)
            return;
         if (tagObject == null)
            return;
         if (!double.TryParse(tagObject.ToString(), out opacityValue))
            return;

         Opacity = opacityValue;
         UpdateOpacityMenuChecks(opacityValue);
      }

      private void MinimalTSMI_Click(object pSender, EventArgs pEventArgs) {
         ApplyViewMode(ViewMode.Minimal);
      }

      private void FeaturesTSMI_Click(object pSender, EventArgs pEventArgs) {
         ApplyViewMode(ViewMode.Features);
      }

      private void ReturnToTopTSMI_Click(object pSender, EventArgs pEventArgs) {
         mReturnToTop = mReturnToTopTSMI.Checked;
      }

      private void PreferencesMenuItem_Click(object pSender, EventArgs pEventArgs) {
         ShowPreferencesPanel();
      }

      private void HelpMenuItem_Click(object pSender, EventArgs pEventArgs) {
         string messageText = "Help is not yet implemented.";
         string captionText = "Help";

         MessageBox.Show(this, messageText, captionText, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }

      private void TransMove_Click(object pSender, EventArgs pEventArgs) {
         ToolStripButton toolStripButton = pSender as ToolStripButton;
         if (toolStripButton == null)
            return;
         PasteMode pasteMode = toolStripButton == mTransferTSB ? PasteMode.Transfer : PasteMode.Transport;

         Paste(pasteMode);
         if (mReturnToTop) {
            TopMost = true;
            TopMost = false;
         }
      }

      private void RevertTSB_Click(object pSender, EventArgs pEventArgs) {
         ApplyViewMode(ViewMode.Features);
      }

      private void ExitTSB_Click(object pSender, EventArgs pEventArgs) {
         Close();
      }
   }
}
