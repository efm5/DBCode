# Create the Syntax folder
$syntaxPath = ".\DBCode\Syntax"
if (-not (Test-Path $syntaxPath)) {
    New-Item -ItemType Directory -Path $syntaxPath | Out-Null
}

# Helper function to write file content
function Write-FileContent {
    param (
        [string]$FileName,
        [string]$Content
    )

    $fullPath = Join-Path $syntaxPath $FileName
    Set-Content -Path $fullPath -Value $Content -Encoding UTF8
}

# -------------------------------
# TokenKind.cs
# -------------------------------
Write-FileContent "TokenKind.cs" @"
using System;

namespace DBCode.Syntax
{
   internal enum TokenKind
   {
      Unknown,
      Whitespace,
      Identifier,
      Keyword,
      Number,
      StringLiteral,
      CharLiteral,
      Comment,
      Preprocessor,
      Operator,
      Punctuation
   }
}
"@

# -------------------------------
# LanguageKind.cs
# -------------------------------
Write-FileContent "LanguageKind.cs" @"
using System;

namespace DBCode.Syntax
{
   internal enum LanguageKind
   {
      CSharp,
      C,
      Cpp,
      Basic,
      FSharp,
      Html,
      Css,
      Xml,
      Json,
      PowerShell,
      Batch,
      Sql,
      Markdown,
      Python
   }
}
"@

# -------------------------------
# Token.cs
# -------------------------------
Write-FileContent "Token.cs" @"
using System;

namespace DBCode.Syntax
{
   internal sealed class Token
   {
      internal Token(TokenKind pKind, int pStartIndex, int pLength)
      {
         Kind = pKind;
         StartIndex = pStartIndex;
         Length = pLength;
      }

      internal TokenKind Kind { get; }
      internal int StartIndex { get; }
      internal int Length { get; }
   }
}
"@

# -------------------------------
# ILanguageDefinition.cs
# -------------------------------
Write-FileContent "ILanguageDefinition.cs" @"
using System.Collections.Generic;

namespace DBCode.Syntax
{
   internal interface ILanguageDefinition
   {
      LanguageKind Language { get; }
      IReadOnlyCollection<string> Keywords { get; }
   }
}
"@

# -------------------------------
# ITokenizer.cs
# -------------------------------
Write-FileContent "ITokenizer.cs" @"
using System.Collections.Generic;

namespace DBCode.Syntax
{
   internal interface ITokenizer
   {
      LanguageKind Language { get; }

      IReadOnlyList<Token> Tokenize(string pText);
   }
}
"@

# -------------------------------
# IHighlighter.cs
# -------------------------------
Write-FileContent "IHighlighter.cs" @"
using System.Collections.Generic;
using System.Windows.Forms;

namespace DBCode.Syntax
{
   internal interface IHighlighter
   {
      LanguageKind Language { get; }

      void ApplyHighlighting(RichTextBox pRichTextBox, IReadOnlyList<Token> pTokens);
   }
}
"@

# -------------------------------
# LanguageRegistry.cs
# -------------------------------
Write-FileContent "LanguageRegistry.cs" @"
using System.Collections.Generic;

namespace DBCode.Syntax
{
   internal static class LanguageRegistry
   {
      private static readonly Dictionary<LanguageKind, ILanguageDefinition> sDefinitions = new();
      private static readonly Dictionary<LanguageKind, ITokenizer> sTokenizers = new();
      private static readonly Dictionary<LanguageKind, IHighlighter> sHighlighters = new();

      internal static void Register(
         ILanguageDefinition pDefinition,
         ITokenizer pTokenizer,
         IHighlighter pHighlighter)
      {
         sDefinitions[pDefinition.Language] = pDefinition;
         sTokenizers[pTokenizer.Language] = pTokenizer;
         sHighlighters[pHighlighter.Language] = pHighlighter;
      }

      internal static ILanguageDefinition GetDefinition(LanguageKind pLanguage)
      {
         return sDefinitions[pLanguage];
      }

      internal static ITokenizer GetTokenizer(LanguageKind pLanguage)
      {
         return sTokenizers[pLanguage];
      }

      internal static IHighlighter GetHighlighter(LanguageKind pLanguage)
      {
         return sHighlighters[pLanguage];
      }
   }
}
"@

# -------------------------------
# HighlighterEngine.cs
# -------------------------------
Write-FileContent "HighlighterEngine.cs" @"
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DBCode.Syntax
{
   internal sealed class HighlighterEngine
   {
      private readonly RichTextBox mRichTextBox;
      private readonly Timer mTimer;
      private LanguageKind mLanguage;

      internal HighlighterEngine(RichTextBox pRichTextBox, LanguageKind pLanguage)
      {
         mRichTextBox = pRichTextBox;
         mLanguage = pLanguage;

         mTimer = new Timer
         {
            Interval = 400
         };
         mTimer.Tick += OnTimerTick;
      }

      internal void SetLanguage(LanguageKind pLanguage)
      {
         mLanguage = pLanguage;
      }

      internal void OnTextChanged()
      {
         mTimer.Stop();
         mTimer.Start();
      }

      private void OnTimerTick(object? pSender, EventArgs pArgs)
      {
         mTimer.Stop();
         HighlightNow();
      }

      private void HighlightNow()
      {
         string text = mRichTextBox.Text;
         if (text.Length == 0)
         {
            return;
         }

         ITokenizer tokenizer = LanguageRegistry.GetTokenizer(mLanguage);
         IHighlighter highlighter = LanguageRegistry.GetHighlighter(mLanguage);

         IReadOnlyList<Token> tokens = tokenizer.Tokenize(text);

         int selectionStart = mRichTextBox.SelectionStart;
         int selectionLength = mRichTextBox.SelectionLength;

         mRichTextBox.SuspendLayout();
         try
         {
            mRichTextBox.Select(0, mRichTextBox.TextLength);
            mRichTextBox.SelectionColor = System.Drawing.Color.Black;

            highlighter.ApplyHighlighting(mRichTextBox, tokens);

            mRichTextBox.Select(selectionStart, selectionLength);
         }
         finally
         {
            mRichTextBox.ResumeLayout();
         }
      }
   }
}
"@

