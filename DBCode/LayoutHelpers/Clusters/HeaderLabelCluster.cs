namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class HeaderLabelCluster : BaseCluster {
         internal Label mLabel;

         internal HeaderLabelCluster(string pText, HeaderLabelSize pSizeMultiplier, Color? pBackgroundColor = null)
            : base(pBackgroundColor) {
            mSkipTheme = true;
            mLabel = new Label() {
               Name = $"HeaderLabelCluster{nameof(mLabel)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pText,
               AutoSize = true,
               Font = CreateNewTitleFont(pSizeMultiplier),
               TextAlign = ContentAlignment.MiddleCenter,
               BackColor = pBackgroundColor ?? Color.Transparent,
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont]
            };
            Controls.Add(mLabel);
            Dock = DockStyle.Top;
         }

         protected override void OnLayout(LayoutEventArgs pArgs) {
            base.OnLayout(pArgs);
            if (mLabel != null) {
               int x = (Width - mLabel.Width) / 2;
               if (x < 0)
                  x = 0;
               mLabel.Left = x;
               mLabel.Top = mEm;
               Height = mLabel.Bottom + mEm;
            }
         }
      }
   }
}
