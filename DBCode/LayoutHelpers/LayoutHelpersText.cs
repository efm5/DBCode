namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool IsNumericString(string pText) {
         if (string.IsNullOrWhiteSpace(pText))
            return false;
         for (int i = 0; i < pText.Length; i++) {
            if (!char.IsDigit(pText[i]))
               return false;
         }
         return true;
      }

      internal static bool IsAlphabeticString(string pText) {
         if (string.IsNullOrWhiteSpace(pText))
            return false;
         for (int i = 0; i < pText.Length; i++) {
            if (!char.IsLetter(pText[i]))
               return false;
         }
         return true;
      }

      internal static bool IsAlphanumericString(string pText) {
         if (string.IsNullOrWhiteSpace(pText))
            return false;
         for (int i = 0; i < pText.Length; i++) {
            if (!char.IsLetterOrDigit(pText[i]))
               return false;
         }
         return true;
      }

      internal static string MassagePascalName(string pCompressedName) {
         if (string.IsNullOrWhiteSpace(pCompressedName))
            return string.Empty;
         StringBuilder expandedName = new StringBuilder(pCompressedName.Length + 8);
         expandedName.Append(pCompressedName[0]);
         for (int index = 1; index < pCompressedName.Length; index++) {
            char nextCharacter = pCompressedName[index];
            if (char.IsUpper(nextCharacter))
               expandedName.Append(' ');
            expandedName.Append(nextCharacter);
         }
         return expandedName.ToString().Trim();
      }
   }
}
