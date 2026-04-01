namespace DBCode.Themes {
   public static class Brightness {
      public static float ComputeLuma(Color pColor) {
         float red = pColor.R / 255f;
         float green = pColor.G / 255f;
         float blue = pColor.B / 255f;
         float luma = (0.2126f * red) + (0.7152f * green) + (0.0722f * blue);

         return luma;
      }

      public static bool IsDark(Color pColor) {
         return ComputeLuma(pColor) < 0.5f;
      }

      public static bool IsLight(Color pColor) {
         return ComputeLuma(pColor) >= 0.5f;
      }

      public static ThemeBrightness Classify(Color pColor) {
         if (ComputeLuma(pColor) < 0.5f)
            return ThemeBrightness.Dark;
         return ThemeBrightness.Light;
      }
   }
}
