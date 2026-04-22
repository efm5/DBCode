namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabeledCheckBoxCluster : BaseCluster {
         private Label? mLabel = null;
         private CheckBox? mCheckBox = null;
         internal Color? mBackgroundColor;

         public LabeledCheckBoxCluster(string pLabelText, string pCheckBoxText, bool pInitialChecked, LabelPosition pLabelPosition,
            Color? pBackgroundColor) : base(pBackgroundColor) {
            mBackgroundColor = pBackgroundColor;
            mLabelPosition = pLabelPosition;
            if ((mLabelPosition == LabelPosition.Top) || (mLabelPosition == LabelPosition.Bottom))
               mLabelPosition = LabelPosition.Left;
            mLabel = new Label {
               AutoSize = true,
               Text = pLabelText,
               BackColor = pBackgroundColor ?? Color.Transparent,
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledCheckBoxClusterLabel{mTabIndex++}"
            };
            mCheckBox = new CheckBox {
               Text = pCheckBoxText,
               AutoSize = true,
               Checked = pInitialChecked,
               BackColor = BackColor,
               TabIndex = mTabIndex,
               Name = $"LabeledCheckBoxClusterCheckBox{mTabIndex++}"
            };
            Controls.Add(mLabel);
            Controls.Add(mCheckBox);
            ApplyLabelPosition(mLabel, mCheckBox);
         }

         protected override void LayoutCluster() {
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
