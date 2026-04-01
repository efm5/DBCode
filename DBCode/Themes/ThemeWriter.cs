using System.Text.Json;

namespace DBCode.Themes {
   public static class ThemeWriter {
      public static void SaveTheme(string pFolderPath, Theme pTheme) {
         if (pTheme == null)
            throw new ArgumentNullException(nameof(pTheme));

         if (!Directory.Exists(pFolderPath))
            Directory.CreateDirectory(pFolderPath);

         string fileName = pTheme.mName + ".json";
         string filePath = Path.Combine(pFolderPath, fileName);

         string json = BuildJson(pTheme);
         File.WriteAllText(filePath, json);
      }

      public static void SaveAllThemes(string pFolderPath, List<Theme> pThemes) {
         if (pThemes == null)
            throw new ArgumentNullException(nameof(pThemes));

         if (!Directory.Exists(pFolderPath))
            Directory.CreateDirectory(pFolderPath);

         foreach (Theme theme in pThemes)
            SaveTheme(pFolderPath, theme);
      }

      private static string BuildJson(Theme pTheme) {
         Dictionary<string, object> root = new Dictionary<string, object>();

         root["Name"] = pTheme.mName;
         root["Brightness"] = pTheme.mBrightness.ToString();

         Dictionary<string, string> fonts = new Dictionary<string, string>();
         foreach (FontUsage usage in Enum.GetValues(typeof(FontUsage))) {
            Font font = pTheme.mFonts[(int)usage];
            fonts[usage.ToString()] = new FontConverter().ConvertToString(font) ?? "";
         }
         root["Fonts"] = fonts;

         Dictionary<string, string> colors = new Dictionary<string, string>();
         foreach (ColorUsage usage in Enum.GetValues(typeof(ColorUsage))) {
            Color color = pTheme.mColors[(int)usage];
            colors[usage.ToString()] = ColorToString(color);
         }
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
