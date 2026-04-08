namespace DBCode {
   internal static partial class LayoutHelpers {
      internal abstract class BaseCluster : Panel {
         protected LabelPosition mLabelPosition = LabelPosition.Left;

         public BaseCluster(Color? pBackgroundColor) {
            BackColor = pBackgroundColor ?? Color.Transparent;
            AutoSize = true;
            TabIndex = LayoutHelpers.TAB_INDEX_IGNORED;
            Name = $"BaseCluster{mTabIndex++}";
         }

         protected void ApplyLabelPosition(Label pLabel, Control pControl) {
            if (mLabelPosition == LabelPosition.Left) {
               pLabel.Location = new Point(0, 0);
               pControl.Location = new Point((pLabel.Width + mEm), 0);
            }
            else if (mLabelPosition == LabelPosition.Right) {
               pControl.Location = new Point(0, 0);
               pLabel.Location = new Point((pControl.Width + mEm), 0);
            }
            else if (mLabelPosition == LabelPosition.Top) {
               pLabel.Location = new Point(0, 0);
               pControl.Location = new Point(0, (pLabel.Height + mEm));
            }
            else {
               pControl.Location = new Point(0, 0);
               pLabel.Location = new Point(0, (pControl.Height + mEm));
            }
         }

         protected void ApplyLabelPosition(Label pLabel, IEnumerable<Control> pControls) {
            //if (mLabelPosition == LabelPosition.Left) {
            //   pLabel.Location = new Point(0, 0);
            //   pControl.Location = new Point((pLabel.Width + mEm), 0);
            //}
            //else if (mLabelPosition == LabelPosition.Right) {
            //   pControl.Location = new Point(0, 0);
            //   pLabel.Location = new Point((pControl.Width + mEm), 0);
            //}
            //else if (mLabelPosition == LabelPosition.Top) {
            //   pLabel.Location = new Point(0, 0);
            //   pControl.Location = new Point(0, (pLabel.Height + mEm));
            //}
            //else {
            //   pControl.Location = new Point(0, 0);
            //   pLabel.Location = new Point(0, (pControl.Height + mEm));
            //}
         }

         public void RefreshControls() {
            foreach (Control control in Controls.OfType<Control>())
               control.Refresh();
         }

         protected virtual void LayoutCluster() {
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
