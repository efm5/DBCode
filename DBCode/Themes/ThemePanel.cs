using System.Globalization;

namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePanel : Panel {
         private const string TemporaryThemePrefix = "\u26A0 TEMPORARY THEME \u26A0 ";
         private readonly Button mApplyButton, mCancelButton, mCloneButton, mHelpButton, mNewButton, mOkButton;
         private readonly HeaderLabelCluster mFifthTitleLabel, mFirstTitleLabel, mFourthTitleLabel, mSecondTitleLabel, mThirdTitleLabel, mTitleLabel;
         private readonly LabeledButtonTextBoxCluster mFirstCluster, mFourthCluster, mSecondCluster, mThirdCluster;
         private readonly StatusStrip mStatusStrip;
         private readonly ToolStripControlHost mApplyHost, mCancelHost, mCloneHost, mHelpHost, mNewHost, mOkHost;
         private readonly ToolStripStatusLabel mSpringLabel;
         private readonly UiState mUiState;
         private readonly VariableWidthTabControl mHighlightTabControl, mPrimaryTabControl;
         private bool mHasCachedPreferredSize, mThemeIsDirty = false;
         private ClusterContainer mApplicationColorsContainer;
         private FontFamily mCachedFontFamily;
         private float mCachedFontSize;
         private FontStyle mCachedFontStyle;
         private Size mCachedPreferredSize;
         private Theme mTemporaryTheme;

         private struct InitialBoundsResult {
            internal Rectangle Bounds;
            internal bool IsClamped;
         }

         public ThemePanel(ThemeUsage pThemeUsage, UiState pUiState) {
            mUiState = pUiState;
            AutoScroll = true;
            AutoSize = false;
            BackColor = Color.Transparent;
            mApplyButton = new Button();
            mCancelButton = new Button();
            mHelpButton = new Button();
            mOkButton = new Button();
            mNewButton = new Button();
            mCloneButton = new Button();
            mTitleLabel = new HeaderLabelCluster("Themes", HeaderLabelSize.Normal);
            mFirstTitleLabel = new HeaderLabelCluster("Fonts", HeaderLabelSize.Small);
            mSecondTitleLabel = new HeaderLabelCluster("Colors", HeaderLabelSize.Small);
            mThirdTitleLabel = new HeaderLabelCluster("Interface", HeaderLabelSize.Small);
            mFourthTitleLabel = new HeaderLabelCluster("C# 1", HeaderLabelSize.Small);
            mFifthTitleLabel = new HeaderLabelCluster("C# 2", HeaderLabelSize.Small);
            mStatusStrip = new StatusStrip();
            mPrimaryTabControl = new VariableWidthTabControl();
            mHighlightTabControl = new VariableWidthTabControl();
            mApplyHost = new ToolStripControlHost(mApplyButton);
            mCancelHost = new ToolStripControlHost(mCancelButton);
            mHelpHost = new ToolStripControlHost(mHelpButton);
            mOkHost = new ToolStripControlHost(mOkButton);
            mNewHost = new ToolStripControlHost(mNewButton);
            mCloneHost = new ToolStripControlHost(mCloneButton);
            mSpringLabel = new ToolStripStatusLabel();
            mFirstCluster = new LabeledButtonTextBoxCluster("The Interface Font:", "Interface", LabelPosition.Left);
            mSecondCluster = new LabeledButtonTextBoxCluster("The Menu Font:", "Menu", LabelPosition.Left);
            mThirdCluster = new LabeledButtonTextBoxCluster("The Static Strip Font:", "Status Strip", LabelPosition.Left);
            mFourthCluster = new LabeledButtonTextBoxCluster("The Text Box Font:", "Box", LabelPosition.Left);
            mTemporaryTheme = mCurrentTheme.Clone(TemporaryThemePrefix + DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture));
         }

         protected override void OnHandleCreated(EventArgs pEventArgs) {
            base.OnHandleCreated(pEventArgs);
            CreateLayout();
            if (mFirstTheme) {
               Size preferredSize = GetPreferredContentSizeCached();
               InitialBoundsResult result = ComputeInitialThemePanelBounds(preferredSize);
               mForm.Bounds = result.Bounds;
            }
            Dock = DockStyle.Fill;
            BeginInvoke(new Action(() => { ApplyThemeToThemePanel(); }));
         }

         private Size GetPreferredContentSizeCached() {
            Font interfaceFont = mTemporaryTheme.mFonts[(int)FontUsage.Interface];

            if (mHasCachedPreferredSize && (mCachedFontFamily == interfaceFont.FontFamily) &&
               ((int)mCachedFontSize == (int)interfaceFont.SizeInPoints) && (mCachedFontStyle == interfaceFont.Style))
               return mCachedPreferredSize;
            mCachedFontFamily = interfaceFont.FontFamily;
            mCachedFontSize = interfaceFont.SizeInPoints;
            mCachedFontStyle = interfaceFont.Style;
            mHasCachedPreferredSize = true;
            return ComputePreferredContentSizeInternal();
         }

         private Size ComputePreferredContentSizeInternal() {
            int maxWidthPrimary = 300, maxHeightPrimary = 300, maxWidthHighlight = 300, maxHeightHighlight = 300, pageWidth, pageHeight, wantedWidth, wantedHeight;

            foreach (Panel panel in mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Interface].Controls.OfType<Panel>()) {
               Control? rightmost = Rightmost(ControlCollectionAsList(panel.Controls));
               Control? bottommost = Bottommost(ControlCollectionAsList(panel.Controls));

               pageWidth = rightmost != null ? rightmost.Right : 300;
               pageHeight = bottommost != null ? bottommost.Bottom : 300;
               if (pageWidth > maxWidthPrimary)
                  maxWidthPrimary = pageWidth;
               if (pageHeight > maxHeightPrimary)
                  maxHeightPrimary = pageHeight;
            }
            foreach (Panel panel in mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Interface].Controls.OfType<Panel>()) {
               Control? rightmost = Rightmost(ControlCollectionAsList(panel.Controls));
               Control? bottommost = Bottommost(ControlCollectionAsList(panel.Controls));

               pageWidth = rightmost != null ? rightmost.Right : 300;
               pageHeight = bottommost != null ? bottommost.Bottom : 300;
               if (pageWidth > maxWidthHighlight)
                  maxWidthHighlight = pageWidth;
               if (pageHeight > maxHeightHighlight)
                  maxHeightHighlight = pageHeight;
            }
            wantedWidth = Math.Max(maxWidthPrimary, maxWidthHighlight);//DEBUG efm5 2026 04 19 add space for scrollbars
            wantedHeight = Math.Max(maxHeightPrimary, maxHeightHighlight) + mTitleLabel.Height + mStatusStrip.Height + mEm2;
            if (wantedWidth < 300)
               wantedWidth = 300;
            if (wantedHeight < 300)
               wantedHeight = 300;
            return new Size(wantedWidth, wantedHeight);
         }

         private void CreateLayout() {
            SuspendLayout();
            CreateTabControls();
            CreateStatusStrip();
            LayoutPrimaryClusters();
            LayoutInterfaceColorsPage();
            LayoutCSharp1ColorsPage();
            mApplyButton.Click += ApplyButton_Click;
            mCancelButton.Click += CancelButton_Click;
            mHelpButton.Click += MainForm.Help_Click;
            mOkButton.Click += OkButton_Click;
            mNewButton.Click += NewButton_Click;
            mCloneButton.Click += CloneButton_Click;
            Controls.AddRange(mPrimaryTabControl, mStatusStrip, mTitleLabel);
            mHasCachedPreferredSize = false;
            ResumeLayout(true);
         }

         private void CreateTabControls() {
            mPrimaryTabControl.TabPages.AddRange([new TabPage("Fonts"), new TabPage("Colors")]);
            mHighlightTabControl.TabPages.AddRange([new TabPage("Interface"), new TabPage("C#"), new TabPage("Basic")]);
            mPrimaryTabControl.Dock = DockStyle.Fill;
            mHighlightTabControl.Dock = DockStyle.Fill;
            mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Color].Controls.Add(mHighlightTabControl);
            mPrimaryTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            mHighlightTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            mPrimaryTabControl.DrawItem += PrimaryTabControl_DrawItem;
            mHighlightTabControl.DrawItem += HighlightTabControl_DrawItem;
            mPrimaryTabControl.SelectedIndex = mUiState.mThemePrimaryTabPageIndex;
            mHighlightTabControl.SelectedIndex = mUiState.mThemeHighlightTabPageIndex;
            mPrimaryTabControl.SelectedIndexChanged += PrimaryTabControl_SelectedIndexChanged;
            mHighlightTabControl.SelectedIndexChanged += HighlightTabControl_SelectedIndexChanged;
         }

         private void LayoutPrimaryClusters() {
            TabPage page = mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Interface];
            Panel scrollPanel = new Panel {
               Name = $"PrimaryTabControlTabPageScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            page.Controls.Add(scrollPanel);
            scrollPanel.Controls.Add(mFirstTitleLabel);
            int top = mFirstTitleLabel.Bottom + mEm;
            foreach (LabeledButtonTextBoxCluster cluster in new[] { mFirstCluster, mSecondCluster, mThirdCluster, mFourthCluster }) {
               cluster.Left = mEm;
               cluster.Top = top;
               scrollPanel.Controls.Add(cluster);
               top = cluster.Bottom + mEm;
            }
         }

         private void LayoutInterfaceColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Interface];
            Panel scrollPanel = new Panel {
               Name = $"InterfaceColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("Interface Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Panel Background", ColorSwatchUsage.PanelBackground);
            AddColorCluster(colorClusters, "TextBox Background", ColorSwatchUsage.TextBox);
            AddColorCluster(colorClusters, "TextBox Font", ColorSwatchUsage.TextBoxFont);
            AddColorCluster(colorClusters, "Menu Background", ColorSwatchUsage.MenuBackground);
            AddColorCluster(colorClusters, "Menu Font", ColorSwatchUsage.MenuFont);
            AddColorCluster(colorClusters, "Interface Background", ColorSwatchUsage.InterfaceBackground);
            AddColorCluster(colorClusters, "Interface Font", ColorSwatchUsage.InterfaceFont);
            AddColorCluster(colorClusters, "Status Background", ColorSwatchUsage.StatusBackground);
            AddColorCluster(colorClusters, "Status Font", ColorSwatchUsage.StatusFont);
            AddColorCluster(colorClusters, "GroupBox Background", ColorSwatchUsage.GroupBoxBackground);
            AddColorCluster(colorClusters, "GroupBox Font", ColorSwatchUsage.GroupBoxFont);
            AddColorCluster(colorClusters, "Tab Header Unselected Font", ColorSwatchUsage.TabHeaderUnselectedFont);
            AddColorCluster(colorClusters, "Tab Header Selected Font", ColorSwatchUsage.TabHeaderSelectedFont);
            AddColorCluster(colorClusters, "Tab Header Unselected Background", ColorSwatchUsage.TabHeaderUnselectedBackground);
            AddColorCluster(colorClusters, "Tab Header Selected Background", ColorSwatchUsage.TabHeaderSelectedBackground);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "InterfaceColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutCSharp1ColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.CSharp1];
            Panel scrollPanel = new Panel {
               Name = $"CSharpColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("C# Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown Token", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace Token", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier Token", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword Token", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number Token", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal Token", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal Token", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment Token", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive Token", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator Token", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation Token", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "CSharpColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         public void BeginThemeEditSession(Theme pCurrentTheme) {
            mCurrentTheme = pCurrentTheme;
            mTemporaryTheme = pCurrentTheme.Clone();
            mHasCachedPreferredSize = false;
            ApplyThemeToThemePanel();
            Invalidate(true);
         }

         private static InitialBoundsResult ComputeInitialThemePanelBounds(Size pPreferredSize) {
            Rectangle screenBounds = ScreenBoundsPrimary();
            Rectangle taskbarBounds = GetTaskbarBounds();
            int availableLeft = screenBounds.Left;
            int availableTop = screenBounds.Top;
            int availableWidth = screenBounds.Width;
            int availableHeight = screenBounds.Height;
            if (!taskbarBounds.IsEmpty) {
               bool taskbarTop = taskbarBounds.Top == screenBounds.Top &&
                                 taskbarBounds.Height < screenBounds.Height;
               bool taskbarBottom = taskbarBounds.Bottom == screenBounds.Bottom &&
                                    taskbarBounds.Height < screenBounds.Height;
               bool taskbarLeft = taskbarBounds.Left == screenBounds.Left &&
                                  taskbarBounds.Width < screenBounds.Width;
               bool taskbarRight = taskbarBounds.Right == screenBounds.Right &&
                                   taskbarBounds.Width < screenBounds.Width;
               if (taskbarTop) {
                  availableTop = taskbarBounds.Bottom;
                  availableHeight = screenBounds.Bottom - availableTop;
               }
               else if (taskbarBottom) {
                  availableHeight = taskbarBounds.Top - screenBounds.Top;
               }
               else if (taskbarLeft) {
                  availableLeft = taskbarBounds.Right;
                  availableWidth = screenBounds.Right - availableLeft;
               }
               else if (taskbarRight) {
                  availableWidth = taskbarBounds.Left - screenBounds.Left;
               }
            }
            int maxWidth = (int)Math.Floor(availableWidth * 0.9f);
            int maxHeight = (int)Math.Floor(availableHeight * 0.9f);
            int width = pPreferredSize.Width;
            int height = pPreferredSize.Height;
            bool widthClamped = width > maxWidth;
            bool heightClamped = height > maxHeight;
            if (widthClamped)
               width = maxWidth;
            if (heightClamped)
               height = maxHeight;
            Rectangle bounds = new Rectangle((availableLeft + ((availableWidth - width) / 2)),
               (availableTop + ((availableHeight - height) / 2)), width, height);
            InitialBoundsResult result;
            result.Bounds = bounds;
            result.IsClamped = widthClamped || heightClamped;
            return result;
         }

         private void AddColorCluster(List<BaseCluster> pClusters, string pLabel, ColorSwatchUsage pUsage,
            LabelPosition pLabelPosition = LabelPosition.Left) {
            Color color = mCurrentTheme.mColors[(int)pUsage];
            LabeledButtonColorSwatchCluster cluster = new LabeledButtonColorSwatchCluster(
               pLabel,
               ToDescription(pUsage),
               pUsage,
               pLabelPosition,
               color
            );
            pClusters.Add(cluster);
         }

         private List<LabeledButtonColorSwatchCluster> CreateColorUsageClusters() {
            List<LabeledButtonColorSwatchCluster> clusters = new List<LabeledButtonColorSwatchCluster>();
            foreach (ColorUsage usage in Enum.GetValues(typeof(ColorUsage))) {
               string labelText = ToDescription(usage);
               string buttonText = ColorUsageButtonNames.Names[usage];
               Color initialColor = mTemporaryTheme.mColors[(int)usage];
               LabeledButtonColorSwatchCluster cluster = new LabeledButtonColorSwatchCluster(labelText, buttonText,
                  (ColorSwatchUsage)usage, LabelPosition.Left, initialColor, null);
               cluster.SwatchClicked += OnColorSwatchClicked;
               clusters.Add(cluster);
            }
            return clusters;
         }

         private void CreateStatusStrip() {
            mStatusStrip.SizingGrip = true;
            mStatusStrip.Dock = DockStyle.Bottom;
            mSpringLabel.Spring = true;
            mHelpButton.Text = "Help";
            mApplyButton.Text = "Apply";
            mOkButton.Text = "OK";
            mCancelButton.Text = "Cancel";
            mNewButton.Text = "New";
            mCloneButton.Text = "Clone";
            mStatusStrip.Items.AddRange(mHelpHost, mNewHost, mCloneHost, mSpringLabel, mApplyHost, mOkHost, mCancelHost);
         }

         private void DrawTabControlItem(VariableWidthTabControl pTabControl, DrawItemEventArgs pArgs) {
            Theme theme = mTemporaryTheme;
            TabPage page = pTabControl.TabPages[pArgs.Index];
            Rectangle rect = pTabControl.GetTabRect(pArgs.Index);
            bool selected = pTabControl.SelectedIndex == pArgs.Index;
            Color back = selected ? theme.mColors[(int)ColorUsage.TabHeaderSelectedBackground]
                                  : theme.mColors[(int)ColorUsage.TabHeaderUnselectedBackground];
            Color fore = selected ? theme.mColors[(int)ColorUsage.TabHeaderSelectedFont]
                                  : theme.mColors[(int)ColorUsage.TabHeaderUnselectedFont];
            Font font = selected ? CreateNewBoldFont() : CreateNewFont();
            using (SolidBrush brush = new SolidBrush(back))
               pArgs.Graphics.FillRectangle(brush, rect);
            TextRenderer.DrawText(pArgs.Graphics, page.Text, font, rect, fore,
               TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
         }

         public void ApplyThemeToThemePanel() {
            Theme theme = mTemporaryTheme;
            BackColor = theme.mColors[(int)ColorUsage.PanelBackground];
            foreach (ToolStripItem item in mStatusStrip.Items)
               if (item is ToolStripControlHost host) {
                  Control control = host.Control;
                  control.ForeColor = theme.mColors[(int)ColorUsage.StatusFont];
                  control.BackColor = theme.mColors[(int)ColorUsage.StatusBackground];
                  control.Font = theme.mFonts[(int)FontUsage.Status];
               }
            ApplyThemeToControlTree(mPrimaryTabControl);
            ApplyThemeToControlTree(mHighlightTabControl);
            mStatusStrip.Renderer = new ToolStripProfessionalRenderer();
            mStatusStrip.Invalidate(true);
            foreach (ToolStripItem item in mStatusStrip.Items) {
               if (item is ToolStripControlHost host) {
                  Control control = host.Control;
                  var handle = control.Handle;
                  control.ForeColor = theme.mColors[(int)ColorUsage.StatusFont];
                  control.BackColor = theme.mColors[(int)ColorUsage.StatusBackground];
                  control.Font = theme.mFonts[(int)FontUsage.Status];
                  control.Invalidate();
                  control.Update();
               }
            }
            mHasCachedPreferredSize = false;
         }

         private void ApplyThemeToControlTree(Control pParent) {
            Theme theme = mTemporaryTheme;
            foreach (Control control in pParent.Controls) {
               if (control is BaseCluster cluster && cluster.mSkipTheme) {
                  ApplyThemeToControlTree(control);
                  continue;
               }
               control.ForeColor = theme.mColors[(int)ColorUsage.InterfaceFont];
               control.BackColor = theme.mColors[(int)ColorUsage.InterfaceBackground];
               control.Font = theme.mFonts[(int)FontUsage.Interface];
               ApplyThemeToControlTree(control);
            }
         }

         public void UpdateTheme() {
            //DEBUG efm5 2026 04 7 commit temporary theme to current theme
         }

         public void ApplyTheme() {
            //DEBUG efm5 2026 04 7 apply temporary theme to preview
         }

         private void RefreshThemePreview() {
            //DEBUG efm5 2026 04 8 do the work
         }

         private void OnColorSwatchClicked(object? pSender, ColorSwatchUsage pUsage) {
            if (pSender is not ColorSwatch swatch)
               return;
            Color newColor = Color.Maroon;
            mTemporaryTheme.mColors[(int)pUsage] = newColor;
            swatch.SetColor(newColor);
            mThemeIsDirty = true;
            TimedMessage($"[TEST MODE] Set {pUsage} to {newColor}", "COLOR UPDATED");
         }

         private void CloseThemePanel() {
            mFirstTheme = false;
            mThemeBounds = mForm.Bounds;
            MainForm.RestoreFromThemePanel();
         }
      }
   }
}
