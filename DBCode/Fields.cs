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

   internal static class Fields {
      public static bool mForceActivation = true;
      public static bool mIsTargetingEnabled = false;
      public static bool mPreMinimalControlBox = true;
      public static bool mReturnToTop = true;
      public static Button? mPreferencesCloseButton = null;
      public static Icon[] mIcons = new Icon[4];
      public static IntPtr mTargetWindow = IntPtr.Zero;
      public static readonly IntPtr mInsertAfterWindow = new IntPtr(0);
      public static MenuStrip? mMenuStrip = null;
      public static Panel? mPreferencesPanel = null;
      public static Rectangle mPrePreferencesBounds = new Rectangle(50, 50, 800, 600);
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
      public static RichTextBox? mMainTextBox = null;
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
   }
}
