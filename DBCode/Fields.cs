using PCRE;

namespace DBCode {
   #region enumerations
   public enum LineEndings { Default, Insert, Append }

   public enum CodingTabPageUsage { CFamily, Basic, FSharp, HTML, CSS, XML, JSON, PowerShell, Batch, SQL, Markdown, Python }

   public enum PickMode { Use, Edit }

   public enum Whitespace { Both, Tabs, Spaces }

   public enum ColorTones { Ignore, Dark, MediumDark, MediumLight, Light }

   public enum ViewMode { Features, Minimal }

   public enum PasteMode { SendAll, PasteSelected, GetAll, GetSelected }

   public enum Icons { CurlyTargeted, CurlyUntargeted, StatusTargeted, StatusUntargeted }

   public enum HelpContext { Main, Options, ThemeEditor, ThemePicker, TargetPicker, ColorPicker, FontPicker }

   public enum LabelUsage : int { Interface, Title }

   public enum HeaderLabelSize : int {
      ExtraLarge = 200,
      Large = 150,
      Normal = 125,
      Small = 110,
      Tiny = 100
   }

   public enum PrimaryTabPageUsage : int { Interface, Color, Examples }

   public enum HighlightTabPageUsage : int {
      Interface, CSharp, C, Cpp, Basic, FSharp, HTML, CSS, XML, JSON, PowerShell, Batch, SQL, Markdown, Python
   }

   public enum TargetingTabPageUsage : int { Include, Exclude }

   public enum OptionsTabPageUsage : int { General, Targeting, Coding, BraceMatching, Shortcuts }

   public enum FontUsage : int {
      [DisplayText("Interface Font")]
      Interface,
      [DisplayText("Menu Font")]
      Menu,
      [DisplayText("Bottom Panel Font")]
      Status,
      [DisplayText("Text Box Font")]
      Text
   }

   public enum ColorSwatchUsage {
      [DisplayText("Panel Background Color")]
      PanelBackground,
      [DisplayText("GroupBox Background Color")]
      GroupBoxBackground,
      [DisplayText("GroupBox Font Color")]
      GroupBoxFont,
      [DisplayText("Interface Background Color")]
      InterfaceBackground,
      [DisplayText("Interface Font Color")]
      InterfaceFont,
      [DisplayText("Menu Background Color")]
      MenuBackground,
      [DisplayText("Menu Font Color")]
      MenuFont,
      [DisplayText("Bottom Panel Background Color")]
      BottomPanelBackground,
      [DisplayText("Bottom Panel Font Color")]
      BottomPanelFont,
      [DisplayText("Text Box Font Color")]
      TextBoxFont,
      [DisplayText("Text Box Color")]
      TextBox,
      [DisplayText("Tab Header Selected Background Color")]
      TabHeaderSelectedBackground,
      [DisplayText("Tab Header Unselected Background Color")]
      TabHeaderUnselectedBackground,
      [DisplayText("Tab Header Selected Font Color")]
      TabHeaderSelectedFont,
      [DisplayText("Tab Header Unselected Color")]
      TabHeaderUnselectedFont
   }

   public enum ColorPickerSwatchUsage {
      Gray,
      Red,
      Green,
      Blue,
      Demo
   }

   public enum ColorRole : int {
      [DisplayText("Unknown Color")]
      Unknown,
      [DisplayText("Whitespace Color")]
      Whitespace,
      [DisplayText("Identifier Color")]
      Identifier,
      [DisplayText("Keyword Color")]
      Keyword,
      [DisplayText("Number Color")]
      Number,
      [DisplayText("String Literal Color")]
      StringLiteral,
      [DisplayText("Character Literal Color")]
      CharLiteral,
      [DisplayText("Comment Color")]
      Comment,
      [DisplayText("Preprocessor Directive Color")]
      PreprocessorDirective,
      [DisplayText("Operator Color")]
      Operator,
      [DisplayText("Punctuation Color")]
      Punctuation
   }

   public enum HighlightUsage : int { Language, Token }

