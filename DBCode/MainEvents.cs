namespace DBCode {
   public sealed partial class MainForm : Form {
      #region main form
      private void MainForm_Load(object? pSender, EventArgs pEventArgs) {
         Size savedSize = mUiState.mFormSize;
         Point savedLocation = mUiState.mFormLocation;
         double savedOpacity = mUiState.mFormOpacity;

         mThemePrimaryTabPageIndex = mUiState.mThemePrimaryTabPageIndex;
         mThemeTargetingTabIndexIndex = mUiState.mThemeTargetingTabIndexIndex;
         mThemeHighlightTabPageIndex = mUiState.mThemeHighlightTabPageIndex;
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
         ClientSizeChanged += OnClientSizeChanged;
         GetString.Show("GetString Test", "Please enter any string to test the GetString harness:", string.Empty, TestGetStringCallback);
      }

      private void TestGetStringCallback(string? pResult, bool pWasCancelled) {
         GetString.Restore();
         if (pWasCancelled || pResult is null)
            return;
         TimedMessage(pResult, "GetString Test Result");
      }

      private void MainForm_FormClosing(object? pSender, FormClosingEventArgs pEventArgs) {
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         mUiState.FormBounds = Bounds;
         mUiState.mFormOpacity = Opacity;
         mUiState.mThemePrimaryTabPageIndex = mThemePrimaryTabPageIndex;
         mUiState.mThemeTargetingTabIndexIndex = mThemeTargetingTabIndexIndex;
         mUiState.mThemeHighlightTabPageIndex = mThemeHighlightTabPageIndex;
         mUiState.mLanguageKind = mCurrentLanguage;
         mUiState.mCurrentThemeName = mCurrentTheme.mName;

         mUiState.WriteToSettings();
         Settings.Default.Save();
         mThemePanel?.Dispose();
         mMainBottomPanel?.Dispose();
         foreach (Themes.Theme theme in mThemes.OfType<Themes.Theme>()) {
            theme.Dispose();
         }
      }

      internal static void OnClientSizeChanged(object? pSender, EventArgs pEventArgs) {
         mActiveLayoutable?.LayoutControls();
      }
      #endregion

      private void ThemeDesign_Click(object? pSender, EventArgs pEventArgs) {
         EnsureThemePanel(ThemeUsage.Design);
      }

      private void ThemeEdit_Click(object? pSender, EventArgs pEventArgs) {
         EnsureThemePickerPanel();
      }

      private void ThemePick_Click(object? pSender, EventArgs pEventArgs) {
         EnsureThemePickerPanel();
      }

      private void OnEditorTextChanged(object? pSender, EventArgs pArgs) {
         mRichTextBox!.TextChanged -= OnEditorTextChanged;
         //mTimer?.Stop();
         mHighlighterEngine!.HighlightNow();
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

      private void ReturnToTopTSMI_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(mReturnToTopTSMI, nameof(mReturnToTopTSMI));
         mReturnToTop = mReturnToTopTSMI.Checked;
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
         mHighlighterEngine!.SetLanguage(mCurrentLanguage);
         mHighlighterEngine.HighlightNow();
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
         if (button == null)
            return;
         PasteMode pasteMode = button == mSendAllButton ? PasteMode.SendAll : PasteMode.PasteSelected;

         Paste(pasteMode);
         if (mReturnToTop) {
            TopMost = true;
            TopMost = false;
         }
      }

      private void RevertButton_Click(object? pSender, EventArgs pEventArgs) {
         ApplyViewMode(ViewMode.Features);
      }

      private void ExitButton_Click(object? pSender, EventArgs pEventArgs) {
         Close();
      }
   }
}
