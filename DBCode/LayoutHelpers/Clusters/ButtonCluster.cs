namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class ButtonCluster : BaseCluster {
         internal Button? mButton;

         internal ButtonCluster(Theme pTheme, string pText, Color? pBackgroundColor = null)
            : base(pTheme, pBackgroundColor) {
            mButton = new Button() {
               Name = $"ButtonCluster{mTabIndex}",
               TabIndex = mTabIndex++,
               Text = pText,
               Location = new Point(1, 1),
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(pTheme.mFonts[(int)FontUsage.Interface]),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            Controls.Add(mButton);
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
            LayoutControls();
            mButton?.Invalidate();
         }

         internal void LayoutControls() {
            //DEBUG efm5 2026 04 4 Maybe nothing to do
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mButton!.Font = CreateNewFont(poFont);
            mButton.ForeColor = poForeColor;
            mButton.BackColor = poBackColor;
         }
      }
   }
}
