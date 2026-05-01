using static DBCode.Themes.ThemeRegistry;

namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePanel : Panel {
         protected override void OnHandleCreated(EventArgs pEventArgs) {
            base.OnHandleCreated(pEventArgs);
            SuspendLayout();
            mThemeBottomPanel.mCancelButton!.Click += CancelButton_Click;
            mThemeBottomPanel.mHelpButton!.Click += MainForm.Help_Click;
            mApplyButton.Click += ApplyButton_Click;
            mNewButton.Click += NewButton_Click;
            mCloneButton.Click += CloneButton_Click;
            mExampleScrollPanel!.ClientSizeChanged += ExampleScrollPanel_ClientSizeChanged;
            Controls.AddRange([mPrimaryTabControl, mThemeBottomPanel, mThemesHeaderCluster]);
            ResumeLayout(false);
            BeginInvoke(new Action(() => { LayoutControls(); }));
         }

         private void PrimaryTabControl_DrawItem(object? pSender, DrawItemEventArgs pArgs) {
            DrawTabControlItem(mPrimaryTabControl, pArgs);
         }

         private void IncludeExcludeTabControl_DrawItem(object? pSender, DrawItemEventArgs pArgs) {
            DrawTabControlItem(mIncludeExcludeTabControl, pArgs);
         }

         private void HighlightTabControl_DrawItem(object? pSender, DrawItemEventArgs pArgs) {
            DrawTabControlItem(mHighlightTabControl, pArgs);
         }

         private void PrimaryTabControl_SelectedIndexChanged(object? pSender, EventArgs pArgs) {
            mUiState.mThemePrimaryTabPageIndex = mPrimaryTabControl.SelectedIndex;
            if (mPrimaryTabControl.SelectedIndex == (int)PrimaryTabPageUsage.Examples)
               HighlightAllExampleBoxes();
         }

         private void IncludeExcludeTabControl_SelectedIndexChanged(object? pSender, EventArgs pArgs) {
            mUiState.mThemeTargetingTabIndexIndex = mIncludeExcludeTabControl.SelectedIndex;
         }

         private void HighlightTabControl_SelectedIndexChanged(object? pSender, EventArgs pArgs) {
            mUiState.mThemeHighlightTabPageIndex = mHighlightTabControl.SelectedIndex;
         }

         private void OnExampleTextChanged(object? pSender, EventArgs pArgs) {
            RichTextBox? box = pSender as RichTextBox;
            if (box?.Tag is not LanguageKind language)
               return;
            box.TextChanged -= OnExampleTextChanged;
            HighlightExampleBox(box, language);
            box.TextChanged += OnExampleTextChanged;
         }

         private void ExampleScrollPanel_ClientSizeChanged(object? pSender, EventArgs pArgs) {
            SizeExamplesContainer();
         }

         private void NewButton_Click(object? pSender, EventArgs pArgs) {
            //DEBUG efm5 2026 04 7 reset temporary theme to defaults
         }

         private void CloneButton_Click(object? pSender, EventArgs pArgs) {
            //DEBUG efm5 2026 04 7 clone current theme to temporary theme
         }

         private void ApplyButton_Click(object? pSender, EventArgs pArgs) {
            GetString.Show(
               "Enter Theme Name",
               "Please enter a name for the new theme:",
               mTemporaryTheme.mName,
               ThemeApplyCallback
            );
         }

         private void ThemeApplyCallback(string? pResult, bool pWasCancelled) {
            GetString.Restore();
            if (pWasCancelled || string.IsNullOrWhiteSpace(pResult))
               return;
            if (!ThemeNameIsUnique(pResult)) {
               GetString.Show("Theme Name Collision",
                  $"A theme named '{pResult}' already exists. Please enter a different name:",
                  pResult, ThemeApplyCallback);
               return;
            }
            Theme newTheme = mTemporaryTheme.Clone(pResult);
            AddTheme(newTheme);
            SetCurrentThemeName(pResult);
            mCurrentTheme = newTheme;
            mThemeIsDirty = true;
            CloseThemePanel();
         }

         private void CancelButton_Click(object? pSender, EventArgs pArgs) {
            CloseThemePanel();
         }

         private void OnColorSwatchClicked(object? pSender, ColorSwatchUsage pUsage) {
            LabeledButtonColorSwatchCluster? swatch = pSender as LabeledButtonColorSwatchCluster;
            if (swatch == null)
               return;
            EnsureColorPickerPanel(mTemporaryTheme, (ColorSwatchUsage)pUsage, swatch.GetColor());
         }

         private void OnSyntaxColorSwatchClicked(object? pSender, TokenKind pTokenKind) {
            LabeledButtonColorSwatchCluster? swatch = pSender as LabeledButtonColorSwatchCluster;
            if (swatch == null)
               return;
            // efm5 TODO: wire to color picker for syntax colors
         }

         private void OnFontButtonClicked(object? pSender, EventArgs pArgs) {
            Button? button = pSender as Button;
            if (button?.Tag is not FontUsage usage)
               return;
            EnsureFontPickerPanel(mTemporaryTheme, usage, mTemporaryTheme.mFonts[(int)usage]);
         }
      }
   }
}
