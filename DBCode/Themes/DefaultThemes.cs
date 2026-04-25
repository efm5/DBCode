using DBCode.Syntax;
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
         theme.mInterfaceColors[(int)ColorUsage.StatusBackground] = ColorTranslator.FromHtml("#001F3F");//Navy blue
         theme.mInterfaceColors[(int)ColorUsage.StatusFont] = Color.Yellow;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedBackground] = ColorTranslator.FromHtml("#1E1E1E");
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedFont] = Color.White;
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedBackground] = ColorTranslator.FromHtml("#2D2D30");
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedFont] = ColorTranslator.FromHtml("#9B9B9B");
         theme.mInterfaceColors[(int)ColorUsage.TextBox] = ColorTranslator.FromHtml("#3C3C3C");//Charcoal gray
         theme.mInterfaceColors[(int)ColorUsage.TextBoxFont] = ColorTranslator.FromHtml("#89C2D9");//Soft blue
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
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground] = ColorTranslator.FromHtml("#E8E6F2");   // lavender‑gray
         theme.mInterfaceColors[(int)ColorUsage.GroupBoxFont] = ColorTranslator.FromHtml("#3A3A3A");         // charcoal
         theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground] = ColorTranslator.FromHtml("#F5F7FA");  // fog‑white
         theme.mInterfaceColors[(int)ColorUsage.InterfaceFont] = ColorTranslator.FromHtml("#3A3A3A");        // charcoal
         theme.mInterfaceColors[(int)ColorUsage.MenuBackground] = ColorTranslator.FromHtml("#E4F0F0");       // pale teal‑gray
         theme.mInterfaceColors[(int)ColorUsage.MenuFont] = ColorTranslator.FromHtml("#3A3A3A");   // charcoal
         theme.mInterfaceColors[(int)ColorUsage.PanelBackground] = ColorTranslator.FromHtml("#ECEBF3");      // soft lavender‑white
         theme.mInterfaceColors[(int)ColorUsage.StatusBackground] = ColorTranslator.FromHtml("#C9E4E7");     // pastel aqua
         theme.mInterfaceColors[(int)ColorUsage.StatusFont] = ColorTranslator.FromHtml("#0066CC"); // strong blue
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedBackground] = ColorTranslator.FromHtml("#FFFFFF");// pure white
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedFont] = ColorTranslator.FromHtml("#5A7FA3"); // muted steel‑blue
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedBackground] = ColorTranslator.FromHtml("#DDE3EE"); // soft blue‑gray
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedFont] = ColorTranslator.FromHtml("#6A6A6A");// dark gray
         theme.mInterfaceColors[(int)ColorUsage.TextBox] = ColorTranslator.FromHtml("#FFFFF0");      //very light yellow
         theme.mInterfaceColors[(int)ColorUsage.TextBoxFont] = ColorTranslator.FromHtml("#2E86C1"); // pastel blue
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
         theme.mInterfaceColors[(int)ColorUsage.MenuFont] = ColorTranslator.FromHtml("#C8C4D8");   // light lavender
         theme.mInterfaceColors[(int)ColorUsage.PanelBackground] = ColorTranslator.FromHtml("#27253A");      // dark soft purple
         theme.mInterfaceColors[(int)ColorUsage.StatusBackground] = ColorTranslator.FromHtml("#3A4F5C");     // muted teal-gray
         theme.mInterfaceColors[(int)ColorUsage.StatusFont] = ColorTranslator.FromHtml("#A3C9D6"); // soft cyan
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedBackground] = ColorTranslator.FromHtml("#3A3548");// dark purple
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedFont] = ColorTranslator.FromHtml("#B8A8D8"); // pastel lavender
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedBackground] = ColorTranslator.FromHtml("#2A2838"); // darker purple
         theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedFont] = ColorTranslator.FromHtml("#8A8A9A");// muted gray
         theme.mInterfaceColors[(int)ColorUsage.TextBox] = ColorTranslator.FromHtml("#2B2838");      // dark purple-gray
         theme.mInterfaceColors[(int)ColorUsage.TextBoxFont] = ColorTranslator.FromHtml("#C9BDD8"); // soft lavender
         SetDarkPastelThemeSyntaxColors(theme);
         if (pAdd)
            mThemes.Add(theme);
         return theme;
      }

      // === Syntax Color Helpers ===
      private static void SetLightThemeSyntaxColors(Theme pTheme) {
         // Default colors for all languages (most tokens are language-agnostic)
         Color defaultText = ColorTranslator.FromHtml("#1E1E1E");     // Black text
         Color keyword = ColorTranslator.FromHtml("#0000FF");         // Blue
         Color comment = ColorTranslator.FromHtml("#008000");         // Green
         Color stringLiteral = ColorTranslator.FromHtml("#A31515");   // Red-brown
         Color number = ColorTranslator.FromHtml("#098658");          // Teal-green
         Color preprocessor = ColorTranslator.FromHtml("#808080");    // Gray
         Color identifier = defaultText;
         Color operatorColor = defaultText;
         Color punctuation = defaultText;

         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = identifier;
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = keyword;
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = number;
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = comment;
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = preprocessor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = operatorColor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = punctuation;
         }

         // Language-specific overrides
         // HTML/XML: tags and attributes
         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000"); // Maroon for tags
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");

         // CSS: properties and selectors
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#FF0000"); // Red for selectors

         // Markdown: keep simple
         pTheme.mHighlightColors[(int)LanguageKind.Markdown][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#0000FF");

         // PlainText: all default text
         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = defaultText;
         }
      }

      private static void SetDarkThemeSyntaxColors(Theme pTheme) {
         // Default colors for all languages
         Color defaultText = ColorTranslator.FromHtml("#D4D4D4");     // Light gray text
         Color keyword = ColorTranslator.FromHtml("#569CD6");         // Light blue
         Color comment = ColorTranslator.FromHtml("#6A9955");         // Green
         Color stringLiteral = ColorTranslator.FromHtml("#CE9178");   // Orange-brown
         Color number = ColorTranslator.FromHtml("#B5CEA8");          // Light green
         Color preprocessor = ColorTranslator.FromHtml("#9B9B9B");    // Gray
         Color identifier = defaultText;
         Color operatorColor = defaultText;
         Color punctuation = defaultText;

         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = identifier;
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = keyword;
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = number;
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = comment;
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = preprocessor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = operatorColor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = punctuation;
         }

         // Language-specific overrides
         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#808080"); // Gray-blue for tags
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#808080");

         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#D7BA7D"); // Gold
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#D16969"); // Light red

         pTheme.mHighlightColors[(int)LanguageKind.Markdown][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#569CD6");

         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = defaultText;
         }
      }

      private static void SetClassicThemeSyntaxColors(Theme pTheme) {
         // Classic Visual Studio 6.0 style colors
         Color defaultText = Color.Black;
         Color keyword = Color.Blue;
         Color comment = ColorTranslator.FromHtml("#008000");         // Green
         Color stringLiteral = ColorTranslator.FromHtml("#A31515");   // Dark red
         Color number = Color.Black;
         Color preprocessor = ColorTranslator.FromHtml("#0000FF");    // Blue
         Color identifier = defaultText;
         Color operatorColor = defaultText;
         Color punctuation = defaultText;

         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = identifier;
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = keyword;
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = number;
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = comment;
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = preprocessor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = operatorColor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = punctuation;
         }

         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");

         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#FF0000");

         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = defaultText;
         }
      }

      private static void SetHighContrastLightSyntaxColors(Theme pTheme) {
         // High contrast with bold primary colors
         Color defaultText = Color.Black;
         Color keyword = Color.Blue;
         Color comment = ColorTranslator.FromHtml("#008000");         // Dark green
         Color stringLiteral = ColorTranslator.FromHtml("#A31515");   // Dark red
         Color number = ColorTranslator.FromHtml("#09885A");          // Dark teal
         Color preprocessor = ColorTranslator.FromHtml("#808080");    // Gray
         Color identifier = defaultText;
         Color operatorColor = defaultText;
         Color punctuation = defaultText;

         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = identifier;
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = keyword;
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = number;
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = comment;
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = preprocessor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = operatorColor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = punctuation;
         }

         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");

         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#800000");
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#C00000");

         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = defaultText;
         }
      }

      private static void SetHighContrastDarkSyntaxColors(Theme pTheme) {
         // High contrast dark with vivid colors
         Color defaultText = Color.White;
         Color keyword = ColorTranslator.FromHtml("#569CD6");         // Bright blue
         Color comment = ColorTranslator.FromHtml("#6A9955");         // Bright green
         Color stringLiteral = ColorTranslator.FromHtml("#CE9178");   // Orange
         Color number = ColorTranslator.FromHtml("#B5CEA8");          // Light green
         Color preprocessor = ColorTranslator.FromHtml("#C586C0");    // Purple
         Color identifier = defaultText;
         Color operatorColor = defaultText;
         Color punctuation = defaultText;

         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = identifier;
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = keyword;
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = number;
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = comment;
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = preprocessor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = operatorColor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = punctuation;
         }

         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#4EC9B0"); // Cyan
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#4EC9B0");

         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#D7BA7D"); // Gold
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#D16969"); // Light red

         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = defaultText;
         }
      }

      private static void SetPastelThemeSyntaxColors(Theme pTheme) {
         // Soft, gentle pastel colors for syntax
         Color defaultText = ColorTranslator.FromHtml("#3A3A3A");     // Charcoal
         Color keyword = ColorTranslator.FromHtml("#7B68A6");         // Muted purple
         Color comment = ColorTranslator.FromHtml("#7FA37C");         // Sage green
         Color stringLiteral = ColorTranslator.FromHtml("#C18686");   // Dusty rose
         Color number = ColorTranslator.FromHtml("#82B5A8");          // Pastel teal
         Color preprocessor = ColorTranslator.FromHtml("#9B9B9B");    // Medium gray
         Color identifier = defaultText;
         Color operatorColor = ColorTranslator.FromHtml("#6A6A6A");   // Dark gray
         Color punctuation = ColorTranslator.FromHtml("#6A6A6A");

         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = identifier;
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = keyword;
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = number;
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = comment;
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = preprocessor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = operatorColor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = punctuation;
         }

         // Language-specific pastel variations
         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#A88D8D"); // Dusty mauve
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#A88D8D");

         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#B8A67D"); // Tan
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#C18686"); // Dusty rose

         pTheme.mHighlightColors[(int)LanguageKind.Markdown][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#7B68A6"); // Muted purple

         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = defaultText;
         }
      }

      private static void SetDarkPastelThemeSyntaxColors(Theme pTheme) {
         // Soft, muted dark pastel colors - gentle on the eyes
         Color defaultText = ColorTranslator.FromHtml("#C9BDD8");     // Soft lavender
         Color keyword = ColorTranslator.FromHtml("#B19CD9");         // Pastel purple
         Color comment = ColorTranslator.FromHtml("#8CAF88");         // Muted sage green
         Color stringLiteral = ColorTranslator.FromHtml("#D4A5A5");   // Dusty pink
         Color number = ColorTranslator.FromHtml("#A0C4BC");          // Soft teal
         Color preprocessor = ColorTranslator.FromHtml("#9B9BA8");    // Soft gray-blue
         Color identifier = defaultText;
         Color operatorColor = ColorTranslator.FromHtml("#A8A8B8");   // Light gray
         Color punctuation = ColorTranslator.FromHtml("#A8A8B8");

         for (int lang = 0; lang < pTheme.mHighlightColors.Length; lang++) {
            pTheme.mHighlightColors[lang][(int)TokenKind.Unknown] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Whitespace] = defaultText;
            pTheme.mHighlightColors[lang][(int)TokenKind.Identifier] = identifier;
            pTheme.mHighlightColors[lang][(int)TokenKind.Keyword] = keyword;
            pTheme.mHighlightColors[lang][(int)TokenKind.Number] = number;
            pTheme.mHighlightColors[lang][(int)TokenKind.StringLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.CharLiteral] = stringLiteral;
            pTheme.mHighlightColors[lang][(int)TokenKind.Comment] = comment;
            pTheme.mHighlightColors[lang][(int)TokenKind.PreprocessorDirective] = preprocessor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Operator] = operatorColor;
            pTheme.mHighlightColors[lang][(int)TokenKind.Punctuation] = punctuation;
         }

         // Language-specific dark pastel variations
         pTheme.mHighlightColors[(int)LanguageKind.Html][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#C4A4A4"); // Soft mauve
         pTheme.mHighlightColors[(int)LanguageKind.Xml][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#C4A4A4");

         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#C9B896"); // Soft tan
         pTheme.mHighlightColors[(int)LanguageKind.Css][(int)TokenKind.Identifier] = ColorTranslator.FromHtml("#D4A5A5"); // Dusty pink

         pTheme.mHighlightColors[(int)LanguageKind.Markdown][(int)TokenKind.Keyword] = ColorTranslator.FromHtml("#B19CD9"); // Pastel purple

         for (int token = 0; token < pTheme.mHighlightColors[(int)LanguageKind.PlainText].Length; token++) {
            pTheme.mHighlightColors[(int)LanguageKind.PlainText][token] = defaultText;
         }
      }
   }
}
