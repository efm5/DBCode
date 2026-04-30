namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabelCluster : BaseCluster {
         internal Label mLabel;

         internal LabelCluster(Theme pTheme, string pText, Color? pBackgroundColor = null)
            : base(pTheme, pBackgroundColor) {
            mLabel = new Label() {
               Name = $"LabelCluster{nameof(mLabel)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pText,
               AutoSize = true,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent,
               Location = new Point(0, 0)
            };
            Controls.Add(mLabel);
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel.Font = CreateNewFont(poFont);
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
         }
      }
   }
}
