namespace DBCode {
   public sealed partial class MainForm : Form {
      public void SuspendClientSizeChanged() {
         ClientSizeChanged -= OnClientSizeChanged;
      }

      public void ResumeClientSizeChanged() {
         ClientSizeChanged += OnClientSizeChanged;
      }

      public static void DisposeFontIfOwned(Font? pFont) {
         if (pFont != null && !pFont.IsSystemFont)
            pFont.Dispose();
      }

      public static void CheckLanguage() {
         foreach (ToolStripMenuItem tsmi in mLanguageMenuItem!.DropDownItems.OfType<ToolStripMenuItem>())
            tsmi.Checked = false;
         switch (mCurrentLanguage) {
            case LanguageKind.CSharp:
               mCSharpTSMI!.Checked = true;
               break;
            case LanguageKind.C:
               mCTSMI!.Checked = true;
               break;
            case LanguageKind.Cpp:
               mCppTSMI!.Checked = true;
               break;
            case LanguageKind.Basic:
               mBasicTSMI!.Checked = true;
               break;
            case LanguageKind.FSharp:
               mFSharpTSMI!.Checked = true;
               break;
            case LanguageKind.Html:
               mHtmlTSMI!.Checked = true;
               break;
            case LanguageKind.Css:
               mCssTSMI!.Checked = true;
               break;
            case LanguageKind.Xml:
               mXmlTSMI!.Checked = true;
               break;
            case LanguageKind.Json:
               mJsonTSMI!.Checked = true;
               break;
            case LanguageKind.PowerShell:
               mPowerShellTSMI!.Checked = true;
               break;
            case LanguageKind.Batch:
               mBatchTSMI!.Checked = true;
               break;
            case LanguageKind.Sql:
               mSqlTSMI!.Checked = true;
               break;
            case LanguageKind.Markdown:
               mMarkdownTSMI!.Checked = true;
               break;
            case LanguageKind.Python:
               mPythonTSMI!.Checked = true;
               break;
            case LanguageKind.PlainText:
               mPlainTextTSMI!.Checked = true;
               break;
         }
      }

      public void RehighlightText() {
         //DEBUG efm5 2026 04 20 do the work
         TimedMessage("Rehighlighting text with the new language selection", "Rehighlighting Text", 2000);
         SuspendLayout();
         switch (mCurrentLanguage) {
            case LanguageKind.PlainText:
               break;
            default:
               break;
         }
         ResumeLayout(true);
      }

      public void LayoutControls() {
         ThrowIfNull(mVersionLabel, nameof(mVersionLabel));
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         ApplyTheme();
         RehighlightText();
         mMainBottomPanel.LayoutControls();
      }

      internal void ApplyTheme() {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         ThrowIfNull(mCurrentLanguageIsTSMI, nameof(mCurrentLanguageIsTSMI));
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         ThrowIfNull(mMenuStrip, nameof(mMenuStrip));
         ThrowIfNull(mHighlighterEngine, nameof(mHighlighterEngine));
         mRichTextBox.TextChanged -= OnEditorTextChanged;
         Theme theme = mCurrentTheme!;
         mCurrentLanguageIsTSMI.Text = mCurrently + theme.mName;
         mForm!.BackColor = theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground];
         mRichTextBox.BackColor = theme.mInterfaceColors[(int)ColorUsage.TextBox];
         mRichTextBox.ForeColor = theme.mInterfaceColors[(int)ColorUsage.TextBoxFont];
         mMenuStrip.BackColor = theme.mInterfaceColors[(int)ColorUsage.MenuBackground];
         foreach (ToolStripMenuItem toolStripMenuItem in mMenuStrip.Items.OfType<ToolStripMenuItem>()) {
            PaintMenuItem(toolStripMenuItem, theme);
            foreach (ToolStripMenuItem subItem in toolStripMenuItem.DropDownItems.OfType<ToolStripMenuItem>())
               PaintMenuItemsRecursive(subItem, theme);
         }
         mMainBottomPanel.BackColor = theme.mInterfaceColors[(int)ColorUsage.StatusBackground];
         mMainBottomPanel.SetFontAndColor();
         LayoutMainBottomPanel();
         mMainBottomPanel.Invalidate(true);
         mHighlighterEngine.HighlightNow();
         mRichTextBox.TextChanged += OnEditorTextChanged;
         ResumeLayout(true);
      }

      public static void PerformFirstLaunchInitialization() {
         TimedMessage("Welcome to DBCode! This message will disappear after a few seconds.", "Welcome to DBCode!", 3000);
         //DEBUG efm5 2026 04 8 fill out the logic
      }

