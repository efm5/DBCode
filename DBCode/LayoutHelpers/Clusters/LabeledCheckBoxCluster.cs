namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabeledCheckBoxCluster : BaseCluster {
         private Label? mLabel = null;
         private CheckBox? mCheckBox = null;
         internal Color? mBackgroundColor;

         public LabeledCheckBoxCluster(Theme pTheme, string pLabelText, string pCheckBoxText, bool pInitialChecked,
            LabelPosition pLabelPosition, Color? pBackgroundColor) : base(pTheme, pBackgroundColor) {
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
            Controls.AddRange([mLabel, mCheckBox]);
            ApplyLabelPosition(mLabel, mCheckBox);
         }

         internal override void LayoutCluster() {
            ThrowIfNull(mLabel, nameof(mLabel));
            ThrowIfNull(mCheckBox, nameof(mCheckBox));
            SetFontAndColor();
            ApplyLabelPosition(mLabel, mCheckBox);
            mLabel.Invalidate();
            mCheckBox.Invalidate();
         }

         internal override void SetFontAndColor() {
            ThrowIfNull(mLabel, nameof(mLabel));
            ThrowIfNull(mCheckBox, nameof(mCheckBox));
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel.Font = CreateNewFont(poFont);
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
            mCheckBox.Font = CreateNewFont(poFont);
            mCheckBox.ForeColor = poForeColor;
            mCheckBox.BackColor = poBackColor;
         }
      }
   }
}
