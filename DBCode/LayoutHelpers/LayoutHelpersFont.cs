namespace DBCode {
   internal static partial class LayoutHelpers {
      public static Font CreateNewFont() {
         if (mCurrentTheme == null)
            return new Font("Segoe UI", 14f, FontStyle.Regular);
         return new Font(mCurrentTheme.mFonts[(int)FontUsage.Interface].Name,
            mCurrentTheme.mFonts[(int)FontUsage.Interface].SizeInPoints, FontStyle.Regular);
      }

      public static Font CreateNewFont(Font? pFont) {
         if (pFont == null)
            return new Font("Segoe UI", 14f, FontStyle.Regular);
         return new Font(pFont.Name, pFont.SizeInPoints, pFont.Style);
      }

      public static Font CreateNewBoldFont() {
         if (mCurrentTheme == null)
            return new Font("Segoe UI", 14f, FontStyle.Bold);
         return new Font(mCurrentTheme.mFonts[(int)FontUsage.Interface].Name,
            mCurrentTheme.mFonts[(int)FontUsage.Interface].SizeInPoints, FontStyle.Bold);
      }

      public static Font CreateNewBoldFont(Font? pFont) {
         if (pFont == null)
            return new Font("Segoe UI", 14f, FontStyle.Bold);
         return new Font(pFont.Name, pFont.SizeInPoints, FontStyle.Bold);
      }

      public static Font CreateNewBoldFont(float pSize) {
         if (mCurrentTheme == null)
            return new Font("Segoe UI", pSize, FontStyle.Bold);
         return new Font(mCurrentTheme.mFonts[(int)FontUsage.Interface].Name, pSize, FontStyle.Bold);
      }

      //public static Font CreateNewTitleFont() {
      //   if (mCurrentTheme == null)
      //      return new Font("Segoe UI", 18f, FontStyle.Bold);
      //   return new Font(mCurrentTheme.mFonts[(int)FontUsage.Interface].Name,
      //      (mCurrentTheme.mFonts[(int)FontUsage.Interface].SizeInPoints * 1.25f), FontStyle.Bold);
      //}

      //public static Font CreateNewTitleFont(float pSize) {
      //   if (mCurrentTheme == null)
      //      return new Font("Segoe UI", 18f * pSize, FontStyle.Bold);
      //   if (pSize < 0)
      //      pSize = 1.25f;
      //   else if (pSize < .5)
      //      pSize = .5f;
      //   else if (pSize > 2)
      //      pSize = 2f;
      //   float baseSize = mCurrentTheme.mFonts[(int)FontUsage.Interface].SizeInPoints * 1.25f;
      //   float finalSize = baseSize * pSize;
      //   return new Font(mCurrentTheme.mFonts[(int)FontUsage.Interface].Name, finalSize, FontStyle.Bold);
      //}

      public static Font CreateNewTitleFont(HeaderLabelSize pSize) {
         if (mCurrentTheme == null)
            return new Font("Segoe UI", 18f, FontStyle.Bold);
         float baseSize = mCurrentTheme.mFonts[(int)FontUsage.Interface].SizeInPoints, multiplier = (int)pSize / 100f;

         baseSize = (float)Math.Ceiling(baseSize * multiplier);
         if (baseSize < 8)
            baseSize = 8f;
         else if (baseSize > 100)
            baseSize = 100f;
         return new Font(mCurrentTheme.mFonts[(int)FontUsage.Interface].Name, baseSize, FontStyle.Bold);
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
