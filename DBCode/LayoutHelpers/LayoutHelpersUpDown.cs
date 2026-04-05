namespace DBCode {
   internal static partial class LayoutHelpers {
      internal static bool UpDownHasValue(NumericUpDown? pUpDown) {
         if (pUpDown == null)
            return false;
         return true;
      }

      internal static decimal UpDownValueOrZero(NumericUpDown? pUpDown) {
         if (pUpDown == null)
            return 0m;
         return pUpDown.Value;
      }

      internal static void UpDownSetValue(NumericUpDown? pUpDown, decimal pValue) {
         if (pUpDown == null)
            return;
         if (pValue < pUpDown.Minimum)
            pUpDown.Value = pUpDown.Minimum;
         else if (pValue > pUpDown.Maximum)
            pUpDown.Value = pUpDown.Maximum;
         else
            pUpDown.Value = pValue;
      }

      internal static void UpDownIncrement(NumericUpDown? pUpDown) {
         if (pUpDown == null)
            return;
         decimal newValue = pUpDown.Value + pUpDown.Increment;
         if (newValue > pUpDown.Maximum)
            newValue = pUpDown.Maximum;
         pUpDown.Value = newValue;
      }

      internal static void UpDownDecrement(NumericUpDown? pUpDown) {
         if (pUpDown == null)
            return;
         decimal newValue = pUpDown.Value - pUpDown.Increment;
         if (newValue < pUpDown.Minimum)
            newValue = pUpDown.Minimum;
         pUpDown.Value = newValue;
      }

      internal static void UpDownEnable(NumericUpDown? pUpDown) {
         if (pUpDown == null)
            return;
         pUpDown.Enabled = true;
      }

      internal static void UpDownDisable(NumericUpDown? pUpDown) {
         if (pUpDown == null)
            return;
         pUpDown.Enabled = false;
      }

      internal static bool UpDownIsEnabled(NumericUpDown? pUpDown) {
         if (pUpDown == null)
            return false;
         return pUpDown.Enabled;
      }

      internal static bool UpDownIsDisabled(NumericUpDown? pUpDown) {
         if (pUpDown == null)
            return true;
         return !pUpDown.Enabled;
      }

      internal static void UpDownShow(NumericUpDown? pUpDown) {
         if (pUpDown == null)
            return;
         pUpDown.Visible = true;
      }

      internal static void UpDownHide(NumericUpDown? pUpDown) {
         if (pUpDown == null)
            return;
         pUpDown.Visible = false;
      }

      internal static bool UpDownIsVisible(NumericUpDown? pUpDown) {
         if (pUpDown == null)
            return false;
         return pUpDown.Visible;
      }

      internal static bool UpDownIsHidden(NumericUpDown? pUpDown) {
         if (pUpDown == null)
            return true;
         return !pUpDown.Visible;
      }

      internal static void UpDownSetForeColor(NumericUpDown? pUpDown, Color pColor) {
         if (pUpDown == null)
            return;
         pUpDown.ForeColor = pColor;
      }

      internal static void UpDownSetBackColor(NumericUpDown? pUpDown, Color pColor) {
         if (pUpDown == null)
            return;
         pUpDown.BackColor = pColor;
      }

      internal static void UpDownSetPadding(NumericUpDown? pUpDown, Padding pPadding) {
         if (pUpDown == null)
            return;
         pUpDown.Padding = pPadding;
      }

      internal static void UpDownSetMinimum(NumericUpDown? pUpDown, decimal pMin) {
         if (pUpDown == null)
            return;
         pUpDown.Minimum = pMin;
         if (pUpDown.Value < pMin)
            pUpDown.Value = pMin;
      }

      internal static void UpDownSetMaximum(NumericUpDown? pUpDown, decimal pMax) {
         if (pUpDown == null)
            return;
         pUpDown.Maximum = pMax;
         if (pUpDown.Value > pMax)
            pUpDown.Value = pMax;
      }

      internal static void UpDownSetIncrement(NumericUpDown? pUpDown, decimal pIncrement) {
         if (pUpDown == null)
            return;
         if (pIncrement <= 0m)
            return;
         pUpDown.Increment = pIncrement;
      }
   }
}