   public enum ThemeUsage : int {
      [DisplayText("DesignTheme")]
      Design,
      [DisplayText("EditTheme")]
      Edit,
      [DisplayText("PickTheme")]
      Pick
   }
   #endregion

   #region classes
   internal static class ClipboardHelper {
      public static void TrySetClipboardText(string pText) {
         if (string.IsNullOrEmpty(pText))
            return;
         const int maxAttempts = 3;
         int attempt = 0;

         while (attempt < maxAttempts) {
            try {
               Clipboard.SetText(pText);
               return;
            }
            catch {
               Thread.Sleep(50);
               attempt++;
            }
         }
      }

      public static string? TryGetClipboardText() {
         const int maxAttempts = 3;
         int attempt = 0;

         while (attempt < maxAttempts) {
            try {
               return Clipboard.GetText();
            }
            catch {
               Thread.Sleep(50);
               attempt++;
            }
         }
         return null;
      }
   }

   internal sealed class FindRecord {
      internal int mPosition;
      internal MatchCollection? mMatches;
      internal List<PcreMatch>? mPcreMatches;
      internal PcreRegex? mPcreRegex;
      internal string mSearchText;
      internal int Count => mPcreMatches != null ? mPcreMatches.Count : mMatches!.Count;
      internal int GetIndex(int pIndex) => mPcreMatches != null ? mPcreMatches[pIndex].Index : mMatches![pIndex].Index;
      internal int GetLength(int pIndex) => mPcreMatches != null ? mPcreMatches[pIndex].Length : mMatches![pIndex].Length;

#pragma warning disable IDE0290
      internal FindRecord(string pSearchText, MatchCollection pMatches) {
         mSearchText = pSearchText;
         mMatches = pMatches;
         mPosition = 0;
      }

      internal FindRecord(string pSearchText, List<PcreMatch> pPcreMatches, PcreRegex pPcreRegex) {
         mSearchText = pSearchText;
         mPcreMatches = pPcreMatches;
         mPcreRegex = pPcreRegex;
         mPosition = 0;
      }
#pragma warning restore IDE0290
   }

   internal sealed class HelpTag {
      public HelpContext Context;
      public string? Anchor;

#pragma warning disable IDE0290 // Use primary constructor
      public HelpTag(HelpContext pContext, string? pAnchor = "") {
         Context = pContext;
         Anchor = pAnchor;
      }
#pragma warning restore IDE0290
   }
   #endregion

