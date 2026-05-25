namespace DBCode {
   internal sealed class UiState {
      // ───── Persisted via Settings.Default ─────
      internal bool mFirstTheme, mFirstLaunch, mUseTabs, mUseSpaces, mUsePCRE, mFindMatchCase, mFindWholeWord, mFindRegularExpressions,
         mFindScopeSelection, mSRMatchCase, mSRWholeWord, mSRRegularExpressions, mSRScopeSelection, mAllIfNothing, mConcatenateCommentFirst,
         mShortcutsDgvAutoSize, mCommentOutBlankLines, mUseThreeSpaces, mEnforceFormattingProtection;
      internal Color mBracePairColor0, mBracePairColor1, mBracePairColor2, mBracePairColor3, mBracePairColor4, mBracePairColor5,
         mBracePairColor6, mBracePairColor7, mBracePairColor8, mBracePairColor9;
      internal double mFormOpacity;
      internal Point mFormLocation, mThemeLocation, mThemePickerLocation;
      internal Size mFormSize, mThemeSize, mThemePickerSize;
      internal int mThemePrimaryTabPageIndex, mThemeTargetingTabIndexIndex, mThemeHighlightTabPageIndex, mOptionsCodingTabControlPageIndex,
         mTopDraggerHeight, mTopDraggerEdge, mActivationDelayMs, mClipboardDelayMs, mReactivationDelayMs,
         mWhitespace, mSpacesPerTab, mSpacesToBecomeTab, mOptionsGeneralTabControlPageIndex,
         mOptionsIncludeExcludeTabControlPageIndex, mSearchHistoryMaxEntries, mReplaceHistoryMaxEntries, mMaximumCommentWidth;
      internal LanguageKind mLanguageKind;
      internal LineEndings mLineEnding;
      internal string mCurrentThemeName, mShortcutsColumnWidths;
      // ───── Persisted as text files ─────
      internal List<string> mFindSearchHistory = [];
      internal List<string> mFindPcreSearchHistory = [];
      internal List<string> mSrSearchHistory = [];
      internal List<string> mSrPcreSearchHistory = [];
      internal List<string> mSrReplaceHistory = [];
      internal List<string> mSrPcreReplaceHistory = [];

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
      internal Rectangle mGetStringBounds = new Rectangle(50, 50, 200, 300);
      internal Rectangle mColorPickerBounds = new Rectangle(50, 50, 450, 550);
      internal Rectangle mFontPickerBounds = new Rectangle(50, 50, 400, 400);
      internal Rectangle mOptionsBounds = new Rectangle(200, 200, 500, 400);
      internal Rectangle mTargetPickerBounds = new Rectangle(50, 50, 800, 600);
      internal bool mTargetPickerFirstShow = true;
      internal bool mOptionsFirstShow = true;
      internal bool mThemePickerFirstShow = true;
      internal bool mColorPickerFirstShow = true;
      internal bool mFontPickerFirstShow = true;

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
         mOptionsGeneralTabControlPageIndex = 0;
         mOptionsIncludeExcludeTabControlPageIndex = 0;
         mOptionsCodingTabControlPageIndex = 0;
         mLanguageKind = LanguageKind.CSharp;
         mCurrentThemeName = string.Empty;
         mShortcutsColumnWidths = string.Empty;
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
         mSearchHistoryMaxEntries = 25;
         mReplaceHistoryMaxEntries = 25;
         mUsePCRE = false;
         mFindMatchCase = false;
         mFindWholeWord = false;
         mFindRegularExpressions = false;
         mFindScopeSelection = false;
         mSRMatchCase = false;
         mSRWholeWord = false;
         mSRRegularExpressions = false;
         mSRScopeSelection = false;
         mAllIfNothing = false;
         mConcatenateCommentFirst = true;
         mMaximumCommentWidth = 80;
         mShortcutsDgvAutoSize = true;
         mCommentOutBlankLines = true;
         mUseThreeSpaces = true;
         mBracePairColor0 = Color.FromArgb(196, 160, 16);   // gold
         mBracePairColor1 = Color.FromArgb(32, 138, 138);   // teal
         mBracePairColor2 = Color.FromArgb(196, 32, 48);    // crimson
         mBracePairColor3 = Color.FromArgb(80, 32, 192);    // indigo
         mBracePairColor4 = Color.FromArgb(168, 32, 192);   // magenta
         mBracePairColor5 = Color.FromArgb(32, 138, 68);    // forest green
         mBracePairColor6 = Color.FromArgb(138, 80, 48);    // sienna
         mBracePairColor7 = Color.FromArgb(32, 80, 192);    // cobalt
         mBracePairColor8 = Color.FromArgb(196, 98, 0);     // amber
         mBracePairColor9 = Color.FromArgb(106, 138, 28);   // olive
         mLineEnding = LineEndings.Default;
         mEnforceFormattingProtection = true;
      }

      public void Read() {
         ReadSettings();
         LoadHistory(mFindSearchHistory, mFindSearchHistoryFile, mSearchHistoryMaxEntries);
         LoadHistory(mFindPcreSearchHistory, mFindPcreSearchHistoryFile, mSearchHistoryMaxEntries);
         LoadHistory(mSrSearchHistory, mSrSearchHistoryFile, mSearchHistoryMaxEntries);
         LoadHistory(mSrPcreSearchHistory, mSrPcreSearchHistoryFile, mSearchHistoryMaxEntries);
         LoadHistory(mSrReplaceHistory, mSrReplaceHistoryFile, mReplaceHistoryMaxEntries);
         LoadHistory(mSrPcreReplaceHistory, mSrPcreReplaceHistoryFile, mReplaceHistoryMaxEntries);
      }

      public void Write() {
         WriteSettings();
         SaveHistory(mFindSearchHistory, mFindSearchHistoryFile, mSearchHistoryMaxEntries);
         SaveHistory(mFindPcreSearchHistory, mFindPcreSearchHistoryFile, mSearchHistoryMaxEntries);
         SaveHistory(mSrSearchHistory, mSrSearchHistoryFile, mSearchHistoryMaxEntries);
         SaveHistory(mSrPcreSearchHistory, mSrPcreSearchHistoryFile, mSearchHistoryMaxEntries);
         SaveHistory(mSrReplaceHistory, mSrReplaceHistoryFile, mReplaceHistoryMaxEntries);
         SaveHistory(mSrPcreReplaceHistory, mSrPcreReplaceHistoryFile, mReplaceHistoryMaxEntries);
      }

      private void ReadSettings() {
         mFormSize = Settings.Default.FormSize;
         mFormLocation = Settings.Default.FormLocation;
         mThemeSize = Settings.Default.ThemeSize;
         mThemeLocation = Settings.Default.ThemeLocation;
         mFormOpacity = Settings.Default.FormOpacity;
         mThemePrimaryTabPageIndex = Settings.Default.ThemePrimaryTabPageIndex;
         mThemeTargetingTabIndexIndex = Settings.Default.ThemeTargetingTabPageIndex;
         mThemeHighlightTabPageIndex = Settings.Default.ThemeHighlightTabPageIndex;
         mOptionsGeneralTabControlPageIndex = Settings.Default.OptionsGeneralTabControlPageIndex;
         mOptionsIncludeExcludeTabControlPageIndex = Settings.Default.OptionsIncludeExcludeTabControlPageIndex;
         mOptionsCodingTabControlPageIndex = Settings.Default.OptionsCodingTabControlPageIndex;
         mUsingThemeName = Settings.Default.CurrentThemeName;
         mThemePickerSize = Settings.Default.ThemePickerSize;
         mThemePickerLocation = Settings.Default.ThemePickerLocation;
         mCurrentLanguage = (LanguageKind)Settings.Default.CurrentLanguage;
         mCurrentThemeName = Settings.Default.CurrentThemeName;
         mShortcutsColumnWidths = Settings.Default.ShortcutsColumnWidths;
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
         mSearchHistoryMaxEntries = Settings.Default.SearchHistoryMaxEntries;
         mReplaceHistoryMaxEntries = Settings.Default.ReplaceHistoryMaxEntries;
         mUsePCRE = Settings.Default.UsePCRE;
         mFindMatchCase = Settings.Default.FindMatchCase;
         mFindWholeWord = Settings.Default.FindWholeWord;
         mFindRegularExpressions = Settings.Default.FindRegularExpressions;
         mFindScopeSelection = Settings.Default.FindScopeSelection;
         mSRMatchCase = Settings.Default.SRMatchCase;
         mSRWholeWord = Settings.Default.SRWholeWord;
         mSRRegularExpressions = Settings.Default.SRRegularExpressions;
         mSRScopeSelection = Settings.Default.SRScopeSelection;
         mAllIfNothing = Settings.Default.AllIfNothing;
         mConcatenateCommentFirst = Settings.Default.ConcatenateCommentFirst;
         mShortcutsDgvAutoSize = Settings.Default.ShortcutsDgvAutoSize;
         mMaximumCommentWidth = Settings.Default.CommentWidth;
         mBracePairColor0 = Settings.Default.BracePairColor0;
         mBracePairColor1 = Settings.Default.BracePairColor1;
         mBracePairColor2 = Settings.Default.BracePairColor2;
         mBracePairColor3 = Settings.Default.BracePairColor3;
         mBracePairColor4 = Settings.Default.BracePairColor4;
         mBracePairColor5 = Settings.Default.BracePairColor5;
         mBracePairColor6 = Settings.Default.BracePairColor6;
         mBracePairColor7 = Settings.Default.BracePairColor7;
         mBracePairColor8 = Settings.Default.BracePairColor8;
         mBracePairColor9 = Settings.Default.BracePairColor9;
         mUseThreeSpaces = Settings.Default.UseThreeSpaces;
         mCommentOutBlankLines = Settings.Default.CommentOutBlankLines;
         mEnforceFormattingProtection = Settings.Default.EnforceFormattingProtection;
      }

      private void WriteSettings() {
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
         Settings.Default.OptionsGeneralTabControlPageIndex = mOptionsGeneralTabControlPageIndex;
         Settings.Default.OptionsIncludeExcludeTabControlPageIndex = mOptionsIncludeExcludeTabControlPageIndex;
         Settings.Default.OptionsCodingTabControlPageIndex = mOptionsCodingTabControlPageIndex;
         Settings.Default.CurrentLanguage = (int)mCurrentLanguage;
         Settings.Default.CurrentThemeName = mCurrentThemeName;
         Settings.Default.ShortcutsColumnWidths = mShortcutsColumnWidths;
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
         Settings.Default.SearchHistoryMaxEntries = mSearchHistoryMaxEntries;
         Settings.Default.ReplaceHistoryMaxEntries = mReplaceHistoryMaxEntries;
         Settings.Default.UsePCRE = mUsePCRE;
         Settings.Default.FindMatchCase = mFindMatchCase;
         Settings.Default.FindWholeWord = mFindWholeWord;
         Settings.Default.FindRegularExpressions = mFindRegularExpressions;
         Settings.Default.FindScopeSelection = mFindScopeSelection;
         Settings.Default.SRMatchCase = mSRMatchCase;
         Settings.Default.SRWholeWord = mSRWholeWord;
         Settings.Default.SRRegularExpressions = mSRRegularExpressions;
         Settings.Default.SRScopeSelection = mSRScopeSelection;
         Settings.Default.AllIfNothing = mAllIfNothing;
         Settings.Default.ConcatenateCommentFirst = mConcatenateCommentFirst;
         Settings.Default.ShortcutsDgvAutoSize = mShortcutsDgvAutoSize;
         Settings.Default.CommentWidth = mMaximumCommentWidth;
         Settings.Default.BracePairColor0 = mBracePairColor0;
         Settings.Default.BracePairColor1 = mBracePairColor1;
         Settings.Default.BracePairColor2 = mBracePairColor2;
         Settings.Default.BracePairColor3 = mBracePairColor3;
         Settings.Default.BracePairColor4 = mBracePairColor4;
         Settings.Default.BracePairColor5 = mBracePairColor5;
         Settings.Default.BracePairColor6 = mBracePairColor6;
         Settings.Default.BracePairColor7 = mBracePairColor7;
         Settings.Default.BracePairColor8 = mBracePairColor8;
         Settings.Default.BracePairColor9 = mBracePairColor9;
         Settings.Default.UseThreeSpaces = mUseThreeSpaces;
         Settings.Default.CommentOutBlankLines = mCommentOutBlankLines;
         Settings.Default.EnforceFormattingProtection = mEnforceFormattingProtection;
      }

      private static void LoadHistory(List<string> pList, string pFile, int pMax) {
         pList.Clear();
         if (!File.Exists(pFile))
            return;
         foreach (string line in File.ReadAllLines(pFile, Encoding.UTF8)) {
            if (!string.IsNullOrWhiteSpace(line) && pList.Count < pMax)
               pList.Add(line);
         }
      }

      private static void SaveHistory(List<string> pList, string pFile, int pMax) {
         int count = Math.Min(pList.Count, pMax);
         string[] lines = new string[count];
         for (int index = 0; index < count; index++)
            lines[index] = pList[index];
         File.WriteAllLines(pFile, lines, Encoding.UTF8);
      }
   }
}
