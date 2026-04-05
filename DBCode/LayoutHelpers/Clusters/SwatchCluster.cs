namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class SwatchCluster : Panel {
         private Label mLabel;
         private Button mButton;
         private ColorSwatch mSwatch;
         private LabelPosition mLabelPosition;
         private int mSpacing = 6;

         public event ColorSwatchClickedHandler? SwatchClicked;

         public SwatchCluster(ColorSwatchUsage pUsage, Color pDefaultColor, string pLabelText,
            LabelPosition pLabelPosition, Color? pBackgroundColor) {
            mLabelPosition = pLabelPosition;
            BackColor = pBackgroundColor ?? Color.Transparent;
            AutoSize = true;
            TabIndex = LayoutHelpers.NextTabIndex();
            Name = "SwatchCluster" + TabIndex;

            mLabel = new Label {
               Text = pLabelText,
               AutoSize = true,
               Font = LayoutHelpers.InterfaceFont()
            };

            mButton = new Button {
               Text = "Select",
               AutoSize = true,
               Font = LayoutHelpers.InterfaceFont()
            };

            mSwatch = new ColorSwatch(pUsage, pDefaultColor, 24, BackColor);
            mSwatch.SwatchClicked += OnSwatchClicked;

            Controls.Add(mLabel);
            Controls.Add(mButton);
            Controls.Add(mSwatch);

            LayoutControls();
         }

         private void OnSwatchClicked(object? pSender, ColorSwatchUsage pUsage) {
            SwatchClicked?.Invoke(this, pUsage);
         }

         protected override void OnLayout(LayoutEventArgs pArgs) {
            base.OnLayout(pArgs);
            LayoutControls();
         }

         private void LayoutControls() {
            int buttonLeft = 0;
            int buttonTop = 0;
            mButton.Left = buttonLeft;
            mButton.Top = buttonTop;

            int swatchLeft = mButton.Right + mSpacing;
            int swatchTop = buttonTop + (mButton.Height - mSwatch.Height) / 2;
            mSwatch.Left = swatchLeft;
            mSwatch.Top = swatchTop;

            int labelLeft = 0;
            int labelTop = 0;

            if (mLabelPosition == LabelPosition.Left) {
               labelLeft = 0;
               labelTop = (mButton.Height - mLabel.Height) / 2;
               mLabel.Left = labelLeft;
               mLabel.Top = labelTop;
               mButton.Left = mLabel.Right + mSpacing;
               mSwatch.Left = mButton.Right + mSpacing;
            }
            else if (mLabelPosition == LabelPosition.Right) {
               labelLeft = mSwatch.Right + mSpacing;
               labelTop = (mButton.Height - mLabel.Height) / 2;
               mLabel.Left = labelLeft;
               mLabel.Top = labelTop;
            }
            else if (mLabelPosition == LabelPosition.Top) {
               labelLeft = mButton.Left;
               labelTop = 0;
               mLabel.Left = labelLeft;
               mLabel.Top = labelTop;
               mButton.Top = mLabel.Bottom + mSpacing;
               mSwatch.Top = mButton.Top + (mButton.Height - mSwatch.Height) / 2;
            }
            else if (mLabelPosition == LabelPosition.Bottom) {
               labelLeft = mButton.Left;
               labelTop = Math.Max(mButton.Bottom, mSwatch.Bottom) + mSpacing;
               mLabel.Left = labelLeft;
               mLabel.Top = labelTop;
            }

            int width = Math.Max(mLabel.Right, Math.Max(mSwatch.Right, mButton.Right));
            int height = Math.Max(mLabel.Bottom, Math.Max(mSwatch.Bottom, mButton.Bottom));
            Width = width;
            Height = height;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing)
               mSwatch.SwatchClicked -= OnSwatchClicked;
            base.Dispose(pDisposing);
         }
      }
   }
}
