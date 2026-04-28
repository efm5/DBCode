namespace DBCode.Syntax.Tokenizing {
   internal sealed class MarkdownTokenizer : ITokenizer {
      public LanguageKind Language {
         get { return LanguageKind.Markdown; }
      }

      public IReadOnlyList<Token> Tokenize(string pText) {
         int length, index, lineStart, lineEnd;
         var tokens = new List<Token>();
         length = pText.Length;
         index = 0;
         while (index < length) {
            lineStart = index;
            lineEnd = FindLineEnd(pText, index, length);
            TokenizeLine(pText, lineStart, lineEnd, tokens);
            index = AdvancePastLineEnd(pText, lineEnd, length);
         }
         return tokens;
      }

      private static int FindLineEnd(string pText, int pStart, int pLength) {
         int index = pStart;
         while (index < pLength && pText[index] != '\n' && pText[index] != '\r') {
            index++;
         }
         return index;
      }

      private static int AdvancePastLineEnd(string pText, int pLineEnd, int pLength) {
         int index = pLineEnd;
         if (index < pLength && pText[index] == '\r') {
            index++;
         }
         if (index < pLength && pText[index] == '\n') {
            index++;
         }
         return index;
      }

      private static void TokenizeLine(string pText, int pStart, int pEnd, List<Token> pTokens) {
         int index, contentStart;
         int lineLength = pEnd - pStart;
         if (lineLength == 0) {
            return;
         }
         index = pStart;
         // skip leading whitespace
         while (index < pEnd && char.IsWhiteSpace(pText[index])) {
            index++;
         }
         if (index >= pEnd) {
            pTokens.Add(new Token(TokenKind.Whitespace, pStart, pEnd - pStart));
            return;
         }
         // heading: # ## ### etc.
         if (pText[index] == '#') {
            contentStart = index;
            while (index < pEnd && pText[index] == '#') {
               index++;
            }
            pTokens.Add(new Token(TokenKind.Keyword, contentStart, pEnd - contentStart));
            return;
         }
         // blockquote: >
         if (pText[index] == '>') {
            pTokens.Add(new Token(TokenKind.Comment, pStart, pEnd - pStart));
            return;
         }
         // code fence: ```
         if (index + 2 < pEnd && pText[index] == '`' &&
             pText[index + 1] == '`' && pText[index + 2] == '`') {
            pTokens.Add(new Token(TokenKind.PreprocessorDirective, pStart, pEnd - pStart));
            return;
         }
         // unordered list: - or *
         if ((pText[index] == '-' || pText[index] == '*') &&
             index + 1 < pEnd && char.IsWhiteSpace(pText[index + 1])) {
            pTokens.Add(new Token(TokenKind.Operator, pStart, pEnd - pStart));
            return;
         }
         // ordered list: 1. 2. etc.
         if (char.IsDigit(pText[index])) {
            int digitStart = index;
            while (index < pEnd && char.IsDigit(pText[index])) {
               index++;
            }
            if (index < pEnd && pText[index] == '.') {
               pTokens.Add(new Token(TokenKind.Operator, pStart, pEnd - pStart));
               return;
            }
            index = digitStart;
         }
         // inline elements — walk character by character
         TokenizeInline(pText, pStart, pEnd, pTokens);
      }

      private static void TokenizeInline(string pText, int pStart, int pEnd, List<Token> pTokens) {
         int index = pStart;
         int plainStart = pStart;

         void FlushPlain(int pFlushEnd) {
            if (pFlushEnd > plainStart)
               pTokens.Add(new Token(TokenKind.Identifier, plainStart, pFlushEnd - plainStart));
         }

         while (index < pEnd) {
            // inline code: `code`
            if (pText[index] == '`') {
               FlushPlain(index);
               int start = index;
               index++;
               while (index < pEnd && pText[index] != '`') {
                  index++;
               }
               if (index < pEnd)
                  index++;
               pTokens.Add(new Token(TokenKind.PreprocessorDirective, start, index - start));
               plainStart = index;
               continue;
            }
            // bold: **text** or __text__
            if (index + 1 < pEnd &&
                ((pText[index] == '*' && pText[index + 1] == '*') ||
                 (pText[index] == '_' && pText[index + 1] == '_'))) {
               FlushPlain(index);
               char marker = pText[index];
               int start = index;
               index += 2;
               while (index + 1 < pEnd &&
                      !(pText[index] == marker && pText[index + 1] == marker)) {
                  index++;
               }
               if (index + 1 < pEnd)
                  index += 2;
               pTokens.Add(new Token(TokenKind.Keyword, start, index - start));
               plainStart = index;
               continue;
            }
            // italic: *text* or _text_
            if (pText[index] == '*' || pText[index] == '_') {
               FlushPlain(index);
               char marker = pText[index];
               int start = index;
               index++;
               while (index < pEnd && pText[index] != marker) {
                  index++;
               }
               if (index < pEnd)
                  index++;
               pTokens.Add(new Token(TokenKind.Keyword, start, index - start));
               plainStart = index;
               continue;
            }
            // strikethrough: ~~text~~
            if (index + 1 < pEnd && pText[index] == '~' && pText[index + 1] == '~') {
               FlushPlain(index);
               int start = index;
               index += 2;
               while (index + 1 < pEnd &&
                      !(pText[index] == '~' && pText[index + 1] == '~')) {
                  index++;
               }
               if (index + 1 < pEnd)
                  index += 2;
               pTokens.Add(new Token(TokenKind.Comment, start, index - start));
               plainStart = index;
               continue;
            }
            // link: [text](url)
            if (pText[index] == '[') {
               FlushPlain(index);
               int start = index;
               index++;
               while (index < pEnd && pText[index] != ']') {
                  index++;
               }
               if (index < pEnd)
                  index++;
               if (index < pEnd && pText[index] == '(') {
                  index++;
                  while (index < pEnd && pText[index] != ')') {
                     index++;
                  }
                  if (index < pEnd)
                     index++;
               }
               pTokens.Add(new Token(TokenKind.StringLiteral, start, index - start));
               plainStart = index;
               continue;
            }
            index++;
         }
         FlushPlain(pEnd);
      }
   }
}
