using static DBCode.LayoutHelpers.ColorSwatchHelpers;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class ColorSwatch : Panel {
         private ColorSwatchUsage mColorSwatchUsage = (ColorSwatchUsage)(-1);
         private ColorPickerSwatchUsage mPickerUsage = (ColorPickerSwatchUsage)(-1);
         private SyntaxColorSwatchUsage mSyntaxColorSwatchUsage = (SyntaxColorSwatchUsage)(-1);
         private Color mSwatchColor = Color.Black;
         private Size mSwatchSize = new Size(24, 24);
         public event ColorSwatchClickedHandler? ColorSwatchClicked;
         public event ColorPickerSwatchClickedHandler? PickerSwatchClicked;
         public event SyntaxColorSwatchClickedHandler? SyntaxSwatchClicked;

         public ColorSwatch(ColorSwatchUsage pUsage, Color pInitialColor, int pSize) {
            mColorSwatchUsage = pUsage;
            mSwatchColor = pInitialColor;
            if (pSize < 8)
               mSwatchSize = GetSwatchSize();
            else
               mSwatchSize = new Size(pSize, pSize);
            Size = mSwatchSize;
            BackColor = Color.Transparent;
            TabIndex = mTabIndex;
            Name = "ColorSwatch" + mTabIndex++;
            MouseClick += OnMouseClick;
         }

         public ColorSwatch(ColorPickerSwatchUsage pUsage, Color pInitialColor, int pSize) {
            mPickerUsage = pUsage;
            mSwatchColor = pInitialColor;
            if (pSize < 8)
               mSwatchSize = GetSwatchSize();
            else
               mSwatchSize = new Size(pSize, pSize);
            Size = mSwatchSize;
            BackColor = Color.Transparent;
            TabIndex = mTabIndex;
            Name = "ColorSwatch" + mTabIndex++;
            MouseClick += OnMouseClick;
         }

         public ColorSwatch(SyntaxColorSwatchUsage pUsage, Color pInitialColor, int pSize) {
            mSyntaxColorSwatchUsage = pUsage;
            mSwatchColor = pInitialColor;
            if (pSize < 8)
               mSwatchSize = GetSwatchSize();
            else
               mSwatchSize = new Size(pSize, pSize);
            Size = mSwatchSize;
            BackColor = Color.Transparent;
            TabIndex = mTabIndex;
            Name = "ColorSwatch" + mTabIndex++;
            MouseClick += OnMouseClick;
         }

         public void SetColor(Color pNewColor) {
            mSwatchColor = pNewColor;
            Invalidate();
         }

         public Color GetColor() {
            return mSwatchColor;
         }

         public void SetSize(int pSize) {
            Size = new Size(pSize, pSize);
         }

         private void OnMouseClick(object? pSender, MouseEventArgs pArgs) {
            if (pArgs.Button != MouseButtons.Left)
               return;
            if (mColorSwatchUsage != (ColorSwatchUsage)(-1))
               ColorSwatchClicked?.Invoke(this, mColorSwatchUsage);
            else if (mPickerUsage != (ColorPickerSwatchUsage)(-1))
               PickerSwatchClicked?.Invoke(this, mPickerUsage);
            else if (mSyntaxColorSwatchUsage != (SyntaxColorSwatchUsage)(-1))
               SyntaxSwatchClicked?.Invoke(this, mSyntaxColorSwatchUsage);
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
