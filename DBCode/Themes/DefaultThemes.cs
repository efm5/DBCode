using static DBCode.Themes.ThemeBrightnessHelper;

namespace DBCode.Themes {
   public static class ThemeBuiltIns {
      public static Theme CreateVisualStudioLightTheme() {
         Theme pTheme = new Theme("Visual Studio Light");
         pTheme.mBrightness = ThemeBrightness.Light;
         pTheme.mColors[(int)ColorUsage.GroupBoxBackground] = ColorTranslator.FromHtml("#F3F3F3");
         pTheme.mColors[(int)ColorUsage.GroupBoxFont] = ColorTranslator.FromHtml("#1E1E1E");
         pTheme.mColors[(int)ColorUsage.InterfaceBackground] = Color.White;
         pTheme.mColors[(int)ColorUsage.InterfaceFont] = ColorTranslator.FromHtml("#1E1E1E");
         pTheme.mColors[(int)ColorUsage.MenuBackground] = ColorTranslator.FromHtml("#F0F0F0");
         pTheme.mColors[(int)ColorUsage.MenuFont] = ColorTranslator.FromHtml("#1E1E1E");
         pTheme.mColors[(int)ColorUsage.PanelBackground] = ColorTranslator.FromHtml("#F3F3F3");
         pTheme.mColors[(int)ColorUsage.StatusBackground] = ColorTranslator.FromHtml("#E5E5E5");
         pTheme.mColors[(int)ColorUsage.StatusFont] = ColorTranslator.FromHtml("#1E1E1E");
         pTheme.mColors[(int)ColorUsage.TabHeaderSelectedBackground] = Color.White;
         pTheme.mColors[(int)ColorUsage.TabHeaderSelectedFont] = ColorTranslator.FromHtml("#007ACC");
         pTheme.mColors[(int)ColorUsage.TabHeaderUnselectedBackground] = ColorTranslator.FromHtml("#E5E5E5");
         pTheme.mColors[(int)ColorUsage.TabHeaderUnselectedFont] = ColorTranslator.FromHtml("#444444");
         pTheme.mColors[(int)ColorUsage.TextBox] = Color.White;
         pTheme.mColors[(int)ColorUsage.TextBoxFont] = ColorTranslator.FromHtml("#1E1E1E");
         return pTheme;
      }

      public static Theme CreateVisualStudioDarkTheme() {
         Theme pTheme = new Theme("Visual Studio Dark");
         pTheme.mBrightness = ThemeBrightness.Dark;
         pTheme.mColors[(int)ColorUsage.GroupBoxBackground] = ColorTranslator.FromHtml("#252526");
         pTheme.mColors[(int)ColorUsage.GroupBoxFont] = ColorTranslator.FromHtml("#D4D4D4");
         pTheme.mColors[(int)ColorUsage.InterfaceBackground] = ColorTranslator.FromHtml("#1E1E1E");
         pTheme.mColors[(int)ColorUsage.InterfaceFont] = ColorTranslator.FromHtml("#D4D4D4");
         pTheme.mColors[(int)ColorUsage.MenuBackground] = ColorTranslator.FromHtml("#2D2D30");
         pTheme.mColors[(int)ColorUsage.MenuFont] = Color.White;
         pTheme.mColors[(int)ColorUsage.PanelBackground] = ColorTranslator.FromHtml("#1E1E1E");
         pTheme.mColors[(int)ColorUsage.StatusBackground] = ColorTranslator.FromHtml("#007ACC");
         pTheme.mColors[(int)ColorUsage.StatusFont] = Color.White;
         pTheme.mColors[(int)ColorUsage.TabHeaderSelectedBackground] = ColorTranslator.FromHtml("#1E1E1E");
         pTheme.mColors[(int)ColorUsage.TabHeaderSelectedFont] = Color.White;
         pTheme.mColors[(int)ColorUsage.TabHeaderUnselectedBackground] = ColorTranslator.FromHtml("#2D2D30");
         pTheme.mColors[(int)ColorUsage.TabHeaderUnselectedFont] = ColorTranslator.FromHtml("#9B9B9B");
         pTheme.mColors[(int)ColorUsage.TextBox] = ColorTranslator.FromHtml("#3C3C3C");
         pTheme.mColors[(int)ColorUsage.TextBoxFont] = ColorTranslator.FromHtml("#D4D4D4");
         return pTheme;
      }

