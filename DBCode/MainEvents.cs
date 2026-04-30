namespace DBCode {
   public sealed partial class MainForm : Form {
      #region main form
      private void MainForm_Load(object? pSender, EventArgs pEventArgs) {
         Size savedSize = mUiState.mFormSize;
         Point savedLocation = mUiState.mFormLocation;
         double savedOpacity = mUiState.mFormOpacity;
         mThemePrimaryTabPageIndex = mUiState.mThemePrimaryTabPageIndex;
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
         EnsureWindowFitsMonitor(this, true);
         ApplyViewMode(ViewMode.Features);
      }

      private void MainForm_Shown(object? pSender, EventArgs pEventArgs) {
         //LayoutBottomPanel();
         UpdateTargetingStatusLabel();
         mVersionLabel?.Top = (mMainBottomPanel!.Height - mVersionLabel.Height) / 2;
      }

      private void MainForm_FormClosing(object? pSender, FormClosingEventArgs pEventArgs) {
         mUiState.mFormOpacity = Opacity;
         mUiState.mFormSize = Size;
         mUiState.mFormLocation = Location;
         mUiState.mThemePrimaryTabPageIndex = mThemePrimaryTabPageIndex;
         mUiState.mThemeHighlightTabPageIndex = mThemeHighlightTabPageIndex;
         mUiState.WriteToSettings();
         if (!mFirstTheme) {
            Settings.Default.ThemeLocation = mThemeBounds.Location;
            Settings.Default.ThemeSize = mThemeBounds.Size;
         }
         Settings.Default.Save();
         mThemePanel?.Dispose();
         mMainBottomPanel?.Dispose();
         foreach (Themes.Theme theme in mThemes.OfType<Themes.Theme>()) {
            theme.Dispose();
         }
      }

      protected override void OnClientSizeChanged(EventArgs pEventArgs) {
         base.OnClientSizeChanged(pEventArgs);
         if (mMainBottomPanel != null && mRichTextBox != null && mMenuStrip != null)
            LayoutMainBottomPanel();
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
