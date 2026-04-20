namespace DBCode.Themes {
   public static class ThemeRegistry {

      public static List<Theme> Themes { get; private set; } = [];
      public static string LastUsedThemeName { get; private set; } = String.Empty;

      public static void Initialize(string pFolderPath, string pLastUsedThemeName) {
         List<Theme> themes = [];

         AddBuiltIns(themes);

         List<Theme> loaded = ThemeManager.LoadThemes(pFolderPath, pLastUsedThemeName);
         AddUserThemes(themes, loaded);

         List<Theme> validated = ValidateThemes(themes);

         Themes = validated;
         LastUsedThemeName = SelectThemeName(validated, pLastUsedThemeName);
      }

      public static void Reload(string pFolderPath) {
         string previous = LastUsedThemeName;
         Initialize(pFolderPath, previous);
      }

      public static bool SetCurrentTheme(string pName) {
         foreach (Theme theme in Themes) {
            if (String.Equals(theme.mName, pName, StringComparison.OrdinalIgnoreCase)) {
               LastUsedThemeName = theme.mName;
               return true;
            }
         }
         return false;
      }

      public static Theme GetCurrentThemeClone() {
         foreach (Theme theme in Themes) {
            if (String.Equals(theme.mName, LastUsedThemeName, StringComparison.OrdinalIgnoreCase))
               return theme.Clone();
         }
         return ThemeDefaults.DefaultLight;
      }

      private static void AddBuiltIns(List<Theme> pList) {
         pList.Add(ThemeDefaults.DefaultLight);
         pList.Add(ThemeDefaults.DefaultDark);
         pList.Add(ThemeDefaults.HighContrastLight);
         pList.Add(ThemeDefaults.HighContrastDark);
         pList.Add(ThemeDefaults.ClassicWin32);
         pList.Add(ThemeDefaults.PastelBreeze);
      }

      private static void AddUserThemes(List<Theme> pList, List<Theme> pUserThemes) {
         foreach (Theme theme in pUserThemes)
            AddThemeIfUnique(pList, theme);
      }

      private static void AddThemeIfUnique(List<Theme> pList, Theme pTheme) {
         foreach (Theme existing in pList) {
            if (String.Equals(existing.mName, pTheme.mName, StringComparison.OrdinalIgnoreCase))
               return;
         }
         pList.Add(pTheme);
      }

      private static List<Theme> ValidateThemes(List<Theme> pThemes) {
         List<Theme> result = [];

         foreach (Theme theme in pThemes) {
            List<string> issues = ThemeDiagnostics.RunReport(theme);
            if (issues.Count == 0)
               result.Add(theme);
         }
         return result;
      }

      private static string SelectThemeName(List<Theme> pThemes, string pLastUsedThemeName) {
         foreach (Theme theme in pThemes) {
            if (String.Equals(theme.mName, pLastUsedThemeName, StringComparison.OrdinalIgnoreCase))
               return theme.mName;
         }
         if (pThemes.Count > 0)
            return pThemes[0].mName;
         return ThemeDefaults.DefaultLight.mName;
      }
   }
}
