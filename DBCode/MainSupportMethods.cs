namespace DBCode {
   public sealed partial class MainForm : Form {
#if DEBUG
      public class MenuNode {
         internal ToolStripMenuItem mItem;
         internal List<MenuNode> mChildren = [];
         internal MenuNode(ToolStripMenuItem pItem) {
            mItem = pItem;
         }
      }

      public static List<char> mUsableCharacters = [
         'a','b','c','d','e','f','g','h','i','j','k','l','m',
   'n','o','p','q','r','s','t','u','v','w','x','y','z',
   '0','1','2','3','4','5','6','7','8','9',
   '`','-','=','[',']',';','\'','"',',','.','/','\\'
      ];

      internal static void ProposeAccelerators(MenuStrip pMenuStrip, List<ButtonBase> pReserved) {

         static char ExtractAccelerator(string pText) {       // '\0' if no accelerator found.
            for (int i = 0; i < pText.Length - 1; i++) {
               if (pText[i] == '&' && pText[i + 1] != '&')
                  return char.ToLower(pText[i + 1]);
            }
            return '\0';
         }

         static bool IsCompliantName(string pName) {          // Must match m[A-Z][a-zA-Z0-9]*TSMI.
            if (pName.Length < 6)
               return false;
            if (pName[0] != 'm')
               return false;
            if (!char.IsUpper(pName[1]))
               return false;
            if (!pName.EndsWith("TSMI"))
               return false;
            for (int i = 2; i < pName.Length - 4; i++) {
               if (!char.IsLetterOrDigit(pName[i]))
                  return false;
            }
            return true;
         }

         static char AssignMenuAccelerator(ToolStripMenuItem pItem,
            List<char> pLocalReserved, List<char> pUsable) {
            string mMasked = pItem.Text.Replace("&&", "\0\0"); // Mask escaped ampersands.
            if (mMasked.Length > 0) {                          // Pass 2 — try first character.
               char mFirst = char.ToLower(mMasked[0]);
               if (pUsable.Contains(mFirst) && !pLocalReserved.Contains(mFirst)) {
                  pItem.Text = "&" + pItem.Text;
                  pLocalReserved.Add(mFirst);
                  return mFirst;
               }
            }
            for (int i = 1; i < mMasked.Length; i++) {        // Pass 3 — scan from second character.
               char mCandidate = char.ToLower(mMasked[i]);
               if (pUsable.Contains(mCandidate) && !pLocalReserved.Contains(mCandidate)) {
                  pItem.Text = pItem.Text.Insert(i, "&");
                  pLocalReserved.Add(mCandidate);
                  return mCandidate;
               }
            }
            return '\0';
         }

         static char AssignControlAccelerator(ButtonBase pControl,
            List<char> pLocalReserved, List<char> pUsable) {
            string mOriginal = pControl.Text;
            string mMasked = mOriginal.Replace("&&", "\0\0");
            if (mMasked.Length > 0) {                          // Pass 2 — try first character.
               char mFirst = char.ToLower(mMasked[0]);
               if (pUsable.Contains(mFirst) && !pLocalReserved.Contains(mFirst)) {
                  pControl.Text = "&" + mOriginal;
                  pLocalReserved.Add(mFirst);
                  return mFirst;
               }
            }
            for (int i = 1; i < mMasked.Length; i++) {        // Pass 3 — scan from second character.
               char mCandidate = char.ToLower(mMasked[i]);
               if (pUsable.Contains(mCandidate) && !pLocalReserved.Contains(mCandidate)) {
                  pControl.Text = mOriginal.Insert(i, "&");
                  pLocalReserved.Add(mCandidate);
                  return mCandidate;
               }
            }
            return '\0';
         }

         static List<MenuNode> BuildTree(ToolStripItemCollection pItems) {
            List<MenuNode> mNodes = [];
            foreach (ToolStripItem mRawItem in pItems) {
               if (mRawItem is not ToolStripMenuItem mMenuItem)
                  continue;
               MenuNode mNode = new MenuNode(mMenuItem);
               if (mMenuItem.HasDropDownItems)
                  mNode.mChildren = BuildTree(mMenuItem.DropDownItems);
               mNodes.Add(mNode);
            }
            return mNodes;
         }

         static void ProcessChildren(
            List<MenuNode> pNodes,
            List<char> pFrozenReserved,
            List<char> pUsable,
            List<string> pNoncompliant,
            List<string> pFailures,
            List<string> pStatements,
            Func<string, char> pExtractAccelerator,
            Func<string, bool> pIsCompliantName,
            Func<ToolStripMenuItem, List<char>, List<char>, char> pAssignMenuAccelerator) {
            List<char> mSubReserved = new List<char>(pFrozenReserved);

            // Pass 1 — collect already-assigned accelerators among these siblings.
            foreach (MenuNode mNode in pNodes) {
               char mExisting = pExtractAccelerator(mNode.mItem.Text);
               if (mExisting != '\0' && !mSubReserved.Contains(mExisting))
                  mSubReserved.Add(mExisting);
            }

            // Passes 2 & 3 — assign to siblings that have none.
            foreach (MenuNode mNode in pNodes) {
               char mExisting = pExtractAccelerator(mNode.mItem.Text);
               if (mExisting != '\0')
                  continue;                                    // Already has one — skip.
               if (!pIsCompliantName(mNode.mItem.Name))
                  pNoncompliant.Add(
                     $"[{mNode.mItem.Name}] \"{mNode.mItem.Text}\" — noncompliant name.");
               char mAssigned = pAssignMenuAccelerator(mNode.mItem, mSubReserved, pUsable);
               if (mAssigned != '\0')
                  pStatements.Add($"{mNode.mItem.Name}.Text = \"{mNode.mItem.Text}\";");
               else
                  pFailures.Add(
                     $"[{mNode.mItem.Name}] \"{mNode.mItem.Text}\" — no accelerator available.");
            }

            // Recurse into children of any node that has them, each getting
            // its own fresh mSubReserved seeded from pFrozenReserved.
            foreach (MenuNode mNode in pNodes) {
               if (mNode.mChildren.Count > 0)
                  ProcessChildren(mNode.mChildren, pFrozenReserved, pUsable,
                     pNoncompliant, pFailures, pStatements,
                     pExtractAccelerator, pIsCompliantName, pAssignMenuAccelerator);
            }
         }                                                     // Closes ProcessChildren.

         // ---- Build the full node tree ------------------------------------------------
         List<MenuNode> mTopLevelNodes = BuildTree(pMenuStrip.Items);
         List<char> mUsedAccelerators = [];
         List<ButtonBase> mControlsNeedingAccel = [];
         List<ButtonBase> mPullList = [];
         List<string> mNoncompliant = [];
         List<string> mFailures = [];
         List<string> mStatements = [];

         // ---- Pass 1: collect already-assigned top-level menu accelerators ------------
         List<MenuNode> mTopLevelNeedingAccel = [];
         foreach (MenuNode mNode in mTopLevelNodes) {
            char mExisting = ExtractAccelerator(mNode.mItem.Text);
            if (mExisting != '\0') {
               if (!mUsedAccelerators.Contains(mExisting))
                  mUsedAccelerators.Add(mExisting);
            }
            else {
               mTopLevelNeedingAccel.Add(mNode);
            }
         }

         // ---- Pass 1: collect already-assigned reserved control accelerators ----------
         foreach (ButtonBase mControl in pReserved) {
            char mExisting = ExtractAccelerator(mControl.Text);
            if (mExisting != '\0') {
               if (!mUsedAccelerators.Contains(mExisting))
                  mUsedAccelerators.Add(mExisting);
               mPullList.Add(mControl);
            }
            else {
               mControlsNeedingAccel.Add(mControl);
            }
         }
         mPullList.Clear();                                    // Pull list served its purpose.

         // ---- Passes 2 & 3: assign top-level menu accelerators -----------------------
         foreach (MenuNode mNode in mTopLevelNeedingAccel) {
            if (!IsCompliantName(mNode.mItem.Name))
               mNoncompliant.Add(
                  $"[{mNode.mItem.Name}] \"{mNode.mItem.Text}\" — noncompliant name.");
            char mAssigned = AssignMenuAccelerator(mNode.mItem, mUsedAccelerators, mUsableCharacters);
            if (mAssigned != '\0')
               mStatements.Add($"{mNode.mItem.Name}.Text = \"{mNode.mItem.Text}\";");
            else
               mFailures.Add(
                  $"[{mNode.mItem.Name}] \"{mNode.mItem.Text}\" — no accelerator available.");
         }

         // ---- Passes 2 & 3: assign reserved control accelerators ---------------------
         foreach (ButtonBase mControl in mControlsNeedingAccel) {
            if (!IsCompliantName(mControl.Name))
               mNoncompliant.Add(
                  $"[{mControl.Name}] \"{mControl.Text}\" — noncompliant name.");
            char mAssigned = AssignControlAccelerator(mControl, mUsedAccelerators, mUsableCharacters);
            if (mAssigned != '\0')
               mStatements.Add($"{mControl.Name}.Text = \"{mControl.Text}\";");
            else
               mFailures.Add(
                  $"[{mControl.Name}] \"{mControl.Text}\" — no accelerator available.");
         }

         // mUsedAccelerators is now frozen — snapshot before subitem processing.
         List<char> mFrozenReserved = new List<char>(mUsedAccelerators);

         // ---- Recurse into subitems of each top-level node ---------------------------
         foreach (MenuNode mTopNode in mTopLevelNodes) {
            if (mTopNode.mChildren.Count > 0)
               ProcessChildren(mTopNode.mChildren, mFrozenReserved, mUsableCharacters,
                  mNoncompliant, mFailures, mStatements,
                  ExtractAccelerator, IsCompliantName, AssignMenuAccelerator);
         }

         // ---- Build and deliver report ------------------------------------------------
         StringBuilder mReport = new StringBuilder();

         mReport.AppendLine("// ---- Noncompliant naming ----");
         if (mNoncompliant.Count == 0)
            mReport.AppendLine("//   None.");
         else
            foreach (string mLine in mNoncompliant)
               mReport.AppendLine(mLine);

         mReport.AppendLine();
         mReport.AppendLine("// ---- Failed accelerator assignments ----");
         if (mFailures.Count == 0)
            mReport.AppendLine("//   None.");
         else
            foreach (string mLine in mFailures)
               mReport.AppendLine(mLine);

         mReport.AppendLine();
         mReport.AppendLine("// ---- Code-ready statements ----");
         if (mStatements.Count == 0)
            mReport.AppendLine("//   None.");
         else
            foreach (string mLine in mStatements)
               mReport.AppendLine(mLine);

         Clipboard.SetText(mReport.ToString());
         TimedMessage("The accelerator information is on the clipboard.", "Information");
      }                                                        // Closes ProposeAccelerators.

      internal static void SuggestMenuShortcuts(List<ToolStripMenuItem> pMenuItems) {
         // Commonly reserved system and application shortcuts — do not auto-assign these.
         List<Keys> mReservedShortcuts = [
            Keys.Control | Keys.A,   Keys.Control | Keys.C,   Keys.Control | Keys.V,
      Keys.Control | Keys.X,   Keys.Control | Keys.Z,   Keys.Control | Keys.Y,
      Keys.Control | Keys.S,   Keys.Control | Keys.P,   Keys.Control | Keys.F,
      Keys.Control | Keys.O,   Keys.Control | Keys.N,   Keys.Control | Keys.W,
      Keys.Control | Keys.T,   Keys.Control | Keys.B,   Keys.Control | Keys.I,
      Keys.Control | Keys.U,   Keys.Control | Keys.H,   Keys.Control | Keys.G,
      Keys.Control | Keys.R,   Keys.Control | Keys.E,   Keys.Control | Keys.D,
      Keys.Control | Keys.K,   Keys.Control | Keys.L,   Keys.Control | Keys.M,
      Keys.Alt     | Keys.F4,
      Keys.F1                  // Reserved for Help — caller assigns manually.
         ];

         // Semantic inference map: try these before the normal tier scan.
         // Each tuple maps a set of keywords to a preferred Keys candidate.
         List<(string[] mKeywords, Keys mSuggested)> mInferenceMap = [
            (["start",    "beginning", "first",    "top"],          Keys.Control | Keys.Home),
      (["end",      "last",      "bottom"],                   Keys.Control | Keys.End),
      (["next",     "forward",   "advance"],                  Keys.Control | Keys.Right),
      (["previous", "back",      "prior",    "prev"],         Keys.Control | Keys.Left),
      (["pagedown", "page down", "later",    "forward page"], Keys.Control | Keys.PageDown),
      (["pageup",   "page up",   "earlier",  "back page"],    Keys.Control | Keys.PageUp),
      (["delete",   "remove",    "erase",    "clear"],        Keys.Control | Keys.Delete),
      (["insert",   "add",       "new",      "create"],       Keys.Control | Keys.Insert),
      (["undo"],                                              Keys.Control | Keys.Back),
      (["up"],                                               Keys.Control | Keys.Up),
      (["down"],                                             Keys.Control | Keys.Down)
         ];

         // Printable toprow characters in candidate order within each tier.
         List<char> mKeyChars = [
            'a','b','c','d','e','f','g','h','i','j','k','l','m',
      'n','o','p','q','r','s','t','u','v','w','x','y','z',
      '0','1','2','3','4','5','6','7','8','9',
      '`','-','=','[',']',';','\'','"',',','.','/','\\'
         ];

         // Navigation and editing keys — only valid starting at Ctrl+[key] tier.
         List<Keys> mNavKeys = [
            Keys.Back,   Keys.Insert, Keys.Delete,
      Keys.Home,   Keys.End,
      Keys.PageUp, Keys.PageDown,
      Keys.Left,   Keys.Right,  Keys.Up,  Keys.Down,
      Keys.Return
         ];

         // NumPad keys — digits then punctuation.
         List<Keys> mNumPadKeys = [
            Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4,
      Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9,
      Keys.Decimal, Keys.Subtract, Keys.Divide, Keys.Multiply, Keys.Add,
      Keys.Return   // NumPad Enter — distinct from toprow Enter in some contexts.
         ];

         static Keys CharToKeys(char pChar) {
            return pChar switch {
               >= 'a' and <= 'z' => (Keys)char.ToUpper(pChar),
               >= '0' and <= '9' => (Keys)pChar,
               '`' => Keys.Oemtilde,
               '-' => Keys.OemMinus,
               '=' => Keys.Oemplus,
               '[' => Keys.OemOpenBrackets,
               ']' => Keys.OemCloseBrackets,
               ';' => Keys.OemSemicolon,
               '\'' => Keys.OemQuotes,
               '"' => Keys.OemQuotes,
               ',' => Keys.Oemcomma,
               '.' => Keys.OemPeriod,
               '/' => Keys.OemQuestion,
               '\\' => Keys.OemBackslash,
               _ => Keys.None
            };
         }

         static List<Keys> BuildCandidates(List<char> pKeyChars, List<Keys> pNavKeys,
            List<Keys> pNumPadKeys, bool pTreatNumPadAsTopRow) {
            List<Keys> mCandidates = [];
            Keys[] mModifierTiers = [
               Keys.Control,
         // Keys.Alt,              // Alt+[key] is unreliable in WinForms at init time;
         //                        // behavior inconsistent across Windows versions.
         //                        // Can be reinstated for dynamic KeyDown interception.
         Keys.Control | Keys.Shift,
         // Keys.Control | Keys.Alt, // Ctrl+Alt == AltGr on international keyboards;
         //                          // can cause OS-level interception. Reliable in
         //                          // practice on US English — reinstate if needed.
         Keys.Shift   | Keys.Alt,
         Keys.Control | Keys.Shift | Keys.Alt
            ];

            foreach (Keys mModifier in mModifierTiers) {
               bool mIsCtrlTier = (mModifier & Keys.Control) != 0; // Nav keys need Ctrl.

               // Toprow characters.
               foreach (char c in pKeyChars) {
                  Keys mBase = CharToKeys(c);
                  if (mBase != Keys.None)
                     mCandidates.Add(mModifier | mBase);
               }

               // Navigation and editing keys — Ctrl required.
               if (mIsCtrlTier) {
                  foreach (Keys mNav in pNavKeys)
                     mCandidates.Add(mModifier | mNav);
               }

               // NumPad keys interleaved with toprow tiers.
               if (pTreatNumPadAsTopRow) {
                  foreach (Keys mPad in pNumPadKeys)
                     mCandidates.Add(mModifier | mPad);
               }
            }

            // NumPad as distinct tiers appended after all toprow tiers.
            if (!pTreatNumPadAsTopRow) {
               Keys[] mNumPadModifierTiers = [
                  Keys.Control,
            // Keys.Alt,
            Keys.Control | Keys.Shift,
            // Keys.Control | Keys.Alt,
            Keys.Shift   | Keys.Alt,
            Keys.Control | Keys.Shift | Keys.Alt
               ];
               foreach (Keys mModifier in mNumPadModifierTiers) {
                  foreach (Keys mPad in pNumPadKeys)
                     mCandidates.Add(mModifier | mPad);
               }
            }

            return mCandidates;
         }

         static Keys TryInference(string pText, string pName,
            List<(string[] mKeywords, Keys mSuggested)> pInferenceMap,
            List<Keys> pUsedShortcuts) {
            string mSearchText = (pText + " " + pName).ToLower();
            foreach ((string[] mKeywords, Keys mSuggested) in pInferenceMap) {
               foreach (string mKeyword in mKeywords) {
                  if (mSearchText.Contains(mKeyword) && !pUsedShortcuts.Contains(mSuggested))
                     return mSuggested;
               }
            }
            return Keys.None;
         }

         static string FormatKeys(Keys pKeys) {
            Keys mModifiers = pKeys & Keys.Modifiers;
            Keys mBaseKey = pKeys & ~Keys.Modifiers;
            string mResult = string.Empty;
            if ((mModifiers & Keys.Control) != 0)
               mResult += "Keys.Control | ";
            if ((mModifiers & Keys.Shift) != 0)
               mResult += "Keys.Shift | ";
            if ((mModifiers & Keys.Alt) != 0)
               mResult += "Keys.Alt | ";
            mResult += $"Keys.{mBaseKey}";
            return mResult;
         }

         List<Keys> mUsedShortcuts = new List<Keys>(mReservedShortcuts);
         List<ToolStripMenuItem> mMenuItems = new List<ToolStripMenuItem>(pMenuItems);
         List<Keys> mCandidates = BuildCandidates(mKeyChars, mNavKeys,
                                                   mNumPadKeys, pTreatNumPadAsTopRow: true);

         // Collect shortcuts already assigned to items in the list.
         foreach (ToolStripMenuItem mItem in mMenuItems) {
            if (mItem.ShortcutKeys != Keys.None && !mUsedShortcuts.Contains(mItem.ShortcutKeys))
               mUsedShortcuts.Add(mItem.ShortcutKeys);
         }

         StringBuilder mReport = new StringBuilder();
         StringBuilder mStatements = new StringBuilder();
         int mAssigned = 0;
         int mUnassigned = 0;

         foreach (ToolStripMenuItem mItem in mMenuItems) {
            if (mItem.DropDownItems.Count > 0)   // Submenu parents get accelerators, not shortcuts.
               continue;
            if (mItem.ShortcutKeys != Keys.None) {
               mReport.AppendLine($"[{mItem.Name}] \"{mItem.Text}\" — already has: " +
                  $"{FormatKeys(mItem.ShortcutKeys)}");
               mAssigned++;
               continue;
            }

            // Try semantic inference first.
            Keys mSuggested = TryInference(mItem.Text, mItem.Name, mInferenceMap, mUsedShortcuts);

            // Fall back to normal tier scan.
            if (mSuggested == Keys.None) {
               foreach (Keys mCandidate in mCandidates) {
                  if (!mUsedShortcuts.Contains(mCandidate)) {
                     mSuggested = mCandidate;
                     break;
                  }
               }
            }

            if (mSuggested != Keys.None) {
               mUsedShortcuts.Add(mSuggested);
               mReport.AppendLine($"[{mItem.Name}] \"{mItem.Text}\" — suggested: " +
                  $"{FormatKeys(mSuggested)}");
               mStatements.AppendLine($"{mItem.Name}.ShortcutKeys = {FormatKeys(mSuggested)};");
               mAssigned++;
            }
            else {
               mReport.AppendLine($"[{mItem.Name}] \"{mItem.Text}\" — NO SHORTCUT AVAILABLE");
               mUnassigned++;
            }
         }

         mReport.AppendLine();
         mReport.AppendLine($"Assigned or suggested : {mAssigned}");
         mReport.AppendLine($"No shortcut available  : {mUnassigned}");
         mReport.AppendLine();
         mReport.AppendLine("// ---- Code-ready statements ----");
         mReport.AppendLine();
         mReport.Append(mStatements);

         Clipboard.SetText(mReport.ToString());
         TimedMessage("The shortcut information is on the clipboard.", "Information");
      }
#endif

      private int MinimumMainBottomWidth() {
         Font font;
         if (mCurrentTheme != null)
            font = mCurrentTheme.mFonts[(int)FontUsage.Status];
         else if (mThemes.Count > 0)
            font = mThemes[0].mFonts[(int)FontUsage.Status];
         else
            font = SystemFonts.StatusFont!;
         using TextBox measuringTextBox = new TextBox {
            Font = font,
            Text = mUnicodeSampleString +
               " Help Send All Paste Selected Get All Get Selected v:0.0.0.0 Revert Exit"
         };
         SizeTextBoxToFitString(out SizeF pSize, measuringTextBox);
         return (int)pSize.Width;
      }

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
         ThrowIfNull(mLanguageMenuItem, nameof(mLanguageMenuItem));
         foreach (ToolStripMenuItem tsmi in mLanguageMenuItem.DropDownItems.OfType<ToolStripMenuItem>())
            tsmi.Checked = false;
         switch (mCurrentLanguage) {
            case LanguageKind.CSharp:
               ThrowIfNull(mCSharpTSMI, nameof(mCSharpTSMI));
               mCSharpTSMI.Checked = true;
               break;
            case LanguageKind.C:
               ThrowIfNull(mCTSMI, nameof(mCTSMI));
               mCTSMI.Checked = true;
               break;
            case LanguageKind.Cpp:
               ThrowIfNull(mCppTSMI, nameof(mCppTSMI));
               mCppTSMI.Checked = true;
               break;
            case LanguageKind.Basic:
               ThrowIfNull(mBasicTSMI, nameof(mBasicTSMI));
               mBasicTSMI.Checked = true;
               break;
            case LanguageKind.FSharp:
               ThrowIfNull(mFSharpTSMI, nameof(mFSharpTSMI));
               mFSharpTSMI.Checked = true;
               break;
            case LanguageKind.Html:
               ThrowIfNull(mHtmlTSMI, nameof(mHtmlTSMI));
               mHtmlTSMI.Checked = true;
               break;
            case LanguageKind.Css:
               ThrowIfNull(mCssTSMI, nameof(mCssTSMI));
               mCssTSMI.Checked = true;
               break;
            case LanguageKind.Xml:
               ThrowIfNull(mXmlTSMI, nameof(mXmlTSMI));
               mXmlTSMI.Checked = true;
               break;
            case LanguageKind.Json:
               ThrowIfNull(mJsonTSMI, nameof(mJsonTSMI));
               mJsonTSMI.Checked = true;
               break;
            case LanguageKind.PowerShell:
               ThrowIfNull(mPowerShellTSMI, nameof(mPowerShellTSMI));
               mPowerShellTSMI.Checked = true;
               break;
            case LanguageKind.Batch:
               ThrowIfNull(mBatchTSMI, nameof(mBatchTSMI));
               mBatchTSMI.Checked = true;
               break;
            case LanguageKind.Sql:
               ThrowIfNull(mSqlTSMI, nameof(mSqlTSMI));
               mSqlTSMI.Checked = true;
               break;
            case LanguageKind.Markdown:
               ThrowIfNull(mMarkdownTSMI, nameof(mMarkdownTSMI));
               mMarkdownTSMI.Checked = true;
               break;
            case LanguageKind.Python:
               ThrowIfNull(mPythonTSMI, nameof(mPythonTSMI));
               mPythonTSMI.Checked = true;
               break;
            case LanguageKind.PlainText:
               ThrowIfNull(mPlainTextTSMI, nameof(mPlainTextTSMI));
               mPlainTextTSMI.Checked = true;
               break;
         }
      }

      public void RehighlightText() {
         ThrowIfNull(mHighlighterEngine, nameof(mHighlighterEngine));
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         mRichTextBox.TextChanged -= OnEditorTextChanged;
         mHighlighterEngine.HighlightNow();
         mRichTextBox.TextChanged += OnEditorTextChanged;
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
         ThrowIfNull(mCurrentThemeIsTSMI, nameof(mCurrentThemeIsTSMI));
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         ThrowIfNull(mMenuStrip, nameof(mMenuStrip));
         ThrowIfNull(mHighlighterEngine, nameof(mHighlighterEngine));
         mRichTextBox.TextChanged -= OnEditorTextChanged;
         Theme theme = mCurrentTheme;
         mCurrentThemeIsTSMI.Text = mCurrently + theme.mName;
         mForm.BackColor = theme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground];
         mRichTextBox.BackColor = theme.mInterfaceColors[(int)ColorSwatchUsage.TextBox];
         mRichTextBox.ForeColor = theme.mInterfaceColors[(int)ColorSwatchUsage.TextBoxFont];
         mMenuStrip.BackColor = theme.mInterfaceColors[(int)ColorSwatchUsage.MenuBackground];
         foreach (ToolStripMenuItem toolStripMenuItem in mMenuStrip.Items.OfType<ToolStripMenuItem>()) {
            PaintMenuItem(toolStripMenuItem, theme);
            foreach (ToolStripMenuItem subItem in toolStripMenuItem.DropDownItems.OfType<ToolStripMenuItem>())
               PaintMenuItemsRecursive(subItem, theme);
         }
         mMainBottomPanel.BackColor = theme.mInterfaceColors[(int)ColorSwatchUsage.StatusBackground];
         mMainBottomPanel.SetFontAndColor();
         LayoutMainBottomPanel();
         mMainBottomPanel.Invalidate(true);
         mHighlighterEngine.HighlightNow();
         mRichTextBox.TextChanged += OnEditorTextChanged;
         ResumeLayout(true);
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

      private static async void SendAllAsync() {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         string text = mRichTextBox.Text;
         if (!string.IsNullOrEmpty(text)) {
            TimedMessage("There is no text to send.", "Nothing To Send", 2500);
            return;
         }
         mScrollableMainPanel.Enabled = false;
         try {
            IntPtr target;
            if (mIsTargetingEnabled) {
               if (!IsValidTargetWindow(mTargetWindow)) {
                  mIsTargetingEnabled = false;
                  mTargetWindow = IntPtr.Zero;
                  mTargetWindowName = string.Empty;
                  if (mTargetedTSMI != null)
                     mTargetedTSMI.Checked = false;
                  UpdateTargetingStatusLabel();
                  TimedMessage("The targeted window no longer exists. Targeting has been turned off.", "Target Lost");
                  return;
               }
               target = mTargetWindow;
            }
            else {
               target = GetMostSuitableWindowAllowedFirst();
               if (target == IntPtr.Zero) {
                  TimedMessage("No suitable target window was found.", "No Target");
                  return;
               }
            }
            ClipboardHelper.TrySetClipboardText(text);
            await Task.Delay(mUiState.mClipboardDelayMs);
            BringWindowToTop(target);
            SetForegroundWindow(target);
            await Task.Delay(mUiState.mActivationDelayMs);
            SendKeys.Send("^v");
            await Task.Delay(mUiState.mClipboardDelayMs);
            mForm.BringToFront();
            mForm.Show();
            mForm.Activate();
            await Task.Delay(mUiState.mReactivationDelayMs);
            mRichTextBox.Clear();
         }
         finally {
            mScrollableMainPanel.Enabled = true;
         }
      }

      private static async void SendSelectedAsync() {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mForm, nameof(mForm));
         string text = mRichTextBox.SelectedText;
         if (string.IsNullOrEmpty(text)) {
            TimedMessage("There is no selected text to send.", "Nothing To Send", 2500);
            return;
         }
         mScrollableMainPanel.Enabled = false;
         try {
            IntPtr target;
            if (mIsTargetingEnabled) {
               if (!IsValidTargetWindow(mTargetWindow)) {
                  mIsTargetingEnabled = false;
                  mTargetWindow = IntPtr.Zero;
                  mTargetWindowName = string.Empty;
                  if (mTargetedTSMI != null)
                     mTargetedTSMI.Checked = false;
                  UpdateTargetingStatusLabel();
                  TimedMessage("The targeted window no longer exists. Targeting has been turned off.", "Target Lost");
                  return;
               }
               target = mTargetWindow;
            }
            else {
               target = GetMostSuitableWindowAllowedFirst();
               if (target == IntPtr.Zero) {
                  TimedMessage("No suitable target window was found.", "No Target");
                  return;
               }
            }
            ClipboardHelper.TrySetClipboardText(text);
            await Task.Delay(mUiState.mClipboardDelayMs);
            BringWindowToTop(target);
            SetForegroundWindow(target);
            await Task.Delay(mUiState.mActivationDelayMs);
            SendKeys.Send("^v");
            await Task.Delay(mUiState.mClipboardDelayMs);
            mForm.BringToFront();
            mForm.Show();
            mForm.Activate();
            await Task.Delay(mUiState.mReactivationDelayMs);
            mRichTextBox.Clear();
         }
         finally {
            mScrollableMainPanel.Enabled = true;
         }
      }

      private static async void GetAllAsync() {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mForm, nameof(mForm));
         bool success = true;
         string? text = null;
         IntPtr target = IntPtr.Zero, getFrom = IntPtr.Zero;

         mScrollableMainPanel.Enabled = false;
         try {
            if (mIsTargetingEnabled) {
               if (!IsValidTargetWindow(mTargetWindow)) {
                  mIsTargetingEnabled = false;
                  mTargetWindow = IntPtr.Zero;
                  mTargetWindowName = string.Empty;
                  if (mTargetedTSMI != null)
                     mTargetedTSMI.Checked = false;
                  UpdateTargetingStatusLabel();
                  TimedMessage("The targeted window no longer exists. Targeting has been turned off.", "Target Lost");
                  return;
               }
               target = mTargetWindow;
            }
            getFrom = GetNextVisibleWindow(mThisWindow);
            if (getFrom == IntPtr.Zero) {
               TimedMessage("No suitable fetch window was found.", "No Fetch Window");
               success = false;
            }
            else {
               BringWindowToTop(getFrom);
               SetForegroundWindow(getFrom);
               Form? form = Control.FromHandle(getFrom) as Form;
               form?.Activate();
               await Task.Delay(mUiState.mActivationDelayMs);
               SendKeys.Send("^a");
               await Task.Delay(mUiState.mClipboardDelayMs);
               SendKeys.Send("^c");
               await Task.Delay(mUiState.mClipboardDelayMs);
               text = ClipboardHelper.TryGetClipboardText();
               if (string.IsNullOrEmpty(text)) {
                  TimedMessage("The clipboard did not contain any text.", "No Text Retrieved", 2500);
                  return;
               }
            }
            mForm.BringToFront();
            mForm.Show();
            mForm.Activate();
            await Task.Delay(mUiState.mReactivationDelayMs);
            if (success)
               InsertAtCursor(mRichTextBox, text);
         }
         finally {
            mScrollableMainPanel.Enabled = true;
         }
      }

      private static async void GetSelectedAsync() {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
         ThrowIfNull(mForm, nameof(mForm));
         bool success = true;
         string? text = null;
         IntPtr target = IntPtr.Zero, getFrom = IntPtr.Zero;

         mScrollableMainPanel.Enabled = false;
         try {
            if (mIsTargetingEnabled) {
               if (!IsValidTargetWindow(mTargetWindow)) {
                  mIsTargetingEnabled = false;
                  mTargetWindow = IntPtr.Zero;
                  mTargetWindowName = string.Empty;
                  if (mTargetedTSMI != null)
                     mTargetedTSMI.Checked = false;
                  UpdateTargetingStatusLabel();
                  TimedMessage("The targeted window no longer exists. Targeting has been turned off.", "Target Lost");
                  return;
               }
               target = mTargetWindow;
            }
            getFrom = GetNextVisibleWindow(mThisWindow);
            if (getFrom == IntPtr.Zero) {
               TimedMessage("No suitable fetch window was found.", "No Fetch Window");
               success = false;
            }
            else {
               BringWindowToTop(getFrom);
               SetForegroundWindow(getFrom);
               Form? form = Control.FromHandle(getFrom) as Form;
               form?.Activate();
               await Task.Delay(mUiState.mActivationDelayMs);
               SendKeys.Send("^c");
               await Task.Delay(mUiState.mClipboardDelayMs);
               text = ClipboardHelper.TryGetClipboardText();
               await Task.Delay(mUiState.mClipboardDelayMs);
               if (string.IsNullOrEmpty(text)) {
                  TimedMessage("The clipboard did not contain any text.", "No Text Retrieved", 2500);
                  return;
               }
            }
            mForm.BringToFront();
            mForm.Show();
            mForm.Activate();
            await Task.Delay(mUiState.mReactivationDelayMs);
            if (success)
               InsertAtCursor(mRichTextBox, text);
         }
         finally {
            mScrollableMainPanel.Enabled = true;
         }
      }

      private static void InsertAtCursor(RichTextBox? pRichTextBox, string? pText) {
         ThrowIfNull(pRichTextBox, nameof(pRichTextBox));
         if (string.IsNullOrEmpty(pText))
            return;
         int selectionStart = pRichTextBox.SelectionStart;
         pRichTextBox.Text = pRichTextBox.Text.Insert(selectionStart, pText);
         pRichTextBox.SelectionStart = selectionStart + pText.Length;
      }

      private static void Paste(PasteMode? pPasteMode) {
         switch (pPasteMode) {
            case PasteMode.SendAll:
               SendAllAsync();
               break;
            case PasteMode.PasteSelected:
               SendSelectedAsync();
               break;
         }
      }

      private static void Get(PasteMode? pPasteMode) {
         switch (pPasteMode) {
            case PasteMode.GetAll:
               GetAllAsync();
               break;
            case PasteMode.GetSelected:
               GetSelectedAsync();
               break;
         }
      }

      private static IntPtr ResolveTargetWindow() {
         if (mIsTargetingEnabled) {
            if (IsValidTargetWindow(mTargetWindow))
               return Fields.mTargetWindow;
            IntPtr pFallback = GetMostSuitableWindow();
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
         IntPtr pWindow = GetMostSuitableWindow();
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
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         mTargetingLabel.Top = (mMainBottomPanel.Height - mTargetingLabel.Height) / 2;
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
         ThrowIfNull(mVisibilityMenuItem, nameof(mVisibilityMenuItem));
         foreach (ToolStripMenuItem tsmi in mVisibilityMenuItem.DropDownItems.OfType<ToolStripMenuItem>()) {
            if (tsmi.Tag is not double tagOpacity)
               ThrowBadCode($"ToolStripMenuItem {tsmi.Name} in {nameof(UpdateOpacityMenuChecks)} has a null or non-double Tag.");
            else
               tsmi.Checked = Math.Abs(tagOpacity - pOpacity) < 0.001;
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
         ThrowIfNull(mMenuStrip, nameof(mMenuStrip));
         ThrowIfNull(mMinimalTSMI, nameof(mMinimalTSMI));
         ThrowIfNull(mFeaturesTSMI, nameof(mFeaturesTSMI));
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
         ThrowIfNull(mMenuStrip, nameof(mMenuStrip));
         ThrowIfNull(mMinimalTSMI, nameof(mMinimalTSMI));
         ThrowIfNull(mFeaturesTSMI, nameof(mFeaturesTSMI));
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

      private void LayoutMainBottomPanel() {
         ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         ThrowIfNull(mMenuStrip, nameof(mMenuStrip));
         mMainBottomPanel.LayoutControls();
      }
   }
}
