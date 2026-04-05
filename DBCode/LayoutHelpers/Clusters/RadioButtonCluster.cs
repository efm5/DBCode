namespace DBCode {
   internal static partial class LayoutHelpers {
      internal class RadioButtonGroup {
         public List<string> mRadioButtonNames = [];
         public int mInitiallyChecked = 0;
         public LabelPosition mLabelPosition = LabelPosition.Left;

#pragma warning disable IDE0290 // Use primary constructor
         public RadioButtonGroup(List<string> pRadioButtonNames, int pInitiallyChecked, LabelPosition pLabelPosition) {
            mRadioButtonNames = pRadioButtonNames;
            mInitiallyChecked = pInitiallyChecked;
            mLabelPosition = pLabelPosition;
         }
#pragma warning restore IDE0290 // Use primary constructor
      }

      internal sealed class RadioButtonCluster : BaseCluster {
         private RadioButtonGroup? mRadioButtonGroup = null;
         private Label? mLabel = null;
         private RadioButton? mRadioButton = null;

         public RadioButtonCluster(string pLabelText, RadioButtonGroup pRadioButtonGroup, LabelPosition pLabelPosition,
            Color? pBackgroundColor) : base(pBackgroundColor) {
            mLabelPosition = pLabelPosition;
            mLabel = new Label {
               AutoSize = true,
               Text = pLabelText,
               BackColor = BackColor,
               TabIndex = LayoutHelpers.TAB_INDEX_IGNORED,
               Name = $"RadioButtonClusterLabel{TabIndex}"
            };
            Controls.Add(mLabel);

            //for each radio button on the Group list create a radio button and add it to the cluster
            int index = 0;
            foreach (string labelName in pRadioButtonGroup.mRadioButtonNames.OfType<string>()) {
               RadioButton radioButton = new RadioButton() {
                  AutoSize = true,
                  Checked = false,
                  BackColor = BackColor,
                  Tag = labelName,
                  TabIndex = LayoutHelpers.NextTabIndex(),
                  Name = $"RadioButtonClusterButton{TabIndex}"
               };
               if (index == pRadioButtonGroup.mInitiallyChecked)
                  radioButton.Checked = true;
               Controls.Add(radioButton);
            }
            LayoutControls();
            FinalizeGroupSize();
         }

         internal void LayoutControls() {
            //DEBUG efm5 2026 04 4 do the math
         }

         internal void FinalizeGroupSize() {
            //DEBUG efm5 2026 04 4 do the math
            int width = 0;
            int height = 0;

            //DEBUG efm5 2026 04 4 use dynamic padding An account for the list of radio buttons

            //if (mLabelPosition == LabelPosition.Left || mLabelPosition == LabelPosition.Right) {
            //   width = pLabel.Width + pControl.Width + 6;
            //   height = Math.Max(pLabel.Height, pControl.Height);
            //}
            //else {
            //   width = Math.Max(pLabel.Width, pControl.Width);
            //   height = pLabel.Height + pControl.Height + 4;
            //}
            Size = new Size(width, height);
         }

         public RadioButtonGroup? RadioButtonGroupl() {
            return mRadioButtonGroup;
         }

         public Label? LabelControl() {
            return mLabel;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               if (mRadioButton != null) {
                  mRadioButton.Dispose();
                  mRadioButton = null;
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
