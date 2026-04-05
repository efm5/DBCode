namespace DBCode {
   internal static partial class LayoutHelpers {
      internal abstract class BaseCluster : Panel {
         protected LabelPosition mLabelPosition = LabelPosition.Left;
         protected Color? mBackgroundColor = null;

         public BaseCluster(Color? pBackgroundColor) {
            mBackgroundColor = pBackgroundColor;
            BackColor = mBackgroundColor ?? Color.Transparent;
            AutoSize = true;
            TabIndex = LayoutHelpers.NextTabIndex();
            Name = $"BaseCluster{TabIndex}";
         }

         protected void ApplyLabelPosition(Label pLabel, Control pControl) {
            int labelWidth = pLabel.Width;
            int controlWidth = pControl.Width;
            int controlHeight = pControl.Height;
            int labelHeight = pLabel.Height;
            //DEBUG efm5 2026 04 4 use dynamic padding
            if (mLabelPosition == LabelPosition.Left) {
               pLabel.Location = new Point(0, (controlHeight - labelHeight) / 2);
               pControl.Location = new Point(labelWidth + 6, 0);
            }
            else if (mLabelPosition == LabelPosition.Right) {
               pControl.Location = new Point(0, 0);
               pLabel.Location = new Point(controlWidth + 6, (controlHeight - labelHeight) / 2);
            }
            else if (mLabelPosition == LabelPosition.Top) {
               pLabel.Location = new Point(0, 0);
               pControl.Location = new Point(0, labelHeight + 4);
            }
            else {
               pControl.Location = new Point(0, 0);
               pLabel.Location = new Point(0, controlHeight + 4);
            }
         }

         protected void FinalizeSize(Label pLabel, Control pControl) {
            int width = 0;
            int height = 0;
            //DEBUG efm5 2026 04 4 use dynamic padding
            if (mLabelPosition == LabelPosition.Left || mLabelPosition == LabelPosition.Right) {
               width = pLabel.Width + pControl.Width + 6;
               height = Math.Max(pLabel.Height, pControl.Height);
            }
            else {
               width = Math.Max(pLabel.Width, pControl.Width);
               height = pLabel.Height + pControl.Height + 4;
            }
            Size = new Size(width, height);
         }

         public LabelPosition GetLabelPosition() {
            return mLabelPosition;
         }

         public virtual int GetHeight() {
            int returnValue = 0;
            //DEBUG efm5 2026 04 4 do the math
            return returnValue;
         }

         public virtual int GetWidth() {
            int returnValue = 0;
            //DEBUG efm5 2026 04 4 do the math
            return returnValue;
         }

         internal virtual void PositionControlPair() {
            //DEBUG efm5 2026 04 4 do the math – Based on label position and the assumption that there are only two controls
         }

         public void SetLabelPosition(LabelPosition pPosition) {
            mLabelPosition = pPosition;
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
