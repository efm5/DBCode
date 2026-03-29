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
      public static Button mPreferencesCloseButton;
      public static Icon[] mIcons = new Icon[4];
      public static IntPtr mTargetWindow = IntPtr.Zero;
      public static readonly IntPtr mInsertAfterWindow = new IntPtr(0);
      public static MenuStrip mMenuStrip;
      public static Panel mPreferencesPanel;
      public static Rectangle mPrePreferencesBounds;
      public static StatusStrip mStatusStrip;
      public static string mPreMinimalText = string.Empty;
      public static string mTargetWindowName = "Under construction";
      public static string mVersionString = string.Empty;
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
      public static RichTextBox mMainTextBox;
      public static ToolStripButton mExitTSB;
      public static ToolStripButton mRevertTSB;
      public static ToolStripButton mTransferTSB;
      public static ToolStripButton mTransportTSB;
      public static ToolStripMenuItem mReturnToTopTSMI;
      public static ToolStripMenuItem mFeaturesTSMI;
      public static ToolStripMenuItem mFiftyTSMI;
      public static ToolStripMenuItem mHelpMenuItem;
      public static ToolStripMenuItem mMinimalTSMI;
      public static ToolStripMenuItem mModeMenuItem;
      public static ToolStripMenuItem mOpaqueTSMI;
      public static ToolStripMenuItem mPreferencesMenuItem;
      public static ToolStripMenuItem mRetargetTSMI;
      public static ToolStripMenuItem mSeventyFiveTSMI;
      public static ToolStripMenuItem mTargetedTSMI;
      public static ToolStripMenuItem mTargetingMenuItem;
      public static ToolStripMenuItem mThirtyTSMI;
      public static ToolStripMenuItem mTransparentTSMI;
      public static ToolStripMenuItem mVisibilityMenuItem;
      public static ToolStripStatusLabel mTargetingStatusLabel;
      public static ToolStripStatusLabel mVersionStatusLabel;
      public static ViewMode mCurrentViewMode;
   }
}
