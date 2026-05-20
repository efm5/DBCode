namespace DBCode {
   public sealed partial class MainForm : Form {
      #region main form
      private void MainForm_Load(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         Size savedSize = mUiState.mFormSize;
         Point savedLocation = mUiState.mFormLocation;
         double savedOpacity = mUiState.mFormOpacity;

         if (!savedSize.IsEmpty)
            Size = savedSize;
         if (!savedLocation.IsEmpty) {
            StartPosition = FormStartPosition.Manual;
            Location = savedLocation;
         }
         if (savedOpacity < 0.0 || savedOpacity > 1.0)
            savedOpacity = 1.0;
         Opacity = savedOpacity;
         UpdateOpacityMenuChecks(savedOpacity);
         EnsureWindowFitsMonitor(this);
         ApplyViewMode(ViewMode.Features);
         UpdateTargetingStatusLabel();
         mActiveLayoutable?.LayoutControls();
         Opacity = mUiState.mFormOpacity;
         ActiveControl = mRichTextBox;
         ClientSizeChanged += OnClientSizeChanged;
#if DEBUG
         //ProposeAccelerators(mMenuStrip, [mSendAllButton, mPasteSelectedButton, mRevertButton,
         //mGetAllButton, mGetSelectedButton, mMainBottomPanel.mCancelButton, mMainBottomPanel.mCancelButton]);
         //RadioButtonClusterTestHarness.Show("RadioButton Cluster Test Harness");

         //ScalableCheckBoxClusterTestHarness.Show("Scalable CheckBox Cluster Test Harness");

         //GetString.Show("GetString Test", "Please enter any string to test the GetString harness:", string.Empty, TestGetStringCallback);

         //private void TestGetStringCallback(string? pResult, bool pWasCancelled) {
         //   GetString.Restore();
         //   if (pWasCancelled || pResult is null)
         //      return;
         //   TimedMessage(pResult, "GetString Test Result");
         //}
#endif
      }

      private void MainForm_FormClosing(object? pSender, FormClosingEventArgs pEventArgs) {
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         mUiState.FormBounds = Bounds;
         mUiState.mFormOpacity = Opacity;
         mUiState.mLanguageKind = mCurrentLanguage;
         mUiState.mCurrentThemeName = mCurrentTheme.mName;
         mUiState.Write();
         Settings.Default.Save();
         if (!Directory.Exists(mDataFolder))
            Directory.CreateDirectory(mDataFolder);
         ThemeWriter.SaveAllThemes(mDataFolder, mThemes);
         mThemePanel?.Dispose();
         mMainBottomPanel?.Dispose();
         foreach (Themes.Theme theme in mThemes.OfType<Themes.Theme>()) {
            theme.Dispose();
         }
      }

      internal static void OnClientSizeChanged(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mActiveLayoutable, nameof(mActiveLayoutable));
         mActiveLayoutable.LayoutControls();
      }
      #endregion

      private void ThemeDesign_Click(object? pSender, EventArgs pEventArgs) {
         EnsureThemePanel(ThemeUsage.Design);
      }

      private void Options_Click(object? pSender, EventArgs pEventArgs) {
         EnsureOptionsPanel();
      }

      private void ThemeEditPick_Click(object? pSender, EventArgs pEventArgs) {
         EnsureThemePickerPanel(PickMode.Edit);
      }

      private void ThemeEditCurrent_Click(object? pSender, EventArgs pEventArgs) {
      }

      private void ThemePick_Click(object? pSender, EventArgs pEventArgs) {
         EnsureThemePickerPanel(PickMode.Use);
      }

      private void OnEditorTextChanged(object? pSender, EventArgs pArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         ThrowIfNull(mHighlighterEngine, nameof(mHighlighterEngine));
         mRichTextBox.TextChanged -= OnEditorTextChanged;
         string whitespace = string.Empty;
         if ((mUiState.mWhitespace == (int)Whitespace.Tabs) && (mUiState.mUseTabs)) {
            for (int i = 0; i < (int)mUiState.mSpacesPerTab; i++)
               whitespace += " ";
            if (mRichTextBox.Text.Contains(whitespace)) {
               do {
                  mRichTextBox.Text = mRichTextBox.Text.Replace(whitespace, "\t");
               } while (mRichTextBox.Text.Contains(whitespace));
               mRichTextBox.Text = mRichTextBox.Text.Replace(whitespace, "\t");
            }
         }
         else if ((mUiState.mWhitespace == (int)Whitespace.Spaces) && (mUiState.mUseSpaces)) {
            for (int i = 0; i < (int)mUiState.mSpacesToBecomeTab; i++)
               whitespace += " ";
            if (mRichTextBox.Text.Contains('\t')) {
               do {
                  mRichTextBox.Text = mRichTextBox.Text.Replace("\t", whitespace);
               } while (mRichTextBox.Text.Contains(whitespace));
               mRichTextBox.Text = mRichTextBox.Text.Replace(whitespace, "\t");
            }
         }//else both - do nothing
         mHighlighterEngine.HighlightNow();
         mRichTextBox.TextChanged += OnEditorTextChanged;
      }

      private void TargetedTSMI_Click(object? pSender, EventArgs pEventArgs) {
         if (pSender == null)
            return;
         ToolStripMenuItem? toolStripMenuItem = pSender as ToolStripMenuItem;
         if (toolStripMenuItem == null)
            return;
         if (toolStripMenuItem.Checked)
            EnterTargetedMode();
         else
            EnterUntargetedMode();
      }

      private void RetargetTSMI_Click(object? pSender, EventArgs pEventArgs) {
         if ((mTargetedTSMI == null) || !mTargetedTSMI.Checked) {
            mIsTargetingEnabled = false;
            return;
         }
         EnterTargetedMode();
         UpdateTargetingStatusLabel();
      }

      private void VisibilityTSMI_Click(object? pSender, EventArgs pEventArgs) {
         ToolStripMenuItem? clickedTSMI = pSender as ToolStripMenuItem;
         object? tagObject = clickedTSMI == null ? null : clickedTSMI.Tag;
         double opacityValue = 0.0;
         if ((clickedTSMI == null) || (tagObject == null))
            return;
         if (!double.TryParse(tagObject.ToString(), out opacityValue))
            return;
         Opacity = opacityValue;
         UpdateOpacityMenuChecks(opacityValue);
      }

      private void MinimalTSMI_Click(object? pSender, EventArgs pEventArgs) {
         ApplyViewMode(ViewMode.Minimal);
      }

      private void FeaturesTSMI_Click(object? pSender, EventArgs pEventArgs) {
         ApplyViewMode(ViewMode.Features);
      }

      private void LanguageTSMI_Click(object? pSender, EventArgs pEventArgs) {
         if (pSender == null)
            return;
         ToolStripMenuItem? toolStripMenuItem = pSender as ToolStripMenuItem;
         if (toolStripMenuItem == null)
            return;
         if (!(toolStripMenuItem.Tag is LanguageKind selectedLanguage))
            return;
         mCurrentLanguage = selectedLanguage;
         CheckLanguage();
         ThrowIfNull(mHighlighterEngine, nameof(mHighlighterEngine));
         mHighlighterEngine.SetLanguage(mCurrentLanguage);
         mHighlighterEngine.HighlightNow();
      }

      private void Undo_Click(object? pSender, EventArgs pEventArgs) {
         Undo();
      }

      private void Redo_Click(object? pSender, EventArgs pEventArgs) {
         Redo();
      }

      private void Find_Click(object? pSender, EventArgs pEventArgs) {
         if (mFindPanel != null)
            return;
         FindPanel.ShowMe();
      }

      private void FindNext_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         if (mCurrentFindRecord == null || mCurrentFindRecord.Count == 0) {
            TimedMessage("No active find. Use Find to search first.", "Find Next");
            return;
         }
         mCurrentFindRecord.mPosition++;
         if (mCurrentFindRecord.mPosition >= mCurrentFindRecord.Count) {
            mCurrentFindRecord.mPosition = mCurrentFindRecord.Count - 1;
            TimedMessage("Sorry, there is nothing (going forward) left to find.", "Nothing To Find");
            return;
         }
         mRichTextBox.SelectionStart = mCurrentFindRecord.GetIndex(mCurrentFindRecord.mPosition);
         mRichTextBox.SelectionLength = mCurrentFindRecord.GetLength(mCurrentFindRecord.mPosition);
         mRichTextBox.ScrollToCaret();
      }

      private void FindPrevious_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         if (mCurrentFindRecord == null || mCurrentFindRecord.Count == 0) {
            TimedMessage("No active find. Use Find to search first.", "Find Previous");
            return;
         }
         mCurrentFindRecord.mPosition--;
         if (mCurrentFindRecord.mPosition < 0) {
            mCurrentFindRecord.mPosition = 0;
            TimedMessage("Sorry, there is nothing (going backward) left to find.", "Nothing To Find");
            return;
         }
         mRichTextBox.SelectionStart = mCurrentFindRecord.GetIndex(mCurrentFindRecord.mPosition);
         mRichTextBox.SelectionLength = mCurrentFindRecord.GetLength(mCurrentFindRecord.mPosition);
         mRichTextBox.ScrollToCaret();
      }

      private void Replace_Click(object? pSender, EventArgs pEventArgs) {
         if (mSearchReplacePanel != null)
            return;
         SearchReplacePanel.ShowMe();
      }

      private void Copy_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         mRichTextBox.Copy();
      }

      private void Cut_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         mRichTextBox.Cut();
      }

      private void Delete_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         if (mRichTextBox.SelectionLength > 0)
            mRichTextBox.SelectedText = string.Empty;
      }

      private void Paste_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         mRichTextBox.Paste();
      }

      private void SelectAll_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         mRichTextBox.SelectAll();
      }

      private void SelectNone_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         mRichTextBox.SelectionLength = 0;
      }

      private void GoTo_Click(object? pSender, EventArgs pEventArgs) { // go to line, ignoring word wrap
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         int lineCount = mRichTextBox.Lines.Length;
         if (lineCount > 1) {
            int currentLine = mRichTextBox.GetLineFromCharIndex(mRichTextBox.SelectionStart) + 1; // convert to one-based
            GetInteger.ShowMe("Go To Line", $"Enter line number (1 – {lineCount}):",
               currentLine, 1, lineCount, GoToCallback);
         }
      }

      private void GoToCallback(int? pResult, bool pWasCancelled) {
         GetInteger.Restore();
         if (pWasCancelled || pResult == null)
            return;
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         int charIndex = mRichTextBox.GetFirstCharIndexFromLine(pResult.Value - 1); // convert to zero-based
         mRichTextBox.SelectionStart = charIndex;
         mRichTextBox.SelectionLength = 0;
         mRichTextBox.ScrollToCaret();
         mRichTextBox.Focus();
         ActiveControl = mRichTextBox;
      }

      private void TrimToBeginning_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         if (mRichTextBox.SelectionStart == 0)
            return;
         mRichTextBox.Text = mRichTextBox.Text[mRichTextBox.SelectionStart..];
         mRichTextBox.SelectionStart = 0;
         mRichTextBox.SelectionLength = 0;
      }

      private void TrimToEnd_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         int start = mRichTextBox.SelectionStart;
         if (start >= mRichTextBox.TextLength)
            return;
         mRichTextBox.Text = mRichTextBox.Text[..start];
         mRichTextBox.SelectionStart = mRichTextBox.TextLength;
         mRichTextBox.SelectionLength = 0;
      }

      private void CopyAll_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         Clipboard.SetText(mRichTextBox.Text);
      }

      private void CopyToBeginning_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         int end = mRichTextBox.SelectionStart;
         if (end == 0)
            return;
         Clipboard.SetText(mRichTextBox.Text[..end]);
      }

      private void CopyToEnd_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         int start = mRichTextBox.SelectionStart;
         if (start >= mRichTextBox.TextLength)
            return;
         Clipboard.SetText(mRichTextBox.Text[start..]);
      }

      private void WordWrap_Click(object? pSender, EventArgs pEventArgs) { // toggle word wrap
         ThrowIfNull(mRichTextBox, nameof(mRichTextBox));
         mRichTextBox.WordWrap = !mRichTextBox.WordWrap;
         mRichTextBox.ScrollBars = mRichTextBox.WordWrap
            ? RichTextBoxScrollBars.Vertical
            : RichTextBoxScrollBars.Both;
      }

      public static void Help_Click(object? pSender, EventArgs pEventArgs) {
         HelpContext context = HelpContext.Main;
         string? anchor = "";
         if (pSender is Control control) {
            if (control.Tag is HelpTag tag) {
               context = tag.Context;
               anchor = tag.Anchor;
            }
         }
         else if (pSender is ToolStripItem item) {
            if (item.Tag is HelpTag tag) {
               context = tag.Context;
               anchor = tag.Anchor;
            }
         }
         else
            return;
         GetHelp(context, anchor);
      }

      private void TransMove_Click(object? pSender, EventArgs pEventArgs) {
         Button? button = pSender as Button;
         ThrowIfNull(button, nameof(button));
         PasteMode? pasteMode = button.Tag as PasteMode?;

         switch (pasteMode) {
            case PasteMode.SendAll:
            case PasteMode.PasteSelected:
               Paste(pasteMode);
               break;
            case PasteMode.GetAll:
            case PasteMode.GetSelected:
               Get(pasteMode);
               break;
         }
         TopMost = true;//efm5 make it pop to the top but do not force it to stay there
         TopMost = false;
      }

      private void RevertButton_Click(object? pSender, EventArgs pEventArgs) {
         ApplyViewMode(ViewMode.Features);
      }

      private void ExitButton_Click(object? pSender, EventArgs pEventArgs) {
         Close();
      }

      #region coding menu
      private void CFamilyAddDoubleTSMI_Click(object? pSender, EventArgs pE) {
         CFamilyAddDouble();
      }

      private void CSharpAddTripleTSMI_Click(object? pSender, EventArgs pE) {
         CSharpAddTriple();
      }

      private void CFamilyBlockCommentTSMI_Click(object? pSender, EventArgs pE) {
         CFamilyBlockComment();
      }

      private void CFamilyCommentOutTSMI_Click(object? pSender, EventArgs pE) {
         CFamilyCommentOut();
      }

      private void CFamilyCommentRemoveTSMI_Click(object? pSender, EventArgs pE) {
         CFamilyCommentRemove();
      }

      private void CFamilyWrapCommentTSMI_Click(object? pSender, EventArgs pE) {
         CFamilyWrapComment();
      }

      private void CSharpAddNotImplementedTSMI_Click(object? pSender, EventArgs pE) {
         CSharpAddNotImplemented();
      }

      private void CSharpReverseEqualityTSMI_Click(object? pSender, EventArgs pE) {
         CSharpReverseEquality();
      }

      private void CSharpExpressionBodiedMethodTSMI_Click(object? pSender, EventArgs pE) =>
         CSharpExpressionBodiedMethod();
      private void ConvertVBToCSharpCommentTSMI_Click(object? pSender, EventArgs pE) =>
         ConvertVBToCSharpComment();

      #region F# coding menu
      private void FSharpBlockCommentTSMI_Click(object? pSender, EventArgs pE) => FSharpBlockComment();
      private void FSharpAddCommentTSMI_Click(object? pSender, EventArgs pE) => FSharpAddComment();
      private void FSharpAddMutableTSMI_Click(object? pSender, EventArgs pE) => FSharpAddMutable();
      private void FSharpAddIgnoreTSMI_Click(object? pSender, EventArgs pE) => FSharpAddIgnore();
      private void FSharpConvertNullNoneTSMI_Click(object? pSender, EventArgs pE) => FSharpConvertNullNone();
      #endregion

      #region basic coding menu
      private void BasicAddSingleQuoteTSMI_Click(object? pSender, EventArgs pE) =>
         BasicAddSingleQuote();
      private void BasicConvertCSharpToVBCommentTSMI_Click(object? pSender, EventArgs pE) =>
         ConvertCSharpToVBComment();
      private void BasicReverseEqualityTSMI_Click(object? pSender, EventArgs pE) =>
         ReverseEqualityVB();
      private void BasicRemoveLineContinuationTSMI_Click(object? pSender, EventArgs pE) =>
         RemoveLineContinuation();
      private void BasicConvertBooleansTSMI_Click(object? pSender, EventArgs pE) =>
         ConvertBooleans();
      private void BasicConvertNullNothingTSMI_Click(object? pSender, EventArgs pE) =>
         ConvertNullNothing();
      private void BasicConvertLogicalOperatorsTSMI_Click(object? pSender, EventArgs pE) =>
         ConvertLogicalOperators();
      #endregion

      #region JSON coding menu
      private void JSONAddCommentTSMI_Click(object? pSender, EventArgs pE) => JSONAddComment();
      private void JSONBlockCommentTSMI_Click(object? pSender, EventArgs pE) => JSONBlockComment();
      private void JSONRemoveCommentTSMI_Click(object? pSender, EventArgs pE) => JSONRemoveComment();
      private void JSONToggleQuotesTSMI_Click(object? pSender, EventArgs pE) => JSONToggleQuotes();
      private void JSONEscapeStringTSMI_Click(object? pSender, EventArgs pE) => JSONEscapeString();
      private void JSONRemoveTrailingCommasTSMI_Click(object? pSender, EventArgs pE) => JSONRemoveTrailingCommas();
      #endregion

      #region XML coding menu
      private void XMLCommentTSMI_Click(object? pSender, EventArgs pE) => XMLComment();
      private void XMLRemoveCommentTSMI_Click(object? pSender, EventArgs pE) => XMLRemoveComment();
      private void XMLEscapeEntitiesTSMI_Click(object? pSender, EventArgs pE) => XMLEscapeEntities();
      private void XMLWrapCDataTSMI_Click(object? pSender, EventArgs pE) => XMLWrapCData();
      private void XMLToggleSelfCloseTSMI_Click(object? pSender, EventArgs pE) => XMLToggleSelfClose();
      #endregion

      #region PowerShell coding menu
      private void PowerShellAddCommentTSMI_Click(object? pSender, EventArgs pE) => PowerShellAddComment();
      private void PowerShellBlockCommentTSMI_Click(object? pSender, EventArgs pE) => PowerShellBlockComment();
      private void PowerShellRemoveCommentTSMI_Click(object? pSender, EventArgs pE) => PowerShellRemoveComment();
      private void PowerShellToggleBooleanTSMI_Click(object? pSender, EventArgs pE) => PowerShellToggleBoolean();
      private void PowerShellToggleQuotesTSMI_Click(object? pSender, EventArgs pE) => PowerShellToggleQuotes();
      #endregion

      #region Batch coding menu
      private void BatchAddCommentTSMI_Click(object? pSender, EventArgs pE) => BatchAddComment();
      private void BatchAddColonCommentTSMI_Click(object? pSender, EventArgs pE) => BatchAddColonComment();
      private void BatchRemoveCommentTSMI_Click(object? pSender, EventArgs pE) => BatchRemoveComment();
      private void BatchToggleCommentStyleTSMI_Click(object? pSender, EventArgs pE) => BatchToggleCommentStyle();
      #endregion

      #region SQL coding menu
      private void SQLAddCommentTSMI_Click(object? pSender, EventArgs pE) => SQLAddComment();
      private void SQLBlockCommentTSMI_Click(object? pSender, EventArgs pE) => SQLBlockComment();
      private void SQLRemoveCommentTSMI_Click(object? pSender, EventArgs pE) => SQLRemoveComment();
      private void SQLToggleNullCheckTSMI_Click(object? pSender, EventArgs pE) => SQLToggleNullCheck();
      private void SQLToggleBooleanTSMI_Click(object? pSender, EventArgs pE) => SQLToggleBoolean();
      #endregion

      #region Markdown coding menu
      private void MarkdownCommentTSMI_Click(object? pSender, EventArgs pE) => MarkdownComment();
      private void MarkdownBoldTSMI_Click(object? pSender, EventArgs pE) => MarkdownBold();
      private void MarkdownItalicTSMI_Click(object? pSender, EventArgs pE) => MarkdownItalic();
      private void MarkdownBoldItalicTSMI_Click(object? pSender, EventArgs pE) => MarkdownBoldItalic();
      private void MarkdownStrikethroughTSMI_Click(object? pSender, EventArgs pE) => MarkdownStrikethrough();
      private void MarkdownCodeTSMI_Click(object? pSender, EventArgs pE) => MarkdownCode();
      private void MarkdownCodeBlockTSMI_Click(object? pSender, EventArgs pE) => MarkdownCodeBlock();
      #endregion

      #region Python coding menu
      private void PythonAddCommentTSMI_Click(object? pSender, EventArgs pE) => PythonAddComment();
      private void PythonBlockCommentTSMI_Click(object? pSender, EventArgs pE) => PythonBlockComment();
      private void PythonRemoveCommentTSMI_Click(object? pSender, EventArgs pE) => PythonRemoveComment();
      private void PythonToggleBooleanTSMI_Click(object? pSender, EventArgs pE) => PythonToggleBoolean();
      private void PythonConvertNullNoneTSMI_Click(object? pSender, EventArgs pE) => PythonConvertNullNone();
      #endregion

      #region CSS coding menu
      private void CSSBlockCommentTSMI_Click(object? pSender, EventArgs pE) => CSSBlockComment();
      private void CSSCommentRemoveTSMI_Click(object? pSender, EventArgs pE) => CSSCommentRemove();
      private void CSSToggleImportantTSMI_Click(object? pSender, EventArgs pE) => CSSToggleImportant();
      private void CSSConvertColorFormatTSMI_Click(object? pSender, EventArgs pE) => CSSConvertColorFormat();
      #endregion

      #region HTML coding menu
      private void HTMLCommentTSMI_Click(object? pSender, EventArgs pE) => HTMLComment();
      private void HTMLBoldTSMI_Click(object? pSender, EventArgs pE) => HTMLBold();
      private void HTMLItalicTSMI_Click(object? pSender, EventArgs pE) => HTMLItalic();
      private void HTMLUnderlineTSMI_Click(object? pSender, EventArgs pE) => HTMLUnderline();
      private void HTMLStrikethroughTSMI_Click(object? pSender, EventArgs pE) => HTMLStrikethrough();
      private void HTMLBigTSMI_Click(object? pSender, EventArgs pE) => HTMLBig();
      private void HTMLBigBigTSMI_Click(object? pSender, EventArgs pE) => HTMLBigBig();
      private void HTMLSmallTSMI_Click(object? pSender, EventArgs pE) => HTMLSmall();
      private void HTMLSmallSmallTSMI_Click(object? pSender, EventArgs pE) => HTMLSmallSmall();
      private void HTMLMarkTSMI_Click(object? pSender, EventArgs pE) => HTMLMark();
      private void HTMLSuperscriptTSMI_Click(object? pSender, EventArgs pE) => HTMLSuperscript();
      private void HTMLSubscriptTSMI_Click(object? pSender, EventArgs pE) => HTMLSubscript();
      private void HTMLCodeTSMI_Click(object? pSender, EventArgs pE) => HTMLCode();
      private void HTMLPreformattedTSMI_Click(object? pSender, EventArgs pE) => HTMLPreformatted();
      private void HTMLColorizeTSMI_Click(object? pSender, EventArgs pE) => HTMLColorize();
      #endregion
      #endregion
   }
}
