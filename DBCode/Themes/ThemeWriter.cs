using System.Text.Json;

namespace DBCode.Themes {
   public static class ThemeWriter {
      public static void SaveTheme(string pFolderPath, Theme pTheme) {
         ArgumentNullException.ThrowIfNull(pTheme);
         if (!Directory.Exists(pFolderPath))
            Directory.CreateDirectory(pFolderPath);

         string fileName = pTheme.mName + ".json";
         string filePath = Path.Combine(pFolderPath, fileName);
         string json = BuildJson(pTheme);
         File.WriteAllText(filePath, json);
      }

      public static void SaveAllThemes(string pFolderPath, List<Theme> pThemes) {
         ArgumentNullException.ThrowIfNull(pThemes);
         if (!Directory.Exists(pFolderPath))
            Directory.CreateDirectory(pFolderPath);

         foreach (Theme theme in pThemes)
            SaveTheme(pFolderPath, theme);
      }

      private static string BuildJson(Theme pTheme) {
         Dictionary<string, object> root = new Dictionary<string, object> {
            ["Name"] = pTheme.mName,
            ["Brightness"] = pTheme.mBrightness.ToString()
         };
         Dictionary<string, string> fonts = [];
         Dictionary<string, string> colors = [];

         foreach (FontUsage usage in Enum.GetValues<FontUsage>()) {
            Font font = pTheme.mFonts[(int)usage];
            fonts[usage.ToString()] = new FontConverter().ConvertToString(font) ?? "";
         }
         foreach (ColorUsage usage in Enum.GetValues<ColorUsage>()) {
            Color color = pTheme.mColors[(int)usage];
            colors[usage.ToString()] = ColorToString(color);
         }
         root["Fonts"] = fonts;
         root["Colors"] = colors;
         JsonSerializerOptions options = new JsonSerializerOptions {
            WriteIndented = true
         };
         return JsonSerializer.Serialize(root, options);
      }

      private static string ColorToString(Color pColor) {
         if (pColor.IsNamedColor)
            return pColor.Name;
         return $"#{pColor.R:X2}{pColor.G:X2}{pColor.B:X2}";
      }
   }
}
