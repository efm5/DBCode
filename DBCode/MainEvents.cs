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

   }
}
