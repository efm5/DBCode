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
