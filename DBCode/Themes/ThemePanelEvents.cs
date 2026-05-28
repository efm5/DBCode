using static DBCode.Themes.ThemeRegistry;

namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePanel : ScrollablePanel {
         protected override void OnHandleCreated(EventArgs pEventArgs) {
            base.OnHandleCreated(pEventArgs);
            SuspendLayout();
            ThrowIfNull(mBottomPanel.mCancelButton, nameof(mBottomPanel.mCancelButton));
            ThrowIfNull(mBottomPanel.mHelpButton, nameof(mBottomPanel.mHelpButton));
            mBottomPanel.mCancelButton.Click += CancelButton_Click;
            mBottomPanel.mHelpButton.Click += MainForm.Help_Click;
            mApplyButton.Click += ApplyButton_Click;
            mNewButton.Click += NewButton_Click;
            mCloneButton.Click += CloneButton_Click;
            ThrowIfNull(mExampleScrollPanel, nameof(mExampleScrollPanel));
            mExampleScrollPanel.ClientSizeChanged += ExampleScrollPanel_ClientSizeChanged;
            Controls.AddRange([mPrimaryTabControl, mBottomPanel, mTitleLabel]);
            ResumeLayout(false);
         }

         private void PrimaryTabControl_DrawItem(object? pSender, DrawItemEventArgs pArgs) {
            MainForm.DrawTabControlItem(mPrimaryTabControl, pArgs, mTemporaryTheme);
         }

         private void HighlightTabControl_DrawItem(object? pSender, DrawItemEventArgs pArgs) {
            MainForm.DrawTabControlItem(mHighlightTabControl, pArgs, mTemporaryTheme);
         }

         private void PrimaryTabControl_SelectedIndexChanged(object? pSender, EventArgs pArgs) {
            mUiState.mThemePrimaryTabPageIndex = mPrimaryTabControl.SelectedIndex;
            if (mPrimaryTabControl.SelectedIndex == (int)PrimaryTabPageUsage.Examples)
               HighlightAllExampleBoxes();
            UpdateActiveScrollablePanel();
         }

         private void HighlightTabControl_SelectedIndexChanged(object? pSender, EventArgs pArgs) {
            mUiState.mThemeHighlightTabPageIndex = mHighlightTabControl.SelectedIndex;
            UpdateActiveScrollablePanel();
         }

         private void OnExampleTextChanged(object? pSender, EventArgs pArgs) {
            RichTextBox? box = pSender as RichTextBox;
            ThrowIfNull(box, nameof(box));
            if (box.Tag is not LanguageKind language)
               return;
            box.TextChanged -= OnExampleTextChanged;
            HighlightExampleBox(box, language);
            box.TextChanged += OnExampleTextChanged;
         }

         private void ExampleScrollPanel_ClientSizeChanged(object? pSender, EventArgs pArgs) {
            SizeExamplesContainer();
         }

         private void NewButton_Click(object? pSender, EventArgs pArgs) {
            if (mTemporaryTheme != null)
               mTemporaryTheme.Dispose();
            mTemporaryTheme = new Theme("new");
         }

         private void CloneButton_Click(object? pSender, EventArgs pArgs) {
            ThrowIfNull(mCurrentTheme, nameof(CloneButton_Click));
            if (mTemporaryTheme != null)
               mTemporaryTheme.Dispose();
            mTemporaryTheme = mCurrentTheme.Clone("new");
         }

         private void ApplyButton_Click(object? pSender, EventArgs pArgs) {
            GetString.ShowMe(
               "Enter Theme Name",
               "Please enter a name for the new theme:",
               mTemporaryTheme.mName,
               ThemeApplyCallback
            );
         }

         private void ThemeApplyCallback(string? pResult, bool pWasCancelled) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            GetString.Restore();
            if (pWasCancelled || string.IsNullOrWhiteSpace(pResult))
               return;
            if (!ThemeNameIsUnique(pResult)) {
               GetString.ShowMe("Theme Name Collision",
                  $"A theme named \"{pResult}\" already exists. Please enter a different name:",
                  pResult, ThemeApplyCallback);
               return;
            }
            Theme newTheme = mTemporaryTheme.Clone(pResult);
            AddTheme(newTheme);
            SetCurrentThemeName(pResult);
            mCurrentTheme.Dispose(); // release GDI fonts from the previous current theme
            mCurrentTheme = newTheme;
            mThemeIsDirty = true;
            CloseThemePanel(); // RestoreFromThemePanel → mForm.ApplyTheme() picks up mCurrentTheme
         }

         private void CancelButton_Click(object? pSender, EventArgs pArgs) {
            CloseThemePanel();
         }

         private void OnColorSwatchClicked(LabeledButtonColorSwatchCluster pSender) {
            if (pSender.Tag is not ColorSwatchUsage usage)
               ThrowBadCode($"Tag was not a ColorSwatchUsage in {nameof(OnColorSwatchClicked)}.");
            else
               EnsureColorPickerPanel(mTemporaryTheme, usage, pSender.GetColor());
         }

         private void OnSyntaxColorSwatchClicked(LabeledButtonColorSwatchCluster pSender) {
            if (pSender.Tag is not (TokenKind tokenKind, LanguageKind languageKind))
               ThrowBadCode($"Tag was not a (TokenKind, LanguageKind) in {nameof(OnSyntaxColorSwatchClicked)}.");
            else
               EnsureColorPickerPanel(mTemporaryTheme, tokenKind, languageKind, pSender.GetColor());
         }

         private void OnFontButtonClicked(LabeledButtonTextBoxCluster pSender) {
            if (pSender.Tag is not FontUsage usage)
               ThrowBadCode($"Tag was not a FontUsage in {nameof(OnFontButtonClicked)}.");
            else
               EnsureFontPickerPanel(mTemporaryTheme, usage, mTemporaryTheme.mFonts[(int)usage]);
         }
      }
   }
}
