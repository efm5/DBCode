namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class TextFieldCluster : BaseCluster {
         private Label? mLabel = null;
         private TextBox? mTextBox = null;

         public TextFieldCluster(string pLabelText, string pInitialText, int pTextBoxWidth, LabelPosition pLabelPosition,
            Color? pBackgroundColor) : base(pBackgroundColor) {

            mLabelPosition = pLabelPosition;
            mLabel = new Label {
               AutoSize = true,
               Text = pLabelText,
               BackColor = BackColor,
               TabIndex = LayoutHelpers.TAB_INDEX_IGNORED
            };
            mTextBox = new TextBox {
               Text = pInitialText,
               Width = pTextBoxWidth,
               BackColor = BackColor,
               TabIndex = LayoutHelpers.NextTabIndex()
            };

            //add controls before layout math
            Controls.Add(mLabel);
            Controls.Add(mTextBox);
            //position label and textbox
            ApplyLabelPosition(mLabel, mTextBox);
            FinalizeSize(mLabel, mTextBox);
         }

         public TextBox? TextBoxControl() {
            return mTextBox;
         }

         public Label? LabelControl() {
            return mLabel;
         }

         public string CurrentText() {
            if (mTextBox == null)
               return string.Empty;
            return mTextBox.Text;
         }

         public void SetText(string pValue) {
            if (mTextBox != null)
               mTextBox.Text = pValue;
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