      public static Theme CreateClassicWin32Theme() {
         Theme pTheme = new Theme("Classic Win32");
         pTheme.mBrightness = ThemeBrightness.Light;
         pTheme.mColors[(int)ColorUsage.GroupBoxBackground] = SystemColors.Control;
         pTheme.mColors[(int)ColorUsage.GroupBoxFont] = SystemColors.ControlText;
         pTheme.mColors[(int)ColorUsage.InterfaceBackground] = SystemColors.Control;
         pTheme.mColors[(int)ColorUsage.InterfaceFont] = SystemColors.ControlText;
         pTheme.mColors[(int)ColorUsage.MenuBackground] = SystemColors.Menu;
         pTheme.mColors[(int)ColorUsage.MenuFont] = SystemColors.MenuText;
         pTheme.mColors[(int)ColorUsage.PanelBackground] = SystemColors.Control;
         pTheme.mColors[(int)ColorUsage.StatusBackground] = SystemColors.Control;
         pTheme.mColors[(int)ColorUsage.StatusFont] = SystemColors.ControlText;
         pTheme.mColors[(int)ColorUsage.TabHeaderSelectedBackground] = SystemColors.Control;
         pTheme.mColors[(int)ColorUsage.TabHeaderSelectedFont] = SystemColors.ControlText;
         pTheme.mColors[(int)ColorUsage.TabHeaderUnselectedBackground] = SystemColors.Control;
         pTheme.mColors[(int)ColorUsage.TabHeaderUnselectedFont] = SystemColors.ControlText;
         pTheme.mColors[(int)ColorUsage.TextBox] = SystemColors.Window;
         pTheme.mColors[(int)ColorUsage.TextBoxFont] = SystemColors.WindowText;
         return pTheme;
      }

      public static Theme CreateHighContrastLightTheme() {
         Theme pTheme = new Theme("High Contrast Light");
         pTheme.mBrightness = ThemeBrightness.Light;

         pTheme.mColors[(int)ColorUsage.GroupBoxBackground] = Color.White;
         pTheme.mColors[(int)ColorUsage.GroupBoxFont] = Color.Black;
         pTheme.mColors[(int)ColorUsage.InterfaceBackground] = Color.White;
         pTheme.mColors[(int)ColorUsage.InterfaceFont] = Color.Black;
         pTheme.mColors[(int)ColorUsage.MenuBackground] = Color.White;
         pTheme.mColors[(int)ColorUsage.MenuFont] = Color.Black;
         pTheme.mColors[(int)ColorUsage.PanelBackground] = Color.White;
         pTheme.mColors[(int)ColorUsage.StatusBackground] = Color.Black;
         pTheme.mColors[(int)ColorUsage.StatusFont] = Color.White;
         pTheme.mColors[(int)ColorUsage.TabHeaderSelectedBackground] = Color.Black;
         pTheme.mColors[(int)ColorUsage.TabHeaderSelectedFont] = Color.Yellow;
         pTheme.mColors[(int)ColorUsage.TabHeaderUnselectedBackground] = Color.White;
         pTheme.mColors[(int)ColorUsage.TabHeaderUnselectedFont] = Color.Black;
         pTheme.mColors[(int)ColorUsage.TextBox] = Color.White;
         pTheme.mColors[(int)ColorUsage.TextBoxFont] = Color.Black;

         return pTheme;
      }

