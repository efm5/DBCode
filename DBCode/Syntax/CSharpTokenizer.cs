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
