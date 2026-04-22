namespace DBCode.Themes {
   public static class ThemeRegistry {
      public static List<Theme> mThemeList { get; private set; } = [];

      public static void Initialize(string pFolderPath, string pLastUsedThemeName) {
         List<Theme> themes = [];
         AddBuiltIns(themes);
         List<Theme> loaded = ThemeManager.LoadThemes();
         AddUserThemes(themes, loaded);
         List<Theme> validated = ValidateThemes(themes);
         mThemeList = validated;
         mPreviousThemeName = SelectThemeName(validated);
      }

      public static void Reload(string pFolderPath) {
         Initialize(pFolderPath, mPreviousThemeName);
      }

      public static bool SetCurrentTheme(string pName) {
         foreach (Theme theme in mThemeList) {
            if (String.Equals(theme.mName, pName, StringComparison.OrdinalIgnoreCase)) {
               mPreviousThemeName = theme.mName;
               return true;
            }
         }
         return false;
      }

      public static Theme GetCurrentThemeClone() {
         foreach (Theme theme in mThemeList) {
            if (String.Equals(theme.mName, mPreviousThemeName, StringComparison.OrdinalIgnoreCase))
               return theme.Clone();
         }
         return ThemeDefaults.DefaultLight;
      }

      private static void AddBuiltIns(List<Theme> pList) {
         pList.Add(ThemeDefaults.DefaultLight);
         pList.Add(ThemeDefaults.DefaultDark);
         pList.Add(ThemeDefaults.HighContrastLight);
         pList.Add(ThemeDefaults.HighContrastDark);
         pList.Add(ThemeDefaults.Classic);
         pList.Add(ThemeDefaults.LightPastel);
         pList.Add(ThemeDefaults.DarkPastel);
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

      private static string SelectThemeName(List<Theme> pThemes) {
         foreach (Theme theme in pThemes) {
            if (String.Equals(theme.mName, mPreviousThemeName, StringComparison.OrdinalIgnoreCase))
               return theme.mName;
         }
         if (pThemes.Count > 0)
            return pThemes[0].mName;
         return ThemeDefaults.DefaultLight.mName;
      }
   }
}
