namespace DBCode.Themes {
   public sealed class ThemeTag {
      public FontUsage mFontUsage;
      public ColorSwatchUsage mForeColorSwatchUsage;
      public ColorSwatchUsage mBackColorSwatchUsage;
      public bool mAllowTransparent;
#pragma warning disable IDE0290
      public ThemeTag(FontUsage pFontUsage, ColorSwatchUsage pFore, ColorSwatchUsage pBack, bool pAllowTransparent = false) {
         mFontUsage = pFontUsage;
         mForeColorSwatchUsage = pFore;
         mBackColorSwatchUsage = pBack;
         mAllowTransparent = pAllowTransparent;
      }
#pragma warning restore IDE0290
   }
}
