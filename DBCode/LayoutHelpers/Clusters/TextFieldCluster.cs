using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class TextFieldCluster : BaseCluster {
         private Label? mLabel = null;
         private Button? mFlattenedButton = null;
         private TextBox? mTextBox = null;
         internal Color? mBackgroundColor;

         public TextFieldCluster(int pTextBoxWidth, string? pLabelText = null, string? pButtonText = null,
            LabelPosition pLabelPosition = LabelPosition.Left, Color? pBackgroundColor = null) : base(pBackgroundColor) {
            if (((pLabelText == null) && (pButtonText == null)) || ((pLabelText != null) && (pButtonText != null)) ||
               ((pButtonText != null) && pBackgroundColor == null))
               throw new ArgumentException("Invalid parameter combination: must provide either pLabelText or pButtonText (but not both), and pBackgroundColor is required when pButtonText is used");
            mBackgroundColor = pBackgroundColor;
            mTextBox = new TextBox {
               Text = mUnicodeSampleString,
               Width = pTextBoxWidth,
               BackColor = BackColor,
               Name = $"TextFieldClusterTextBox{mTabIndex++}"
            };
            if (pLabelText == null) {
               mFlattenedButton = new Button {
                  Text = pButtonText,
                  AutoSize = true,
                  AutoSizeMode = AutoSizeMode.GrowAndShrink,
                  Location = new Point(0, 0),
                  TabIndex = mTabIndex++,
                  Name = $"TextFieldClusterButton{mTabIndex++}"
               };
               Controls.AddRange(mFlattenedButton, mTextBox!);
               FlattenButton(mFlattenedButton, pBackgroundColor);
               mTextBox.Location = new Point(mFlattenedButton.Right, 0);
            }
            else {
               mLabelPosition = pLabelPosition;
               mLabel = new Label {
                  AutoSize = true,
                  Text = pLabelText,
                  BackColor = pBackgroundColor ?? Color.Transparent,
                  TabIndex = TAB_INDEX_IGNORED
               };
               Controls.AddRange(mLabel, mTextBox!);
               ApplyLabelPosition(mLabel, mTextBox);
            }
            mTextBox.TabIndex = mTabIndex++;
         }

         private void LayoutControls() {
            if (mLabel != null)
               ApplyLabelPosition(mLabel, mTextBox!);
            else
               mTextBox!.Location = new Point(mFlattenedButton!.Right, 0);
         }

         internal override void LayoutCluster(Theme pTheme) {
            SetFontAndColor(pTheme);
            LayoutControls();
            mLabel?.Invalidate();
            mTextBox?.Invalidate();
            mFlattenedButton?.Invalidate();
         }

         public void SetFontAndColor(Theme pTheme) {
            Theme.ThemeSimpleThings(pTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel!.Font = poFont;
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
            mTextBox!.Font = poFont;
            mTextBox.ForeColor = poForeColor;
            mTextBox.BackColor = poBackColor;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               if (mTextBox != null) {
                  mTextBox.Dispose();
                  mTextBox = null;
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
