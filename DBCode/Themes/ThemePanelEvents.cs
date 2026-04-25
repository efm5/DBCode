namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePanel : Panel {
         private void PrimaryTabControl_DrawItem(object? pSender, DrawItemEventArgs pArgs) {
            DrawTabControlItem(mPrimaryTabControl, pArgs);
         }

         private void HighlightTabControl_DrawItem(object? pSender, DrawItemEventArgs pArgs) {
            DrawTabControlItem(mHighlightTabControl, pArgs);
         }

         private void PrimaryTabControl_SelectedIndexChanged(object? pSender, EventArgs pArgs) {
            mUiState.mThemePrimaryTabPageIndex = mPrimaryTabControl.SelectedIndex;
         }

         private void HighlightTabControl_SelectedIndexChanged(object? pSender, EventArgs pArgs) {
            mUiState.mThemeHighlightTabPageIndex = mHighlightTabControl.SelectedIndex;
         }

         private void NewButton_Click(object? pSender, EventArgs pArgs) {
            //DEBUG efm5 2026 04 7 reset temporary theme to defaults
         }

         private void CloneButton_Click(object? pSender, EventArgs pArgs) {
            //DEBUG efm5 2026 04 7 clone current theme to temporary theme
         }

         private void ApplyButton_Click(object? pSender, EventArgs pArgs) {
            ThrowIfNull(mForm, nameof(mForm));
            (mForm as MainForm)?.EnsureGetStringPanel(
               "Enter Theme Name",
               "Please enter a name for the new theme:",
               mTemporaryTheme.mName,
               ApplyThemeCallback
            );
         }

         private void ApplyThemeCallback(string? pResult, bool pWasCancelled) {
            ThrowIfNull(mForm, nameof(mForm));
            (mForm as MainForm)?.RestoreFromGetStringPanel();
            if (!pWasCancelled && !string.IsNullOrWhiteSpace(pResult)) {
               string newName = pResult;
               //DEBUG efm5 2026 04 25 Eventually, but not now add the new theme to the list of themes and make it the current theme
            }
         }

         private void CancelButton_Click(object? pSender, EventArgs pArgs) {
            CloseThemePanel();
         }

         private void OnColorSwatchClicked(object? pSender, ColorSwatchUsage pUsage) {
            LabeledButtonColorSwatchCluster? swatch = pSender as LabeledButtonColorSwatchCluster;
            if (swatch == null)
               return;
            EnsureColorPickerPanel(mTemporaryTheme, (ColorUsage)pUsage, swatch.GetColor());
         }

         private void OnFontButtonClicked(object? pSender, EventArgs pArgs) {
            Button? button = pSender as Button;
            if (button?.Tag is not FontUsage usage)
               return;
            EnsureFontPickerPanel(mTemporaryTheme, usage, mTemporaryTheme.mFonts[(int)usage]);
         }
      }
   }
}
