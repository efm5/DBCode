namespace DBCode {
   public class VariableWidthTabControl : TabControl {
      public readonly List<int> TabHeaderWidths = [];
      private Color mCapturedBackColor = SystemColors.Control;
      private bool mRecalculating;

      public void SetStripBackColor(Color pColor) {
         mCapturedBackColor = pColor;
      }

      public void RecalculateItemSize(Font pFont) {
         if (TabCount == 0 || !IsHandleCreated || mRecalculating)
            return;
         mRecalculating = true;
         try {
            using Font boldFont = new Font(pFont, pFont.Style | FontStyle.Bold);
            FontFamily family = boldFont.FontFamily;
            float emHeight = family.GetEmHeight(boldFont.Style);
            float ascent = family.GetCellAscent(boldFont.Style);
            float descent = family.GetCellDescent(boldFont.Style);
            float cellHeight = boldFont.Size * (ascent + descent) / emHeight;
            int hPad = (int)Math.Ceiling(Math.Max(20, boldFont.Size * mFontWidthAdjustment));
            int vPad = (int)Math.Ceiling(Math.Max(12, boldFont.Size * mFontHeightAdjustment));
            int tabHeight = (int)Math.Ceiling(cellHeight) + vPad;
            ItemSize = new Size(0, tabHeight);
            SendMessage(Handle, TCM_SETPADDING, 0, MakeLParam(hPad, vPad));
            TabHeaderWidths.Clear();
            for (int i = 0; i < TabCount; i++)
               TabHeaderWidths.Add(GetTabRect(i).Width);
            Invalidate();
         } finally {
            mRecalculating = false;
         }
      }

      protected override void OnHandleCreated(EventArgs pEventArgs) {
         base.OnHandleCreated(pEventArgs);
         RecalculateItemSize(Font ?? SystemFonts.DefaultFont);
      }

      protected override void OnFontChanged(EventArgs pEventArgs) {
         base.OnFontChanged(pEventArgs);
         RecalculateItemSize(Font ?? SystemFonts.DefaultFont);
      }

      // DrawItem fires inside base.WndProc and paints every tab header; we then fill only
      // the gaps between headers so the themed background shows without overwriting them.
      // This works identically for single-line and multiline — no flag state needed.
      protected override void WndProc(ref Message pMessage) {
         if (pMessage.Msg == 0x0201) {
            int lp = pMessage.LParam.ToInt32();
            int hitTab = HitTestTabHeaders(new Point(lp & 0xFFFF, (lp >> 16) & 0xFFFF));
            if (hitTab >= 0) {
               SelectedIndex = hitTab;
               Focus();
               return;
            }
         }
         base.WndProc(ref pMessage);
         if (pMessage.Msg == 0x000F && TabCount > 0 && DrawMode == TabDrawMode.OwnerDrawFixed) {
            Rectangle stripRect = new Rectangle(0, 0, Width, DisplayRectangle.Top);
            using Graphics g = CreateGraphics();
            using SolidBrush brush = new SolidBrush(mCapturedBackColor);
            using Region stripRegion = new Region(stripRect);
            for (int i = 0; i < TabCount; i++)
               stripRegion.Exclude(GetTabRect(i));
            g.FillRegion(brush, stripRegion);
         }
      }

      public int GetTabStripHeight(int pAvailableWidth) {
         Font font = Font ?? SystemFonts.DefaultFont;
         int singleRowHeight = ItemSize.Height;
         if (singleRowHeight == 0) {
            using Font boldFont = new Font(font, font.Style | FontStyle.Bold);
            FontFamily family = boldFont.FontFamily;
            float emHeight = family.GetEmHeight(boldFont.Style);
            float ascent = family.GetCellAscent(boldFont.Style);
            float descent = family.GetCellDescent(boldFont.Style);
            int vPad = (int)Math.Ceiling(Math.Max(12, boldFont.Size * mFontHeightAdjustment));
            singleRowHeight = (int)Math.Ceiling(boldFont.Size * (ascent + descent) / emHeight) + vPad;
         }
         if (pAvailableWidth <= 0 || TabCount == 0)
            return singleRowHeight;
         List<int> widths = TabHeaderWidths.Count > 0
            ? TabHeaderWidths
            : ComputeTabWidths(font);
         int rows = 1;
         int rowWidth = 0;
         for (int i = 0; i < widths.Count; i++) {
            rowWidth += widths[i];
            if (rowWidth > pAvailableWidth) {
               rows++;
               rowWidth = widths[i];
            }
         }
         return rows * singleRowHeight;
      }

      private List<int> ComputeTabWidths(Font pFont) {
         using Font boldFont = new Font(pFont, pFont.Style | FontStyle.Bold);
         int hPad = (int)Math.Ceiling(Math.Max(20, boldFont.Size * mFontWidthAdjustment));
         List<int> widths = [];
         for (int i = 0; i < TabCount; i++) {
            int w = TextRenderer.MeasureText(TabPages[i].Text, boldFont).Width;
            widths.Add(w + 2 * hPad);
         }
         return widths;
      }

      private int HitTestTabHeaders(Point pMouseLocation) {
         for (int index = 0; index < TabCount; index++) {
            if (GetTabRect(index).Contains(pMouseLocation))
               return index;
         }
         return -1;
      }
   }
}
