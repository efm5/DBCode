namespace DBCode.Syntax {
   internal sealed class BraceMatchingEngine {
      internal const int DepthCount = 10;

      // Languages for which brace colorization applies. HTML, XML, Markdown, Batch,
      // and PlainText are excluded: HTML/XML use tag-based structure, the others
      // have no meaningful paired-bracket syntax.
      private static readonly HashSet<LanguageKind> sBraceLanguages = [
         LanguageKind.CSharp, LanguageKind.C, LanguageKind.Cpp,
         LanguageKind.FSharp, LanguageKind.Basic,
         LanguageKind.Css, LanguageKind.Json,
         LanguageKind.PowerShell, LanguageKind.Python,
         LanguageKind.Sql,
      ];

      // Returns the user-configured brace pair color for the given index (0–9).
      // Colors are stored as individual UiState fields and edited via Options.
      internal static Color GetBracePairColor(int pIndex) => (pIndex % DepthCount) switch {
         0 => mUiState.mBracePairColor0,
         1 => mUiState.mBracePairColor1,
         2 => mUiState.mBracePairColor2,
         3 => mUiState.mBracePairColor3,
         4 => mUiState.mBracePairColor4,
         5 => mUiState.mBracePairColor5,
         6 => mUiState.mBracePairColor6,
         7 => mUiState.mBracePairColor7,
         8 => mUiState.mBracePairColor8,
         _ => mUiState.mBracePairColor9,
      };

      // Colors every non-string/non-comment brace character in the document.
      // Uses a single forward pass with a stack-based color assignment: each open
      // brace draws the next sequential color index and pushes it; the matching
      // close brace pops and reuses the same index. Sibling pairs at the same
      // nesting level each get a distinct color; nested pairs escalate naturally.
      // Caller owns WM_SETREDRAW freeze, SuspendLayout, and selection restoration.
      // The whole-text SelectionBackColor reset in HighlightNow already cleared
      // any previous brace highlights before this is called.
      internal static void ColorizeAllBraces(RichTextBox pBox, IReadOnlyList<Token>? pTokens,
                                             LanguageKind pLanguage) {
         if (!sBraceLanguages.Contains(pLanguage))
            return;
         string text = pBox.Text;
         if (text.Length == 0)
            return;
         Stack<int> colorStack = new Stack<int>();
         int nextColorIndex = 0;
         for (int i = 0; i < text.Length; i++) {
            char ch = text[i];
            if (!IsBraceChar(ch))
               continue;
            if (IsInStringOrComment(i, pTokens))
               continue;
            int colorIndex;
            if (IsOpenBrace(ch)) {
               colorIndex = nextColorIndex;
               nextColorIndex = (nextColorIndex + 1) % DepthCount;
               colorStack.Push(colorIndex);
            }
            else {
               colorIndex = colorStack.Count > 0 ? colorStack.Pop() : 0;
            }
            pBox.SelectionStart = i;
            pBox.SelectionLength = 1;
            pBox.SelectionBackColor = GetBracePairColor(colorIndex);
         }
      }

      private static bool IsBraceChar(char pCh) =>
         pCh is '{' or '}' or '(' or ')' or '[' or ']';

      private static bool IsOpenBrace(char pCh) => pCh is '{' or '(' or '[';

      // Returns true if pIndex falls within a StringLiteral, CharLiteral, or Comment token.
      // Tokens are assumed sorted by StartIndex; the loop exits early once past pIndex.
      private static bool IsInStringOrComment(int pIndex, IReadOnlyList<Token>? pTokens) {
         if (pTokens == null)
            return false;
         foreach (Token token in pTokens) {
            if (token.StartIndex > pIndex)
               break;
            if (pIndex < token.StartIndex + token.Length)
               return token.Kind is TokenKind.StringLiteral
                                 or TokenKind.CharLiteral
                                 or TokenKind.Comment;
         }
         return false;
      }
   }
}
