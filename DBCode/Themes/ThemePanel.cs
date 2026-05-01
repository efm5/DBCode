using System.Globalization;

namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePanel : Panel {
         private const string TemporaryThemePrefix = "\u26A0 TEMPORARY THEME \u26A0 ";
         private List<BaseCluster> mFontsClusters = [], mInterfaceColorClusters = [],
            mCSharpColorClusters = [], mCColorClusters = [], mCppColorClusters = [],
            mBasicColorClusters = [], mFSharpColorClusters = [],
            mHTMLColorClusters = [], mCSSColorClusters = [], mXMLColorClusters = [],
            mJSONColorClusters = [], mPowerShellColorClusters = [],
            mBatchColorClusters = [], mSQLColorClusters = [],
            mMarkdownColorClusters = [], mPythonColorClusters = [],
            mExamplesClusters = [];
         private readonly List<List<BaseCluster>> mAllClusters = [];
         public static bool mRepaint = false;
         //public static BottomPanel mThemeBottomPanel;//DEBUG efm5 2026 04 30 eventually implement
         private readonly Button mApplyButton, mCancelButton, mCloneButton, mHelpButton, mNewButton;
         private ClusterContainer mExamplesContainer, mFontsContainer, mInterfaceColorsContainer, mCSharpColorsContainer,
            mCColorsContainer, mCppColorsContainer, mBasicColorsContainer, mFSharpColorsContainer, mHTMLColorsContainer,
            mCSSColorsContainer, mXMLColorsContainer, mJSONColorsContainer, mPowerShellColorsContainer,
            mBatchColorsContainer, mSQLColorsContainer, mMarkdownColorsContainer, mPythonColorsContainer;
         private readonly List<ClusterContainer> mClusterContainers = [];
         private readonly DataGridView mIncludeDataGridView, mExcludeDataGridView;
         private readonly HeaderLabelCluster mInterfaceHeaderCluster, mThemesHeaderCluster, mTargetingHeaderCluster,
            mIncludeHeaderCluster, mExcludeHeaderCluster, mCSharpHeaderCluster,
            mCHeaderCluster, mCppHeaderCluster, mBasicHeaderCluster, mFSharpHeaderCluster, mHTMLHeaderCluster,
            mCSSHeaderCluster, mXMLHeaderCluster, mJSONHeaderCluster, mPowerShellHeaderCluster, mBatchHeaderCluster,
            mSQLHeaderCluster, mMarkdownHeaderCluster, mPythonHeaderCluster, mExamplesHeaderCluster;
         private readonly Button mExampleButton, mExampleHostButton;
         private readonly MenuStrip mExampleMenuStrip;
         private readonly ToolStripMenuItem mExampleTSMI, mExampleTSMISubItem;
         private readonly ToolStripControlHost mExampleStatusButtonHost;
         private readonly ToolStripStatusLabel mExampleStatusLabel;
         private readonly StatusStrip mExampleStatusStrip;
         private readonly GroupBox mExampleGroupBox;
         private readonly CheckBox mExampleCheckBox;
         private readonly RichTextBox mExampleRichTextBox;
         private readonly RadioButton mExampleRadioButton;
         private readonly RichTextBox[] mExampleRichTextBoxs;
         private readonly RichTextBoxCluster[] mExampleRichTextBoxClusters;
         private readonly StatusStrip mStatusStrip;
#pragma warning disable IDE0300
         private static readonly string[] mLanguageExamples = new string[] {
    // CSharp
    @"// C# example
using System.Collections.Generic;

namespace Example {
    /// <summary>A sample class.</summary>
    public class Calculator {
        private const double Pi = 3.14159;

        public int Add(int a, int b) => a + b;

        public static List<int> Range(int start, int count) {
            List<int> result = [];
            for (int i = start; i < start + count; i++)
                result.Add(i);
            return result;
        }
    }
}",
    // C
    @"/* C example */
#include <stdio.h>
#include <stdlib.h>

#define MAX_SIZE 100

typedef struct {
    int x, y;
} Point;

int main(int argc, char *argv[]) {
    Point p = { 3, 7 };
    double ratio = p.x / (double)p.y;
    printf(""Ratio: %.4f\n"", ratio);
    return EXIT_SUCCESS;
}",
    // Cpp
    @"// C++ example
#include <iostream>
#include <vector>

template<typename T>
class Stack {
    std::vector<T> mData;
public:
    void push(T value) { mData.push_back(value); }
    T pop() {
        T val = mData.back();
        mData.pop_back();
        return val;
    }
    bool empty() const { return mData.empty(); }
};

int main() {
    Stack<int> s;
    s.push(42);
    std::cout << s.pop() << std::endl;
}",
    // Basic
    @"' Basic example
Dim numbers(4) As Integer
Dim total As Integer = 0

For i As Integer = 0 To 4
    numbers(i) = i * i
    total += numbers(i)
Next i

Dim avg As Double = total / 5.0
Console.WriteLine($""Average: {avg:F2}"")",
    // FSharp
    @"// F# example
let isPrime n =
    if n < 2 then false
    else
        let limit = int (sqrt (float n))
        [ 2 .. limit ] |> List.forall (fun i -> n % i <> 0)

let primes = [ 1 .. 50 ] |> List.filter isPrime
printfn ""Primes up to 50: %A"" primes",
    // Html
    @"<!-- HTML example -->
<!DOCTYPE html>
<html lang=""en"">
  <head>
    <meta charset=""UTF-8"" />
    <title>Sample Page</title>
    <link rel=""stylesheet"" href=""styles.css"" />
  </head>
  <body>
    <h1 class=""title"">Hello, World!</h1>
    <p>Visit <a href=""https://example.com"">example.com</a> for details.</p>
    <img src=""logo.png"" alt=""Logo"" width=""200"" />
  </body>
</html>",
    // Css
    @"/* CSS example */
:root {
    --primary: #3a7bd5;
    --radius: 4px;
}

body {
    font-family: 'Segoe UI', sans-serif;
    background-color: #f0f0f0;
    margin: 0;
    padding: 1rem;
}

.card:hover {
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
    transform: translateY(-2px);
    transition: all 0.3s ease;
}",
    // Xml
    @"<!-- XML example -->
<?xml version=""1.0"" encoding=""UTF-8""?>
<library version=""2"">
  <book id=""1"" available=""true"">
    <title>The Pragmatic Programmer</title>
    <author>Hunt &amp; Thomas</author>
    <price currency=""USD"">49.99</price>
  </book>
</library>",
    // Json
    @"{
    ""name"": ""DBCode"",
    ""version"": ""1.0.0"",
    ""settings"": {
        ""theme"": ""Dark"",
        ""fontSize"": 14,
        ""wordWrap"": true
    },
    ""languages"": [ ""C#"", ""C"", ""C++"", ""Python"" ],
    ""license"": null
}",
    // PowerShell
    @"# PowerShell example
param(
    [string]$Path = ""."",
    [int]$MaxDepth = 2
)

function Get-LargeFiles {
    param([string]$Dir, [long]$MinBytes = 1MB)
    Get-ChildItem -Path $Dir -Recurse -File |
        Where-Object { $_.Length -gt $MinBytes } |
        Sort-Object Length -Descending
}

$files = Get-LargeFiles -Dir $Path
$files | ForEach-Object { Write-Host ""$($_.Name): $($_.Length / 1MB) MB"" }",
    // Batch
    @"@ECHO OFF
REM Batch example
SETLOCAL ENABLEDELAYEDEXPANSION

SET count=0
FOR %%F IN (*.txt) DO (
    SET /A count+=1
    ECHO Found: %%F
)

IF %count% EQU 0 (
    ECHO No text files found.
) ELSE (
    ECHO Total files: %count%
)
ENDLOCAL",
    // Sql
    @"-- SQL example
CREATE TABLE Employees (
    Id       INT           PRIMARY KEY IDENTITY,
    Name     NVARCHAR(100) NOT NULL,
    Salary   DECIMAL(10,2) DEFAULT 0.00,
    HireDate DATE
);

SELECT
    e.Name,
    e.Salary,
    DATEDIFF(YEAR, e.HireDate, GETDATE()) AS YearsEmployed
FROM Employees e
WHERE e.Salary > 50000
ORDER BY e.Salary DESC;",
    // Markdown
    @"# Markdown example

## Overview
This is a **bold** statement with *italic* and ~~strikethrough~~ text.

### Code
Inline `code` and a fenced block:

- Item one
- Item **two**

> Blockquote with a [link](https://example.com)",
    // Python
    @"# Python example
from typing import Generator

def fibonacci(limit: int) -> Generator[int, None, None]:
    """"""Yield Fibonacci numbers up to limit.""""""
    a, b = 0, 1
    while a <= limit:
        yield a
        a, b = b, a + b

primes = [n for n in range(2, 50)
          if all(n % i != 0 for i in range(2, n))]

print(f""Fibs: {list(fibonacci(100))}"")
print(f""Primes: {primes}"")",
    // PlainText
    @"Plain text example - no syntax highlighting is applied to this content.
All text appears in the default foreground color."
};
#pragma warning restore IDE0300
         private readonly ToolStripControlHost mApplyHost, mCancelHost, mCloneHost, mHelpHost, mNewHost;
         private readonly ToolStripStatusLabel mSpringLabel;
         private readonly VariableWidthTabControl mPrimaryTabControl, mHighlightTabControl, mIncludeExcludeTabControl;
         private bool mThemeIsDirty = false;
         private Panel? mPrimaryScrollPanel, mHighlightInterfaceScrollPanel, mExampleScrollPanel, mHighlightCSharpScrollPanel,
           mHighlightCScrollPanel, mHighlightCppScrollPanel, mHighlightBasicScrollPanel, mHighlightFSharpScrollPanel,
           mHighlightHTMLScrollPanel, mHighlightCSSScrollPanel, mHighlightXMLScrollPanel, mHighlightJSONScrollPanel,
           mHighlightPowerShellScrollPanel, mHighlightBatchScrollPanel, mHighlightSQLScrollPanel,
           mHighlightMarkdownScrollPanel, mHighlightPythonScrollPanel, mIncludeScrollPanel, mExcludeScrollPanel;
         private readonly List<Panel?> mAllScrollPanels = [];
         private Theme mTemporaryTheme;
         private ThemeUsage mThemeUsage;

         public ThemePanel(ThemeUsage pThemeUsage) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            ThrowIfNull(mForm, nameof(mForm));
            SuspendLayout();
            string temporaryName = TemporaryThemePrefix + DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);

            mThemeUsage = pThemeUsage;
            Dock = DockStyle.Fill;
            if (mCurrentTheme.mIsBuiltIn)
               temporaryName += " CLONED FROM " + mCurrentTheme.mName;
            mTemporaryTheme = mCurrentTheme.Clone(temporaryName);
            AutoScroll = true;
            AutoSize = false;
            BackColor = Color.Transparent;
            mApplyButton = new Button();
            mCancelButton = new Button();
            mHelpButton = new Button() { Tag = new HelpTag(HelpContext.Theme, ToDescription(pThemeUsage)) };
            mNewButton = new Button();
            mCloneButton = new Button();
            mThemesHeaderCluster = new HeaderLabelCluster(mTemporaryTheme,
               $"Current Theme’s Name: “{mCurrentTheme.mName}”", HeaderLabelSize.Normal);
            mTargetingHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "Targeting", HeaderLabelSize.Small);
            mIncludeHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "Windows To Include While Transferring", HeaderLabelSize.Small);
            mExcludeHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "Windows To Exclude While Transferring", HeaderLabelSize.Small);
            mInterfaceHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "Interface Colors", HeaderLabelSize.Small);
            mExamplesHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "Examples", HeaderLabelSize.Small);
            mCSharpHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "C# Token Highlight Colors", HeaderLabelSize.Small);
            mCHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "C Token Highlight Colors", HeaderLabelSize.Small);
            mCppHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "C++ Token Highlight Colors", HeaderLabelSize.Small);
            mBasicHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "Basic Token Highlight Colors", HeaderLabelSize.Small);
            mFSharpHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "F# Token Highlight Colors", HeaderLabelSize.Small);
            mHTMLHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "HTML Token Highlight Colors", HeaderLabelSize.Small);
            mCSSHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "CSS Token Highlight Colors", HeaderLabelSize.Small);
            mXMLHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "XML Token Highlight Colors", HeaderLabelSize.Small);
            mJSONHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "JSON Token Highlight Colors", HeaderLabelSize.Small);
            mPowerShellHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "PowerShell Token Highlight Colors", HeaderLabelSize.Small);
            mBatchHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "Batch Token Highlight Colors", HeaderLabelSize.Small);
            mSQLHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "SQL Token Highlight Colors", HeaderLabelSize.Small);
            mMarkdownHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "Markdown Token Highlight Colors", HeaderLabelSize.Small);
            mPythonHeaderCluster = new HeaderLabelCluster(mTemporaryTheme, "Python Token Highlight Colors", HeaderLabelSize.Small);
            mIncludeDataGridView = new DataGridView {
               Name = "IncludeDataGridView",
               AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
               AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
               AllowUserToAddRows = true,
               AllowUserToDeleteRows = true,
               AllowUserToResizeRows = false,
               RowHeadersVisible = false,
               ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
               SelectionMode = DataGridViewSelectionMode.FullRowSelect,
               MultiSelect = true,
               Dock = DockStyle.Fill
            };
            mExcludeDataGridView = new DataGridView {
               Name = "ExcludeDataGridView",
               AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
               AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
               AllowUserToAddRows = true,
               AllowUserToDeleteRows = true,
               AllowUserToResizeRows = false,
               RowHeadersVisible = false,
               ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
               SelectionMode = DataGridViewSelectionMode.FullRowSelect,
               MultiSelect = true,
               Dock = DockStyle.Fill
            };
            mIncludeDataGridView.ColumnHeadersVisible = false;
            mExcludeDataGridView.ColumnHeadersVisible = false;
            mIncludeDataGridView.RowTemplate.Height = 25;
            mExcludeDataGridView.RowTemplate.Height = 25;// Add columns
            mIncludeDataGridView.Columns.Add(new DataGridViewTextBoxColumn {
               HeaderText = "Include",
               Name = "IncludeColumn"
            });

            mExcludeDataGridView.Columns.Add(new DataGridViewTextBoxColumn {
               HeaderText = "Exclude",
               Name = "ExcludeColumn"
            });

            // Add 10 empty rows to each
            for (int i = 0; i < 10; i++) {
               mIncludeDataGridView.Rows.Add("");
               mExcludeDataGridView.Rows.Add("");
            }
            mStatusStrip = new StatusStrip();
            mApplyHost = new ToolStripControlHost(mApplyButton);
            mCancelHost = new ToolStripControlHost(mCancelButton);
            mHelpHost = new ToolStripControlHost(mHelpButton);
            mNewHost = new ToolStripControlHost(mNewButton);
            mCloneHost = new ToolStripControlHost(mCloneButton);
            mPrimaryTabControl = new VariableWidthTabControl();
            mPrimaryTabControl.TabPages.AddRange([new TabPage("Fonts"), new TabPage("Colors"), new TabPage("Targeting"), new TabPage("Examples")]);
            mIncludeExcludeTabControl = new VariableWidthTabControl();
            mIncludeExcludeTabControl.TabPages.AddRange([new TabPage("Inclusions"), new TabPage("Exclusions")]);
            mHighlightTabControl = new VariableWidthTabControl();
            mHighlightTabControl.TabPages.AddRange([new TabPage("Interface"), new TabPage("C#"), new TabPage("C"), new TabPage("C++"), new TabPage("Basic"), new TabPage("F#"), new TabPage("HTML"), new TabPage("CSS"), new TabPage("XML"), new TabPage("JSON"), new TabPage("Power Shell"), new TabPage("Batch"), new TabPage("SQL"), new TabPage("Markdown"), new TabPage("Python")]);
            mPrimaryTabControl.Dock = DockStyle.Fill;
            mIncludeExcludeTabControl.Dock = DockStyle.Fill;
            mHighlightTabControl.Dock = DockStyle.Fill;
            mPrimaryTabControl.SetStripBackColor(mTemporaryTheme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground]);
            mIncludeExcludeTabControl.SetStripBackColor(mTemporaryTheme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground]);
            mHighlightTabControl.SetStripBackColor(mTemporaryTheme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground]);
            mPrimaryTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            mIncludeExcludeTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            mHighlightTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            mPrimaryTabControl.DrawItem += PrimaryTabControl_DrawItem;
            mIncludeExcludeTabControl.DrawItem += IncludeExcludeTabControl_DrawItem;
            mHighlightTabControl.DrawItem += HighlightTabControl_DrawItem;
            mPrimaryTabControl.SelectedIndexChanged += PrimaryTabControl_SelectedIndexChanged;
            mIncludeExcludeTabControl.SelectedIndexChanged += IncludeExcludeTabControl_SelectedIndexChanged;
            mHighlightTabControl.SelectedIndexChanged += HighlightTabControl_SelectedIndexChanged;
            mStatusStrip.SizingGrip = true;
            mStatusStrip.Dock = DockStyle.Bottom;
            mSpringLabel = new ToolStripStatusLabel {
               Spring = true
            };
            mHelpButton.Text = "&Help";
            mApplyButton.Text = "&Apply";
            mCancelButton.Text = "&Cancel";
            mNewButton.Text = "&New";
            mCloneButton.Text = "C&lone";
            mStatusStrip.Items.AddRange([mHelpHost, mNewHost, mCloneHost, mSpringLabel, mApplyHost, mCancelHost]);
            mApplyButton.Click += ApplyButton_Click;
            mCancelButton.Click += CancelButton_Click;
            mHelpButton.Click += MainForm.Help_Click;
            mNewButton.Click += NewButton_Click;
            mCloneButton.Click += CloneButton_Click;
            mPrimaryScrollPanel = new Panel {
               Name = $"PrimaryTabControlTabPageScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mIncludeScrollPanel = new Panel {
               Name = $"IncludeTabControlTabPageScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mExcludeScrollPanel = new Panel {
               Name = $"ExcludeTabControlTabPageScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightInterfaceScrollPanel = new Panel {
               Name = $"InterfaceColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mExampleScrollPanel = new Panel {
               Name = $"mExampleScrollPanelTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightCSharpScrollPanel = new Panel {
               Name = $"CSharpColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightCScrollPanel = new Panel {
               Name = $"CColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightCppScrollPanel = new Panel {
               Name = $"CppColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightBasicScrollPanel = new Panel {
               Name = $"BasicColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightFSharpScrollPanel = new Panel {
               Name = $"FSharpColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightHTMLScrollPanel = new Panel {
               Name = $"HTMLColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightCSSScrollPanel = new Panel {
               Name = $"CSSColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightXMLScrollPanel = new Panel {
               Name = $"XMLColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightJSONScrollPanel = new Panel {
               Name = $"JSONColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightPowerShellScrollPanel = new Panel {
               Name = $"PowerShellColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightBatchScrollPanel = new Panel {
               Name = $"BatchColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightSQLScrollPanel = new Panel {
               Name = $"SQLColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightMarkdownScrollPanel = new Panel {
               Name = $"MarkdownColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
            mHighlightPythonScrollPanel = new Panel {
               Name = $"PythonColorsTabPagesScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true
            };
#pragma warning disable CA2263
            mExampleRichTextBoxs = new RichTextBox[Enum.GetValues(typeof(DBCode.Syntax.LanguageKind)).Length];
            mExampleRichTextBoxClusters = new RichTextBoxCluster[Enum.GetValues(typeof(DBCode.Syntax.LanguageKind)).Length];
#pragma warning restore CA2263
            for (int i = 0; i < mExampleRichTextBoxClusters.Length; i++) {
               mExampleRichTextBoxClusters[i] = new RichTextBoxCluster(
                   mTemporaryTheme,
                   400,
                   Enum.GetName(typeof(DBCode.Syntax.LanguageKind), i),
                   null,
                   LabelPosition.Top
               );
               RichTextBox textBox = mExampleRichTextBoxClusters[i].GetRichTextBox;
               mExampleRichTextBoxs[i] = textBox;
               textBox.Text = mLanguageExamples[i];
               mExamplesClusters.Add(mExampleRichTextBoxClusters[i]);
            }
            string font = $"Family: {mTemporaryTheme.mFonts[(int)FontUsage.Interface].FontFamily.Name}, Size: {mTemporaryTheme.mFonts[(int)FontUsage.Interface].Size} Style: {mTemporaryTheme.mFonts[(int)FontUsage.Interface].Style}";
            AddFontCluster(mFontsClusters, $"The Interface Font: {font}", "Interface", FontUsage.Interface, LabelPosition.Right);
            font = $"Family: {mTemporaryTheme.mFonts[(int)FontUsage.Menu].FontFamily.Name}, Size: {mTemporaryTheme.mFonts[(int)FontUsage.Menu].Size} Style: {mTemporaryTheme.mFonts[(int)FontUsage.Menu].Style}";
            AddFontCluster(mFontsClusters, $"The Menu Font: {font}", "Menu", FontUsage.Menu, LabelPosition.Right);
            font = $"Family: {mTemporaryTheme.mFonts[(int)FontUsage.Status].FontFamily.Name}, Size: {mTemporaryTheme.mFonts[(int)FontUsage.Status].Size} Style: {mTemporaryTheme.mFonts[(int)FontUsage.Status].Style}";
            AddFontCluster(mFontsClusters, $"The Status Strip Font: {font}", "Status Strip", FontUsage.Status, LabelPosition.Right);
            font = $"Family: {mTemporaryTheme.mFonts[(int)FontUsage.Text].FontFamily.Name}, Size: {mTemporaryTheme.mFonts[(int)FontUsage.Text].Size} Style: {mTemporaryTheme.mFonts[(int)FontUsage.Text].Style}";
            AddFontCluster(mFontsClusters, $"The Textbox Font: {font}", "Text Box", FontUsage.Text, LabelPosition.Right);
            mFontsContainer = new ClusterContainer(mPrimaryScrollPanel, mFontsClusters, ClusterLayoutMode.FixedRows, 0, 0, 0, 4) {
               Name = "InterfaceColorsClusterContainer",
               Tag = FontUsage.Interface
            };
            mPrimaryScrollPanel.Controls.AddRange(mFontsContainer.mClusters.Cast<Control>().ToArray());//DEBUG efm5 2026 04 28 this may be a problem
#pragma warning disable IDE0017
            mExamplesContainer = new ClusterContainer(mExampleScrollPanel, mExamplesClusters, ClusterLayoutMode.FlowLayout) {
               Name = "ExamplesClusterContainer",
               Tag = "Examples"
            };
            //efm5 autosize = false may not happen in the ctor – It would be overridden by the flow layout logic, so set it explicitly here after construction
            mExamplesContainer.AutoSize = false;
#pragma warning restore IDE0017
            mExampleMenuStrip = new MenuStrip() { Name = "ExampleMenuStrip" };
            mExampleTSMI = new ToolStripMenuItem { Name = "ExampleTSMI", Text = "Example &Menu" };
            mExampleTSMISubItem = new ToolStripMenuItem { Name = "ExampleTSMISubItem", Text = "Example &Item" };
            mExampleTSMI.DropDownItems.Add(mExampleTSMISubItem);
            mExampleMenuStrip.Items.Add(mExampleTSMI);
            mExampleMenuStrip.Dock = DockStyle.Top;
            mExampleGroupBox = new GroupBox {
               Name = "ExampleGroupBox",
               Text = "Example Group",
               AutoSize = false,
               Dock = DockStyle.Top
            };
            mExampleButton = new Button {
               Name = "ExampleButton",
               Text = "&Button",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mExampleCheckBox = new CheckBox {
               Name = "ExampleCheckBox",
               Text = "Option",
               AutoSize = true
            };
            mExampleRichTextBox = new RichTextBox {
               Name = "ExampleRichTextBox",
               Text = mUnicodeSampleString,
               Multiline = true,
               ScrollBars = RichTextBoxScrollBars.Both,
               Width = 300,
               Height = mEm * 4
            };
            mExampleRadioButton = new RadioButton {
               Name = "ExampleRadioButton",
               Text = "Testing",
               AutoSize = true
            };
            mExampleGroupBox.Controls.AddRange([mExampleButton, mExampleCheckBox, mExampleRichTextBox, mExampleRadioButton]);
            mExampleStatusLabel = new ToolStripStatusLabel {
               Name = "ExampleStatusLabel",
               Text = "Status: Ready",
               Spring = true
            };
            mExampleHostButton = new Button {
               Name = "ExampleHostButton",
               Text = "&Host",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Location = new Point(1, 1)
            };
            mExampleStatusButtonHost = new ToolStripControlHost(mExampleHostButton) {
               Name = "ExampleStatusButtonHost"
            };
            mExampleStatusStrip = new StatusStrip {
               Name = "ExampleStatusStrip",
               SizingGrip = false,
               AutoSize = true,
               Dock = DockStyle.Top
            };
            mExampleStatusStrip.Items.AddRange([mExampleStatusButtonHost, mExampleStatusLabel]);
            mExampleScrollPanel.Controls.AddRange([mExamplesContainer, mExampleStatusStrip, mExampleGroupBox, mExampleMenuStrip, mExamplesHeaderCluster]);
            mIncludeExcludeTabControl.TabPages[(int)TargetingTabPageUsage.Include].Controls.Add(mIncludeScrollPanel);
            mIncludeScrollPanel.Controls.AddRange([mIncludeDataGridView, mIncludeHeaderCluster]);
            mIncludeExcludeTabControl.TabPages[(int)TargetingTabPageUsage.Exclude].Controls.Add(mExcludeScrollPanel);
            mExcludeScrollPanel.Controls.AddRange([mExcludeDataGridView, mExcludeHeaderCluster]);
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
            mInterfaceColorsContainer = new ClusterContainer(mHighlightInterfaceScrollPanel, mInterfaceColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "InterfaceColorsClusterContainer"
            };
            mHighlightInterfaceScrollPanel.Controls.AddRange([mInterfaceHeaderCluster, mInterfaceColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.CSharp].Controls.Add(mHighlightCSharpScrollPanel);
            AddColorCluster(mCSharpColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mCSharpColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mCSharpColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mCSharpColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mCSharpColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mCSharpColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mCSharpColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mCSharpColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mCSharpColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mCSharpColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mCSharpColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mCSharpColorsContainer = new ClusterContainer(mHighlightCSharpScrollPanel, mCSharpColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "CSharpColorsClusterContainer"
            };
            mHighlightCSharpScrollPanel.Controls.AddRange([mCSharpHeaderCluster, mCSharpColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.C].Controls.Add(mHighlightCScrollPanel);
            AddColorCluster(mCColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mCColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mCColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mCColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mCColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mCColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mCColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mCColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mCColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mCColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mCColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mCColorsContainer = new ClusterContainer(mHighlightCScrollPanel, mCColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "CColorsClusterContainer"
            };
            mHighlightCScrollPanel.Controls.AddRange([mCHeaderCluster, mCColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Cpp].Controls.Add(mHighlightCppScrollPanel);
            AddColorCluster(mCppColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mCppColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mCppColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mCppColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mCppColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mCppColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mCppColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mCppColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mCppColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mCppColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mCppColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mCppColorsContainer = new ClusterContainer(mHighlightCppScrollPanel, mCppColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "CppColorsClusterContainer"
            };
            mHighlightCppScrollPanel.Controls.AddRange([mCppHeaderCluster, mCppColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Basic].Controls.Add(mHighlightBasicScrollPanel);
            AddColorCluster(mBasicColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mBasicColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mBasicColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mBasicColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mBasicColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mBasicColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mBasicColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mBasicColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mBasicColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mBasicColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mBasicColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mBasicColorsContainer = new ClusterContainer(mHighlightBasicScrollPanel, mBasicColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "BasicColorsClusterContainer"
            };
            mHighlightBasicScrollPanel.Controls.AddRange([mBasicHeaderCluster, mBasicColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.FSharp].Controls.Add(mHighlightFSharpScrollPanel);
            AddColorCluster(mFSharpColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mFSharpColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mFSharpColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mFSharpColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mFSharpColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mFSharpColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mFSharpColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mFSharpColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mFSharpColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mFSharpColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mFSharpColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mFSharpColorsContainer = new ClusterContainer(mHighlightFSharpScrollPanel, mFSharpColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "FSharpColorsClusterContainer"
            };
            mHighlightFSharpScrollPanel.Controls.AddRange([mFSharpHeaderCluster, mFSharpColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.HTML].Controls.Add(mHighlightHTMLScrollPanel);
            AddColorCluster(mHTMLColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mHTMLColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mHTMLColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mHTMLColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mHTMLColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mHTMLColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mHTMLColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mHTMLColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mHTMLColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mHTMLColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mHTMLColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mHTMLColorsContainer = new ClusterContainer(mHighlightHTMLScrollPanel, mHTMLColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "HTMLColorsClusterContainer"
            };
            mHighlightHTMLScrollPanel.Controls.AddRange([mHTMLHeaderCluster, mHTMLColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.CSS].Controls.Add(mHighlightCSSScrollPanel);
            AddColorCluster(mCSSColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mCSSColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mCSSColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mCSSColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mCSSColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mCSSColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mCSSColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mCSSColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mCSSColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mCSSColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mCSSColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mCSSColorsContainer = new ClusterContainer(mHighlightCSSScrollPanel, mCSSColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "CSSColorsClusterContainer"
            };
            mHighlightCSSScrollPanel.Controls.AddRange([mCSSHeaderCluster, mCSSColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.XML].Controls.Add(mHighlightXMLScrollPanel);
            AddColorCluster(mXMLColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mXMLColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mXMLColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mXMLColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mXMLColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mXMLColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mXMLColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mXMLColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mXMLColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mXMLColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mXMLColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mXMLColorsContainer = new ClusterContainer(mHighlightXMLScrollPanel, mXMLColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "XMLColorsClusterContainer"
            };
            mHighlightXMLScrollPanel.Controls.AddRange([mXMLHeaderCluster, mXMLColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.JSON].Controls.Add(mHighlightJSONScrollPanel);
            AddColorCluster(mJSONColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mJSONColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mJSONColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mJSONColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mJSONColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mJSONColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mJSONColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mJSONColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mJSONColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mJSONColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mJSONColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mJSONColorsContainer = new ClusterContainer(mHighlightJSONScrollPanel, mJSONColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "JSONColorsClusterContainer"
            };
            mHighlightJSONScrollPanel.Controls.AddRange([mJSONHeaderCluster, mJSONColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.PowerShell].Controls.Add(mHighlightPowerShellScrollPanel);
            AddColorCluster(mPowerShellColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mPowerShellColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mPowerShellColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mPowerShellColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mPowerShellColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mPowerShellColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mPowerShellColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mPowerShellColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mPowerShellColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mPowerShellColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mPowerShellColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mPowerShellColorsContainer = new ClusterContainer(mHighlightPowerShellScrollPanel, mPowerShellColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "PowerShellColorsClusterContainer"
            };
            mHighlightPowerShellScrollPanel.Controls.AddRange([mPowerShellHeaderCluster, mPowerShellColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Batch].Controls.Add(mHighlightBatchScrollPanel);
            AddColorCluster(mBatchColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mBatchColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mBatchColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mBatchColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mBatchColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mBatchColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mBatchColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mBatchColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mBatchColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mBatchColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mBatchColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mBatchColorsContainer = new ClusterContainer(mHighlightBatchScrollPanel, mBatchColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "BatchColorsClusterContainer"
            };
            mHighlightBatchScrollPanel.Controls.AddRange([mBatchHeaderCluster, mBatchColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.SQL].Controls.Add(mHighlightSQLScrollPanel);
            AddColorCluster(mSQLColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mSQLColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mSQLColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mSQLColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mSQLColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mSQLColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mSQLColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mSQLColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mSQLColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mSQLColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mSQLColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mSQLColorsContainer = new ClusterContainer(mHighlightSQLScrollPanel, mSQLColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "SQLColorsClusterContainer"
            };
            mHighlightSQLScrollPanel.Controls.AddRange([mSQLHeaderCluster, mSQLColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Markdown].Controls.Add(mHighlightMarkdownScrollPanel);
            AddColorCluster(mMarkdownColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mMarkdownColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mMarkdownColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mMarkdownColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mMarkdownColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mMarkdownColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mMarkdownColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mMarkdownColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mMarkdownColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mMarkdownColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mMarkdownColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mMarkdownColorsContainer = new ClusterContainer(mHighlightMarkdownScrollPanel, mMarkdownColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "MarkdownColorsClusterContainer"
            };
            mHighlightMarkdownScrollPanel.Controls.AddRange([mMarkdownHeaderCluster, mMarkdownColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Python].Controls.Add(mHighlightPythonScrollPanel);
            AddColorCluster(mPythonColorClusters, "Unknown", ColorSwatchUsage.Unknown);
            AddColorCluster(mPythonColorClusters, "Whitespace", ColorSwatchUsage.Whitespace);
            AddColorCluster(mPythonColorClusters, "Identifier", ColorSwatchUsage.Identifier);
            AddColorCluster(mPythonColorClusters, "Keyword", ColorSwatchUsage.Keyword);
            AddColorCluster(mPythonColorClusters, "Number", ColorSwatchUsage.Number);
            AddColorCluster(mPythonColorClusters, "String Literal", ColorSwatchUsage.StringLiteral);
            AddColorCluster(mPythonColorClusters, "Character Literal", ColorSwatchUsage.CharLiteral);
            AddColorCluster(mPythonColorClusters, "Comment", ColorSwatchUsage.Comment);
            AddColorCluster(mPythonColorClusters, "Preprocessor Directive", ColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mPythonColorClusters, "Operator", ColorSwatchUsage.Operator);
            AddColorCluster(mPythonColorClusters, "Punctuation", ColorSwatchUsage.Punctuation);
            mPythonColorsContainer = new ClusterContainer(mHighlightPythonScrollPanel, mPythonColorClusters, ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "PythonColorsClusterContainer"
            };
            //DEBUG efm5 2026 04 30 implement Eventually
            //mThemeBottomPanel = new BottomPanel {
            //   Name = "ThemeBottomPanel",
            //   TabIndex = mTabIndex++
            //};
            //mActiveLayoutable = mThemeBottomPanel;
            mHighlightPythonScrollPanel.Controls.AddRange([mPythonHeaderCluster, mPythonColorsContainer]);
            mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Interface].Controls.Add(mPrimaryScrollPanel);
            mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Color].Controls.Add(mHighlightTabControl);
            mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Targeting].Controls.Add(mIncludeExcludeTabControl);
            mPrimaryTabControl.TabPages[(int)PrimaryTabPageUsage.Examples].Controls.Add(mExampleScrollPanel);
            mClusterContainers.AddRange([mFontsContainer, mInterfaceColorsContainer,
               mCSharpColorsContainer, mCColorsContainer, mCppColorsContainer, mBasicColorsContainer,
               mFSharpColorsContainer, mHTMLColorsContainer, mCSSColorsContainer, mXMLColorsContainer,
               mJSONColorsContainer, mPowerShellColorsContainer, mBatchColorsContainer, mSQLColorsContainer,
               mMarkdownColorsContainer, mPythonColorsContainer, mExamplesContainer]);
            mAllClusters.AddRange([mFontsClusters, mInterfaceColorClusters, mCSharpColorClusters,
               mCColorClusters, mCppColorClusters, mBasicColorClusters, mFSharpColorClusters, mHTMLColorClusters,
               mCSSColorClusters, mXMLColorClusters, mJSONColorClusters, mPowerShellColorClusters, mBatchColorClusters,
               mSQLColorClusters, mMarkdownColorClusters, mPythonColorClusters, mExamplesClusters]);
            mAllScrollPanels.AddRange([mPrimaryScrollPanel, mIncludeScrollPanel,
               mHighlightInterfaceScrollPanel, mExampleScrollPanel, mHighlightCSharpScrollPanel, mHighlightCScrollPanel,
               mHighlightCppScrollPanel, mHighlightBasicScrollPanel, mHighlightFSharpScrollPanel, mHighlightHTMLScrollPanel,
               mHighlightCSSScrollPanel, mHighlightXMLScrollPanel, mHighlightJSONScrollPanel,
               mHighlightPowerShellScrollPanel, mHighlightBatchScrollPanel, mHighlightSQLScrollPanel,
               mHighlightMarkdownScrollPanel, mHighlightPythonScrollPanel]);
            ResumeLayout(false);
         }

         protected override void OnHandleCreated(EventArgs pEventArgs) {
            base.OnHandleCreated(pEventArgs);
            SuspendLayout();
            Controls.AddRange([mPrimaryTabControl, mStatusStrip, mThemesHeaderCluster]);
            ResumeLayout(false);
            BeginInvoke(new Action(() => { LayoutControls(); }));
         }

         public void SetThemeUsage(ThemeUsage pThemeUsage) {
            mThemeUsage = pThemeUsage;
         }


         public Size WantedSize() {
            int maxWidth = 0, maxHeight = 0, pageWidth, pageHeight, wantedWidth, wantedHeight;

            // Get tab strip heights - use GetTabRect which works even before full layout
            int primaryTabStripHeight = mPrimaryTabControl.TabCount > 0 ? mPrimaryTabControl.GetTabRect(0).Height + 4 : 25;
            int highlightTabStripHeight = mHighlightTabControl.TabCount > 0 ? mHighlightTabControl.GetTabRect(0).Height + 4 : 25;

            // Check the Fonts page of the primary tab control
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

            // Check all pages of the highlight tab control (nested in the Colors page)
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
               mStatusStrip.Height + primaryTabStripHeight + highlightTabStripHeight + mEm3;
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
            ResumeLayout(true);
         }

         private void LayoutClustersAndContainers() {
            Point location = GetGroupBoxFirstLineOffset(mExampleGroupBox);
            int x = location.X, y = location.Y;
            int tallest = Tallest([ mExampleButton, mExampleCheckBox,
               mExampleRichTextBox,  mExampleRadioButton ]);

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
               clusterContainer.LayoutClusters();
               if (clusterContainer.Tag is FontUsage)
                  clusterContainer.ArrangeControlsInRows(mEmHalf);
               else if (clusterContainer.Tag is ColorUsage)
                  clusterContainer.ArrangeControlsInGrid(2, 1, mEmHalf);
               else if (clusterContainer.Tag is string)
                  clusterContainer.ArrangeControlsFlow(mEmHalf);
               clusterContainer.Invalidate();
            }
         }

         private void ApplyThemeToControlTree(Control pParent) {
            Color color = mTemporaryTheme.mInterfaceColors[(int)ColorUsage.InterfaceBackground];
            foreach (Control control in pParent.Controls) {
               if (control is BaseCluster cluster && cluster.mSkipTheme) {
                  ApplyThemeToControlTree(control);
                  continue;
               }
               control.ForeColor = mTemporaryTheme.mInterfaceColors[(int)ColorUsage.InterfaceFont];
               if (control is not BaseCluster && control is not TabControl && control is not TabPage) {
                  control.BackColor = mTemporaryTheme.mInterfaceColors[(int)ColorUsage.InterfaceBackground];
                  control.Font = CreateNewFont(mTemporaryTheme.mFonts[(int)FontUsage.Interface]);
               }
               if (control is BaseCluster baseCluster) {
                  baseCluster.LayoutCluster();
                  baseCluster.LayoutCluster();
               }
               if (control is GroupBox groupBox) {
                  groupBox.Font = CreateNewBoldFont();
                  groupBox.BackColor = mTemporaryTheme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground];
                  groupBox.ForeColor = mTemporaryTheme.mInterfaceColors[(int)ColorUsage.GroupBoxFont];
               }
               if (control is MenuStrip menuStrip) {
                  menuStrip.BackColor = mTemporaryTheme.mInterfaceColors[(int)ColorUsage.MenuBackground];
                  foreach (ToolStripItem item in menuStrip.Items)
                     if (item is ToolStripMenuItem tsmi)
                        PaintMenuItemsRecursive(tsmi, mTemporaryTheme);
               }
               if (control is StatusStrip statusStrip)
                  ApplyThemeToToolStrip(statusStrip, FontUsage.Status, ColorUsage.StatusFont, ColorUsage.StatusBackground);
               ApplyThemeToControlTree(control);
            }
         }

         private void ApplyThemeToToolStrip(ToolStrip pToolStrip, FontUsage pFontUsage, ColorUsage pForeUsage,
            ColorUsage pBackUsage) {
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
            BackColor = theme.mInterfaceColors[(int)ColorUsage.PanelBackground];
            mStatusStrip.BackColor = theme.mInterfaceColors[(int)ColorUsage.StatusBackground];
            mPrimaryTabControl.SetStripBackColor(theme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground]);
            mPrimaryTabControl.ResetStripBackgroundPainted();
            mHighlightTabControl.SetStripBackColor(theme.mInterfaceColors[(int)ColorUsage.GroupBoxBackground]);
            mHighlightTabControl.ResetStripBackgroundPainted();
            ApplyThemeToControlTree(mPrimaryTabControl);
            ApplyThemeToControlTree(mHighlightTabControl);
            mStatusStrip.Renderer = new ToolStripProfessionalRenderer();
            mStatusStrip.Invalidate(true);
            foreach (ToolStripItem item in mStatusStrip.Items) {
               if (item is ToolStripControlHost host) {
                  Control control = host.Control;
                  control.ForeColor = theme.mInterfaceColors[(int)ColorUsage.StatusFont];
                  control.BackColor = theme.mInterfaceColors[(int)ColorUsage.StatusBackground];
                  control.Font = (Font)theme.mFonts[(int)FontUsage.Status].Clone();
                  control.Invalidate();
                  control.Update();
               }
            }
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
               pBox.SelectionColor = mTemporaryTheme.mInterfaceColors[(int)ColorUsage.TextBoxFont];
               highlighter.ApplyHighlighting(pBox, tokens, mTemporaryTheme);
               pBox.Select(selectionStart, selectionLength);
            }
            finally {
               pBox.ResumeLayout();
            }
         }

         private void HighlightAllExampleBoxes() {
            for (int i = 0; i < mExampleRichTextBoxs.Length; i++) {
               HighlightExampleBox(mExampleRichTextBoxs[i], (LanguageKind)i);
            }
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

         private List<LabeledButtonColorSwatchCluster> CreateColorUsageClusters() {
            List<LabeledButtonColorSwatchCluster> clusters = [];
            foreach (ColorUsage usage in Enum.GetValues<ColorUsage>()) {
               string labelText = ToDescription(usage);
               string buttonText = ColorUsageButtonNames.Names[usage];
               Color initialColor = mTemporaryTheme.mInterfaceColors[(int)usage];
               LabeledButtonColorSwatchCluster cluster = new LabeledButtonColorSwatchCluster(mTemporaryTheme, labelText,
                  buttonText, (ColorSwatchUsage)usage, LabelPosition.Left, initialColor, null);
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

         public bool ThemeIsDirty() {
            return mThemeIsDirty;
         }

         public void EnsureColorPickerPanel(Theme pTheme, ColorUsage pUsage, Color pInitialColor) {
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
            mFirstTheme = false;
            mForm.RestoreFromThemePanel();
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               // Unhook event handlers before disposing controls
               mApplyButton.Click -= ApplyButton_Click;
               mCancelButton.Click -= CancelButton_Click;
               mHelpButton.Click -= MainForm.Help_Click;
               mNewButton.Click -= NewButton_Click;
               mCloneButton.Click -= CloneButton_Click;
               mPrimaryTabControl.DrawItem -= PrimaryTabControl_DrawItem;
               mHighlightTabControl.DrawItem -= HighlightTabControl_DrawItem;
               mPrimaryTabControl.SelectedIndexChanged -= PrimaryTabControl_SelectedIndexChanged;
               mHighlightTabControl.SelectedIndexChanged -= HighlightTabControl_SelectedIndexChanged;
               // Dispose temporary theme (not a Control, not covered by base)
               mTemporaryTheme?.Dispose();
               // Dispose buttons
               mApplyButton?.Dispose();
               mCancelButton?.Dispose();
               mCloneButton?.Dispose();
               mHelpButton?.Dispose();
               mNewButton?.Dispose();
               // Dispose ToolStrip components
               mApplyHost?.Dispose();
               mCancelHost?.Dispose();
               mCloneHost?.Dispose();
               mHelpHost?.Dispose();
               mNewHost?.Dispose();
               mSpringLabel?.Dispose();
               mStatusStrip?.Dispose();
               // Dispose tab controls
               mHighlightTabControl?.Dispose();
               mPrimaryTabControl?.Dispose();
               // Dispose cluster containers
               foreach (ClusterContainer container in mClusterContainers)
                  container?.Dispose();
               // Dispose header clusters
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
               mExampleGroupBox?.Dispose(); // cascades Button, CheckBox, RichTextBox, RadioButton
               // Dispose scroll panels
               foreach (Panel? panel in mAllScrollPanels)
                  panel?.Dispose();
               // Dispose clusters in lists
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
