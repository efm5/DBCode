namespace DBCode {
   internal static partial class LayoutHelpers {

      internal sealed class LabeledColorSwatchCluster : BaseCluster {
         private ColorSwatch mSwatch;
         private Label mLabel;

         public LabeledColorSwatchCluster(Theme pTheme, string pLabelText, LabelPosition pLabelPosition, Color pInitialColor)
            : base(pTheme, null) {
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               TextAlign = ContentAlignment.MiddleCenter,
               Font = CreateNewFont()
            };
            mSwatch = new ColorSwatch(ColorPickerSwatchUsage.Demo, pInitialColor, 50);
            mSwatch.SetSize(mEm3);
            Controls.AddRange([mLabel, mSwatch]);
            LayoutControls();
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
            LayoutControls();
            mLabel.Invalidate();
            mSwatch.Invalidate();
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel.Font = CreateNewFont(poFont);
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
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
