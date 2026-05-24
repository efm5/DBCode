namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class HeaderLabelCluster : BaseCluster {
         internal Label mLabel;
         private HeaderLabelSize mSizeMultiplier;

         internal HeaderLabelCluster(Theme pTheme, string pText, HeaderLabelSize pSizeMultiplier,
            Color? pBackgroundColor = null) : base(pTheme, pBackgroundColor) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mSizeMultiplier = pSizeMultiplier;
            mSkipTheme = true;
            mLabel = new Label() {
               Name = $"HeaderLabelCluster{nameof(mLabel)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pText,
               AutoSize = true,
               Font = CreateNewTitleFont(pSizeMultiplier),
               TextAlign = ContentAlignment.MiddleCenter,
               BackColor = pBackgroundColor ?? Color.Transparent,
               ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont]
            };
            Controls.Add(mLabel);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Dock = DockStyle.Top;
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
            if (mLabel != null) {
               int x = (Width - mLabel.Width) / 2;
               if (x < 0)
                  x = 0;
               mLabel.Left = x;
               mLabel.Top = mEm;
               Height = mLabel.Bottom + mEm;
               mLabel.Invalidate();
               mLabel.Refresh();
            }
         }

         public void CenterTitle() {
            if (mLabel != null) {
               int x = (Width - mLabel.Width) / 2;
               if (x < 0)
                  x = 0;
               mLabel.Left = x;
               mLabel.Invalidate();
               mLabel.Refresh();
            }
         }

         internal override void LayoutControlsOnly() {
            if (mLabel != null) {
               int x = (Width - mLabel.Width) / 2;
               if (x < 0)
                  x = 0;
               mLabel.Left = x;
               mLabel.Top = mEm;
               Height = mLabel.Bottom + mEm;
               mLabel.Invalidate();
            }
         }

         public void CenterTitle(Panel pPanel) {
            ThrowIfNull(pPanel, nameof(pPanel));
            ThrowIfNull(mLabel, nameof(mLabel));
            int x = (pPanel.ClientSize.Width - mLabel.Width) / 2;
            if (x < 0)
               x = 0;
            mLabel.Left = x;
            mLabel.Invalidate();
            mLabel.Refresh();
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            UpdateFont(mLabel, CreateNewTitleFont(mSizeMultiplier));
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing)
               MainForm.DisposeFontIfOwned(mLabel.Font);
            base.Dispose(pDisposing);
         }
      }
   }
}
