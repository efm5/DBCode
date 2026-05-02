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
         internal readonly BottomPanel mThemeBottomPanel;
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
         private readonly Button mExampleButton, mBottomExampleButton;
         private readonly MenuStrip mExampleMenuStrip;
         private readonly ToolStripMenuItem mExampleTSMI, mExampleTSMISubItem;
         private readonly BottomPanel mExampleBottomPanel;
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
            mThemeBottomPanel = new BottomPanel(mTemporaryTheme, "&Cancel") {
               Name = $"ThemeBottomPanel{mTabIndex}",
               TabIndex = mTabIndex++
            };
            mThemeBottomPanel.AddLeftControl(mNewButton);
            mThemeBottomPanel.AddLeftControl(mCloneButton);
            mThemeBottomPanel.AddRightControl(mApplyButton);
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
               Name = "FontsClusterContainer",
               TabIndex = mTabIndex++
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
               TabIndex = mTabIndex++,
               Text = "Example Group"
            };
            mExampleButton = new Button {
               Name = "ExampleButton",
               TabIndex = mTabIndex++,
               Text = "Button",
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mExampleCheckBox = new CheckBox {
               Name = "ExampleCheckBox",
               TabIndex = mTabIndex++,
               Text = "Option",
               AutoSize = true
            };
            mExampleRichTextBox = new RichTextBox {
               Name = "ExampleRichTextBox",
               TabIndex = mTabIndex++,
               Text = mUnicodeSampleString,
               Multiline = true,
               ScrollBars = RichTextBoxScrollBars.Both,
               Width = 300,
               Height = mEm * 4
            };
            mExampleRadioButton = new RadioButton {
               Name = "ExampleRadioButton",
               TabIndex = mTabIndex++,
               Text = "Radio Button",
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
               AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            mExampleBottomPanel = new BottomPanel(mTemporaryTheme) {
               Name = "ExampleBottomPanel",
               TabIndex = mTabIndex++
            };
            mExampleBottomPanel.Anchor = mAnchorTopLeft;
            mExampleBottomPanel.mHelpButton!.Text = "NO Help";
            mExampleBottomPanel.mCancelButton!.Text = "UN Canceled";
            mExampleBottomPanel.AddLeftControl(mBottomExampleButton);
            mExampleScrollPanel.Controls.AddRange([mExamplesContainer, mExampleBottomPanel,
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
            AddColorCluster(mCSharpColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.CSharp);
            AddColorCluster(mCSharpColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.CSharp);
            AddColorCluster(mCSharpColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.CSharp);
            AddColorCluster(mCSharpColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.CSharp);
            AddColorCluster(mCSharpColorClusters, "Number", TokenKind.Number, LanguageKind.CSharp);
            AddColorCluster(mCSharpColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.CSharp);
            AddColorCluster(mCSharpColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.CSharp);
            AddColorCluster(mCSharpColorClusters, "Comment", TokenKind.Comment, LanguageKind.CSharp);
            AddColorCluster(mCSharpColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.CSharp);
            AddColorCluster(mCSharpColorClusters, "Operator", TokenKind.Operator, LanguageKind.CSharp);
            AddColorCluster(mCSharpColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.CSharp);
            mCSharpColorsContainer = new ClusterContainer(mHighlightCSharpScrollPanel, mCSharpColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "CSharpColorsClusterContainer"
            };
            mHighlightCSharpScrollPanel.Controls.AddRange([mCSharpHeaderCluster, mCSharpColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.C].Controls.Add(mHighlightCScrollPanel);
            AddColorCluster(mCColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.C);
            AddColorCluster(mCColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.C);
            AddColorCluster(mCColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.C);
            AddColorCluster(mCColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.C);
            AddColorCluster(mCColorClusters, "Number", TokenKind.Number, LanguageKind.C);
            AddColorCluster(mCColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.C);
            AddColorCluster(mCColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.C);
            AddColorCluster(mCColorClusters, "Comment", TokenKind.Comment, LanguageKind.C);
            AddColorCluster(mCColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.C);
            AddColorCluster(mCColorClusters, "Operator", TokenKind.Operator, LanguageKind.C);
            AddColorCluster(mCColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.C);
            mCColorsContainer = new ClusterContainer(mHighlightCScrollPanel, mCColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "CColorsClusterContainer"
            };
            mHighlightCScrollPanel.Controls.AddRange([mCHeaderCluster, mCColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Cpp].Controls.Add(mHighlightCppScrollPanel);
            AddColorCluster(mCppColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.Cpp);
            AddColorCluster(mCppColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.Cpp);
            AddColorCluster(mCppColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.Cpp);
            AddColorCluster(mCppColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.Cpp);
            AddColorCluster(mCppColorClusters, "Number", TokenKind.Number, LanguageKind.Cpp);
            AddColorCluster(mCppColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.Cpp);
            AddColorCluster(mCppColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.Cpp);
            AddColorCluster(mCppColorClusters, "Comment", TokenKind.Comment, LanguageKind.Cpp);
            AddColorCluster(mCppColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.Cpp);
            AddColorCluster(mCppColorClusters, "Operator", TokenKind.Operator, LanguageKind.Cpp);
            AddColorCluster(mCppColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.Cpp);
            mCppColorsContainer = new ClusterContainer(mHighlightCppScrollPanel, mCppColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "CppColorsClusterContainer"
            };
            mHighlightCppScrollPanel.Controls.AddRange([mCppHeaderCluster, mCppColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Basic].Controls.Add(mHighlightBasicScrollPanel);
            AddColorCluster(mBasicColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.Basic);
            AddColorCluster(mBasicColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.Basic);
            AddColorCluster(mBasicColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.Basic);
            AddColorCluster(mBasicColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.Basic);
            AddColorCluster(mBasicColorClusters, "Number", TokenKind.Number, LanguageKind.Basic);
            AddColorCluster(mBasicColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.Basic);
            AddColorCluster(mBasicColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.Basic);
            AddColorCluster(mBasicColorClusters, "Comment", TokenKind.Comment, LanguageKind.Basic);
            AddColorCluster(mBasicColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.Basic);
            AddColorCluster(mBasicColorClusters, "Operator", TokenKind.Operator, LanguageKind.Basic);
            AddColorCluster(mBasicColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.Basic);
            mBasicColorsContainer = new ClusterContainer(mHighlightBasicScrollPanel, mBasicColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "BasicColorsClusterContainer"
            };
            mHighlightBasicScrollPanel.Controls.AddRange([mBasicHeaderCluster, mBasicColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.FSharp].Controls.Add(mHighlightFSharpScrollPanel);
            AddColorCluster(mFSharpColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.FSharp);
            AddColorCluster(mFSharpColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.FSharp);
            AddColorCluster(mFSharpColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.FSharp);
            AddColorCluster(mFSharpColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.FSharp);
            AddColorCluster(mFSharpColorClusters, "Number", TokenKind.Number, LanguageKind.FSharp);
            AddColorCluster(mFSharpColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.FSharp);
            AddColorCluster(mFSharpColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.FSharp);
            AddColorCluster(mFSharpColorClusters, "Comment", TokenKind.Comment, LanguageKind.FSharp);
            AddColorCluster(mFSharpColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.FSharp);
            AddColorCluster(mFSharpColorClusters, "Operator", TokenKind.Operator, LanguageKind.FSharp);
            AddColorCluster(mFSharpColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.FSharp);
            mFSharpColorsContainer = new ClusterContainer(mHighlightFSharpScrollPanel, mFSharpColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "FSharpColorsClusterContainer"
            };
            mHighlightFSharpScrollPanel.Controls.AddRange([mFSharpHeaderCluster, mFSharpColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.HTML].Controls.Add(mHighlightHTMLScrollPanel);
            AddColorCluster(mHTMLColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.Html);
            AddColorCluster(mHTMLColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.Html);
            AddColorCluster(mHTMLColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.Html);
            AddColorCluster(mHTMLColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.Html);
            AddColorCluster(mHTMLColorClusters, "Number", TokenKind.Number, LanguageKind.Html);
            AddColorCluster(mHTMLColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.Html);
            AddColorCluster(mHTMLColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.Html);
            AddColorCluster(mHTMLColorClusters, "Comment", TokenKind.Comment, LanguageKind.Html);
            AddColorCluster(mHTMLColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.Html);
            AddColorCluster(mHTMLColorClusters, "Operator", TokenKind.Operator, LanguageKind.Html);
            AddColorCluster(mHTMLColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.Html);
            mHTMLColorsContainer = new ClusterContainer(mHighlightHTMLScrollPanel, mHTMLColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "HTMLColorsClusterContainer"
            };
            mHighlightHTMLScrollPanel.Controls.AddRange([mHTMLHeaderCluster, mHTMLColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.CSS].Controls.Add(mHighlightCSSScrollPanel);
            AddColorCluster(mCSSColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.Css);
            AddColorCluster(mCSSColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.Css);
            AddColorCluster(mCSSColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.Css);
            AddColorCluster(mCSSColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.Css);
            AddColorCluster(mCSSColorClusters, "Number", TokenKind.Number, LanguageKind.Css);
            AddColorCluster(mCSSColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.Css);
            AddColorCluster(mCSSColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.Css);
            AddColorCluster(mCSSColorClusters, "Comment", TokenKind.Comment, LanguageKind.Css);
            AddColorCluster(mCSSColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.Css);
            AddColorCluster(mCSSColorClusters, "Operator", TokenKind.Operator, LanguageKind.Css);
            AddColorCluster(mCSSColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.Css);
            mCSSColorsContainer = new ClusterContainer(mHighlightCSSScrollPanel, mCSSColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "CSSColorsClusterContainer"
            };
            mHighlightCSSScrollPanel.Controls.AddRange([mCSSHeaderCluster, mCSSColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.XML].Controls.Add(mHighlightXMLScrollPanel);
            AddColorCluster(mXMLColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.Xml);
            AddColorCluster(mXMLColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.Xml);
            AddColorCluster(mXMLColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.Xml);
            AddColorCluster(mXMLColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.Xml);
            AddColorCluster(mXMLColorClusters, "Number", TokenKind.Number, LanguageKind.Xml);
            AddColorCluster(mXMLColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.Xml);
            AddColorCluster(mXMLColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.Xml);
            AddColorCluster(mXMLColorClusters, "Comment", TokenKind.Comment, LanguageKind.Xml);
            AddColorCluster(mXMLColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.Xml);
            AddColorCluster(mXMLColorClusters, "Operator", TokenKind.Operator, LanguageKind.Xml);
            AddColorCluster(mXMLColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.Xml);
            mXMLColorsContainer = new ClusterContainer(mHighlightXMLScrollPanel, mXMLColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "XMLColorsClusterContainer"
            };
            mHighlightXMLScrollPanel.Controls.AddRange([mXMLHeaderCluster, mXMLColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.JSON].Controls.Add(mHighlightJSONScrollPanel);
            AddColorCluster(mJSONColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.Json);
            AddColorCluster(mJSONColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.Json);
            AddColorCluster(mJSONColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.Json);
            AddColorCluster(mJSONColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.Json);
            AddColorCluster(mJSONColorClusters, "Number", TokenKind.Number, LanguageKind.Json);
            AddColorCluster(mJSONColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.Json);
            AddColorCluster(mJSONColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.Json);
            AddColorCluster(mJSONColorClusters, "Comment", TokenKind.Comment, LanguageKind.Json);
            AddColorCluster(mJSONColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.Json);
            AddColorCluster(mJSONColorClusters, "Operator", TokenKind.Operator, LanguageKind.Json);
            AddColorCluster(mJSONColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.Json);
            mJSONColorsContainer = new ClusterContainer(mHighlightJSONScrollPanel, mJSONColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "JSONColorsClusterContainer"
            };
            mHighlightJSONScrollPanel.Controls.AddRange([mJSONHeaderCluster, mJSONColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.PowerShell].Controls.Add(mHighlightPowerShellScrollPanel);
            AddColorCluster(mPowerShellColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.PowerShell);
            AddColorCluster(mPowerShellColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.PowerShell);
            AddColorCluster(mPowerShellColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.PowerShell);
            AddColorCluster(mPowerShellColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.PowerShell);
            AddColorCluster(mPowerShellColorClusters, "Number", TokenKind.Number, LanguageKind.PowerShell);
            AddColorCluster(mPowerShellColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.PowerShell);
            AddColorCluster(mPowerShellColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.PowerShell);
            AddColorCluster(mPowerShellColorClusters, "Comment", TokenKind.Comment, LanguageKind.PowerShell);
            AddColorCluster(mPowerShellColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.PowerShell);
            AddColorCluster(mPowerShellColorClusters, "Operator", TokenKind.Operator, LanguageKind.PowerShell);
            AddColorCluster(mPowerShellColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.PowerShell);
            mPowerShellColorsContainer = new ClusterContainer(mHighlightPowerShellScrollPanel, mPowerShellColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "PowerShellColorsClusterContainer"
            };
            mHighlightPowerShellScrollPanel.Controls.AddRange([mPowerShellHeaderCluster, mPowerShellColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Batch].Controls.Add(mHighlightBatchScrollPanel);
            AddColorCluster(mBatchColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.Batch);
            AddColorCluster(mBatchColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.Batch);
            AddColorCluster(mBatchColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.Batch);
            AddColorCluster(mBatchColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.Batch);
            AddColorCluster(mBatchColorClusters, "Number", TokenKind.Number, LanguageKind.Batch);
            AddColorCluster(mBatchColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.Batch);
            AddColorCluster(mBatchColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.Batch);
            AddColorCluster(mBatchColorClusters, "Comment", TokenKind.Comment, LanguageKind.Batch);
            AddColorCluster(mBatchColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.Batch);
            AddColorCluster(mBatchColorClusters, "Operator", TokenKind.Operator, LanguageKind.Batch);
            AddColorCluster(mBatchColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.Batch);
            mBatchColorsContainer = new ClusterContainer(mHighlightBatchScrollPanel, mBatchColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "BatchColorsClusterContainer"
            };
            mHighlightBatchScrollPanel.Controls.AddRange([mBatchHeaderCluster, mBatchColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.SQL].Controls.Add(mHighlightSQLScrollPanel);
            AddColorCluster(mSQLColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.Sql);
            AddColorCluster(mSQLColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.Sql);
            AddColorCluster(mSQLColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.Sql);
            AddColorCluster(mSQLColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.Sql);
            AddColorCluster(mSQLColorClusters, "Number", TokenKind.Number, LanguageKind.Sql);
            AddColorCluster(mSQLColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.Sql);
            AddColorCluster(mSQLColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.Sql);
            AddColorCluster(mSQLColorClusters, "Comment", TokenKind.Comment, LanguageKind.Sql);
            AddColorCluster(mSQLColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.Sql);
            AddColorCluster(mSQLColorClusters, "Operator", TokenKind.Operator, LanguageKind.Sql);
            AddColorCluster(mSQLColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.Sql);
            mSQLColorsContainer = new ClusterContainer(mHighlightSQLScrollPanel, mSQLColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "SQLColorsClusterContainer"
            };
            mHighlightSQLScrollPanel.Controls.AddRange([mSQLHeaderCluster, mSQLColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Markdown].Controls.Add(mHighlightMarkdownScrollPanel);
            AddColorCluster(mMarkdownColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.Markdown);
            AddColorCluster(mMarkdownColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.Markdown);
            AddColorCluster(mMarkdownColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.Markdown);
            AddColorCluster(mMarkdownColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.Markdown);
            AddColorCluster(mMarkdownColorClusters, "Number", TokenKind.Number, LanguageKind.Markdown);
            AddColorCluster(mMarkdownColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.Markdown);
            AddColorCluster(mMarkdownColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.Markdown);
            AddColorCluster(mMarkdownColorClusters, "Comment", TokenKind.Comment, LanguageKind.Markdown);
            AddColorCluster(mMarkdownColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.Markdown);
            AddColorCluster(mMarkdownColorClusters, "Operator", TokenKind.Operator, LanguageKind.Markdown);
            AddColorCluster(mMarkdownColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.Markdown);
            mMarkdownColorsContainer = new ClusterContainer(mHighlightMarkdownScrollPanel, mMarkdownColorClusters,
               ClusterLayoutMode.FixedColumns, 0, 0, 3, 0) {
               Name = "MarkdownColorsClusterContainer"
            };
            mHighlightMarkdownScrollPanel.Controls.AddRange([mMarkdownHeaderCluster, mMarkdownColorsContainer]);
            mHighlightTabControl.TabPages[(int)HighlightTabPageUsage.Python].Controls.Add(mHighlightPythonScrollPanel);
            AddColorCluster(mPythonColorClusters, "Unknown", TokenKind.Unknown, LanguageKind.Python);
            AddColorCluster(mPythonColorClusters, "Whitespace", TokenKind.Whitespace, LanguageKind.Python);
            AddColorCluster(mPythonColorClusters, "Identifier", TokenKind.Identifier, LanguageKind.Python);
            AddColorCluster(mPythonColorClusters, "Keyword", TokenKind.Keyword, LanguageKind.Python);
            AddColorCluster(mPythonColorClusters, "Number", TokenKind.Number, LanguageKind.Python);
            AddColorCluster(mPythonColorClusters, "String Literal", TokenKind.StringLiteral, LanguageKind.Python);
            AddColorCluster(mPythonColorClusters, "Character Literal", TokenKind.CharLiteral, LanguageKind.Python);
            AddColorCluster(mPythonColorClusters, "Comment", TokenKind.Comment, LanguageKind.Python);
            AddColorCluster(mPythonColorClusters, "Preprocessor Directive", TokenKind.PreprocessorDirective, LanguageKind.Python);
            AddColorCluster(mPythonColorClusters, "Operator", TokenKind.Operator, LanguageKind.Python);
            AddColorCluster(mPythonColorClusters, "Punctuation", TokenKind.Punctuation, LanguageKind.Python);
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
