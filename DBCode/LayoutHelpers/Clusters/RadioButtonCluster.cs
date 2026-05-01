using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class RadioButtonCluster : BaseCluster {
         private Label? mLabel = null;
         List<RadioButton> mRadioButtons = [];

         public RadioButtonCluster(Theme pTheme, string pLabelText, LabelPosition pLabelPosition,
            List<string> pRadioButtonNames, int pInitiallyChecked, Color? pBackgroundColor)
            : base(pTheme, pBackgroundColor) {
            mLabelPosition = pLabelPosition;
            mLabel = new Label {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"TitleLabelCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            Controls.Add(mLabel);
            //for each radio button on the Group list create a radio button and add it to the cluster
            int index = 0;
            foreach (string labelName in pRadioButtonNames.OfType<string>()) {
               RadioButton radioButton = new RadioButton() {
                  TabIndex = mTabIndex,
                  Name = $"RadioButtonClusterButton{nameof(radioButton)}{mTabIndex++}",
                  Text = labelName,
                  AutoSize = true,
                  Font = CreateNewFont(),
                  ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
                  BackColor = pBackgroundColor ?? Color.Transparent
               };
               if ((pInitiallyChecked > -1) && (pInitiallyChecked < pRadioButtonNames.Count)) {
                  if (index++ == pInitiallyChecked)
                     radioButton.Checked = true;
               }
               Controls.Add(radioButton);
            }
            LayoutControls();
            ApplyLabelPosition(mLabel, mRadioButtons);
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel!.Font = CreateNewFont(poFont);
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
            foreach (RadioButton radioButton in mRadioButtons) {
               radioButton.Font = CreateNewFont(poFont);
               radioButton.ForeColor = poForeColor;
               radioButton.BackColor = poBackColor;
            }
         }

         internal void LayoutControls() {
            //DEBUG efm5 2026 04 4 do the math – Just the radio buttons
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
            LayoutControls();
            mLabel!.Invalidate();
            foreach (RadioButton radioButton in mRadioButtons)
               radioButton.Invalidate();
         }

         //protected override void Dispose(bool pDisposing) {
         //   if (pDisposing) {
         //      //DEBUG efm5 2026 04 27 if there are ever handlers dispose of them here
         //   }
         //   base.Dispose(pDisposing);
         //}
      }
   }
}
