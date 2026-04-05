namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabeledCheckBoxCluster : BaseCluster {
         private Label? mLabel = null;
         private CheckBox? mCheckBox = null;

         public LabeledCheckBoxCluster(string pLabelText, bool pInitialChecked, LabelPosition pLabelPosition,
            Color? pBackgroundColor) : base(pBackgroundColor) {
            mLabelPosition = pLabelPosition;
            mLabel = new Label {
               AutoSize = true,
               Text = pLabelText,
               BackColor = BackColor,
               TabIndex = LayoutHelpers.TAB_INDEX_IGNORED,
               Name = $"LabeledCheckBoxClusterLabel{TabIndex}"
            };
            mCheckBox = new CheckBox {
               AutoSize = true,
               Checked = pInitialChecked,
               BackColor = BackColor,
               TabIndex = LayoutHelpers.NextTabIndex(),
               Name = $"LabeledCheckBoxClusterCheckBox{TabIndex}"
            };
            //add controls before layout math
            Controls.Add(mLabel);
            Controls.Add(mCheckBox);
            ApplyLabelPosition(mLabel, mCheckBox);
            FinalizeSize(mLabel, mCheckBox);
         }

         public CheckBox? CheckBoxControl() {
            return mCheckBox;
         }

         public Label? LabelControl() {
            return mLabel;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               if (mCheckBox != null) {
                  mCheckBox.Dispose();
                  mCheckBox = null;
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
