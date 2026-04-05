namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool PictureBoxHasImage(PictureBox? pPictureBox) {
         if (pPictureBox == null)
            return false;
         return pPictureBox.Image != null;
      }

      internal static bool PictureBoxHasNoImage(PictureBox? pPictureBox) {
         if (pPictureBox == null)
            return true;
         return pPictureBox.Image == null;
      }

      internal static Image? PictureBoxImageOrNull(PictureBox? pPictureBox) {
         if (pPictureBox == null)
            return null;
         return pPictureBox.Image;
      }

      internal static void PictureBoxSetImage(PictureBox? pPictureBox, Image? pImage) {
         if (pPictureBox == null)
            return;
         pPictureBox.Image = pImage;
      }

      internal static void PictureBoxClearImage(PictureBox? pPictureBox) {
         if (pPictureBox == null)
            return;
         pPictureBox.Image = null;
      }

      internal static void PictureBoxEnable(PictureBox? pPictureBox) {
         if (pPictureBox == null)
            return;
         pPictureBox.Enabled = true;
      }

      internal static void PictureBoxDisable(PictureBox? pPictureBox) {
         if (pPictureBox == null)
            return;
         pPictureBox.Enabled = false;
      }

      internal static bool PictureBoxIsEnabled(PictureBox? pPictureBox) {
         if (pPictureBox == null)
            return false;
         return pPictureBox.Enabled;
      }

      internal static bool PictureBoxIsDisabled(PictureBox? pPictureBox) {
         if (pPictureBox == null)
            return true;
         return !pPictureBox.Enabled;
      }

      internal static void PictureBoxShow(PictureBox? pPictureBox) {
         if (pPictureBox == null)
            return;
         pPictureBox.Visible = true;
      }

      internal static void PictureBoxHide(PictureBox? pPictureBox) {
         if (pPictureBox == null)
            return;
         pPictureBox.Visible = false;
      }

      internal static bool PictureBoxIsVisible(PictureBox? pPictureBox) {
         if (pPictureBox == null)
            return false;
         return pPictureBox.Visible;
      }

      internal static bool PictureBoxIsHidden(PictureBox? pPictureBox) {
         if (pPictureBox == null)
            return true;
         return !pPictureBox.Visible;
      }

      internal static void PictureBoxSetSizeMode(PictureBox? pPictureBox, PictureBoxSizeMode pMode) {
         if (pPictureBox == null)
            return;
         pPictureBox.SizeMode = pMode;
      }

      internal static void PictureBoxSetBorderStyle(PictureBox? pPictureBox, BorderStyle pStyle) {
         if (pPictureBox == null)
            return;
         pPictureBox.BorderStyle = pStyle;
      }

      internal static void PictureBoxSetPadding(PictureBox? pPictureBox, Padding pPadding) {
         if (pPictureBox == null)
            return;
         pPictureBox.Padding = pPadding;
      }

      internal static void PictureBoxSetBackColor(PictureBox? pPictureBox, Color pColor) {
         if (pPictureBox == null)
            return;
         pPictureBox.BackColor = pColor;
      }
   }
}
