using System.Collections.ObjectModel;
using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static Size CreateSizeFromFloats(float pWidth, float pHeight) {
         return new Size((int)pWidth, (int)pHeight);
      }

      internal static int LargestInt(int pFirst, int pSecond) {
         if (pFirst > pSecond)
            return pFirst;
         return pSecond;
      }

      internal static float LargestFloat(float pFirst, float pSecond) {
         if (pFirst > pSecond)
            return pFirst;
         return pSecond;
      }

      internal static double LargestDouble(double pFirst, double pSecond) {
         if (pFirst > pSecond)
            return pFirst;
         return pSecond;
      }

      internal static int SmallestInt(int pFirst, int pSecond) {
         if (pFirst < pSecond)
            return pFirst;
         return pSecond;
      }

      internal static float SmallestFloat(float pFirst, float pSecond) {
         if (pFirst < pSecond)
            return pFirst;
         return pSecond;
      }

      internal static double SmallestDouble(double pFirst, double pSecond) {
         if (pFirst < pSecond)
            return pFirst;
         return pSecond;
      }

      internal static int ClampInt(int pValue, int pMinimum, int pMaximum) {
         if (pValue < pMinimum)
            return pMinimum;
         if (pValue > pMaximum)
            return pMaximum;
         return pValue;
      }

      internal static float ClampFloat(float pValue, float pMinimum, float pMaximum) {
         if (pValue < pMinimum)
            return pMinimum;
         if (pValue > pMaximum)
            return pMaximum;
         return pValue;
      }

      internal static double ClampDouble(double pValue, double pMinimum, double pMaximum) {
         if (pValue < pMinimum)
            return pMinimum;
         if (pValue > pMaximum)
            return pMaximum;
         return pValue;
      }

      internal static bool IsBetweenInt(int pValue, int pMinimum, int pMaximum) {
         if (pValue < pMinimum)
            return false;
         if (pValue > pMaximum)
            return false;
         return true;
      }

      internal static bool IsBetweenFloat(float pValue, float pMinimum, float pMaximum) {
         if (pValue < pMinimum)
            return false;
         if (pValue > pMaximum)
            return false;
         return true;
      }

      internal static bool IsBetweenDouble(double pValue, double pMinimum, double pMaximum) {
         if (pValue < pMinimum)
            return false;
         if (pValue > pMaximum)
            return false;
         return true;
      }

      internal static int MaxInt(int pFirst, int pSecond) {
         if (pFirst >= pSecond)
            return pFirst;
         return pSecond;
      }

      internal static int MinInt(int pFirst, int pSecond) {
         if (pFirst <= pSecond)
            return pFirst;
         return pSecond;
      }

      internal static float MaxFloat(float pFirst, float pSecond) {
         if (pFirst >= pSecond)
            return pFirst;
         return pSecond;
      }

      internal static float MinFloat(float pFirst, float pSecond) {
         if (pFirst <= pSecond)
            return pFirst;
         return pSecond;
      }

      internal static double MaxDouble(double pFirst, double pSecond) {
         if (pFirst >= pSecond)
            return pFirst;
         return pSecond;
      }

      internal static double MinDouble(double pFirst, double pSecond) {
         if (pFirst <= pSecond)
            return pFirst;
         return pSecond;
      }

      internal static int AbsInt(int pValue) {
         if (pValue < 0)
            return -pValue;
         return pValue;
      }

      internal static float AbsFloat(float pValue) {
         if (pValue < 0f)
            return -pValue;
         return pValue;
      }

      internal static double AbsDouble(double pValue) {
         if (pValue < 0d)
            return -pValue;
         return pValue;
      }

      internal static bool NearlyEqualFloat(float pFirst, float pSecond, float pTolerance) {
         float difference = pFirst - pSecond;
         if (difference < 0f)
            difference = -difference;
         if (difference <= pTolerance)
            return true;
         return false;
      }

      internal static bool NearlyEqualDouble(double pFirst, double pSecond, double pTolerance) {
         double difference = pFirst - pSecond;
         if (difference < 0d)
            difference = -difference;
         if (difference <= pTolerance)
            return true;
         return false;
      }

      internal static int TotalWidth(List<Control>? pControls, int pEm) {
         if (pControls == null || pControls.Count == 0)
            return 0;
         int maxRight = 0, rightEdge = 0;
         for (int index = 0; index < pControls.Count; index++) {
            Control nextControl = pControls[index];
            rightEdge = nextControl.Left + nextControl.Width;
            if (rightEdge > maxRight)
               maxRight = rightEdge;
         }
         return maxRight + pEm;
      }

      internal static int TotalWidth(IEnumerable<Control>? pControls, int pEm) {
         if (pControls == null)
            return 0;
         int maxRight = 0, rightEdge = 0;
         foreach (Control nextControl in pControls) {
            rightEdge = nextControl.Left + nextControl.Width;
            if (rightEdge > maxRight)
               maxRight = rightEdge;
         }
         return maxRight + pEm;
      }

      internal static int TotalHeight(List<Control>? pControls, int pEm) {
         if (pControls == null || pControls.Count == 0)
            return 0;
         int maxBottom = 0, bottomEdge = 0;
         for (int index = 0; index < pControls.Count; index++) {
            Control nextControl = pControls[index];
            bottomEdge = nextControl.Top + nextControl.Height;
            if (bottomEdge > maxBottom)
               maxBottom = bottomEdge;
         }
         return maxBottom + pEm;
      }

      internal static int TotalHeight(IEnumerable<Control>? pControls, int pEm) {
         if (pControls == null)
            return 0;
         int maxBottom = 0, bottomEdge = 0;
         foreach (Control nextControl in pControls) {
            bottomEdge = nextControl.Top + nextControl.Height;
            if (bottomEdge > maxBottom)
               maxBottom = bottomEdge;
         }
         return maxBottom + pEm;
      }

      internal static Rectangle TotalBounds(List<Control>? pControls, int pEm) {
         if (pControls == null || pControls.Count == 0)
            return Rectangle.Empty;
         int minLeft = pControls[0].Left, minTop = pControls[0].Top,
             maxRight = pControls[0].Left + pControls[0].Width,
             maxBottom = pControls[0].Top + pControls[0].Height,
             left = 0, top = 0, right = 0, bottom = 0;
         for (int index = 1; index < pControls.Count; index++) {
            Control nextControl = pControls[index];
            left = nextControl.Left;
            top = nextControl.Top;
            right = nextControl.Left + nextControl.Width;
            bottom = nextControl.Top + nextControl.Height;
            if (left < minLeft)
               minLeft = left;
            if (top < minTop)
               minTop = top;
            if (right > maxRight)
               maxRight = right;
            if (bottom > maxBottom)
               maxBottom = bottom;
         }
         return new Rectangle(minLeft, minTop, (maxRight - minLeft) + pEm, (maxBottom - minTop) + pEm);
      }

      internal static Rectangle TotalBounds(IEnumerable<Control>? pControls, int pEm) {
         if (pControls == null)
            return Rectangle.Empty;
         Control? currentControl = null;
         foreach (Control nextControl in pControls) {
            if (currentControl == null)
               currentControl = nextControl;
         }
         if (currentControl == null)
            return Rectangle.Empty;
         int minLeft = currentControl.Left, minTop = currentControl.Top,
             maxRight = currentControl.Left + currentControl.Width,
             maxBottom = currentControl.Top + currentControl.Height,
             left = 0, top = 0, right = 0, bottom = 0;
         foreach (Control nextControl in pControls) {
            left = nextControl.Left;
            top = nextControl.Top;
            right = nextControl.Left + nextControl.Width;
            bottom = nextControl.Top + nextControl.Height;
            if (left < minLeft)
               minLeft = left;
            if (top < minTop)
               minTop = top;
            if (right > maxRight)
               maxRight = right;
            if (bottom > maxBottom)
               maxBottom = bottom;
         }
         return new Rectangle(minLeft, minTop, (maxRight - minLeft) + pEm, (maxBottom - minTop) + pEm);
      }

      internal static int MaxRight(List<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return 0;
         int maxRight = 0, rightEdge = 0;
         for (int index = 0; index < pControls.Count; index++) {
            Control nextControl = pControls[index];
            rightEdge = nextControl.Left + nextControl.Width;
            if (rightEdge > maxRight)
               maxRight = rightEdge;
         }
         return maxRight;
      }

      internal static int MaxRight(IEnumerable<Control>? pControls) {
         if (pControls == null)
            return 0;
         int maxRight = 0, rightEdge = 0;
         foreach (Control nextControl in pControls) {
            rightEdge = nextControl.Left + nextControl.Width;
            if (rightEdge > maxRight)
               maxRight = rightEdge;
         }
         return maxRight;
      }

      internal static int MaxBottom(List<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return 0;
         int maxBottom = 0, bottomEdge = 0;
         for (int index = 0; index < pControls.Count; index++) {
            Control nextControl = pControls[index];
            bottomEdge = nextControl.Top + nextControl.Height;
            if (bottomEdge > maxBottom)
               maxBottom = bottomEdge;
         }
         return maxBottom;
      }

      internal static int MaxBottom(IEnumerable<Control>? pControls) {
         if (pControls == null)
            return 0;
         int maxBottom = 0, bottomEdge = 0;
         foreach (Control nextControl in pControls) {
            bottomEdge = nextControl.Top + nextControl.Height;
            if (bottomEdge > maxBottom)
               maxBottom = bottomEdge;
         }
         return maxBottom;
      }

      internal static Control? Tallest(List<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Height > currentControl.Height)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Tallest(Collection<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Height > currentControl.Height)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Tallest(IEnumerable<Control>? pControls) {
         if (pControls == null)
            return null;
         Control? currentControl = null;
         foreach (Control nextControl in pControls) {
            if (currentControl == null)
               currentControl = nextControl;
            else if (nextControl.Height > currentControl.Height)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Shortest(List<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Height < currentControl.Height)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Shortest(Collection<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Height < currentControl.Height)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Shortest(IEnumerable<Control>? pControls) {
         if (pControls == null)
            return null;
         Control? currentControl = null;
         foreach (Control nextControl in pControls) {
            if (currentControl == null)
               currentControl = nextControl;
            else if (nextControl.Height < currentControl.Height)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Widest(List<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Width > currentControl.Width)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Widest(Collection<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Width > currentControl.Width)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Widest(IEnumerable<Control>? pControls) {
         if (pControls == null)
            return null;
         Control? currentControl = null;
         foreach (Control nextControl in pControls) {
            if (currentControl == null)
               currentControl = nextControl;
            else if (nextControl.Width > currentControl.Width)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Leftmost(List<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Left < currentControl.Left)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Leftmost(Collection<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Left < currentControl.Left)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Leftmost(IEnumerable<Control>? pControls) {
         if (pControls == null)
            return null;
         Control? currentControl = null;
         foreach (Control nextControl in pControls) {
            if (currentControl == null)
               currentControl = nextControl;
            else if (nextControl.Left < currentControl.Left)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Rightmost(List<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Right > currentControl.Right)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Rightmost(Collection<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Right > currentControl.Right)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Rightmost(IEnumerable<Control>? pControls) {
         if (pControls == null)
            return null;
         Control? currentControl = null;
         foreach (Control nextControl in pControls) {
            if (currentControl == null)
               currentControl = nextControl;
            else if (nextControl.Right > currentControl.Right)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Topmost(List<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Top < currentControl.Top)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Topmost(Collection<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Top < currentControl.Top)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Topmost(IEnumerable<Control>? pControls) {
         if (pControls == null)
            return null;
         Control? currentControl = null;
         foreach (Control nextControl in pControls) {
            if (currentControl == null)
               currentControl = nextControl;
            else if (nextControl.Top < currentControl.Top)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Bottommost(List<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Bottom > currentControl.Bottom)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Bottommost(Collection<Control>? pControls) {
         if (pControls == null || pControls.Count == 0)
            return null;
         Control currentControl = pControls[0], nextControl = pControls[0];
         for (int index = 1; index < pControls.Count; index++) {
            nextControl = pControls[index];
            if (nextControl.Bottom > currentControl.Bottom)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Control? Bottommost(IEnumerable<Control>? pControls) {
         if (pControls == null)
            return null;
         Control? currentControl = null;
         foreach (Control nextControl in pControls) {
            if (currentControl == null)
               currentControl = nextControl;
            else if (nextControl.Bottom > currentControl.Bottom)
               currentControl = nextControl;
         }
         return currentControl;
      }

      internal static Size CloneSize(Size pSize) {
         return new Size(pSize.Width, pSize.Height);
      }

      internal static Point ClonePoint(Point pPoint) {
         return new Point(pPoint.X, pPoint.Y);
      }

      internal static int GetTabHeaderHeight(Font pFont) {
         return TextRenderer.MeasureText(Fields.mUnicodeSampleString,
            new Font(pFont.FontFamily, pFont.Size + 1f, FontStyle.Bold)).Height + 6;
      }

      internal static int GetTabHeaderWidth(string pText, Font pFont) {
         return TextRenderer.MeasureText(pText, pFont).Width + 20;
      }

      internal static void ComputeTabWidths(VariableWidthTabControl pTabControl) {
         pTabControl.TabHeaderWidths.Clear();
         for (int i = 0; i < pTabControl.TabPages.Count; i++) {
            TabPage page = pTabControl.TabPages[i];
            Font font = mCurrentTheme!.mFonts[(int)FontUsage.Interface];
            int width = GetTabHeaderWidth(page.Text, font);
            pTabControl.TabHeaderWidths.Add(width);
         }
      }
   }
}
