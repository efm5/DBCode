using System.Globalization;

namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePanel : Panel {
         private const string TemporaryThemePrefix = "\u26A0 TEMPORARY THEME \u26A0 ";
         private readonly Button mApplyButton, mCancelButton, mCloneButton, mHelpButton, mNewButton, mOkButton;
         private readonly HeaderLabelCluster mFirstTitleLabel, mSecondTitleLabel, mThirdTitleLabel, mTitleLabel;
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
            mHelpButton = new Button() { Tag = new HelpTag(HelpContext.Theme, ToDescription(pThemeUsage)) };
            mOkButton = new Button();
            mNewButton = new Button();
            mCloneButton = new Button();
            mTitleLabel = new HeaderLabelCluster("Themes", HeaderLabelSize.Normal);
            mFirstTitleLabel = new HeaderLabelCluster("Fonts", HeaderLabelSize.Small);
            mSecondTitleLabel = new HeaderLabelCluster("Colors", HeaderLabelSize.Small);
            mThirdTitleLabel = new HeaderLabelCluster("Interface", HeaderLabelSize.Small);
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
            BeginInvoke(new Action(() => { ApplyThemeToPanel(); }));
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
            LayoutCSharpTokenColorsPage();
            LayoutCTokenColorsPage();
            LayoutCppTokenColorsPage();
            LayoutBasicTokenColorsPage();
            LayoutFSharpTokenColorsPage();
            LayoutHTMLTokenColorsPage();
            LayoutCSSTokenColorsPage();
            LayoutXMLTokenColorsPage();
            LayoutJSONTokenColorsPage();
            LayoutPowerShellTokenColorsPage();
            LayoutBatchTokenColorsPage();
            LayoutSQLTokenColorsPage();
            LayoutMarkdownTokenColorsPage();
            LayoutPythonTokenColorsPage();
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
            mHighlightTabControl.TabPages.AddRange([new TabPage("Interface"), new TabPage("C#"), new TabPage("C"), new TabPage("C++"),
               new TabPage("Basic"), new TabPage("F#"), new TabPage("HTML"), new TabPage("CSS"), new TabPage("XML"), new TabPage("JSON"),
               new TabPage("Power Shell"), new TabPage("Batch"), new TabPage("SQL"), new TabPage("Markdown"), new TabPage("Python")]);
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

         private void LayoutCSharpTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.CSharp];
            Panel scrollPanel = new Panel {
               Name = $"CSharpColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("C# Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "CSharpColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutCTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.C];
            Panel scrollPanel = new Panel {
               Name = $"CColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("C Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "CColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutCppTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Cpp];
            Panel scrollPanel = new Panel {
               Name = $"CppColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("C++ Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "CppColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutBasicTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Basic];
            Panel scrollPanel = new Panel {
               Name = $"BasicColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("Basic Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "BasicColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutFSharpTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.FSharp];
            Panel scrollPanel = new Panel {
               Name = $"FSharpColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("F# Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "FSharpColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutHTMLTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.HTML];
            Panel scrollPanel = new Panel {
               Name = $"HTMLColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("HTML Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "HTMLColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutCSSTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.CSS];
            Panel scrollPanel = new Panel {
               Name = $"CSSColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("CSS Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "CSSColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutXMLTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.XML];
            Panel scrollPanel = new Panel {
               Name = $"XMLColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("XML Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "XMLColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutJSONTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.JSON];
            Panel scrollPanel = new Panel {
               Name = $"JSONColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("JSON Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "JSONColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutPowerShellTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.PowerShell];
            Panel scrollPanel = new Panel {
               Name = $"PowerShellColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("PowerShell Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "PowerShellColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutBatchTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Batch];
            Panel scrollPanel = new Panel {
               Name = $"BatchColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("Batch Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "BatchColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutSQLTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.SQL];
            Panel scrollPanel = new Panel {
               Name = $"SQLColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("SQL Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "SQLColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutMarkdownTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Markdown];
            Panel scrollPanel = new Panel {
               Name = $"MarkdownColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("Markdown Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "MarkdownColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         private void LayoutPythonTokenColorsPage() {
            TabPage page = mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Python];
            Panel scrollPanel = new Panel {
               Name = $"PythonColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            page.Controls.Add(scrollPanel);
            HeaderLabelCluster header = new HeaderLabelCluster("Python Token Highlight Colors", HeaderLabelSize.Small);
            List<BaseCluster> colorClusters = new List<BaseCluster>();
            AddColorCluster(colorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(colorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(colorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(colorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(colorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(colorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(colorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(colorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(colorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(colorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(colorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            ClusterContainer colorsContainer = new ClusterContainer(colorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "PythonColorsClusterContainer",
               AutoScroll = true
            };
            scrollPanel.Controls.Add(colorsContainer);
            scrollPanel.Controls.Add(header);
         }

         public void BeginThemeEditSession(Theme pCurrentTheme) {
            mCurrentTheme = pCurrentTheme;
            mTemporaryTheme = pCurrentTheme.Clone();
            mHasCachedPreferredSize = false;
            ApplyThemeToPanel();
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
            Color color = mTemporaryTheme.mInterfaceColors[(int)pUsage];
            LabeledButtonColorSwatchCluster cluster = new LabeledButtonColorSwatchCluster(
               pLabel,
               ToDescription(pUsage),
               pUsage,
               pLabelPosition,
               color
            );
            cluster.SwatchClicked += OnColorSwatchClicked;
            pClusters.Add(cluster);
         }

         private List<LabeledButtonColorSwatchCluster> CreateColorUsageClusters() {
            List<LabeledButtonColorSwatchCluster> clusters = new List<LabeledButtonColorSwatchCluster>();
            foreach (ColorUsage usage in Enum.GetValues(typeof(ColorUsage))) {
               string labelText = ToDescription(usage);
               string buttonText = ColorUsageButtonNames.Names[usage];
               Color initialColor = mTemporaryTheme.mInterfaceColors[(int)usage];
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
            Color back = selected ? theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedBackground]
                                  : theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedBackground];
            Color fore = selected ? theme.mInterfaceColors[(int)ColorUsage.TabHeaderSelectedFont]
                                  : theme.mInterfaceColors[(int)ColorUsage.TabHeaderUnselectedFont];
            Font font = selected ? CreateNewBoldFont() : CreateNewFont();
            using (SolidBrush brush = new SolidBrush(back))
               pArgs.Graphics.FillRectangle(brush, rect);
            TextRenderer.DrawText(pArgs.Graphics, page.Text, font, rect, fore,
               TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
         }

         public void ApplyThemeToPanel() {
            Theme theme = mTemporaryTheme;
            BackColor = theme.mInterfaceColors[(int)ColorUsage.PanelBackground];
            foreach (ToolStripItem item in mStatusStrip.Items) {
               if (item is ToolStripControlHost host) {
                  Control control = host.Control;
                  control.ForeColor = theme.mInterfaceColors[(int)ColorUsage.StatusFont];
                  control.BackColor = theme.mInterfaceColors[(int)ColorUsage.StatusBackground];
                  control.Font = theme.mFonts[(int)FontUsage.Status];
               }
            }
            ApplyThemeToControlTree(mPrimaryTabControl);
            ApplyThemeToControlTree(mHighlightTabControl);
            mStatusStrip.Renderer = new ToolStripProfessionalRenderer();
            mStatusStrip.Invalidate(true);
            foreach (ToolStripItem item in mStatusStrip.Items) {
               if (item is ToolStripControlHost host) {
                  Control control = host.Control;
                  nint handle = control.Handle;
                  control.ForeColor = theme.mInterfaceColors[(int)ColorUsage.StatusFont];
                  control.BackColor = theme.mInterfaceColors[(int)ColorUsage.StatusBackground];
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
               control.ForeColor = theme.mInterfaceColors[(int)ColorUsage.InterfaceFont];
               control.BackColor = theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground];
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

         public void EnsureColorPickerPanel(Theme pTheme, ColorUsage pUsage, Color pInitialColor) {
            mPrePickerBounds = Bounds;
            if (mColorPickerPanel == null)
               mColorPickerPanel = new ColorPickerPanel(pTheme, pInitialColor);
            if (!mFirstColorPicker)
               Bounds = mColorPickerBounds;
            else
               Bounds = new Rectangle(100, 100, 1000, 800);
            ShowColorPickerPanel();
         }

         public void ShowColorPickerPanel() {
            if (mColorPickerPanel == null)
               return;
            if (mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Remove(mThemePanel);
            if (!mForm.Controls.Contains(mColorPickerPanel))
               mForm.Controls.Add(mColorPickerPanel);
            mColorPickerPanel.PerformLayout();
            mColorPickerPanel.Dock = DockStyle.Fill;
            mColorPickerPanel.Visible = true;
            mColorPickerPanel.BringToFront();
            mColorPickerPanel.Show();
         }

         public static void RestoreFromColorPickerPanel() {
            if (mForm == null)
               return;
            if (mColorPickerPanel != null) {
               mColorPickerPanel.Visible = false;
               mColorPickerPanel.SendToBack();
               if (mForm.Controls.Contains(mColorPickerPanel))
                  mForm.Controls.Remove(mColorPickerPanel);
            }
            mForm.Bounds = mPrePickerBounds;
            if (!mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Add(mThemePanel);
         }

         public void EnsureFontPickerPanel(Theme pTheme, FontUsage pUsage, Font pInitialFont) {
            mPrePickerBounds = Bounds;
            if (mFontPickerPanel == null)
               mFontPickerPanel = new FontPickerPanel(pTheme, pInitialFont);
            if (!mFirstFontPicker)
               Bounds = mFontPickerBounds;
            else
               Bounds = new Rectangle(100, 100, 1000, 800);
            ShowFontPickerPanel();
         }

         public void ShowFontPickerPanel() {
            if (mFontPickerPanel == null)
               return;
            if (mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Remove(mThemePanel);
            if (!mForm.Controls.Contains(mFontPickerPanel))
               mForm.Controls.Add(mFontPickerPanel);
            mFontPickerPanel.PerformLayout();
            mFontPickerPanel.Dock = DockStyle.Fill;
            mFontPickerPanel.Visible = true;
            mFontPickerPanel.BringToFront();
            mFontPickerPanel.Show();
         }

         public static void RestoreFromFontPickerPanel() {
            if (mForm == null)
               return;
            if (mFontPickerPanel != null) {
               mFontPickerPanel.Visible = false;
               mFontPickerPanel.SendToBack();
               if (mForm.Controls.Contains(mFontPickerPanel))
                  mForm.Controls.Remove(mFontPickerPanel);
            }
            mForm.Bounds = mPreFontPickerBounds;
            if (!mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Add(mThemePanel);
         }

         private void CloseThemePanel() {
            mFirstTheme = false;
            mThemeBounds = mForm.Bounds;
            MainForm.RestoreFromThemePanel();
         }
      }
   }
}
