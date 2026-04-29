using static DBCode.Themes.ThemeBrightnessHelper;

namespace DBCode.Themes {
   public static class ThemeBuiltIns {
      public static Theme CreateLightTheme(bool pAdd = true) {
         Theme theme = new Theme("Light", true) {
            mBrightness = ThemeBrightness.Light
         };
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground] = ColorTranslator.FromHtml("#F3F3F3");
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxFont] = ColorTranslator.FromHtml("#1E1E1E");
         theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.InterfaceFont] = ColorTranslator.FromHtml("#1E1E1E");
         theme.mInterfaceColors[(int)ColorUsage.MenuBackground] = ColorTranslator.FromHtml("#F0F0F0");
         theme.mInterfaceColors[(int)ColorUsage.MenuFont] = ColorTranslator.FromHtml("#1E1E1E");
         theme.mInterfaceColors[(int)ColorUsage.PanelBackground] = ColorTranslator.FromHtml("#F3F3F3");
         theme.mInterfaceColors[(int)ColorUsage.StatusBackground] = ColorTranslator.FromHtml("#E5E5E5");
         theme.mInterfaceColors[(int)ColorUsage.StatusFont] = ColorTranslator.FromHtml("#1E1E1E");
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedBackground] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedFont] = ColorTranslator.FromHtml("#007ACC");
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedBackground] = ColorTranslator.FromHtml("#E5E5E5");
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedFont] = ColorTranslator.FromHtml("#444444");
         theme.mInterfaceColors[(int)ColorUsage.TextBox] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.TextBoxFont] = ColorTranslator.FromHtml("#1E1E1E");
         SetLightThemeSyntaxColors(theme);
         if (pAdd)
            mThemes.Add(theme);
         return theme;
      }

      public static Theme CreateDarkTheme(bool pAdd = true) {
         Theme theme = new Theme("Dark", true) {
            mBrightness = ThemeBrightness.Dark
         };
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground] = ColorTranslator.FromHtml("#252526");
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxFont] = ColorTranslator.FromHtml("#D4D4D4");
         theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground] = ColorTranslator.FromHtml("#1E1E1E");
         theme.mInterfaceColors[(int)ColorUsage.InterfaceFont] = ColorTranslator.FromHtml("#D4D4D4");
         theme.mInterfaceColors[(int)ColorUsage.MenuBackground] = ColorTranslator.FromHtml("#2D2D30");
         theme.mInterfaceColors[(int)ColorUsage.MenuFont] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.PanelBackground] = ColorTranslator.FromHtml("#1E1E1E");
         theme.mInterfaceColors[(int)ColorUsage.StatusBackground] = ColorTranslator.FromHtml("#001F3F"); // Navy blue
         theme.mInterfaceColors[(int)ColorUsage.StatusFont] = Color.Yellow;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedBackground] = ColorTranslator.FromHtml("#1E1E1E");
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedFont] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedBackground] = ColorTranslator.FromHtml("#2D2D30");
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedFont] = ColorTranslator.FromHtml("#9B9B9B");
         theme.mInterfaceColors[(int)ColorUsage.TextBox] = ColorTranslator.FromHtml("#3C3C3C"); // Charcoal gray
         theme.mInterfaceColors[(int)ColorUsage.TextBoxFont] = ColorTranslator.FromHtml("#89C2D9"); // Soft blue
         SetDarkThemeSyntaxColors(theme);
         if (pAdd)
            mThemes.Add(theme);
         return theme;
      }

      public static Theme CreateClassicTheme(bool pAdd = true) {
         Theme theme = new Theme("Classic", true) {
            mBrightness = ThemeBrightness.Light
         };
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground] = SystemColors.Control;
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxFont] = SystemColors.ControlText;
         theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground] = SystemColors.Control;
         theme.mInterfaceColors[(int)ColorUsage.InterfaceFont] = SystemColors.ControlText;
         theme.mInterfaceColors[(int)ColorUsage.MenuBackground] = SystemColors.Menu;
         theme.mInterfaceColors[(int)ColorUsage.MenuFont] = SystemColors.MenuText;
         theme.mInterfaceColors[(int)ColorUsage.PanelBackground] = SystemColors.Control;
         theme.mInterfaceColors[(int)ColorUsage.StatusBackground] = SystemColors.Control;
         theme.mInterfaceColors[(int)ColorUsage.StatusFont] = SystemColors.ControlText;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedBackground] = SystemColors.Control;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedFont] = SystemColors.ControlText;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedBackground] = SystemColors.Control;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedFont] = SystemColors.ControlText;
         theme.mInterfaceColors[(int)ColorUsage.TextBox] = SystemColors.Window;
         theme.mInterfaceColors[(int)ColorUsage.TextBoxFont] = SystemColors.WindowText;
         SetClassicThemeSyntaxColors(theme);
         if (pAdd)
            mThemes.Add(theme);
         return theme;
      }

      public static Theme CreateHighContrastLightTheme(bool pAdd = true) {
         Theme theme = new Theme("High Contrast Light", true) {
            mBrightness = ThemeBrightness.Light
         };
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxFont] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.InterfaceFont] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.MenuBackground] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.MenuFont] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.PanelBackground] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.StatusBackground] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.StatusFont] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedBackground] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedFont] = Color.Yellow;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedBackground] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedFont] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.TextBox] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.TextBoxFont] = Color.Black;
         SetHighContrastLightSyntaxColors(theme);
         if (pAdd)
            mThemes.Add(theme);
         return theme;
      }

      public static Theme CreateHighContrastDarkTheme(bool pAdd = true) {
         Theme theme = new Theme("High Contrast Dark", true) {
            mBrightness = ThemeBrightness.Dark
         };
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxFont] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.InterfaceFont] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.MenuBackground] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.MenuFont] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.PanelBackground] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.StatusBackground] = Color.Yellow;
         theme.mInterfaceColors[(int)ColorUsage.StatusFont] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedBackground] = Color.Yellow;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedFont] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedBackground] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedFont] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.TextBox] = Color.Black;
         theme.mInterfaceColors[(int)ColorUsage.TextBoxFont] = Color.White;
         SetHighContrastDarkSyntaxColors(theme);
         if (pAdd)
            mThemes.Add(theme);
         return theme;
      }

      public static Theme CreateLightPastelTheme(bool pAdd = true) {
         Theme theme = new Theme("Light Pastel", true) {
            mBrightness = ThemeBrightness.Light
         };
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground] = ColorTranslator.FromHtml("#E8E6F2");   // lavender-gray
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxFont] = ColorTranslator.FromHtml("#3A3A3A");         // charcoal
         theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground] = ColorTranslator.FromHtml("#F5F7FA");  // fog-white
         theme.mInterfaceColors[(int)ColorUsage.InterfaceFont] = ColorTranslator.FromHtml("#3A3A3A");        // charcoal
         theme.mInterfaceColors[(int)ColorUsage.MenuBackground] = ColorTranslator.FromHtml("#E4F0F0");       // pale teal-gray
         theme.mInterfaceColors[(int)ColorUsage.MenuFont] = ColorTranslator.FromHtml("#3A3A3A");             // charcoal
         theme.mInterfaceColors[(int)ColorUsage.PanelBackground] = ColorTranslator.FromHtml("#ECEBF3");      // soft lavender-white
         theme.mInterfaceColors[(int)ColorUsage.StatusBackground] = ColorTranslator.FromHtml("#C9E4E7");     // pastel aqua
         theme.mInterfaceColors[(int)ColorUsage.StatusFont] = ColorTranslator.FromHtml("#0066CC");           // strong blue
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedBackground] = ColorTranslator.FromHtml("#FFFFFF");  // pure white
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedFont] = ColorTranslator.FromHtml("#5A7FA3"); // muted steel-blue
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedBackground] = ColorTranslator.FromHtml("#DDE3EE"); // soft blue-gray
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedFont] = ColorTranslator.FromHtml("#6A6A6A"); // dark gray
         theme.mInterfaceColors[(int)ColorUsage.TextBox] = ColorTranslator.FromHtml("#FFFFF0");              // very light yellow
         theme.mInterfaceColors[(int)ColorUsage.TextBoxFont] = ColorTranslator.FromHtml("#2E86C1");          // pastel blue
         SetPastelThemeSyntaxColors(theme);
         if (pAdd)
            mThemes.Add(theme);
         return theme;
      }

      public static Theme CreateDarkPastelTheme(bool pAdd = true) {
         Theme theme = new Theme("Dark Pastel", true) {
            mBrightness = ThemeBrightness.Dark
         };
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground] = ColorTranslator.FromHtml("#2D2B3A");   // dark lavender-gray
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxFont] = ColorTranslator.FromHtml("#C8C4D8");         // light lavender
         theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground] = ColorTranslator.FromHtml("#1F1D2B");  // deep purple-black
         theme.mInterfaceColors[(int)ColorUsage.InterfaceFont] = ColorTranslator.FromHtml("#D4D0E0");        // soft white-lavender
         theme.mInterfaceColors[(int)ColorUsage.MenuBackground] = ColorTranslator.FromHtml("#252837");       // dark blue-gray
         theme.mInterfaceColors[(int)ColorUsage.MenuFont] = ColorTranslator.FromHtml("#C8C4D8");             // light lavender
         theme.mInterfaceColors[(int)ColorUsage.PanelBackground] = ColorTranslator.FromHtml("#27253A");      // dark soft purple
         theme.mInterfaceColors[(int)ColorUsage.StatusBackground] = ColorTranslator.FromHtml("#3A4F5C");     // muted teal-gray
         theme.mInterfaceColors[(int)ColorUsage.StatusFont] = ColorTranslator.FromHtml("#A3C9D6");           // soft cyan
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedBackground] = ColorTranslator.FromHtml("#3A3548");  // dark purple
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedFont] = ColorTranslator.FromHtml("#B8A8D8"); // pastel lavender
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedBackground] = ColorTranslator.FromHtml("#2A2838"); // darker purple
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedFont] = ColorTranslator.FromHtml("#8A8A9A"); // muted gray
         theme.mInterfaceColors[(int)ColorUsage.TextBox] = ColorTranslator.FromHtml("#2B2838");              // dark purple-gray
         theme.mInterfaceColors[(int)ColorUsage.TextBoxFont] = ColorTranslator.FromHtml("#C9BDD8");          // soft lavender
         SetDarkPastelThemeSyntaxColors(theme);
         if (pAdd)
            mThemes.Add(theme);
         return theme;
      }

      // === Syntax Color Helpers ===
      private static void SetLightThemeSyntaxColors(Theme pTheme) {
         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = ColorTranslator.FromHtml("#FF6600"); // Warning orange
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = ColorTranslator.FromHtml("#E0E0E0"); // Barely visible on white
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#2A2A2A"); // Near black
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#0000FF"); // Blue
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = ColorTranslator.FromHtml("#098658"); // Teal-green
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = ColorTranslator.FromHtml("#A31515"); // Red-brown
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = ColorTranslator.FromHtml("#D4860B"); // Amber
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = ColorTranslator.FromHtml("#008000"); // Green
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = ColorTranslator.FromHtml("#AF00DB"); // Vivid purple
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = ColorTranslator.FromHtml("#7A7A7A"); // Medium gray
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = ColorTranslator.FromHtml("#5A5A5A"); // Dark gray
         }
         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000"); // Maroon for tags
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#FF0000"); // Red for selectors
         pTheme.mHighlightColors[(int)LanguageKind.Markdown][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#0000FF");
         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = ColorTranslator.FromHtml("#1E1E1E"); // Black text
         }
      }

      private static void SetDarkThemeSyntaxColors(Theme pTheme) {
         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = ColorTranslator.FromHtml("#FF6600"); // Warning orange
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = ColorTranslator.FromHtml("#4A4A4A"); // Barely visible on charcoal
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#CCCCCC"); // Light gray
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#569CD6"); // Light blue
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = ColorTranslator.FromHtml("#B5CEA8"); // Light green
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = ColorTranslator.FromHtml("#CE9178"); // Orange-brown
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = ColorTranslator.FromHtml("#DCDCAA"); // Yellow-gold
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = ColorTranslator.FromHtml("#6A9955"); // Green
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = ColorTranslator.FromHtml("#C586C0"); // Soft purple
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = ColorTranslator.FromHtml("#9CDCFE"); // Light blue
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = ColorTranslator.FromHtml("#C8C8C8"); // Light gray
         }
         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#808080"); // Gray-blue for tags
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#808080");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#D7BA7D"); // Gold
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#D16969"); // Light red
         pTheme.mHighlightColors[(int)LanguageKind.Markdown][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#569CD6");
         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = ColorTranslator.FromHtml("#D4D4D4"); // Light gray text
         }
      }

      private static void SetClassicThemeSyntaxColors(Theme pTheme) {
         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = ColorTranslator.FromHtml("#FF6600"); // Warning orange
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = ColorTranslator.FromHtml("#D8D8D8"); // Barely visible on window white
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#2A2A2A"); // Near black
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = Color.Blue;
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = Color.Black;
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = ColorTranslator.FromHtml("#A31515"); // Dark red
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = ColorTranslator.FromHtml("#D4860B"); // Amber
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = ColorTranslator.FromHtml("#008000"); // Green
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = ColorTranslator.FromHtml("#AF00DB"); // Vivid purple
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = ColorTranslator.FromHtml("#7A7A7A"); // Medium gray
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = ColorTranslator.FromHtml("#5A5A5A"); // Dark gray
         }
         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#FF0000");
         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = Color.Black;
         }
      }

      private static void SetHighContrastLightSyntaxColors(Theme pTheme) {
         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = ColorTranslator.FromHtml("#FF6600"); // Warning orange
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = ColorTranslator.FromHtml("#E8E8E8"); // Barely visible on white
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#2A2A2A"); // Near black
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = Color.Blue;
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = ColorTranslator.FromHtml("#09885A"); // Dark teal
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = ColorTranslator.FromHtml("#A31515"); // Dark red
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = ColorTranslator.FromHtml("#C27D00"); // Dark amber
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = ColorTranslator.FromHtml("#008000"); // Dark green
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = ColorTranslator.FromHtml("#800080"); // Pure purple
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = ColorTranslator.FromHtml("#7A7A7A"); // Medium gray
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = ColorTranslator.FromHtml("#5A5A5A"); // Dark gray
         }
         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#C00000");
         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = Color.Black;
         }
      }

      private static void SetHighContrastDarkSyntaxColors(Theme pTheme) {
         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = ColorTranslator.FromHtml("#FF6600"); // Warning orange
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = ColorTranslator.FromHtml("#1A1A1A"); // Barely visible on black
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#E0E0E0"); // Near white
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#569CD6"); // Bright blue
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = ColorTranslator.FromHtml("#B5CEA8"); // Light green
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = ColorTranslator.FromHtml("#CE9178"); // Orange
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = ColorTranslator.FromHtml("#DCDCAA"); // Yellow-gold
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = ColorTranslator.FromHtml("#6A9955"); // Bright green
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = ColorTranslator.FromHtml("#C586C0"); // Soft purple
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = ColorTranslator.FromHtml("#9CDCFE"); // Light blue
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = ColorTranslator.FromHtml("#C8C8C8"); // Light gray
         }
         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#4EC9B0"); // Cyan
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#4EC9B0");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#D7BA7D"); // Gold
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#D16969"); // Light red
         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = Color.White;
         }
      }

      private static void SetPastelThemeSyntaxColors(Theme pTheme) {
         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = ColorTranslator.FromHtml("#FF6600"); // Warning orange
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = ColorTranslator.FromHtml("#E8E8E0"); // Barely visible on light yellow
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#4A4A4A"); // Dark charcoal
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#7B68A6"); // Muted purple
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = ColorTranslator.FromHtml("#82B5A8"); // Pastel teal
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = ColorTranslator.FromHtml("#C18686"); // Dusty rose
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = ColorTranslator.FromHtml("#C4A35A"); // Warm tan
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = ColorTranslator.FromHtml("#7FA37C"); // Sage green
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = ColorTranslator.FromHtml("#9B7FBA"); // Muted lavender-purple
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = ColorTranslator.FromHtml("#6A6A6A"); // Dark gray
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = ColorTranslator.FromHtml("#6A6A6A"); // Dark gray
         }
         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#A88D8D"); // Dusty mauve
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#A88D8D");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#B8A67D"); // Tan
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#C18686"); // Dusty rose
         pTheme.mHighlightColors[(int)LanguageKind.Markdown][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#7B68A6"); // Muted purple
         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = ColorTranslator.FromHtml("#3A3A3A"); // Charcoal
         }
      }

      private static void SetDarkPastelThemeSyntaxColors(Theme pTheme) {
         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = ColorTranslator.FromHtml("#FF6600"); // Warning orange
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = ColorTranslator.FromHtml("#3A3545"); // Barely visible on dark purple-gray
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#E0D8F0"); // Very light lavender
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#B19CD9"); // Pastel purple
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = ColorTranslator.FromHtml("#A0C4BC"); // Soft teal
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = ColorTranslator.FromHtml("#D4A5A5"); // Dusty pink
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = ColorTranslator.FromHtml("#C4B87A"); // Soft gold
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = ColorTranslator.FromHtml("#8CAF88"); // Muted sage green
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = ColorTranslator.FromHtml("#B8A0D8"); // Soft lavender-purple
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = ColorTranslator.FromHtml("#A8A8B8"); // Light gray
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = ColorTranslator.FromHtml("#A8A8B8"); // Light gray
         }
         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#C4A4A4"); // Soft mauve
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#C4A4A4");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#C9B896"); // Soft tan
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#D4A5A5"); // Dusty pink
         pTheme.mHighlightColors[(int)LanguageKind.Markdown][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#B19CD9"); // Pastel purple
         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = ColorTranslator.FromHtml("#C9BDD8"); // Soft lavender
         }
      }
   }
}
