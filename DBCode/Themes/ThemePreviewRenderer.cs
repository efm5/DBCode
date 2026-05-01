namespace DBCode.Themes {
   public static class ThemePreviewRenderer {
      // Constants for deterministic layout
      private const int SwatchSize = 32;
      private const int SwatchSpacing = 12;
      private const int TextSpacing = 8;
      private const int BorderThickness = 1;

      public static void DrawColorSwatch(Graphics pGraphics, Rectangle pBounds, Color pColor, string pLabel, Font pFont) {
         if (pGraphics == null)
            return;
         // Swatch rectangle
         Rectangle swatchRect = new Rectangle(
            pBounds.X,
            pBounds.Y,
            SwatchSize,
            SwatchSize
         );

         using (Brush brush = new SolidBrush(pColor))
            pGraphics.FillRectangle(brush, swatchRect);
         // High-contrast border
         Color borderColor = GetBorderColor(pColor);
         using (Pen pen = new Pen(borderColor, BorderThickness))
            pGraphics.DrawRectangle(pen, swatchRect);
         // Label
         int textX = pBounds.X + SwatchSize + TextSpacing;
         int textY = pBounds.Y + (SwatchSize / 2);
         DrawCenteredText(pGraphics, pLabel, pFont, textX, textY);
      }

      public static void DrawFontSample(Graphics pGraphics, Rectangle pBounds, Font pFont, string pLabel) {
         if (pGraphics == null)
            return;
         int labelX = pBounds.X;
         int labelY = pBounds.Y;
         DrawLeftAlignedText(pGraphics, pLabel, pFont, labelX, labelY);
         int sampleX = pBounds.X;
         int sampleY = pBounds.Y + pFont.Height + TextSpacing;

         DrawLeftAlignedText(pGraphics, mUnicodeSampleString, pFont, sampleX, sampleY);
      }

      public static void DrawCombinedPreview(Graphics pGraphics, Rectangle pBounds, Theme pTheme) {
         if (pGraphics == null || pTheme == null)
            return;
         int y = pBounds.Y;

         // Draw all colors
         foreach (ColorSwatchUsage usage in Enum.GetValues<ColorSwatchUsage>()) {
            Rectangle row = new Rectangle(pBounds.X, y, pBounds.Width, SwatchSize);
            DrawColorSwatch(pGraphics, row, pTheme.mInterfaceColors[(int)usage], usage.ToString(), pTheme.mFonts[(int)FontUsage.Interface]);
            y += SwatchSize + SwatchSpacing;
         }
         // Spacer
         y += SwatchSpacing;
         // Draw all fonts
         foreach (FontUsage usage in Enum.GetValues<FontUsage>()) {
            Rectangle row = new Rectangle(pBounds.X, y, pBounds.Width, pTheme.mFonts[(int)usage].Height * 3);
            DrawFontSample(pGraphics, row, pTheme.mFonts[(int)usage], usage.ToString());
            y += row.Height + SwatchSpacing;
         }
      }

      private static void DrawCenteredText(Graphics pGraphics, string pText, Font pFont, int pX, int pCenterY) {
         if (String.IsNullOrWhiteSpace(pText))
            return;
         SizeF size = pGraphics.MeasureString(pText, pFont);
         int y = pCenterY - (int)(size.Height / 2);
         using Brush brush = new SolidBrush(Color.White);
         pGraphics.DrawString(pText, pFont, brush, pX, y);
      }

      private static void DrawLeftAlignedText(Graphics pGraphics, string pText, Font pFont, int pX, int pY) {
         if (String.IsNullOrWhiteSpace(pText))
            return;
         using Brush brush = new SolidBrush(Color.White);
         pGraphics.DrawString(pText, pFont, brush, pX, pY);
      }

      private static Color GetBorderColor(Color pColor) {
         // High-contrast border based on luminance
         int luminance = (pColor.R * 299 + pColor.G * 587 + pColor.B * 114) / 1000;

         return luminance < 128 ? Color.White : Color.Black;
      }
   }
}
