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
