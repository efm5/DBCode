using DBCode.Syntax;
using DBCode.Themes;

#pragma warning disable CS0649//DEBUG efm5 2026 03 31 just until we start using the layout
#pragma warning disable CS0414//DEBUG efm5 2026 03 31 just until we start using the layout
namespace DBCode {
   #region enumerations
   public enum ViewMode { Features, Minimal }

   public enum PasteMode { Transfer, Transport }

   public enum Icons { CurlyTargeted, CurlyUntargeted, StatusTargeted, StatusUntargeted }

   public enum HelpContext { Main, Theme, ColorPicker, FontPicker }

   public enum LabelUsage : int { Interface, Title }

   public enum HeaderLabelSize : int {
      ExtraLarge = 200,
      Large = 150,
      Normal = 125,
      Small = 110,
      Tiny = 100
   }

   public enum PrimaryTabPageUsage : int { Interface, Color }

   public enum HighlightTabPageUsage : int {
      Interface, CSharp, C, Cpp, Basic, FSharp, HTML, CSS, XML, JSON, PowerShell, Batch, SQL, Markdown, Python
   }

   public enum FontUsage : int {
      [DisplayText("Interface Font")]
      Interface,
      [DisplayText("Menu Font")]
      Menu,
      [DisplayText("Status Strip Font")]
      Status,
      [DisplayText("Text Box Font")]
      Text
   }

   public enum ColorUsage : int {
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
      [DisplayText("Status Bar Background Color")]
      StatusBackground,
      [DisplayText("Status Bar Font Color")]
      StatusFont,
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
      TabHeaderUnselectedFont,
      // Syntax Tokens
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

   internal enum ColorRole : int {
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
      public const AnchorStyles mBottomLeftAnchor = AnchorStyles.Bottom | AnchorStyles.Left,
         mBottomRightAnchor = AnchorStyles.Bottom | AnchorStyles.Right,
         mTopLeftAnchor = AnchorStyles.Top | AnchorStyles.Left,
         mTopLeftBottomRightAnchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
         mTopRightAnchor = AnchorStyles.Top | AnchorStyles.Right;
      public static bool mFirstGray = true, mFirstLaunch = true, mForceActivation = true, mIsTargetingEnabled = false,
         mPreMinimalControlBox = true, mReturnToTop = true, mFirstTheme = true, mFirstThemePicker = true,
         mFirstColorPicker = true;
      public static float mFontWidthAdjustment = 0.5f, mOFontSize, mScaling;
      public static FontUsage mFontUsage = FontUsage.Text;
      public static Form? mForm = null;
      public static HighlighterEngine? mHighlighterEngine = null;
      public static Icon[] mIcons = new Icon[4];
      public static int mThemeHighlightTabPageIndex = 0, mThemePrimaryTabPageIndex = 0;
      public static IntPtr mTargetWindow = IntPtr.Zero;
      public static readonly IntPtr mInsertAfterWindow = new IntPtr(0);
      public static LanguageKind mCurrentLanguage = LanguageKind.CSharp;
      public static MenuStrip? mMenuStrip = null;
      public static ThemePanel? mThemePanel = null;
      public static ColorPickerPanel? mColorPickerPanel = null;
      public static ThemePickerPanel? mThemePickerPanel = null;
      public static readonly PropertyInfo[] mPredefinedColors =
         typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static);
      public static Rectangle mPreThemeBounds = new Rectangle(50, 50, 800, 600), mThemeBounds,
         mPreThemePickerBounds = new Rectangle(50, 50, 800, 600), mThemePickerBounds,
         mPrePickerBounds = new Rectangle(50, 50, 800, 600), mColorPickerBounds;
      public static RichTextBox? mRichTextBox = null;
      public static Size mMonitorSize, mResolution;
      public static StatusStrip? mStatusStrip = null;
      public static string mPreMinimalText = string.Empty, mTargetWindowName = "Under construction",
         mVersionString = "0.0.0.0", mPreviousThemeName = string.Empty;
      public static readonly string mAppFolder = AppDomain.CurrentDomain.BaseDirectory,
        mMyDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\",
        mDataFolder = mMyDocumentsFolder + @"DBCode_Data\", mHelpFolder = mAppFolder + @"Help\", mCurrently = "Currently:  ";
      public static readonly List<string> mBlackList = [
         //efm5 these are case insensitive
         //Windows not to paste into
         "ApplicationFrameHost", "Auto Box", "Calculator", "DB Code", "dictation box", "dictationbox", "Dragon",
         "explorer", "Grid", "Hotkeys for Dragon", "hotkeysForDragon", "HxOutlook", "HyperNote Box", "HyperNoteBox",
         "iexplorer", "MediaMonkey", "Music.UI", "obkagent", "ScriptedSandbox64", "Search Correct", "SearchCorrect",
         "SP Quick Panel", "SPQuickPanel", "SP Search", "SPSearch", "svchost", "SystemSettings", "TextInputHost",
         "Windows Explorer", "XboxApp" ],
         mIDEs = [
            //efm5 these are case insensitive; Uses Contains() – “Visual Studio” matches “Visual Studio Code”
            //Appropriate target Windows to paste into
            "Arduino", "Eclipse", "Emacs", "IntelliJ", "NetBeans", "Particle", "PSPad", "Visual Studio", "vim", "Xcode"  ];
      public static readonly string mUnicodeSampleString = "Unicode test: ÀÑÇ ÿ ɱ ǵ ʰ ā̋ ȇ ō̱ ╭╯ 🜁";
      public static List<Theme> mThemes = new List<Theme>();//efm5 This line and the next must be before any code that uses mThemes – Such as the next line
      public static Theme? mCurrentTheme = ThemeBuiltIns.CreateDarkTheme(false);
      public static readonly ToolStripSeparator toolStripSeparator1 = new ToolStripSeparator();
      public static ToolStripStatusLabel? mTargetingStatusLabel = null, mVersionStatusLabel = null;
      public static HelpContext mUIContext = HelpContext.Main;
      public static UiState mUiState = null!;
      public static ViewMode mCurrentViewMode = ViewMode.Features;
      #endregion

      #region main menu
      public static ToolStripButton? mExitTSB = null, mRevertTSB = null, mTransferTSB = null, mTransportTSB = null;
      public static ToolStripMenuItem? mFeaturesTSMI = null, mFiftyTSMI = null, mHelpMenuItem = null, mMinimalTSMI = null,
         mModeMenuItem = null, mLanguageMenuItem = null, mOpaqueTSMI = null, mRetargetTSMI = null, mReturnToTopTSMI = null, mSeventyFiveTSMI = null,
         mTargetedTSMI = null, mTargetingMenuItem = null, mThemeDesignTSMI = null, mThemeEditTSMI = null,
         mThemeMenuItem = null, mThemePickTSMI = null, mThirtyTSMI = null, mTransparentTSMI = null,
         mVisibilityMenuItem = null, mPlainTextTSMI = null, mCSharpTSMI = null, mCTSMI = null, mCppTSMI = null, mBasicTSMI = null, mFSharpTSMI = null,
         mHtmlTSMI = null, mCssTSMI = null, mXmlTSMI = null, mJsonTSMI = null, mPowerShellTSMI = null, mBatchTSMI = null, mSqlTSMI = null,
         mMarkdownTSMI = null, mPythonTSMI = null, mCurrentLanguageIsTSMI = null;
      #endregion
   }
   #endregion
#pragma warning restore CS0649//DEBUG efm5 2026 03 31 just until we start using the layout
#pragma warning restore CS0414//DEBUG efm5 2026 03 31 just until we start using the layout
}
