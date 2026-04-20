using static DBCode.Themes.ThemeBrightnessHelper;

namespace DBCode.Themes {
   public class Theme {
      public string mName { get; set; } = string.Empty;
      public ThemeBrightness mBrightness { get; set; } = ThemeBrightness.Light;
      public Font[] mFonts { get; private set; }
      public Color[] mColors { get; private set; }

      public Theme(string pName) {
         mName = pName;
         mFonts = new Font[Enum.GetValues<FontUsage>().Length];
         mColors = new Color[Enum.GetValues<ColorUsage>().Length];
         mFonts[(int)FontUsage.Text] = CreateNewFont();
         mFonts[(int)FontUsage.Interface] = CreateNewFont();
         mFonts[(int)FontUsage.Menu] = CreateNewFont();
         mFonts[(int)FontUsage.Status] = CreateNewFont();
         mColors[(int)ColorUsage.PanelBackground] = Color.Black;
         mColors[(int)ColorUsage.TextBox] = Color.Black;
         mColors[(int)ColorUsage.TextBoxFont] = Color.LightGoldenrodYellow;
         mColors[(int)ColorUsage.MenuBackground] = Color.Black;
         mColors[(int)ColorUsage.MenuFont] = Color.LightGoldenrodYellow;
         mColors[(int)ColorUsage.InterfaceBackground] = Color.Black;
         mColors[(int)ColorUsage.InterfaceFont] = Color.LightGoldenrodYellow;
         mColors[(int)ColorUsage.StatusBackground] = Color.DarkBlue;
         mColors[(int)ColorUsage.StatusFont] = Color.LightGoldenrodYellow;
         mColors[(int)ColorUsage.GroupBoxBackground] = Color.DarkGray;
         mColors[(int)ColorUsage.GroupBoxFont] = Color.Goldenrod;
         mColors[(int)ColorUsage.TabHeaderUnselectedFont] = Color.Yellow;
         mColors[(int)ColorUsage.TabHeaderSelectedFont] = Color.Blue;
         mColors[(int)ColorUsage.TabHeaderUnselectedBackground] = Color.DarkGray;
         mColors[(int)ColorUsage.TabHeaderSelectedBackground] = Color.Goldenrod;
         mColors[(int)ColorUsage.Unknown] = Color.White;
         mColors[(int)ColorUsage.Whitespace] = Color.DarkGray;
         mColors[(int)ColorUsage.Identifier] = Color.Goldenrod;
         mColors[(int)ColorUsage.Keyword] = Color.LightGoldenrodYellow;
         mColors[(int)ColorUsage.Number] = Color.Blue;
         mColors[(int)ColorUsage.StringLiteral] = Color.Cornsilk;
         mColors[(int)ColorUsage.CharLiteral] = Color.Yellow;
         mColors[(int)ColorUsage.Comment] = Color.Green;
         mColors[(int)ColorUsage.PreprocessorDirective] = Color.LightBlue;
         mColors[(int)ColorUsage.Operator] = Color.Magenta;
         mColors[(int)ColorUsage.Punctuation] = Color.IndianRed;
      }

      public Theme Clone() {
         Theme pClone = new Theme(mName) { mBrightness = mBrightness };

         for (int i = 0; i < mFonts.Length; i++) {
            if (mFonts[i] != null)
               pClone.mFonts[i] = (Font)mFonts[i].Clone();
         }
         for (int i = 0; i < mColors.Length; i++)
            pClone.mColors[i] = mColors[i];
         return pClone;
      }

      public Theme Clone(string pName) {
         Theme pClone = new Theme(pName) { mBrightness = mBrightness };

         for (int i = 0; i < mFonts.Length; i++) {
            if (mFonts[i] != null)
               pClone.mFonts[i] = (Font)mFonts[i].Clone();
         }
         for (int i = 0; i < mColors.Length; i++)
            pClone.mColors[i] = mColors[i];
         return pClone;
      }
   }

   public static class ThemeDefaults {
      public static Theme DefaultLight { get => field.Clone(); } = ThemeBuiltIns.CreateVisualStudioLightTheme();
      public static Theme DefaultDark { get => field.Clone(); } = ThemeBuiltIns.CreateVisualStudioDarkTheme();
      public static Theme HighContrastLight { get => field.Clone(); } = ThemeBuiltIns.CreateHighContrastLightTheme();
      public static Theme HighContrastDark { get => field.Clone(); } = ThemeBuiltIns.CreateHighContrastDarkTheme();
      public static Theme ClassicWin32 { get => field.Clone(); } = ThemeBuiltIns.CreateClassicWin32Theme();
      public static Theme PastelBreeze { get => field.Clone(); } = ThemeBuiltIns.CreatePastelBreezeTheme();
   }
}
