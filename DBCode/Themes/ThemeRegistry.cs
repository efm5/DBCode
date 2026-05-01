namespace DBCode.Themes {
   public static class ThemeRegistry {
      public static void Initialize() {
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         AddBuiltIns();
         List<Theme> loaded = ThemeManager.LoadThemes();
         AddUserThemes(loaded);
         List<Theme> validated = ValidateThemes(mThemes);
         mThemes = validated;
         mUsingThemeName = SelectThemeName(mThemes);
         if (SetCurrentThemeName(mUsingThemeName)) {
            mCurrentTheme.Dispose();
            mCurrentTheme = GetCurrentThemeClone();
         }
      }

      public static void Reload() {
         Initialize();
      }

      public static bool SetCurrentThemeName(string pName) {
         foreach (Theme theme in mThemes) {
            if (String.Equals(theme.mName, pName, StringComparison.OrdinalIgnoreCase)) {
               mUsingThemeName = theme.mName;
               return true;
            }
         }
         return false;
      }

      public static bool ThemeNameIsUnique(string pName) {
         foreach (Theme theme in mThemes) {
            if (String.Equals(theme.mName, pName, StringComparison.OrdinalIgnoreCase))
               return false;
         }
         return true;
      }

      public static void AddTheme(Theme pTheme) {
         if (ThemeNameIsUnique(pTheme.mName))
            mThemes.Add(pTheme);
      }

      public static Theme GetCurrentThemeClone() {
         foreach (Theme theme in mThemes) {
            if (String.Equals(theme.mName, mUsingThemeName, StringComparison.OrdinalIgnoreCase))
               return theme.Clone();
         }
         return ThemeDefaults.DefaultLight;
      }

      private static void AddBuiltIns() {
         mThemes.Add(ThemeDefaults.DefaultLight);
         mThemes.Add(ThemeDefaults.DefaultDark);
         mThemes.Add(ThemeDefaults.HighContrastLight);
         mThemes.Add(ThemeDefaults.HighContrastDark);
         mThemes.Add(ThemeDefaults.Classic);
         mThemes.Add(ThemeDefaults.LightPastel);
         mThemes.Add(ThemeDefaults.DarkPastel);
      }

      private static void AddUserThemes(List<Theme> pUserThemes) {
         foreach (Theme theme in pUserThemes)
            AddThemeIfUnique(theme);
      }

      private static bool AddThemeIfUnique(Theme pTheme) {
         foreach (Theme existing in mThemes) {
            if (String.Equals(existing.mName, pTheme.mName, StringComparison.OrdinalIgnoreCase))
               return false;
         }
         mThemes.Add(pTheme);
         return true;
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
            if (String.Equals(theme.mName, mUsingThemeName, StringComparison.OrdinalIgnoreCase))
               return theme.mName;
         }
         if (pThemes.Count > 0)
            return pThemes[0].mName;
         return ThemeDefaults.DefaultLight.mName;
      }
   }
}
