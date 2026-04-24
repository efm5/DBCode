using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {

      internal abstract class BaseCluster : Panel {
         protected LabelPosition mLabelPosition = LabelPosition.Left;
         private static int mNextClusterId = 1;
         internal bool mSkipTheme = false;

         protected BaseCluster(Color? pBackgroundColor) {
            BackColor = pBackgroundColor ?? Color.Transparent;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
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
               pControl.Location = new Point(pLabel.Width, 0);
            }
            else if (mLabelPosition == LabelPosition.Right) {
               pControl.Location = new Point(0, 0);
               pLabel.Location = new Point(pControl.Width, 0);
            }
            else if (mLabelPosition == LabelPosition.Top) {
               pLabel.Location = new Point(0, 0);
               pControl.Location = new Point(0, pLabel.Height + mEmHalf);
            }
            else {
               pControl.Location = new Point(0, 0);
               pLabel.Location = new Point(0, pControl.Height + mEmHalf);
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
               int x = pLabel.Width + mEm;
               bool isFirstControl = true;
               foreach (Control control in controls) {
                  control.Location = new Point(x, 0);
                  if (isFirstControl)
                     x += control.Width + mEm;
                  else
                     x += control.Width + mEm3;
                  isFirstControl = false;
               }
            }
            else if (mLabelPosition == LabelPosition.Right) {
               int x = 0;
               bool isFirstControl = true;
               foreach (Control control in controls) {
                  control.Location = new Point(x, 0);
                  if (isFirstControl)
                     x += control.Width + mEm;
                  else
                     x += control.Width + mEm3;
                  isFirstControl = false;
               }
               pLabel.Location = new Point(x, 0);
            }
            else if (mLabelPosition == LabelPosition.Top) {
               pLabel.Location = new Point(0, 0);
               int y = pLabel.Height + mEm;
               bool isFirstControl = true;
               foreach (Control control in controls) {
                  control.Location = new Point(0, y);
                  if (isFirstControl)
                     y += control.Height + mEm;
                  else
                     y += control.Height + mEm3;
                  isFirstControl = false;
               }
            }
            else {
               int y = 0;
               bool isFirstControl = true;
               foreach (Control control in controls) {
                  control.Location = new Point(0, y);
                  if (isFirstControl)
                     y += control.Height + mEm;
                  else
                     y += control.Height + mEm3;
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
