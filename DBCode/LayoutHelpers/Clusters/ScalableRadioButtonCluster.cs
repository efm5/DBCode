namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class ScalableRadioButtonCluster : BaseCluster {
         internal ScalableRadioButtons.RadioPanel mRadioPanel;

         internal ScalableRadioButtonCluster(Theme pTheme, List<ScalableRadioButtons.RadioButtonQuad> pQuads,
            bool pInterface, bool pHorizontal = true, int pMaximumWidth = 0, Color? pBackgroundColor = null)
            : base(pTheme, pBackgroundColor) {
            mRadioPanel = new ScalableRadioButtons.RadioPanel(pQuads, pTheme, pInterface, pHorizontal, pMaximumWidth) {
               Name = $"ScalableRadioButtonCluster{nameof(mRadioPanel)}{mTabIndex}",
               TabIndex = TAB_INDEX_IGNORED
            };
            Controls.Add(mRadioPanel);
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
            mRadioPanel.LayoutRadioPanel();
            mRadioPanel.Location = new Point(0, 0);
         }

         internal override void SetFontAndColor() {
            mRadioPanel.PaintButtons();
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               foreach (ScalableRadioButtons.ScalableRadioButton button in mRadioPanel.mScalableRadioButtons)
                  MainForm.DisposeFontIfOwned(button.Font);
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
