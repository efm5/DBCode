using System.Text.Json;
using static DBCode.Themes.ThemeBrightnessHelper;

namespace DBCode.Themes {
   public static class ThemeManager {
      public static List<Theme> LoadThemes() {
         List<Theme> themes = [];
         if (!Directory.Exists(mDataFolder))
            return themes;
         string[] files = Directory.GetFiles(mDataFolder, "*.json", SearchOption.TopDirectoryOnly);
         foreach (string file in files) {
            try {
               Theme theme = LoadThemeFromJson(file);
               themes.Add(theme);
            }
            catch (Exception pException) {
               string name = Path.GetFileName(file);
               throw new Exception($"Failed to load theme '{name}'.", pException);
            }
         }
         return themes;
      }

      public static Theme SelectTheme(List<Theme> pThemes, string pLastUsedThemeName) {
         foreach (Theme theme in pThemes) {
            if (String.Equals(theme.mName, pLastUsedThemeName, StringComparison.OrdinalIgnoreCase))
               return theme;
         }

         if (pThemes.Count > 0)
            return pThemes[0];
         return new Theme("Default");
      }

      private static Theme LoadThemeFromJson(string pFilePath) {
         string json = File.ReadAllText(pFilePath);
         JsonDocument doc = JsonDocument.Parse(json);
         JsonElement root = doc.RootElement;
         string name = root.GetProperty("Name").GetString() ?? "Unnamed";
         ThemeBrightness brightness = Enum.Parse<ThemeBrightness>(root.GetProperty("Brightness").GetString() ?? "Light", true);
         Theme theme = new Theme(name) {
            mBrightness = brightness
         };
         JsonElement fonts = root.GetProperty("Fonts");

         foreach (FontUsage usage in Enum.GetValues<FontUsage>()) {
            string key = usage.ToString();
            if (!fonts.TryGetProperty(key, out JsonElement fontElement))
               throw new Exception($"Theme '{name}' is missing font '{key}'.");
            string fontString = fontElement.GetString() ?? "";

            theme.mFonts[(int)usage] = ParseFont(fontString);
         }

         JsonElement colors = root.GetProperty("Colors");
         foreach (ColorUsage usage in Enum.GetValues<ColorUsage>()) {
            string key = usage.ToString();
            if (!colors.TryGetProperty(key, out JsonElement colorElement))
               throw new Exception($"Theme '{name}' is missing color '{key}'.");
            string colorString = colorElement.GetString() ?? "";

            theme.mInterfaceColors[(int)usage] = ParseColor(colorString);
         }

         if (root.TryGetProperty("HighlightColors", out JsonElement highlightColors)) {
            foreach (DBCode.Syntax.LanguageKind language in Enum.GetValues<DBCode.Syntax.LanguageKind>()) {
               string languageKey = language.ToString();
               if (highlightColors.TryGetProperty(languageKey, out JsonElement tokenColors)) {
                  foreach (DBCode.Syntax.TokenKind token in Enum.GetValues<DBCode.Syntax.TokenKind>()) {
                     string tokenKey = token.ToString();
                     if (tokenColors.TryGetProperty(tokenKey, out JsonElement colorElement)) {
                        string colorString = colorElement.GetString() ?? "";
                        theme.mHighlightColors[(int)language][(int)token] = ParseColor(colorString);
                     }
                  }
               }
            }
         }
         return theme;
      }

      private static Font ParseFont(string pFontString) {
         try {
            return new FontConverter().ConvertFromString(pFontString) as Font
                   ?? new Font("Segoe UI", 12f);
         }
         catch {
            return new Font("Segoe UI", 12f);
         }
      }

      private static Color ParseColor(string pColorString) {
         if (String.IsNullOrWhiteSpace(pColorString))
            return Color.Black;
         if (pColorString.StartsWith('#'))
            return ColorTranslator.FromHtml(pColorString);
         try {
            return Color.FromName(pColorString);
         }
         catch {
            return Color.Black;
         }
      }
   }
}
