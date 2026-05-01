namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePanel : Panel {
         public void SetThemeUsage(ThemeUsage pThemeUsage) {
            mThemeUsage = pThemeUsage;
         }

         public Size WantedSize() {
            int maxWidth = 0, maxHeight = 0, pageWidth, pageHeight, wantedWidth, wantedHeight;
            int primaryTabStripHeight = mPrimaryTabControl.TabCount > 0 ? mPrimaryTabControl.GetTabRect(0).Height + 4 : 25;
            int highlightTabStripHeight = mHighlightTabControl.TabCount > 0 ? mHighlightTabControl.GetTabRect(0).Height + 4 : 25;
            foreach (Panel panel in mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Interface].Controls.OfType<Panel>()) {
               Control? rightmost = Rightmost(ControlCollectionAsList(panel.Controls));
               Control? bottommost = Bottommost(ControlCollectionAsList(panel.Controls));
               pageWidth = rightmost != null ? rightmost.Right : 300;
               pageHeight = bottommost != null ? bottommost.Bottom : 300;
               if (pageWidth > maxWidth)
                  maxWidth = pageWidth;
               if (pageHeight > maxHeight)
                  maxHeight = pageHeight;
            }
            foreach (TabPage tabPage in mHighlightTabControl.TabPages) {
               foreach (Panel panel in tabPage.Controls.OfType<Panel>()) {
                  Control? rightmost = Rightmost(ControlCollectionAsList(panel.Controls));
                  Control? bottommost = Bottommost(ControlCollectionAsList(panel.Controls));
                  pageWidth = rightmost != null ? rightmost.Right : 300;
                  pageHeight = bottommost != null ? bottommost.Bottom : 300;
                  if (pageWidth > maxWidth)
                     maxWidth = pageWidth;
                  if (pageHeight > maxHeight)
                     maxHeight = pageHeight;
               }
            }
            wantedWidth = maxWidth + mEm3 + SystemInformation.VerticalScrollBarWidth;
            wantedHeight = maxHeight + SystemInformation.HorizontalScrollBarHeight + mThemesHeaderCluster.Height +
               mThemeBottomPanel.Height + primaryTabStripHeight + highlightTabStripHeight + mEm3;
            if (wantedWidth < 300)
               wantedWidth = 300;
            if (wantedHeight < 300)
               wantedHeight = 300;
            return new Size(wantedWidth, wantedHeight);
         }

         public void LayoutControls() {
            SuspendLayout();
            ThrowIfNull(mForm, nameof(mForm));
            int savedIndex = mPrimaryTabControl.SelectedIndex;
            mPrimaryTabControl.SelectedIndex = (int)PrimaryTabPageUsage.Interface;
            mPrimaryTabControl.SelectedIndex = (int)PrimaryTabPageUsage.Color;
            mPrimaryTabControl.SelectedIndex = (int)PrimaryTabPageUsage.Examples;
            mPrimaryTabControl.SelectedIndex = (int)PrimaryTabPageUsage.Targeting;
            mPrimaryTabControl.SelectedIndex = savedIndex;
            savedIndex = mHighlightTabControl.SelectedIndex;
            for (int i = 0; i < mHighlightTabControl.TabPages.Count; i++)
               mHighlightTabControl.SelectedIndex = i;
            mHighlightTabControl.SelectedIndex = savedIndex;
            savedIndex = mIncludeExcludeTabControl.SelectedIndex;
            for (int i = 0; i < mIncludeExcludeTabControl.TabPages.Count; i++)
               mIncludeExcludeTabControl.SelectedIndex = i;
            mIncludeExcludeTabControl.SelectedIndex = savedIndex;
            ApplyTheme(mTemporaryTheme);
            LayoutClustersAndContainers();
            SizePanel(mExamplesContainer, mIndent, false);
            mExamplesContainer.Height += mEmHalf;
            mExamplesContainer.Location = new Point(mIndent, mExampleStatusStrip.Bottom + mEmHalf);
            mThemeBottomPanel.LayoutControls();
            mPrimaryTabControl.Location = new Point(mIndent, mThemesHeaderCluster.Bottom + mEmHalf);
            mPrimaryTabControl.Width = ClientSize.Width - (2 * mIndent);
            mPrimaryTabControl.Height = ClientSize.Height - (mThemeBottomPanel.Height + mThemesHeaderCluster.Height + mEm);
            mPrimaryTabControl.Anchor = mAnchorTopLeftBottomRight;
            ResumeLayout(true);
         }

         private void SizeExamplesContainer() {
            mExamplesContainer.Width = mExampleScrollPanel!.ClientSize.Width - mRightPad;
            mExamplesContainer.LayoutClusters();
            SizePanel(mExamplesContainer, mIndent, false);
            mExamplesContainer.Height += mEmHalf;
            mExamplesContainer.Location = new Point(mIndent, mExampleStatusStrip.Bottom + mEmHalf);
         }

         private void LayoutClustersAndContainers() {
            Point location = GetGroupBoxFirstLineOffset(mExampleGroupBox);
            int x = location.X, y = location.Y;
            int tallest = Tallest([mExampleButton, mExampleCheckBox,
               mExampleRichTextBox, mExampleRadioButton]);
            mExampleButton.Location = new Point(x, y + (tallest - mExampleButton.Height) / 2);
            x += mExampleButton.Width + mEm;
            mExampleCheckBox.Location = new Point(x, y + (tallest - mExampleCheckBox.Height) / 2);
            x += mExampleCheckBox.Width + mEm;
            mExampleRichTextBox.Location = new Point(x, y);
            x += mExampleRichTextBox.Width + mEm;
            mExampleRadioButton.Location = new Point(x, y + (tallest - mExampleRadioButton.Height) / 2);
            x += mExampleRadioButton.Width + mEm;
            SizeTextBoxToFitString(out SizeF pOSize, mExampleRichTextBox);
            mExampleRichTextBox.Size = LayoutHelpers.SizeFromSizeF(pOSize);
            SizeGroupBox(mExampleGroupBox);
            foreach (List<BaseCluster> clusterBases in mAllClusters.OfType<List<BaseCluster>>()) {
               foreach (BaseCluster cluster in clusterBases.OfType<BaseCluster>()) {
                  cluster.LayoutCluster();
                  SizePanel(cluster);
               }
            }
            foreach (ClusterContainer clusterContainer in mClusterContainers.OfType<ClusterContainer>()) {
               Panel parent = clusterContainer.mPanelParent as Panel ?? throw new InvalidOperationException(
                  $"Expected parent of ClusterContainer {clusterContainer.Name} to be a Panel.");
               if (parent.Controls.Count > 1)
                  parent.Controls[1].Top = parent.Controls[0].Bottom + mEm;
               clusterContainer.LayoutClusters(); // ClusterLayoutMode set at construction handles dispatch
               clusterContainer.Invalidate();
            }
         }

         private void ApplyThemeToControlTree(Control pParent) {
            Color color = mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground];
            foreach (Control control in pParent.Controls) {
               if (control is BaseCluster cluster && cluster.mSkipTheme) {
                  ApplyThemeToControlTree(control);
                  continue;
               }
               control.ForeColor = mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont];
               if (control is not BaseCluster && control is not TabControl && control is not TabPage) {
                  control.BackColor = mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground];
                  control.Font = CreateNewFont(mTemporaryTheme.mFonts[(int)FontUsage.Interface]);
               }
               if (control is BaseCluster baseCluster) {
                  baseCluster.LayoutCluster();
                  baseCluster.LayoutCluster();
               }
               if (control is GroupBox groupBox) {
                  groupBox.Font = CreateNewBoldFont();
                  groupBox.BackColor = mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
                  groupBox.ForeColor = mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont];
               }
               if (control is MenuStrip menuStrip) {
                  menuStrip.BackColor = mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.MenuBackground];
                  foreach (ToolStripItem item in menuStrip.Items)
                     if (item is ToolStripMenuItem tsmi)
                        PaintMenuItemsRecursive(tsmi, mTemporaryTheme);
               }
               if (control is StatusStrip statusStrip)
                  ApplyThemeToToolStrip(statusStrip, FontUsage.Status, ColorSwatchUsage.StatusFont, ColorSwatchUsage.StatusBackground);
               ApplyThemeToControlTree(control);
            }
         }

         private void ApplyThemeToToolStrip(ToolStrip pToolStrip, FontUsage pFontUsage, ColorSwatchUsage pForeUsage,
            ColorSwatchUsage pBackUsage) {
            pToolStrip.BackColor = mTemporaryTheme.mInterfaceColors[(int)pBackUsage];
            pToolStrip.ForeColor = mTemporaryTheme.mInterfaceColors[(int)pForeUsage];
            pToolStrip.Font = CreateNewFont(mTemporaryTheme.mFonts[(int)pFontUsage]);
            pToolStrip.Renderer = new ToolStripProfessionalRenderer();
            foreach (ToolStripItem item in pToolStrip.Items) {
               if (item is ToolStripControlHost host) {
                  host.Control.ForeColor = mTemporaryTheme.mInterfaceColors[(int)pForeUsage];
                  host.Control.BackColor = mTemporaryTheme.mInterfaceColors[(int)pBackUsage];
                  host.Control.Font = CreateNewFont(mTemporaryTheme.mFonts[(int)pFontUsage]);
               }
               else {
                  item.ForeColor = mTemporaryTheme.mInterfaceColors[(int)pForeUsage];
                  item.BackColor = mTemporaryTheme.mInterfaceColors[(int)pBackUsage];
                  item.Font = CreateNewFont(mTemporaryTheme.mFonts[(int)pFontUsage]);
               }
            }
         }

         public void ApplyTheme(Theme pTheme) {
            Theme clonedTheme = pTheme.Clone();
            mTemporaryTheme.Dispose();
            mTemporaryTheme = clonedTheme;
            Theme theme = clonedTheme;
            BackColor = theme.mInterfaceColors[(int)ColorSwatchUsage.PanelBackground];
            mPrimaryTabControl.SetStripBackColor(theme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]);
            mPrimaryTabControl.ResetStripBackgroundPainted();
            mHighlightTabControl.SetStripBackColor(theme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]);
            mHighlightTabControl.ResetStripBackgroundPainted();
            ApplyThemeToControlTree(mPrimaryTabControl);
            ApplyThemeToControlTree(mHighlightTabControl);
            mThemeBottomPanel.SetFontAndColor();
            mPrimaryTabControl.Invalidate(true);
            mHighlightTabControl.Invalidate(true);
         }

         private void HighlightExampleBox(RichTextBox pBox, LanguageKind pLanguage) {
            string text;
            ITokenizer tokenizer;
            IHighlighter highlighter;
            IReadOnlyList<Token> tokens;
            int selectionStart, selectionLength;
            if (pLanguage == LanguageKind.PlainText)
               return;
            text = pBox.Text;
            if (text.Length == 0)
               return;
            tokenizer = LanguageRegistry.GetTokenizer(pLanguage);
            highlighter = LanguageRegistry.GetHighlighter(pLanguage);
            tokens = tokenizer.Tokenize(text);
            selectionStart = pBox.SelectionStart;
            selectionLength = pBox.SelectionLength;
            pBox.SuspendLayout();
            try {
               pBox.Select(0, pBox.TextLength);
               pBox.SelectionColor = mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.TextBoxFont];
               highlighter.ApplyHighlighting(pBox, tokens, mTemporaryTheme);
               pBox.Select(selectionStart, selectionLength);
            }
            finally {
               pBox.ResumeLayout();
            }
         }

         private void HighlightAllExampleBoxes() {
            for (int i = 0; i < mExampleRichTextBoxs.Length; i++)
               HighlightExampleBox(mExampleRichTextBoxs[i], (LanguageKind)i);
         }

         private void AddFontCluster(List<BaseCluster> pClusters, string pLabelText, string pButtonText, FontUsage pUsage,
            LabelPosition pLabelPosition = LabelPosition.Right) {
            LabeledButtonTextBoxCluster cluster = new LabeledButtonTextBoxCluster(mTemporaryTheme, pLabelText, pButtonText,
               pLabelPosition) {
               Tag = pUsage
            };
            cluster.mButton.Tag = pUsage;
            cluster.LayoutCluster();
            cluster.mButton.Click += OnFontButtonClicked;
            pClusters.Add(cluster);
         }

         public void UpdateFontLabels(FontUsage pUsage) {
            string fontDescription = string.Empty;
            Font font = mTemporaryTheme.mFonts[(int)pUsage];
            switch (pUsage) {
               case FontUsage.Interface:
                  fontDescription = $"Family: {mTemporaryTheme.mFonts[(int)FontUsage.Interface].FontFamily.Name}, Size: {mTemporaryTheme.mFonts[(int)FontUsage.Interface].Size} Style: {mTemporaryTheme.mFonts[(int)FontUsage.Interface].Style}";
                  break;
               case FontUsage.Menu:
                  fontDescription = $"Family: {mTemporaryTheme.mFonts[(int)FontUsage.Menu].FontFamily.Name}, Size: {mTemporaryTheme.mFonts[(int)FontUsage.Menu].Size} Style: {mTemporaryTheme.mFonts[(int)FontUsage.Menu].Style}";
                  break;
               case FontUsage.Status:
                  fontDescription = $"Family: {mTemporaryTheme.mFonts[(int)FontUsage.Status].FontFamily.Name}, Size: {mTemporaryTheme.mFonts[(int)FontUsage.Status].Size} Style: {mTemporaryTheme.mFonts[(int)FontUsage.Status].Style}";
                  break;
               case FontUsage.Text:
                  fontDescription = $"Family: {mTemporaryTheme.mFonts[(int)FontUsage.Text].FontFamily.Name}, Size: {mTemporaryTheme.mFonts[(int)FontUsage.Text].Size} Style: {mTemporaryTheme.mFonts[(int)FontUsage.Text].Style}";
                  break;
            }
            ((LabeledButtonTextBoxCluster)mFontsClusters[(int)pUsage]).UpdateLabel(fontDescription);
         }

         private void AddColorCluster(List<BaseCluster> pClusters, string pLabel, ColorSwatchUsage pUsage,
            LabelPosition pLabelPosition = LabelPosition.Left) {
            Color color = mTemporaryTheme.mInterfaceColors[(int)pUsage];
            LabeledButtonColorSwatchCluster cluster = new LabeledButtonColorSwatchCluster(mTemporaryTheme, pLabel,
               ToDescription(pUsage), pUsage, pLabelPosition, color);
            cluster.SwatchClicked += OnColorSwatchClicked;
            pClusters.Add(cluster);
         }

         private void AddColorCluster(List<BaseCluster> pClusters, string pLabel, TokenKind pTokenKind,
            LanguageKind pLanguage, LabelPosition pLabelPosition = LabelPosition.Left) {
            Color color = mTemporaryTheme.mHighlightColors[(int)pLanguage][(int)pTokenKind];
            LabeledButtonColorSwatchCluster cluster = new LabeledButtonColorSwatchCluster(mTemporaryTheme, pLabel,
               ToDescription(pTokenKind), pTokenKind, pLabelPosition, color);
            cluster.SyntaxSwatchClicked += OnSyntaxColorSwatchClicked;
            pClusters.Add(cluster);
         }

         private List<LabeledButtonColorSwatchCluster> CreateColorUsageClusters() {
            List<LabeledButtonColorSwatchCluster> clusters = [];
            foreach (ColorSwatchUsage usage in Enum.GetValues<ColorSwatchUsage>()) {
               string labelText = ToDescription(usage);
               string buttonText = ColorSwatchUsageButtonNames.Names[usage];
               Color initialColor = mTemporaryTheme.mInterfaceColors[(int)usage];
               LabeledButtonColorSwatchCluster cluster = new LabeledButtonColorSwatchCluster(mTemporaryTheme, labelText,
                  buttonText, usage, LabelPosition.Left, initialColor, null);
               cluster.SwatchClicked += OnColorSwatchClicked;
               clusters.Add(cluster);
            }
            return clusters;
         }

         private void DrawTabControlItem(VariableWidthTabControl pTabControl, DrawItemEventArgs pArgs) {
            Theme theme = mTemporaryTheme;
            TabPage page = pTabControl.TabPages[pArgs.Index];
            Rectangle rect = pTabControl.GetTabRect(pArgs.Index);
            bool selected = pTabControl.SelectedIndex == pArgs.Index;
            Color back = selected ? theme.mInterfaceColors[(int)ColorSwatchUsage.TabHeaderSelectedBackground]
                                  : theme.mInterfaceColors[(int)ColorSwatchUsage.TabHeaderUnselectedBackground];
            Color fore = selected ? theme.mInterfaceColors[(int)ColorSwatchUsage.TabHeaderSelectedFont]
                                  : theme.mInterfaceColors[(int)ColorSwatchUsage.TabHeaderUnselectedFont];
            Font font = selected ? CreateNewBoldFont() : CreateNewFont();
            using (SolidBrush brush = new SolidBrush(back))
               pArgs.Graphics.FillRectangle(brush, rect);
            TextRenderer.DrawText(pArgs.Graphics, page.Text, font, rect, fore,
               TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
         }

         public bool ThemeIsDirty() {
            return mThemeIsDirty;
         }

         public void EnsureColorPickerPanel(Theme pTheme, ColorSwatchUsage pUsage, Color pInitialColor) {
            ThrowIfNull(mForm, nameof(mForm));
            mUiState.FormBounds = Bounds;
            if (mColorPickerPanel == null)
               mColorPickerPanel = new ColorPickerPanel(pTheme, pUsage, pInitialColor);
            else
               mColorPickerPanel.LayoutControls();
            mForm.SuspendClientSizeChanged();
            if (!mFirstColorPicker)
               Bounds = mUiState.mColorPickerBounds;
            mForm.ResumeClientSizeChanged();
            ShowColorPickerPanel();
         }

         public void ShowColorPickerPanel() {
            ThrowIfNull(mColorPickerPanel, nameof(mColorPickerPanel));
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mThemePanel, nameof(mThemePanel));
            if (mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Remove(mThemePanel);
            if (!mForm.Controls.Contains(mColorPickerPanel))
               mForm.Controls.Add(mColorPickerPanel);
            mColorPickerPanel.PerformLayout();
            mColorPickerPanel.Dock = DockStyle.Fill;
            mColorPickerPanel.Visible = true;
            mColorPickerPanel.BringToFront();
            mColorPickerPanel.Show();
            if (mFirstColorPicker) {
               mForm.PerformLayout();
               Size requiredSize = mColorPickerPanel.GetRequiredSize();
               Rectangle screenBounds = ScreenBoundsPrimary();
               int maxWidth = (int)(screenBounds.Width * 0.9);
               int maxHeight = (int)(screenBounds.Height * 0.9);
               int width = Math.Min(requiredSize.Width, maxWidth);
               int height = Math.Min(requiredSize.Height, maxHeight);
               mForm.SuspendClientSizeChanged();
               mForm.ClientSize = new Size(width, height);
               Point center = ScreenCenterPrimary();
               mForm.Location = new Point(center.X - (width / 2), center.Y - (height / 2));
               EnsureWindowFitsMonitor(mForm, false);
               mForm.ResumeClientSizeChanged();
               mFirstColorPicker = false;
            }
            else {
               mForm.SuspendClientSizeChanged();
               EnsureWindowFitsMonitor(mForm, false);
               mForm.ResumeClientSizeChanged();
            }
         }

         public static void RestoreFromColorPickerPanel() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mColorPickerPanel, nameof(mColorPickerPanel));
            ThrowIfNull(mThemePanel, nameof(mThemePanel));
            mColorPickerPanel.Visible = false;
            mColorPickerPanel.SendToBack();
            if (mForm.Controls.Contains(mColorPickerPanel))
               mForm.Controls.Remove(mColorPickerPanel);
            mUiState.mColorPickerBounds = mForm.Bounds;
            mForm.SuspendClientSizeChanged();
            mForm.Bounds = mUiState.FormBounds;
            mForm.ResumeClientSizeChanged();
            if (!mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Add(mThemePanel);
            if (mRepaint) {
               mRepaint = false;
               mThemePanel.ApplyTheme(mThemePanel.mTemporaryTheme);
               if (mThemePanel.mPrimaryTabControl.SelectedIndex == (int)PrimaryTabPageUsage.Examples)
                  mThemePanel.HighlightAllExampleBoxes();
               mThemePanel.Invalidate(true);
            }
         }

         public void EnsureFontPickerPanel(Theme pTheme, FontUsage pUsage, Font pInitialFont) {
            ThrowIfNull(mForm, nameof(mForm));
            mUiState.FormBounds = Bounds;
            if (mFontPickerPanel == null)
               mFontPickerPanel = new FontPickerPanel(pTheme, pUsage, pInitialFont);
            else
               mFontPickerPanel.LayoutControls();
            mForm.SuspendClientSizeChanged();
            if (!mFirstFontPicker)
               Bounds = mUiState.mFontPickerBounds;
            mForm.ResumeClientSizeChanged();
            ShowFontPickerPanel();
         }

         public void ShowFontPickerPanel() {
            ThrowIfNull(mFontPickerPanel, nameof(mFontPickerPanel));
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mThemePanel, nameof(mThemePanel));
            if (mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Remove(mThemePanel);
            if (!mForm.Controls.Contains(mFontPickerPanel))
               mForm.Controls.Add(mFontPickerPanel);
            mFontPickerPanel.PerformLayout();
            mFontPickerPanel.Dock = DockStyle.Fill;
            mFontPickerPanel.Visible = true;
            mFontPickerPanel.BringToFront();
            mFontPickerPanel.Show();
            if (mFirstFontPicker) {
               mForm.PerformLayout();
               Size requiredSize = mFontPickerPanel.GetRequiredSize();
               Rectangle screenBounds = ScreenBoundsPrimary();
               int maxWidth = (int)(screenBounds.Width * 0.9);
               int maxHeight = (int)(screenBounds.Height * 0.9);
               int width = Math.Min(requiredSize.Width, maxWidth);
               int height = Math.Min(requiredSize.Height, maxHeight);
               mForm.SuspendClientSizeChanged();
               mForm.ClientSize = new Size(width, height);
               Point center = ScreenCenterPrimary();
               mForm.Location = new Point(center.X - (width / 2), center.Y - (height / 2));
               EnsureWindowFitsMonitor(mForm, false);
               mForm.ResumeClientSizeChanged();
               mFirstFontPicker = false;
            }
            else {
               mForm.SuspendClientSizeChanged();
               EnsureWindowFitsMonitor(mForm, false);
               mForm.ResumeClientSizeChanged();
            }
         }

         public static void RestoreFromFontPickerPanel(Theme? pTheme = null) {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mFontPickerPanel, nameof(mFontPickerPanel));
            ThrowIfNull(mThemePanel, nameof(mThemePanel));
            mFontPickerPanel.Visible = false;
            mFontPickerPanel.SendToBack();
            if (mForm.Controls.Contains(mFontPickerPanel))
               mForm.Controls.Remove(mFontPickerPanel);
            mUiState.mFontPickerBounds = mForm.Bounds;
            mForm.SuspendClientSizeChanged();
            mForm.Bounds = mUiState.FormBounds;
            mForm.ResumeClientSizeChanged();
            if (!mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Add(mThemePanel);
            if (mRepaint) {
               mRepaint = false;
               if (pTheme != null)
                  mThemePanel.ApplyTheme(pTheme);
               if (mThemePanel.mPrimaryTabControl.SelectedIndex == (int)PrimaryTabPageUsage.Examples)
                  mThemePanel.HighlightAllExampleBoxes();
               mThemePanel.Invalidate(true);
            }
         }

         private void CloseThemePanel() {
            ThrowIfNull(mForm, nameof(mForm));
            mForm.RestoreFromThemePanel();
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               mThemeBottomPanel.mCancelButton!.Click -= CancelButton_Click;
               mThemeBottomPanel.mHelpButton!.Click -= MainForm.Help_Click;
               mApplyButton.Click -= ApplyButton_Click;
               mNewButton.Click -= NewButton_Click;
               mCloneButton.Click -= CloneButton_Click;
               mPrimaryTabControl.DrawItem -= PrimaryTabControl_DrawItem;
               mIncludeExcludeTabControl.DrawItem -= IncludeExcludeTabControl_DrawItem;
               mHighlightTabControl.DrawItem -= HighlightTabControl_DrawItem;
               mPrimaryTabControl.SelectedIndexChanged -= PrimaryTabControl_SelectedIndexChanged;
               mIncludeExcludeTabControl.SelectedIndexChanged -= IncludeExcludeTabControl_SelectedIndexChanged;
               mHighlightTabControl.SelectedIndexChanged -= HighlightTabControl_SelectedIndexChanged;
               if (mExampleScrollPanel != null)
                  mExampleScrollPanel.ClientSizeChanged -= ExampleScrollPanel_ClientSizeChanged;
               mTemporaryTheme?.Dispose();
               // mApplyButton, mNewButton, mCloneButton disposed by mThemeBottomPanel.Dispose
               // mHelpButton, mCancelButton owned and disposed by mThemeBottomPanel
               mThemeBottomPanel?.Dispose();
               mHighlightTabControl?.Dispose();
               mIncludeExcludeTabControl?.Dispose();
               mPrimaryTabControl?.Dispose();
               foreach (ClusterContainer container in mClusterContainers)
                  container?.Dispose();
               mInterfaceHeaderCluster?.Dispose();
               mTargetingHeaderCluster?.Dispose();
               mIncludeHeaderCluster?.Dispose();
               mExcludeHeaderCluster?.Dispose();
               mThemesHeaderCluster?.Dispose();
               mExamplesHeaderCluster?.Dispose();
               mCSharpHeaderCluster?.Dispose();
               mCHeaderCluster?.Dispose();
               mCppHeaderCluster?.Dispose();
               mBasicHeaderCluster?.Dispose();
               mFSharpHeaderCluster?.Dispose();
               mHTMLHeaderCluster?.Dispose();
               mCSSHeaderCluster?.Dispose();
               mXMLHeaderCluster?.Dispose();
               mJSONHeaderCluster?.Dispose();
               mPowerShellHeaderCluster?.Dispose();
               mBatchHeaderCluster?.Dispose();
               mSQLHeaderCluster?.Dispose();
               mMarkdownHeaderCluster?.Dispose();
               mPythonHeaderCluster?.Dispose();
               mExampleMenuStrip?.Dispose();
               mExampleStatusStrip?.Dispose();
               mExampleStatusButtonHost?.Dispose();
               mExampleGroupBox?.Dispose();
               foreach (Panel? panel in mAllScrollPanels)
                  panel?.Dispose();
               foreach (List<BaseCluster> clusterList in mAllClusters) {
                  foreach (BaseCluster cluster in clusterList)
                     cluster?.Dispose();
               }
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
