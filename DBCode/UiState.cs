namespace DBCode {
   internal sealed class UiState {
      internal Size mFormSize;
      internal Point mFormLocation;
      internal Size mThemeSize;
      internal Point mThemeLocation;
      internal Size mThemePickerSize;
      internal Point mThemePickerLocation;
      internal double mFormOpacity;
      internal int mThemePrimaryTabPageIndex;
      internal int mThemeTargetingTabIndexIndex;
      internal int mThemeHighlightTabPageIndex;
      internal LanguageKind mLanguageKind;
      internal string mCurrentThemeName;

      public UiState() {
         mFormSize = new Size(400, 300);
         mFormLocation = new Point(200, 150);
         mThemeSize = new Size(600, 400);
         mThemeLocation = new Point(100, 100);
         mThemePickerSize = new Size(600, 400);
         mThemePickerLocation = new Point(100, 100);
         mFormOpacity = 1.0;
         mThemePrimaryTabPageIndex = 0;
         mThemeTargetingTabIndexIndex = 0;
         mThemeHighlightTabPageIndex = 0;
         mLanguageKind = LanguageKind.CSharp;
         mCurrentThemeName = string.Empty;
      }

      public void ReadFromSettings() {
         mFormSize = Settings.Default.FormSize;
         mFormLocation = Settings.Default.FormLocation;
         mThemeSize = Settings.Default.ThemeSize;
         mThemeLocation = Settings.Default.ThemeLocation;
         mFormOpacity = Settings.Default.FormOpacity;
         mThemePrimaryTabPageIndex = Settings.Default.ThemePrimaryTabPageIndex;
         mThemeTargetingTabIndexIndex = Settings.Default.ThemeTargetingTabPageIndex;
         mThemeHighlightTabPageIndex = Settings.Default.ThemeHighlightTabPageIndex;
         mUsingThemeName = Settings.Default.CurrentThemeName;
         mThemePickerSize = Settings.Default.ThemePickerSize;
         mThemePickerLocation = Settings.Default.ThemePickerLocation;
         mCurrentLanguage = (LanguageKind)Settings.Default.CurrentLanguage;
         mCurrentThemeName = Settings.Default.CurrentThemeName;
      }

      public void WriteToSettings() {
         Settings.Default.FormSize = mFormSize;
         Settings.Default.FormLocation = mFormLocation;
         Settings.Default.ThemeSize = mThemeSize;
         Settings.Default.ThemeLocation = mThemeLocation;
         Settings.Default.ThemePickerSize = mThemePickerSize;
         Settings.Default.ThemePickerLocation = mThemePickerLocation;
         Settings.Default.FormOpacity = mFormOpacity;
         Settings.Default.ThemePrimaryTabPageIndex = mThemePrimaryTabPageIndex;
         Settings.Default.ThemeTargetingTabPageIndex = mThemeTargetingTabIndexIndex;
         Settings.Default.ThemeHighlightTabPageIndex = mThemeHighlightTabPageIndex;
         Settings.Default.CurrentLanguage = (int)mCurrentLanguage;
         Settings.Default.CurrentThemeName = mCurrentThemeName;
      }
   }
}