      public static void GetHelp(HelpContext pUIContext, string? pSpecificHREFAnchor = "") {
         ThrowIfNull(pSpecificHREFAnchor, nameof(pSpecificHREFAnchor));
         try {
            string? fullyQualifiedPath = Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty;

            fullyQualifiedPath = Path.Combine(fullyQualifiedPath, "Help") ?? string.Empty;
            switch (pUIContext) {
               case HelpContext.Main:
                  fullyQualifiedPath = Path.Combine(fullyQualifiedPath, "DBCodeHelp.html");
                  break;
               case HelpContext.Theme:
                  fullyQualifiedPath = Path.Combine(fullyQualifiedPath, "DBCodeThemeHelp.html");
                  break;
               case HelpContext.FontPicker:
                  fullyQualifiedPath = Path.Combine(fullyQualifiedPath, "DBCodeFontPickerHelp.html");
                  break;
               case HelpContext.ColorPicker:
                  fullyQualifiedPath = Path.Combine(fullyQualifiedPath, "DBCodeColorPickerHelp.html");
                  break;
            }
            if (!File.Exists(fullyQualifiedPath)) {
               TimedMessage(string.Format("DBCode's help HTML file:\ncould not be found"), "Missing Help File");
               return;
            }
            Process process = new Process {
               StartInfo = new ProcessStartInfo(fullyQualifiedPath) {
                  UseShellExecute = true
               }
            };
            process.Start();
            Thread.Sleep(100);//efm5 this seems to be necessary
         }
         catch (Exception pException) {
            TimedMessage("Opening the Help HTML file failed\n" + pException.ToString(), "File ERROR");
         }
      }

      private static Bitmap GetSizedImage(Icons pWhich, float pFontSize) {
         return GetSizedIcon(pWhich, pFontSize).ToBitmap();
      }

      private static Size ChooseIconSize(float pFontSize) {
         if (pFontSize <= 16f)
            return new Size(16, 16);
         if (pFontSize <= 24f)
            return new Size(24, 24);
         if (pFontSize <= 32f)
            return new Size(32, 32);
         if (pFontSize <= 48f)
            return new Size(48, 48);
         if (pFontSize <= 64f)
            return new Size(64, 64);
         if (pFontSize <= 128f)
            return new Size(128, 128);
         return new Size(256, 256);
      }

      private static Icon GetSizedIcon(Icons pWhich, float pFontSize) {
         return new Icon(GetIcon(pWhich), ChooseIconSize(pFontSize));
      }

      private static Icon GetIcon(Icons pWhich) {
         return mIcons[(int)pWhich];
      }

      private void ReturnToTop() {
         if (mForceActivation) {
            BringToTop_Activate();
         }
         else {
            BringToTop_NoActivate();
         }
      }

      private void BringToTop_Activate() {
         SetForegroundWindow(Handle);
      }

