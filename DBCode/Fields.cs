using DBCode.Syntax;

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

   public enum EscapeFrom : int {
      ColorPicker,
      FontPicker,
      Main,
      Preferences,
      ThemeCreator,
      ThemeEditor,
      ThemePicker
   }

   public enum FontUsage : int {
      [DisplayText("Interface")]
      InterfaceFont,
      [DisplayText("Menu")]
      MenuFont,
      [DisplayText("Status Bar")]
      StatusBarFont,
      [DisplayText("Text Box")]
      TextBoxFont,
   }

   public enum ColorUsage : int {
      [DisplayText("main window Background")]
      BackgroundColor,
      [DisplayText("GroupBox Background")]
      GroupBoxBackgroundColor,
      [DisplayText("GroupBox Font")]
      GroupBoxFontColor,
      [DisplayText("Interface Background")]
      InterfaceBackgroundColor,
      [DisplayText("Interface Font")]
      InterfaceFontColor,
      [DisplayText("Menu Background")]
      MenuBackgroundColor,
      [DisplayText("Menu Font")]
      MenuFontColor,
      [DisplayText("Status Bar Background")]
      StatusBarBackgroundColor,
      [DisplayText("Status Bar Font")]
      StatusBarFontColor,
   }

   class Theme {
      public Color mMainWindowBackgroundColor { get; set; } = SystemColors.Window;
      public Color mGroupBoxBackgroundColor { get; set; } = SystemColors.Control;
      public Color mGroupBoxFontColor { get; set; } = SystemColors.ControlText;
      public Color mInterfaceBackgroundColor { get; set; } = SystemColors.Control;
      public Color mInterfaceFontColor { get; set; } = SystemColors.ControlText;
      public Color mMenuBackgroundColor { get; set; } = SystemColors.Control;
      public Color mMenuFontColor { get; set; } = SystemColors.ControlText;
      public Color mStatusBarBackgroundColor { get; set; } = SystemColors.Control;
      public Color mStatusBarFontColor { get; set; } = SystemColors.ControlText;
      public Font mTextBoxFont { get; set; } = SystemFonts.DefaultFont;
      public Font mInterfaceFont { get; set; } = SystemFonts.DefaultFont;
      public Font mMenuFont { get; set; } = SystemFonts.DefaultFont;
      public Font mStatusStripFont { get; set; } = SystemFonts.DefaultFont;
   }

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

   internal static class Fields {
      public static bool mForceActivation = true;
      public static bool mIsTargetingEnabled = false;
      public static bool mPreMinimalControlBox = true;
      public static bool mReturnToTop = true;
      public static bool mFirstGray = true;
      public static bool mIsHighlighting = false;
      public static bool mSuppressTextChanged = false;
      public static Button? mPreferencesCloseButton = null;
      public static EscapeFrom mEscapeFrom = EscapeFrom.Main;
      public static float mOFontSize, mScaling, mFontWidthAdjustment = 0.5f;
      public static FontUsage mFontUsage = FontUsage.TextBoxFont;
      public static HighlighterEngine? mHighlighterEngine = null;
      public static Icon[] mIcons = new Icon[4];
      public static int
                  //#pragma warning disable IDE0052
                  //#pragma warning disable IDE0051
                  //#pragma warning disable IDE0044
                  //#pragma warning disable CS0414
                  //efm5 these variables are rarely used but some modules need them – keeping them on hand
                  //sScalingTabControlVerticalPad = 0, sScalingTabControlHorizontalPad = 0,
                  //#pragma warning restore CS0414
                  //#pragma warning restore IDE0044
                  //#pragma warning restore IDE0051
                  //#pragma warning restore IDE0052
                  mComboBoxMaxDropdownHeight = 800, mComboBoxMaxDropdownWidth = 1000, mBookmarkRow = 0, mDoTabIndexBase = 80000, mDoIncrement = 1,
         mDoPanelHeight = 0, mFindPosition, mFindLength, mScalingGroupBoxTopLinePad = 0, mSelectionStart, mSelectionLength, mMenuLeftOffset = 30, mEM = 10,
         mIndent = 5, mCancelOffset = 15, mOkOffset = 15, mTitleBarHeight, mGroupTopPad = 2, mScalePad = 1, mTransitionSteps = 5, mTransitionInterval = 8,
         mMatchIndex = 0, mDefaultAskingMessage = 0, mGroupRightPad = 0, mGroupBottomPad = 0, mGroupLeftPad = 25, mMaximumGridWidth = 200, mVerticalScrollOffset = 20,
         mIconTabIndex = 90000, mIconVerticalPad = 10, mIconHorizontalPad = 10, mTabIndex = 200000, mIconRows = 0, mPartitionRows = 0, mHorizontalScrollOffset = 20,
         mOpenWithTableLayoutPanelRow = 0, mSlideshowDuration = 0, mWidestMenu = 0, mMakingShortcuts = 0,
         mWidgetHorizontalSpace = 3, mWidgetBigHorizontalSpace = 10, mWidgetVerticalOffset = 2, mWidgetBigVerticalOffset = 7,
         mAssociatedButtonPostCheckBoxVerticalOffset = 3,
         mAssociatedButtonPostLabelHorizontalSpace = 3,
         mAssociatedButtonPostLabelVerticalOffset = -4,
         mAssociatedButtonPostTextBoxVerticalOffset = 0,
         mAssociatedCheckBoxPostButtonHorizontalSpace = 0,
         mAssociatedCheckBoxPostButtonVerticalOffset = 4,
         mAssociatedComboBoxPostButtonHorizontalSpace = 0,
         mAssociatedComboBoxPostButtonVerticalOffset = 4,
         mAssociatedLabelPostButtonHorizontalSpace = 0,
         mAssociatedLabelPostButtonVerticalOffset = 5,
         mAssociatedLabelPostCheckBoxHorizontalSpace = 0,
         mAssociatedLabelPostCheckBoxVerticalOffset = 1,
         mAssociatedLabelPostPanelVerticalOffset = 3,
         mAssociatedLabelPostUpDownHorizontalSpace = 0,
         mAssociatedLabelPostUpDownVerticalOffset = 1,
         mAssociatedPostVerticalOffset = -1,
         mAssociatedSliderPostUpDownHorizontalSpace = 0,
         mAssociatedSliderPostUpDownVerticalOffset = 3,
         mAssociatedTextBoxPostButtonHorizontalSpace = 0,
         mAssociatedTextBoxPostButtonVerticalOffset = 2,
         mAssociatedTextBoxPostCheckBoxVerticalOffset = 0,
         mAssociatedTextBoxPostComboBoxHorizontalSpace = 1,
         mAssociatedTextBoxPostComboBoxVerticalOffset = 0,
         mAssociatedTextPostButtonVerticalOffset = 5,
         mAssociatedUpDownPostButtonHorizontalSpace = 0,
         mAssociatedUpDownPostButtonVerticalOffset = 4,
         mAssociatedUpDownPostCheckBoxHorizontalSpace = 0,
         mAssociatedUpDownPostCheckBoxVerticalOffset = -2,
#pragma warning disable IDE0052
#pragma warning disable IDE0051
#pragma warning disable IDE0044
#pragma warning disable CS0414
      //efm5 these variables are rarely used but some modules need them – keeping them on hand
         mAssociatedCheckBoxVerticalOffset = 4;
#pragma warning restore CS0414
#pragma warning restore IDE0044
#pragma warning restore IDE0051
#pragma warning restore IDE0052
      public static IntPtr mTargetWindow = IntPtr.Zero;
      public static readonly IntPtr mInsertAfterWindow = new IntPtr(0);
      public static MenuStrip? mMenuStrip = null;
      public static Panel? mPreferencesPanel = null;
      public static readonly PropertyInfo[] mPredefinedColors = typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static);
      public static Rectangle mPrePreferencesBounds = new Rectangle(50, 50, 800, 600);
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
      public static RichTextBox? mRichTextBox = null;
      public static Theme? mCurrentTheme = new Theme();
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
      public static ToolStripMenuItem? mPreferencesMenuItem = null;
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
      private static Panel? mPickFontPanel = null;
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
}
