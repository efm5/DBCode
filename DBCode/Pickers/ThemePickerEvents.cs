namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePickerPanel : Panel {
         protected override void OnHandleCreated(EventArgs pEventArgs) {
            base.OnHandleCreated(pEventArgs);
            ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
            Dock = DockStyle.Fill;
            CreateLayout();
            mClusterContainer.Dock = DockStyle.Fill;
            PerformLayout();
            mClusterContainer.PerformLayout();
         }

         private void CancelButton_Click(object? pSender, EventArgs pArgs) {
            CloseThemePickerPanel();
         }

         private void PickThemeButton_Click(object? pSender, EventArgs pArgs) {
            ThrowIfNull(mForm, nameof(mForm));
            Button? button = pSender as Button;
            if (button == null)
               return;
            Theme? theme = button.Tag as Theme;
            if (theme != null)
               mCurrentTheme = theme.Clone();
            CloseThemePickerPanel();
            mForm.ApplyTheme();
         }
      }
   }
}
