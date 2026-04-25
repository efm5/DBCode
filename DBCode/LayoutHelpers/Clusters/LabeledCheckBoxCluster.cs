using DBCode.Themes;

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
            Controls.AddRange(mLabel, mCheckBox);
            ApplyLabelPosition(mLabel, mCheckBox);
         }

         internal override void LayoutCluster(Theme pTheme) {
            SetFontAndColor(pTheme);
            ApplyLabelPosition(mLabel!, mCheckBox!);
            mLabel!.Invalidate();
            mCheckBox!.Invalidate();
         }

         public void SetFontAndColor(Theme pTheme) {
            Theme.ThemeInterfaceThings(pTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel!.Font = poFont;
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
            mCheckBox!.Font = poFont;
            mCheckBox.ForeColor = poForeColor;
            mCheckBox.BackColor = poBackColor;
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
