
using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabelCluster : BaseCluster {
         internal Label mLabel;

         internal LabelCluster(string pText, Color? pBackgroundColor = null)
            : base(pBackgroundColor) {
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

         internal override void LayoutCluster(Theme pTheme) {
            SetFontAndColor(pTheme);
         }

         public void SetFontAndColor(Theme pTheme) {
            Theme.ThemeInterfaceThings(pTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel.Font = poFont;
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
         }
      }
   }
}
