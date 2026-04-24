using System.Globalization;

namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePanel : Panel {
         private const string TemporaryThemePrefix = "\u26A0 TEMPORARY THEME \u26A0 ";
         private List<BaseCluster> mFontsClusters = [], mInterfaceColorClusters = [], mCSharpcolorClusters = [],
            mCcolorClusters = [], mCppcolorClusters = [], mBasiccolorClusters = [], mFSharpcolorClusters = [],
            mHTMLcolorClusters = [], mCSScolorClusters = [], mXMLcolorClusters = [], mJSONcolorClusters = [],
            mPowerShellcolorClusters = [], mBatchcolorClusters = [], mSQLcolorClusters = [],
            mMarkdowncolorClusters = [], mPythoncolorClusters = [];
         private readonly Button mApplyButton, mCancelButton, mCloneButton, mHelpButton, mNewButton, mOkButton;
         private ClusterContainer mFontsContainer, mInterfaceColorsContainer, mCSharpColorsContainer, mCColorsContainer,
            mCppColorsContainer, mBasicColorsContainer, mFSharpColorsContainer, mHTMLColorsContainer,
            mCSSColorsContainer, mXMLColorsContainer, mJSONColorsContainer, mPowerShellColorsContainer,
            mBatchColorsContainer, mSQLColorsContainer, mMarkdownColorsContainer, mPythonColorsContainer;
         private readonly HeaderLabelCluster mFontsHeaderCluster, mColorsHeaderCluster, mInterfaceHeaderCluster,
            mThemesHeaderCluster, mCSharpHeaderCluster, mCHeaderCluster, mCppHeaderCluster, mBasicHeaderCluster,
            mFSharpHeaderCluster, mHTMLHeaderCluster, mCSSHeaderCluster, mXMLHeaderCluster, mJSONHeaderCluster,
            mPowerShellHeaderCluster, mBatchHeaderCluster, mSQLHeaderCluster, mMarkdownHeaderCluster,
            mPythonHeaderCluster;
         private readonly StatusStrip mStatusStrip;
         private readonly ToolStripControlHost mApplyHost, mCancelHost, mCloneHost, mHelpHost, mNewHost, mOkHost;
         private readonly ToolStripStatusLabel mSpringLabel;
         private readonly UiState mUiState;
         private readonly VariableWidthTabControl mHighlightTabControl, mPrimaryTabControl;
         private bool mThemeIsDirty = false;
         private Panel? mPrimaryScrollPanel, mHighlightInterfaceScrollPanel, mHighlightCSharpScrollPanel,
           mHighlightCScrollPanel, mHighlightCppScrollPanel, mHighlightBasicScrollPanel, mHighlightFSharpScrollPanel,
           mHighlightHTMLScrollPanel, mHighlightCSSScrollPanel, mHighlightXMLScrollPanel, mHighlightJSONScrollPanel,
           mHighlightPowerShellScrollPanel, mHighlightBatchScrollPanel, mHighlightSQLScrollPanel,
           mHighlightMarkdownScrollPanel, mHighlightPythonScrollPanel;
         private Size mCachedPreferredSize;
         private Theme mTemporaryTheme;

         public ThemePanel(ThemeUsage pThemeUsage, UiState pUiState) {
            SuspendLayout();
            mTemporaryTheme = mCurrentTheme!.Clone(TemporaryThemePrefix +
               DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture));
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
            mThemesHeaderCluster = new HeaderLabelCluster("Themes", HeaderLabelSize.Normal);
            mFontsHeaderCluster = new HeaderLabelCluster("Fonts", HeaderLabelSize.Small);
            mColorsHeaderCluster = new HeaderLabelCluster("Colors", HeaderLabelSize.Small);
            mInterfaceHeaderCluster = new HeaderLabelCluster("Interface", HeaderLabelSize.Small);
            mCSharpHeaderCluster = new HeaderLabelCluster("C# Token Highlight Colors", HeaderLabelSize.Small);
            mCHeaderCluster = new HeaderLabelCluster("C Token Highlight Colors", HeaderLabelSize.Small);
            mCppHeaderCluster = new HeaderLabelCluster("C++ Token Highlight Colors", HeaderLabelSize.Small);
            mBasicHeaderCluster = new HeaderLabelCluster("Basic Token Highlight Colors", HeaderLabelSize.Small);
            mFSharpHeaderCluster = new HeaderLabelCluster("F# Token Highlight Colors", HeaderLabelSize.Small);
            mHTMLHeaderCluster = new HeaderLabelCluster("HTML Token Highlight Colors", HeaderLabelSize.Small);
            mCSSHeaderCluster = new HeaderLabelCluster("CSS Token Highlight Colors", HeaderLabelSize.Small);
            mXMLHeaderCluster = new HeaderLabelCluster("XML Token Highlight Colors", HeaderLabelSize.Small);
            mJSONHeaderCluster = new HeaderLabelCluster("JSON Token Highlight Colors", HeaderLabelSize.Small);
            mPowerShellHeaderCluster = new HeaderLabelCluster("PowerShell Token Highlight Colors", HeaderLabelSize.Small);
            mBatchHeaderCluster = new HeaderLabelCluster("Batch Token Highlight Colors", HeaderLabelSize.Small);
            mSQLHeaderCluster = new HeaderLabelCluster("SQL Token Highlight Colors", HeaderLabelSize.Small);
            mMarkdownHeaderCluster = new HeaderLabelCluster("Markdown Token Highlight Colors", HeaderLabelSize.Small);
            mPythonHeaderCluster = new HeaderLabelCluster("Python Token Highlight Colors", HeaderLabelSize.Small);
            mStatusStrip = new StatusStrip();
            mApplyHost = new ToolStripControlHost(mApplyButton);
            mCancelHost = new ToolStripControlHost(mCancelButton);
            mHelpHost = new ToolStripControlHost(mHelpButton);
            mOkHost = new ToolStripControlHost(mOkButton);
            mNewHost = new ToolStripControlHost(mNewButton);
            mCloneHost = new ToolStripControlHost(mCloneButton);
            mPrimaryTabControl = new VariableWidthTabControl();
            mPrimaryTabControl.TabPages.AddRange([new TabPage("Fonts"), new TabPage("Colors")]);
            mHighlightTabControl = new VariableWidthTabControl();
            mHighlightTabControl.TabPages.AddRange([new TabPage("Interface"), new TabPage("C#"), new TabPage("C"), new TabPage("C++"),
               new TabPage("Basic"), new TabPage("F#"), new TabPage("HTML"), new TabPage("CSS"), new TabPage("XML"), new TabPage("JSON"),
               new TabPage("Power Shell"), new TabPage("Batch"), new TabPage("SQL"), new TabPage("Markdown"), new TabPage("Python")]);
            mPrimaryTabControl.Dock = DockStyle.Fill;
            mHighlightTabControl.Dock = DockStyle.Fill;
            mPrimaryTabControl.SetStripBackColor(mTemporaryTheme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground]);
            mHighlightTabControl.SetStripBackColor(mTemporaryTheme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground]);
            mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Color].Controls.Add(mHighlightTabControl);
            mPrimaryTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            mHighlightTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            mPrimaryTabControl.DrawItem += PrimaryTabControl_DrawItem;
            mHighlightTabControl.DrawItem += HighlightTabControl_DrawItem;
            mPrimaryTabControl.SelectedIndex = mUiState.mThemePrimaryTabPageIndex;
            mHighlightTabControl.SelectedIndex = mUiState.mThemeHighlightTabPageIndex;
            mPrimaryTabControl.SelectedIndexChanged += PrimaryTabControl_SelectedIndexChanged;
            mHighlightTabControl.SelectedIndexChanged += HighlightTabControl_SelectedIndexChanged;
            mPrimaryTabControl.ResetStripBackgroundPainted();
            mHighlightTabControl.ResetStripBackgroundPainted();
            mPrimaryTabControl.SetStripBackColor(mTemporaryTheme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground]);
            mHighlightTabControl.SetStripBackColor(mTemporaryTheme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground]);
            mStatusStrip.SizingGrip = true;
            mStatusStrip.Dock = DockStyle.Bottom;
            mSpringLabel = new ToolStripStatusLabel {
               Spring = true
            };
            mHelpButton.Text = "Help";
            mApplyButton.Text = "Apply";
            mOkButton.Text = "OK";
            mCancelButton.Text = "Cancel";
            mNewButton.Text = "New";
            mCloneButton.Text = "Clone";
            mStatusStrip.Items.AddRange(mHelpHost, mNewHost, mCloneHost, mSpringLabel, mApplyHost, mOkHost, mCancelHost);
            mApplyButton.Click += ApplyButton_Click;
            mCancelButton.Click += CancelButton_Click;
            mHelpButton.Click += MainForm.Help_Click;
            mOkButton.Click += OkButton_Click;
            mNewButton.Click += NewButton_Click;
            mCloneButton.Click += CloneButton_Click;
            Controls.AddRange(mPrimaryTabControl, mStatusStrip, mThemesHeaderCluster);
            AddFontCluster(mFontsClusters, "The Interface Font:", "Interface", FontUsage.Interface, LabelPosition.Left);
            AddFontCluster(mFontsClusters, "The Menu Font:", "Menu", FontUsage.Menu, LabelPosition.Left);
            AddFontCluster(mFontsClusters, "The Status Strip Font:", "Status Strip", FontUsage.Status, LabelPosition.Left);
            AddFontCluster(mFontsClusters, "The Textbox Font:", "Text Box", FontUsage.Text, LabelPosition.Left);
            mPrimaryScrollPanel = new Panel {
               Name = $"PrimaryTabControlTabPageScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mFontsContainer = new ClusterContainer(mFontsClusters, ClusterLayoutMode.FixedRows, 0, 0, 0, 4) {
               Dock = DockStyle.Fill,
               Name = "InterfaceColorsClusterContainer",
               AutoScroll = true
            };
            mPrimaryScrollPanel.Controls.AddRange(mFontsHeaderCluster, mFontsContainer);
            mHighlightInterfaceScrollPanel = new Panel {
               Name = $"InterfaceColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Interface].Controls.Add(mHighlightInterfaceScrollPanel);
            AddColorCluster(mInterfaceColorClusters, "Panel Background", ColorSwatchUsage.PanelBackground);
            AddColorCluster(mInterfaceColorClusters, "TextBox Background", ColorSwatchUsage.TextBox);
            AddColorCluster(mInterfaceColorClusters, "TextBox Font", ColorSwatchUsage.TextBoxFont);
            AddColorCluster(mInterfaceColorClusters, "Menu Background", ColorSwatchUsage.MenuBackground);
            AddColorCluster(mInterfaceColorClusters, "Menu Font", ColorSwatchUsage.MenuFont);
            AddColorCluster(mInterfaceColorClusters, "Interface Background", ColorSwatchUsage.InterfaceBackground);
            AddColorCluster(mInterfaceColorClusters, "Interface Font", ColorSwatchUsage.InterfaceFont);
            AddColorCluster(mInterfaceColorClusters, "Status Background", ColorSwatchUsage.StatusBackground);
            AddColorCluster(mInterfaceColorClusters, "Status Font", ColorSwatchUsage.StatusFont);
            AddColorCluster(mInterfaceColorClusters, "GroupBox Background", ColorSwatchUsage.GroupBoxBackground);
            AddColorCluster(mInterfaceColorClusters, "GroupBox Font", ColorSwatchUsage.GroupBoxFont);
            AddColorCluster(mInterfaceColorClusters, "Tab Header Unselected Font", ColorSwatchUsage.TabHeaderUnselectedFont);
            AddColorCluster(mInterfaceColorClusters, "Tab Header Selected Font", ColorSwatchUsage.TabHeaderSelectedFont);
            AddColorCluster(mInterfaceColorClusters, "Tab Header Unselected Background", ColorSwatchUsage.TabHeaderUnselectedBackground);
            AddColorCluster(mInterfaceColorClusters, "Tab Header Selected Background", ColorSwatchUsage.TabHeaderSelectedBackground);
            mInterfaceColorsContainer = new ClusterContainer(mInterfaceColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "InterfaceColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightInterfaceScrollPanel.Controls.AddRange(mInterfaceHeaderCluster, mInterfaceColorsContainer);
            mHighlightCSharpScrollPanel = new Panel {
               Name = $"CSharpColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.CSharp].Controls.Add(mHighlightCSharpScrollPanel);
            AddColorCluster(mCSharpcolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mCSharpcolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mCSharpcolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mCSharpcolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mCSharpcolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mCSharpcolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mCSharpcolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mCSharpcolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mCSharpcolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mCSharpcolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mCSharpcolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mCSharpColorsContainer = new ClusterContainer(mCSharpcolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "CSharpColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightCSharpScrollPanel.Controls.AddRange(mCSharpHeaderCluster, mCSharpColorsContainer);

            mHighlightCScrollPanel = new Panel {
               Name = $"CColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.C].Controls.Add(mHighlightCScrollPanel);
            AddColorCluster(mCcolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mCcolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mCcolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mCcolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mCcolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mCcolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mCcolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mCcolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mCcolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mCcolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mCcolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mCColorsContainer = new ClusterContainer(mCcolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "CColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightCScrollPanel.Controls.AddRange(mCHeaderCluster, mCColorsContainer);

            mHighlightCppScrollPanel = new Panel {
               Name = $"CppColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Cpp].Controls.Add(mHighlightCppScrollPanel);
            AddColorCluster(mCppcolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mCppcolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mCppcolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mCppcolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mCppcolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mCppcolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mCppcolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mCppcolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mCppcolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mCppcolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mCppcolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mCppColorsContainer = new ClusterContainer(mCppcolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "CppColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightCppScrollPanel.Controls.AddRange(mCppHeaderCluster, mCppColorsContainer);

            mHighlightBasicScrollPanel = new Panel {
               Name = $"BasicColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Basic].Controls.Add(mHighlightBasicScrollPanel);
            AddColorCluster(mBasiccolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mBasiccolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mBasiccolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mBasiccolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mBasiccolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mBasiccolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mBasiccolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mBasiccolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mBasiccolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mBasiccolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mBasiccolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mBasicColorsContainer = new ClusterContainer(mBasiccolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "BasicColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightBasicScrollPanel.Controls.AddRange(mBasicHeaderCluster, mBasicColorsContainer);

            mHighlightFSharpScrollPanel = new Panel {
               Name = $"FSharpColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.FSharp].Controls.Add(mHighlightFSharpScrollPanel);
            AddColorCluster(mFSharpcolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mFSharpcolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mFSharpcolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mFSharpcolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mFSharpcolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mFSharpcolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mFSharpcolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mFSharpcolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mFSharpcolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mFSharpcolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mFSharpcolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mFSharpColorsContainer = new ClusterContainer(mFSharpcolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "FSharpColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightFSharpScrollPanel.Controls.AddRange(mFSharpHeaderCluster, mFSharpColorsContainer);

            mHighlightHTMLScrollPanel = new Panel {
               Name = $"HTMLColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.HTML].Controls.Add(mHighlightHTMLScrollPanel);
            AddColorCluster(mHTMLcolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mHTMLcolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mHTMLcolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mHTMLcolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mHTMLcolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mHTMLcolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mHTMLcolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mHTMLcolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mHTMLcolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mHTMLcolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mHTMLcolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mHTMLColorsContainer = new ClusterContainer(mHTMLcolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "HTMLColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightHTMLScrollPanel.Controls.AddRange(mHTMLHeaderCluster, mHTMLColorsContainer);

            mHighlightCSSScrollPanel = new Panel {
               Name = $"CSSColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.CSS].Controls.Add(mHighlightCSSScrollPanel);
            AddColorCluster(mCSScolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mCSScolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mCSScolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mCSScolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mCSScolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mCSScolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mCSScolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mCSScolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mCSScolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mCSScolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mCSScolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mCSSColorsContainer = new ClusterContainer(mCSScolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "CSSColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightCSSScrollPanel.Controls.AddRange(mCSSHeaderCluster, mCSSColorsContainer);

            mHighlightXMLScrollPanel = new Panel {
               Name = $"XMLColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.XML].Controls.Add(mHighlightXMLScrollPanel);
            AddColorCluster(mXMLcolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mXMLcolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mXMLcolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mXMLcolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mXMLcolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mXMLcolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mXMLcolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mXMLcolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mXMLcolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mXMLcolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mXMLcolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mXMLColorsContainer = new ClusterContainer(mXMLcolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "XMLColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightXMLScrollPanel.Controls.AddRange(mXMLHeaderCluster, mXMLColorsContainer);

            mHighlightJSONScrollPanel = new Panel {
               Name = $"JSONColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.JSON].Controls.Add(mHighlightJSONScrollPanel);
            AddColorCluster(mJSONcolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mJSONcolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mJSONcolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mJSONcolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mJSONcolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mJSONcolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mJSONcolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mJSONcolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mJSONcolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mJSONcolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mJSONcolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mJSONColorsContainer = new ClusterContainer(mJSONcolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "JSONColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightJSONScrollPanel.Controls.AddRange(mJSONHeaderCluster, mJSONColorsContainer);

            mHighlightPowerShellScrollPanel = new Panel {
               Name = $"PowerShellColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.PowerShell].Controls.Add(mHighlightPowerShellScrollPanel);
            AddColorCluster(mPowerShellcolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mPowerShellcolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mPowerShellcolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mPowerShellcolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mPowerShellcolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mPowerShellcolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mPowerShellcolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mPowerShellcolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mPowerShellcolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mPowerShellcolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mPowerShellcolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mPowerShellColorsContainer = new ClusterContainer(mPowerShellcolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "PowerShellColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightPowerShellScrollPanel.Controls.AddRange(mPowerShellHeaderCluster, mPowerShellColorsContainer);

            mHighlightBatchScrollPanel = new Panel {
               Name = $"BatchColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Batch].Controls.Add(mHighlightBatchScrollPanel);
            AddColorCluster(mBatchcolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mBatchcolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mBatchcolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mBatchcolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mBatchcolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mBatchcolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mBatchcolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mBatchcolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mBatchcolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mBatchcolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mBatchcolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mBatchColorsContainer = new ClusterContainer(mBatchcolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "BatchColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightBatchScrollPanel.Controls.AddRange(mBatchHeaderCluster, mBatchColorsContainer);

            mHighlightSQLScrollPanel = new Panel {
               Name = $"SQLColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.SQL].Controls.Add(mHighlightSQLScrollPanel);
            AddColorCluster(mSQLcolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mSQLcolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mSQLcolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mSQLcolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mSQLcolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mSQLcolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mSQLcolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mSQLcolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mSQLcolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mSQLcolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mSQLcolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mSQLColorsContainer = new ClusterContainer(mSQLcolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "SQLColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightSQLScrollPanel.Controls.AddRange(mSQLHeaderCluster, mSQLColorsContainer);

            mHighlightMarkdownScrollPanel = new Panel {
               Name = $"MarkdownColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Markdown].Controls.Add(mHighlightMarkdownScrollPanel);
            AddColorCluster(mMarkdowncolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mMarkdowncolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mMarkdowncolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mMarkdowncolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mMarkdowncolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mMarkdowncolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mMarkdowncolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mMarkdowncolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mMarkdowncolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mMarkdowncolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mMarkdowncolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mMarkdownColorsContainer = new ClusterContainer(mMarkdowncolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "MarkdownColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightMarkdownScrollPanel.Controls.AddRange(mMarkdownHeaderCluster, mMarkdownColorsContainer);

            mHighlightPythonScrollPanel = new Panel {
               Name = $"PythonColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill
            };
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Python].Controls.Add(mHighlightPythonScrollPanel);
            AddColorCluster(mPythoncolorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mPythoncolorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mPythoncolorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mPythoncolorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mPythoncolorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mPythoncolorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mPythoncolorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mPythoncolorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mPythoncolorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mPythoncolorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mPythoncolorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mPythonColorsContainer = new ClusterContainer(mPythoncolorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Dock = DockStyle.Fill,
               Name = "PythonColorsClusterContainer",
               AutoScroll = true
            };
            mHighlightPythonScrollPanel.Controls.AddRange(mPythonHeaderCluster, mPythonColorsContainer);

            Dock = DockStyle.Fill;
            ResumeLayout(false);
         }

         public Size GetPreferredContentSizeCached() {
            int maxWidth = 300, maxHeight = 300;
            List<Panel?> allScrollPanels = [ mPrimaryScrollPanel, mHighlightInterfaceScrollPanel,
            mHighlightCSharpScrollPanel, mHighlightCScrollPanel, mHighlightCppScrollPanel, mHighlightBasicScrollPanel,
            mHighlightFSharpScrollPanel, mHighlightHTMLScrollPanel, mHighlightCSSScrollPanel, mHighlightXMLScrollPanel,
            mHighlightJSONScrollPanel, mHighlightPowerShellScrollPanel, mHighlightBatchScrollPanel,
            mHighlightSQLScrollPanel, mHighlightMarkdownScrollPanel, mHighlightPythonScrollPanel ];

            // Measure all scroll panels and find the maximum dimensions
            foreach (Panel? scrollPanel in allScrollPanels) {
               if (scrollPanel != null && scrollPanel.Controls.Count > 0) {
                  Control? rightmost = Rightmost(ControlCollectionAsList(scrollPanel.Controls));
                  Control? bottommost = Bottommost(ControlCollectionAsList(scrollPanel.Controls));

                  if (rightmost != null && rightmost.Right > maxWidth)
                     maxWidth = rightmost.Right;
                  if (bottommost != null && bottommost.Bottom > maxHeight)
                     maxHeight = bottommost.Bottom;
               }
            }
            int primaryTabStripHeight = mPrimaryTabControl.ItemSize.Height + 4;
            int highlightTabStripHeight = mHighlightTabControl.ItemSize.Height + 4;
            int wantedWidth = maxWidth + SystemInformation.VerticalScrollBarWidth;
            int wantedHeight = maxHeight + SystemInformation.HorizontalScrollBarHeight + mThemesHeaderCluster.Height + mStatusStrip.Height +
               primaryTabStripHeight + highlightTabStripHeight + mEm2;

            if (wantedWidth < 300)
               wantedWidth = 300;
            if (wantedHeight < 300)
               wantedHeight = 300;
            return new Size(wantedWidth, wantedHeight);
         }

         public void LayoutControls() {
            SuspendLayout();
            ApplyThemeToPanel();
            LayoutClusters();
            ResumeLayout(true);
         }

         private void LayoutClusters() {
            foreach (BaseCluster cluster in mFontsClusters.OfType<BaseCluster>())
               cluster.LayoutCluster(mTemporaryTheme);
         }

         private void AddFontCluster(List<BaseCluster> pClusters, string pLabelText, string pButtonText, FontUsage pUsage,
            LabelPosition pLabelPosition = LabelPosition.Left) {
            LabeledButtonTextBoxCluster cluster = new LabeledButtonTextBoxCluster(pLabelText, pButtonText,
               pLabelPosition) {
               Tag = pUsage
            };
            cluster.mButton.Tag = pUsage;
            cluster.mButton.Click += OnFontButtonClicked;
            pClusters.Add(cluster);
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
            List<LabeledButtonColorSwatchCluster> clusters = [];
            foreach (ColorUsage usage in Enum.GetValues<ColorUsage>()) {
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
            mStatusStrip.BackColor = theme.mInterfaceColors[(int)ColorUsage.StatusBackground];
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
         }

         private void ApplyThemeToControlTree(Control pParent) {
            Theme theme = mTemporaryTheme;
            foreach (Control control in pParent.Controls) {
               if (control is BaseCluster cluster && cluster.mSkipTheme) {
                  ApplyThemeToControlTree(control);
                  continue;
               }
               control.ForeColor = theme.mInterfaceColors[(int)ColorUsage.InterfaceFont];
               if (!(control is BaseCluster))
                  control.BackColor = theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground];
               control.Font = theme.mFonts[(int)FontUsage.Interface];
               ApplyThemeToControlTree(control);
            }
         }

         public bool ThemeIsDirty() {
            return mThemeIsDirty;
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
               mColorPickerPanel = new ColorPickerPanel(pTheme, pUsage, pInitialColor);
            else
               mColorPickerPanel.LayoutControls();
            if (!mFirstColorPicker)
               Bounds = mColorPickerBounds;
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
               mForm.ClientSize = new Size(width, height);
               Point center = ScreenCenterPrimary();
               mForm.Location = new Point(center.X - (width / 2), center.Y - (height / 2));
               EnsureWindowFitsMonitor(mForm, false);
               mFirstColorPicker = false;
            }
            else {
               EnsureWindowFitsMonitor(mForm, false);
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
            mColorPickerBounds = mForm.Bounds;
            mForm.Bounds = mPrePickerBounds;
            if (!mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Add(mThemePanel);
            if (mColorPickerPanel.ColorHasChanged()) {
               mThemePanel.ApplyThemeToPanel();
               mThemePanel.Invalidate(true);
            }
         }

         public void EnsureFontPickerPanel(Theme pTheme, FontUsage pUsage, Font pInitialFont) {
            mPrePickerBounds = Bounds;
            if (mFontPickerPanel == null)
               mFontPickerPanel = new FontPickerPanel(pTheme, pUsage, pInitialFont);
            else
               mFontPickerPanel.LayoutControls();
            if (!mFirstFontPicker)
               Bounds = mFontPickerBounds;
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
               mForm.ClientSize = new Size(width, height);
               Point center = ScreenCenterPrimary();
               mForm.Location = new Point(center.X - (width / 2), center.Y - (height / 2));
               EnsureWindowFitsMonitor(mForm, false);
               mFirstFontPicker = false;
            }
            else {
               EnsureWindowFitsMonitor(mForm, false);
            }
         }

         public static void RestoreFromFontPickerPanel() {
            ThrowIfNull(mForm, nameof(mForm));
            ThrowIfNull(mFontPickerPanel, nameof(mFontPickerPanel));
            ThrowIfNull(mThemePanel, nameof(mThemePanel));

            mFontPickerPanel.Visible = false;
            mFontPickerPanel.SendToBack();
            if (mForm.Controls.Contains(mFontPickerPanel))
               mForm.Controls.Remove(mFontPickerPanel);
            mFontPickerBounds = mForm.Bounds;
            mForm.Bounds = mPrePickerBounds;
            if (!mForm.Controls.Contains(mThemePanel))
               mForm.Controls.Add(mThemePanel);
            if (mFontPickerPanel.FontHasChanged()) {
               mThemePanel.ApplyThemeToPanel();
               mThemePanel.Invalidate(true);
            }
         }

         private void CloseThemePanel() {
            mFirstTheme = false;
            ThrowIfNull(mForm, nameof(mForm));

            mThemeBounds = mForm.Bounds;
            mForm.RestoreFromThemePanel();
         }
      }
   }
}
