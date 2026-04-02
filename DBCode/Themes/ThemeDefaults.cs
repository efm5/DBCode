using static DBCode.Themes.ThemeBrightnessHelper;

namespace DBCode.Themes {
   public static class ThemeDefaults {
      public static readonly Theme DefaultLight = BuildDefaultLight();
      public static readonly Theme DefaultDark = BuildDefaultDark();

      private static Theme BuildDefaultLight() {
         Theme theme = new Theme("Light") {
            mBrightness = ThemeBrightness.Light
         };

         // Fonts
         theme.mFonts[(int)FontUsage.Interface] = new Font("Segoe UI", 18f, FontStyle.Regular);
         theme.mFonts[(int)FontUsage.Menu] = new Font("Segoe UI", 18f, FontStyle.Regular);
         theme.mFonts[(int)FontUsage.Status] = new Font("Segoe UI", 16f, FontStyle.Regular);
         theme.mFonts[(int)FontUsage.Text] = new Font("Consolas", 18f, FontStyle.Regular);
         // Colors
         theme.mColors[(int)ColorUsage.PanelBackground] = Color.White;
         theme.mColors[(int)ColorUsage.GroupBoxBackground] = Color.WhiteSmoke;
         theme.mColors[(int)ColorUsage.GroupBoxFont] = Color.Black;
         theme.mColors[(int)ColorUsage.InterfaceBackground] = Color.White;
         theme.mColors[(int)ColorUsage.InterfaceFont] = Color.Black;
         theme.mColors[(int)ColorUsage.MenuBackground] = Color.Gainsboro;
         theme.mColors[(int)ColorUsage.MenuFont] = Color.Black;
         theme.mColors[(int)ColorUsage.StatusBackground] = Color.Gainsboro;
         theme.mColors[(int)ColorUsage.StatusFont] = Color.Black;
         theme.mColors[(int)ColorUsage.TextBoxFont] = Color.Black;
         return theme;
      }

      private static Theme BuildDefaultDark() {
         Theme theme = new Theme("Dark") {
            mBrightness = ThemeBrightness.Dark
         };

         // Fonts
         theme.mFonts[(int)FontUsage.Interface] = new Font("Segoe UI", 18f, FontStyle.Regular);
         theme.mFonts[(int)FontUsage.Menu] = new Font("Segoe UI", 18f, FontStyle.Regular);
         theme.mFonts[(int)FontUsage.Status] = new Font("Segoe UI", 16f, FontStyle.Regular);
         theme.mFonts[(int)FontUsage.Text] = new Font("Consolas", 18f, FontStyle.Regular);
         // Colors
         theme.mColors[(int)ColorUsage.PanelBackground] = Color.FromArgb(32, 32, 32);
         theme.mColors[(int)ColorUsage.GroupBoxBackground] = Color.FromArgb(48, 48, 48);
         theme.mColors[(int)ColorUsage.GroupBoxFont] = Color.White;
         theme.mColors[(int)ColorUsage.InterfaceBackground] = Color.FromArgb(28, 28, 28);
         theme.mColors[(int)ColorUsage.InterfaceFont] = Color.White;
         theme.mColors[(int)ColorUsage.MenuBackground] = Color.FromArgb(45, 45, 45);
         theme.mColors[(int)ColorUsage.MenuFont] = Color.White;
         theme.mColors[(int)ColorUsage.StatusBackground] = Color.FromArgb(40, 40, 40);
         theme.mColors[(int)ColorUsage.StatusFont] = Color.White;
         theme.mColors[(int)ColorUsage.TextBoxFont] = Color.White;
         return theme;
      }
   }
}
