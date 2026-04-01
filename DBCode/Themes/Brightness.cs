namespace DBCode.Themes {
   public static class Brightness {
      public static float ComputeLuma(Color pColor) {
         float r = pColor.R / 255f;
         float g = pColor.G / 255f;
         float b = pColor.B / 255f;
         float luma = (0.2126f * r) + (0.7152f * g) + (0.0722f * b);

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