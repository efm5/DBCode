using static DBCode.Themes.ThemeBrightnessHelper;

namespace DBCode.Themes {
   public class Theme {
      public bool mIsBuiltIn = false;
      public Color[] mInterfaceColors { get; private set; }
      public Color[][] mHighlightColors { get; private set; }
      public Font[] mFonts { get; private set; }
      public string mName { get; set; } = string.Empty;
      public ThemeBrightness mBrightness { get; set; } = ThemeBrightness.Light;

      public Theme(string pName, bool pIsBuiltIn = false) {
         mName = pName;
         mIsBuiltIn = pIsBuiltIn;
         mFonts = new Font[Enum.GetValues<FontUsage>().Length];
         mInterfaceColors = new Color[Enum.GetValues<ColorSwatchUsage>().Length];
         mHighlightColors = new Color[Enum.GetValues<LanguageKind>().Length][];
         for (int i = 0; i < mHighlightColors.Length; i++)
            mHighlightColors[i] = new Color[Enum.GetValues<TokenKind>().Length];
         mFonts[(int)FontUsage.Text] = CreateNewFont();
         mFonts[(int)FontUsage.Interface] = CreateNewFont();
         mFonts[(int)FontUsage.Menu] = CreateNewFont();
         mFonts[(int)FontUsage.Status] = CreateNewFont();
         mInterfaceColors[(int)ColorSwatchUsage.PanelBackground] = Color.Black;
         mInterfaceColors[(int)ColorSwatchUsage.TextBox] = Color.Black;
         mInterfaceColors[(int)ColorSwatchUsage.TextBoxFont] = Color.LightGoldenrodYellow;
         mInterfaceColors[(int)ColorSwatchUsage.MenuBackground] = Color.Black;
         mInterfaceColors[(int)ColorSwatchUsage.MenuFont] = Color.LightGoldenrodYellow;
         mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground] = Color.Black;
         mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont] = Color.LightGoldenrodYellow;
         mInterfaceColors[(int)ColorSwatchUsage.StatusBackground] = Color.DarkBlue;
         mInterfaceColors[(int)ColorSwatchUsage.StatusFont] = Color.LightGoldenrodYellow;
         mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground] = Color.DarkGray;
         mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont] = Color.Goldenrod;
         mInterfaceColors[(int)ColorSwatchUsage.TabHeaderUnselectedFont] = Color.Yellow;
         mInterfaceColors[(int)ColorSwatchUsage.TabHeaderSelectedFont] = Color.Blue;
         mInterfaceColors[(int)ColorSwatchUsage.TabHeaderUnselectedBackground] = Color.DarkGray;
         mInterfaceColors[(int)ColorSwatchUsage.TabHeaderSelectedBackground] = Color.Goldenrod;
      }

      public Theme Clone() {
         Theme pClone = new Theme(mName) { mBrightness = mBrightness };

         for (int i = 0; i < mFonts.Length; i++) {
            if (mFonts[i] != null)
               pClone.mFonts[i] = (Font)mFonts[i].Clone();
         }
         for (int i = 0; i < mInterfaceColors.Length; i++)
            pClone.mInterfaceColors[i] = mInterfaceColors[i];
         for (int i = 0; i < mHighlightColors.Length; i++) {
            for (int j = 0; j < mHighlightColors[i].Length; j++) {
               pClone.mHighlightColors[i][j] = mHighlightColors[i][j];
            }
         }
         return pClone;
      }

      public Theme Clone(string pName) {
         Theme pClone = new Theme(pName) { mBrightness = mBrightness };

         for (int i = 0; i < mFonts.Length; i++) {
            if (mFonts[i] != null)
               pClone.mFonts[i] = (Font)mFonts[i].Clone();
         }
         for (int i = 0; i < mInterfaceColors.Length; i++)
            pClone.mInterfaceColors[i] = mInterfaceColors[i];
         for (int i = 0; i < mHighlightColors.Length; i++) {
            for (int j = 0; j < mHighlightColors[i].Length; j++) {
               pClone.mHighlightColors[i][j] = mHighlightColors[i][j];
            }
         }
         return pClone;
      }

      public static void ThemeInterfaceThings(Theme pTheme, out Font pOutFont, out Color pOutForeColor,
         out Color pOutBackColor) {
         pOutFont = pTheme.mFonts[(int)FontUsage.Interface];
         pOutForeColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont];
         pOutBackColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground];
      }

      public static void ThemeMenuThings(Theme pTheme, out Font pOutFont, out Color pOutForeColor,
         out Color pOutBackColor) {
         pOutFont = pTheme.mFonts[(int)FontUsage.Menu];
         pOutForeColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.MenuFont];
         pOutBackColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.MenuBackground];
      }

      public static void ThemeStatusThings(Theme pTheme, out Font pOutFont, out Color pOutForeColor,
         out Color pOutBackColor) {
         pOutFont = pTheme.mFonts[(int)FontUsage.Status];
         pOutForeColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.StatusFont];
         pOutBackColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.StatusBackground];
      }

      public static void ThemeTextBoxThings(Theme pTheme, out Font pOutFont, out Color pOutForeColor,
         out Color pOutBackColor) {
         pOutFont = pTheme.mFonts[(int)FontUsage.Text];
         pOutForeColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.TextBoxFont];
         pOutBackColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.TextBox];
      }

      public static void ThemeAllThings(Theme pTheme, out Font pOutFont, out Color pOutForeColor, out Color pOutBackColor,
          out Color pOutGroupBoxBackgroundColor, out Color pOutStatusBackgroundColor, out Color pOutStatusForeColor,
           out Font pOutStatusFont) {
         pOutFont = pTheme.mFonts[(int)FontUsage.Interface];
         pOutForeColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont];
         pOutBackColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground];
         pOutGroupBoxBackgroundColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
         pOutStatusBackgroundColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.StatusBackground];
         pOutStatusForeColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.StatusFont];
         pOutStatusFont = pTheme.mFonts[(int)FontUsage.Status];
      }

      public void Dispose() {
         if (mFonts != null) {
            foreach (Font font in mFonts) {
               font?.Dispose();
            }
         }
      }
   }
}
