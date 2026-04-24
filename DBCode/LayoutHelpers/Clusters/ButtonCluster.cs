namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class ButtonCluster : Button {
         internal ButtonCluster(string pText, Color? pBackgroundColor = null) {
            Name = $"ButtonCluster{mTabIndex}";
            TabIndex = mTabIndex++;
            Text = pText;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Font = CreateNewFont();
            ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont];
            BackColor = pBackgroundColor ?? Color.Transparent;
         }

         public void SetFontAndColor(Font pFont, Color pForeColor, Color pBackColor) {
            Font = pFont;
            ForeColor = pForeColor;
            BackColor = pBackColor;
         }
      }
   }
}
