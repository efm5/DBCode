using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class CheckBoxCluster : BaseCluster {
         internal CheckBox mCheckBox;

         internal CheckBoxCluster(Theme pTheme, string pText, Color? pBackgroundColor = null)
            : base(pTheme, pBackgroundColor) {
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

         internal override void LayoutCluster() {
            SetFontAndColor();
            mCheckBox.Location = new Point(0, 0);
            mCheckBox.Invalidate();
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mCheckBox.Font = CreateNewFont(poFont);
            mCheckBox.ForeColor = poForeColor;
            mCheckBox.BackColor = poBackColor;
         }
      }
   }
}
