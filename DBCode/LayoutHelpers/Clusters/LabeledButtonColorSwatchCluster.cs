namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class LabeledButtonColorSwatchCluster : BaseCluster {
         private Button mButton;
         private ColorSwatch mSwatch;
         private Label mLabel;
         private ColorSwatchUsage mColorUsage = (ColorSwatchUsage)(-1);
         private ColorPickerSwatchUsage mColorPickerUsage = (ColorPickerSwatchUsage)(-1);
         private TokenKind mTokenKind = (TokenKind)(-1);

         public event ColorSwatchClickedHandler? SwatchClicked;
         public event ColorPickerSwatchClickedHandler? PickerSwatchClicked;
         public event SyntaxColorSwatchClickedHandler? SyntaxSwatchClicked;

         public LabeledButtonColorSwatchCluster(Theme pTheme, string pLabelText, string pButtonText, ColorSwatchUsage pUsage,
            LabelPosition pLabelPosition, Color pInitialColor, Color? pBackgroundColor = null) : base(pTheme, pBackgroundColor) {
            mColorUsage = pUsage;
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               TextAlign = ContentAlignment.MiddleCenter,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            mButton = new Button() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mButton)}{mTabIndex++}",
               Text = pButtonText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               Tag = pUsage
            };
            mButton.Click += Button_Click;
            mSwatch = new ColorSwatch(pUsage, pInitialColor, -1);
            mSwatch.ColorSwatchClicked += Swatch_Click;
            Controls.AddRange([mLabel, mButton, mSwatch]);
            LayoutControls();
         }

         public LabeledButtonColorSwatchCluster(Theme pTheme, string pLabelText, string pButtonText,
            ColorPickerSwatchUsage pUsage, LabelPosition pLabelPosition, Color pInitialColor, Color? pBackgroundColor = null)
            : base(pTheme, pBackgroundColor) {
            mColorPickerUsage = pUsage;
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               TextAlign = ContentAlignment.MiddleCenter,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            mButton = new Button() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mButton)}{mTabIndex++}",
               Text = pButtonText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               Tag = pUsage
            };
            mButton.Click += Button_Click;
            mSwatch = new ColorSwatch(pUsage, pInitialColor, -1);
            mSwatch.PickerSwatchClicked += Swatch_Click;
            Controls.AddRange([mLabel, mButton, mSwatch]);
            LayoutControls();
         }

         public LabeledButtonColorSwatchCluster(Theme pTheme, string pLabelText, string pButtonText,
            TokenKind pTokenKind, LabelPosition pLabelPosition, Color pInitialColor, Color? pBackgroundColor = null)
            : base(pTheme, pBackgroundColor) {
            mTokenKind = pTokenKind;
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               TextAlign = ContentAlignment.MiddleCenter,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            mButton = new Button() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mButton)}{mTabIndex++}",
               Text = pButtonText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               Tag = pTokenKind
            };
            mButton.Click += Button_Click;
            mSwatch = new ColorSwatch(pTokenKind, pInitialColor, -1);
            mSwatch.SyntaxSwatchClicked += Swatch_Click;
            Controls.AddRange([mLabel, mButton, mSwatch]);
            LayoutControls();
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel.Font = CreateNewFont(poFont);
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
            mButton.Font = CreateNewFont(poFont);
            mButton.ForeColor = poForeColor;
         }

         private void Button_Click(object? pSender, EventArgs pArgs) {
            if (mColorUsage != (ColorSwatchUsage)(-1))
               SwatchClicked?.Invoke(this, mColorUsage);
            else if (mColorPickerUsage != (ColorPickerSwatchUsage)(-1))
               PickerSwatchClicked?.Invoke(this, mColorPickerUsage);
            else if (mTokenKind != (TokenKind)(-1))
               SyntaxSwatchClicked?.Invoke(this, mTokenKind);
         }

         private void Swatch_Click(object? pSender, ColorSwatchUsage pUsage) {
            SwatchClicked?.Invoke(this, pUsage);
         }

         private void Swatch_Click(object? pSender, ColorPickerSwatchUsage pUsage) {
            PickerSwatchClicked?.Invoke(this, pUsage);
         }

         private void Swatch_Click(object? pSender, TokenKind pTokenKind) {
            SyntaxSwatchClicked?.Invoke(this, pTokenKind);
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
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
               mSwatch.ColorSwatchClicked -= Swatch_Click;
               mSwatch.PickerSwatchClicked -= Swatch_Click;
               mSwatch.SyntaxSwatchClicked -= Swatch_Click;
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