      public static Theme CreateHighContrastDarkTheme() {
         Theme pTheme = new Theme("High Contrast Dark");
         pTheme.mBrightness = ThemeBrightness.Dark;

         pTheme.mColors[(int)ColorUsage.GroupBoxBackground] = Color.Black;
         pTheme.mColors[(int)ColorUsage.GroupBoxFont] = Color.White;
         pTheme.mColors[(int)ColorUsage.InterfaceBackground] = Color.Black;
         pTheme.mColors[(int)ColorUsage.InterfaceFont] = Color.White;
         pTheme.mColors[(int)ColorUsage.MenuBackground] = Color.Black;
         pTheme.mColors[(int)ColorUsage.MenuFont] = Color.White;
         pTheme.mColors[(int)ColorUsage.PanelBackground] = Color.Black;
         pTheme.mColors[(int)ColorUsage.StatusBackground] = Color.Yellow;
         pTheme.mColors[(int)ColorUsage.StatusFont] = Color.Black;
         pTheme.mColors[(int)ColorUsage.TabHeaderSelectedBackground] = Color.Yellow;
         pTheme.mColors[(int)ColorUsage.TabHeaderSelectedFont] = Color.Black;
         pTheme.mColors[(int)ColorUsage.TabHeaderUnselectedBackground] = Color.Black;
         pTheme.mColors[(int)ColorUsage.TabHeaderUnselectedFont] = Color.White;
         pTheme.mColors[(int)ColorUsage.TextBox] = Color.Black;
         pTheme.mColors[(int)ColorUsage.TextBoxFont] = Color.White;

         return pTheme;
      }

      public static Theme CreatePastelBreezeTheme() {
         Theme pTheme = new Theme("Pastel Breeze");
         pTheme.mBrightness = ThemeBrightness.Light;
         pTheme.mColors[(int)ColorUsage.GroupBoxBackground] = ColorTranslator.FromHtml("#E8E6F2");   // lavender‑gray
         pTheme.mColors[(int)ColorUsage.GroupBoxFont] = ColorTranslator.FromHtml("#3A3A3A");         // charcoal
         pTheme.mColors[(int)ColorUsage.InterfaceBackground] = ColorTranslator.FromHtml("#F5F7FA");  // fog‑white
         pTheme.mColors[(int)ColorUsage.InterfaceFont] = ColorTranslator.FromHtml("#3A3A3A");        // charcoal
         pTheme.mColors[(int)ColorUsage.MenuBackground] = ColorTranslator.FromHtml("#E4F0F0");       // pale teal‑gray
         pTheme.mColors[(int)ColorUsage.MenuFont] = ColorTranslator.FromHtml("#3A3A3A");
         pTheme.mColors[(int)ColorUsage.PanelBackground] = ColorTranslator.FromHtml("#ECEBF3");      // soft lavender‑white
         pTheme.mColors[(int)ColorUsage.StatusBackground] = ColorTranslator.FromHtml("#C9E4E7");     // pastel aqua
         pTheme.mColors[(int)ColorUsage.StatusFont] = ColorTranslator.FromHtml("#2E2E2E");
         pTheme.mColors[(int)ColorUsage.TabHeaderSelectedBackground] = ColorTranslator.FromHtml("#FFFFFF");
         pTheme.mColors[(int)ColorUsage.TabHeaderSelectedFont] = ColorTranslator.FromHtml("#5A7FA3"); // muted steel‑blue
         pTheme.mColors[(int)ColorUsage.TabHeaderUnselectedBackground] = ColorTranslator.FromHtml("#DDE3EE"); // soft blue‑gray
         pTheme.mColors[(int)ColorUsage.TabHeaderUnselectedFont] = ColorTranslator.FromHtml("#6A6A6A");
         pTheme.mColors[(int)ColorUsage.TextBox] = ColorTranslator.FromHtml("#FFFFFF");
         pTheme.mColors[(int)ColorUsage.TextBoxFont] = ColorTranslator.FromHtml("#3A3A3A");
         return pTheme;
      }
   }
}
