using DBCode.Syntax;

namespace DBCode {
   public sealed partial class MainForm : Form {
      public MainForm() {
         InitializeUI();
         MinimumSize = new Size(700, 200);

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
         mVersionStatusLabel?.Text = "v: " + mVersionString;
         mCurrentViewMode = ViewMode.Features;
         mIsTargetingEnabled = false;
         mTargetWindowName = string.Empty;
         mPreMinimalText = Text;
         mPreMinimalControlBox = ControlBox;

         UpdateTargetingStatusLabel();
      }

      private void InitializeUI() {
         mMenuStrip = new MenuStrip();
         mTargetingMenuItem = new ToolStripMenuItem();
         mVisibilityMenuItem = new ToolStripMenuItem();
         mModeMenuItem = new ToolStripMenuItem();
         mThemeMenuItem = new ToolStripMenuItem();
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
         mRichTextBox = new RichTextBox();
         mStatusStrip = new StatusStrip();
         mTransferTSB = new ToolStripButton();
         mTransportTSB = new ToolStripButton();
         mTargetingStatusLabel = new ToolStripStatusLabel();
         mVersionStatusLabel = new ToolStripStatusLabel();
         mRevertTSB = new ToolStripButton();
         mExitTSB = new ToolStripButton();
         mTimer = new System.Windows.Forms.Timer {
            Interval = 400
         };

         SuspendLayout();

         StartPosition = FormStartPosition.CenterScreen;
         ClientSize = new Size(800, 800);
         MinimumSize = new Size(400, 300);
         Text = "DB Code";

         mMenuStrip.Dock = DockStyle.Top;
         mMenuStrip.Name = "menuStrip";
         mMenuStrip.TabIndex = 0;

         mTargetingMenuItem.Name = "targetingMenuItem";
         mTargetingMenuItem.Text = "&Targeting";

         mVisibilityMenuItem.Name = "visibilityMenuItem";
         mVisibilityMenuItem.Text = "&Visibility";
         mVisibilityMenuItem.ShortcutKeys = Keys.Control | Keys.G;
         mVisibilityMenuItem.ShowShortcutKeys = true;

         mModeMenuItem.Name = "modeMenuItem";
         mModeMenuItem.Text = "&Mode";

         mThemeMenuItem.Name = "preferencesMenuItem";
         mThemeMenuItem.Text = "&Theme";
         mThemeMenuItem.ShortcutKeys = Keys.Control | Keys.B;

         mHelpMenuItem.Name = "helpMenuItem";
         mHelpMenuItem.Text = "&Help";
         mHelpMenuItem.ShortcutKeys = Keys.F1;
         mHelpMenuItem.Click += Help_Click;

         mTargetedTSMI.Name = "targetedTSMI";
         mTargetedTSMI.Text = "&Targeted";
         mTargetedTSMI.CheckOnClick = true;
         mTargetedTSMI.ShortcutKeys = Keys.Control | Keys.D;
         mTargetedTSMI.Click += TargetedTSMI_Click;

         mRetargetTSMI.Name = "retargetTSMI";
         mRetargetTSMI.Text = "&Retarget";
         mRetargetTSMI.ShortcutKeys = Keys.Control | Keys.E;
         mRetargetTSMI.Click += RetargetTSMI_Click;

         mTargetingMenuItem.DropDownItems.Add(mTargetedTSMI);
         mTargetingMenuItem.DropDownItems.Add(mRetargetTSMI);

         mTransparentTSMI.Name = "transparentTSMI";
         mTransparentTSMI.Text = "&Transparent";
         mTransparentTSMI.Tag = 0.0;
         mTransparentTSMI.Click += VisibilityTSMI_Click;

         mThirtyTSMI.Name = "thirtyTSMI";
         mThirtyTSMI.Text = "&30%";
         mThirtyTSMI.Tag = 0.3;
         mThirtyTSMI.Click += VisibilityTSMI_Click;

         mFiftyTSMI.Name = "fiftyTSMI";
         mFiftyTSMI.Text = "&50%";
         mFiftyTSMI.Tag = 0.5;
         mFiftyTSMI.Click += VisibilityTSMI_Click;

         mSeventyFiveTSMI.Name = "seventyFiveTSMI";
         mSeventyFiveTSMI.Text = "&75%";
         mSeventyFiveTSMI.Tag = 0.75;
         mSeventyFiveTSMI.Click += VisibilityTSMI_Click;

         mOpaqueTSMI.Name = "opaqueTSMI";
         mOpaqueTSMI.Text = "&Opaque";
         mOpaqueTSMI.Tag = 1.0;
         mOpaqueTSMI.Click += VisibilityTSMI_Click;

         mVisibilityMenuItem.DropDownItems.Add(mTransparentTSMI);
         mVisibilityMenuItem.DropDownItems.Add(mThirtyTSMI);
         mVisibilityMenuItem.DropDownItems.Add(mFiftyTSMI);
         mVisibilityMenuItem.DropDownItems.Add(mSeventyFiveTSMI);
         mVisibilityMenuItem.DropDownItems.Add(mOpaqueTSMI);

         mMinimalTSMI.Name = "minimalTSMI";
         mMinimalTSMI.Text = "&Minimal";
         mMinimalTSMI.Click += MinimalTSMI_Click;

         mFeaturesTSMI.Name = "featuresTSMI";
         mFeaturesTSMI.Text = "&Features";
         mFeaturesTSMI.Checked = true;
         mFeaturesTSMI.Click += FeaturesTSMI_Click;

         mReturnToTopTSMI.Name = "returnToTopTSMI";
         mReturnToTopTSMI.Text = "&Return To Top";
         mReturnToTopTSMI.CheckOnClick = true;
         mReturnToTopTSMI.ShortcutKeys = Keys.Control | Keys.H;
         mReturnToTopTSMI.Click += ReturnToTopTSMI_Click;

         mModeMenuItem.DropDownItems.Add(mMinimalTSMI);
         mModeMenuItem.DropDownItems.Add(mFeaturesTSMI);
         mModeMenuItem.DropDownItems.Add(mReturnToTopTSMI);

         mMenuStrip.Items.Add(mTargetingMenuItem);
         mMenuStrip.Items.Add(mVisibilityMenuItem);
         mMenuStrip.Items.Add(mModeMenuItem);
         mMenuStrip.Items.Add(mThemeMenuItem);
         mMenuStrip.Items.Add(mHelpMenuItem);

         mRichTextBox.Multiline = true;
         mRichTextBox.ScrollBars = RichTextBoxScrollBars.Both;
         mRichTextBox.AcceptsTab = true;
         mRichTextBox.WordWrap = false;
         mRichTextBox.Dock = DockStyle.Fill;
         mRichTextBox.Name = "mainTextBox";
         mRichTextBox.TabIndex = 1;

         mStatusStrip.Dock = DockStyle.Bottom;
         mStatusStrip.SizingGrip = true;
         mStatusStrip.Name = "statusStrip";
         mStatusStrip.TabIndex = 2;

         mTransferTSB.DisplayStyle = ToolStripItemDisplayStyle.Text;
         mTransferTSB.Name = "transferTSB";
         mTransferTSB.Text = "&Transfer";
         mTransferTSB.Tag = PasteMode.Transfer;
         mTransferTSB.Click += TransMove_Click;

         mTransportTSB.DisplayStyle = ToolStripItemDisplayStyle.Text;
         mTransportTSB.Name = "transportTSB";
         mTransportTSB.Text = "Trans&port";
         mTransportTSB.Tag = PasteMode.Transport;
         mTransportTSB.Click += TransMove_Click;

         mTargetingStatusLabel.Name = "targetingStatusLabel";
         mTargetingStatusLabel.Text = "Untargeted";
         mTargetingStatusLabel.Spring = false;

         mVersionStatusLabel.Name = "versionStatusLabel";
         mVersionStatusLabel.Text = "v 0.0.0.0";
         mVersionStatusLabel.Spring = true;
         mVersionStatusLabel.TextAlign = ContentAlignment.MiddleRight;

         mRevertTSB.DisplayStyle = ToolStripItemDisplayStyle.Text;
         mRevertTSB.Name = "revertTSB";
         mRevertTSB.Text = "&Revert";
         mRevertTSB.Alignment = ToolStripItemAlignment.Right;
         mRevertTSB.Click += RevertTSB_Click;

         mExitTSB.DisplayStyle = ToolStripItemDisplayStyle.Text;
         mExitTSB.Name = "exitTSB";
         mExitTSB.Text = "E&xit";
         mExitTSB.Alignment = ToolStripItemAlignment.Right;
         mExitTSB.Click += ExitTSB_Click;

         mStatusStrip.Items.Add(mTransferTSB);
         mStatusStrip.Items.Add(mTransportTSB);
         mStatusStrip.Items.Add(mTargetingStatusLabel);
         mStatusStrip.Items.Add(mVersionStatusLabel);
         mStatusStrip.Items.Add(mRevertTSB);
         mStatusStrip.Items.Add(mExitTSB);

         Controls.Add(mRichTextBox);
         Controls.Add(mStatusStrip);
         Controls.Add(mMenuStrip);

         Load += MainForm_Load;
         FormClosing += MainForm_FormClosing;
         mHighlighterEngine = new HighlighterEngine(mRichTextBox, LanguageKind.CSharp);
         mRichTextBox.TextChanged += OnEditorTextChanged;
         LoadEmbeddedIcons();
         InitializeIcon();

         ResumeLayout(false);
         PerformLayout();
      }
   }
}
