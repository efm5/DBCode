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
   }
}
