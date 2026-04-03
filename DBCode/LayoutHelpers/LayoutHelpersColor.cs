namespace DBCode {
   internal static partial class LayoutHelpers {
#pragma warning disable IDE1006

      public static string MassageColorName(string pCompressedName) {
         string expandedName = string.Empty;
         Color color;
         if (!IsKnownColor(pCompressedName, out color))
            return pCompressedName;
         foreach (char c in pCompressedName)
            if (char.IsUpper(c))
               expandedName += " " + string.Format("{0}", c);
            else
               expandedName += string.Format("{0}", c);
         expandedName = expandedName.Trim(' ');
         return expandedName;
      }

      public static Color SubtlyDifferent(Color pColor) {
         int r = 128;
         int g = 128;
         int b = 128;
         if ((pColor.R + pColor.G + pColor.B) < 382) {
            r = (int)Math.Floor(pColor.R * 1.1f);
            if (r > 255)
               r = 255;
            g = (int)Math.Floor(pColor.G * 1.1f);
            if (g > 255)
               g = 255;
            b = (int)Math.Floor(pColor.B * 1.1f);
            if (b > 255)
               b = 255;
         }
         else {
            r = (int)Math.Floor(pColor.R * 0.9f);
            if (r < 0)
               r = 0;
            g = (int)Math.Floor(pColor.G * 0.9f);
            if (g < 0)
               g = 0;
            b = (int)Math.Floor(pColor.B * 0.9f);
            if (b < 0)
               b = 0;
         }
         return Color.FromArgb(r, g, b);
      }

      public static bool ColorsAreSimilar(Color pColor1, Color pColor2) {
         int rDist = Math.Abs(pColor1.R - pColor2.R);
         int gDist = Math.Abs(pColor1.G - pColor2.G);
         int bDist = Math.Abs(pColor1.B - pColor2.B);
         if ((rDist + gDist + bDist) > 260)
            return false;
         return true;
      }

      public static bool ColorsAreIdentical(Color pColor1, Color pColor2) {
         if ((pColor1.R == pColor2.R) && (pColor1.G == pColor2.G) && (pColor1.B == pColor2.B))
            return true;
         return false;
      }

      public static Color ContrastingColor(Color pColor) {
         if ((pColor.R == pColor.G) && (pColor.R == pColor.B)) {
            if ((pColor.R + pColor.G + pColor.B) < 382)
               return Color.LightBlue;
            else
               return Color.DarkBlue;
         }
         else {
            if ((pColor.R + pColor.G + pColor.B) < 382)
               return Color.LightGray;
            else
               return Color.DarkGray;
         }
      }

      public static bool IsKnownColor(Color pColor) {
         Color color;
         foreach (string colorName in Enum.GetNames<KnownColor>()) {
            KnownColor oKnownColor;
            if (!Enum.TryParse<KnownColor>(colorName, out oKnownColor))
               continue;
            if (oKnownColor > KnownColor.Transparent) {
               color = Color.FromName(colorName);
               if (color == pColor)
                  return true;
            }
         }
         return false;
      }

      public static bool IsKnownColor(string pColorName, out Color pOColor) {
         pOColor = Color.Transparent;
         List<string> colors = new List<string>();
         foreach (string colorName in Enum.GetNames<KnownColor>()) {
            KnownColor oKnownColor;
            if (!Enum.TryParse<KnownColor>(colorName, out oKnownColor))
               continue;
            if (oKnownColor > KnownColor.Transparent)
               colors.Add(colorName);
         }
         if (colors.Contains(pColorName, StringComparer.OrdinalIgnoreCase)) {
            pOColor = Color.FromName(pColorName);
            return true;
         }
         return false;
      }

      public static bool IsKnownColor(string pColorName) {
         List<string> colors = new List<string>();
         foreach (string colorName in Enum.GetNames<KnownColor>()) {
            KnownColor oKnownColor;
            if (!Enum.TryParse<KnownColor>(colorName, out oKnownColor))
               continue;
            if (oKnownColor > KnownColor.Transparent)
               colors.Add(colorName);
         }
         if (colors.Contains(pColorName, StringComparer.OrdinalIgnoreCase))
            return true;
         return false;
      }

#pragma warning restore IDE1006
   }
}
