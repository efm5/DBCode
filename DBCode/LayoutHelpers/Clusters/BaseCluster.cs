using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {

      internal abstract class BaseCluster : Panel {
         protected LabelPosition mLabelPosition = LabelPosition.Left;
         private static int mNextClusterId = 1;
         internal bool mSkipTheme = false;

         protected BaseCluster(Color? pBackgroundColor) {
            BackColor = pBackgroundColor ?? Color.Transparent;
            //AutoSize = true;
            //AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TabStop = false;
            TabIndex = TAB_INDEX_IGNORED;
            Name = $"BaseCluster{mNextClusterId}";
            mNextClusterId++;
         }

         protected void GlueControlsHorizontally(Control pFirstControl, Control pSecondControl, int pSpacing) {
            pSecondControl.Location = new Point(pFirstControl.Right + pSpacing, pFirstControl.Top);
         }

         protected void GlueControlsVertically(Control pFirstControl, Control pSecondControl, int pSpacing) {
            pSecondControl.Location = new Point(pFirstControl.Left, pFirstControl.Bottom + pSpacing);
         }

         protected void ApplyLabelPosition(Label pLabel, Control pControl) {
            if (mLabelPosition == LabelPosition.Left) {
               pLabel.Location = new Point(0, 0);
               pControl.Location = new Point(pLabel.Right + mEm, 0);
            }
            else if (mLabelPosition == LabelPosition.Right) {
               pControl.Location = new Point(0, 0);
               pLabel.Location = new Point(pControl.Right + mEm, 0);
            }
            else if (mLabelPosition == LabelPosition.Top) {
               pLabel.Location = new Point(0, 0);
               pControl.Location = new Point(0, pLabel.Bottom + mEmHalf);
            }
            else {
               pControl.Location = new Point(0, 0);
               pLabel.Location = new Point(0, pControl.Bottom + mEmHalf);
            }
         }

         protected void ApplyLabelPosition(Label pLabel, Control pControl1, Control pControl2) {
            if (mLabelPosition == LabelPosition.Left) {
               pLabel.Location = new Point(0, 0);
               pControl1.Location = new Point(pLabel.Right + mEm, 0);
               pControl2.Location = new Point(pControl1.Right + mEm, 0);
            }
            else if (mLabelPosition == LabelPosition.Right) {
               pControl1.Location = new Point(0, 0);
               pControl2.Location = new Point(pControl1.Right + mEm, 0);
               pLabel.Location = new Point(pControl2.Right + mEm, 0);
               int deltaBug = 0;//DEBUG efm5 2026 04 24 testing
            }
            else if (mLabelPosition == LabelPosition.Top) {
               pLabel.Location = new Point(0, 0);
               pControl1.Location = new Point(0, pLabel.Bottom + mEmHalf);
               pControl2.Location = new Point(pControl1.Right + mEm, pControl1.Top);
            }
            else {
               pControl1.Location = new Point(0, 0);
               pControl2.Location = new Point(pControl1.Right + mEm, 0);
               pLabel.Location = new Point(0, pControl1.Bottom + mEmHalf);
            }
         }

#pragma warning disable IDE0305
         protected void ApplyLabelPosition(Label pLabel, IEnumerable<Control> pControls) {
            List<Control> controlList = [.. pControls];
            Control[] controls = controlList.ToArray();
            if (controls.Length == 0)
               return;
            if (mLabelPosition == LabelPosition.Left) {
               pLabel.Location = new Point(0, 0);
               int x = pLabel.Right + mEm;
               bool isFirstControl = true;
               foreach (Control control in controls) {
                  control.Location = new Point(x, 0);
                  if (isFirstControl)
                     x += control.Right + mEm;
                  else
                     x += control.Right + mEm3;
                  isFirstControl = false;
               }
            }
            else if (mLabelPosition == LabelPosition.Right) {
               int x = 0;
               bool isFirstControl = true;
               foreach (Control control in controls) {
                  control.Location = new Point(x, 0);
                  if (isFirstControl)
                     x += control.Right + mEm;
                  else
                     x += control.Right + mEm3;
                  isFirstControl = false;
               }
               pLabel.Location = new Point(x, 0);
            }
            else if (mLabelPosition == LabelPosition.Top) {
               pLabel.Location = new Point(0, 0);
               int y = pLabel.Bottom + mEm;
               bool isFirstControl = true;
               foreach (Control control in controls) {
                  control.Location = new Point(0, y);
                  if (isFirstControl)
                     y += control.Bottom + mEm;
                  else
                     y += control.Bottom + mEm3;
                  isFirstControl = false;
               }
            }
            else {
               int y = 0;
               bool isFirstControl = true;
               foreach (Control control in controls) {
                  control.Location = new Point(0, y);
                  if (isFirstControl)
                     y += control.Bottom + mEm;
                  else
                     y += control.Bottom + mEm3;
                  isFirstControl = false;
               }
               pLabel.Location = new Point(0, y);
            }
         }
#pragma warning restore IDE0305

         public void RefreshControls() {
            foreach (Control control in Controls)
               control.Refresh();
         }

         internal abstract void LayoutCluster(Theme pTheme);

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               foreach (Control control in Controls)
                  control.Dispose();
               Controls.Clear();
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
