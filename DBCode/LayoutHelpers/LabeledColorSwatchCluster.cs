namespace DBCode {
   internal static partial class LayoutHelpers {

      internal sealed class LabeledColorSwatchCluster : BaseCluster {
         private ColorSwatch mSwatch;
         private Label mLabel;

         public LabeledColorSwatchCluster(string pLabelText, LabelPosition pLabelPosition, Color pInitialColor) :
            base(null) {
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               TextAlign = ContentAlignment.MiddleCenter,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont],
               BackColor = Color.Transparent
            };
            mSwatch = new ColorSwatch(ColorSwatchUsage.Demo, pInitialColor, 50, null);
            mSwatch.SetSize(mEm3);
            Controls.AddRange(mLabel, mSwatch);
            LayoutControls();
         }

         protected override void OnLayout(LayoutEventArgs pArgs) {
            base.OnLayout(pArgs);
            LayoutControls();
         }

         protected override void LayoutCluster() {
            // Layout handled in LayoutControls()
         }

         private void LayoutControls() {
            if (mLabelPosition == LabelPosition.Left) {
               mLabel.Left = 0;
               mSwatch.Left = mLabel.Right + mEm;
               mSwatch.Top = 0;
               mLabel.Top = (mSwatch.Height / 2) - (mLabel.Height / 2);
            }
            else if (mLabelPosition == LabelPosition.Right) {
               mSwatch.Left = 0;
               mSwatch.Top = 0;
               mLabel.Left = mSwatch.Right + mEm;
               mLabel.Top = (mSwatch.Height / 2) - (mLabel.Height / 2);
            }
            else if (mLabelPosition == LabelPosition.Top) {
               mLabel.Top = 0;
               mSwatch.Left = 0;
               mSwatch.Top = mLabel.Bottom + mEmHalf;
               mLabel.Left = (mSwatch.Width / 2) - (mLabel.Width / 2);
            }
            else if (mLabelPosition == LabelPosition.Bottom) {
               mSwatch.Left = 0;
               mSwatch.Top = 0;
               mLabel.Left = (mSwatch.Width / 2) - (mLabel.Width / 2);
               mLabel.Top = mSwatch.Bottom + mEmHalf;
            }
         }

         public void SetSize(int pSize) {
            mSwatch.SetSize(pSize);
            LayoutControls();
         }

         public Color GetColor() {
            return mSwatch.GetColor();
         }
      }
   }
}
