namespace DBCode.Themes {
   public enum ThemeBrightness : int {
      Light = 0,
      Dark = 1
   }

   public class Theme {
      public string mName { get; set; } = string.Empty;
      public ThemeBrightness mBrightness { get; set; } = ThemeBrightness.Light;
      public Font[] mFonts { get; private set; }
      public Color[] mColors { get; private set; }

#pragma warning disable IDE0290
      public Theme(string pName) {
         mName = pName;
         mFonts = new Font[Enum.GetValues<FontUsage>().Length];
         mColors = new Color[Enum.GetValues<ColorUsage>().Length];
      }
#pragma warning restore IDE0290

      public Theme Clone() {
         Theme pClone = new Theme(mName) { mBrightness = mBrightness };

         for (int i = 0; i < mFonts.Length; i++) {
            if (mFonts[i] != null)
               pClone.mFonts[i] = (Font)mFonts[i].Clone();
         }
         for (int i = 0; i < mColors.Length; i++)
            pClone.mColors[i] = mColors[i];
         return pClone;
      }
   }
}