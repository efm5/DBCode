namespace DBCode.Themes {
   public static class ThemeRegistry {
      public static List<Theme> Themes { get; private set; } = new List<Theme>();
      public static Theme CurrentTheme { get; private set; } = new Theme(String.Empty);
      public static string LastUsedThemeName { get; private set; } = String.Empty;

      public static void Initialize(string pFolderPath, string pLastUsedThemeName) {
         _ = pLastUsedThemeName;

         List<Theme> loaded = ThemeManager.LoadThemes(pFolderPath, pLastUsedThemeName);
         List<Theme> validated = ValidateThemes(loaded);

         if (validated.Count == 0) {
            validated.Add(ThemeDefaults.DefaultLight);
            validated.Add(ThemeDefaults.DefaultDark);
         }

         Themes = validated;

         Theme selected = SelectTheme(validated, pLastUsedThemeName);
         CurrentTheme = selected;
         LastUsedThemeName = selected.mName;
      }

      public static void Reload(string pFolderPath) {
         string previous = LastUsedThemeName;

         List<Theme> loaded = ThemeManager.LoadThemes(pFolderPath, previous);
         List<Theme> validated = ValidateThemes(loaded);

         if (validated.Count == 0) {
            validated.Add(ThemeDefaults.DefaultLight);
            validated.Add(ThemeDefaults.DefaultDark);
         }

         Themes = validated;

         Theme selected = SelectTheme(validated, previous);
         CurrentTheme = selected;
         LastUsedThemeName = selected.mName;
      }

      public static bool SetCurrentTheme(string pName) {
         foreach (Theme theme in Themes) {
            if (String.Equals(theme.mName, pName, StringComparison.OrdinalIgnoreCase)) {
               CurrentTheme = theme;
               LastUsedThemeName = theme.mName;
               return true;
            }
         }
         return false;
      }

      private static List<Theme> ValidateThemes(List<Theme> pThemes) {
         List<Theme> result = new List<Theme>();

         foreach (Theme theme in pThemes) {
            List<string> issues = ThemeDiagnostics.RunReport(theme);

            if (issues.Count == 0)
               result.Add(theme);
         }

         return result;
      }

      private static Theme SelectTheme(List<Theme> pThemes, string pLastUsedThemeName) {
         foreach (Theme theme in pThemes) {
            if (String.Equals(theme.mName, pLastUsedThemeName, StringComparison.OrdinalIgnoreCase))
               return theme;
         }

         if (pThemes.Count > 0)
            return pThemes[0];

         return ThemeDefaults.DefaultLight;
      }
   }
}
