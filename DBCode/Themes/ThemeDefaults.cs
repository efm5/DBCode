namespace DBCode.Themes {
   public static class ThemeDefaults {
      public static Theme DefaultLight { get => field.Clone(); } = ThemeBuiltIns.CreateLightTheme(false);
      public static Theme DefaultDark { get => field.Clone(); } = ThemeBuiltIns.CreateDarkTheme(false);
      public static Theme HighContrastLight { get => field.Clone(); } = ThemeBuiltIns.CreateHighContrastLightTheme(false);
      public static Theme HighContrastDark { get => field.Clone(); } = ThemeBuiltIns.CreateHighContrastDarkTheme(false);
      public static Theme Classic { get => field.Clone(); } = ThemeBuiltIns.CreateClassicTheme(false);
      public static Theme LightPastel { get => field.Clone(); } = ThemeBuiltIns.CreateLightPastelTheme(false);
      public static Theme DarkPastel { get => field.Clone(); } = ThemeBuiltIns.CreateDarkPastelTheme(false);
   }
}
