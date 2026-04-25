using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class CheckBoxCluster : BaseCluster {
         internal CheckBox mCheckBox;

         internal CheckBoxCluster(string pText, Color? pBackgroundColor = null)
            : base(pBackgroundColor) {

            mCheckBox = new CheckBox() {
               Name = $"CheckBoxCluster{nameof(mCheckBox)}{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pText,
               AutoSize = true,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };

            Controls.Add(mCheckBox);
         }

         internal override void LayoutCluster(Theme pTheme) {
            SetFontAndColor(pTheme);
            mCheckBox.Location = new Point(0, 0);
            mCheckBox.Invalidate();
         }

         public void SetFontAndColor(Theme pTheme) {
            Theme.ThemeInterfaceThings(pTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mCheckBox.Font = poFont;
            mCheckBox.ForeColor = poForeColor;
            mCheckBox.BackColor = poBackColor;
         }
      }
   }
}
