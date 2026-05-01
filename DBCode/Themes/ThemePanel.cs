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
         private readonly Button mApplyButton, mNewButton, mCloneButton;
         private readonly BottomPanel mThemeBottomPanel;
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
            mNewButton = new Button();
            mCloneButton = new Button();
            mThemesHeaderCluster = new HeaderLabelCluster(mTemporaryTheme,
               $"Current Theme's Name: \u201c{mCurrentTheme.mName}\u201d", HeaderLabelSize.Normal);
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
            mExcludeDataGridView.RowTemplate.Height = 25;
            mIncludeDataGridView.Columns.Add(new DataGridViewTextBoxColumn {
               HeaderText = "Include",
               Name = "IncludeColumn"
            });
            mExcludeDataGridView.Columns.Add(new DataGridViewTextBoxColumn {
               HeaderText = "Exclude",
               Name = "ExcludeColumn"
            });
            for (int i = 0; i < 10; i++) {
               mIncludeDataGridView.Rows.Add("");
               mExcludeDataGridView.Rows.Add("");
            }
            mApplyButton.Text = "&Apply";
            mNewButton.Text = "&New";
            mCloneButton.Text = "C&lone";
            mThemeBottomPanel = new BottomPanel(mTemporaryTheme, "&Cancel") {
               Name = $"ThemeBottomPanel{mTabIndex}",
               TabIndex = mTabIndex++
            };
            mThemeBottomPanel.AddLeftControl(mNewButton);
            mThemeBottomPanel.AddLeftControl(mCloneButton);
            mThemeBottomPanel.AddRightControl(mApplyButton);
            mActiveLayoutable = mThemeBottomPanel;
            mPrimaryTabControl = new VariableWidthTabControl();
            mPrimaryTabControl.TabPages.AddRange([new TabPage("Fonts"), new TabPage("Colors"),
               new TabPage("Targeting"), new TabPage("Examples")]);
            mIncludeExcludeTabControl = new VariableWidthTabControl();
            mIncludeExcludeTabControl.TabPages.AddRange([new TabPage("Inclusions"), new TabPage("Exclusions")]);
            mHighlightTabControl = new VariableWidthTabControl();
            mHighlightTabControl.TabPages.AddRange([new TabPage("Interface"), new TabPage("C#"), new TabPage("C"),
               new TabPage("C++"), new TabPage("Basic"), new TabPage("F#"), new TabPage("HTML"), new TabPage("CSS"),
               new TabPage("XML"), new TabPage("JSON"), new TabPage("Power Shell"), new TabPage("Batch"),
               new TabPage("SQL"), new TabPage("Markdown"), new TabPage("Python")]);
            mPrimaryTabControl.Dock = DockStyle.Fill;
            mIncludeExcludeTabControl.Dock = DockStyle.Fill;
            mHighlightTabControl.Dock = DockStyle.Fill;
            mPrimaryTabControl.SetStripBackColor(mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]);
            mIncludeExcludeTabControl.SetStripBackColor(mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]);
            mHighlightTabControl.SetStripBackColor(mTemporaryTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground]);
            mPrimaryTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            mIncludeExcludeTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            mHighlightTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            mPrimaryTabControl.DrawItem += PrimaryTabControl_DrawItem;
            mIncludeExcludeTabControl.DrawItem += IncludeExcludeTabControl_DrawItem;
            mHighlightTabControl.DrawItem += HighlightTabControl_DrawItem;
            mPrimaryTabControl.SelectedIndexChanged += PrimaryTabControl_SelectedIndexChanged;
            mIncludeExcludeTabControl.SelectedIndexChanged += IncludeExcludeTabControl_SelectedIndexChanged;
            mHighlightTabControl.SelectedIndexChanged += HighlightTabControl_SelectedIndexChanged;
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
               Name = "FontsClusterContainer"
            };
            mPrimaryScrollPanel.Controls.AddRange(mFontsContainer.mClusters.Cast<Control>().ToArray());//DEBUG efm5 2026 04 28 this may be a problem
