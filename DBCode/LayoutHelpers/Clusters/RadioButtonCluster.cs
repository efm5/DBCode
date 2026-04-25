using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class RadioButtonCluster : BaseCluster {
         private Label? mLabel = null;
         List<RadioButton> mRadioButtons = [];
         internal Color? mBackgroundColor;

         public RadioButtonCluster(string pLabelText, LabelPosition pLabelPosition, List<string> pRadioButtonNames, int pInitiallyChecked,
            Color? pBackgroundColor) : base(pBackgroundColor) {
            mBackgroundColor = pBackgroundColor;
            mLabelPosition = pLabelPosition;
            mLabel = new Label {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"TitleLabelCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont],
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
                  ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont],
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

         public void SetFontAndColor(Theme pTheme) {
            Theme.ThemeInterfaceThings(pTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel!.Font = poFont;
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
            foreach (RadioButton radioButton in mRadioButtons) {
               radioButton.Font = poFont;
               radioButton.ForeColor = poForeColor;
               radioButton.BackColor = poBackColor;
            }
         }

         internal void LayoutControls() {
            //DEBUG efm5 2026 04 4 do the math – Just the radio buttons use
         }

         internal override void LayoutCluster(Theme pTheme) {
            SetFontAndColor(pTheme);
            LayoutControls();
            mLabel!.Invalidate();
            foreach (RadioButton radioButton in mRadioButtons)
               radioButton.Invalidate();
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               if (mRadioButtons != null) {
                  foreach (RadioButton radioButton in mRadioButtons) {
                     radioButton.Dispose();
                  }
                  mRadioButtons.Clear();
               }
               if (mLabel != null) {
                  mLabel.Dispose();
                  mLabel = null;
               }
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
