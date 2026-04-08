using static DBCode.Themes.ThemeBrightnessHelper;

namespace DBCode.Themes {
   public class Theme {
      public string mName { get; set; } = string.Empty;
      public ThemeBrightness mBrightness { get; set; } = ThemeBrightness.Light;
      public Font[] mFonts { get; private set; }
      public Color[] mColors { get; private set; }
      public Theme(string pName) {
         mName = pName;
         mFonts = new Font[Enum.GetValues<FontUsage>().Length];
         mColors = new Color[Enum.GetValues<ColorUsage>().Length];
         mFonts[(int)FontUsage.Text] = CreateNewFont();
         mFonts[(int)FontUsage.Interface] = CreateNewFont();
         mFonts[(int)FontUsage.Menu] = CreateNewFont();
         mFonts[(int)FontUsage.Status] = CreateNewFont();
         mColors[(int)ColorUsage.PanelBackground] = Color.Black;
         mColors[(int)ColorUsage.TextBox] = Color.Black;
         mColors[(int)ColorUsage.TextBoxFont] = Color.LightGoldenrodYellow;
         mColors[(int)ColorUsage.MenuBackground] = Color.Black;
         mColors[(int)ColorUsage.MenuFont] = Color.LightGoldenrodYellow;
         mColors[(int)ColorUsage.InterfaceBackground] = Color.Black;
         mColors[(int)ColorUsage.InterfaceFont] = Color.LightGoldenrodYellow;
         mColors[(int)ColorUsage.StatusBackground] = Color.DarkBlue;
         mColors[(int)ColorUsage.StatusFont] = Color.LightGoldenrodYellow;
         mColors[(int)ColorUsage.GroupBoxBackground] = Color.DarkGray;
         mColors[(int)ColorUsage.GroupBoxFont] = Color.Goldenrod;
         mColors[(int)ColorUsage.TabHeaderUnselectedFont] = Color.Yellow;
         mColors[(int)ColorUsage.TabHeaderSelectedFont] = Color.Blue;
         mColors[(int)ColorUsage.TabHeaderUnselectedBackground] = Color.DarkGray;
         mColors[(int)ColorUsage.TabHeaderSelectedBackground] = Color.Goldenrod;
      }
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
