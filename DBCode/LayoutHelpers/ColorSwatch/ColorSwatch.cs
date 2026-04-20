using static DBCode.LayoutHelpers.ColorSwatchHelpers;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class ColorSwatch : Panel {
         private ColorSwatchUsage mUsage = ColorSwatchUsage.PanelBackground;
         private Color mSwatchColor = Color.Black;
         private Size mSwatchSize = new Size(24, 24);
         public event ColorSwatchClickedHandler? SwatchClicked;

         public ColorSwatch(ColorSwatchUsage pUsage, Color pInitialColor, Color? pBackgroundColor = null) {
            mUsage = pUsage;
            mSwatchColor = pInitialColor;
            mSwatchSize = GetSwatchSize();
            Size = mSwatchSize;
            BackColor = Color.Transparent;
            TabIndex = LayoutHelpers.NextTabIndex();
            Name = "ColorSwatch" + TabIndex;
            MouseClick += OnMouseClick;
         }

         public void SetColor(Color pNewColor) {
            mSwatchColor = pNewColor;
            Invalidate();
         }

         private void OnMouseClick(object? pSender, MouseEventArgs pArgs) {
            if (pArgs.Button != MouseButtons.Left)
               return;
            SwatchClicked?.Invoke(this, mUsage);
         }

         protected override void OnPaint(PaintEventArgs pArgs) {
            base.OnPaint(pArgs);
            using (SolidBrush brush = new SolidBrush(mSwatchColor))
               pArgs.Graphics.FillRectangle(brush, 0, 0, mSwatchSize.Width, mSwatchSize.Height);
            pArgs.Graphics.DrawRectangle(Pens.Black, 0, 0, (mSwatchSize.Width - 1), (mSwatchSize.Height - 1));
            pArgs.Graphics.DrawRectangle(Pens.White, 1, 1, (mSwatchSize.Width - 3), (mSwatchSize.Height - 3));
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing)
               MouseClick -= OnMouseClick;
            base.Dispose(pDisposing);
         }
      }
   }
}
