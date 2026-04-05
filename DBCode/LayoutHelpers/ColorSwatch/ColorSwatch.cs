namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class ColorSwatch : Panel {
         private ColorSwatchUsage mUsage = ColorSwatchUsage.PanelBackground;
         private Color mSwatchColor = Color.Black;
         private int mSwatchSize = 24;
         public event ColorSwatchClickedHandler? SwatchClicked;

         public ColorSwatch(ColorSwatchUsage pUsage, Color pColor, int pSwatchSize, Color? pBackgroundColor) {
            mUsage = pUsage;
            mSwatchColor = pColor;
            mSwatchSize = pSwatchSize;

            BackColor = pBackgroundColor ?? Color.Transparent;
            Width = mSwatchSize;
            Height = mSwatchSize;
            TabIndex = LayoutHelpers.NextTabIndex();
            Name = "ColorSwatch" + TabIndex;

            MouseClick += OnMouseClick;
         }

         private void OnMouseClick(object? pSender, MouseEventArgs pArgs) {
            if (pArgs.Button != MouseButtons.Left)
               return;
            SwatchClicked?.Invoke(this, mUsage);
         }

         public ColorSwatchUsage Usage() {
            return mUsage;
         }

         public Color SwatchColor() {
            return mSwatchColor;
         }

         public void SetSwatchColor(Color pColor) {
            mSwatchColor = pColor;
            Invalidate();
         }

         protected override void OnPaint(PaintEventArgs pArgs) {
            base.OnPaint(pArgs);

            using (SolidBrush brush = new SolidBrush(mSwatchColor)) {
               pArgs.Graphics.FillRectangle(brush, 0, 0, mSwatchSize, mSwatchSize);
            }

            pArgs.Graphics.DrawRectangle(Pens.Black, 0, 0, mSwatchSize - 1, mSwatchSize - 1);
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               MouseClick -= OnMouseClick;
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
