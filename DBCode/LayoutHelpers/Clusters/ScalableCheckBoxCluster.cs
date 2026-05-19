namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class ScalableCheckBoxCluster : BaseCluster {
         internal ScalableCheckBox mScalableCheckBox;

         internal ScalableCheckBoxCluster(Theme pTheme, string pText, bool pInterface, Color? pBackgroundColor = null)
            : base(pTheme, pBackgroundColor) {
            mScalableCheckBox = new ScalableCheckBox(pText, pTheme, pInterface) {
               Name = $"ScalableCheckBoxCluster{nameof(mScalableCheckBox)}{mTabIndex}",
               TabIndex = mTabIndex++
            };
            Controls.Add(mScalableCheckBox);
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
            mScalableCheckBox.Location = new Point(0, 0);
         }

         internal override void SetFontAndColor() {
            mScalableCheckBox.SetFontAndColors(mTheme);
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing)
               MainForm.DisposeFontIfOwned(mScalableCheckBox.Font);
            base.Dispose(pDisposing);
         }
      }
   }
}
