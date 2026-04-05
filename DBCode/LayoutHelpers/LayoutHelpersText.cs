namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool TextIsEmpty(string pText) {
         if (string.IsNullOrWhiteSpace(pText))
            return true;
         return false;
      }

      internal static bool TextIsNotEmpty(string pText) {
         if (string.IsNullOrWhiteSpace(pText))
            return false;
         return true;
      }

      internal static string TrimOrEmpty(string pText) {
         if (string.IsNullOrWhiteSpace(pText))
            return string.Empty;
         return pText.Trim();
      }

      internal static string NormalizeWhitespace(string pText) {
         if (string.IsNullOrWhiteSpace(pText))
            return string.Empty;
         System.Text.StringBuilder builder = new System.Text.StringBuilder(pText.Length);
         bool previousWasSpace = false;
         for (int i = 0; i < pText.Length; i++) {
            char c = pText[i];
            if (char.IsWhiteSpace(c)) {
               if (!previousWasSpace) {
                  builder.Append(' ');
                  previousWasSpace = true;
               }
            }
            else {
               builder.Append(c);
               previousWasSpace = false;
            }
         }
         return builder.ToString().Trim();
      }

      internal static string EnsureEndsWithPeriod(string pText) {
         if (string.IsNullOrWhiteSpace(pText))
            return string.Empty;
         string trimmed = pText.Trim();
         if (trimmed.EndsWith('.'))
            return trimmed;
         return trimmed + '.';
      }

      internal static int CountLines(string pText) {
         if (string.IsNullOrEmpty(pText))
            return 0;
         int lineCount = 1;
         for (int i = 0; i < pText.Length; i++) {
            if (pText[i] == '\n')
               lineCount++;
         }
         return lineCount;
      }

      internal static string SafeSubstring(string pText, int pStartIndex, int pLength) {
         if (string.IsNullOrEmpty(pText))
            return string.Empty;
         if (pStartIndex < 0)
            pStartIndex = 0;
         if (pStartIndex >= pText.Length)
            return string.Empty;
         int maxLength = pText.Length - pStartIndex;
         if (pLength > maxLength)
            pLength = maxLength;
         return pText.Substring(pStartIndex, pLength);
      }

      internal static int SafeIndexOf(string pText, string pSearch) {
         if (string.IsNullOrEmpty(pText))
            return -1;
         if (string.IsNullOrEmpty(pSearch))
            return -1;
         return pText.IndexOf(pSearch, StringComparison.Ordinal);
      }

      internal static int SafeLastIndexOf(string pText, string pSearch) {
         if (string.IsNullOrEmpty(pText))
            return -1;
         if (string.IsNullOrEmpty(pSearch))
            return -1;
         return pText.LastIndexOf(pSearch, StringComparison.Ordinal);
      }

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
