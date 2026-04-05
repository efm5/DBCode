namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool RectangleContains(Rectangle pRectangle, Point pPoint) {
         return pRectangle.Contains(pPoint);
      }

      internal static bool IsSizeBigger(Size pOriginalSize, Size pProposedSize) {
         if (pProposedSize.Width > pOriginalSize.Width)
            return true;
         if (pProposedSize.Height > pOriginalSize.Height)
            return true;
         return false;
      }
   }
}
