namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static class MeasureHelper {
         // Returns the width a Button with the given text and font will occupy when AutoSize/GrowAndShrink.
         // Uses a temporary Button so WinForms own PreferredSize calculation (text + OS padding) is exact.
         internal static int ButtonWidth(string pText, Font pFont) {
            using Button temp = new Button {
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = pFont,
               Text = pText
            };
            return temp.PreferredSize.Width;
         }

         // Returns the minimum panel width needed to display a BottomPanel's full button row without
         // overlap, using the same spacing formula as BottomPanel.LayoutControls().
         internal static int BottomPanelNeededWidth(string pHelpText, string pCancelText,
            List<string> pLeftTexts, List<string> pRightTexts, Font pFont) {
            int width = ButtonWidth(pHelpText, pFont) + mEm2;
            foreach (string text in pLeftTexts)
               width += ButtonWidth(text, pFont) + mEm;
            foreach (string text in pRightTexts)
               width += ButtonWidth(text, pFont) + mEm;
            width += ButtonWidth(pCancelText, pFont) + mCancelOffset * 2;
            return width;
         }
      }
   }
}
