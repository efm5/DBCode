using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal class TwoLineHeaderLabelCluster : BaseCluster {
         internal Label mTopLabel, mBottomLabel;

         internal TwoLineHeaderLabelCluster(string pTopText, string pBottomText,
            HeaderLabelSize pTopSizeMultiplier = HeaderLabelSize.Normal,
            HeaderLabelSize pBottomSizeMultiplier = HeaderLabelSize.Small,
            Color? pBackgroundColor = null) : base(pBackgroundColor) {
            mSkipTheme = true;
            mTopLabel = new Label() {
               Name = $"HeaderLabelCluster{nameof(mTopLabel)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pTopText,
               AutoSize = true,
               Font = CreateNewTitleFont(pTopSizeMultiplier),
               TextAlign = ContentAlignment.MiddleCenter,
               BackColor = pBackgroundColor ?? Color.Transparent,
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont]
            };
            mBottomLabel = new Label() {
               Name = $"HeaderLabelCluster{nameof(mBottomLabel)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pBottomText,
               AutoSize = true,
               Font = CreateNewTitleFont(pBottomSizeMultiplier),
               TextAlign = ContentAlignment.MiddleLeft,
               BackColor = pBackgroundColor ?? Color.Transparent,
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont]
            };
            Controls.AddRange(mTopLabel, mBottomLabel);
            Dock = DockStyle.Top;
         }

         public void ResetBottomLabel(string pText) {
            mBottomLabel.Text = pText;
            mBottomLabel.Refresh();
         }

         public void SetFontAndColor(Theme pTheme) {
            Theme.ThemeSimpleThings(pTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mTopLabel.Font = CreateNewTitleFont(poFont, HeaderLabelSize.Normal);
            mTopLabel.ForeColor = poForeColor;
            mTopLabel.BackColor = poBackColor;
            mBottomLabel.Font = CreateNewTitleFont(poFont, HeaderLabelSize.Small);
            mBottomLabel.ForeColor = poForeColor;
            mBottomLabel.BackColor = poBackColor;
         }

         internal override void LayoutCluster(Theme pTheme) {
            SetFontAndColor(pTheme);
            if ((mTopLabel != null) && (mBottomLabel != null)) {
               int x = (Width - mTopLabel.Width) / 2;
               if (x < 0)
                  x = 0;
               mTopLabel.Left = x;
               mTopLabel.Top = mEm;
               mBottomLabel.Left = mEm;
               mBottomLabel.Top = mTopLabel.Bottom + mEm;
               Height = mBottomLabel.Bottom + mEm;
               mTopLabel.Invalidate();
               mBottomLabel.Invalidate();
            }
         }
      }
   }
}