   #region fields
   internal static class Fields {
      #region general
      public const AnchorStyles mAnchorBottomLeft = AnchorStyles.Bottom | AnchorStyles.Left,
         mAnchorBottomLeftRight = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
         mAnchorBottomRight = AnchorStyles.Bottom | AnchorStyles.Right,
         mAnchorTopLeft = AnchorStyles.Top | AnchorStyles.Left,
         mAnchorTopLeftBottomRight =
         AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
         mAnchorTopRight = AnchorStyles.Top | AnchorStyles.Right;
      public static bool mFirstGray = true, mForceActivation = true, mUseControlPasting,
         mIsTargetingEnabled = false, mPreMinimalControlBox = true, mUsePCRE = false;
      public static BottomPanel mMainBottomPanel = null!;
      public static Button mSendAllButton = null!, mPasteSelectedButton = null!, mRevertButton = null!,
         mGetAllButton = null!, mGetSelectedButton = null!;
      public static ColorPickerPanel? mColorPickerPanel = null;
      public static List<Control>? mBottomPanelExcluded = null;
      public static FindPanel? mFindPanel = null;
      public static FindRecord? mCurrentFindRecord = null;
      public static FindRecord? mCurrentSearchRecord = null;
      public static FontPickerPanel? mFontPickerPanel = null;
      public static FontUsage mFontUsage = FontUsage.Text;
      public static ConfirmationDialog? mConfirmationPanel = null;
      public static GetString? mGetStringPanel = null;
      public static GetInteger? mGetIntegerPanel = null;
      public static HighlighterEngine? mHighlighterEngine = null;
      public static Icon[] mIcons = new Icon[4];
      public static ILayoutable? mActiveLayoutable = null;
      public static Panel? mActiveScrollablePanel = null;
      public static DataGridView? mActiveScrollableDataGridView = null;
      public static IntPtr mTargetWindow = IntPtr.Zero, mThisWindow = IntPtr.Zero;
      public static readonly IntPtr mInsertAfterWindow = new IntPtr(0);
      public static Label mTargetingLabel = null!, mVersionLabel = null!;
      public static LanguageKind mCurrentLanguage = LanguageKind.CSharp;
      public static LineNumberPanel mLineNumberPanel = null!;
      public static MainForm mForm = null!;
      public static OptionsPanel? mOptionsPanel = null;
      public static readonly PropertyInfo[] mPredefinedColors =
         typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static);
      public static PlainRichTextBox mRichTextBox = null!;
      public static ScrollablePanel mScrollableMainPanel = null!;
      public static SearchReplacePanel? mSearchReplacePanel = null;
      public static TargetPickerPanel? mTargetPickerPanel = null;
      public static ThemePanel? mThemePanel = null;
      public static ThemePickerPanel? mThemePickerPanel = null;
      public static string mPreMinimalText = string.Empty, mTargetWindowName = "Under construction",
         mVersionString = "0.0.0.0", mUsingThemeName = string.Empty;
      public static readonly string mUnicodeSampleString = "Unicode test: ÀÑÇ ÿ ɱ ǵ ʰ ā̋ ȇ ō̱ ╭╯ 🜁",
         mAppFolder = AppDomain.CurrentDomain.BaseDirectory,
         mMyDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\",
         mDataFolder = mMyDocumentsFolder + @"DBCode_Data\", mHelpFolder = mAppFolder + @"Help\",
         mAllowedTargetWindows = mDataFolder + @"AllowedTargetWindows.txt",
         mDisallowedTargetWindows = mDataFolder + @"DisallowedTargetWindows.txt",
         mFindSearchHistoryFile = mDataFolder + @"FindSearchHistory.txt",
         mFindPcreSearchHistoryFile = mDataFolder + @"FindPcreSearchHistory.txt",
         mSrSearchHistoryFile = mDataFolder + @"SrSearchHistory.txt",
         mSrPcreSearchHistoryFile = mDataFolder + @"SrPcreSearchHistory.txt",
         mSrReplaceHistoryFile = mDataFolder + @"SrReplaceHistory.txt",
         mSrPcreReplaceHistoryFile = mDataFolder + @"SrPcreReplaceHistory.txt",
         mCurrently = "Currently:  ";
      public static readonly List<string> mDisallowed = [
         //efm5 these are case insensitive
         //Windows not to paste into
         "ApplicationFrameHost", "Auto Box", "Calculator", "DB Code", "dictation box", "dictationbox", "Dragon",
         "explorer", "Grid", "Hotkeys for Dragon", "hotkeysForDragon", "HxOutlook", "HyperNote Box", "HyperNoteBox",
         "iexplorer", "MediaMonkey", "Music.UI", "obkagent", "ScriptedSandbox64", "Search Correct", "SearchCorrect",
         "SP Quick Panel", "SPQuickPanel", "SP Search", "SPSearch", "svchost", "SystemSettings", "TextInputHost",
         "Windows Explorer", "XboxApp" ],
         mAllowed = [//IDEs and other apps that are appropriate to paste into
            //efm5 these are case insensitive; Uses Contains()
            //Appropriate target Windows to paste into
            "Arduino", "Claude",
            "Code", //Visual Studio Code
            "CurlyPad", "Eclipse", "Emacs", "IntelliJ", "NetBeans", "Particle", "PSPad", "Visual Studio",
            "devenv", //Visual Studio
            "vim", "Xcode"  ];
      public static List<TargetWindow> mTargets = [];
      public static List<Theme> mThemes = [];//efm5 This line and the next must be before any code that uses mThemes – Such as the next line
      public static Theme mCurrentTheme = ThemeBuiltIns.CreateDarkTheme(false);
      public static HelpContext mUIContext = HelpContext.Main;
      public static UiState mUiState = null!;
      public static ViewMode mCurrentViewMode = ViewMode.Features;
      public static Keys mScrollingEdgeTopKey = Keys.Control | Keys.Alt | Keys.F1,
         mScrollingEdgeBottomKey = Keys.Control | Keys.Alt | Keys.F2,
         mScrollingEdgeLeftKey = Keys.Control | Keys.Alt | Keys.F3,
         mScrollingEdgeRightKey = Keys.Control | Keys.Alt | Keys.F4,
         mScrollingScrollUpKey = Keys.Control | Keys.Alt | Keys.F5,
         mScrollingScrollDownKey = Keys.Control | Keys.Alt | Keys.F6,
         mScrollingScrollLeftKey = Keys.Control | Keys.Alt | Keys.F7,
         mScrollingScrollRightKey = Keys.Control | Keys.Alt | Keys.F8,
         mScrollingPageUpKey = Keys.Control | Keys.Alt | Keys.F9,
         mScrollingPageDownKey = Keys.Control | Keys.Alt | Keys.F10,
         mScrollingPageLeftKey = Keys.Control | Keys.Alt | Keys.F11,
         mScrollingPageRightKey = Keys.Control | Keys.Alt | Keys.F12;
      #endregion

