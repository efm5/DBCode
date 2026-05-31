namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePickerPanel : ScrollablePanel {
         protected override void OnHandleCreated(EventArgs pEventArgs) {
            base.OnHandleCreated(pEventArgs);
            ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
            ThrowIfNull(mBottomPanel, nameof(mBottomPanel));
            mForm.SuspendClientSizeChanged();
            mForm.Bounds = mUiState.ThemePickerBounds;
            if (mUiState.mThemePickerFirstShow) {
               CenterFormOnMonitor(mForm);
               mUiState.mThemePickerFirstShow = false;
            }
            Size = new Size(mForm.Size.Width - 20, mForm.Size.Height - 20);
            CreateLayout();
            ApplyTheme();
            BackColor = Color.FromArgb(128, 128, 128);//efm5 use a neutral gray background so that the themed buttons will stand out more
            Controls.AddRange([mClusterContainer, mTitleLabel, mBottomPanel]);
            mClusterContainer.PerformLayout();
            LayoutPanel();
            mForm.ResumeClientSizeChanged();
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
            if (mPickMode == PickMode.Edit) {
               mForm.EnsureThemePanel(ThemeUsage.Edit);
               CloseThemePickerPanel();
            }
            else {
               if (theme != null)
                  mCurrentTheme = theme.Clone();
               mForm.ApplyTheme();
               CloseThemePickerPanel();
            }
         }
      }
   }
}
