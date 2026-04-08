using DBCode.Syntax;
using DBCode.Themes;

#pragma warning disable CS0649//DEBUG efm5 2026 03 31 just until we start using the layout
#pragma warning disable CS0414//DEBUG efm5 2026 03 31 just until we start using the layout
namespace DBCode {
   public enum ViewMode {
      Features,
      Minimal
   }

   public enum PasteMode {
      Transfer,
      Transport
   }

   public enum Icons {
      CurlyTargeted,
      CurlyUntargeted,
      StatusTargeted,
      StatusUntargeted
   }

   public enum UIContext { Main, Theme, ColorPicker, FontPicker }

   public enum LabelUsage : int { Interface, Title }

   public enum PrimaryTabPageUsage : int { Interface, Color }

   public enum HighlightTabPageUsage : int { Interface, CSharp1, CSharp2 }

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
      TabHeaderUnselectedFont
   }

   public enum ThemeUsage : int { Design, Edit, Pick }

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
      public UIContext Context;
      public string? Anchor;

#pragma warning disable IDE0290 // Use primary constructor
      public HelpTag(UIContext pContext, string? pAnchor = "") {
         Context = pContext;
         Anchor = pAnchor;
      }
#pragma warning restore IDE0290
   }

   internal static class Fields {
      public const AnchorStyles mTopLeftAnchor = AnchorStyles.Top | AnchorStyles.Left,
        mTopRightAnchor = AnchorStyles.Top | AnchorStyles.Right,
        mBottomLeftAnchor = AnchorStyles.Bottom | AnchorStyles.Left,
        mBottomRightAnchor = AnchorStyles.Bottom | AnchorStyles.Right,
        mTopLeftBottomRightAnchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
      public static bool mForceActivation = true;
      public static bool mIsTargetingEnabled = false;
      public static bool mPreMinimalControlBox = true;
      public static bool mReturnToTop = true;
      public static bool mFirstGray = true;
      public static bool mIsHighlighting = false;
      public static bool mSuppressTextChanged = false;
      public static UIContext mUIContext = UIContext.Main;
      public static float mOFontSize, mScaling, mFontWidthAdjustment = 0.5f;
      public static FontUsage mFontUsage = FontUsage.Text;
      public static HighlighterEngine? mHighlighterEngine = null;
      public static Icon[] mIcons = new Icon[4];
      public static int mThemePrimaryTabPageIndex = 0, mThemeHighlightTabPageIndex = 0;
      public static IntPtr mTargetWindow = IntPtr.Zero;
      public static readonly IntPtr mInsertAfterWindow = new IntPtr(0);
      public static MenuStrip? mMenuStrip = null;
      public static ThemePanel? mThemePanel = null;
      public static readonly PropertyInfo[] mPredefinedColors = typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static);
      public static Rectangle mPreThemeBounds = new Rectangle(50, 50, 800, 600);
      public static Size mResolution, mMonitorSize;
      public static StatusStrip? mStatusStrip = null;
      public static string mPreMinimalText = string.Empty;
      public static string mTargetWindowName = "Under construction";
      public static string mVersionString = "0.0.0.0";
      public static readonly List<string>
         mIDEs = [//efm5 these are case insensitive
                  //Appropriate target Windows to paste into
            "Visual Studio", "Arduino", "Particle", "IntelliJ",
               "Eclipse", "NetBeans", "Xcode", "PSPad", "vim", "Emacs" ],
         mBlackList = [//efm5 these are case insensitive
                       //Windows not to paste into
            "ApplicationFrameHost", "Auto Box", "Calculator", "dictation box", "dictationbox",
               "Dragon", "explorer", "Grid", "Hotkeys for Dragon", "hotkeysForDragon", "HxOutlook",
               "HyperNote Box", "HyperNoteBox", "iexplorer", "MediaMonkey", "Music.UI", "obkagent",
               "ScriptedSandbox64", "Search Correct", "SearchCorrect", "SP Quick Panel",
               "SPQuickPanel", "SP Search", "SPSearch", "svchost",  "SystemSettings",
               "TextInputHost",  "Windows Explorer", "XboxApp" ];
      public static readonly string mUnicodeSampleString = "Unicode test: ÀÑÇ ÿ ɱ ǵ ʰ ā̋ ȇ ō̱ ╭╯ 🜁";
      public static RichTextBox? mRichTextBox = null;
      public static Theme? mCurrentTheme = new Theme(string.Empty);
      public static System.Windows.Forms.Timer? mTimer;
      public static ToolStripButton? mExitTSB = null;
      public static ToolStripButton? mRevertTSB = null;
      public static ToolStripButton? mTransferTSB = null;
      public static ToolStripButton? mTransportTSB = null;
      public static ToolStripMenuItem? mReturnToTopTSMI = null;
      public static ToolStripMenuItem? mFeaturesTSMI = null;
      public static ToolStripMenuItem? mFiftyTSMI = null;
      public static ToolStripMenuItem? mHelpMenuItem = null;
      public static ToolStripMenuItem? mMinimalTSMI = null;
      public static ToolStripMenuItem? mModeMenuItem = null;
      public static ToolStripMenuItem? mOpaqueTSMI = null;
      public static ToolStripMenuItem? mThemeMenuItem = null;
      public static ToolStripMenuItem? mThemeDesignTSMI = null;
      public static ToolStripMenuItem? mThemePickTSMI = null;
      public static ToolStripMenuItem? mThemeEditTSMI = null;
      public static ToolStripMenuItem? mRetargetTSMI = null;
      public static ToolStripMenuItem? mSeventyFiveTSMI = null;
      public static ToolStripMenuItem? mTargetedTSMI = null;
      public static ToolStripMenuItem? mTargetingMenuItem = null;
      public static ToolStripMenuItem? mThirtyTSMI = null;
      public static ToolStripMenuItem? mTransparentTSMI = null;
      public static ToolStripMenuItem? mVisibilityMenuItem = null;
      public static ToolStripStatusLabel? mTargetingStatusLabel = null;
      public static ToolStripStatusLabel? mVersionStatusLabel = null;
      public static ViewMode mCurrentViewMode = ViewMode.Features;

      //Picker fields
      //DEBUG efm5 2026 03 30 Some of These need to be initialized
      public static Label? mFontDescriptionLabel = null;
      public static Panel? mPickFontPanel = null;
      public static Panel? mFontPickerBottomPanel = null;
      public static Button? mBluePrefixButton;
      public static Button? mColorPickerCancelButton;
      public static Button? mColorPickerHelpButton;
      public static Button? mColorPickerOkButton;
      public static Button? mFontFamilyDropDownPrefixButton;
      public static Button? mFontFamilyTextBoxPrefixButton;
      public static Button? mFontPickerCancelButton;
      public static Button? mFontPickerHelpButton;
      public static Button? mFontPickerOkButton;
      public static Button? mFontSizeDropDownPrefixButton;
      public static Button? mFontSizePrefixButton;
      public static Button? mGrayPrefixButton;
      public static Button? mGreenPrefixButton;
      public static Button? mNamedColorPrefixButton;
      public static Button? mRedPrefixButton;
      public static Button? mRefreshButton;
      public static CheckBox? mBoldStyleCheckBox;
      public static CheckBox? mItalicsStyleCheckBox;
      public static CheckBox? mNormalStyleCheckBox;
      public static CheckBox? mStrikethroughStyleCheckBox;
      public static CheckBox? mUnderlineStyleCheckBox;
      public static CheckBox? mUseGrayscaleCheckBox;
      public static CheckBox? mUseNamedCheckBox;
      public static ComboBox? mFontFamilyComboBox;
      public static ComboBox? mFontSizeComboBox;
      public static ComboBox? mNamedColorsComboBox;
      public static Form? mForm = null;
      public static GroupBox? mFontStyleGroupBox;
      public static Label? mPickColorTitleLabel;
      public static Label? mPickColorUsageLabel;
      public static Label? mPickFontTitleLabel;
      public static Label? mPickFontUsageLabel;
      public static NumericUpDown? mBlueUpDown;
      public static NumericUpDown? mGrayUpDown;
      public static NumericUpDown? mGreenUpDown;
      public static NumericUpDown? mRedUpDown;
      public static Panel? mColorPickerBottomPanel;
      public static Panel? mColorPickerExampleInnerPanel;
      public static Panel? mColorPickerExampleMiddlePanel;
      public static Panel? mColorPickerExampleOuterPanel;
      public static Panel? mColorPickerNamedColorPanel;
      public static Panel? mColorPickerSliderPanel;
      public static Panel? mGrayscaleInnerExamplePanel;
      public static Panel? mGrayscaleMiddleExamplePanel;
      public static Panel? mGrayscaleOuterExamplePanel;
      public static Panel? mPickColorPanel;
      public static TextBox? mFontFamilyNameTextBox;
      public static TextBox? mFontSizeTextBox;
      public static TrackBar? mBlueSlider;
      public static TrackBar? mGraySlider;
      public static TrackBar? mGreenSlider;
      public static TrackBar? mRedSlider;
      //DEBUG efm5 2026 03 30 End of MAYBE not Initialized
   }
#pragma warning restore CS0649//DEBUG efm5 2026 03 31 just until we start using the layout
#pragma warning restore CS0414//DEBUG efm5 2026 03 31 just until we start using the layout
}
