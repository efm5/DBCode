namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePickerPanel : Panel {
         protected override void OnHandleCreated(EventArgs pEventArgs) {
            base.OnHandleCreated(pEventArgs);
            CreateLayout();
            Dock = DockStyle.Fill;
         }

         private void CancelButton_Click(object? pSender, EventArgs pArgs) {
            CloseThemePickerPanel();
         }

         private void PickThemeButton_Click(object? pSender, EventArgs pArgs) {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            Button? button = pSender as Button;
            if (button == null)
               return;
            Theme? theme = button.Tag as Theme;
            if (theme != null) {
               CloseThemePickerPanel();
               mCurrentTheme = theme.Clone();
               mForm.ApplyThemeToMainForm();
            }
            CloseThemePickerPanel();
         }
      }
   }
}
