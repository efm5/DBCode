using DBCode.Syntax;
using static DBCode.Themes.ThemeBrightnessHelper;

namespace DBCode.Themes {
   public class Theme {
      public string mName { get; set; } = string.Empty;
      public ThemeBrightness mBrightness { get; set; } = ThemeBrightness.Light;
      public Font[] mFonts { get; private set; }
      public Color[] mInterfaceColors { get; private set; }
      public Color[][] mHighlightColors { get; private set; }

      public Theme(string pName) {
         mName = pName;
         mFonts = new Font[Enum.GetValues<FontUsage>().Length];
         mInterfaceColors = new Color[Enum.GetValues<ColorUsage>().Length];
         mHighlightColors = new Color[Enum.GetValues<LanguageKind>().Length][];
         for (int i = 0; i < mHighlightColors.Length; i++)
            mHighlightColors[i] = new Color[Enum.GetValues<TokenKind>().Length];
         mFonts[(int)FontUsage.Text] = CreateNewFont();
         mFonts[(int)FontUsage.Interface] = CreateNewFont();
         mFonts[(int)FontUsage.Menu] = CreateNewFont();
         mFonts[(int)FontUsage.Status] = CreateNewFont();
         mInterfaceColors[(int)ColorUsage.PanelBackground] = Color.Black;
         mInterfaceColors[(int)ColorUsage.TextBox] = Color.Black;
         mInterfaceColors[(int)ColorUsage.TextBoxFont] = Color.LightGoldenrodYellow;
         mInterfaceColors[(int)ColorUsage.MenuBackground] = Color.Black;
         mInterfaceColors[(int)ColorUsage.MenuFont] = Color.LightGoldenrodYellow;
         mInterfaceColors[(int)ColorUsage.InterfaceBackground] = Color.Black;
         mInterfaceColors[(int)ColorUsage.InterfaceFont] = Color.LightGoldenrodYellow;
         mInterfaceColors[(int)ColorUsage.StatusBackground] = Color.DarkBlue;
         mInterfaceColors[(int)ColorUsage.StatusFont] = Color.LightGoldenrodYellow;
         mInterfaceColors[(int)ColorUsage.GroupBoxBackground] = Color.DarkGray;
         mInterfaceColors[(int)ColorUsage.GroupBoxFont] = Color.Goldenrod;
         mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedFont] = Color.Yellow;
         mInterfaceColors[(int)ColorUsage.TabHeaderSelectedFont] = Color.Blue;
         mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedBackground] = Color.DarkGray;
         mInterfaceColors[(int)ColorUsage.TabHeaderSelectedBackground] = Color.Goldenrod;
         mInterfaceColors[(int)ColorUsage.Unknown] = Color.White;
         mInterfaceColors[(int)ColorUsage.Whitespace] = Color.DarkGray;
         mInterfaceColors[(int)ColorUsage.Identifier] = Color.Goldenrod;
         mInterfaceColors[(int)ColorUsage.Keyword] = Color.LightGoldenrodYellow;
         mInterfaceColors[(int)ColorUsage.Number] = Color.Blue;
         mInterfaceColors[(int)ColorUsage.StringLiteral] = Color.Cornsilk;
         mInterfaceColors[(int)ColorUsage.CharLiteral] = Color.Yellow;
         mInterfaceColors[(int)ColorUsage.Comment] = Color.Green;
         mInterfaceColors[(int)ColorUsage.PreprocessorDirective] = Color.LightBlue;
         mInterfaceColors[(int)ColorUsage.Operator] = Color.Magenta;
         mInterfaceColors[(int)ColorUsage.Punctuation] = Color.IndianRed;
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
   }

   public static class ThemeDefaults {
      public static Theme DefaultLight { get => field.Clone(); } = ThemeBuiltIns.CreateLightTheme();
      public static Theme DefaultDark { get => field.Clone(); } = ThemeBuiltIns.CreateDarkTheme();
      public static Theme HighContrastLight { get => field.Clone(); } = ThemeBuiltIns.CreateHighContrastLightTheme();
      public static Theme HighContrastDark { get => field.Clone(); } = ThemeBuiltIns.CreateHighContrastDarkTheme();
      public static Theme Classic { get => field.Clone(); } = ThemeBuiltIns.CreateClassicTheme();
      public static Theme LightPastel { get => field.Clone(); } = ThemeBuiltIns.CreateLightPastelTheme();
      public static Theme DarkPastel { get => field.Clone(); } = ThemeBuiltIns.CreateDarkPastelTheme();
   }
}
