namespace DBCode {
   internal sealed partial class TargetPickerPanel : ScrollablePanel {
      protected override void OnHandleCreated(EventArgs pEventArgs) {
         base.OnHandleCreated(pEventArgs);
         ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
         ThrowIfNull(mBottomPanel, nameof(mBottomPanel));
         mForm.SuspendClientSizeChanged();
         mForm.Bounds = mUiState.mTargetPickerBounds;
         if (mUiState.mTargetPickerFirstShow) {
            CenterFormOnMonitor(mForm);
            mUiState.mTargetPickerFirstShow = false;
         }
         Size = new Size(mForm.Size.Width - 20, mForm.Size.Height - 20);
         CreateLayout();
         ApplyTarget();
         Controls.AddRange([mClusterContainer, mTitleLabel, mBottomPanel]);
         mClusterContainer.PerformLayout();
         LayoutPanel();
         mForm.ResumeClientSizeChanged();
      }

      private void CancelButton_Click(object? pSender, EventArgs pArgs) {
         mTargetSelected = false;
         CloseTargetPickerPanel();
      }

      private void PickTargetButton_Click(object? pSender, EventArgs pArgs) {
         ThrowIfNull(mForm, nameof(mForm));
         Button? button = pSender as Button;
         if (button == null)
            return;
         TargetWindow? target = button.Tag as TargetWindow;
         if (target != null) {
            mTargetWindow = target.mWindowHandle;
            mTargetWindowName = target.ProposeDragonFriendlyName();
            mIsTargetingEnabled = true;
            mTargetSelected = true;
         }
         CloseTargetPickerPanel();
      }
   }
}
