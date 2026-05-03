namespace DBCode {
   internal static partial class LayoutHelpers {
      // ── Dominant tone sampling ─────────────────────────────────────────────────────────────

      /// <summary>
      /// Samples a uniform grid of pixels across pControl's client area and returns the
      /// dominant ColorTones value by majority vote. Designed for sampling the Form's
      /// ScrollablePanel (which is always DockStyle.Fill) to determine the effective
      /// background tone in its current visual state (enabled or disabled).
      /// </summary>
      internal static ColorTones SampleDominantTone(Control pControl) {
         const int GridSize = 8;   // 8×8 = 64 samples; adjust here only if needed
         Rectangle bounds = pControl.RectangleToScreen(pControl.ClientRectangle);
         int[] counts = new int[Enum.GetValues<ColorTones>().Length];
         using Graphics graphics = Graphics.FromHwnd(pControl.Handle);
         IntPtr hdc = graphics.GetHdc();
         try {
            for (int row = 0; row < GridSize; row++) {
               for (int col = 0; col < GridSize; col++) {
                  int x = bounds.Left + (col * bounds.Width) / (GridSize - 1);
                  int y = bounds.Top + (row * bounds.Height) / (GridSize - 1);
                  uint colorRef = LayoutHelpersNativeMethods.GetPixel(hdc, x, y);
                  Color color = Color.FromArgb(
                     (int)(colorRef & 0xFF),
                     (int)((colorRef >> 8) & 0xFF),
                     (int)((colorRef >> 16) & 0xFF));
                  counts[(int)ColorTone.GetTone(color)]++;
               }
            }
         }
         finally {
            graphics.ReleaseHdc(hdc);
         }
         return (ColorTones)Array.IndexOf(counts, counts.Max());
      }

      internal static class ColorTone {
         private const float ALPHA_IGNORE_THRESHOLD = 16f / 255f;
         private const float DARK_MAX = 0.18f;
         private const float LIGHT_MIN = 0.73f;
         private static readonly FloatRange sDarkBucket = new(0.00f, DARK_MAX);
         private static readonly FloatRange sMediumDarkBucket = new(DARK_MAX + float.Epsilon, 0.45f);
         private static readonly FloatRange sMediumLightBucket = new(0.46f, LIGHT_MIN - float.Epsilon);
         private static readonly FloatRange sLightBucket = new(LIGHT_MIN, 1.00f);

         internal static ColorTones GetTone(Color pColor) {
            float alpha = pColor.A / 255f;
            if (alpha < ALPHA_IGNORE_THRESHOLD)
               return ColorTones.Ignore;
            Color blended = BlendWithGray(pColor, alpha);
            float luminance = ComputeLuminance(blended);
            if (sDarkBucket.Contains(luminance))
               return ColorTones.Dark;
            if (sMediumDarkBucket.Contains(luminance))
               return ColorTones.MediumDark;
            if (sMediumLightBucket.Contains(luminance))
               return ColorTones.MediumLight;
            if (sLightBucket.Contains(luminance))
               return ColorTones.Light;
            return ColorTones.Ignore;     // should be unreachable; luminance is always 0.0–1.0
         }

         private static Color BlendWithGray(Color pColor, float pAlpha) {
            const float gray = 128f / 255f;
            float oneMinusAlpha = 1f - pAlpha;
            int r = ClampToByte((int)Math.Round((pAlpha * pColor.R) + (oneMinusAlpha * gray * 255f)));
            int g = ClampToByte((int)Math.Round((pAlpha * pColor.G) + (oneMinusAlpha * gray * 255f)));
            int b = ClampToByte((int)Math.Round((pAlpha * pColor.B) + (oneMinusAlpha * gray * 255f)));
            return Color.FromArgb(255, r, g, b);
         }

         private static float ComputeLuminance(Color pColor) {
            float r = Linearize(pColor.R / 255f);
            float g = Linearize(pColor.G / 255f);
            float b = Linearize(pColor.B / 255f);
            return (0.2126f * r) + (0.7152f * g) + (0.0722f * b);
         }

         private static float Linearize(float pChannel) {
            if (pChannel <= 0.04045f)
               return pChannel / 12.92f;
            return MathF.Pow((pChannel + 0.055f) / 1.055f, 2.4f);
         }
         //A few notes: 
         //Why float.Epsilon for the bucket boundaries rather than a hardcoded gap 
         //like 0.01f: float.Epsilon is the smallest representable float difference, so the 
         //buckets are truly contiguous with no dead zone between them. A gap of 0.01f would 
         //mean luminance values like 0.455f fall into no bucket at all. Why the final return
         //ColorTones.Ignore is not ThrowIfOutOfBounds: ComputeLuminance mathematically
         //guarantees a result in [0.0, 1.0] because Linearize is monotone and the WCAG
         //weights sum to exactly 1.0. But the compiler doesn't know that, and a future
         //maintainer changing the weights might break that invariant silently. Returning
         //Ignore rather than throwing keeps it non-fatal consistent with your decision
         //about transparency, and the unreachable comment signals intent clearly. The
         //Linearize threshold 0.04045f is the exact sRGB standard value — some sources
         //cite 0.03928f which is a rounding artifact from an older draft. The 0.04045f
         //value is correct for modern sRGB. FloatRange as readonly struct means it
         //allocates on the stack when used locally and the four static readonly
         //instances in ColorTone are effectively zero-cost after startup.
      }

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
