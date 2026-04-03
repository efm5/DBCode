namespace DBCode {
   internal static partial class LayoutHelpers {
#pragma warning disable IDE1006

      public static Font CreateNewFont(Font pFont) {
         return new Font(pFont.Name, pFont.SizeInPoints, pFont.Style);
      }

      public static Font CreateNewTitleFont() {
         if (mCurrentTheme == null)
            return new Font("Segoe UI", 18f, FontStyle.Bold);
         return new Font(
            mCurrentTheme.mFonts[(int)FontUsage.Interface].Name,
            mCurrentTheme.mFonts[(int)FontUsage.Interface].SizeInPoints + 4f,
            FontStyle.Bold);
      }

      public static Font CreateNewWarningFont() {
         if (mCurrentTheme == null)
            return new Font("Segoe UI", 18f, FontStyle.Bold);
         return new Font(
            mCurrentTheme.mFonts[(int)FontUsage.Interface].Name,
            mCurrentTheme.mFonts[(int)FontUsage.Interface].SizeInPoints + 2f,
            FontStyle.Bold);
      }

      public static bool FontsAreEquals(Font pFirst, Font pSecond) {
         if (!string.Equals(pFirst.Name, pSecond.Name, StringComparison.OrdinalIgnoreCase))
            return false;
         if (pFirst.SizeInPoints != pSecond.SizeInPoints)
            return false;
         if (pFirst.Style != pSecond.Style)
            return false;
         return true;
      }

      private static bool FontsAreEqual(Font pFirstFont, Font pSecondFont) {
         if ((pFirstFont.Name == pSecondFont.Name) &&
             (pFirstFont.SizeInPoints == pSecondFont.SizeInPoints) &&
             (pFirstFont.Style == pSecondFont.Style))
            return true;
         return false;
      }

#pragma warning restore IDE1006
   }
}
