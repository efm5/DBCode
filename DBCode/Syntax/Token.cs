namespace DBCode.Syntax {
   internal sealed class Token {
      internal TokenKind Kind { get; }
      internal int StartIndex { get; }
      internal int Length { get; }

      internal Token(TokenKind pKind, int pStartIndex, int pLength) {
         Kind = pKind;
         StartIndex = pStartIndex;
         Length = pLength;
      }
   }
}
