namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static class ColorSwatchHelpers {
         public static int CalculateSwatchSize() {
            int tallest = 0;
            Panel panel = new Panel();
            Font useFont = mCurrentTheme!.mFonts[(int)FontUsage.Interface];
            Control[] samples = [
               new Button { Text = mUnicodeSampleString, AutoSize = true, Font = useFont },
               new Label { Text = mUnicodeSampleString, AutoSize = true, Font = useFont },
               new CheckBox { Text = mUnicodeSampleString, AutoSize = true, Font = useFont },
               new RadioButton { Text = mUnicodeSampleString, AutoSize = true, Font = useFont }
            ];
            foreach (Control control in samples) {
               panel.Controls.Add(control);
               if (control.Height > tallest)
                  tallest = control.Height;
            }
            panel.Dispose();
            return tallest;
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
