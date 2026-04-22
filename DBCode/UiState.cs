using DBCode.Syntax;

namespace DBCode {
   internal sealed class UiState {
      public Size mFormSize;
      public Point mFormLocation;
      public Size mThemeSize;
      public Point mThemeLocation;
      public Size mThemePickerSize;
      public Point mThemePickerLocation;
      public double mFormOpacity;
      public int mThemePrimaryTabPageIndex;
      public int mThemeHighlightTabPageIndex;

      public UiState() {
         mFormSize = new Size(400, 300);
         mFormLocation = new Point(200, 150);
         mThemeSize = new Size(600, 400);
         mThemeLocation = new Point(100, 100);
         mThemePickerSize = new Size(600, 400);
         mThemePickerLocation = new Point(100, 100);
         mFormOpacity = 1.0;
         mThemePrimaryTabPageIndex = 0;
         mThemeHighlightTabPageIndex = 0;
      }

      public void ReadFromSettings() {
         mFormSize = Settings.Default.FormSize;
         mFormLocation = Settings.Default.FormLocation;
         mThemeSize = Settings.Default.ThemeSize;
         mThemeLocation = Settings.Default.ThemeLocation;
         mFormOpacity = Settings.Default.FormOpacity;
         mThemePrimaryTabPageIndex = Settings.Default.ThemePrimaryTabPageIndex;
         mThemeHighlightTabPageIndex = Settings.Default.ThemeHighlightTabPageIndex;
         mPreviousThemeName = Settings.Default.CurrentThemeName;
         mThemePickerSize = Settings.Default.ThemePickerSize;
         mThemePickerLocation = Settings.Default.ThemePickerLocation;
         mCurrentLanguage = (LanguageKind)Settings.Default.CurrentLanguage;
      }

      public void WriteToSettings() {
         Settings.Default.FormSize = mFormSize;
         Settings.Default.FormLocation = mFormLocation;
         Settings.Default.ThemeSize = mThemeSize;
         Settings.Default.ThemeLocation = mThemeLocation;
         Settings.Default.FormOpacity = mFormOpacity;
         Settings.Default.ThemePrimaryTabPageIndex = mThemePrimaryTabPageIndex;
         Settings.Default.ThemeHighlightTabPageIndex = mThemeHighlightTabPageIndex;
         Settings.Default.ThemePickerSize = mThemePickerSize;
         Settings.Default.ThemePickerLocation = mThemePickerLocation;
         Settings.Default.CurrentLanguage = (int)mCurrentLanguage;
      }
   }
}
