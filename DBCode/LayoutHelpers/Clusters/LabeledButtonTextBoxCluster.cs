namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabeledButtonTextBoxCluster : BaseCluster {
         internal Label mLabel { get; private set; }
         internal Button mButton { get; private set; }
         internal TextBox mExampleTextBox { get; private set; }

         internal LabeledButtonTextBoxCluster(Theme pTheme, string pLabelText, string pButtonText,
            LabelPosition pLabelPosition, Color? pBackgroundColor = null) : base(pTheme, pBackgroundColor) {
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledButtonTextBoxCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               TextAlign = ContentAlignment.MiddleCenter,
               AutoSize = true
            };
            mButton = new Button() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonTextBoxCluster{nameof(mButton)}{mTabIndex++}",
               Text = pButtonText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };
            mExampleTextBox = new TextBox() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonTextBoxCluster{nameof(mExampleTextBox)}{mTabIndex++}",
               Width = 300,
               Text = mUnicodeSampleString,
               Multiline = false,
            };
            Controls.AddRange([mLabel, mButton, mExampleTextBox]);
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
            ApplyLabelPosition(mLabel, mButton, mExampleTextBox);
            mLabel.Invalidate();
            mButton.Invalidate();
            mExampleTextBox.Invalidate();
            mLabel.Refresh();
            mButton.Refresh();
            mExampleTextBox.Refresh();
         }

         internal override void SetFontAndColor() {
            LabeledButtonTextBoxCluster Me = this;
            object? tag = Tag;
            ThrowIfNull(tag, $"Tag was null for {nameof(LabeledButtonTextBoxCluster)}. This should never happen.");

            if (tag is FontUsage) {
               FontUsage fontUsage = (FontUsage)tag;
               Font poFont;
               Color poForeColor;
               Color poBackColor;

               switch (fontUsage) {
                  case FontUsage.Interface:
                  default:
                     Theme.ThemeInterfaceThings(mTheme, out poFont, out poForeColor, out poBackColor);
                     break;
                  case FontUsage.Menu:
                     Theme.ThemeMenuThings(mTheme, out poFont, out poForeColor, out poBackColor);
                     break;
                  case FontUsage.Status:
                     Theme.ThemeStatusThings(mTheme, out poFont, out poForeColor, out poBackColor);
                     break;
                  case FontUsage.Text:
                     Theme.ThemeTextBoxThings(mTheme, out poFont, out poForeColor, out poBackColor);
                     break;
               }
               mLabel.Font = CreateNewFont(poFont);
               mLabel.ForeColor = poForeColor;
               mLabel.BackColor = poBackColor;
               mButton.Font = CreateNewFont(poFont);
               mButton.ForeColor = poForeColor;
               mButton.BackColor = poBackColor;
               mExampleTextBox.Font = CreateNewFont(poFont);
               mExampleTextBox.ForeColor = poForeColor;
               mExampleTextBox.BackColor = poBackColor;
            }
         }

         public void UpdateLabel(string pNewText) {
            mLabel.Text = pNewText;
         }
      }
   }
}