# -------------------------------
# CSharpLanguageDefinition.cs
# -------------------------------
Write-FileContent "CSharpLanguageDefinition.cs" @"
using System.Collections.Generic;

namespace DBCode.Syntax
{
   internal sealed class CSharpLanguageDefinition : ILanguageDefinition
   {
      private static readonly string[] sKeywords =
      {
         ""abstract"", ""as"", ""base"", ""bool"", ""break"", ""byte"", ""case"", ""catch"",
         ""char"", ""checked"", ""class"", ""const"", ""continue"", ""decimal"", ""default"",
         ""delegate"", ""do"", ""double"", ""else"", ""enum"", ""event"", ""explicit"",
         ""extern"", ""false"", ""finally"", ""fixed"", ""float"", ""for"", ""foreach"",
         ""goto"", ""if"", ""implicit"", ""in"", ""int"", ""interface"", ""internal"", ""is"",
         ""lock"", ""long"", ""namespace"", ""new"", ""null"", ""object"", ""operator"",
         ""out"", ""override"", ""params"", ""private"", ""protected"", ""public"",
         ""readonly"", ""ref"", ""return"", ""sbyte"", ""sealed"", ""short"", ""sizeof"",
         ""stackalloc"", ""static"", ""string"", ""struct"", ""switch"", ""this"", ""throw"",
         ""true"", ""try"", ""typeof"", ""uint"", ""ulong"", ""unchecked"", ""unsafe"",
         ""ushort"", ""using"", ""virtual"", ""void"", ""volatile"", ""while""
      };

      public LanguageKind Language => LanguageKind.CSharp;

      public IReadOnlyCollection<string> Keywords => sKeywords;
   }
}
"@

# -------------------------------
# CSharpTokenizer.cs
# -------------------------------
Write-FileContent "CSharpTokenizer.cs" @"
using System.Collections.Generic;

namespace DBCode.Syntax
{
   internal sealed class CSharpTokenizer : ITokenizer
   {
      private readonly ILanguageDefinition mDefinition;

      internal CSharpTokenizer(ILanguageDefinition pDefinition)
      {
         mDefinition = pDefinition;
      }

      public LanguageKind Language => LanguageKind.CSharp;

      public IReadOnlyList<Token> Tokenize(string pText)
      {
         var tokens = new List<Token>();
         int index = 0;

         while (index < pText.Length)
         {
            char c = pText[index];

            if (char.IsWhiteSpace(c))
            {
               int start = index;
               while (index < pText.Length && char.IsWhiteSpace(pText[index]))
               {
                  index++;
               }

               tokens.Add(new Token(TokenKind.Whitespace, start, index - start));
               continue;
            }

            if (IsIdentifierStart(c))
            {
               int start = index;
               index++;

               while (index < pText.Length && IsIdentifierPart(pText[index]))
               {
                  index++;
               }

               int length = index - start;
               string value = pText.Substring(start, length);

               TokenKind kind = mDefinition.Keywords.Contains(value)
                  ? TokenKind.Keyword
                  : TokenKind.Identifier;

               tokens.Add(new Token(kind, start, length));
               continue;
            }

            tokens.Add(new Token(TokenKind.Unknown, index, 1));
            index++;
         }

         return tokens;
      }

      private static bool IsIdentifierStart(char pChar)
      {
         return char.IsLetter(pChar) || pChar == '_' ||
            char.GetUnicodeCategory(pChar) == System.Globalization.UnicodeCategory.LetterNumber;
      }

      private static bool IsIdentifierPart(char pChar)
      {
         if (IsIdentifierStart(pChar))
         {
            return true;
         }

         return char.IsDigit(pChar);
      }
   }
}
"@

# -------------------------------
# CSharpHighlighter.cs
# -------------------------------
Write-FileContent "CSharpHighlighter.cs" @"
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DBCode.Syntax
{
   internal sealed class CSharpHighlighter : IHighlighter
   {
      public LanguageKind Language => LanguageKind.CSharp;

      public void ApplyHighlighting(RichTextBox pRichTextBox, IReadOnlyList<Token> pTokens)
      {
         foreach (Token token in pTokens)
         {
            Color color = GetColor(token.Kind);

            if (color == Color.Black)
            {
               continue;
            }

            pRichTextBox.Select(token.StartIndex, token.Length);
            pRichTextBox.SelectionColor = color;
         }
      }

      private static Color GetColor(TokenKind pKind)
      {
         return pKind switch
         {
            TokenKind.Keyword       => Color.Blue,
            TokenKind.StringLiteral => Color.Brown,
            TokenKind.CharLiteral   => Color.Brown,
            TokenKind.Comment       => Color.Green,
            TokenKind.Number        => Color.DarkCyan,
            TokenKind.Preprocessor  => Color.Purple,
            _                       => Color.Black
         };
      }
   }
}
"@
