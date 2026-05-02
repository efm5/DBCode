namespace DBCode {
   internal static partial class LayoutHelpers {
      internal class TwoLineHeaderLabelCluster : BaseCluster {
         internal Label mTopLabel, mBottomLabel;

         internal TwoLineHeaderLabelCluster(Theme pTheme, string pTopText, string pBottomText,
            HeaderLabelSize pTopSizeMultiplier = HeaderLabelSize.Normal,
            HeaderLabelSize pBottomSizeMultiplier = HeaderLabelSize.Small,
            Color? pBackgroundColor = null) : base(pTheme, pBackgroundColor) {
            mSkipTheme = true;
            mTopLabel = new Label() {
               Name = $"HeaderLabelCluster{nameof(mTopLabel)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pTopText,
               AutoSize = true,
               Font = CreateNewTitleFont(pTopSizeMultiplier),
               TextAlign = ContentAlignment.MiddleCenter
            };
            mBottomLabel = new Label() {
               Name = $"HeaderLabelCluster{nameof(mBottomLabel)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pBottomText,
               AutoSize = true,
               Font = CreateNewTitleFont(pBottomSizeMultiplier),
               TextAlign = ContentAlignment.MiddleLeft
            };
            Controls.AddRange([mTopLabel, mBottomLabel]);
            Dock = DockStyle.Top;
         }

         public void ResetBottomLabel(string pText) {
            mBottomLabel.Text = pText;
            mBottomLabel.Refresh();
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mTopLabel.Font = CreateNewTitleFont(poFont, HeaderLabelSize.Normal);
            mTopLabel.ForeColor = poForeColor;
            mTopLabel.BackColor = poBackColor;
            mBottomLabel.Font = CreateNewTitleFont(poFont, HeaderLabelSize.Small);
            mBottomLabel.ForeColor = poForeColor;
            mBottomLabel.BackColor = poBackColor;
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
            if ((mTopLabel != null) && (mBottomLabel != null)) {
               int x = (Width - mTopLabel.Width) / 2;
               if (x < 0)
                  x = 0;
               mTopLabel.Left = x;
               mTopLabel.Top = mEm;
               mBottomLabel.Left = mIndent;
               mBottomLabel.Top = mTopLabel.Bottom + mEm;
               Height = mBottomLabel.Bottom + mEm;
               mTopLabel.Invalidate();
               mBottomLabel.Invalidate();
            }
         }
      }
   }
}
