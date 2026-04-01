namespace DBCode.Themes {
   public sealed class ThemeTag {
      public FontUsage mFontUsage;
      public ColorUsage mForeColorUsage;
      public ColorUsage mBackColorUsage;
      public bool mAllowTransparent;
#pragma warning disable IDE0290
      public ThemeTag(FontUsage pFontUsage, ColorUsage pFore, ColorUsage pBack, bool pAllowTransparent = false) {
         mFontUsage = pFontUsage;
         mForeColorUsage = pFore;
         mBackColorUsage = pBack;
         mAllowTransparent = pAllowTransparent;
      }
#pragma warning restore IDE0290
   }
}
