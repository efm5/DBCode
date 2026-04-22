namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static string MassageColorName(string pCompressedName) {
         if (string.IsNullOrWhiteSpace(pCompressedName))
            return string.Empty;
         Color knownColor;
         if (!IsKnownColor(pCompressedName, out knownColor))
            return pCompressedName;
         System.Text.StringBuilder expandedName = new System.Text.StringBuilder(pCompressedName.Length + 8);
         expandedName.Append(pCompressedName[0]);
         for (int index = 1; index < pCompressedName.Length; index++) {
            char nextCharacter = pCompressedName[index];
            if (char.IsUpper(nextCharacter))
               expandedName.Append(' ');
            expandedName.Append(nextCharacter);
         }
         return expandedName.ToString().Trim();
      }

      internal static Color SubtlyDifferent(Color pColor) {
         bool isDark = (pColor.R + pColor.G + pColor.B) < 382;
         float factor = isDark ? 1.1f : 0.9f;
         int red = ClampToByte((int)Math.Floor(pColor.R * factor)),
             green = ClampToByte((int)Math.Floor(pColor.G * factor)),
             blue = ClampToByte((int)Math.Floor(pColor.B * factor));
         return Color.FromArgb(red, green, blue);
      }

      internal static bool ColorsAreSimilar(Color pFirstColor, Color pSecondColor) {
         int redDistance = Math.Abs(pFirstColor.R - pSecondColor.R),
             greenDistance = Math.Abs(pFirstColor.G - pSecondColor.G),
             blueDistance = Math.Abs(pFirstColor.B - pSecondColor.B);
         if ((redDistance + greenDistance + blueDistance) > 260)
            return false;
         return true;
      }

      internal static bool ColorsAreIdentical(Color pFirstColor, Color pSecondColor) {
         if ((pFirstColor.R == pSecondColor.R) &&
             (pFirstColor.G == pSecondColor.G) &&
             (pFirstColor.B == pSecondColor.B))
            return true;
         return false;
      }

      internal static Color ContrastingColor(Color pColor) {
         if ((pColor.R == pColor.G) && (pColor.R == pColor.B)) {
            int sum = pColor.R + pColor.G + pColor.B;
            if (sum < 382)
               return Color.LightBlue;
            return Color.DarkBlue;
         }
         int brightness = pColor.R + pColor.G + pColor.B;
         if (brightness < 382)
            return Color.LightGray;
         return Color.DarkGray;
      }

      internal static bool IsKnownColor(Color pColor) {
         foreach (string nextColorName in Enum.GetNames<KnownColor>()) {
            KnownColor nextKnownColor;
            if (!Enum.TryParse<KnownColor>(nextColorName, out nextKnownColor))
               continue;
            if (nextKnownColor <= KnownColor.Transparent)
               continue;
            Color knownColor = Color.FromName(nextColorName);
            if (knownColor.ToArgb() == pColor.ToArgb())
               return true;
         }
         return false;
      }

      internal static bool IsKnownColor(string pColorName, out Color pOutputColor) {
         pOutputColor = Color.Transparent;
         if (string.IsNullOrWhiteSpace(pColorName))
            return false;
         foreach (string nextColorName in Enum.GetNames<KnownColor>()) {
            KnownColor nextKnownColor;
            if (!Enum.TryParse<KnownColor>(nextColorName, out nextKnownColor))
               continue;
            if (nextKnownColor <= KnownColor.Transparent)
               continue;
            if (string.Equals(nextColorName, pColorName, StringComparison.OrdinalIgnoreCase)) {
               pOutputColor = Color.FromName(nextColorName);
               return true;
            }
         }
         return false;
      }

      internal static bool IsKnownColor(string pColorName) {
         if (string.IsNullOrWhiteSpace(pColorName))
            return false;
         foreach (string nextColorName in Enum.GetNames<KnownColor>()) {
            KnownColor nextKnownColor;
            if (!Enum.TryParse<KnownColor>(nextColorName, out nextKnownColor))
               continue;
            if (nextKnownColor <= KnownColor.Transparent)
               continue;
            if (string.Equals(nextColorName, pColorName, StringComparison.OrdinalIgnoreCase))
               return true;
         }
         return false;
      }

      private static int ClampToByte(int pValue) {
         if (pValue < 0)
            return 0;
         if (pValue > 255)
            return 255;
         return pValue;
      }

   }
}
