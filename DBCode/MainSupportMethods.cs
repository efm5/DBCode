namespace DBCode {
   public sealed partial class MainForm : Form {
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

      private static void Paste(PasteMode pPasteMode) {
         IntPtr pTarget = ResolveTargetWindow();
         if (pTarget == IntPtr.Zero)
            return;

         switch (pPasteMode) {
            case PasteMode.Transport:
               TimedMessage("transporting data to the target window", "PASTING", 2000);
               break;
            case PasteMode.Transfer:
               TimedMessage("transferring data to the target window", "PASTING", 2000);
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
         if (pWindow == IntPtr.Zero) {
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
         float fontSize = mTargetingStatusLabel.Font.Size;

         if (mIsTargetingEnabled) {
            mTargetingStatusLabel.Image = GetSizedImage(Icons.StatusTargeted, fontSize);
            mTargetingStatusLabel.Text = mTargetWindowName;
         }
         else {
            mTargetingStatusLabel.Image = GetSizedImage(Icons.StatusUntargeted, fontSize);
            mTargetingStatusLabel.Text = string.Empty;
         }
      }

      private static void LoadEmbeddedIcons() {
         Assembly assembly = Assembly.GetExecutingAssembly();
         const string throwPrefix = "Error loading embedded icon resource: ";

         string resourceName = "DBCode.CurlyTargetedIcon.ico";
         using Stream stream1 = assembly.GetManifestResourceStream(resourceName);
         if (stream1 is null)
            throw new InvalidOperationException(throwPrefix + resourceName);
         mIcons[(int)Icons.CurlyTargeted] = new Icon(stream1);

         resourceName = "DBCode.CurlyUntargetedIcon.ico";
         using Stream stream2 = assembly.GetManifestResourceStream(resourceName);
         if (stream2 is null)
            throw new InvalidOperationException(throwPrefix + resourceName);
         mIcons[(int)Icons.CurlyUntargeted] = new Icon(stream2);

         resourceName = "DBCode.StatusTargetedIcon.ico";
         using Stream stream3 = assembly.GetManifestResourceStream(resourceName);
         if (stream3 is null)
            throw new InvalidOperationException(throwPrefix + resourceName);
         mIcons[(int)Icons.StatusTargeted] = new Icon(stream3);

         resourceName = "DBCode.StatusUntargetedIcon.ico";
         using Stream stream4 = assembly.GetManifestResourceStream(resourceName);
         if (stream4 is null)
            throw new InvalidOperationException(throwPrefix + resourceName);
         mIcons[(int)Icons.StatusUntargeted] = new Icon(stream4);
      }

      private void InitializeIcon() {
         Icon = mIcons[(int)Icons.StatusUntargeted];
      }

      private static void UpdateOpacityMenuChecks(double pOpacity) {
         ToolStripMenuItem pItem = null;
         double itemOpacity = 0.0;

         foreach (ToolStripMenuItem pItemLoop in mVisibilityMenuItem.DropDownItems) {
            pItem = pItemLoop;
            itemOpacity = (double)pItem.Tag;
            pItem.Checked = Math.Abs(itemOpacity - pOpacity) < 0.001;
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

         mMenuStrip.Visible = false;
         Text = string.Empty;
         ControlBox = false;

         mMinimalTSMI.Checked = true;
         mFeaturesTSMI.Checked = false;
      }

      private void EnterFeaturesView() {
         string restoredText = mPreMinimalText;
         bool restoredControlBox = mPreMinimalControlBox;

         if (string.IsNullOrEmpty(restoredText))
            restoredText = "Targeting Application";

         Text = restoredText;
         ControlBox = restoredControlBox;
         mMenuStrip.Visible = true;

         mMinimalTSMI.Checked = false;
         mFeaturesTSMI.Checked = true;
      }

      public static void TimedMessage(string pMessage, string pTitle = "", int pDuration = 4500) {
         _ = MessageBoxTimeout(IntPtr.Zero, pMessage, pTitle, TIMED_MESSAGEBOX_FLAGS, 0, pDuration);
      }
   }
}