      #region main & context menus
      public static MenuStrip mMenuStrip = null!;
      public static ContextMenuStrip mMainContextMenuStrip = null!, mGeneralContextMenuStrip = null!;
      public static readonly ToolStripSeparator toolStripSeparator1 = new ToolStripSeparator(),
         toolStripSeparator2 = new ToolStripSeparator(), toolStripSeparator3 = new ToolStripSeparator(),
         toolStripSeparator4 = new ToolStripSeparator(), toolStripSeparator5 = new ToolStripSeparator(),
         toolStripSeparator6 = new ToolStripSeparator(), toolStripSeparator7 = new ToolStripSeparator(),
         toolStripSeparator8 = new ToolStripSeparator(), toolStripSeparator9 = new ToolStripSeparator(),
         toolStripSeparator10 = new ToolStripSeparator(), toolStripSeparator11 = new ToolStripSeparator(),
         toolStripSeparator12 = new ToolStripSeparator(), toolStripSeparator13 = new ToolStripSeparator(),
         toolStripSeparator14 = new ToolStripSeparator(), toolStripSeparator15 = new ToolStripSeparator(),
         toolStripSeparator16 = new ToolStripSeparator(), toolStripSeparator17 = new ToolStripSeparator(),
         toolStripSeparator18 = new ToolStripSeparator(), toolStripSeparator19 = new ToolStripSeparator(),
         toolStripSeparator20 = new ToolStripSeparator(), toolStripSeparator21 = new ToolStripSeparator(),
         toolStripSeparator22 = new ToolStripSeparator(), toolStripSeparator23 = new ToolStripSeparator(),
         toolStripSeparator24 = new ToolStripSeparator(), toolStripSeparator25 = new ToolStripSeparator(),
         toolStripSeparator26 = new ToolStripSeparator(), toolStripSeparator27 = new ToolStripSeparator(),
         toolStripSeparator28 = new ToolStripSeparator(), toolStripSeparator29 = new ToolStripSeparator(),
         toolStripSeparator30 = new ToolStripSeparator(), toolStripSeparator31 = new ToolStripSeparator(),
         toolStripSeparator32 = new ToolStripSeparator();
      public static ToolStripMenuItem?
      #region top-level menu items
         mEditMenuItem = null, mTargetingMenuItem = null, mViewMenuItem = null, mModeMenuItem = null, mLanguageMenuItem = null,
         mCodingMenuItem = null, mOptionsMenuItem = null, mThemeMenuItem = null, mHelpMenuItem = null,
      #endregion
      #region have grandchildren – No shortcut keys
         mThemePickTSMI = null, mCodingCFamilyTSMI = null, mCodingBasicTSMI = null, mCodingFSharpTSMI = null, mCodingHTMLTSMI = null,
         mCodingCSSTSMI = null, mCodingXMLTSMI = null, mCodingJSONTSMI = null, mCodingPowerShellTSMI = null, mCodingBatchTSMI = null,
         mCodingSQLTSMI = null, mCodingMarkdownTSMI = null, mCodingPythonTSMI = null,
         mViewScrollingTSMI = null, mViewScrollingEdgeTSMI = null, mViewScrollingScrollTSMI = null, mViewScrollingPageTSMI = null,
         mContextScrollingEdgeTSMI = null, mContextScrollingScrollTSMI = null, mContextScrollingPageTSMI = null,
      #endregion
      #region Are children and need shortcut keys
         mEditUndoTSMI = null, mEditRedoTSMI = null, mEditCutTSMI = null, mEditDeleteTSMI = null,
         mEditPasteTSMI = null, mEditSelectAllTSMI = null, mEditSelectNoneTSMI = null,
         mEditTrimToBeginningTSMI = null, mEditTrimToEndTSMI = null, mEditCopyTSMI = null, mEditCopyAllTSMI = null,
         mEditCopyToBeginningTSMI = null, mEditCopyToEndTSMI = null, mEditFindTSMI = null, mEditFindNextTSMI = null,
         mEditFindPreviousTSMI = null, mEditReplaceTSMI = null, mEditGoToTSMI = null,
         mTargetingTargetedTSMI = null, mTargetingRetargetTSMI = null, mTargetingDefaultEndingsTSMI = null, mTargetingInsertCRTSMI = null,
         mTargetingAppendCRTSMI = null,
         mViewTransparentTSMI = null, mViewThirtyTSMI = null, mViewFiftyTSMI = null, mViewSeventyFiveTSMI = null, mViewOpaqueTSMI = null,
         mViewScrollingEdgeTopTSMI = null, mViewScrollingEdgeBottomTSMI = null, mViewScrollingEdgeLeftTSMI = null, mViewScrollingEdgeRightTSMI = null,
         mViewScrollingScrollUpTSMI = null, mViewScrollingScrollDownTSMI = null, mViewScrollingScrollLeftTSMI = null, mViewScrollingScrollRightTSMI = null,
         mViewScrollingPageUpTSMI = null, mViewScrollingPageDownTSMI = null, mViewScrollingPageLeftTSMI = null, mViewScrollingPageRightTSMI = null,
         mViewWordWrapTSMI = null, mViewLineNumbersTSMI = null,
         mModeFeaturesTSMI = null, mModeMinimalTSMI = null,
         mThemeCurrentThemeIsTSMI = null, mThemeDesignTSMI = null, mThemeEditTSMI = null, mThemePickCurrentTSMI = null, mThemePickEditTSMI = null,
         mLanguageCSharpTSMI = null, mLanguageCTSMI = null, mLanguageCppTSMI = null, mLanguageBasicTSMI = null,
         mLanguageFSharpTSMI = null, mLanguageHtmlTSMI = null, mLanguageCssTSMI = null, mLanguageXmlTSMI = null, mLanguageJsonTSMI = null,
         mLanguagePowerShellTSMI = null, mLanguageBatchTSMI = null, mLanguageSqlTSMI = null, mLanguageMarkdownTSMI = null,
         mLanguagePythonTSMI = null, mLanguagePlainTextTSMI = null,
         mCSharpCommentAddDoubleTSMI = null, mCSharpAddNotImplementedTSMI = null, mCSharpCommentAddTripleTSMI = null,
         mCFamilyCPlusPlusCommentTSMI = null, mCFamilyCSharpExpressionBodiedMethodTSMI = null, mCFamilyCSharpCommentOutTSMI = null,
         mCFamilyCSharpCommentRemoveTSMI = null, mCSharpReverseEqualityTSMI = null, mCFamilyCSharpWrapTSMI = null,
         mBasicConvertVBToCSharpCommentTSMI = null, mBasicAddSingleQuoteTSMI = null, mBasicConvertCSharpToVBCommentTSMI = null,
         mBasicReverseEqualityTSMI = null, mBasicRemoveLineContinuationTSMI = null, mBasicConvertBooleansTSMI = null,
         mBasicConvertNullNothingTSMI = null, mBasicConvertLogicalOperatorsTSMI = null,
         mFSharpBlockCommentTSMI = null, mFSharpAddCommentTSMI = null, mFSharpAddMutableTSMI = null, mFSharpAddIgnoreTSMI = null,
         mFSharpConvertNullNoneTSMI = null,
         mHTMLCommentTSMI = null, mHTMLBoldTSMI = null, mHTMLItalicTSMI = null, mHTMLUnderlineTSMI = null, mHTMLStrikethroughTSMI = null,
         mHTMLBigTSMI = null, mHTMLBigBigTSMI = null, mHTMLSmallTSMI = null, mHTMLSmallSmallTSMI = null, mHTMLMarkTSMI = null,
         mHTMLSuperscriptTSMI = null, mHTMLSubscriptTSMI = null, mHTMLCodeTSMI = null, mHTMLPreformattedTSMI = null, mHTMLColorizeTSMI = null,
         mCSSBlockCommentTSMI = null, mCSSCommentRemoveTSMI = null, mCSSToggleImportantTSMI = null, mCSSConvertColorFormatTSMI = null,
         mXMLCommentTSMI = null, mXMLRemoveCommentTSMI = null, mXMLEscapeEntitiesTSMI = null, mXMLWrapCDataTSMI = null,
         mXMLToggleSelfCloseTSMI = null,
         mJSONAddCommentTSMI = null, mJSONBlockCommentTSMI = null, mJSONRemoveCommentTSMI = null, mJSONToggleQuotesTSMI = null,
         mJSONEscapeStringTSMI = null, mJSONRemoveTrailingCommasTSMI = null,
         mPowerShellAddCommentTSMI = null, mPowerShellBlockCommentTSMI = null, mPowerShellRemoveCommentTSMI = null,
         mPowerShellToggleBooleanTSMI = null, mPowerShellToggleQuotesTSMI = null,
         mBatchAddCommentTSMI = null, mBatchAddColonCommentTSMI = null, mBatchRemoveCommentTSMI = null, mBatchToggleCommentStyleTSMI = null,
         mSQLAddCommentTSMI = null, mSQLBlockCommentTSMI = null, mSQLRemoveCommentTSMI = null, mSQLToggleNullCheckTSMI = null,
         mSQLToggleBooleanTSMI = null,
         mMarkdownCommentTSMI = null, mMarkdownBoldTSMI = null, mMarkdownItalicTSMI = null, mMarkdownBoldItalicTSMI = null,
         mMarkdownStrikethroughTSMI = null, mMarkdownCodeTSMI = null, mMarkdownCodeBlockTSMI = null,
         mPythonAddCommentTSMI = null, mPythonBlockCommentTSMI = null, mPythonRemoveCommentTSMI = null, mPythonToggleBooleanTSMI = null,
         mPythonConvertNullNoneTSMI = null,
         mContextScrollingEdgeTopTSMI = null, mContextScrollingEdgeBottomTSMI = null, mContextScrollingEdgeLeftTSMI = null,
         mContextScrollingEdgeRightTSMI = null, mContextScrollingScrollUpTSMI = null, mContextScrollingScrollDownTSMI = null,
         mContextScrollingScrollLeftTSMI = null, mContextScrollingScrollRightTSMI = null, mContextScrollingPageUpTSMI = null,
         mContextScrollingPageDownTSMI = null, mContextScrollingPageLeftTSMI = null, mContextScrollingPageRightTSMI = null,
         mContextUndoTSMI = null, mContextRedoTSMI = null, mContextCopyAllTSMI = null, mContextCopyTSMI = null, mContextCutTSMI = null,
         mContextDeleteTSMI = null, mContextPasteTSMI = null, mContextSelectAllTSMI = null, mContextSelectNoneTSMI = null,
         mContextFindTSMI = null, mContextFindNextTSMI = null, mContextFindPreviousTSMI = null, mContextReplaceTSMI = null,
         mContextGoToTSMI = null;
      #endregion
      #endregion
   }
   #endregion
}
