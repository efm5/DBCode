namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePanel : ScrollablePanel {
         public void SetThemeUsage(ThemeUsage pThemeUsage) {
            mThemeUsage = pThemeUsage;
         }

         public void LayoutControls() {
            SuspendLayout();
            //efm5 - switching to each tab forces the controls to be created and laid out, which is necessary
            //to get accurate measurements for the wanted size of the panel; without this, the panel may report
            //a smaller wanted size than it actually needs
            mPrimaryTabControl.SelectedIndexChanged -= PrimaryTabControl_SelectedIndexChanged;
            mPrimaryTabControl.DrawItem -= PrimaryTabControl_DrawItem;
            mHighlightTabControl.SelectedIndexChanged -= HighlightTabControl_SelectedIndexChanged;
            mHighlightTabControl.DrawItem -= HighlightTabControl_DrawItem;
            int savedPrimary = mUiState.mThemePrimaryTabPageIndex;
            int savedHighlight = mUiState.mThemeHighlightTabPageIndex;
            mPrimaryTabControl.SelectedIndex = (int)PrimaryTabPageUsage.Interface;
            mPrimaryTabControl.SelectedIndex = (int)PrimaryTabPageUsage.Color;
            mPrimaryTabControl.SelectedIndex = (int)PrimaryTabPageUsage.Examples;
            for (int i = 0; i < mHighlightTabControl.TabPages.Count; i++)
               mHighlightTabControl.SelectedIndex = i;
            //efm5 - now that all controls have been visited, restore the persisted selections
            mPrimaryTabControl.SelectedIndex = savedPrimary;
            mHighlightTabControl.SelectedIndex = savedHighlight;
            mPrimaryTabControl.SelectedIndexChanged += PrimaryTabControl_SelectedIndexChanged;
            mHighlightTabControl.SelectedIndexChanged += HighlightTabControl_SelectedIndexChanged;
            mPrimaryTabControl.DrawItem += PrimaryTabControl_DrawItem;
            mHighlightTabControl.DrawItem += HighlightTabControl_DrawItem;
            ApplyTheme(mTemporaryTheme);
            LayoutClustersAndContainers();
            mBottomPanel.LayoutControls();
            mExampleBottomPanel.LayoutControls();
            SizeExamplesContainer();
            mExamplesContainer.Height += mEmHalf;
            mExampleGroupBox.Location = new Point(mIndent, mExampleMenuStrip.Bottom + mEmHalf);
            mExampleBottomPanel.Location = new Point(mIndent, mExampleGroupBox.Bottom + mEmHalf);
            mExampleBottomPanel.Anchor = mAnchorTopLeftRight;
            mExampleBottomPanel.Width = mExampleScrollPanel.ClientSize.Width - (2 * mIndent);
            mExampleBottomPanel.PositionRightControls();
            mExamplesContainer.Location = new Point(mIndent, mExampleBottomPanel.Bottom + mEmHalf);
            mPrimaryTabControl.Location = new Point(mIndent, mTitleLabel.Bottom + mEmHalf);
            mPrimaryTabControl.Width = ClientSize.Width - (2 * mIndent);
            mPrimaryTabControl.Height = ClientSize.Height - (mBottomPanel.Height + mTitleLabel.Height + mEm);
            mPrimaryTabControl.Anchor = mAnchorTopLeftBottomRight;
            ResumeLayout(true);
         }

         internal void UpdateActiveScrollablePanel() {
            int primaryIndex = mPrimaryTabControl.SelectedIndex;
            if (primaryIndex == (int)PrimaryTabPageUsage.Interface)
               mActiveScrollablePanel = mPrimaryScrollPanel;
            else if (primaryIndex == (int)PrimaryTabPageUsage.Examples)
               mActiveScrollablePanel = mExampleScrollPanel;
            else
               mActiveScrollablePanel = mHighlightScrollPanels[mHighlightTabControl.SelectedIndex];
         }

         private void SizeExamplesContainer() {
            ThrowIfNull(mExampleScrollPanel, nameof(mExampleScrollPanel));
            mExamplesContainer.Width = mExampleScrollPanel.ClientSize.Width - mRightPad;
            mExamplesContainer.LayoutClusters();
            SizePanel(mExamplesContainer, mIndent, false);
            mExamplesContainer.Height += mEmHalf;
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
            SizeTextBoxToFitString(out SizeF pOSize, mExampleRichTextBox);
            mExampleRichTextBox.Size = LayoutHelpers.SizeFromSizeF(pOSize);
            mExampleRichTextBox.Location = new Point(x, y);
            x += mExampleRichTextBox.Width + mEm;
            mExampleRadioButton.Location = new Point(x, y + (tallest - mExampleRadioButton.Height) / 2);
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
               clusterContainer.LayoutClusters();
               clusterContainer.Invalidate();
            }
         }

         private void ApplyThemeToControlTree(Control pParent) {
            foreach (Control control in pParent.Controls) {
               if (control is BaseCluster skipCluster && skipCluster.mSkipTheme) {
                  skipCluster.mTheme.Dispose();
                  skipCluster.mTheme = mTemporaryTheme.Clone();
                  skipCluster.LayoutCluster();
                  continue;
               }
               control.ForeColor = mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont];
               if (control is not BaseCluster && control is not TabControl && control is not TabPage && control is not GroupBox) {
                  control.BackColor = mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground];
                  Font oldControlFont = control.Font;
                  Font newControlFont = CreateNewFont(mTemporaryTheme.mFonts[(int)FontUsage.Interface]);
                  control.Font = newControlFont;
                  if (object.ReferenceEquals(control.Font, newControlFont)) {
                     if (!object.ReferenceEquals(oldControlFont, pParent.Font))
                        MainForm.DisposeFontIfOwned(oldControlFont);
                  }
                  else
                     newControlFont.Dispose();
               }
               if (control is BaseCluster baseCluster) {
                  baseCluster.LayoutCluster();
               }
               else if (control is GroupBox groupBox) {
                  Font oldGroupFont = groupBox.Font;
                  Font newGroupFont = CreateNewBoldFont();
                  groupBox.Font = newGroupFont;
                  if (object.ReferenceEquals(groupBox.Font, newGroupFont))
                     MainForm.DisposeFontIfOwned(oldGroupFont);
                  else
                     newGroupFont.Dispose();
                  groupBox.BackColor = mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
                  groupBox.ForeColor = mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont];
               }
               else if (control is MenuStrip menuStrip) {
                  menuStrip.BackColor = mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.MenuBackground];
                  foreach (ToolStripItem item in menuStrip.Items)
                     if (item is ToolStripMenuItem tsmi)
                        PaintMenuItemsRecursive(tsmi, mTemporaryTheme);
               }
               else if (control is StatusStrip statusStrip)
                  ApplyThemeToToolStrip(statusStrip, FontUsage.Status, ColorSwatchUsage.BottomPanelFont,
                     ColorSwatchUsage.BottomPanelBackground);
               if (control is not BaseCluster) // let the cluster own its children entirely
                  ApplyThemeToControlTree(control);
            }
         }

         private void ApplyThemeToToolStrip(ToolStrip pToolStrip, FontUsage pFontUsage,
            ColorSwatchUsage pForeUsage, ColorSwatchUsage pBackUsage) {
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
            foreach (List<BaseCluster> clusterList in mAllClusters) {
               foreach (BaseCluster cluster in clusterList) {
                  cluster.mTheme.Dispose();
                  cluster.mTheme = clonedTheme.Clone();
               }
            }
            mTitleLabel.mTheme.Dispose();
            mTitleLabel.mTheme = clonedTheme.Clone();
            mTitleLabel.LayoutCluster();
            foreach (FontUsage fontUsage in Enum.GetValues<FontUsage>())
               UpdateFontLabels(fontUsage);
            BackColor = clonedTheme.mInterfaceColors[(int)ColorSwatchUsage.PanelBackground];
            mPrimaryTabControl.SetStripBackColor(clonedTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]);
            ApplyThemeToControlTree(mPrimaryTabControl);
            ApplyThemeToControlTree(mHighlightTabControl);
            mBottomPanel.SetFontAndColor();
            mExampleBottomPanel.SetFontAndColor();
            Font rawInterfaceFont = clonedTheme.mFonts[(int)FontUsage.Interface];
            mPrimaryTabControl.Font = rawInterfaceFont;
            mHighlightTabControl.Font = rawInterfaceFont;
            mPrimaryTabControl.RecalculateItemSize(rawInterfaceFont);
            mHighlightTabControl.RecalculateItemSize(rawInterfaceFont);
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

         private void AddFontCluster(List<BaseCluster> pClusters, string pLabelText, string pButtonText,
            FontUsage pUsage, LabelPosition pLabelPosition = LabelPosition.Right) {
            LabeledButtonTextBoxCluster cluster = new LabeledButtonTextBoxCluster(mTemporaryTheme, pLabelText,
               pButtonText, pLabelPosition) {
               Tag = pUsage
            };
            cluster.mButton.Tag = pUsage;
            cluster.LayoutCluster();
            cluster.FontButtonClicked += OnFontButtonClicked;
            pClusters.Add(cluster);
         }

         public void UpdateFontLabels(FontUsage pUsage) {
            string fontDescription = string.Empty;
            switch (pUsage) {
               case FontUsage.Interface:
                  fontDescription = $"Family: {mTemporaryTheme.mFonts[(int)FontUsage.Interface].FontFamily.Name}, " +
                     $"Size: {mTemporaryTheme.mFonts[(int)FontUsage.Interface].Size} " +
                     $"Style: {mTemporaryTheme.mFonts[(int)FontUsage.Interface].Style}";
                  break;
               case FontUsage.Menu:
                  fontDescription = $"Family: {mTemporaryTheme.mFonts[(int)FontUsage.Menu].FontFamily.Name}, " +
                     $"Size: {mTemporaryTheme.mFonts[(int)FontUsage.Menu].Size} " +
                     $"Style: {mTemporaryTheme.mFonts[(int)FontUsage.Menu].Style}";
                  break;
               case FontUsage.Status:
                  fontDescription = $"Family: {mTemporaryTheme.mFonts[(int)FontUsage.Status].FontFamily.Name}, " +
                     $"Size: {mTemporaryTheme.mFonts[(int)FontUsage.Status].Size} " +
                     $"Style: {mTemporaryTheme.mFonts[(int)FontUsage.Status].Style}";
                  break;
               case FontUsage.Text:
                  fontDescription = $"Family: {mTemporaryTheme.mFonts[(int)FontUsage.Text].FontFamily.Name}, " +
                     $"Size: {mTemporaryTheme.mFonts[(int)FontUsage.Text].Size} " +
                     $"Style: {mTemporaryTheme.mFonts[(int)FontUsage.Text].Style}";
                  break;
            }
            ((LabeledButtonTextBoxCluster)mFontsClusters[(int)pUsage]).UpdateLabel(fontDescription);
         }

         private void AddColorCluster(List<BaseCluster> pClusters, string pLabel, ColorSwatchUsage pUsage,
            LabelPosition pLabelPosition = LabelPosition.Left) {
            Color color = mTemporaryTheme.mInterfaceColors[(int)pUsage];
            LabeledButtonColorSwatchCluster cluster = new LabeledButtonColorSwatchCluster(mTemporaryTheme, pLabel,
               ToDescription(pUsage), pUsage, pLabelPosition, color) {
               Tag = pUsage
            };
            cluster.SwatchClicked += OnColorSwatchClicked;
            pClusters.Add(cluster);
         }

         private void AddColorCluster(List<BaseCluster> pClusters, string pLabel, TokenKind pTokenKind,
            LanguageKind pLanguage, LabelPosition pLabelPosition = LabelPosition.Left) {
            Color color = mTemporaryTheme.mHighlightColors[(int)pLanguage][(int)pTokenKind];
            LabeledButtonColorSwatchCluster cluster = new LabeledButtonColorSwatchCluster(mTemporaryTheme, pLabel,
               ToDescription(pTokenKind), pTokenKind, pLabelPosition, color) {
               Tag = (pTokenKind, pLanguage)
            };
            cluster.SwatchClicked += OnSyntaxColorSwatchClicked;
            mSyntaxColorClusters.Add(cluster);
            pClusters.Add(cluster);
         }

         private List<LabeledButtonColorSwatchCluster> CreateColorUsageClusters() {
            List<LabeledButtonColorSwatchCluster> clusters = [];
            foreach (ColorSwatchUsage usage in Enum.GetValues<ColorSwatchUsage>()) {
               string labelText = ToDescription(usage);
               string buttonText = ColorSwatchUsageButtonNames.Names[usage];
               Color initialColor = mTemporaryTheme.mInterfaceColors[(int)usage];
               LabeledButtonColorSwatchCluster cluster = new LabeledButtonColorSwatchCluster(mTemporaryTheme,
                  labelText, buttonText, usage, LabelPosition.Left, initialColor, null) {
                  Tag = usage
               };
               cluster.SwatchClicked += OnColorSwatchClicked;
               clusters.Add(cluster);
            }
            return clusters;
         }

         public bool ThemeIsDirty() {
            return mThemeIsDirty;
         }

         public static void EnsureColorPickerPanel(Theme pTheme, ColorSwatchUsage pUsage, Color pInitialColor) {
            ThrowIfNull(mForm, nameof(mForm));
            mUiState.ThemeBounds = mForm.Bounds;
            mColorPickerPanel?.Dispose();
            mColorPickerPanel = new ColorPickerPanel(pTheme, pUsage, pInitialColor);
            ShowColorPickerPanel();
         }

         public static void EnsureColorPickerPanel(Theme pTheme, TokenKind pTokenKind, LanguageKind pLanguageKind,
            Color pInitialColor) {
            ThrowIfNull(mForm, nameof(mForm));
            mUiState.ThemeBounds = mForm.Bounds;
            mColorPickerPanel?.Dispose();
            mColorPickerPanel = new ColorPickerPanel(pTheme, pTokenKind, pLanguageKind, pInitialColor);
            ShowColorPickerPanel();
         }

         public static void EnsureColorPickerPanel(Theme pTheme, Color pInitialColor, Action<Color> pColorCallback) {
            ThrowIfNull(mForm, nameof(mForm));
            mUiState.ThemeBounds = mForm.Bounds;
            mColorPickerPanel?.Dispose();
            mColorPickerPanel = new ColorPickerPanel(pTheme, pInitialColor, pColorCallback,
               RestoreFromColorPickerPanelToOptions);
            ShowColorPickerPanelFromOptions();
         }

         public static void ShowColorPickerPanel() {
            ThrowIfNull(mColorPickerPanel, nameof(mColorPickerPanel));
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mThemePanel, nameof(mThemePanel));
            if (mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Remove(mThemePanel);
            if (!mForm.Controls.Contains(mColorPickerPanel))
               mForm.Controls.Add(mColorPickerPanel);
            mColorPickerPanel.ContextMenuStrip = mGeneralContextMenuStrip;
            mActiveScrollablePanel = mColorPickerPanel.ScrollPanel;
            mActiveLayoutable = mColorPickerPanel.mBottomPanel;
            mColorPickerPanel.mTitleLabel.CenterTitle();
            mColorPickerPanel.mBottomPanel.PositionRightControls();
            mColorPickerPanel.Dock = DockStyle.Fill;
            mColorPickerPanel.Visible = true;
            mColorPickerPanel.BringToFront();
            mColorPickerPanel.Show();
         }

         public static void ShowColorPickerPanelFromOptions() {
            ThrowIfNull(mColorPickerPanel, nameof(mColorPickerPanel));
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mOptionsPanel, nameof(mOptionsPanel));
            if (!mForm.Controls.Contains(mColorPickerPanel))
               mForm.Controls.Add(mColorPickerPanel);
            mColorPickerPanel.ContextMenuStrip = mGeneralContextMenuStrip;
            mActiveScrollablePanel = mColorPickerPanel.ScrollPanel;
            mActiveLayoutable = mColorPickerPanel.mBottomPanel;
            mColorPickerPanel.mTitleLabel.CenterTitle();
            mColorPickerPanel.mBottomPanel.PositionRightControls();
            mColorPickerPanel.Dock = DockStyle.Fill;
            mColorPickerPanel.Visible = true;
            mColorPickerPanel.BringToFront();
            mColorPickerPanel.Show();
         }

         public static void RestoreFromColorPickerPanel() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mColorPickerPanel, nameof(mColorPickerPanel));
            ThrowIfNull(mThemePanel, nameof(mThemePanel));
            if (mForm.Controls.Contains(mColorPickerPanel))
               mForm.Controls.Remove(mColorPickerPanel);
            mColorPickerPanel.ContextMenuStrip = null;
            mColorPickerPanel.Dispose();
            mColorPickerPanel = null;
            mUiState.mColorPickerBounds = mForm.Bounds;
            mForm.SuspendClientSizeChanged();
            mForm.Bounds = mUiState.ThemeBounds;
            mActiveLayoutable = mThemePanel.mBottomPanel;
            mForm.ResumeClientSizeChanged();
            if (!mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Add(mThemePanel);
            mThemePanel.UpdateActiveScrollablePanel();
            mThemePanel.BringToFront();
            mThemePanel.Show();
            mForm.Activate();
            mThemePanel.Focus();
            if (mRepaint) {
               mRepaint = false;
               mThemePanel.ApplyTheme(mThemePanel.mTemporaryTheme);
               if (mThemePanel.mPrimaryTabControl.SelectedIndex == (int)PrimaryTabPageUsage.Examples)
                  mThemePanel.HighlightAllExampleBoxes();
               mThemePanel.Invalidate(true);
            }
         }

         public static void RestoreFromColorPickerPanelToOptions() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mColorPickerPanel, nameof(mColorPickerPanel));
            ThrowIfNull(mOptionsPanel, nameof(mOptionsPanel));
            if (mForm.Controls.Contains(mColorPickerPanel))
               mForm.Controls.Remove(mColorPickerPanel);
            mColorPickerPanel.ContextMenuStrip = null;
            mColorPickerPanel.Dispose();
            mColorPickerPanel = null;
            mUiState.mColorPickerBounds = mForm.Bounds;
            mForm.SuspendClientSizeChanged();
            mForm.Bounds = mUiState.ThemeBounds;
            mActiveLayoutable = mOptionsPanel.mBottomPanel;
            mForm.ResumeClientSizeChanged();
            if (!mForm.Controls.Contains(mOptionsPanel))
               mForm.Controls.Add(mOptionsPanel);
            mOptionsPanel.UpdateActiveScrollablePanel();
            mOptionsPanel.BringToFront();
            mOptionsPanel.Visible = true;
            mOptionsPanel.Show();
            mForm.Activate();
            mOptionsPanel.Focus();
            mOptionsPanel.RefreshBraceExample();
         }

         public static void EnsureColorPickerPanelFromCoding(Theme pTheme, Color pInitialColor,
            Action<Color> pColorCallback) {
            ThrowIfNull(mForm, nameof(mForm));
            mUiState.ThemeBounds = mForm.Bounds;
            mColorPickerPanel?.Dispose();
            mColorPickerPanel = new ColorPickerPanel(pTheme, pInitialColor, pColorCallback,
               RestoreFromColorPickerPanelToCoding, "Select a color for the HTML span element");
            ShowColorPickerPanelFromCoding();
         }

         public static void ShowColorPickerPanelFromCoding() {
            ThrowIfNull(mColorPickerPanel, nameof(mColorPickerPanel));
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
            if (!mForm.Controls.Contains(mColorPickerPanel))
               mForm.Controls.Add(mColorPickerPanel);
            mColorPickerPanel.ContextMenuStrip = mGeneralContextMenuStrip;
            mActiveScrollablePanel = mColorPickerPanel.ScrollPanel;
            mActiveLayoutable = mColorPickerPanel.mBottomPanel;
            mColorPickerPanel.mTitleLabel.CenterTitle();
            mColorPickerPanel.mBottomPanel.PositionRightControls();
            mColorPickerPanel.Dock = DockStyle.Fill;
            mColorPickerPanel.Visible = true;
            mColorPickerPanel.BringToFront();
            mColorPickerPanel.Show();
         }

         public static void RestoreFromColorPickerPanelToCoding() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mColorPickerPanel, nameof(mColorPickerPanel));
            ThrowIfNull(mScrollableMainPanel, nameof(mScrollableMainPanel));
            ThrowIfNull(mMainBottomPanel, nameof(mMainBottomPanel));
            if (mForm.Controls.Contains(mColorPickerPanel))
               mForm.Controls.Remove(mColorPickerPanel);
            mColorPickerPanel.ContextMenuStrip = null;
            mColorPickerPanel.Dispose();
            mColorPickerPanel = null;
            mUiState.mColorPickerBounds = mForm.Bounds;
            mForm.SuspendClientSizeChanged();
            mForm.Bounds = mUiState.ThemeBounds;
            mActiveLayoutable = mMainBottomPanel;
            mForm.ResumeClientSizeChanged();
            mActiveScrollablePanel = mScrollableMainPanel;
            mScrollableMainPanel.BringToFront();
            mScrollableMainPanel.Visible = true;
            mForm.Activate();
            mScrollableMainPanel.Focus();
            mMainBottomPanel.LayoutControls();
         }

         public static void EnsureFontPickerPanel(Theme pTheme, FontUsage pUsage, Font pInitialFont) {
            ThrowIfNull(mForm, nameof(mForm));
            Theme theme = pTheme.Clone();
            mUiState.ThemeBounds = mForm.Bounds;
            mFontPickerPanel?.Dispose();
            mFontPickerPanel = new FontPickerPanel(theme, pUsage, pInitialFont);
            mBottomPanelExcluded = new List<Control>([mFontPickerPanel.mFontDescriptionLabel]);
            ShowFontPickerPanel();
         }

         public static void ShowFontPickerPanel() {
            ThrowIfNull(mFontPickerPanel, nameof(mFontPickerPanel));
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mThemePanel, nameof(mThemePanel));
            if (mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Remove(mThemePanel);
            if (!mForm.Controls.Contains(mFontPickerPanel))
               mForm.Controls.Add(mFontPickerPanel);
            mFontPickerPanel.ContextMenuStrip = mGeneralContextMenuStrip;
            mActiveScrollablePanel = mFontPickerPanel.ScrollPanel;
            mActiveLayoutable = mFontPickerPanel.mBottomPanel;
            mFontPickerPanel.mTitleLabel.CenterTitle();
            mFontPickerPanel.mBottomPanel.PositionRightControls();
            mFontPickerPanel.Dock = DockStyle.Fill;
            mFontPickerPanel.Visible = true;
            mFontPickerPanel.BringToFront();
            mFontPickerPanel.Show();
         }

         public static void RestoreFromFontPickerPanel(Theme? pTheme = null) {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mFontPickerPanel, nameof(mFontPickerPanel));
            ThrowIfNull(mThemePanel, nameof(mThemePanel));
            ThrowIfNull(mBottomPanelExcluded, nameof(mBottomPanelExcluded));
            mBottomPanelExcluded.Clear();
            mBottomPanelExcluded = null;
            if (mForm.Controls.Contains(mFontPickerPanel))
               mForm.Controls.Remove(mFontPickerPanel);
            mFontPickerPanel.ContextMenuStrip = null;
            mFontPickerPanel.Dispose();
            mFontPickerPanel = null;
            mUiState.mFontPickerBounds = mForm.Bounds;
            mForm.SuspendClientSizeChanged();
            mForm.Bounds = mUiState.ThemeBounds;
            mActiveLayoutable = mThemePanel.mBottomPanel;
            mForm.ResumeClientSizeChanged();
            if (!mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Add(mThemePanel);
            mThemePanel.UpdateActiveScrollablePanel();
            mThemePanel.BringToFront();
            mThemePanel.Show();
            mForm.Activate();
            mThemePanel.Focus();
            if (mRepaint) {
               mRepaint = false;
               if (pTheme != null) {
                  mThemePanel.ApplyTheme(pTheme);
                  mThemePanel.LayoutClustersAndContainers();
               }
               if (mThemePanel.mPrimaryTabControl.SelectedIndex == (int)PrimaryTabPageUsage.Examples)
                  mThemePanel.HighlightAllExampleBoxes();
               mThemePanel.Invalidate(true);
            }
         }

         private static void CloseThemePanel() {
            ThrowIfNull(mForm, nameof(mForm));
            mForm.RestoreFromThemePanel();
         }
      }
   }
}
