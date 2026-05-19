namespace DBCode {
   internal static partial class LayoutHelpers {
      internal class TwoLineHeaderLabelCluster : BaseCluster {
         internal Label mTopLabel, mBottomLabel;
         HeaderLabelSize mTopSizeMultiplier, mBottomSizeMultiplier;

         internal TwoLineHeaderLabelCluster(Theme pTheme, string pTopText, string pBottomText,
            HeaderLabelSize pTopSizeMultiplier = HeaderLabelSize.Normal,
            HeaderLabelSize pBottomSizeMultiplier = HeaderLabelSize.Small,
            Color? pBackgroundColor = null) : base(pTheme, pBackgroundColor) {
            mTopSizeMultiplier = pTopSizeMultiplier;
            mBottomSizeMultiplier = pBottomSizeMultiplier;
            mSkipTheme = true;
            mTopLabel = new Label() {
               Name = $"HeaderLabelCluster{nameof(mTopLabel)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pTopText,
               AutoSize = true,
               TextAlign = ContentAlignment.MiddleCenter
            };
            mBottomLabel = new Label() {
               Name = $"HeaderLabelCluster{nameof(mBottomLabel)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pBottomText,
               AutoSize = true,
               TextAlign = ContentAlignment.MiddleLeft
            };
            Controls.AddRange([mTopLabel, mBottomLabel]);
            SetFontAndColor();
            LayoutCluster();
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Dock = DockStyle.Top;
         }

         public void ResetBottomLabel(string pText) {
            mBottomLabel.Text = pText;
            mBottomLabel.Refresh();
         }

         internal override void LayoutCluster() {
            if ((Parent != null) && (mTopLabel != null) && (mBottomLabel != null)) {
               SetFontAndColor();
               int x = (Parent.ClientSize.Width - mTopLabel.Width) / 2;
               if (x < 0)
                  x = 0;
               mTopLabel.Left = x;
               mTopLabel.Top = mEm;
               mBottomLabel.Left = mIndent;
               mBottomLabel.Top = mTopLabel.Bottom + mEm;
               Height = mBottomLabel.Bottom + mEm;
               mTopLabel.Invalidate();
               mBottomLabel.Invalidate();
               mTopLabel.Refresh();
               mBottomLabel.Refresh();
            }
         }

         internal override void LayoutControlsOnly() {
            if ((Parent != null) && (mTopLabel != null) && (mBottomLabel != null)) {
               int x = (Parent.ClientSize.Width - mTopLabel.Width) / 2;
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

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            UpdateFont(mTopLabel, CreateNewTitleFont(poFont, mTopSizeMultiplier));
            mTopLabel.ForeColor = poForeColor;
            mTopLabel.BackColor = poBackColor;
            UpdateFont(mBottomLabel, CreateNewTitleFont(poFont, mBottomSizeMultiplier));
            mBottomLabel.ForeColor = poForeColor;
            mBottomLabel.BackColor = poBackColor;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               MainForm.DisposeFontIfOwned(mTopLabel.Font);
               MainForm.DisposeFontIfOwned(mBottomLabel.Font);
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
