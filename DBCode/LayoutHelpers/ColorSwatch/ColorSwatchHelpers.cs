namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static class ColorSwatchHelpers {
         internal static Size GetSwatchSize() {
            using Panel tempPanel = new Panel();
            using Button tempButton = new Button() {
               Text = mUnicodeSampleString,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont()
            };
            tempPanel.Controls.Add(tempButton);
            int dimension = tempButton.Height;
            return new Size(dimension, dimension);
         }

         public static int BorderInset() {
            int inset = 2;
            if (mEm > 14)
               inset++;
            if (mEm > 20)
               inset++;
            if (mEm > 24)
               inset++;
            if (mEm > 30)
               inset++;
            if (mEm > 40)
               inset++;
            if (mEm > 60)
               inset++;
            return inset;
         }

         public static Color[] BorderColors(Control pParent) {
            Color baseColor = pParent.BackColor;
            int luminance = (int)((baseColor.R * 0.299) + (baseColor.G * 0.587) + (baseColor.B * 0.114));
            Color outer = luminance < 128 ? Color.White : Color.Black;
            Color inner = luminance < 128 ? Color.LightGray : Color.DarkGray;
            return [outer, inner];
         }
      }
   }
}
