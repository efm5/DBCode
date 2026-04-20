namespace DBCode {
   internal static partial class LayoutHelpers {

      internal sealed class LabeledButtonColorSwatchCluster : BaseCluster {
         private Button mButton;
         private ColorSwatch mSwatch;
         private Label mLabel;

         public event ColorSwatchClickedHandler? SwatchClicked;

         public LabeledButtonColorSwatchCluster(string pLabelText, string pButtonText, ColorSwatchUsage pUsage,
            LabelPosition pLabelPosition, Color pInitialColor, Color? pBackgroundColor = null) : base(pBackgroundColor) {
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = LayoutHelpers.TAB_INDEX_IGNORED,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               Top = mEm,
               TextAlign = ContentAlignment.MiddleCenter,
               Height = 40,
               Font = LayoutHelpers.CreateNewFont(),
               ForeColor = mCurrentTheme!.mColors[(int)ColorUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            mButton = new Button() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mButton)}{mTabIndex++}",
               Text = pButtonText,
               Top = mEm,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = LayoutHelpers.CreateNewFont(),
               ForeColor = mCurrentTheme!.mColors[(int)ColorUsage.InterfaceFont]
            };
            mSwatch = new ColorSwatch(pUsage, pInitialColor, pBackgroundColor);
            mSwatch.SwatchClicked += OnSwatchClicked;
            Controls.AddRange(mLabel, mButton, mSwatch);
            LayoutControls();
         }

         private void OnSwatchClicked(object? pSender, ColorSwatchUsage pUsage) {
            SwatchClicked?.Invoke(this, pUsage);
         }

         protected override void OnLayout(LayoutEventArgs pArgs) {
            base.OnLayout(pArgs);
            LayoutControls();
         }

         protected override void LayoutCluster() {
            // Layout handled in LayoutControls()
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
               mButton.Top = mLabel.Bottom + mEm;

               mSwatch.Left = mButton.Right + mEm;
               mSwatch.Top = mButton.Top;
            }
            else if (mLabelPosition == LabelPosition.Bottom) {
               mButton.Left = 0;
               mButton.Top = 0;

               mSwatch.Left = mButton.Right + mEm;
               mSwatch.Top = 0;

               mLabel.Left = 0;
               mLabel.Top = Math.Max(mButton.Bottom, mSwatch.Bottom);
            }
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing)
               mSwatch.SwatchClicked -= OnSwatchClicked;
            base.Dispose(pDisposing);
         }
      }
   }
}
