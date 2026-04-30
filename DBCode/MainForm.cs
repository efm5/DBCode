namespace DBCode {
   public sealed partial class MainForm : Form {
      public MainForm() {
         mForm = this;
         InitializeUIPart1();
         MinimumSize = new Size(400, 200);
         Assembly assembly = Assembly.GetExecutingAssembly();
         FileVersionInfo? fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
         if (fileVersionInfo == null)
            mVersionString = "0.0.0.0";
         else {
            if (fileVersionInfo.FileVersion == null)
               mVersionString = "0.0.0.0";
            else {
               mVersionString = fileVersionInfo.FileVersion;
               if (mVersionString == null)
                  mVersionString = "0.0.0.0";
            }
         }
         mVersionLabel?.Text = "v: " + mVersionString;
         mCurrentViewMode = ViewMode.Features;
         mIsTargetingEnabled = false;
         mTargetWindowName = string.Empty;
         mPreMinimalText = Text;
         mPreMinimalControlBox = ControlBox;
      }

      private void MakeNews() {
         mMenuStrip = new MenuStrip();
         mTargetingMenuItem = new ToolStripMenuItem();
         mVisibilityMenuItem = new ToolStripMenuItem();
         mModeMenuItem = new ToolStripMenuItem();
         mLanguageMenuItem = new ToolStripMenuItem();
         mThemeMenuItem = new ToolStripMenuItem();
         mThemeDesignTSMI = new ToolStripMenuItem();
         mThemePickTSMI = new ToolStripMenuItem();
         mThemeEditTSMI = new ToolStripMenuItem();
         mHelpMenuItem = new ToolStripMenuItem();
         mTargetedTSMI = new ToolStripMenuItem();
         mRetargetTSMI = new ToolStripMenuItem();
         mTransparentTSMI = new ToolStripMenuItem();
         mThirtyTSMI = new ToolStripMenuItem();
         mFiftyTSMI = new ToolStripMenuItem();
         mSeventyFiveTSMI = new ToolStripMenuItem();
         mOpaqueTSMI = new ToolStripMenuItem();
         mMinimalTSMI = new ToolStripMenuItem();
         mFeaturesTSMI = new ToolStripMenuItem();
         mReturnToTopTSMI = new ToolStripMenuItem();
         mPlainTextTSMI = new ToolStripMenuItem();
         mCSharpTSMI = new ToolStripMenuItem();
         mCTSMI = new ToolStripMenuItem();
         mCppTSMI = new ToolStripMenuItem();
         mBasicTSMI = new ToolStripMenuItem();
         mFSharpTSMI = new ToolStripMenuItem();
         mHtmlTSMI = new ToolStripMenuItem();
         mCssTSMI = new ToolStripMenuItem();
         mXmlTSMI = new ToolStripMenuItem();
         mJsonTSMI = new ToolStripMenuItem();
         mPowerShellTSMI = new ToolStripMenuItem();
         mBatchTSMI = new ToolStripMenuItem();
         mSqlTSMI = new ToolStripMenuItem();
         mMarkdownTSMI = new ToolStripMenuItem();
         mPythonTSMI = new ToolStripMenuItem();
         mCurrentLanguageIsTSMI = new ToolStripMenuItem();
         mRichTextBox = new RichTextBox();
         mSendAllButton = new Button();
         mPasteSelectedButton = new Button();
         mTargetingLabel = new Label();
         mVersionLabel = new Label();
         mRevertButton = new Button();
         mMainBottomPanel = new BottomPanel(mCurrentTheme!, "E&xit");
      }

      private void InitializeUIPart1() {
         mUiState = new UiState();
         mFirstLaunch = Settings.Default.FirstLaunch;
         mThemeBounds = new Rectangle(Settings.Default.ThemeLocation, Settings.Default.ThemeSize);
         mUiState.ReadFromSettings();
         if (mFirstLaunch) {
            PerformFirstLaunchInitialization();
            Settings.Default.FirstLaunch = false;
         }
         ThemeRegistry.Initialize(); // must precede MakeNews so mCurrentTheme and mThemes are ready
         MakeNews();
         LoadEmbeddedIcons();
         InitializeUIPart2();
         InitializeUIPart3();
         SuspendLayout();
         //DEBUG efm5 2026 04 2 many of these need tab index – TabIndex = mTabIndex++;
         StartPosition = FormStartPosition.Manual;
         ClientSize = new Size(800, 800);
         MinimumSize = new Size(400, 300);
         Text = "DB Code";
         mMenuStrip!.Dock = DockStyle.Top;
         mMenuStrip.Name = "menuStrip";
         mMenuStrip.TabIndex = mTabIndex++;
         mTargetingMenuItem!.Name = "targetingMenuItem";
         mTargetingMenuItem.Text = "&Targeting";
         mVisibilityMenuItem!.Name = "visibilityMenuItem";
         mVisibilityMenuItem.Text = "&Visibility";
         mVisibilityMenuItem.ShortcutKeys = Keys.Control | Keys.G;
         mVisibilityMenuItem.ShowShortcutKeys = true;
         mModeMenuItem!.Name = "modeMenuItem";
         mModeMenuItem.Text = "&Mode";
         mLanguageMenuItem!.Name = "languageMenuItem";
         mLanguageMenuItem.Text = "&Language";
         mThemeMenuItem!.Name = "themeMenuItem";
         mThemeMenuItem.Text = "&Theme";
         mThemeDesignTSMI!.Name = "themeDesignTSMI";
         mThemeDesignTSMI.Text = "&Design";
         mThemeDesignTSMI.ShortcutKeys = Keys.Control | Keys.Shift | Keys.D;
         mThemeDesignTSMI.Click += ThemeDesign_Click;
         mThemeEditTSMI!.Name = "themeEditTSMI";
         mThemeEditTSMI.Text = "&Edit";
         mThemeEditTSMI.ShortcutKeys = Keys.Control | Keys.Shift | Keys.E;
         mThemeEditTSMI.Click += ThemeEdit_Click;
         mThemePickTSMI!.Name = "themePickTSMI";
         mThemePickTSMI.Text = "&Pick";
         mThemePickTSMI.ShortcutKeys = Keys.Control | Keys.Shift | Keys.P;
         mThemePickTSMI.Click += ThemePick_Click;
         mThemeMenuItem.DropDownItems.AddRange([mCurrentLanguageIsTSMI!, toolStripSeparator1, mThemePickTSMI, mThemeDesignTSMI, mThemeEditTSMI]);
         mHelpMenuItem!.Name = "helpMenuItem";
         mHelpMenuItem.Text = "&Help";
         mHelpMenuItem.Tag = new HelpTag(HelpContext.Main);
         mHelpMenuItem.ShortcutKeys = Keys.F1;
         mHelpMenuItem.Click += Help_Click;
         mTargetedTSMI!.Name = "targetedTSMI";
         mTargetedTSMI.Text = "&Targeted";
         mTargetedTSMI.CheckOnClick = true;
         mTargetedTSMI.ShortcutKeys = Keys.Control | Keys.D;
         mTargetedTSMI.Click += TargetedTSMI_Click;
         mRetargetTSMI!.Name = "retargetTSMI";
         mRetargetTSMI.Text = "&Retarget";
         mRetargetTSMI.ShortcutKeys = Keys.Control | Keys.E;
         mRetargetTSMI.Click += RetargetTSMI_Click;
         mTargetingMenuItem.DropDownItems.Add(mTargetedTSMI);
         mTargetingMenuItem.DropDownItems.Add(mRetargetTSMI);
         Load += MainForm_Load;
         Shown += MainForm_Shown;
         FormClosing += MainForm_FormClosing;
         mHighlighterEngine = new HighlighterEngine(mRichTextBox!, mCurrentLanguage);
         mRichTextBox!.TextChanged += OnEditorTextChanged;
         InitializeIcon();
         CheckLanguage();
         AdjustForThemeFont(mCurrentTheme!.mFonts[(int)FontUsage.Interface]);
         ApplyThemeToMainForm();
         UpdateTargetingStatusLabel();
         ResumeLayout(false);
         PerformLayout();
      }

      private void InitializeUIPart2() {
         mTransparentTSMI!.Name = "transparentTSMI";
         mTransparentTSMI.Text = "&Transparent";
         mTransparentTSMI.Tag = 0.0;
         mTransparentTSMI.Click += VisibilityTSMI_Click;
         mThirtyTSMI!.Name = "thirtyTSMI";
         mThirtyTSMI.Text = "&30%";
         mThirtyTSMI.Tag = 0.3;
         mThirtyTSMI.Click += VisibilityTSMI_Click;
         mFiftyTSMI!.Name = "fiftyTSMI";
         mFiftyTSMI.Text = "&50%";
         mFiftyTSMI.Tag = 0.5;
         mFiftyTSMI.Click += VisibilityTSMI_Click;
         mSeventyFiveTSMI!.Name = "seventyFiveTSMI";
         mSeventyFiveTSMI.Text = "&75%";
         mSeventyFiveTSMI.Tag = 0.75;
         mSeventyFiveTSMI.Click += VisibilityTSMI_Click;
         mOpaqueTSMI!.Name = "opaqueTSMI";
         mOpaqueTSMI.Text = "&Opaque";
         mOpaqueTSMI.Tag = 1.0;
         mOpaqueTSMI.Click += VisibilityTSMI_Click;
         mVisibilityMenuItem!.DropDownItems.Add(mTransparentTSMI);
         mVisibilityMenuItem.DropDownItems.Add(mThirtyTSMI);
         mVisibilityMenuItem.DropDownItems.Add(mFiftyTSMI);
         mVisibilityMenuItem.DropDownItems.Add(mSeventyFiveTSMI);
         mVisibilityMenuItem.DropDownItems.Add(mOpaqueTSMI);
         mMinimalTSMI!.Name = "minimalTSMI";
         mMinimalTSMI.Text = "&Minimal";
         mMinimalTSMI.Click += MinimalTSMI_Click;
         mFeaturesTSMI!.Name = "featuresTSMI";
         mFeaturesTSMI.Text = "&Features";
         mFeaturesTSMI.Checked = true;
         mFeaturesTSMI.Click += FeaturesTSMI_Click;
         mReturnToTopTSMI!.Name = "returnToTopTSMI";
         mReturnToTopTSMI.Text = "&Return To Top";
         mReturnToTopTSMI.CheckOnClick = true;
         mReturnToTopTSMI.ShortcutKeys = Keys.Control | Keys.H;
         mReturnToTopTSMI.Click += ReturnToTopTSMI_Click;
         mPlainTextTSMI!.Name = "plaintextTSMI";
         mPlainTextTSMI.Text = "Plain&text";
         mPlainTextTSMI.Tag = LanguageKind.PlainText;
         mPlainTextTSMI.Click += LanguageTSMI_Click;
         mCSharpTSMI!.Name = "cSharpTSMI";
         mCSharpTSMI.Text = "CSha&rp";
         mCSharpTSMI.Tag = LanguageKind.CSharp;
         mCSharpTSMI.Click += LanguageTSMI_Click;
         mCTSMI!.Name = "cTSMI";
         mCTSMI.Text = "&C";
         mCTSMI.Tag = LanguageKind.C;
         mCTSMI.Click += LanguageTSMI_Click;
         mCppTSMI!.Name = "cppTSMI";
         mCppTSMI.Text = "C&pp";
         mCppTSMI.Tag = LanguageKind.Cpp;
         mCppTSMI.Click += LanguageTSMI_Click;
         mBasicTSMI!.Name = "basicTSMI";
         mBasicTSMI.Text = "&Basic";
         mBasicTSMI.Tag = LanguageKind.Basic;
         mBasicTSMI.Click += LanguageTSMI_Click;
         mFSharpTSMI!.Name = "fSharpTSMI";
         mFSharpTSMI.Text = "&FSharp";
         mFSharpTSMI.Tag = LanguageKind.FSharp;
         mFSharpTSMI.Click += LanguageTSMI_Click;
         mHtmlTSMI!.Name = "hTMLTSMI";
         mHtmlTSMI.Text = "&HTML";
         mHtmlTSMI.Tag = LanguageKind.Html;
         mHtmlTSMI.Click += LanguageTSMI_Click;
         mCssTSMI!.Name = "cSSTSMI";
         mCssTSMI.Text = "C&SS";
         mCssTSMI.Tag = LanguageKind.Css;
         mCssTSMI.Click += LanguageTSMI_Click;
         mXmlTSMI!.Name = "xMLTSMI";
         mXmlTSMI.Text = "&XML";
         mXmlTSMI.Tag = LanguageKind.Xml;
         mXmlTSMI.Click += LanguageTSMI_Click;
         mJsonTSMI!.Name = "jSONTSMI";
         mJsonTSMI.Text = "&JSON";
         mJsonTSMI.Tag = LanguageKind.Json;
         mJsonTSMI.Click += LanguageTSMI_Click;
         mPowerShellTSMI!.Name = "powerShellTSMI";
         mPowerShellTSMI.Text = "Po&werShell";
         mPowerShellTSMI.Tag = LanguageKind.PowerShell;
         mPowerShellTSMI.Click += LanguageTSMI_Click;
         mBatchTSMI!.Name = "batchTSMI";
         mBatchTSMI.Text = "B&atch";
         mBatchTSMI.Tag = LanguageKind.Batch;
         mBatchTSMI.Click += LanguageTSMI_Click;
         mSqlTSMI!.Name = "sQLTSMI";
         mSqlTSMI.Text = "S&QL";
         mSqlTSMI.Tag = LanguageKind.Sql;
         mSqlTSMI.Click += LanguageTSMI_Click;
         mMarkdownTSMI!.Name = "markdownTSMI";
         mMarkdownTSMI.Text = "&Markdown";
         mMarkdownTSMI.Tag = LanguageKind.Markdown;
         mMarkdownTSMI.Click += LanguageTSMI_Click;
         mPythonTSMI!.Name = "pythonTSMI";
         mPythonTSMI.Text = "P&ython";
         mPythonTSMI.Tag = LanguageKind.Python;
         mPythonTSMI.Click += LanguageTSMI_Click;
         mCurrentLanguageIsTSMI!.Name = "currentLanguageIsTSMI";
         mCurrentLanguageIsTSMI.Text = mCurrently + mCurrentTheme!.mName;
         mModeMenuItem!.DropDownItems.AddRange([mMinimalTSMI, mFeaturesTSMI, mReturnToTopTSMI]);
         mLanguageMenuItem!.DropDownItems.AddRange([mPlainTextTSMI, mCSharpTSMI, mCTSMI, mCppTSMI, mBasicTSMI, mFSharpTSMI, mHtmlTSMI, mCssTSMI, mXmlTSMI, mJsonTSMI, mPowerShellTSMI, mBatchTSMI, mSqlTSMI, mMarkdownTSMI, mPythonTSMI]);
         mMenuStrip!.Items.AddRange([mTargetingMenuItem!, mVisibilityMenuItem, mModeMenuItem, mLanguageMenuItem, mThemeMenuItem!, mHelpMenuItem!]);
      }

      private void InitializeUIPart3() {
         mRichTextBox!.Multiline = true;
         mRichTextBox.ScrollBars = RichTextBoxScrollBars.Both;
         mRichTextBox.AcceptsTab = true;
         mRichTextBox.WordWrap = false;
         mRichTextBox.Anchor = mAnchorTopLeftBottomRight;
         mRichTextBox.Name = "mainTextBox";
         mRichTextBox.TabIndex = mTabIndex++;
         mSendAllButton!.Text = "&Send All";
         mSendAllButton.Name = "sendAllButton";
         mSendAllButton.TabIndex = mTabIndex++;
         mSendAllButton.AutoSize = true;
         mSendAllButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
         mSendAllButton.Tag = PasteMode.SendAll;
         mSendAllButton.Click += TransMove_Click;
         mPasteSelectedButton!.Text = "&Paste Selected";
         mPasteSelectedButton.Name = "pasteSelectedButton";
         mPasteSelectedButton.TabIndex = mTabIndex++;
         mPasteSelectedButton.AutoSize = true;
         mPasteSelectedButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
         mPasteSelectedButton.Tag = PasteMode.PasteSelected;
         mPasteSelectedButton.Click += TransMove_Click;
         mTargetingLabel!.Name = "targetingLabel";
         mTargetingLabel.Text = string.Empty;
         mTargetingLabel.ImageAlign = ContentAlignment.MiddleLeft;
         mTargetingLabel.TextAlign = ContentAlignment.MiddleRight;
         mTargetingLabel.TabIndex = mTabIndex++;
         mVersionLabel!.Name = "versionLabel";
         mVersionLabel.Text = "v: " + mVersionString;
         mVersionLabel.AutoSize = true;
         mVersionLabel.TextAlign = ContentAlignment.MiddleLeft;
         mVersionLabel.TabIndex = mTabIndex++;
         mRevertButton!.Text = "&Revert";
         mRevertButton.Name = "revertButton";
         mRevertButton.TabIndex = mTabIndex++;
         mRevertButton.AutoSize = true;
         mRevertButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
         mRevertButton.Tag = PasteMode.SendAll;
         mRevertButton.Click += RevertButton_Click;
         mMainBottomPanel!.AddLeftControl(mSendAllButton);
         mMainBottomPanel.AddLeftControl(mPasteSelectedButton);
         mMainBottomPanel.AddLeftControl(mTargetingLabel); // must be last — stretches to fill
         mMainBottomPanel.AddRightControl(mVersionLabel);  // added first = leftmost of right group
         mMainBottomPanel.AddRightControl(mRevertButton);
         mMainBottomPanel.mCancelButton!.Click += ExitButton_Click;
         Controls.AddRange([mRichTextBox!, mMainBottomPanel!, mMenuStrip!]);
      }
   }
}
