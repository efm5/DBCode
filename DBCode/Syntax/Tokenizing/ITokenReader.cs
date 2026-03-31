namespace DBCode.Syntax.Tokenizing {
   internal interface ITokenReader {
      bool TryRead(
         string pText,
         int pStartIndex,
         out Token pToken,
         out int pNewIndex
      );
   }
}
