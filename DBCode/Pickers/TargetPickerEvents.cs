namespace DBCode {
   internal sealed partial class TargetPickerPanel : ScrollablePanel {
      protected override void OnHandleCreated(EventArgs pEventArgs) {
         base.OnHandleCreated(pEventArgs);
         ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
         ThrowIfNull(mTargetPickerBottomPanel, nameof(mTargetPickerBottomPanel));
         mForm.SuspendClientSizeChanged();
         mForm.Bounds = mUiState.mTargetPickerBounds;
         if (mUiState.mTargetPickerFirstShow) {
            CenterFormOnMonitor(mForm);
            mUiState.mTargetPickerFirstShow = false;
         }
         Size = new Size(mForm.Size.Width - 20, mForm.Size.Height - 20);
         CreateLayout();
         ApplyTarget();
         Controls.AddRange([mClusterContainer, mTitleLabel, mTargetPickerBottomPanel]);
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
         Target? target = button.Tag as Target;
         if (target != null) {
            mTargetWindow = target.mHandle;
            mTargetWindowName = target.mName;
            mIsTargetingEnabled = true;
            mTargetSelected = true;
         }
         CloseTargetPickerPanel();
      }
   }
}