      private void BringToTop_NoActivate() {
         SetWindowPos(Handle, mInsertAfterWindow, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
      }

      private static void Paste(PasteMode pPasteMode) {
         IntPtr pTarget = ResolveTargetWindow();
         if (pTarget == IntPtr.Zero)
            return;

         switch (pPasteMode) {
            case PasteMode.SendAll:
               TimedMessage("sending all data to the target window", "PASTING", 2000);
               break;
            case PasteMode.PasteSelected:
               TimedMessage("pasting selected data to the target window", "PASTING", 2000);
               break;
         }
         //DEBUG bring to top, paste, restore DBCode if AlwaysOnTop
         //efm5 I'm probably going to have to rethink always on top to not be TopMost and instead be Top
      }

      private static IntPtr ResolveTargetWindow() {
         if (mIsTargetingEnabled) {
            if (IsValidTargetWindow(mTargetWindow))
               return Fields.mTargetWindow;

            //#pragma warning disable IDE0028//efm5 warning
            IntPtr pFallback = GetMostSuitableWindow();
            //#pragma warning restore IDE0028

            mTargetWindow = pFallback;
            mTargetWindowName = GetWindowTitle(pFallback);
            UpdateTargetingStatusLabel();
            return pFallback;
         }
         return GetFunctionalTargetWindow();
      }

      private static IntPtr GetFunctionalTargetWindow() {
         return GetMostSuitableWindow();
      }

      private static void EnterTargetedMode() {
         //#pragma warning disable IDE0028//efm5 warning
         IntPtr pWindow = GetMostSuitableWindow();
         //#pragma warning restore IDE0028
         if ((pWindow == IntPtr.Zero) && (mTargetedTSMI != null)) {
            mTargetedTSMI.Checked = false;
            EnterUntargetedMode();
            return;
         }

         mIsTargetingEnabled = true;
         mTargetWindow = pWindow;
         mTargetWindowName = GetWindowTitle(pWindow);
         UpdateTargetingStatusLabel();
      }

      private static void EnterUntargetedMode() {
         mIsTargetingEnabled = false;
         mTargetWindow = IntPtr.Zero;
         mTargetWindowName = string.Empty;
         UpdateTargetingStatusLabel();
      }

      private static void UpdateTargetingStatusLabel() {
         if (mTargetingLabel == null)
            return;
         float fontSize = mTargetingLabel.Font.Size;
         Size fontMeasure = TextRenderer.MeasureText("Wg", mTargetingLabel.Font); // avoids Font.Height DC dependency
         if (mIsTargetingEnabled) {
            Image image = GetSizedImage(Icons.StatusTargeted, fontSize);
            mTargetingLabel.Image = image;
            mTargetingLabel.Text = GetWindowTitle(mTargetWindow);
            int textWidth = TextRenderer.MeasureText(mTargetingLabel.Text, mTargetingLabel.Font).Width;
            int height = Math.Max(image.Height, fontMeasure.Height);
            mTargetingLabel.Size = new Size(image.Width + textWidth + 4, height);
         }
         else {
            Image image = GetSizedImage(Icons.StatusUntargeted, fontSize);
            mTargetingLabel.Image = image;
            mTargetingLabel.Text = string.Empty;
            int height = Math.Max(image.Height, fontMeasure.Height);
            mTargetingLabel.Size = new Size(image.Width, height);
         }
         mTargetingLabel.Top = (mMainBottomPanel!.Height - mTargetingLabel.Height) / 2;
      }

      private static void LoadEmbeddedIcons() {
         const string throwPrefix = "Error loading embedded icon resource: ";
         Assembly? assembly = Assembly.GetExecutingAssembly();
         if (assembly == null)
            throw new InvalidOperationException(throwPrefix + "Assembly is null.");
         string resourceName = "DBCode.Icons.CurlyTargetedIcon.ico";
         using Stream? stream1 = assembly.GetManifestResourceStream(resourceName);

         if (stream1 is null)
            throw new InvalidOperationException(throwPrefix + resourceName);
         mIcons[(int)Icons.CurlyTargeted] = new Icon(stream1);

         resourceName = "DBCode.Icons.CurlyUntargetedIcon.ico";
         using Stream? stream2 = assembly.GetManifestResourceStream(resourceName);
         if (stream2 is null)
            throw new InvalidOperationException(throwPrefix + resourceName);
         mIcons[(int)Icons.CurlyUntargeted] = new Icon(stream2);

         resourceName = "DBCode.Icons.StatusTargetedIcon.ico";
         using Stream? stream3 = assembly.GetManifestResourceStream(resourceName);
         if (stream3 is null)
            throw new InvalidOperationException(throwPrefix + resourceName);
         mIcons[(int)Icons.StatusTargeted] = new Icon(stream3);

         resourceName = "DBCode.Icons.StatusUntargetedIcon.ico";
         using Stream? stream4 = assembly.GetManifestResourceStream(resourceName);
         if (stream4 is null)
            throw new InvalidOperationException(throwPrefix + resourceName);
         mIcons[(int)Icons.StatusUntargeted] = new Icon(stream4);
      }

      private void InitializeIcon() {
         Icon = mIcons[(int)Icons.StatusTargeted];
      }

      private static void UpdateOpacityMenuChecks(double pOpacity) {
         if (mVisibilityMenuItem == null)
            return;
         foreach (ToolStripMenuItem tsmi in mVisibilityMenuItem.DropDownItems.OfType<ToolStripMenuItem>()) {
            if (tsmi.Tag != null)
               tsmi.Checked = Math.Abs((double)tsmi.Tag - pOpacity) < 0.001;
            //else we could warn the programmer of the problem
         }
      }

      private void ApplyViewMode(ViewMode pViewMode) {
         ViewMode previousMode = mCurrentViewMode;

         if (previousMode == pViewMode)
            return;

         mCurrentViewMode = pViewMode;
         if (pViewMode == ViewMode.Minimal)
            EnterMinimalView();
         else
            EnterFeaturesView();
      }

      private void EnterMinimalView() {
         string currentText = Text;
         bool currentControlBox = ControlBox;

         mPreMinimalText = currentText;
         mPreMinimalControlBox = currentControlBox;

         mMenuStrip?.Visible = false;
         Text = string.Empty;
         ControlBox = false;

         mMinimalTSMI?.Checked = true;
         mFeaturesTSMI?.Checked = false;
      }

      private void EnterFeaturesView() {
         string restoredText = mPreMinimalText;
         bool restoredControlBox = mPreMinimalControlBox;

         if (string.IsNullOrEmpty(restoredText))
            restoredText = "Targeting Application";

         Text = restoredText;
         ControlBox = restoredControlBox;
         mMenuStrip?.Visible = true;

         mMinimalTSMI?.Checked = false;
         mFeaturesTSMI?.Checked = true;
      }

      private void LayoutMainBottomPanel() {
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         ThrowIfNull(mMenuStrip, nameof(mMenuStrip));
         mMainBottomPanel.LayoutControls();
      }
   }
}
