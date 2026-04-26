using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class RichTextFieldCluster : BaseCluster {
         private Label? mLabel = null;
         private Button? mFlattenedButton = null;
         private RichTextBox? mRichTextBox = null;
         internal Color? mBackgroundColor;

         public RichTextFieldCluster(Theme pTheme, int pTextBoxWidth, string? pLabelText = null, string? pButtonText = null,
            LabelPosition pLabelPosition = LabelPosition.Left, Color? pBackgroundColor = null)
            : base(pTheme, pBackgroundColor) {
            if (((pLabelText == null) && (pButtonText == null)) || ((pLabelText != null) && (pButtonText != null)) ||
               ((pButtonText != null) && pBackgroundColor == null))
               throw new ArgumentException("Invalid parameter combination: must provide either pLabelText or pButtonText (but not both), and pBackgroundColor is required when pButtonText is used");
            mBackgroundColor = pBackgroundColor;
            mRichTextBox = new RichTextBox {
               Text = mUnicodeSampleString,
               Width = pTextBoxWidth,
               Name = $"RichTextFieldClusterRichTextBox{mTabIndex++}",
               Multiline = true,
               ScrollBars = RichTextBoxScrollBars.Both
            };
            if (pLabelText == null) {
               mFlattenedButton = new Button {
                  Text = pButtonText,
                  AutoSize = true,
                  AutoSizeMode = AutoSizeMode.GrowAndShrink,
                  Location = new Point(0, 0),
                  TabIndex = mTabIndex++,
                  Name = $"RichTextFieldClusterButton{mTabIndex++}"
               };
               Controls.AddRange(mFlattenedButton, mRichTextBox!);
               FlattenButton(mFlattenedButton, pBackgroundColor);
               mRichTextBox.Location = new Point(mFlattenedButton.Right, 0);
            }
            else {
               mLabelPosition = pLabelPosition;
               mLabel = new Label {
                  AutoSize = true,
                  Text = pLabelText,
                  TabIndex = TAB_INDEX_IGNORED
               };
               Controls.AddRange(mLabel, mRichTextBox!);
               ApplyLabelPosition(mLabel, mRichTextBox);
            }
            mRichTextBox.TabIndex = mTabIndex++;
         }

         private void LayoutControls() {
            if (mLabel != null)
               ApplyLabelPosition(mLabel, mRichTextBox!);
            else
               mRichTextBox!.Location = new Point(mFlattenedButton!.Right, 0);
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
            LayoutControls();
            mLabel?.Invalidate();
            mRichTextBox?.Invalidate();
            mFlattenedButton?.Invalidate();
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel!.Font = poFont;
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
            mRichTextBox!.Font = poFont;
            mRichTextBox.ForeColor = poForeColor;
            mRichTextBox.BackColor = poBackColor;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               if (mRichTextBox != null) {
                  mRichTextBox.Dispose();
                  mRichTextBox = null;
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
