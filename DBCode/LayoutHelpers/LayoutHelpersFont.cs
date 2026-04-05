namespace DBCode {
   internal static partial class LayoutHelpers {
      public static Font CreateNewFont() {
         if (mCurrentTheme == null)
            return new Font("Segoe UI", 14f, FontStyle.Bold);
         return new Font(mCurrentTheme.mFonts[(int)FontUsage.Interface].Name,
            mCurrentTheme.mFonts[(int)FontUsage.Interface].SizeInPoints, FontStyle.Regular);
      }

      public static Font CreateNewFont(Font? pFont) {
         if (pFont == null)
            return new Font("Segoe UI", 14f, FontStyle.Regular);
         return new Font(pFont.Name, pFont.SizeInPoints, pFont.Style);
      }

      public static Font CreateNewTitleFont() {
         if (mCurrentTheme == null)
            return new Font("Segoe UI", 18f, FontStyle.Bold);
         float baseSize = mCurrentTheme.mFonts[(int)FontUsage.Interface].SizeInPoints;
         return new Font(mCurrentTheme.mFonts[(int)FontUsage.Interface].Name, baseSize + 4f, FontStyle.Bold);
      }

      public static Font CreateNewWarningFont() {
         if (mCurrentTheme == null)
            return new Font("Segoe UI", 18f, FontStyle.Bold);
         float baseSize = mCurrentTheme.mFonts[(int)FontUsage.Interface].SizeInPoints;
         return new Font(mCurrentTheme.mFonts[(int)FontUsage.Interface].Name, baseSize + 2f, FontStyle.Bold);
      }

      public static bool FontsAreEquals(Font? pFirst, Font? pSecond) {
         if (pFirst == null)
            return false;
         if (pSecond == null)
            return false;
         if (!string.Equals(pFirst.Name, pSecond.Name, StringComparison.OrdinalIgnoreCase))
            return false;
         if (pFirst.SizeInPoints != pSecond.SizeInPoints)
            return false;
         if (pFirst.Style != pSecond.Style)
            return false;
         return true;
      }
   }
}
