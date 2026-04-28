namespace DBCode.Syntax.Tokenizing {
   internal sealed class FSharpTokenizer : ITokenizer {
      private readonly List<ITokenReader> mReaders;

      internal FSharpTokenizer(ILanguageDefinition pDefinition) {
         mReaders = [
            new WhitespaceReader(),
            new FSharpCommentReader(),
            new StringReader(),
            new CharReader(),
            new NumberReader(),
            new KeywordReader(pDefinition),
            new OperatorReader()
         ];
      }

      public LanguageKind Language {
         get { return LanguageKind.FSharp; }
      }

      public IReadOnlyList<Token> Tokenize(string pText) {
         int length, index, newIndex;
         bool matched;
         Token token;
         var tokens = new List<Token>();
         length = pText.Length;
         index = 0;
         while (index < length) {
            token = new Token(TokenKind.Unknown, index, 0);
            newIndex = index;
            matched = false;
            foreach (ITokenReader reader in mReaders) {
               if (reader.TryRead(pText, index, out Token tempToken, out int tempIndex)) {
                  token = tempToken;
                  newIndex = tempIndex;
                  matched = true;
                  break;
               }
            }
            if (!matched) {
               token = new Token(TokenKind.Unknown, index, 1);
               newIndex = index + 1;
            }
            tokens.Add(token);
            index = newIndex;
         }
         return tokens;
      }
   }
}
