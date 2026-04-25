using DBCode.Themes;

namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabeledButtonColorSwatchCluster : BaseCluster {
         private Button mButton;
         private ColorSwatch mSwatch;
         private Label mLabel;
         private ColorSwatchUsage mUsage;

         public event ColorSwatchClickedHandler? SwatchClicked;

         public LabeledButtonColorSwatchCluster(string pLabelText, string pButtonText, ColorSwatchUsage pUsage,
            LabelPosition pLabelPosition, Color pInitialColor, Color? pBackgroundColor = null) : base(pBackgroundColor) {
            mUsage = pUsage;
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               TextAlign = ContentAlignment.MiddleCenter,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            mButton = new Button() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mButton)}{mTabIndex++}",
               Text = pButtonText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceFont],
               Tag = pUsage
            };
            mButton.Click += Button_Click;
            mSwatch = new ColorSwatch(pUsage, pInitialColor, -1);
            mSwatch.SwatchClicked += Swatch_Click;
            Controls.AddRange(mLabel, mButton, mSwatch);
            LayoutControls();
         }

         public void SetFontAndColor(Theme pTheme) {
            Theme.ThemeSimpleThings(pTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel.Font = poFont;
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
            mButton.Font = poFont;
            mButton.ForeColor = poForeColor;
         }

         private void Button_Click(object? pSender, EventArgs pArgs) {
            SwatchClicked?.Invoke(this, mUsage);
         }

         private void Swatch_Click(object? pSender, ColorSwatchUsage pUsage) {
            SwatchClicked?.Invoke(this, pUsage);
         }

         internal override void LayoutCluster(Theme pTheme) {
            SetFontAndColor(pTheme);
            ApplyLabelPosition(mLabel, mButton);
            GlueControlsHorizontally(mButton, mSwatch, mEm);
            mLabel.Invalidate();
            mButton.Invalidate();
            mSwatch.Invalidate();
         }

         private void LayoutControls() {
            if (mLabelPosition == LabelPosition.Left) {
               mLabel.Left = 0;
               mLabel.Top = 0;
               mButton.Left = mLabel.Right + mEm;
               mButton.Top = 0;
               mSwatch.Left = mButton.Right + mEm;
               mSwatch.Top = 0;
            }
            else if (mLabelPosition == LabelPosition.Right) {
               mButton.Left = 0;
               mButton.Top = 0;
               mSwatch.Left = mButton.Right + mEm;
               mSwatch.Top = 0;
               mLabel.Left = mSwatch.Right + mEm;
               mLabel.Top = 0;
            }
            else if (mLabelPosition == LabelPosition.Top) {
               mLabel.Left = 0;
               mLabel.Top = 0;
               mButton.Left = 0;
               mButton.Top = mLabel.Bottom + mEmHalf;
               mSwatch.Left = mButton.Right + mEm;
               mSwatch.Top = mButton.Top;
            }
            else if (mLabelPosition == LabelPosition.Bottom) {
               mButton.Left = 0;
               mButton.Top = 0;
               mSwatch.Left = mButton.Right + mEm;
               mSwatch.Top = 0;
               mLabel.Left = 0;
               mLabel.Top = Math.Max(mButton.Bottom, mSwatch.Bottom) + mEmHalf;
            }
         }

         public void SetSize(int pSize) {
            mSwatch.SetSize(pSize);
         }

         public Color GetColor() {
            return mSwatch.GetColor();
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               mButton.Click -= Button_Click;
               mSwatch.SwatchClicked -= Swatch_Click;
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
