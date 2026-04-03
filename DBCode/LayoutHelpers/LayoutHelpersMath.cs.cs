namespace DBCode {
   internal static partial class LayoutHelpers {
#pragma warning disable IDE1006
      public static int Largest(List<int> pNumbers) {
         if ((pNumbers == null) || (pNumbers.Count == 0))
            throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNumbers));
         int largest = pNumbers[0];

         foreach (int number in pNumbers)
            if (number > largest)
               largest = number;
         return largest;
      }

      public static float Largest(List<float> pNumbers) {
         if ((pNumbers == null) || (pNumbers.Count == 0))
            throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNumbers));
         float largest = pNumbers[0];

         foreach (float number in pNumbers)
            if (number > largest)
               largest = number;
         return largest;
      }

      public static double Largest(List<double> pNumbers) {
         if ((pNumbers == null) || (pNumbers.Count == 0))
            throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNumbers));
         double largest = pNumbers[0];

         foreach (double number in pNumbers)
            if (number > largest)
               largest = number;
         return largest;
      }

      public static int Smallest(List<int> pNumbers) {
         if ((pNumbers == null) || (pNumbers.Count == 0))
            throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNumbers));
         int smallest = pNumbers[0];

         foreach (int number in pNumbers)
            if (number < smallest)
               smallest = number;
         return smallest;
      }

      public static float Smallest(List<float> pNumbers) {
         if ((pNumbers == null) || (pNumbers.Count == 0))
            throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNumbers));
         float smallest = pNumbers[0];

         foreach (float number in pNumbers)
            if (number < smallest)
               smallest = number;
         return smallest;
      }

      public static double Smallest(List<double> pNumbers) {
         if ((pNumbers == null) || (pNumbers.Count == 0))
            throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNumbers));
         double smallest = pNumbers[0];

         foreach (double number in pNumbers)
            if (number < smallest)
               smallest = number;
         return smallest;
      }

      public static int Tallest(List<Control> pControls) {
         int tallest = 0;

         foreach (Control control in pControls)
            if (control.Height > tallest)
               tallest = control.Height;
         return tallest;
      }

      public static int Tallest(Control.ControlCollection pControls) {
         int tallest = 0;

         foreach (Control control in pControls)
            if (control.Height > tallest)
               tallest = control.Height;
         return tallest;
      }

      public static int Shortest(List<Control> pControls) {
         int shortest = int.MaxValue;

         foreach (Control control in pControls)
            if (control.Height < shortest)
               shortest = control.Height;
         return shortest;
      }

      public static int Shortest(Control.ControlCollection pControls) {
         int shortest = int.MaxValue;

         foreach (Control control in pControls)
            if (control.Height < shortest)
               shortest = control.Height;
         return shortest;
      }

      public static int Widest(List<Control> pControls) {
         int widest = 0;

         foreach (Control control in pControls)
            if (control.Width > widest)
               widest = control.Width;
         return widest;
      }

      public static int Widest(Control.ControlCollection pControls) {
         int widest = 0;

         foreach (Control control in pControls)
            if (control.Width > widest)
               widest = control.Width;
         return widest;
      }

      public static int Rightmost(List<Control> pControls) {
         int rightmost = 0;

         foreach (Control control in pControls)
            if (control.Right > rightmost)
               rightmost = control.Right;
         return rightmost;
      }

      public static int Rightmost(Control.ControlCollection pControls) {
         int rightmost = 0;

         foreach (Control control in pControls)
            if (control.Right > rightmost)
               rightmost = control.Right;
         return rightmost;
      }

      public static int Leftmost(List<Control> pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return 0;
         int leftmost = pControls[0].Left;

         foreach (Control control in pControls)
            if (control.Left < leftmost)
               leftmost = control.Left;
         return leftmost;
      }

      public static int Leftmost(Control.ControlCollection pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return 0;
         int leftmost = pControls[0].Left;

         foreach (Control control in pControls)
            if (control.Left < leftmost)
               leftmost = control.Left;
         return leftmost;
      }

      public static int Topmost(List<Control> pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return 0;
         int topmost = pControls[0].Top;

         foreach (Control control in pControls)
            if (control.Top < topmost)
               topmost = control.Top;
         return topmost;
      }

      public static int Topmost(Control.ControlCollection pControls) {
         if ((pControls == null) || (pControls.Count == 0))
            return 0;
         int topmost = pControls[0].Top;

         foreach (Control control in pControls)
            if (control.Top < topmost)
               topmost = control.Top;
         return topmost;
      }

      public static int Bottommost(List<Control> pControls) {
         int bottommost = 0;

         foreach (Control control in pControls)
            if (control.Bottom > bottommost)
               bottommost = control.Bottom;
         return bottommost;
      }

      public static int Bottommost(Control.ControlCollection pControls) {
         int bottommost = 0;

         foreach (Control control in pControls)
            if (control.Bottom > bottommost)
               bottommost = control.Bottom;
         return bottommost;
      }

      public static int TotalWidth(List<Control>? pControls, int pPadding) {
         if ((pControls == null) || (pControls.Count == 0))
            return 0;

#pragma warning disable CS8602
         int returnValue = pControls[0].Left;
#pragma warning restore CS8602

         foreach (Control control in pControls)
            returnValue += control.Width;

         returnValue += (pPadding * pControls.Count);
         return returnValue;
      }

      public static Size SizeFromFloats(float pWidth, float pHeight) {
         return new Size((int)Math.Ceiling(pWidth), (int)Math.Ceiling(pHeight));
      }

      public static void SetComboBoxSize(out SizeF pSize, ComboBox pComboBox, string pExample = "") {
         SizeF stringSize = new SizeF(0, 0),
            paddingSize = new SizeF(0, 0);
         pSize = stringSize;
         Font font = pComboBox.Font;

         using (Graphics graphics = pComboBox.CreateGraphics()) {
            paddingSize = graphics.MeasureString("0yÑ", font);
            paddingSize.Height += (font.SizeInPoints / 2.7f);
            if (!string.IsNullOrEmpty(pExample)) //Prefer example
               stringSize = graphics.MeasureString(pExample, font);
            else if (pComboBox.Items.Count > 0) {
               foreach (string phrase in pComboBox.Items) {
                  if (!string.IsNullOrEmpty(phrase)) {
                     SizeF temporaryStringSize = new SizeF(0, 0);
                     temporaryStringSize = graphics.MeasureString(phrase, font);
                     if (temporaryStringSize.Width > stringSize.Width)
                        stringSize.Width = temporaryStringSize.Width;
                  }
               }
            }
            else//Worst-case
               stringSize = graphics.MeasureString("The quick brown fox", font);
         }
         pSize.Width = stringSize.Width + paddingSize.Width + SystemInformation.VerticalScrollBarWidth;
         pSize.Height = stringSize.Height + paddingSize.Height;
      }

      public static void SetComboBoxDropDownWidth(ComboBox pComboBox, int pMinimumWidth = 50) {
         if (pComboBox.Items.Count == 0)
            return;//don't change the width
         Font font = pComboBox.Font;
         float boxWidth = 0;
         SizeF stringSize = new SizeF();
         int minWidth = pMinimumWidth;

         if (minWidth == 0)
            minWidth = pComboBox.Width;
         else {
            try {
               using (Graphics graphics = pComboBox.CreateGraphics()) {
                  foreach (string phrase in pComboBox.Items) {
                     if (!string.IsNullOrEmpty(phrase)) {
                        stringSize = graphics.MeasureString(phrase, font);
                        if (stringSize.Width > boxWidth)
                           boxWidth = stringSize.Width;
                     }
                  }
               }
               if (boxWidth < minWidth)
                  boxWidth = minWidth;
               if (boxWidth > COMBOBOX_MAXIMUM_DROPDOWN_WIDTH)
                  boxWidth = COMBOBOX_MAXIMUM_DROPDOWN_WIDTH;
               pComboBox.DropDownWidth = (int)boxWidth;
            }
            catch (Exception) {
               //_ = AskingAsync(new TM("SetComboBoxDropDownWidth; exception caught and handled", pException));
               pComboBox.DropDownWidth = 200;
            }
         }
      }

      //public static void SetComboBoxWidth(ComboBox pComboBox) {
      //   float boxWidth = 0f;
      //   SizeF stringSize = new SizeF();

      //   using (Graphics graphics = pComboBox.CreateGraphics()) {
      //      foreach (object item in pComboBox.Items) {
      //         if (item.GetType() == typeof(string)) {
      //            if (!String.IsNullOrEmpty((string)item)) {
      //               stringSize = graphics.MeasureString((string)item, Settings.Default.InterfaceFont);
      //               if (stringSize.Width > boxWidth)
      //                  boxWidth = stringSize.Width;
      //            }
      //         }
      //      }
      //   }
      //   boxWidth = boxWidth + gIndent + SystemInformation.VerticalScrollBarWidth;
      //   pComboBox.Width = (int)boxWidth;
      //}

      public static void SetUpDownBoxWidth(NumericUpDown pNumericUpDown) {
         float boxWidth = 0f, boxHeight = 0f;
         SizeF stringSize = new SizeF();
         string minimumValue = string.Format("{0}", pNumericUpDown.Minimum),
            maximumValue = string.Format("{0}", pNumericUpDown.Maximum);

         using (Graphics graphics = pNumericUpDown.CreateGraphics()) {
            if (maximumValue.Length > minimumValue.Length)
               stringSize = graphics.MeasureString(maximumValue + "0", pNumericUpDown.Font);
            else
               stringSize = graphics.MeasureString(minimumValue + "0", pNumericUpDown.Font);
            if (stringSize.Width > boxWidth)
               boxWidth = stringSize.Width;
            if (stringSize.Height > boxHeight)
               boxHeight = stringSize.Height;
         }
         //The Up/Down arrows is about the same width as the scrollbar width
         pNumericUpDown.Width = (int)(boxWidth + SystemInformation.VerticalScrollBarWidth);
         pNumericUpDown.Height = (int)(boxHeight + mIndent);
      }

      public static bool IsSizeBigger(Size pOriginal, Size pProposed) {
         if ((pProposed.Height > pOriginal.Height) || (pProposed.Width > pOriginal.Width))
            return true;
         return false;
      }
      public static bool RectangleContainsPoint(Rectangle pRectangle, Point pPoint) {
         return (pPoint.X >= pRectangle.Left) && (pPoint.X <= pRectangle.Right) &&
            (pPoint.Y >= pRectangle.Top) && (pPoint.Y <= pRectangle.Bottom);
      }

      public static Size SizeFromSizeF(SizeF pSizeF) {
         return new Size((int)Math.Ceiling(pSizeF.Width), (int)Math.Ceiling(pSizeF.Height));
      }

      public static Size SizeFromSize(Size pSize) {
         return new Size(pSize.Width, pSize.Height);
      }

      internal static RECT RECTFromRectangle(Rectangle pRectangle) {
         RECT rect = new RECT();
         rect.Left = pRectangle.Left;
         rect.Top = pRectangle.Top;
         rect.Right = pRectangle.Right;
         rect.Bottom = pRectangle.Bottom;
         return rect;
      }

      internal static Rectangle RectangleFromRECT(RECT pRECT) {
         return new Rectangle(
            pRECT.Left,
            pRECT.Top,
            pRECT.Right - pRECT.Left,
            pRECT.Bottom - pRECT.Top);
      }

      public static Point PlusEquals(Point pPointA, Point pPointB) {
         Point result = new Point();
         result.X = pPointA.X + pPointB.X;
         result.Y = pPointA.Y + pPointB.Y;
         return result;
      }

      public static Point PointFromPoint(Point pPointA) {
         Point result = new Point();
         result.X = pPointA.X;
         result.Y = pPointA.Y;
         return result;
      }
#pragma warning restore IDE1006
   }
}
