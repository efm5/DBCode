namespace DBCode {
   internal sealed class UiState {
      // ───── Persisted geometry (read/written via Settings) ─────
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
      internal bool mFirstTheme;
      internal bool mFirstLaunch;
      internal int mTopDraggerHeight;
      internal int mTopDraggerEdge;
      internal int mActivationDelayMs;
      internal int mClipboardDelayMs;
      internal int mReactivationDelayMs;
      internal int mWhitespace;
      internal int mSpacesPerTab;
      internal int mSpacesToBecomeTab;
      internal bool mUseTabs;
      internal bool mUseSpaces;

      internal Rectangle FormBounds {
         get => new Rectangle(mFormLocation, mFormSize);
         set { mFormLocation = value.Location; mFormSize = value.Size; }
      }
      internal Rectangle ThemeBounds {
         get => new Rectangle(mThemeLocation, mThemeSize);
         set { mThemeLocation = value.Location; mThemeSize = value.Size; }
      }
      internal Rectangle ThemePickerBounds {
         get => new Rectangle(mThemePickerLocation, mThemePickerSize);
         set { mThemePickerLocation = value.Location; mThemePickerSize = value.Size; }
      }
      // ───── Session-only geometry (not persisted) ─────
      internal Rectangle mPickerBounds = new Rectangle(50, 50, 800, 600);
      internal Rectangle mGetStringBounds = new Rectangle(50, 50, 200, 300);
      internal Rectangle mColorPickerBounds = new Rectangle(50, 50, 450, 550);
      internal Rectangle mFontPickerBounds = new Rectangle(50, 50, 400, 400);
      internal Rectangle mOptionsBounds = new Rectangle(200, 200, 500, 400);

      public UiState() {
         mFormSize = new Size(400, 300);
         mFormLocation = new Point(200, 150);
         mThemeSize = new Size(600, 400);
         mThemeLocation = new Point(100, 100);
         mThemePickerSize = new Size(700, 500);
         mThemePickerLocation = new Point(610, 290);
         mFormOpacity = 1.0;
         mThemePrimaryTabPageIndex = 0;
         mThemeTargetingTabIndexIndex = 0;
         mThemeHighlightTabPageIndex = 0;
         mLanguageKind = LanguageKind.CSharp;
         mCurrentThemeName = string.Empty;
         mFirstTheme = true;
         mFirstLaunch = true;
         mTopDraggerHeight = 10;
         mTopDraggerEdge = 2;
         mActivationDelayMs = 350;
         mClipboardDelayMs = 350;
         mReactivationDelayMs = 350;
         mWhitespace = (int)Whitespace.Tabs;
         mSpacesPerTab = 3;
         mSpacesToBecomeTab = 3;
         mUseTabs = false;
         mUseSpaces = true;
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
         mFirstTheme = Settings.Default.FirstTheme;
         mFirstLaunch = Settings.Default.FirstLaunch;
         mTopDraggerHeight = Settings.Default.TopDraggerHeight;
         mTopDraggerEdge = Settings.Default.TopDraggerEdge;
         mActivationDelayMs = Settings.Default.ActivationDelayMs;
         mClipboardDelayMs = Settings.Default.ClipboardDelayMs;
         mReactivationDelayMs = Settings.Default.ReactivationDelayMs;
         mWhitespace = Settings.Default.Whitespace;
         mSpacesPerTab = Settings.Default.SpacesPerTab;
         mSpacesToBecomeTab = Settings.Default.SpacesToBecomeTab;
         mUseTabs = Settings.Default.UseTabs;
         mUseSpaces = Settings.Default.UseSpaces;
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
         Settings.Default.FirstTheme = mFirstTheme;
         Settings.Default.FirstLaunch = mFirstLaunch;
         Settings.Default.TopDraggerHeight = mTopDraggerHeight;
         Settings.Default.TopDraggerEdge = mTopDraggerEdge;
         Settings.Default.ActivationDelayMs = mActivationDelayMs;
         Settings.Default.ClipboardDelayMs = mClipboardDelayMs;
         Settings.Default.ReactivationDelayMs = mReactivationDelayMs;
         Settings.Default.Whitespace = (int)mWhitespace;
         Settings.Default.SpacesPerTab = mSpacesPerTab;
         Settings.Default.SpacesToBecomeTab = mSpacesToBecomeTab;
         Settings.Default.UseTabs = mUseTabs;
         Settings.Default.UseSpaces = mUseSpaces;
      }
   }
}
