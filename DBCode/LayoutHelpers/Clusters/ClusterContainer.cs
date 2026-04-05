namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class ClusterContainer : Panel {
         private int mVerticalSpacing = 6, mHorizontalSpacing = 6, mCurrentY = 0, mCurrentX = 0;
         private bool mAutoFlowVertical = true;

         public ClusterContainer(bool pAutoFlowVertical, int pHorizontalSpacing, int pVerticalSpacing, Color? pBackgroundColor) {
            mAutoFlowVertical = pAutoFlowVertical;
            mHorizontalSpacing = pHorizontalSpacing;
            mVerticalSpacing = pVerticalSpacing;
            TabIndex = LayoutHelpers.NextTabIndex();
            AutoSize = true;
            BackColor = pBackgroundColor ?? Color.Transparent;
            Name = $"ClusterContainer{TabIndex}";
         }

         public void AddCluster(BaseCluster pCluster) {
            if (pCluster == null)
               return;

            if (mAutoFlowVertical) {
               PositionClusterVertical(pCluster);
            }
            else {
               PositionClusterHorizontal(pCluster);
            }

            Controls.Add(pCluster);
            UpdateContainerSize(pCluster);
         }

         private void PositionClusterVertical(BaseCluster pCluster) {
            pCluster.Location = new Point(mCurrentX, mCurrentY);
            mCurrentY += pCluster.Height + mVerticalSpacing;
         }

         private void PositionClusterHorizontal(BaseCluster pCluster) {
            pCluster.Location = new Point(mCurrentX, mCurrentY);
            mCurrentX += pCluster.Width + mHorizontalSpacing;
         }

         private void UpdateContainerSize(BaseCluster pCluster) {
            int newWidth = Math.Max(Width, pCluster.Right + mHorizontalSpacing);
            int newHeight = Math.Max(Height, pCluster.Bottom + mVerticalSpacing);
            Size = new Size(newWidth, newHeight);
         }

         public void ResetLayout() {
            mCurrentX = 0;
            mCurrentY = 0;
            Size = new Size(0, 0);
         }

         public void SetVerticalFlow() {
            mAutoFlowVertical = true;
         }

         public void SetHorizontalFlow() {
            mAutoFlowVertical = false;
         }

         public void SetSpacing(int pHorizontal, int pVertical) {
            mHorizontalSpacing = pHorizontal;
            mVerticalSpacing = pVertical;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               foreach (Control control in Controls) {
                  control.Dispose();
               }
               Controls.Clear();
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