#pragma warning disable IDE0017
            mExamplesContainer = new ClusterContainer(mExampleScrollPanel, mExamplesClusters, ClusterLayoutMode.FlowLayout) {
               Name = "ExamplesClusterContainer"
            };
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
            mExampleScrollPanel.Controls.AddRange([mExamplesContainer, mExampleStatusStrip,
               mExampleGroupBox, mExampleMenuStrip, mExamplesHeaderCluster]);
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
            mInterfaceColorsContainer = new ClusterContainer(mHighlightInterfaceScrollPanel, mInterfaceColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "InterfaceColorsClusterContainer"
            };
            mHighlightInterfaceScrollPanel.Controls.AddRange([mInterfaceHeaderCluster, mInterfaceColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.CSharp].Controls.Add(mHighlightCSharpScrollPanel);
            AddColorCluster(mCSharpColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mCSharpColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mCSharpColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mCSharpColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mCSharpColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mCSharpColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mCSharpColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mCSharpColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mCSharpColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mCSharpColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mCSharpColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mCSharpColorsContainer = new ClusterContainer(mHighlightCSharpScrollPanel, mCSharpColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "CSharpColorsClusterContainer"
            };
            mHighlightCSharpScrollPanel.Controls.AddRange([mCSharpHeaderCluster, mCSharpColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.C].Controls.Add(mHighlightCScrollPanel);
            AddColorCluster(mCColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mCColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mCColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mCColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mCColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mCColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mCColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mCColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mCColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mCColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mCColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mCColorsContainer = new ClusterContainer(mHighlightCScrollPanel, mCColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "CColorsClusterContainer"
            };
            mHighlightCScrollPanel.Controls.AddRange([mCHeaderCluster, mCColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Cpp].Controls.Add(mHighlightCppScrollPanel);
            AddColorCluster(mCppColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mCppColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mCppColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mCppColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mCppColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mCppColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mCppColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mCppColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mCppColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mCppColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mCppColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mCppColorsContainer = new ClusterContainer(mHighlightCppScrollPanel, mCppColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "CppColorsClusterContainer"
            };
            mHighlightCppScrollPanel.Controls.AddRange([mCppHeaderCluster, mCppColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Basic].Controls.Add(mHighlightBasicScrollPanel);
            AddColorCluster(mBasicColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mBasicColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mBasicColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mBasicColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mBasicColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mBasicColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mBasicColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mBasicColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mBasicColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mBasicColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mBasicColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mBasicColorsContainer = new ClusterContainer(mHighlightBasicScrollPanel, mBasicColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "BasicColorsClusterContainer"
            };
            mHighlightBasicScrollPanel.Controls.AddRange([mBasicHeaderCluster, mBasicColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.FSharp].Controls.Add(mHighlightFSharpScrollPanel);
            AddColorCluster(mFSharpColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mFSharpColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mFSharpColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mFSharpColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mFSharpColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mFSharpColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mFSharpColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mFSharpColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mFSharpColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mFSharpColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mFSharpColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mFSharpColorsContainer = new ClusterContainer(mHighlightFSharpScrollPanel, mFSharpColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "FSharpColorsClusterContainer"
            };
            mHighlightFSharpScrollPanel.Controls.AddRange([mFSharpHeaderCluster, mFSharpColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.HTML].Controls.Add(mHighlightHTMLScrollPanel);
            AddColorCluster(mHTMLColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mHTMLColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mHTMLColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mHTMLColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mHTMLColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mHTMLColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mHTMLColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mHTMLColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mHTMLColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mHTMLColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mHTMLColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mHTMLColorsContainer = new ClusterContainer(mHighlightHTMLScrollPanel, mHTMLColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "HTMLColorsClusterContainer"
            };
            mHighlightHTMLScrollPanel.Controls.AddRange([mHTMLHeaderCluster, mHTMLColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.CSS].Controls.Add(mHighlightCSSScrollPanel);
            AddColorCluster(mCSSColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mCSSColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mCSSColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mCSSColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mCSSColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mCSSColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mCSSColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mCSSColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mCSSColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mCSSColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mCSSColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mCSSColorsContainer = new ClusterContainer(mHighlightCSSScrollPanel, mCSSColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "CSSColorsClusterContainer"
            };
            mHighlightCSSScrollPanel.Controls.AddRange([mCSSHeaderCluster, mCSSColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.XML].Controls.Add(mHighlightXMLScrollPanel);
            AddColorCluster(mXMLColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mXMLColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mXMLColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mXMLColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mXMLColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mXMLColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mXMLColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mXMLColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mXMLColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mXMLColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mXMLColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mXMLColorsContainer = new ClusterContainer(mHighlightXMLScrollPanel, mXMLColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "XMLColorsClusterContainer"
            };
            mHighlightXMLScrollPanel.Controls.AddRange([mXMLHeaderCluster, mXMLColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.JSON].Controls.Add(mHighlightJSONScrollPanel);
            AddColorCluster(mJSONColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mJSONColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mJSONColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mJSONColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mJSONColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mJSONColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mJSONColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mJSONColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mJSONColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mJSONColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mJSONColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mJSONColorsContainer = new ClusterContainer(mHighlightJSONScrollPanel, mJSONColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "JSONColorsClusterContainer"
            };
            mHighlightJSONScrollPanel.Controls.AddRange([mJSONHeaderCluster, mJSONColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.PowerShell].Controls.Add(mHighlightPowerShellScrollPanel);
            AddColorCluster(mPowerShellColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mPowerShellColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mPowerShellColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mPowerShellColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mPowerShellColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mPowerShellColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mPowerShellColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mPowerShellColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mPowerShellColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mPowerShellColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mPowerShellColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mPowerShellColorsContainer = new ClusterContainer(mHighlightPowerShellScrollPanel, mPowerShellColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "PowerShellColorsClusterContainer"
            };
            mHighlightPowerShellScrollPanel.Controls.AddRange([mPowerShellHeaderCluster, mPowerShellColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Batch].Controls.Add(mHighlightBatchScrollPanel);
            AddColorCluster(mBatchColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mBatchColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mBatchColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mBatchColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mBatchColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mBatchColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mBatchColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mBatchColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mBatchColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mBatchColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mBatchColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mBatchColorsContainer = new ClusterContainer(mHighlightBatchScrollPanel, mBatchColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "BatchColorsClusterContainer"
            };
            mHighlightBatchScrollPanel.Controls.AddRange([mBatchHeaderCluster, mBatchColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.SQL].Controls.Add(mHighlightSQLScrollPanel);
            AddColorCluster(mSQLColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mSQLColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mSQLColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mSQLColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mSQLColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mSQLColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mSQLColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mSQLColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mSQLColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mSQLColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mSQLColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mSQLColorsContainer = new ClusterContainer(mHighlightSQLScrollPanel, mSQLColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "SQLColorsClusterContainer"
            };
            mHighlightSQLScrollPanel.Controls.AddRange([mSQLHeaderCluster, mSQLColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Markdown].Controls.Add(mHighlightMarkdownScrollPanel);
            AddColorCluster(mMarkdownColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mMarkdownColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mMarkdownColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mMarkdownColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mMarkdownColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mMarkdownColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mMarkdownColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mMarkdownColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mMarkdownColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mMarkdownColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mMarkdownColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mMarkdownColorsContainer = new ClusterContainer(mHighlightMarkdownScrollPanel, mMarkdownColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "MarkdownColorsClusterContainer"
            };
            mHighlightMarkdownScrollPanel.Controls.AddRange([mMarkdownHeaderCluster, mMarkdownColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Python].Controls.Add(mHighlightPythonScrollPanel);
            AddColorCluster(mPythonColorClusters, "Unknown", SyntaxColorSwatchUsage.Unknown);
            AddColorCluster(mPythonColorClusters, "Whitespace", SyntaxColorSwatchUsage.Whitespace);
            AddColorCluster(mPythonColorClusters, "Identifier", SyntaxColorSwatchUsage.Identifier);
            AddColorCluster(mPythonColorClusters, "Keyword", SyntaxColorSwatchUsage.Keyword);
            AddColorCluster(mPythonColorClusters, "Number", SyntaxColorSwatchUsage.Number);
            AddColorCluster(mPythonColorClusters, "String Literal", SyntaxColorSwatchUsage.StringLiteral);
            AddColorCluster(mPythonColorClusters, "Character Literal", SyntaxColorSwatchUsage.CharLiteral);
            AddColorCluster(mPythonColorClusters, "Comment", SyntaxColorSwatchUsage.Comment);
            AddColorCluster(mPythonColorClusters, "Preprocessor Directive", SyntaxColorSwatchUsage.PreprocessorDirective);
            AddColorCluster(mPythonColorClusters, "Operator", SyntaxColorSwatchUsage.Operator);
            AddColorCluster(mPythonColorClusters, "Punctuation", SyntaxColorSwatchUsage.Punctuation);
            mPythonColorsContainer = new ClusterContainer(mHighlightPythonScrollPanel, mPythonColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "PythonColorsClusterContainer"
            };
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
      }
   }
}
