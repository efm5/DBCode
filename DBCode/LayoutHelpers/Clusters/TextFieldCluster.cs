namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class TextFieldCluster : BaseCluster {
         private Label? mLabel = null;
         private TextBox? mTextBox = null;
         internal Color? mBackgroundColor;

         public TextFieldCluster(string pLabelText, int pTextBoxWidth, LabelPosition pLabelPosition,
            Color? pBackgroundColor) : base(pBackgroundColor) {
            mBackgroundColor = pBackgroundColor;
            mLabelPosition = pLabelPosition;
            mLabel = new Label {
               AutoSize = true,
               Text = pLabelText,
               BackColor = BackColor,
               TabIndex = LayoutHelpers.TAB_INDEX_IGNORED
            };
            mTextBox = new TextBox {
               Text = mUnicodeSampleString,
               Width = pTextBoxWidth,
               BackColor = BackColor,
               TabIndex = LayoutHelpers.NextTabIndex()
            };
            Controls.Add(mLabel);
            Controls.Add(mTextBox);
            ApplyLabelPosition(mLabel, mTextBox);
         }

         protected override void LayoutCluster() {
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
